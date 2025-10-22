using System;
using System.Reflection;
using BarberFlow.Application.UseCases.Billings.Reports.Pdf.Fonts;
using BarberFlow.Application.UseCases.Billings.Reports.Pdf.Helpers;
using BarberFlow.Domain.Enums;
using BarberFlow.Domain.Repositories.Billings;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;

namespace BarberFlow.Application.UseCases.Billings.Reports.Pdf;

public class GenerateBillingPdfReportUseCase : IGenerateBillingPdfReportUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private const int HEIGHT_ROW_BILLING_TABLE = 25;
    private readonly IBillingsReadOnlyRepository _billingsRepository;
    private readonly List<string> _tempFiles = [];

    public GenerateBillingPdfReportUseCase(IBillingsReadOnlyRepository billingsRepository)
    {
        _billingsRepository = billingsRepository;
        GlobalFontSettings.FontResolver = new BillingsReportFontResolver();
    }
    public async Task<byte[]> ExecuteAsync(DateOnly month)
    {
        var billings = await _billingsRepository.GetBillingsByMonthAsync(month);

        billings = [.. billings.Where(b => b.Status == BillingStatus.Pago)];

        if (!billings.Any()) return [];

        var document = CreateDocument(month);
        var page = CreatePage(document);

        CreateHeaderWithProfilePhotoAndName(page);

        var totalBillings = billings.Sum(b => b.Amount);

        CreateTotaBillingSection(page, totalBillings);

        CreateBody(page, billings);

        var result = RenderDocument(document);

        CleanupTempFiles();

        return result;
    }

    #region CreateDocument
    private static Document CreateDocument(DateOnly month)
    {
        var document = new Document();
        document.Info.Title = $"Relatório de Faturamento - {month:MM/yyyy}";
        document.Info.Author = "Gabriel Henrique";

        var style = document.Styles["Normal"]!;
        style.Font.Name = FontsHelper.ROBOTO_REGULAR;

        return document;
    }

    #endregion

    #region CreatePage
    private static Section CreatePage(Document document)
    {
        var section = document.AddSection();

        section.PageSetup = document.DefaultPageSetup.Clone();
        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.TopMargin = 40;
        section.PageSetup.BottomMargin = 40;
        section.PageSetup.LeftMargin = 35;
        section.PageSetup.RightMargin = 35;

        return section;
    }

    #endregion

    #region CreateHeader
    private void CreateHeaderWithProfilePhotoAndName(Section section)
    {
        var table = section.AddTable();
        table.AddColumn();
        table.AddColumn("300");

        var row = table.AddRow();
        row.VerticalAlignment = VerticalAlignment.Center;

        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "BarberFlow.Application.UseCases.Billings.Reports.Pdf.Resources.profile.png";

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream != null)
        {
            var tempFileName = Path.GetTempFileName();
            using (var fileStream = File.Create(tempFileName))
            {
                stream.CopyTo(fileStream);
            }

            _tempFiles.Add(tempFileName);
            row.Cells[0].AddImage(tempFileName);
        }

        row.Cells[1].AddParagraph("Barbearia Top Flow");
        row.Cells[1].Format.Font = new Font { Name = FontsHelper.BEBAS_NEUE_REGULAR, Size = 25 };
    }
    #endregion CreateHeader

    #region CreateTotalBillingSection
    private static void CreateTotaBillingSection(Section section, decimal totalBillings)
    {
        var paragraph = section.AddParagraph();
        paragraph.Format.SpaceBefore = 40;
        paragraph.Format.SpaceAfter = 8;

        paragraph.Format.Alignment = ParagraphAlignment.Left;
        paragraph.Format.Font = new Font { Name = FontsHelper.ROBOTO_MEDIUM, Size = 15 };
        paragraph.AddText("Faturamento da Semana");

        var totalParagraph = section.AddParagraph();
        totalParagraph.Format.SpaceAfter = 64;

        totalParagraph.Format.Alignment = ParagraphAlignment.Left;
        totalParagraph.Format.Font = new Font { Name = FontsHelper.BEBAS_NEUE_REGULAR, Size = 50 };
        totalParagraph.AddText($"{CURRENCY_SYMBOL} {totalBillings:N2}");
    }
    #endregion

    #region CreateBody
    private static void CreateBody(Section section, IEnumerable<Domain.Entities.Billing> billings)
    {
        foreach (var billing in billings)
        {
            var table = CreateBillingTable(section);
            var row = table.AddRow();
            row.Height = HEIGHT_ROW_BILLING_TABLE;
            row.VerticalAlignment = VerticalAlignment.Center;

            AddBillingTitle(row.Cells[0], billing.ServiceName);
            AddHeaderForAmount(row.Cells[3]);

            row = table.AddRow();
            FillRowWithBillingData(row, billing);

            if (!string.IsNullOrWhiteSpace(billing.Notes))
            {
                var notesRow = table.AddRow();
                notesRow.Height = HEIGHT_ROW_BILLING_TABLE;
                notesRow.Cells[0].AddParagraph(billing.Notes);
                notesRow.Cells[0].MergeRight = 2;
                notesRow.Cells[0].Shading.Color = ColorHelper.LIGHT_GRAY;
                notesRow.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                notesRow.Cells[0].Format.LeftIndent = 9;
                notesRow.Cells[0].Format.Font = new Font
                {
                    Name = FontsHelper.ROBOTO_REGULAR,
                    Size = 10,
                    Color = ColorHelper.FONT_GRAY
                };
            }

            AddWhiteSpace(table);
        }
    }
    #endregion

    #region CreateBillingTable
    private static Table CreateBillingTable(Section section)
    {
        var table = section.AddTable();

        table.AddColumn("175").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("100").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("175").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("70").Format.Alignment = ParagraphAlignment.Right;

        return table;
    }
    #endregion

    #region AddBillingTitle
    private static void AddBillingTitle(Cell cell, string title)
    {
        cell.AddParagraph(title);
        cell.Format.Font = new Font
        {
            Name = FontsHelper.BEBAS_NEUE_REGULAR,
            Size = 12,
            Color = ColorHelper.WHITE
        };
        cell.Shading.Color = ColorHelper.DARK_GREEN;
        cell.MergeRight = 2;
        cell.Format.LeftIndent = 7;
    }
    #endregion

    #region AddHeaderForAmount
    private static void AddHeaderForAmount(Cell cell)
    {
        cell.AddParagraph("Valor");
        cell.Format.Font = new Font
        {
            Name = FontsHelper.BEBAS_NEUE_REGULAR,
            Size = 15,
            Color = ColorHelper.WHITE
        };
        cell.Shading.Color = ColorHelper.LIGHT_GREEN;
    }
    #endregion

    #region FillRowWithBillingData
    private static void FillRowWithBillingData(Row row, Domain.Entities.Billing billing)
    {
        row.VerticalAlignment = VerticalAlignment.Center;
        row.Height = HEIGHT_ROW_BILLING_TABLE;

        row.Cells[0].AddParagraph($"{billing.Date:dd 'de' MMMM 'de' yyyy}");
        SetStyleBaseForBillingDataCell(row.Cells[0]);

        row.Cells[1].AddParagraph(billing.CreatedAt.ToString("HH:mm"));
        SetStyleBaseForBillingDataCell(row.Cells[1]);

        row.Cells[2].AddParagraph(billing.PaymentMethod.ToString());
        SetStyleBaseForBillingDataCell(row.Cells[2]);

        row.Cells[3].AddParagraph($"{CURRENCY_SYMBOL} {billing.Amount:N2}");
        SetStyleBaseForBillingDataCell(row.Cells[3], true);

    }
    #endregion

    #region SetStyleBaseForBillingDataCell
    private static void SetStyleBaseForBillingDataCell(Cell cell, bool isAmountCell = false)
    {
        cell.Format.Font = new Font
        {
            Name = FontsHelper.ROBOTO_REGULAR,
            Size = 10,
            Color = ColorHelper.BLACK
        };
        cell.Shading.Color = isAmountCell ? ColorHelper.GRAY : ColorHelper.WHITE;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.Format.LeftIndent = 6;
    }
    #endregion

    #region AddWhiteSpace
    private static void AddWhiteSpace(Table table)
    {
        var spacerRow = table.AddRow();
        spacerRow.Height = 16;
        spacerRow.Cells[0].Borders.Visible = false;
    }
    #endregion

    #region RenderDocument
    private byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document
        };

        try
        {
            renderer.RenderDocument();
        }
        catch (System.Exception ex)
        {
            throw new System.Exception("Erro ao renderizar o documento PDF.", ex);
        }

        using var file = new MemoryStream();
        renderer.PdfDocument.Save(file);


        return file.ToArray();
    }
    #endregion

    #region CleanupTempFiles
    private void CleanupTempFiles()
    {
        foreach (var tempFile in _tempFiles)
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }

        _tempFiles.Clear();
    }
    #endregion
}

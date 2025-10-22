using BarberFlow.Domain.Entities;
using BarberFlow.Domain.Enums;
using BarberFlow.Domain.Repositories.Billings;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;

namespace BarberFlow.Application.UseCases.Billings.Reports.Excel;

public class GenerateBillingExcelReportUseCase : IGenerateBillingExcelReportUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private readonly IBillingsReadOnlyRepository _repository;

    public GenerateBillingExcelReportUseCase(IBillingsReadOnlyRepository repository)
    {
        _repository = repository;
    }
    public async Task<byte[]> ExecuteAsync(DateOnly month)
    {
        var billings = await _repository.GetBillingsByMonthAsync(month);

        if (!billings.Any()) return [];

        billings = [.. billings.Where(b => b.Status != BillingStatus.Cancelado)];

        using var workbook = new XLWorkbook
        {
            Author = "Gabriel Henrique"
        };

        workbook.Style.Font.FontName = "Times New Roman";
        workbook.Style.Font.FontSize = 12;

        var workSheet = workbook.Worksheets.Add($"Relatório de Faturamento {month:MM-Y}");

        InsertHeader(workSheet);
        InsertBody(workSheet, billings);

        var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray();

    }

    private static void InsertHeader(IXLWorksheet workSheet)
    {
        workSheet.Cell("A1").Value = "Título";
        workSheet.Cell("B1").Value = "Data";
        workSheet.Cell("C1").Value = "Tipo de pagamento";
        workSheet.Cell("D1").Value = "Valor";
        workSheet.Cell("E1").Value = "Descrição";

        var headerRange = workSheet.Range("A1:E1");
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#305757");
        headerRange.Style.Font.FontColor = XLColor.White;
        headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
    }

    private static void InsertBody(IXLWorksheet workSheet, IEnumerable<Billing> billings)
    {
        foreach (var billing in billings)
        {
            var lastRowUsed = workSheet.LastRowUsed();
            workSheet.Cell($"A{lastRowUsed.RowNumber() + 1}").Value = billing.ServiceName;
            workSheet.Cell($"B{lastRowUsed.RowNumber() + 1}").Value = billing.Date.ToString("dd/MM/yyyy");
            workSheet.Cell($"C{lastRowUsed.RowNumber() + 1}").Value = billing.PaymentMethod.ToString();
            workSheet.Cell($"D{lastRowUsed.RowNumber() + 1}").Value = $"{CURRENCY_SYMBOL} {billing.Amount:F2}";
            workSheet.Cell($"E{lastRowUsed.RowNumber() + 1}").Value = billing.Notes;
        }

        workSheet.Columns().AdjustToContents();
    }
}

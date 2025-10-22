using System.Reflection;
using PdfSharp.Fonts;
using PdfSharp.Quality;

namespace BarberFlow.Application.UseCases.Billings.Reports.Pdf.Fonts;

public class BillingsReportFontResolver : IFontResolver
{
    public byte[]? GetFont(string faceName)
    {
        var stream = ReadFontFile(faceName);
        stream ??= ReadFontFile(FontsHelper.DEFAULT_FONT_FAMILY);

        var length = stream!.Length;

        var data = new byte[length];

        stream.Read(buffer: data, offset: 0, count: (int)length);

        return data;
    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo(familyName);
    }

    private static Stream? ReadFontFile(string faceName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        return assembly.GetManifestResourceStream($"BarberFlow.Application.UseCases.Billings.Reports.Pdf.Fonts.{faceName}.ttf");
    }

}

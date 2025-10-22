using MigraDoc.DocumentObjectModel;

namespace BarberFlow.Application.UseCases.Billings.Reports.Pdf.Helpers;

public abstract class ColorHelper
{
    public static readonly Color BLACK = Color.Parse("#000000");
    public static readonly Color WHITE = Color.Parse("#FFFFFF");
    public static readonly Color GRAY = Color.Parse("#D7DFDF");
    public static readonly Color LIGHT_GRAY = Color.Parse("#EDF3F3");
    public static readonly Color FONT_GRAY = Color.Parse("#7D7D7D");
    public static readonly Color DARK_GREEN = Color.Parse("#1A2B2B");
    public static readonly Color LIGHT_GREEN = Color.Parse("#205858");

}

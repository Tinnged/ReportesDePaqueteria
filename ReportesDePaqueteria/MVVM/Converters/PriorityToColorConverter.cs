using System.Globalization;

namespace ReportesDePaqueteria.MVVM.Converters;

public class PriorityToColorConverter : IValueConverter
{
    private static readonly Lazy<PriorityToColorConverter> _instance =
        new Lazy<PriorityToColorConverter>(() => new PriorityToColorConverter());

    public static PriorityToColorConverter Instance => _instance.Value;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string priority)
        {
            return priority switch
            {
                "Alta" => Color.FromArgb("#F44336"),
                "Media" => Color.FromArgb("#FF9800"),
                "Baja" => Color.FromArgb("#4CAF50"),
                _ => Color.FromArgb("#666666")
            };
        }
        return Color.FromArgb("#666666");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
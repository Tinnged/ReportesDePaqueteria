using System.Globalization;

namespace ReportesDePaqueteria.MVVM.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                string format = parameter as string ?? "dd/MM/yyyy HH:mm";

                // Special formats
                if (format == "relative")
                {
                    var diff = DateTime.Now - dateTime;

                    if (diff.TotalMinutes < 60)
                        return $"Hace {(int)diff.TotalMinutes} minutos";
                    if (diff.TotalHours < 24)
                        return $"Hace {(int)diff.TotalHours} horas";
                    if (diff.TotalDays < 7)
                        return $"Hace {(int)diff.TotalDays} días";

                    return dateTime.ToString("dd/MM/yyyy");
                }

                return dateTime.ToString(format);
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

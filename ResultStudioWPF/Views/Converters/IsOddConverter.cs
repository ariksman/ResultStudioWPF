using System;
using System.Windows.Data;
using System.Windows.Media;

namespace ResultStudioWPF.Views.Converters
{
    public class IsOddConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return false;

            int index = (int)value;
            return IsOdd(index);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ResultStudioWPF.Helpers;
using ResultStudioWPF.ViewModels;

namespace ResultStudioWPF.Views.Converters
{
    public class MeasurementToBrushConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double input = values[0] as double? ?? 0;
            Constants.MeasurementAxis axis = values[1] as Constants.MeasurementAxis? ?? Constants.MeasurementAxis.X;

            var toleranceX = new ViewModelLocator().SettingsViewModel.XAxisTolerance;
            var referenceX = new ViewModelLocator().SettingsViewModel.XAxisReference;

            var toleranceY = new ViewModelLocator().SettingsViewModel.YAxisTolerance;
            var referenceY = new ViewModelLocator().SettingsViewModel.YAxisReference;

            var toleranceZ = new ViewModelLocator().SettingsViewModel.ZAxisTolerance;
            var referenceZ = new ViewModelLocator().SettingsViewModel.ZAxisReference;

            switch (axis)
            {
                case Constants.MeasurementAxis.X:
                    return CheckValueTolerance(input, referenceX, toleranceX);
                    break;
                case Constants.MeasurementAxis.Y:
                    return CheckValueTolerance(input, referenceY, toleranceY);
                    break;
                case Constants.MeasurementAxis.Z:
                    return CheckValueTolerance(input, referenceZ, toleranceZ);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="reference"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        private static object CheckValueTolerance(double input, double reference, double tolerance)
        {
            if (input > (reference + tolerance))
            {
                return Brushes.Crimson;
            }
            if (input < (reference - tolerance))
            {
                return Brushes.Crimson;
            }

            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

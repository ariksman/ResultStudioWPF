using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using ResultStudioWPF.Domain.DomainModel.Enumerations;
using ResultStudioWPF.ViewModels;

namespace ResultStudioWPF.Views.Converters
{
  public class MeasurementToBrushConverter : IMultiValueConverter
  {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      var input = values[0] as double? ?? 0;
      var axis = values[1] as MeasurementAxisType ?? MeasurementAxisType.X;

      var toleranceX = ViewModelLocator.Instance.SettingsViewModel.XAxisTolerance;
      var referenceX = ViewModelLocator.Instance.SettingsViewModel.XAxisReference;

      var toleranceY = ViewModelLocator.Instance.SettingsViewModel.YAxisTolerance;
      var referenceY = ViewModelLocator.Instance.SettingsViewModel.YAxisReference;

      var toleranceZ = ViewModelLocator.Instance.SettingsViewModel.ZAxisTolerance;
      var referenceZ = ViewModelLocator.Instance.SettingsViewModel.ZAxisReference;

      if (axis == MeasurementAxisType.X)
        return CheckValueTolerance(MeasurementAxisType.IsValid(input, referenceX, toleranceX));
      if (axis == MeasurementAxisType.Y)
        return CheckValueTolerance(MeasurementAxisType.IsValid(input, referenceY, toleranceY));
      if (axis == MeasurementAxisType.Z)
        return CheckValueTolerance(MeasurementAxisType.IsValid(input, referenceZ, toleranceZ));

      throw new ArgumentOutOfRangeException();
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static object CheckValueTolerance(bool valid)
    {
      return !valid ? Brushes.Crimson : DependencyProperty.UnsetValue;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
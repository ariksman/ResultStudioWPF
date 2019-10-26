using System.Collections.ObjectModel;

namespace ResultStudioWPF.ViewModels
{
  public class DataSetModel : ISharedSettingsContext
  {
    public bool ProgressBarIsIndetermined { get; set; }
    public int ProgressBarValue { get; set; }
    public string FilePath { get; set; }
    public double XAxisTolerance { get; set; }
    public double YAxisTolerance { get; set; }
    public double ZAxisTolerance { get; set; }
    public double XAxisReference { get; set; }
    public double YAxisReference { get; set; }
    public double ZAxisReference { get; set; }
    public double XVariance { get; set; }
    public double YVariance { get; set; }
    public double ZVariance { get; set; }
    public int ErrorCount { get; set; }

    public ObservableCollection<MeasurementPointViewModel> DataSet { get; set; } = new ObservableCollection<MeasurementPointViewModel>();
  }
}
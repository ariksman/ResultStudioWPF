using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ResultStudioWPF.ViewModels
{
  public interface ISharedSettingsContext
  {
    double XAxisReference { get; }
    double YAxisReference { get; }
    double ZAxisReference { get; }

    ObservableCollection<MeasurementPointViewModel> DataSet { get; }
  }
}
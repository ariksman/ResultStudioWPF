using System;
using System.Collections.ObjectModel;

namespace ResultStudioWPF.Models.Services
{
  public interface IDataCreator
  {
    ObservableCollection<MeasurementPointViewModel> CreateSubframeDataset(double referenceX, double referenceY, double referenceZ, int subframeCount, double spread, IProgress<int> progress);
  }
}
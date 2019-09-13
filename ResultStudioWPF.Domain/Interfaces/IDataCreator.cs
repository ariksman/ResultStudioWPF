using System;
using System.Collections.ObjectModel;
using ResultStudioWPF.Domain.DomainModel;
using ResultStudioWPF.Domain.DomainModel.ValueObjects;

namespace ResultStudioWPF.Domain.Interfaces
{
  public interface IDataCreator
  {
    ObservableCollection<IMeasurementPoint> CreateSubframeDataset(Reference reference, int subframeCount, double spread, IProgress<int> progress);
  }
}
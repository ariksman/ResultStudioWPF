using System;
using System.Collections.Generic;
using ResultStudioWPF.Domain.DomainModel.Entities;
using ResultStudioWPF.Domain.DomainModel.ValueObjects;

namespace ResultStudioWPF.Domain.Interfaces
{
  public interface IDataCreator
  {
    List<MeasurementPoint> CreateSubframeDataset(Reference reference, int subframeCount, double spread, IProgress<int> progress);
  }
}
using System.Collections.Generic;
using ResultStudioWPF.Domain.DomainModel.Enumerations;
using ResultStudioWPF.Domain.Interfaces;

namespace ResultStudioWPF.Application.Interfaces
{
  public interface IAnalyzeDataSet
  {
    IEnumerable<IMeasurementPoint> DataSet { set; }

    double CalculateDataVariance(MeasurementAxisType axis);
  }
}
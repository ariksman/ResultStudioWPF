using System.Collections.Generic;
using ResultStudioWPF.Application.Helpers;
using ResultStudioWPF.Domain.DomainModels.Enumerations;

namespace ResultStudioWPF.Application.Interfaces
{
  public interface IAnalyseDataSet
  {
    IEnumerable<IMeasurementPoint> DataSet { set; }

    double CalculateDataVariance(MeasurementAxisType axis);
  }
}
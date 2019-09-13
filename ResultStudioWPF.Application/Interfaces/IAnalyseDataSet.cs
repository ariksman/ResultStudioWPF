using System.Collections.Generic;
using ResultStudioWPF.Domain.DomainModel;
using ResultStudioWPF.Domain.DomainModel.Enumerations;

namespace ResultStudioWPF.Application.Interfaces
{
  public interface IAnalyseDataSet
  {
    IEnumerable<IMeasurementPoint> DataSet { set; }

    double CalculateDataVariance(MeasurementAxisType axis);
  }
}
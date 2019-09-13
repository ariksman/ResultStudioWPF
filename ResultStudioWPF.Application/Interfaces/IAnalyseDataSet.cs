using System.Collections.Generic;
using ResultStudioWPF.Domain.DomainModel;
using ResultStudioWPF.Domain.DomainModel.Enumerations;
using ResultStudioWPF.Domain.Interfaces;

namespace ResultStudioWPF.Application.Interfaces
{
  public interface IAnalyseDataSet
  {
    IEnumerable<IMeasurementPoint> DataSet { set; }

    double CalculateDataVariance(MeasurementAxisType axis);
  }
}
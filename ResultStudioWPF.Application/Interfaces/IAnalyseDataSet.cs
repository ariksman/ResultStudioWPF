using System.Collections.Generic;
using ResultStudioWPF.Application.Helpers;

namespace ResultStudioWPF.Application.Interfaces
{
  public interface IAnalyseDataSet
  {
    IEnumerable<IMeasurementPoint> DataSet { set; }

    double CalculateDataVariance(Constants.MeasurementAxis axis);
  }
}
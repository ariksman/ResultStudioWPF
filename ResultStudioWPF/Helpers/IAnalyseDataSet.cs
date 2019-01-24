using System.Collections.Generic;
using ResultStudioWPF.Models;

namespace ResultStudioWPF.Helpers
{
  public interface IAnalyseDataSet
  {
    IEnumerable<MeasurementPoint> DataSet { set; }

    double CalculateDataVariance(Constants.MeasurementAxis axis);
  }
}
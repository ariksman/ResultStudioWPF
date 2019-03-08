using System.Collections.Generic;
using ResultStudioWPF.Helpers;
using ResultStudioWPF.Models;

namespace ResultStudioWPF.ViewModels.Services
{
  public interface IAnalyseDataSet
  {
    IEnumerable<MeasurementPoint> DataSet { set; }

    double CalculateDataVariance(Constants.MeasurementAxis axis);
  }
}
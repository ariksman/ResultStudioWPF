using System.Collections.Generic;
using ResultStudioWPF.Models;

namespace ResultStudioWPF.Helpers
{
  public interface IDataFileReader
  {
    IList<MeasurementPoint> DataSet { get; set; }
    string TheFile { get; set; }
  }
}
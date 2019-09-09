using System.Collections.Generic;
using ResultStudioWPF.Models;

namespace ResultStudioWPF.ViewModels.Services
{
  public interface IDataFileReader
  {
    void ReadFile();

    IList<MeasurementPointViewModel> DataSet { get; set; }
    string TheFile { get; set; }
  }
}
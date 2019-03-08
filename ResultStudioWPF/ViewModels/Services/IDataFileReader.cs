using System;
using System.Collections.Generic;
using ResultStudioWPF.Models;

namespace ResultStudioWPF.ViewModels.Services
{
  public interface IDataFileReader
  {
    void ReadFile();

    IList<MeasurementPoint> DataSet { get; set; }
    string TheFile { get; set; }
    IProgress<int> ReportProgress { get; set; }
  }
}
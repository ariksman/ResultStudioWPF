using ResultStudioWPF.Application.Helpers;

namespace ResultStudioWPF.Application.Interfaces
{
  public interface IMeasurementPoint  
  {
    Constants.MeasurementAxis AxisName { get; set; }
    double Value { get; set; }
  }
}


using ResultStudioWPF.Domain.DomainModel.Enumerations;

namespace ResultStudioWPF.Domain.Interfaces
{
  public interface IMeasurementPoint  
  {
    int Index { get; set; }
    MeasurementAxisType Axis { get; set; }
    double Value { get; set; }
  }
}

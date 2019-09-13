
using ResultStudioWPF.Domain.DomainModel.Enumerations;

namespace ResultStudioWPF.Domain.DomainModel
{
  public interface IMeasurementPoint  
  {
    int Index { get; set; }
    MeasurementAxisType Axis { get; set; }
    double Value { get; set; }
  }
}

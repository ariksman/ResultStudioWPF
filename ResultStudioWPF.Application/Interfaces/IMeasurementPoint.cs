
using ResultStudioWPF.Domain.DomainModel.Enumerations;

namespace ResultStudioWPF.Application.Interfaces
{
  public interface IMeasurementPoint  
  {
    MeasurementAxisType AxisName { get; set; }
    double Value { get; set; }
  }
}

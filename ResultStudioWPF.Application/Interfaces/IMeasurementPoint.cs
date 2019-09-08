
using ResultStudioWPF.Domain;
using ResultStudioWPF.Domain.DomainModels.Enumerations;

namespace ResultStudioWPF.Application.Interfaces
{
  public interface IMeasurementPoint  
  {
    MeasurementAxisType AxisName { get; set; }
    double Value { get; set; }
  }
}

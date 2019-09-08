using ResultStudioWPF.Domain.DomainModel.Enumerations;

namespace ResultStudioWPF.Domain.Services
{
  public class ValueValidatorService : IMeasurementPointValueValidator
  {
    public bool IsValid(MeasurementAxisType measurementAxisType, Tolerance tolerance, Reference reference)
    {
      return measurementAxisType.IsValid(tolerance, reference);
    }
  }

  public interface IMeasurementPointValueValidator
  {
    bool IsValid(MeasurementAxisType measurementAxisType, Tolerance tolerance, Reference reference);
  }
}
using ResultStudioWPF.Domain.DomainModel.Enumerations;
using ResultStudioWPF.Domain.DomainModel.ValueObjects;

namespace ResultStudioWPF.Domain.PureServices
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
using ResultStudioWPF.Domain.DomainModel.Enumerations;
using ResultStudioWPF.Domain.DomainModel.ValueObjects;

namespace ResultStudioWPF.ViewModels
{
  public class MeasurementPointModel
  {
    public int Index { get; set; }
    public MeasurementAxisType MeasurementAxisType { get; set; }
    public Tolerance Tolerance { get; set; }
    public double Value { get; set; }
  }
}
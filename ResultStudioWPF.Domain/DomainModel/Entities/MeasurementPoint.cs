using ResultStudioWPF.Domain.DDD;
using ResultStudioWPF.Domain.DomainModel.Enumerations;

namespace ResultStudioWPF.Domain.DomainModel.Entities
{
  public class MeasurementPoint : Entity
  {

    public int IndexNumber { get; set; }
    public double Value { get; set; }
    public MeasurementAxisType MeasurementAxisType { get; set; }
    public Tolerance Tolerance { get; set; }
    public Reference Reference { get; set; }
  }
}
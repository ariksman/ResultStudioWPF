using ResultStudioWPF.Domain.DDD;

namespace ResultStudioWPF.Domain.DomainModel.Entities
{
  public class MeasurementPoint : Entity
  {

    public int IndexNumber { get; set; }
    public Tolerance Tolerance { get; set; }
    public Reference Reference { get; set; }
  }
}
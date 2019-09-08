using System.Collections.Generic;
using ResultStudioWPF.Domain.DDD;

namespace ResultStudioWPF.Domain.DomainModel.Entities
{
  public class DataSet : AggregateRoot
  {
    public string Name { get; set; }
    public IReadOnlyList<MeasurementPoint> MeasurementPoints { get; set; }
  }
}

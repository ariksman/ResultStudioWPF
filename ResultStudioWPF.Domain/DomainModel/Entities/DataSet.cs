using System.Collections.Generic;
using System.Linq;
using ResultStudioWPF.Domain.DDD;
using ResultStudioWPF.Domain.DomainModel.Enumerations;

namespace ResultStudioWPF.Domain.DomainModel.Entities
{
  public class DataSet : AggregateRoot
  {
    public string Name { get; set; }
    public IReadOnlyList<MeasurementPoint> MeasurementPoints { get; set; }

    public double CalculateDataVariance(MeasurementAxisType axis)
    {
      var data = MeasurementPoints
        .Where(m => m.MeasurementAxisType.Value == axis.Value)
        .Select(m => m.Value)
        .ToList();

      return axis.CalculateStandardDeviation(data);
    }
  }
}

using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ResultStudioWPF.Domain.DDD;

namespace ResultStudioWPF.Domain
{
  public class DataSet : AggregateRoot
  {
    public string Name { get; set; }
    public IReadOnlyList<MeasurementPoint> MeasurementPoints { get; set; }
  }
}

using System.Collections.Generic;
using ResultStudioWPF.Domain.DDD;

namespace ResultStudioWPF.Domain
{
  public class MeasurementPoint : ValueObject
  {

    public int IndexNumber { get; set; }  


    protected override IEnumerable<object> GetEqualityComponents()
    {
      throw new System.NotImplementedException();
    }
  }
}
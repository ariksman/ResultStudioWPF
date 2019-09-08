using System.Collections.Generic;
using ResultStudioWPF.Domain.DDD;

namespace ResultStudioWPF.Domain
{
  public class Reference : ValueObject
  {
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }


    protected override IEnumerable<object> GetEqualityComponents()
    {
      yield return X;
      yield return Y;
      yield return Z;
    }
  }
}
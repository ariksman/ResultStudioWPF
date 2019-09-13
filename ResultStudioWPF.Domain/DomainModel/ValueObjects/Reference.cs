using System.Collections.Generic;
using ResultStudioWPF.Domain.DDD;

namespace ResultStudioWPF.Domain
{
  public class Reference : ValueObject
  {
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public Reference(double x, double y, double z)
    {
      X = x;
      Y = y;
      Z = z;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
      yield return X;
      yield return Y;
      yield return Z;
    }
  }
}
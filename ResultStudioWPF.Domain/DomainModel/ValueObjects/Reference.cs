using System.Collections.Generic;
using ResultStudioWPF.Domain.DDD;

namespace ResultStudioWPF.Domain.DomainModel.ValueObjects
{
  public class Reference : ValueObject
  {
    public double X { get; }
    public double Y { get; }
    public double Z { get; }

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
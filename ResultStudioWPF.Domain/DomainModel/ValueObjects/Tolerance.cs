using System.Collections.Generic;
using ValueObject = ResultStudioWPF.Domain.DDD.ValueObject;

namespace ResultStudioWPF.Domain.DomainModel.ValueObjects
{
  public class Tolerance : ValueObject
  {
    public double X { get; }
    public double Y { get; }
    public double Z { get; }

    public Tolerance(double x, double y, double z)
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
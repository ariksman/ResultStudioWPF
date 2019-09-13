using System.Collections.Generic;
using ValueObject = ResultStudioWPF.Domain.DDD.ValueObject;

namespace ResultStudioWPF.Domain.DomainModel.ValueObjects
{
  public class Tolerance : ValueObject
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
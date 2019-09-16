using System;
using System.Collections.Generic;
using System.Linq;
using ResultStudioWPF.Domain.DomainModel.ValueObjects;

namespace ResultStudioWPF.Domain.DomainModel.Enumerations
{
  public abstract class MeasurementAxisType : Enumeration
  {
    public static MeasurementAxisType X = new XAxisType();
    public static MeasurementAxisType Y = new YAxisType();
    public static MeasurementAxisType Z = new ZAxisType();
    public static MeasurementAxisType UnknownType = new UnknownTypeType();

    public MeasurementAxisType() {}

    public MeasurementAxisType(int id, string name): base(id, name) {}

    public static MeasurementAxisType GetType(string axis)
    {
      if (string.Equals(axis, "X", StringComparison.OrdinalIgnoreCase)) return new XAxisType();
      if (string.Equals(axis, "Y", StringComparison.OrdinalIgnoreCase)) return new YAxisType();
      if (string.Equals(axis, "Z", StringComparison.OrdinalIgnoreCase)) return new ZAxisType();

      return MeasurementAxisType.UnknownType;
    }

    public static bool IsValid(double input, double reference, double tolerance)
    {
      if (input > (reference + tolerance))
      {
        return false;
      }
      if (input < (reference - tolerance))
      {
        return false;
      }

      return true;
    }

    public abstract double CalculateStandardDeviation(IEnumerable<double> data);
    public abstract bool IsValid(Tolerance tolerance, Reference reference);

    private class XAxisType : MeasurementAxisType
    {
      public XAxisType() : base(0, "X-axis")
      {
        
      }
      public override double CalculateStandardDeviation(IEnumerable<double> data)
      {
        return StandardDeviation(data);
      }

      public override bool IsValid(Tolerance tolerance, Reference reference)
      {
        if (Value > (reference.X + tolerance.X)) return false;
        if (Value < (reference.X - tolerance.X)) return false;

        return true;
      }
    }

    private class YAxisType : MeasurementAxisType
    {
      public YAxisType() : base(1, "Y-axis")
      {
        
      }
      public override double CalculateStandardDeviation(IEnumerable<double> data)
      {
        return StandardDeviation(data);
      }

      public override bool IsValid(Tolerance tolerance, Reference reference)
      {
        if (Value > (reference.Y + tolerance.Y)) return false;
        if (Value < (reference.Y - tolerance.Y)) return false;

        return true;
      }
    }

    private class ZAxisType : MeasurementAxisType
    {
      public ZAxisType() : base(2, "Z-axis")
      {
        
      }
      public override double CalculateStandardDeviation(IEnumerable<double> data)
      {
        return StandardDeviation(data);
      }

      public override bool IsValid(Tolerance tolerance, Reference reference)
      {
        if (Value > (reference.Z + tolerance.Z)) return false;
        if (Value < (reference.Z - tolerance.Z)) return false;

        return true;
      }
    }

    private class UnknownTypeType : MeasurementAxisType
    {
      public override double CalculateStandardDeviation(IEnumerable<double> data)
      {
        throw new NotImplementedException();
      }

      public override bool IsValid(Tolerance tolerance, Reference reference)
      {
        throw new NotImplementedException();
      }
    }

    private double StandardDeviation(IEnumerable<double> values)
    {
      var localValues = values.ToList();
      double avg = localValues.Average();
      return Math.Sqrt(localValues.Average(v => Math.Pow(v - avg, 2)));
    }
  }


}
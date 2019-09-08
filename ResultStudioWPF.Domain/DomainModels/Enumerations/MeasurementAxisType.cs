using System;
using System.Collections.Generic;
using System.Linq;

namespace ResultStudioWPF.Domain.DomainModels.Enumerations
{
  public abstract class MeasurementAxisType : Enumeration
  {
    public static MeasurementAxisType X = new XAxisType();
    public static MeasurementAxisType Y = new YAxisType();
    public static MeasurementAxisType Z = new ZAxisType();
    public static MeasurementAxisType UnknownType = new UnknownTypeType();

    public MeasurementAxisType()
    {
      
    }

    public MeasurementAxisType(int id, string name): base(id, name)
    {
      
    }

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


    private class XAxisType : MeasurementAxisType
    {

      public override double CalculateStandardDeviation(IEnumerable<double> data)
      {
        return StandardDeviation(data);
      }
    }

    private class YAxisType : MeasurementAxisType
    {

      public override double CalculateStandardDeviation(IEnumerable<double> data)
      {
        return StandardDeviation(data);
      }
    }

    private class ZAxisType : MeasurementAxisType
    {

      public override double CalculateStandardDeviation(IEnumerable<double> data)
      {
        return StandardDeviation(data);
      }
    }

    private class UnknownTypeType : MeasurementAxisType
    {
      public override double CalculateStandardDeviation(IEnumerable<double> data)
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
using ResultStudioWPF.Domain.DDD;
using ResultStudioWPF.Domain.DomainModel.Enumerations;
using ResultStudioWPF.Domain.Interfaces;

namespace ResultStudioWPF.Domain.DomainModel.Entities
{
  public class MeasurementPoint : Entity, IMeasurementPoint
  {

    public MeasurementPoint(
      int index,
      double value,
      MeasurementAxisType axis)
    {
      Index = index;
      Value = value;
      Axis = axis;
    }

    public MeasurementPoint(
      int index, 
      double value, 
      MeasurementAxisType axis, 
      Tolerance tolerance, 
      Reference reference)
    {
      Index = index;
      Value = value;
      Axis = axis;
      Tolerance = tolerance;
      Reference = reference;
    }

    public int Index { get; set; }
    public double Value { get; set; }
    public MeasurementAxisType Axis { get; set; }
    public Tolerance Tolerance { get; set; }
    public Reference Reference { get; set; }
  }
}
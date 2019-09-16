using ResultStudioWPF.Domain.DDD;
using ResultStudioWPF.Domain.DomainModel.Enumerations;
using ResultStudioWPF.Domain.DomainModel.ValueObjects;
using ResultStudioWPF.Domain.Interfaces;

namespace ResultStudioWPF.Domain.DomainModel.Entities
{
  public class MeasurementPoint : Entity
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

    public int Index { get; }
    public double Value { get; }
    public MeasurementAxisType Axis { get; }
    public Tolerance Tolerance { get; }
    public Reference Reference { get; }
  }
}
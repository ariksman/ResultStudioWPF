using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using CSharpFunctionalExtensions;
using ResultStudioWPF.Domain.DDD;
using ResultStudioWPF.Domain.DomainModel.Enumerations;

namespace ResultStudioWPF.Domain.DomainModel.Entities
{
  public class DataSet : AggregateRoot
  {
    public string Name { get; }
    public IReadOnlyList<MeasurementPoint> MeasurementPoints { get; }

    private DataSet(string name, IEnumerable<MeasurementPoint> measurementPoints)
    {
      Name = name;
      MeasurementPoints = new List<MeasurementPoint>(measurementPoints);
    }

    public static Result<DataSet> Create(Maybe<string> nameOrNothing, IEnumerable<MeasurementPoint> measurementPoints)
    {
      return nameOrNothing.ToResult("Name is compulsory")
        .Map(name => name)
        .Ensure(name => !string.IsNullOrWhiteSpace(name), "")
        .Ensure((name) => measurementPoints != null, "Data can not be null")
        .Map(name => new DataSet(name, measurementPoints));
    }

    public double CalculateVariance(MeasurementAxisType axis)
    {
      var data = MeasurementPoints
        .Where(m => m.Axis.Value == axis.Value)
        .Select(m => m.Value)
        .ToList();

      return axis.CalculateStandardDeviation(data);
    }
  }
}

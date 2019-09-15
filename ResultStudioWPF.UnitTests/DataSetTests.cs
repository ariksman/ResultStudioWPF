using System.Collections.Generic;
using NUnit.Framework;
using ResultStudioWPF.Domain.DomainModel.Entities;
using ResultStudioWPF.Domain.DomainModel.Enumerations;

namespace ResultStudioWPF.UnitTests
{
  [TestFixture]
  public class DataSetTests
  {
    [Test]
    public void CalculateDataVariance_WhenCalled_ReturnVarianceOfDataSetArgument()
    {
      // Arrange
      var measurementSet = new List<MeasurementPoint>()
      {
        new MeasurementPoint(1, 5, MeasurementAxisType.X),
        new MeasurementPoint(2, 5, MeasurementAxisType.X),
        new MeasurementPoint(3, 5, MeasurementAxisType.X),
      };
      var dataSet = DataSet.Create("test Data", measurementSet);

      // Act
      var result = dataSet.Value.CalculateVariance(MeasurementAxisType.X);

      // Assert
      Assert.That(result, Is.EqualTo(0));
      Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void CalculateDataVariance_WhenCalled_ReturnVarianceOfDataContainingDuplicateMeasurements()
    {
      // Arrange
      var measurementSet = new List<MeasurementPoint>()
      {
        new MeasurementPoint(1, 5, MeasurementAxisType.X),
        new MeasurementPoint(2, 5, MeasurementAxisType.X),
        new MeasurementPoint(2, 5, MeasurementAxisType.X),
      };
      var dataSet = DataSet.Create("test Data", measurementSet);

      // Act
      var result = dataSet.Value.CalculateVariance(MeasurementAxisType.X);

      // Assert
      Assert.That(result, Is.EqualTo(0));
      Assert.That(result, Is.Not.Null);
    }
  }
}

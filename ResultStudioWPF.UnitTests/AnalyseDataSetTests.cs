using System;
using System.Collections.Generic;
using NUnit.Framework;
using ResultStudioWPF.Domain.DomainModel.Enumerations;
using ResultStudioWPF.Models;
using ResultStudioWPF.ViewModels.Services;

namespace ResultStudioWPF.UnitTests
{
  [TestFixture]
  public class AnalyseDataSetTests
  {
    [Test]
    public void CalculateDataVariance_WhenCalled_ReturnVarianceOfDataSetArgument()
    {
      // Arrange
      var dataSet = new List<MeasurementPointViewModel>()
      {
        new MeasurementPointViewModel()
        {
          Index = 1,
          Axis = MeasurementAxisType.X,
          Value = 5,
        },
        new MeasurementPointViewModel()
        {
          Index = 2,
          Axis = MeasurementAxisType.X,
          Value = 5,
        },
        new MeasurementPointViewModel()
        {
          Index = 3,
          Axis = MeasurementAxisType.X,
          Value = 5,
        },
      };
      var dataAnalyser = new AnalyzeDataSet { DataSet = dataSet };

      // Act
      var result = dataAnalyser.CalculateDataVariance(MeasurementAxisType.X);

      // Assert
      Assert.That(result, Is.EqualTo(0));
      Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void CalculateDataVariance_WhenCalled_ReturnVarianceOfDataContainingDuplicateMeasurements()
    {
      // Arrange
      var dataSet = new List<MeasurementPointViewModel>()
      {
        new MeasurementPointViewModel()
        {
          Index = 1,
          Axis = MeasurementAxisType.X,
          Value = 5,
        },
        new MeasurementPointViewModel()
        {
          Index = 2,
          Axis = MeasurementAxisType.X,
          Value = 5,
        },
        new MeasurementPointViewModel()
        {
          Index = 2,
          Axis = MeasurementAxisType.X,
          Value = 5,
        },
      };
      var dataAnalyzer = new AnalyzeDataSet {DataSet = dataSet};

      // Act
      var result = dataAnalyzer.CalculateDataVariance(MeasurementAxisType.X);

      // Assert
      Assert.That(result, Is.EqualTo(0));
      Assert.That(result, Is.Not.Null);
    }
  }
}

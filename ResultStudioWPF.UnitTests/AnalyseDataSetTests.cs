using System;
using System.Collections.Generic;
using NUnit.Framework;
using ResultStudioWPF.Helpers;
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
            var dataSet = new List<MeasurementPoint>()
            {
                new MeasurementPoint()
                {
                    MeasurementNumber = 1,
                    AxisName = Constants.MeasurementAxis.X,
                    Value = 5,
                },
                new MeasurementPoint()
                {
                    MeasurementNumber = 2,
                    AxisName = Constants.MeasurementAxis.X,
                    Value = 5,
                },
                new MeasurementPoint()
                {
                    MeasurementNumber = 3,
                    AxisName = Constants.MeasurementAxis.X,
                    Value = 5,
                },
            };
            var datasetAnalyser = new AnalyseDataSet(dataSet);


            // Act
            var result = datasetAnalyser.CalculateDataVariance(Constants.MeasurementAxis.X);

            // Assert
            Assert.That(result, Is.EqualTo(0));
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void CalculateDataVariance_WhenCalled_ReturnVarianceOfDataContainingDuplicateMeasurements()
        {
            // Arrange
            var dataSet = new List<MeasurementPoint>()
            {
                new MeasurementPoint()
                {
                    MeasurementNumber = 1,
                    AxisName = Constants.MeasurementAxis.X,
                    Value = 5,
                },
                new MeasurementPoint()
                {
                    MeasurementNumber = 2,
                    AxisName = Constants.MeasurementAxis.X,
                    Value = 5,
                },
                new MeasurementPoint()
                {
                    MeasurementNumber = 2,
                    AxisName = Constants.MeasurementAxis.X,
                    Value = 5,
                },
            };
            var datasetAnalyser = new AnalyseDataSet(dataSet);

            // Act
            var result = datasetAnalyser.CalculateDataVariance(Constants.MeasurementAxis.X);

            // Assert
            Assert.That(result, Is.EqualTo(0));
            Assert.That(result, Is.Not.Null);
        }
    }
}

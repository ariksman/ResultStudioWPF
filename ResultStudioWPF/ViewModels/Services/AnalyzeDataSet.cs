using System;
using System.Collections.Generic;
using ResultStudioWPF.Application;
using ResultStudioWPF.Application.Helpers;
using ResultStudioWPF.Application.Interfaces;
using ResultStudioWPF.Models;

namespace ResultStudioWPF.ViewModels.Services
{
  public class AnalyzeDataSet : IAnalyseDataSet
  {
    private IList<double> _dataSetX = new List<double>();
    private IList<double> _dataSetY = new List<double>();
    private IList<double> _dataSetZ = new List<double>();

    public AnalyzeDataSet()
    {
    }

    private IEnumerable<IMeasurementPoint> _dataSet;

    public IEnumerable<IMeasurementPoint> DataSet
    {
      set
      {
        _dataSet = value;
        SeparateMeasurementData();
      }
    }

    private void SeparateMeasurementData()
    {
      foreach (var measurementPoint in _dataSet)
      {
        if (measurementPoint.AxisName == Constants.MeasurementAxis.X)
        {
          _dataSetX.Add(measurementPoint.Value);
        }

        if (measurementPoint.AxisName == Constants.MeasurementAxis.Y)
        {
          _dataSetY.Add(measurementPoint.Value);
        }

        if (measurementPoint.AxisName == Constants.MeasurementAxis.Z)
        {
          _dataSetZ.Add(measurementPoint.Value);
        }
      }
    }

    public double CalculateDataVariance(Constants.MeasurementAxis axis)
    {
      switch (axis)
      {
        case Constants.MeasurementAxis.X:
          return _dataSetX.StandardDeviation();
        case Constants.MeasurementAxis.Y:
          return _dataSetY.StandardDeviation();
        case Constants.MeasurementAxis.Z:
          return _dataSetZ.StandardDeviation();
        default:
          throw new ArgumentOutOfRangeException(nameof(axis), axis, null);
      }
    }
  }
}
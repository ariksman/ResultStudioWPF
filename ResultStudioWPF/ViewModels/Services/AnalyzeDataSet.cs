using System;
using System.Collections.Generic;
using ResultStudioWPF.Application.Interfaces;
using ResultStudioWPF.Domain.DomainModel.Enumerations;
using ResultStudioWPF.Domain.Interfaces;

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
        if (measurementPoint.Axis == MeasurementAxisType.X)
        {
          _dataSetX.Add(measurementPoint.Value);
        }

        if (measurementPoint.Axis == MeasurementAxisType.Y)
        {
          _dataSetY.Add(measurementPoint.Value);
        }

        if (measurementPoint.Axis == MeasurementAxisType.Z)
        {
          _dataSetZ.Add(measurementPoint.Value);
        }
      }
    }

    public double CalculateDataVariance(MeasurementAxisType axis)
    {
      if (axis == MeasurementAxisType.X)
        return axis.CalculateStandardDeviation(_dataSetX);
      if (axis == MeasurementAxisType.Y)
        return axis.CalculateStandardDeviation(_dataSetY);
      if (axis == MeasurementAxisType.Z)
        return axis.CalculateStandardDeviation(_dataSetZ);

      throw new ArgumentOutOfRangeException(nameof(axis), axis, null);
    }
  }
}
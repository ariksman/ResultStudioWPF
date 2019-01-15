using System;
using System.Collections.Generic;
using ResultStudioWPF.Models;

namespace ResultStudioWPF.Helpers
{
    public class AnalyseDataSet : IAnalyseDataSet
  {
        private IList<double> _dataSetX = new List<double>() ;
        private IList<double> _dataSetY = new List<double>();
        private IList<double> _dataSetZ = new List<double>();
        private readonly IEnumerable<MeasurementPoint> _dataSet;

        public AnalyseDataSet(IEnumerable<MeasurementPoint> dataSet)
        {
            _dataSet = dataSet;
            SeparateMeasurementData();
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

 /*       public double CalculateDataVarianceX()
        {
            return _dataSetX.StandardDeviation();
        }
        public double CalculateDataVarianceY()
        {
            return _dataSetY.StandardDeviation();
        }
        public double CalculateDataVarianceZ()
        {
            return _dataSetZ.StandardDeviation();
        }*/
    }
}
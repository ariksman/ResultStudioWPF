using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using OxyPlot;
using ResultStudioWPF.Helpers;
using ResultStudioWPF.Messages;
using ResultStudioWPF.ViewModels;

namespace ResultStudioWPF.Models
{
    public class MeasurementPoint : ModelBase
    {
        public MeasurementPoint()
        {
            
        }

        #region Public methods
        public void RefreshAllProperties()
        {
            foreach (var prop in this.GetType().GetProperties())
                NotifyPropertyChanged(null, prop.Name);
        }

        public void CheckAllTolerances()
        {
            foreach (var propertyInfo in this.GetType().GetProperties())
            {
                if (propertyInfo.Name == "Value")
                {
                    CheckValueTolerance(this.Value);
                }
            }
        }

        #endregion

        private int _measurementNumber;
        public int MeasurementNumber
        {
            get { return _measurementNumber; }
            set
            {
                _measurementNumber = value;
            }
        }

        private Constants.MeasurementAxis _axisName;
        public Constants.MeasurementAxis AxisName
        {
            get { return _axisName; }
            set
            {
                if (_axisName == value)
                {
                    return;
                }
                _axisName = value;
            }
        }

        private double _value;
        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                CheckValueTolerance(value);

                NotifyPropertyChanged(null);
            }
        }

        private void CheckValueTolerance(double value)
        {
            if (this.AxisName == Constants.MeasurementAxis.X)
            {
                var xTolerance = new ViewModelLocator().SettingsViewModel.XAxisTolerance;
                var xReference = new ViewModelLocator().SettingsViewModel.XAxisReference;

                if (_value > xReference + xTolerance)
                {

                    AddError("Value", "Given measurement value (" + _value + ") is higher than tolerance " + xTolerance + " allows");
                }
                else if (value < xReference - xTolerance)
                {
                    AddError("Value",
                        "Given measurement value (" + _value + ") is lower than tolerance " + xTolerance +
                        " allows");
                }
                else
                {
                    RemoveError("Value");
                }
            }
            if (this.AxisName == Constants.MeasurementAxis.Y)
            {
                var yTolerance = new ViewModelLocator().SettingsViewModel.YAxisTolerance;
                var yReference = new ViewModelLocator().SettingsViewModel.YAxisReference;

                if (_value > yReference + yTolerance)
                {

                    AddError("Value", "Given measurement value (" + _value + ") is higher than tolerance " + yTolerance + " allows");
                }
                else if (value < yReference - yTolerance)
                {
                    AddError("Value",
                        "Given measurement value (" + _value + ") is lower than tolerance " + yTolerance +
                        " allows");
                }
                else
                {
                    RemoveError("Value");
                }
            }

            if (this.AxisName == Constants.MeasurementAxis.Z)
            {
                var zTolerance = new ViewModelLocator().SettingsViewModel.ZAxisTolerance;
                var zReference = new ViewModelLocator().SettingsViewModel.ZAxisReference;

                if (_value > zReference + zTolerance)
                {

                    AddError("Value", "Given measurement value (" + _value + ") is higher than tolerance " + zTolerance + " allows");
                }
                else if (value < zReference - zTolerance)
                {
                    AddError("Value",
                        "Given measurement value (" + _value + ") is lower than tolerance " + zTolerance +
                        " allows");
                }
                else
                {
                    RemoveError("Value");
                }
            }
        }
    }
}

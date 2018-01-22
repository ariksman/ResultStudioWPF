using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using OxyPlot;
using ResultStudioWPF.Helpers;
using ResultStudioWPF.Messages;

namespace ResultStudioWPF.Models
{
    public class MeasurementPoint : ModelBase
    {
        public MeasurementPoint()
        {
            
        }

        private int _measurementNumber;
        public int MeasurementNumber
        {
            get { return _measurementNumber; }
            set
            {
                if (_measurementNumber == value)
                {
                    return;
                }
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
                NotifyPropertyChanged("Value", (valid) => { AppMessages.EntityIsValid.Send(valid); });
            }
        }
    }
}

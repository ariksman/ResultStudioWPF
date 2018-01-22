using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using ResultStudioWPF.Models;

namespace ResultStudioWPF.Helpers
{
    public class DataFileReader
    {
        private string _theFile;

        public DataFileReader()
        {
            CommentDelimiter = ";";
            DataSet = new ObservableCollection<MeasurementPoint>();

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = @"c:\temp\",
                Filter = "Text|*.txt|All|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var sFilePath = openFileDialog.FileName;
                if (sFilePath != null)
                {
                    TheFile = openFileDialog.FileName;
                }
            }
        }

        public string CommentDelimiter { get; set; }
        private ObservableCollection<MeasurementPoint> _dataSet;
        public ObservableCollection<MeasurementPoint> DataSet
        {
            get { return _dataSet; }
            set
            {
                if (_dataSet == value)
                {
                    return;
                }
                _dataSet = value;
            }
        }
        /// <summary>
        /// The initialization file path.
        /// </summary>
        public string TheFile
        {
            get
            {
                return _theFile;
            }
            set
            {
                _theFile = null;
                if (File.Exists(value))
                {
                    _theFile = value;
                    using (StreamReader sr = new StreamReader(_theFile))
                    {
                        string line = "";
                        char[] charsToTrim = { ' ' };
                        while ((line = sr.ReadLine()) != null)
                        {
                            line = line.Trim();
                            if (line.Length == 0) continue;  // empty line

                            string[] parts = line.Split('\t');

                            var measurementNumber = 0;
                            Int32.TryParse(parts[0], out measurementNumber);

                            Constants.MeasurementAxis axis;
                            switch (parts[1])
                            {
                                case "X":
                                    axis = Constants.MeasurementAxis.X;
                                    break;
                                case "Y":
                                    axis = Constants.MeasurementAxis.Y;
                                    break;
                                case "Z":
                                    axis = Constants.MeasurementAxis.Z;
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }

                            double measurement = 0;
                            Double.TryParse(parts[2], out measurement);

                            var newMeasurementPoint = new MeasurementPoint()
                            {
                                MeasurementNumber = measurementNumber,
                                AxisName = axis,
                                Value = measurement,

                            };

                            _dataSet.Add(newMeasurementPoint);
                        }
                    }
                }
            }
        }
    }
}

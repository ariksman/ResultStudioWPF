using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ResultStudioWPF.Data;
using ResultStudioWPF.Helpers;
using ResultStudioWPF.Messages;
using ResultStudioWPF.Models;

namespace ResultStudioWPF.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public ICollectionView SubframeDataSetCollectionView { get; private set; }

        public SettingsViewModel()
        {

            _xAxisReference = 2000;
            _yAxisReference = -500;
            _zAxisReference = 378;

            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
/*                DataSet = new ObservableCollection<MeasurementPoint>()
                {
                    new MeasurementPoint()
                    {
                        MeasurementNumber = 1,
                        AxisName = Constants.MeasurementAxis.X,
                        Value = 1,
                        IsValid = true
                    },
                    new MeasurementPoint()
                    {
                        MeasurementNumber = 2,
                        AxisName = Constants.MeasurementAxis.Y,
                        Value = 22,
                        IsValid = true
                    },
                    new MeasurementPoint()
                    {
                        MeasurementNumber = 3,
                        AxisName = Constants.MeasurementAxis.Z,
                        Value = 33,
                        IsValid = true
                    }
                };*/
            }
            else
            {
                // Code runs "for real"

                // Messages

                // Commands
                ImportDataFromFileCommand = new RelayCommand<RoutedEventArgs>(ImportDataFromFileExecute);
                CreateRandomMeasurementDataClickCommand =
                    new RelayCommand<RoutedEventArgs>(CreateRandomMeasurementDataClickExecute);

                // Collections
                SubframeDataSetCollectionView = CollectionViewSource.GetDefaultView(_dataSet);

                //Setup
                ProgressBarIsIndetermined = false;
            }
        }

        private async void CreateRandomMeasurementDataClickExecute(RoutedEventArgs obj)
        {
            var frameCount = 20;
            FilePath = "Random data in use";

            ProgressBarIsIndetermined = true;
            var progress = new Progress<int>(status =>
            {
                ProgressBarValue = status;
            });

            await Task.Run(() =>
            {
                var dataCreator = new DataCreator();
                DataSet = dataCreator.CreateSubframeDataset(_xAxisReference, _yAxisReference, _zAxisReference,
                    frameCount, 100, progress);
            });

            ProgressBarIsIndetermined = false;

            CheckHowManySubframesIsInvalid();
        }

        private async void ImportDataFromFileExecute(RoutedEventArgs obj)
        {
            ProgressBarIsIndetermined = true;
            var progress = new Progress<int>(status =>
            {
                ProgressBarValue = status;
            });
            
            await Task.Run(() =>
            {
                var fileImporter = new DataFileReader(progress);
                DataSet = fileImporter.DataSet;
                FilePath = fileImporter.TheFile;
            });

            ProgressBarIsIndetermined = false;

            CheckHowManySubframesIsInvalid();
        }

        private void RefreshDataGridTolerance()
        {
            foreach (var measurementPoint in DataSet)
            {
                //TODO: Change this into a real way to raiseproperty changed without actually changing the value
                //var temp = measurementPoint.Value;
                //measurementPoint.Value = temp;

                measurementPoint.RefreshAllProperties();
                measurementPoint.CheckAllTolerances();
            }

            CheckHowManySubframesIsInvalid();
        }

        private void CheckHowManySubframesIsInvalid()
        {
            ErrorCount = DataSet.Count(item => item.HasErrors);
        }

        #region RelayCommands

        public RelayCommand<RoutedEventArgs> ImportDataFromFileCommand { private set; get; }
        public RelayCommand<RoutedEventArgs> CreateRandomMeasurementDataClickCommand { private set; get; }

        #endregion

        #region Input data validation methods

        #endregion

        #region Public collections
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
                RaisePropertyChanged();

                
                if (_dataSet != null && _dataSet.Count > 0)
                {
                    var dataSetAnalyser = new AnalyseDataSet(_dataSet);

                    XVariance = dataSetAnalyser.CalculateDataVariance(Constants.MeasurementAxis.X);
                    YVariance = dataSetAnalyser.CalculateDataVariance(Constants.MeasurementAxis.Y);
                    ZVariance = dataSetAnalyser.CalculateDataVariance(Constants.MeasurementAxis.Z);

                    AppMessages.PlotDataSet.Send(_dataSet);
                }
            }
        }
        #endregion

        #region Public Properties
        private bool _progressBarIsIndetermined;
        public bool ProgressBarIsIndetermined
        {
            get { return _progressBarIsIndetermined; }

            set
            {
                if (_progressBarIsIndetermined == value)
                {
                    return;
                }
                _progressBarIsIndetermined = value;
                RaisePropertyChanged();
            }
        }

        private int _progressBarValue;
        public int ProgressBarValue
        {
            get { return _progressBarValue; }

            set
            {
                if (_progressBarValue == value)
                {
                    return;
                }
                _progressBarValue = value;
                RaisePropertyChanged();
            }
        }

        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }

            set
            {
                if (_filePath == value)
                {
                    return;
                }
                _filePath = value;
                RaisePropertyChanged();
            }
        }

        private double _xAxisTolerance;
        public double XAxisTolerance
        {
            get { return _xAxisTolerance; }

            set
            {
                if (_xAxisTolerance == value)
                {
                    return;
                }
                /*Regex pattern = new Regex("^[-]?[[0-9]*(.[0-9]*)$]");
                if (pattern.IsMatch(value))
                {

                    _xAxisTolerance = value;
                }
                else
                {

                    return;
                }*/
                //TODO: data validation
                _xAxisTolerance = value;
                RefreshDataGridTolerance();
                RaisePropertyChanged();
            }
        }

        private double _yAxisTolerance;
        public double YAxisTolerance
        {
            get { return _yAxisTolerance; }

            set
            {
                if (_yAxisTolerance == value)
                {
                    return;
                }

                //TODO: data validation
                _yAxisTolerance = value;
                RefreshDataGridTolerance();
                RaisePropertyChanged();
            }
        }

        private double _zAxisTolerance;
        public double ZAxisTolerance
        {
            get { return _zAxisTolerance; }

            set
            {
                if (_zAxisTolerance == value)
                {
                    return;
                }
                //TODO: data validation
                _zAxisTolerance = value;
                RefreshDataGridTolerance();
                RaisePropertyChanged();
            }
        }

        private double _xAxisReference;
        public double XAxisReference
        {
            get { return _xAxisReference; }

            set
            {
                if (_xAxisReference == value)
                {
                    return;
                }
                //TODO: data validation
                _xAxisReference = value;
                RaisePropertyChanged();
            }
        }

        private double _yAxisReference;
        public double YAxisReference
        {
            get { return _yAxisReference; }

            set
            {
                if (_yAxisReference == value)
                {
                    return;
                }
                //TODO: data validation
                _yAxisReference = value;
                RaisePropertyChanged();
            }
        }
        private double _zAxisReference;
        public double ZAxisReference
        {
            get { return _zAxisReference; }

            set
            {
                if (_zAxisReference == value)
                {
                    return;
                }
                //TODO: data validation
                _zAxisReference = value;
                RaisePropertyChanged();
            }
        }

        private double _xVariance;
        public double XVariance
        {
            get { return _xVariance; }

            set
            {
                if (_xVariance == value)
                {
                    return;
                }
                //TODO: data validation
                _xVariance = value;
                RaisePropertyChanged();
            }
        }

        private double _yVariance;
        public double YVariance
        {
            get { return _yVariance; }

            set
            {
                if (_yVariance == value)
                {
                    return;
                }
                //TODO: data validation
                _yVariance = value;
                RaisePropertyChanged();
            }
        }

        private double _zVariance;
        public double ZVariance
        {
            get { return _zVariance; }

            set
            {
                if (_zVariance == value)
                {
                    return;
                }
                //TODO: data validation
                _zVariance = value;
                RaisePropertyChanged();
            }
        }

        private int _errorCount;
        public int ErrorCount
        {
            get { return _errorCount; }

            set
            {
                if (_errorCount == value)
                {
                    return;
                }
                _errorCount = value;
                RaisePropertyChanged();
            }
        }

        #endregion
    }
}
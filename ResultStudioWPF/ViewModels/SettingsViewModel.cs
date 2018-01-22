﻿using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            }
            else
            {
                // Code runs "for real"

                // Messages

                // Commands
                ImportDataFromFileCommand = new RelayCommand<RoutedEventArgs>(ImportDataFromFileExecute);
                CreateRandomMeasurementDataClickCommand = new RelayCommand<RoutedEventArgs>(CreateRandomMeasurementDataClickExecute);
                ÍmportMeasurementDataFromFileClickCommand = new RelayCommand<RoutedEventArgs>(ÍmportMeasurementDataFromFileClickÉxecute);

                // Collections
                SubframeDataSetCollectionView = CollectionViewSource.GetDefaultView(_dataSet);
            }
        }

        private async void ImportDataFromFileExecute(RoutedEventArgs obj)
        {
            await Task.Run(() =>
            {
                var fileImporter = new DataFileReader();
                DataSet = fileImporter.DataSet;
            });
        }

        private void RefreshDataGridTolerance()
        {
            foreach (var measurementPoint in DataSet)
            {
                //TODO: Change this into a real way to raiseproperty changed without actually changing the value
                var temp = measurementPoint.Value;
                measurementPoint.Value = temp;
            }
        }

        #region RelayCommands
        

        public RelayCommand<RoutedEventArgs> ImportDataFromFileCommand { private set; get; }
        public RelayCommand<RoutedEventArgs> CreateRandomMeasurementDataClickCommand { private set; get; }
        public RelayCommand<RoutedEventArgs> ÍmportMeasurementDataFromFileClickCommand { private set; get; }



        private void CreateRandomMeasurementDataClickExecute(RoutedEventArgs obj)
        {
            var frameCount = 20;
            var dataCreator = new DataCreator();
            DataSet = dataCreator.CreateSubframeDataset(_xAxisReference, _yAxisReference, _zAxisReference, frameCount, 100);

        }

        private void ÍmportMeasurementDataFromFileClickÉxecute(RoutedEventArgs obj)
        {
            throw new System.NotImplementedException();
        }
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

                var dataSetAnalyser = new AnalyseDataSet(_dataSet);
                XVariance = dataSetAnalyser.CalculateDataVarianceX();
                YVariance = dataSetAnalyser.CalculateDataVarianceY();
                ZVariance = dataSetAnalyser.CalculateDataVarianceZ();

                AppMessages.PlotDataSet.Send(_dataSet);

                RaisePropertyChanged("DataSet");
            }
        }
        #endregion

        #region Public Properties

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
                RaisePropertyChanged("XAxisTolerance");
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
                RaisePropertyChanged("YAxisTolerance");
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
                RaisePropertyChanged("ZAxisTolerance");
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
                RaisePropertyChanged("_XAxisReference");
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
                RaisePropertyChanged("YAxisReference");
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
                RaisePropertyChanged("ZAxisReference");
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
                RaisePropertyChanged("XVariance");
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
                RaisePropertyChanged("YVariance");
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
                RaisePropertyChanged("ZVariance");
            }
        }

        #endregion
    }
}
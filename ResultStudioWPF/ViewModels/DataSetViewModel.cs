using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using AutoMapper;
using CSharpFunctionalExtensions;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ResultStudioWPF.Application.CQS;
using ResultStudioWPF.Application.Interfaces;
using ResultStudioWPF.Common.CQS;
using ResultStudioWPF.Domain.DomainModel.Entities;
using ResultStudioWPF.Domain.DomainModel.Enumerations;
using ResultStudioWPF.Domain.Interfaces;
using ResultStudioWPF.Domain.UseCases.DataSet;
using ResultStudioWPF.ViewModels.Messages;

namespace ResultStudioWPF.ViewModels
{
  public class DataSetViewModel : ViewModelBase, ISharedSettingsContext
  {
    public ICollectionView SubFrameDataSetCollectionView { get; private set; }

    private readonly IMessageDialogService _messageDialogService;
    private readonly IMapper _mapper;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public DataSetViewModel(
      IMessageDialogService messageDialogService,
      IMapper mapper,
      ICommandDispatcher commandDispatcher,
      IQueryDispatcher queryDispatcher)
    {
      _messageDialogService = messageDialogService;
      _mapper = mapper;
      _commandDispatcher = commandDispatcher;
      _queryDispatcher = queryDispatcher;

      _xAxisReference = 2000;
      _yAxisReference = -500;
      _zAxisReference = 378;

      if (IsInDesignMode)
      {
        // Code runs in Blend --> create design time data.
      }
      else
      {
        SubFrameDataSetCollectionView = CollectionViewSource.GetDefaultView(_dataSet);
        ProgressBarIsIndetermined = false;
        DataSet = new ObservableCollection<MeasurementPointViewModel>();
      }
    }

    #region RelayCommands

    #region import data relay command

    private RelayCommand<RoutedEventArgs> _importDataFromFileCommand;

    public RelayCommand<RoutedEventArgs> ImportDataFromFileCommand =>
      _importDataFromFileCommand
      ?? (_importDataFromFileCommand = new RelayCommand<RoutedEventArgs>(
        ImportDataFromFileExecute
      ));

    private async void ImportDataFromFileExecute(RoutedEventArgs obj)
    {
      ProgressBarIsIndetermined = true;
      var progress = new Progress<int>(status => { ProgressBarValue = status; });

      await Task.Run(() =>
      {
        var query = new GetDataSetFromFile(progress);
        _queryDispatcher.Dispatch<GetDataSetFromFile, Result<DataSet>>(query)
          .OnSuccessTry(result =>
          {
            FilePath = result.Name;
            XVariance = result.CalculateVariance(MeasurementAxisType.X);
            YVariance = result.CalculateVariance(MeasurementAxisType.Y);
            ZVariance = result.CalculateVariance(MeasurementAxisType.Z);
            DataSet = _mapper.Map<ObservableCollection<MeasurementPointViewModel>>(result.MeasurementPoints);
          })
          .OnFailure((result) =>
          {
            _messageDialogService.ShowErrorMessage("SettingsViewModel", "Failed to load file", result);
          });
      });

      ProgressBarIsIndetermined = false;
      UpdateSubFramesErrorCount();
    }

    #endregion

    #region create random data relay command

    private RelayCommand<RoutedEventArgs> _createRandomMeasurementDataClickCommand;

    public RelayCommand<RoutedEventArgs> CreateRandomMeasurementDataClickCommand =>
      _createRandomMeasurementDataClickCommand
      ?? (_createRandomMeasurementDataClickCommand = new RelayCommand<RoutedEventArgs>(
        CreateRandomMeasurementDataClickExecute
      ));

    private async void CreateRandomMeasurementDataClickExecute(RoutedEventArgs obj)
    {
      var frameCount = 20;
      var spread = 100;

      ProgressBarIsIndetermined = true;
      var progress = new Progress<int>(status => { ProgressBarValue = status; });

      await Task.Run(() =>
      {
        var query = new GetRandomDataSetQuery(
          _xAxisReference,
          _yAxisReference,
          _zAxisReference,
          frameCount,
          spread,
          progress);

        _queryDispatcher.Dispatch<GetRandomDataSetQuery, Result<DataSet>>(query)
          .OnSuccessTry(
            result =>
            {
              FilePath = result.Name;
              XVariance = result.CalculateVariance(MeasurementAxisType.X);
              YVariance = result.CalculateVariance(MeasurementAxisType.Y);
              ZVariance = result.CalculateVariance(MeasurementAxisType.Z);
              DataSet = _mapper.Map<ObservableCollection<MeasurementPointViewModel>>(result.MeasurementPoints); 
            });
      });

      ProgressBarIsIndetermined = false;

      UpdateSubFramesErrorCount();
    }

    #endregion

    #endregion

    #region private methods

    private void RefreshDataGridTolerance()
    {
      foreach (var measurementPoint in DataSet)
      {
        measurementPoint.RefreshAllProperties();
        measurementPoint.CheckAllTolerances();
      }

      UpdateSubFramesErrorCount();
    }

    private void UpdateSubFramesErrorCount()
    {
      ErrorCount = DataSet.Count(item => item.HasErrors);
    }

    #endregion

    #region Public collections

    private ObservableCollection<MeasurementPointViewModel> _dataSet;

    public ObservableCollection<MeasurementPointViewModel> DataSet
    {
      get => _dataSet;
      set
      {
        if (_dataSet == value) return;

        _dataSet = value;
        RaisePropertyChanged();


        if (_dataSet != null && _dataSet.Count > 0)
        {
          AppMessages.PlotDataSet.Send(_dataSet);
        }
      }
    }

    #endregion

    #region Public Properties

    private MeasurementPointViewModel _measurementPoint;

    public MeasurementPointViewModel MeasurementPoint
    {
      get => _measurementPoint;
      set { _measurementPoint = value; }
    }


    private bool _progressBarIsIndetermined;

    public bool ProgressBarIsIndetermined
    {
      get => _progressBarIsIndetermined;

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
      get => _progressBarValue;

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
      get => _filePath;

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
      get => _xAxisTolerance;

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
      get => _yAxisTolerance;

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
      get => _zAxisTolerance;

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
      get => _xAxisReference;

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
      get => _yAxisReference;

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
      get => _zAxisReference;

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
      get => _xVariance;

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
      get => _yVariance;

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
      get => _zVariance;

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
      get => _errorCount;

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
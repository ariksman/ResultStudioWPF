using System;
using System.Collections.Generic;
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
using OxyPlot;
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
  public class DataSetViewModel : ViewModelBase
  {
    public DataSetModel Model { get; }
    public ICollectionView SubFrameDataSetCollectionView { get; private set; }

    private readonly IMessageDialogService _messageDialogService;
    private readonly IMapper _mapper;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly Func<MeasurementPointModel, MeasurementPointViewModel> _measurementPointViewModelFunc;

    public DataSetViewModel(
      DataSetModel model,
      IMessageDialogService messageDialogService,
      IMapper mapper,
      ICommandDispatcher commandDispatcher,
      IQueryDispatcher queryDispatcher,
      Func<MeasurementPointModel, MeasurementPointViewModel> measurementPointViewModelFunc)
    {
      Model = model;
      _messageDialogService = messageDialogService;
      _mapper = mapper;
      _commandDispatcher = commandDispatcher;
      _queryDispatcher = queryDispatcher;
      _measurementPointViewModelFunc = measurementPointViewModelFunc;

      Model.XAxisReference = 2000;
      Model.YAxisReference = -500;
      Model.ZAxisReference = 378;

      if (IsInDesignMode)
      {
        // Code runs in Blend --> create design time data.
      }
      else
      {
        SubFrameDataSetCollectionView = CollectionViewSource.GetDefaultView(Model.DataSet);
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
            var pointModels = _mapper.Map<List<MeasurementPointModel>>(result.MeasurementPoints);
            var featureViewModels = pointModels.Select(model => _measurementPointViewModelFunc(model)).ToList();

            DataSet.Clear();
            foreach (var measurementPointViewModel in featureViewModels)
            {
              DataSet.Add(measurementPointViewModel);
            }

            AppMessages.PlotDataSet.Send(Model.DataSet);
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
          Model.XAxisReference,
          Model.YAxisReference,
          Model.ZAxisReference,
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
              var pointModels = _mapper.Map<List<MeasurementPointModel>>(result.MeasurementPoints); // TODO: fix mapping, now partial
              var pointViewModels = pointModels.Select(model => _measurementPointViewModelFunc(model)).ToList();

              DataSet = new ObservableCollection<MeasurementPointViewModel>(pointViewModels); // TODO: avoid renew
              AppMessages.PlotDataSet.Send(Model.DataSet);
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

    public ObservableCollection<MeasurementPointViewModel> DataSet
    {
      get => Model.DataSet;
      set
      {
        if (Model.DataSet == value) return;

        Model.DataSet = value;
        RaisePropertyChanged();
      }
    }

    #endregion

    #region Public Properties

    public MeasurementPointViewModel MeasurementPoint { get; set; }

    public bool ProgressBarIsIndetermined
    {
      get => Model.ProgressBarIsIndetermined;

      set
      {
        if (Model.ProgressBarIsIndetermined == value)
        {
          return;
        }

        Model.ProgressBarIsIndetermined = value;
        RaisePropertyChanged();
      }
    }


    public int ProgressBarValue
    {
      get => Model.ProgressBarValue;

      set
      {
        if (Model.ProgressBarValue == value)
        {
          return;
        }

        Model.ProgressBarValue = value;
        RaisePropertyChanged();
      }
    }


    public string FilePath
    {
      get => Model.FilePath;

      set
      {
        if (Model.FilePath == value)
        {
          return;
        }

        Model.FilePath = value;
        RaisePropertyChanged();
      }
    }


    public double XAxisTolerance
    {
      get => Model.XAxisTolerance;

      set
      {
        if (Model.XAxisTolerance == value)
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
        Model.XAxisTolerance = value;
        RefreshDataGridTolerance();
        RaisePropertyChanged();
      }
    }


    public double YAxisTolerance
    {
      get => Model.YAxisTolerance;

      set
      {
        if (Model.YAxisTolerance == value)
        {
          return;
        }

        //TODO: data validation
        Model.YAxisTolerance = value;
        RefreshDataGridTolerance();
        RaisePropertyChanged();
      }
    }


    public double ZAxisTolerance
    {
      get => Model.ZAxisTolerance;

      set
      {
        if (Model.ZAxisTolerance == value)
        {
          return;
        }

        //TODO: data validation
        Model.ZAxisTolerance = value;
        RefreshDataGridTolerance();
        RaisePropertyChanged();
      }
    }


    public double XAxisReference
    {
      get => Model.XAxisReference;

      set
      {
        if (Model.XAxisReference == value)
        {
          return;
        }

        //TODO: data validation
        Model.XAxisReference = value;
        RaisePropertyChanged();
      }
    }

    public double YAxisReference
    {
      get => Model.YAxisReference;

      set
      {
        if (Model.YAxisReference == value)
        {
          return;
        }

        //TODO: data validation
        Model.YAxisReference = value;
        RaisePropertyChanged();
      }
    }


    public double ZAxisReference
    {
      get => Model.ZAxisReference;

      set
      {
        if (Model.ZAxisReference == value)
        {
          return;
        }

        //TODO: data validation
        Model.ZAxisReference = value;
        RaisePropertyChanged();
      }
    }


    public double XVariance
    {
      get => Model.XVariance;

      set
      {
        if (Model.XVariance == value)
        {
          return;
        }

        //TODO: data validation
        Model.XVariance = value;
        RaisePropertyChanged();
      }
    }

    public double YVariance
    {
      get => Model.YVariance;

      set
      {
        if (Model.YVariance == value)
        {
          return;
        }

        //TODO: data validation
        Model.YVariance = value;
        RaisePropertyChanged();
      }
    }


    public double ZVariance
    {
      get => Model.ZVariance;

      set
      {
        if (Model.ZVariance == value)
        {
          return;
        }

        //TODO: data validation
        Model.ZVariance = value;
        RaisePropertyChanged();
      }
    }


    public int ErrorCount
    {
      get => Model.ErrorCount;

      set
      {
        if (Model.ErrorCount == value)
        {
          return;
        }

        Model.ErrorCount = value;
        RaisePropertyChanged();
      }
    }

    #endregion
  }
}
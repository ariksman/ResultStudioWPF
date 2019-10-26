using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using AutoMapper;
using CSharpFunctionalExtensions;
using Moq;
using NUnit.Framework;
using ResultStudioWPF.Application.CQS;
using ResultStudioWPF.Application.Interfaces;
using ResultStudioWPF.Common.CQS;
using ResultStudioWPF.Domain.DomainModel.Entities;
using ResultStudioWPF.Domain.DomainModel.Enumerations;
using ResultStudioWPF.Domain.DomainModel.ValueObjects;
using ResultStudioWPF.Domain.UseCases.DataSet;
using ResultStudioWPF.ViewModels;

namespace ResultStudioWPF.UnitTests
{
  public class DataSetViewmodelTests
  {
    private Mock<ICommandDispatcher> _commandDispatcherMock;
    private Mock<IQueryDispatcher> _queryDispatcherMock;
    private Mock<IMapper> _mapperMock;
    private Mock<IMessageDialogService> _messageDialogMock;
    private Func<MeasurementPointModel, MeasurementPointViewModel> _measurementPointViewModelFunc;
    private DataSetModel _dataSetModel;

    [SetUp]
    public void SetUp()
    {
      
    }

    [Test]
    public void ImportDataFromFile_ShouldUpdateFilePath_WhenLoadSucceeds()
    {
      var testString = "testDataString";
      var dataSet = DataSet.Create(testString, new List<MeasurementPoint>());

      _dataSetModel = new DataSetModel();
      _queryDispatcherMock = new Mock<IQueryDispatcher>();
      _queryDispatcherMock
        .Setup(e => e.Dispatch<GetDataSetFromFile, Result<DataSet>>(It.IsAny<GetDataSetFromFile>()))
        .Returns(Result.Ok(dataSet.Value));

      _measurementPointViewModelFunc = model => new MeasurementPointViewModel(model, new DataSetModel());
      _commandDispatcherMock = new Mock<ICommandDispatcher>();
      _mapperMock = new Mock<IMapper>();
      _messageDialogMock = new Mock<IMessageDialogService>();

      var viewModel = new DataSetViewModel( 
        _dataSetModel,
        _messageDialogMock.Object,
        _mapperMock.Object,
        _commandDispatcherMock.Object,
        _queryDispatcherMock.Object,
        _measurementPointViewModelFunc);

      viewModel.FilePath = string.Empty;

      viewModel.ImportDataFromFileCommand.Execute(null);

      Assert.IsTrue(viewModel.FilePath == testString);
    }

    [Test]
    public void ImportDataFromFile_ShouldShowErrorMessage_WhenFailToLoadData()
    {
      var error = "Custom error msg";
      _dataSetModel = new DataSetModel();
      _queryDispatcherMock = new Mock<IQueryDispatcher>();
      _queryDispatcherMock
        .Setup(e => e.Dispatch<GetDataSetFromFile, Result<DataSet>>(It.IsAny<GetDataSetFromFile>()))
        .Returns(Result.Fail<DataSet>(error));
      _commandDispatcherMock = new Mock<ICommandDispatcher>();
      _mapperMock = new Mock<IMapper>();
      _messageDialogMock = new Mock<IMessageDialogService>();
      _measurementPointViewModelFunc = model => new MeasurementPointViewModel(model, new DataSetModel());

      var viewModel = new DataSetViewModel(
        _dataSetModel,
        _messageDialogMock.Object,
        _mapperMock.Object,
        _commandDispatcherMock.Object,
        _queryDispatcherMock.Object,
        _measurementPointViewModelFunc);

      viewModel.ImportDataFromFileCommand.Execute(null);

      _messageDialogMock.Verify(m => m.ShowErrorMessage("SettingsViewModel", "Failed to load file", error));
    }

    [Test]
    public void CreateRandomMeasurementDataClickCommand_ShouldImportRandomData_WhenCalled()
    {
      var data = DataSet.Create("test set",
        new List<MeasurementPoint>()
          {new MeasurementPoint(1, 1, MeasurementAxisType.X, new Tolerance(1, 2, 3), new Reference(1, 2, 3))});
      
      _dataSetModel = new DataSetModel();
      _queryDispatcherMock = new Mock<IQueryDispatcher>();
      _queryDispatcherMock
        .Setup(e => e.Dispatch<GetRandomDataSetQuery, Result<DataSet>>(It.IsAny<GetRandomDataSetQuery>()))
        .Returns(Result.Ok<DataSet>(data.Value));
      _commandDispatcherMock = new Mock<ICommandDispatcher>();
      _mapperMock = new Mock<IMapper>();
      //_mapperMock.Setup(m => m.Map<IReadOnlyList<MeasurementPoint>, ObservableCollection<MeasurementPointViewModel>>(data.Value.MeasurementPoints))
      //  .Returns(() => new ObservableCollection<MeasurementPointViewModel>() { new MeasurementPointViewModel()})
      _messageDialogMock = new Mock<IMessageDialogService>();
      _measurementPointViewModelFunc = model => new MeasurementPointViewModel(model, new DataSetModel());

      var viewModel = new DataSetViewModel(
        _dataSetModel,
        _messageDialogMock.Object,
        _mapperMock.Object,
        _commandDispatcherMock.Object,
        _queryDispatcherMock.Object,
        _measurementPointViewModelFunc);

      viewModel.CreateRandomMeasurementDataClickCommand.Execute(null);

      Assert.Contains(data, viewModel.DataSet);
    }
  }
}
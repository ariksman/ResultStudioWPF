using System.Collections.Generic;
using System.Windows.Threading;
using AutoMapper;
using CSharpFunctionalExtensions;
using Moq;
using NUnit.Framework;
using ResultStudioWPF.Application.CQS;
using ResultStudioWPF.Application.Interfaces;
using ResultStudioWPF.Common.CQS;
using ResultStudioWPF.Domain.DomainModel.Entities;
using ResultStudioWPF.Domain.UseCases.DataSet;
using ResultStudioWPF.ViewModels;

namespace ResultStudioWPF.UnitTests
{
  public class SettingsViewmodelTests
  {
    private Mock<ICommandDispatcher> _commandDispatcherMock;
    private Mock<IQueryDispatcher> _queryDispatcherMock;
    private Mock<IMapper> _mapperMock;
    private Mock<IMessageDialogService> _messageDialogMock;
    private SettingsViewModel _viewModel;

    [SetUp]
    public void SetUp()
    {
      
    }

    [Test]
    public void ImportDataFromFile_ShouldUpdateFilePath_WhenLoadSucceeds()
    {
      var testString = "testDataString";
      var dataSet = DataSet.Create(testString, new List<MeasurementPoint>());

      var query = new GetDataSetFromFile();
      _queryDispatcherMock = new Mock<IQueryDispatcher>();
      _queryDispatcherMock
        .Setup(e => e.Dispatch<GetDataSetFromFile, Result<DataSet>>(query))
        .Returns(Result.Ok(dataSet.Value));

      _commandDispatcherMock = new Mock<ICommandDispatcher>();
      _mapperMock = new Mock<IMapper>();
      _messageDialogMock = new Mock<IMessageDialogService>();

      _viewModel = new SettingsViewModel(
        _messageDialogMock.Object,
        _mapperMock.Object,
        _commandDispatcherMock.Object,
        _queryDispatcherMock.Object);

      _viewModel.FilePath = string.Empty;

      _viewModel.ImportDataFromFileCommand.Execute(null);

      Assert.IsTrue(_viewModel.FilePath == testString);
    }

    [Test]
    public void ImportDataFromFile_ShouldShowErrorMessage_WhenFailToLoadData()
    {
      _viewModel.FilePath = string.Empty;

      _viewModel.ImportDataFromFileCommand.Execute(null);

      //Assert.IsNull(_viewModel.SelectedCadModelDto);
    }
  }
}
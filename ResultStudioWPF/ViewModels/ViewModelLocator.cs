using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using ResultStudioWPF.Infrastructure;

namespace ResultStudioWPF.ViewModels
{
  /// <summary>
  /// This class contains static references to all the view models in the
  /// application and provides an entry point for the bindings.
  /// </summary>
  public class ViewModelLocator
  {
    private static ViewModelLocator _instance;
    private IContainer _container;

    private ILifetimeScope _resultsViewModelLifetimeScope;
    private ILifetimeScope _dataSetViewModelLifetimeScope;

    /// <summary>
    /// Initializes a new instance of the ViewModelLocator class and register all classes for DI-container.
    /// Additionally, registers all profiles for Auto mapper.
    /// </summary>
    public ViewModelLocator()
    {
      if (!ServiceLocator.IsLocationProviderSet)
      {
        _instance = this;
        RegisterServices();
      }
    }

    public static ViewModelLocator Instance => _instance ?? (_instance = new ViewModelLocator());

    #region autofac registration

    private void RegisterServices()
    {
      var builder = new ContainerBuilder();
      var assemblies = GetAllProgramAssemblies().ToList();

      builder.RegisterModule(new HandlerAutoFacModule(assemblies));
      builder.RegisterModule<ViewModelServicesModule>();
      builder.RegisterModule(new AutoMapperModule(assemblies));

      _container = builder.Build();

      ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(_container));
    }

    private static IEnumerable<Assembly> GetAllProgramAssemblies()
    {
      return new List<Assembly>()
      {
        Assembly.Load("ResultStudioWPF.Application"),
        Assembly.Load("ResultStudioWPF.Domain"),
        Assembly.Load("ResultStudioWPF"),
      };
    }

    #endregion

    #region ViewModel properties

    public ResultsViewModel ResultsViewModel
    {
      get
      {
        _resultsViewModelLifetimeScope = _container.BeginLifetimeScope();
        return _resultsViewModelLifetimeScope.Resolve<ResultsViewModel>();
      }
    }

    public DataSetViewModel SettingsViewModel
    {
      get
      {
        _dataSetViewModelLifetimeScope = _container.BeginLifetimeScope();
        return _dataSetViewModelLifetimeScope.Resolve<DataSetViewModel>();
      }
    }

    #endregion
  }
}
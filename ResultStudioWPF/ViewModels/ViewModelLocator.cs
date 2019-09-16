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
    /// <summary>
    /// Initializes a new instance of the ViewModelLocator class and register all classes for DI-container.
    /// Additionally, registers all profiles for Auto mapper.
    /// </summary>
    static ViewModelLocator()
    {
      if (!ServiceLocator.IsLocationProviderSet)
      {
        RegisterServices();
      }
    }

    #region autofac registration

    private static void RegisterServices()
    {
      var builder = new ContainerBuilder();
      var assemblies = GetAllProgramAssemblies().ToList();

      builder.RegisterModule(new HandlerAutoFacModule(assemblies));
      builder.RegisterModule<ViewModelServicesModule>();
      builder.RegisterModule(new AutoMapperModule(assemblies));

      var container = builder.Build();

      ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
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

    public ResultsViewModel ResultsViewModel => ServiceLocator.Current.GetInstance<ResultsViewModel>();

    public DataSetViewModel SettingsViewModel
    {
      get
      {
        try
        {
          return ServiceLocator.Current.GetInstance<DataSetViewModel>();
        }
        catch (Exception e)
        {
          Debugger.Break();
          throw;
        }
      }
    }

    #endregion
  }
}
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using ResultStudioWPF.Helpers;

namespace ResultStudioWPF.ViewModels
{
  /// <summary>
  /// This class contains static references to all the view models in the
  /// application and provides an entry point for the bindings.
  /// </summary>
  public class ViewModelLocator
  {
    #region LifetimeManagers

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the ViewModelLocator class.
    /// </summary>
    static ViewModelLocator()
    {
      if (!ServiceLocator.IsLocationProviderSet)
      {
        RegisterServices(registerFakes: true);
      }
    }

    #endregion

    #region autofac registration

    private static void RegisterServices(ContainerBuilder registrations = null, bool registerFakes = false)
    {
      var builder = new ContainerBuilder();

      if (ViewModelBase.IsInDesignModeStatic || registerFakes)
      {
        builder.RegisterModule<AutofacModule>();

      }
      else
      {
        builder.RegisterModule<AutofacModule>();
      }
      var container = builder.Build();
      registrations?.Update(container);

      ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
    }

    #endregion


    #region ViewModel properties

    public ResultsViewModel ResultsViewModel => ServiceLocator.Current.GetInstance<ResultsViewModel>();
    public SettingsViewModel SettingsViewModel => ServiceLocator.Current.GetInstance<SettingsViewModel>();

    #endregion
  }
}

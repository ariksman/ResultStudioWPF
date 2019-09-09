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
    /// Initializes a new instance of the ViewModelLocator class.
    /// </summary>
    static ViewModelLocator()
    {
      if (!ServiceLocator.IsLocationProviderSet)
      {
        RegisterServices(registerFakes: true);
      }
    }

    #region autofac registration

    private static void RegisterServices(bool registerFakes = false)
    {
      var builder = new ContainerBuilder();

      if (ViewModelBase.IsInDesignModeStatic || registerFakes)
      {
        builder.RegisterModule<ViewModelServicesModule>();
      }
      else
      {
        builder.RegisterModule<ViewModelServicesModule>();
        builder.RegisterModule<HandlerAutoFacModule>();
      }
      var container = builder.Build();

      ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
    }

    #endregion


    #region ViewModel properties

    public ResultsViewModel ResultsViewModel => ServiceLocator.Current.GetInstance<ResultsViewModel>();
    public SettingsViewModel SettingsViewModel => ServiceLocator.Current.GetInstance<SettingsViewModel>();

    #endregion
  }
}

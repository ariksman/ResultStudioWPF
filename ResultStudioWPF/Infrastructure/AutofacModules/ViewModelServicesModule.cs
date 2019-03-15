using Autofac;
using ResultStudioWPF.Models.Services;
using ResultStudioWPF.ViewModels;
using ResultStudioWPF.ViewModels.Services;

namespace ResultStudioWPF.Infrastructure.AutofacModules
{
  public class ViewModelServicesModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<DataFileReader>().As<IDataFileReader>();
      builder.RegisterType<AnalyzeDataSet>().As<IAnalyseDataSet>();
      builder.RegisterType<DataCreator>().As<IDataCreator>();

      builder.RegisterType<SettingsViewModel>().AsSelf().SingleInstance();
      builder.RegisterType<ResultsViewModel>().AsSelf().SingleInstance();
    }
  }
}

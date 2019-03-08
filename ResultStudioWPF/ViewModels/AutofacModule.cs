using System;
using Autofac;
using ResultStudioWPF.Helpers;
using ResultStudioWPF.ViewModels.Services;

namespace ResultStudioWPF.ViewModels
{
  public class AutofacModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<AnalyseDataSet>().As<IAnalyseDataSet>();
      builder.RegisterType<DataFileReader>().As<IDataFileReader>();

      builder.RegisterType<ResultsViewModel>().AsSelf().SingleInstance();
      builder.RegisterType<SettingsViewModel>().AsSelf().SingleInstance();

      //Type[] types =
      //{
      //  typeof (AnalyseDataSet),
      //  typeof (DataFileReader)
      //};
      //builder.RegisterTypes(types).AsImplementedInterfaces();
    }
  }
}

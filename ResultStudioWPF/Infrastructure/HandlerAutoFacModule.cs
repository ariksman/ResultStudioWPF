using System.Reflection;
using Autofac;
using ResultStudioWPF.Application.CQS;
using ResultStudioWPF.Common.CQS;
using Module = Autofac.Module;

namespace ResultStudioWPF.Infrastructure
{
  public class HandlerAutoFacModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      Assembly applicationLayerAssembly = Assembly.Load("ResultStudioWPF.Application");

      builder.RegisterAssemblyTypes(applicationLayerAssembly)
        .AsClosedTypesOf(typeof(ICommandHandler<,>));
      builder.RegisterAssemblyTypes(applicationLayerAssembly)
        .AsClosedTypesOf(typeof(IQueryHandler<,>));

      Assembly viewLayerAssembly = Assembly.Load("ResultStudioWPF.View");

      builder.RegisterAssemblyTypes(viewLayerAssembly)
        .AsClosedTypesOf(typeof(ICommandHandler<,>));
      builder.RegisterAssemblyTypes(viewLayerAssembly)
        .AsClosedTypesOf(typeof(IQueryHandler<,>));


      builder.RegisterType<QueryDispatcher>().As<IQueryDispatcher>();
      builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();
    }
  }
}
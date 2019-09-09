using System.Threading.Tasks;
using Autofac;
using CSharpFunctionalExtensions;
using ResultStudioWPF.Application.CQS;

namespace ResultStudioWPF.Common.CQS
{
  public class CommandDispatcher : ICommandDispatcher
  {
    private readonly ILifetimeScope _scope;

    public CommandDispatcher(ILifetimeScope scope)
    {
      _scope = scope;
    }

    public TResult Dispatch<TCommand, TResult>(TCommand command)
      where TCommand : ICommand
      where TResult : IResult
    {
      var handler = _scope.Resolve<ICommandHandler<TCommand, TResult>>();
      return handler.Handle(command);
    }

    public Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command)
      where TCommand : ICommand
      where TResult : IResult
    {
      var handler = _scope.Resolve<ICommandHandler<TCommand, TResult>>();
      return handler.HandleAsync(command);
    }
  }
}
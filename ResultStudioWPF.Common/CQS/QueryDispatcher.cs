using System.Threading.Tasks;
using Autofac;
using ResultStudioWPF.Application.CQS;

namespace ResultStudioWPF.Common.CQS
{
  public class QueryDispatcher : IQueryDispatcher
  {
    private readonly ILifetimeScope _scope;

    public QueryDispatcher(ILifetimeScope scope)
    {
      _scope = scope;
    }

    public TResult Dispatch<TQuery, TResult>(TQuery query)
      where TQuery : IQuery
      where TResult : new()
    {
      var handler = _scope.Resolve<IQueryHandler<TQuery, TResult>>();
      return handler.Handle(query);
    }

    public Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query)
      where TQuery : IQuery
      where TResult : new()
    {
      var handler = _scope.Resolve<IQueryHandler<TQuery, TResult>>();
      return handler.HandleAsync(query);
    }
  }
}

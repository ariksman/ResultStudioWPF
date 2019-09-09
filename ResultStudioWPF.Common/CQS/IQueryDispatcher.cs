using System.Threading.Tasks;
using ResultStudioWPF.Common.CQS;

namespace ResultStudioWPF.Application.CQS
{
  public interface IQueryDispatcher
  {
    /// <summary>
    /// Dispatches a query and retrieves a query result
    /// </summary>
    /// <typeparam name="TQuery">Request to execute type</typeparam>
    /// <typeparam name="TResult">Request Result to get back type</typeparam>
    /// <param name="query">Request to execute</param>
    /// <returns>Request Result to get back</returns>
    TResult Dispatch<TQuery, TResult>(TQuery query)
      where TQuery : IQuery
      where TResult : new();

    /// <summary>
    /// Dispatches a query and retrieves am async query result
    /// </summary>
    /// <typeparam name="TQuery">Request to execute type</typeparam>
    /// <typeparam name="TResult">Request Result to get back type</typeparam>
    /// <param name="query">Request to execute</param>
    /// <returns>Request Result to get back</returns>
    Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query)
      where TQuery : IQuery
      where TResult : new();
  }
}
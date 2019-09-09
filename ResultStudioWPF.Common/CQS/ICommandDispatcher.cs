using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace ResultStudioWPF.Common.CQS
{
  public interface ICommandDispatcher
  {
    /// <summary>
    /// Dispatches a command to its handler
    /// </summary>
    /// <typeparam name="TCommand">Command Type</typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="command">The command to be passed to the handler</param>
    TResult Dispatch<TCommand, TResult>(TCommand command)
      where TCommand : ICommand
      where TResult : IResult;


    /// <summary>
    /// Dispatches an async command to its handler
    /// </summary>
    /// <typeparam name="TCommand">Command Type</typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="command">The command to be passed to the handler</param>
    Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command)
      where TCommand : ICommand
      where TResult : IResult;
  }
}
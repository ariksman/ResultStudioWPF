using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ResultStudioWPF.Common.CQS;
using ResultStudioWPF.Domain.DomainModel.Entities;
using ResultStudioWPF.Domain.Interfaces;
using ResultStudioWPF.Domain.UseCases.DataSet;

namespace ResultStudioWPF.Application.UseCaseHandlers
{
  public class GetRandomDataQueryHandler : IQueryHandler<GetRandomDataSetQuery, Result<DataSet>>
  {
    private readonly IDataCreator _dataCreator;

    public GetRandomDataQueryHandler(IDataCreator dataCreator)
    {
      _dataCreator = dataCreator ?? throw new ArgumentException(nameof(dataCreator));
    }

    public Result<DataSet> Handle(GetRandomDataSetQuery query)
    {
      try
      {
        IDataCreator dataCreator = _dataCreator;
        var data = dataCreator.CreateSubframeDataset(
          query.Reference,
          query.FrameCount,
          100,
          query.Progress);

        var dataSet = DataSet.Create("Random data in use", data);

        return dataSet;
      }
      catch (Exception e)
      {
        return Result.Fail<DataSet>(e.Message);
      }
    }

    public Task<Result<DataSet>> HandleAsync(GetRandomDataSetQuery query)
    {
      throw new NotImplementedException();
    }
  }
}

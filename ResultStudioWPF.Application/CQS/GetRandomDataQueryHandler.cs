using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ResultStudioWPF.Common.CQS;
using ResultStudioWPF.Domain.CQS.DataSet;
using ResultStudioWPF.Domain.Interfaces;

namespace ResultStudioWPF.Application.CQS
{
  public class GetRandomDataQueryHandler : IQueryHandler<GetRandomDataSetQuery, Result<ObservableCollection<IMeasurementPoint>>>
  {
    private readonly IDataCreator _dataCreator;

    public GetRandomDataQueryHandler(IDataCreator dataCreator)
    {
      _dataCreator = dataCreator;
    }

    public Result<ObservableCollection<IMeasurementPoint>> Handle(GetRandomDataSetQuery query)
    {
      try
      {
        IDataCreator dataCreator = _dataCreator;
        var data = dataCreator.CreateSubframeDataset(
          query.Reference,
          query.FrameCount,
          100,
          query.Progress);

        return Result.Ok(data);
      }
      catch (Exception e)
      {
        return Result.Fail<ObservableCollection<IMeasurementPoint>>(e.Message);
      }
    }

    public Task<Result<ObservableCollection<IMeasurementPoint>>> HandleAsync(GetRandomDataSetQuery query)
    {
      throw new NotImplementedException();
    }
  }
}

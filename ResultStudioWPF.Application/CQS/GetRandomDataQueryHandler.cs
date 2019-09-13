using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ResultStudioWPF.Application.Interfaces;
using ResultStudioWPF.Common.CQS;
using ResultStudioWPF.Domain.CQS.DataSet;
using ResultStudioWPF.Domain.DomainModel.Entities;
using ResultStudioWPF.Domain.Interfaces;
using ResultStudioWPF.Domain.Services;

namespace ResultStudioWPF.Application.CQS
{
  public class GetRandomDataQueryHandler : IQueryHandler<GetRandomDataSetQuery, Result<DataSet>>
  {
    private readonly IDataCreator _dataCreator;

    public GetRandomDataQueryHandler(IDataCreator dataCreator)
    {
      _dataCreator = dataCreator;
    }

    public Result<DataSet> Handle(GetRandomDataSetQuery query)
    {
      throw new NotImplementedException();
    }

    public Task<Result<DataSet>> HandleAsync(GetRandomDataSetQuery query)
    {
      throw new NotImplementedException();
    }
  }
}

using System;
using ResultStudioWPF.Common.CQS;

namespace ResultStudioWPF.Domain.UseCases.DataSet
{
  public class GetDataSetFromFile : IQuery
  {
    public IProgress<int> Progress { get; }

    public GetDataSetFromFile(IProgress<int> progress = null)
    {
      Progress = progress;
    }
  }
}
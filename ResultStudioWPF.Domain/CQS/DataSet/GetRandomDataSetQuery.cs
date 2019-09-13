using System;
using ResultStudioWPF.Common.CQS;

namespace ResultStudioWPF.Domain.CQS.DataSet
{
  public class GetRandomDataSetQuery : IQuery
  {
    public IProgress<int> Progress { get; }
    public Reference Reference { get; }

    public GetRandomDataSetQuery(double xReference, double yReference, double zReference, IProgress<int> progress = null)
    {
      Progress = progress;
      Reference = new Reference(xReference, yReference, zReference);
    }
  }
}

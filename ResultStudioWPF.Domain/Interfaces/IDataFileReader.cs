using System.Collections.Generic;
using CSharpFunctionalExtensions;
using ResultStudioWPF.Domain.DomainModel.Entities;

namespace ResultStudioWPF.Domain.Interfaces
{
  public interface IDataFileReader
  {
    Result<DataSet> ReadFileIntoDataSet();
  }
}
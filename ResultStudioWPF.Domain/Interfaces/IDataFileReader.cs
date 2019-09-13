using System.Collections.Generic;
using ResultStudioWPF.Domain.DomainModel.Entities;

namespace ResultStudioWPF.Domain.Interfaces
{
  public interface IDataFileReader
  {
    List<MeasurementPoint> ReadFileIntoDataSet();
  }
}
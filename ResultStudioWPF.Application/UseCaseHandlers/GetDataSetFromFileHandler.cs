using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ResultStudioWPF.Application.Interfaces;
using ResultStudioWPF.Common.CQS;
using ResultStudioWPF.Domain.DomainModel.Entities;
using ResultStudioWPF.Domain.DomainModel.ValueObjects;
using ResultStudioWPF.Domain.Interfaces;
using ResultStudioWPF.Domain.UseCases.DataSet;

namespace ResultStudioWPF.Application.UseCaseHandlers
{
  public class GetDataSetFromFileHandler : IQueryHandler<GetDataSetFromFile, Result<List<MeasurementPoint>>>
  {
    private readonly Func<FilePath, IDataFileReader> _dataFileReaderFunc;
    private readonly IFileDialogProvider _fileDialogProvider;

    public GetDataSetFromFileHandler(Func<FilePath, IDataFileReader> dataFileReaderFunc, IFileDialogProvider fileDialogProvider)
    {
      _dataFileReaderFunc = dataFileReaderFunc ?? throw new ArgumentException(nameof(dataFileReaderFunc));
      _fileDialogProvider = fileDialogProvider ?? throw new ArgumentException(nameof(fileDialogProvider));
    }

    public Result<List<MeasurementPoint>> Handle(GetDataSetFromFile query)
    {
      var filter = "Text|*.txt|All|*.*";
      var files = _fileDialogProvider.GetFilePaths(filter);
      var file = FilePath.Create(files.FirstOrDefault());

      if (file.IsFailure) return Result.Fail<List<MeasurementPoint>>("Error while getting the file path");

      var reader = _dataFileReaderFunc(file.Value);
      var data = reader.ReadFileIntoDataSet();

      return Result.Ok(data);
    }

    public Task<Result<List<MeasurementPoint>>> HandleAsync(GetDataSetFromFile query)
    {
      throw new NotImplementedException();
    }
  }
}
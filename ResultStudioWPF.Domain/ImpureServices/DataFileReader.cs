using System;
using System.Collections.Generic;
using System.IO;
using CSharpFunctionalExtensions;
using ResultStudioWPF.Domain.DomainModel.Entities;
using ResultStudioWPF.Domain.DomainModel.Enumerations;
using ResultStudioWPF.Domain.DomainModel.ValueObjects;
using ResultStudioWPF.Domain.Interfaces;

namespace ResultStudioWPF.Domain.ImpureServices
{
  public class DataFileReader : IDataFileReader
  {
    private readonly FilePath _path;
    private readonly IProgress<int> _reportProgress;

    public DataFileReader(FilePath path, IProgress<int> progress = null)
    {
      _path = path ?? throw new ArgumentException(nameof(path));
      _reportProgress = progress;
    }

    public Result<DataSet> ReadFileIntoDataSet()
    {
      using (var sr = new StreamReader(_path.Value))
      {
        var measurementPoints = new List<MeasurementPoint>();

        string line = "";
        char[] charsToTrim = {' '};
        var progressCount = 1;
        while ((line = sr.ReadLine()) != null)
        {
          line = line.Trim();
          if (line.Length == 0) continue; // empty line

          string[] parts = ParseLineIntoMeasurementPointArray(line);

          Int32.TryParse(parts[0], out var measurementNumber);
          MeasurementAxisType axisValue = ParseAxisValue(parts[1]);
          Double.TryParse(parts[2], out var measurement);

          var newMeasurementPoint = new MeasurementPoint(measurementNumber, measurement, axisValue);

          measurementPoints.Add(newMeasurementPoint);
          _reportProgress?.Report(progressCount++);
        }

        var dataSet = DataSet.Create(_path.Value, measurementPoints);

        return dataSet;
      }
    }

    private MeasurementAxisType ParseAxisValue(string axisString)
    {
      return MeasurementAxisType.GetType(axisString);
    }

    private string[] ParseLineIntoMeasurementPointArray(string line)
    {
      return line.Split('\t');
    }
  }
}

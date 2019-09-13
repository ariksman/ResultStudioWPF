using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ResultStudioWPF.Domain.DomainModel;
using ResultStudioWPF.Domain.DomainModel.Entities;
using ResultStudioWPF.Domain.DomainModel.Enumerations;
using ResultStudioWPF.Domain.DomainModel.ValueObjects;
using ResultStudioWPF.Domain.Interfaces;

namespace ResultStudioWPF.Domain.Services
{
  public class DataCreator : IDataCreator
  {
    private readonly Random _rnd;

    public DataCreator()
    {
      _rnd = new Random();
    }

    public ObservableCollection<IMeasurementPoint> CreateSubframeDataset(Reference reference, int subframeCount,
      double spread, IProgress<int> progress)
    {
      progress.Report(1);

      var dataSetX = CreateRandomListOfNumber(subframeCount, reference.X, spread);
      var dataSetY = CreateRandomListOfNumber(subframeCount, reference.Y, spread);
      var dataSetZ = CreateRandomListOfNumber(subframeCount, reference.Z, spread);

      return AddMeasurementsToDataset(dataSetX, dataSetY, dataSetZ, subframeCount, progress);
    }

    private ObservableCollection<IMeasurementPoint> AddMeasurementsToDataset(IList<double> dataSetX,
      IList<double> dataSetY, IList<double> dataSetZ, int subframeCount, IProgress<int> progress)
    {
      var dataSet = new ObservableCollection<IMeasurementPoint>();

      for (int i = 1; i < subframeCount + 1; i++)
      {
        var newMeasurementX = new MeasurementPoint(i, dataSetX.ElementAtOrDefault(i - 1), MeasurementAxisType.X);
        dataSet.Add(newMeasurementX);

        var newMeasurementY = new MeasurementPoint(i, dataSetY.ElementAtOrDefault(i - 1), MeasurementAxisType.Y);
        dataSet.Add(newMeasurementY);

        var newMeasurementZ = new MeasurementPoint(i, dataSetZ.ElementAtOrDefault(i - 1), MeasurementAxisType.Z);
        dataSet.Add(newMeasurementZ);

        progress.Report(i * 3);
      }

      return dataSet;
    }

    private IList<double> CreateRandomListOfNumber(int numberOfElements, double reference, double spread)
    {
      var datasetX = new List<double>();

      var maximum = reference + spread;
      var minimum = reference - spread;

      for (int i = 1; i < numberOfElements + 1; i++)
      {
        datasetX.Add(GetRandomNumber(minimum, maximum));
      }

      return datasetX;
    }

    private double GetRandomNumber(double minimum, double maximum)
    {
      return _rnd.NextDouble() * (maximum - minimum) + minimum;
    }
  }
}
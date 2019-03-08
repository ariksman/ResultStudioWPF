using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ResultStudioWPF.Models.Services
{
    public class DataCreator
    {
        private readonly Random _rnd;

        public DataCreator()
        {
            _rnd = new Random();
        }

        public ObservableCollection<MeasurementPoint> CreateSubframeDataset(double referenceX, double referenceY, double referenceZ, int subframeCount, double spread, IProgress<int> progress)
        {

            progress.Report(1);

            var dataSetX = CreateRandomListOfNumber(subframeCount, referenceX, spread);
            var dataSetY = CreateRandomListOfNumber(subframeCount, referenceY, spread);
            var dataSetZ = CreateRandomListOfNumber(subframeCount, referenceZ, spread);

            return AddMeasurementsToDataset(dataSetX, dataSetY, dataSetZ, subframeCount, progress);

        }

        private ObservableCollection<MeasurementPoint> AddMeasurementsToDataset(IList<double> dataSetX, IList<double> dataSetY, IList<double> dataSetZ, int subframeCount, IProgress<int> progress)
        {
            var dataset = new ObservableCollection<MeasurementPoint>();

            for (int i = 1; i < subframeCount+1; i++)
            {
                var newMeasurementX = new MeasurementPoint()
                {
                    AxisName = Helpers.Constants.MeasurementAxis.X
                                ,
                    MeasurementNumber = i
                                ,
                    Value = dataSetX.ElementAtOrDefault(i - 1)
                };
                dataset.Add(newMeasurementX);

                var newMeasurementY = new MeasurementPoint()
                {
                    AxisName = Helpers.Constants.MeasurementAxis.Y
                                    ,
                    MeasurementNumber = i
                                    ,
                    Value = dataSetY.ElementAtOrDefault(i - 1)
                };
                dataset.Add(newMeasurementY);

                var newMeasurementZ = new MeasurementPoint()
                {
                    AxisName = Helpers.Constants.MeasurementAxis.Z
                                    ,
                    MeasurementNumber = i
                                    ,
                    Value = dataSetZ.ElementAtOrDefault(i - 1)
                };
                dataset.Add(newMeasurementZ);

                progress.Report(i*3);
            }

            return dataset;
        }

        private IList<double> CreateRandomListOfNumber(int numberOfElements, double reference, double spread)
        {
            var datasetX = new List<double>();

            var maximum = reference + spread;
            var minimum = reference - spread;

            for (int i = 1; i < numberOfElements+1; i++)
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

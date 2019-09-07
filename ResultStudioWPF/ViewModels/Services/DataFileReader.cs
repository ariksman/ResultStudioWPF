﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using ResultStudioWPF.Application.Helpers;
using ResultStudioWPF.Application.Interfaces;
using ResultStudioWPF.Models;

namespace ResultStudioWPF.ViewModels.Services
{
  public class DataFileReader : IDataFileReader
  {
    private string _theFile;
    private readonly IProgress<int> _reportProgress;
    private IList<MeasurementPoint> _dataSet;

    public DataFileReader(IProgress<int> progress)
    {
      DataSet = new List<MeasurementPoint>();
      _reportProgress = progress;
    }

    public IList<MeasurementPoint> DataSet
    {
      get { return _dataSet; }
      set
      {
        if (_dataSet == value)
        {
          return;
        }

        _dataSet = value;
      }
    }

    /// <summary>
    /// The initialization file path.
    /// </summary>
    public string TheFile
    {
      get { return _theFile; }
      set
      {
        _theFile = null;
        if (File.Exists(value))
        {
          _theFile = value;
          ReadFileIntoDataset();
        }
      }
    }

    public void ReadFile()
    {
      OpenFileDialog openFileDialog = new OpenFileDialog
      {
        InitialDirectory = @"c:\temp\",
        Filter = "Text|*.txt|All|*.*"
      };

      if (openFileDialog.ShowDialog() != true) return;

      var sFilePath = openFileDialog.FileName;
      TheFile = openFileDialog.FileName;
    }

    private void ReadFileIntoDataset()
    {
      using (StreamReader sr = new StreamReader(_theFile))
      {
        string line = "";
        char[] charsToTrim = {' '};
        var progressCount = 1;
        while ((line = sr.ReadLine()) != null)
        {
          line = line.Trim();
          if (line.Length == 0) continue; // empty line


          string[] parts = ParseLineIntoMeasurementPointArray(line);

          int measurementNumber;
          Int32.TryParse(parts[0], out measurementNumber);

          Constants.MeasurementAxis axisValue = ParseAxisValue(parts[1]);

          double measurement;
          Double.TryParse(parts[2], out measurement);

          var newMeasurementPoint = new MeasurementPoint()
          {
            MeasurementNumber = measurementNumber,
            AxisName = axisValue,
            Value = measurement,
          };

          _dataSet.Add(newMeasurementPoint);
          _reportProgress?.Report(progressCount++);
        }
      }
    }

    private Constants.MeasurementAxis ParseAxisValue(string axisString)
    {
      try
      {
        Constants.MeasurementAxis axisValue =
          (Constants.MeasurementAxis) Enum.Parse(typeof(Constants.MeasurementAxis), axisString);
        if (Enum.IsDefined(typeof(Constants.MeasurementAxis), axisValue))
        {
          return axisValue;
        }

        return Constants.MeasurementAxis.Unknown;
      }
      catch (ArgumentException)
      {
        Console.WriteLine("'{0}' is not a member of the MeasurementAxis enumeration.", axisString);
        return Constants.MeasurementAxis.Unknown;
      }
    }

    private string[] ParseLineIntoMeasurementPointArray(string line)
    {
      return line.Split('\t');
    }
  }
}

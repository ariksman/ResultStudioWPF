using ResultStudioWPF.Domain.DomainModel.Enumerations;
using ResultStudioWPF.Domain.DomainModel.ValueObjects;
using ResultStudioWPF.Domain.Interfaces;

namespace ResultStudioWPF.ViewModels
{
  public class MeasurementPointViewModel : ModelBase, IMeasurementPoint
  {
    public MeasurementPointViewModel()
    {
    }

    #region Public roperties

    private int _indexNumber;

    public int Index
    {
      get => _indexNumber;
      set { _indexNumber = value; }
    }

    private MeasurementAxisType _axisName;

    public MeasurementAxisType Axis
    {
      get => _axisName;
      set
      {
        if (_axisName == value)
        {
          return;
        }

        _axisName = value;
      }
    }

    private Tolerance _tolerance;

    public Tolerance Tolerance
    {
      get => _tolerance;
      set
      {
        _tolerance = value;
        NotifyPropertyChanged();
      }
    }

    private double _value;

    public double Value
    {
      get => _value;
      set
      {
        _value = value;
        CheckValueTolerance(value);

        NotifyPropertyChanged();
      }
    }

    #endregion

    #region Public methods

    public void RefreshAllProperties()
    {
      foreach (var prop in GetType().GetProperties())
        NotifyPropertyChanged(null, prop.Name);
    }

    public void CheckAllTolerances()
    {
      foreach (var propertyInfo in GetType().GetProperties())
      {
        if (propertyInfo.Name == "Value")
        {
          CheckValueTolerance(Value);
        }
      }
    }

    #endregion

    #region Private methods

    private void CheckValueTolerance(double value)
    {
      if (Axis == MeasurementAxisType.X)
      {
        var xTolerance = new ViewModelLocator().SettingsViewModel.XAxisTolerance;
        var xReference = new ViewModelLocator().SettingsViewModel.XAxisReference;

        if (_value > xReference + xTolerance)
        {
          AddError("Value",
            "Given measurement value (" + _value + ") is higher than tolerance " + xTolerance + " allows");
        }
        else if (value < xReference - xTolerance)
        {
          AddError("Value",
            "Given measurement value (" + _value + ") is lower than tolerance " + xTolerance +
            " allows");
        }
        else
        {
          RemoveError("Value");
        }

        return;
      }

      if (Axis == MeasurementAxisType.Y)
      {
        var yTolerance = new ViewModelLocator().SettingsViewModel.YAxisTolerance;
        var yReference = new ViewModelLocator().SettingsViewModel.YAxisReference;

        if (_value > yReference + yTolerance)
        {
          AddError("Value",
            "Given measurement value (" + _value + ") is higher than tolerance " + yTolerance + " allows");
        }
        else if (value < yReference - yTolerance)
        {
          AddError("Value",
            "Given measurement value (" + _value + ") is lower than tolerance " + yTolerance +
            " allows");
        }
        else
        {
          RemoveError("Value");
        }

        return;
      }

      if (Axis == MeasurementAxisType.Z)
      {
        var zTolerance = new ViewModelLocator().SettingsViewModel.ZAxisTolerance;
        var zReference = new ViewModelLocator().SettingsViewModel.ZAxisReference;

        if (_value > zReference + zTolerance)
        {
          AddError("Value",
            "Given measurement value (" + _value + ") is higher than tolerance " + zTolerance + " allows");
        }
        else if (value < zReference - zTolerance)
        {
          AddError("Value",
            "Given measurement value (" + _value + ") is lower than tolerance " + zTolerance +
            " allows");
        }
        else
        {
          RemoveError("Value");
        }

        return;
      }
    }

    #endregion
  }
}
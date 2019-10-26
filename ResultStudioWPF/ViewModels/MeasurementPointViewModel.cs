using ResultStudioWPF.Domain.DomainModel.Enumerations;
using ResultStudioWPF.Domain.DomainModel.ValueObjects;
using ResultStudioWPF.Domain.Interfaces;

namespace ResultStudioWPF.ViewModels
{
  public class MeasurementPointViewModel : ModelBase, IMeasurementPoint
  {
    private readonly ISharedSettingsContext _settingsContext;

    public MeasurementPointViewModel(ISharedSettingsContext settingsContext)
    {
      _settingsContext = settingsContext;
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
        UpdateAllBindings();
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
        var xTolerance = _settingsContext.XAxisTolerance;
        var xReference = _settingsContext.XAxisReference;

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
        var yTolerance = _settingsContext.YAxisTolerance;
        var yReference = _settingsContext.YAxisReference;

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
        var zTolerance = _settingsContext.ZAxisTolerance;
        var zReference = _settingsContext.ZAxisReference;

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
using ResultStudioWPF.Domain.DomainModel.Enumerations;
using ResultStudioWPF.Domain.DomainModel.ValueObjects;
using ResultStudioWPF.Domain.Interfaces;

namespace ResultStudioWPF.ViewModels
{
  public class MeasurementPointViewModel : ModelBase, IMeasurementPoint
  {
    public MeasurementPointModel Model { get; }
    private readonly ISharedSettingsContext _settingsContext;

    public MeasurementPointViewModel(MeasurementPointModel model, ISharedSettingsContext settingsContext)
    {
      Model = model;
      _settingsContext = settingsContext;
    }

    #region Public roperties

    public int Index
    {
      get => Model.Index;
      set { Model.Index = value; }
    }

    public MeasurementAxisType Axis
    {
      get => Model.MeasurementAxisType;
      set
      {
        if (Model.MeasurementAxisType == value)
        {
          return;
        }

        Model.MeasurementAxisType = value;
      }
    }


    public Tolerance Tolerance
    {
      get => Model.Tolerance;
      set
      {
        Model.Tolerance = value;
        NotifyPropertyChanged();
      }
    }


    public double Value
    {
      get => Model.Value;
      set
      {
        Model.Value = value;
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

        if (Model.Value > xReference + xTolerance)
        {
          AddError("Value",
            "Given measurement value (" + Model.Value + ") is higher than tolerance " + xTolerance + " allows");
        }
        else if (value < xReference - xTolerance)
        {
          AddError("Value",
            "Given measurement value (" + Model.Value + ") is lower than tolerance " + xTolerance +
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

        if (Model.Value > yReference + yTolerance)
        {
          AddError("Value",
            "Given measurement value (" + Model.Value + ") is higher than tolerance " + yTolerance + " allows");
        }
        else if (value < yReference - yTolerance)
        {
          AddError("Value",
            "Given measurement value (" + Model.Value + ") is lower than tolerance " + yTolerance +
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

        if (Model.Value > zReference + zTolerance)
        {
          AddError("Value",
            "Given measurement value (" + Model.Value + ") is higher than tolerance " + zTolerance + " allows");
        }
        else if (value < zReference - zTolerance)
        {
          AddError("Value",
            "Given measurement value (" + Model.Value + ") is lower than tolerance " + zTolerance +
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
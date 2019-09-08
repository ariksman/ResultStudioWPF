using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ResultStudioWPF.Domain
{
  /// <summary> An enumeration.
  ///           https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types
  ///           https://lostechies.com/jimmybogard/2008/08/12/enumeration-classes/
  ///            </summary>
  public abstract class Enumeration : IComparable
  {
    private readonly int _value;
    private readonly string _displayName;

    protected Enumeration()
    {
    }

    protected Enumeration(int value, string displayName)
    {
      _value = value;
      _displayName = displayName;
    }

    public int Value
    {
      get { return _value; }
    }

    public string DisplayName
    {
      get { return _displayName; }
    }

    public override string ToString()
    {
      return DisplayName;
    }

    public static IEnumerable<T> GetAll<T>() where T : Enumeration
    {
      var fields = typeof(T).GetFields(BindingFlags.Public |
                                       BindingFlags.Static |
                                       BindingFlags.DeclaredOnly);

      return fields.Select(f => f.GetValue(null)).Cast<T>();
    }

    public override bool Equals(object obj)
    {
      var otherValue = obj as Enumeration;

      if (otherValue == null)
      {
        return false;
      }

      var typeMatches = GetType().Equals(obj.GetType());
      var valueMatches = _value.Equals(otherValue.Value);

      return typeMatches && valueMatches;
    }

    public override int GetHashCode()
    {
      return _value.GetHashCode();
    }

    public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
    {
      var absoluteDifference = Math.Abs(firstValue.Value - secondValue.Value);
      return absoluteDifference;
    }

    public static T FromValue<T>(int value) where T : Enumeration
    {
      var matchingItem = Parse<T, int>(value, "value", item => item.Value == value);
      return matchingItem;
    }

    public static T FromDisplayName<T>(string displayName) where T : Enumeration
    {
      var matchingItem = Parse<T, string>(displayName, "display name", item => item.DisplayName == displayName);
      return matchingItem;
    }

    private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
    {
      var matchingItem = GetAll<T>().FirstOrDefault(predicate);

      if (matchingItem == null)
      {
        var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
        throw new ApplicationException(message);
      }

      return matchingItem;
    }

    public int CompareTo(object other)
    {
      return Value.CompareTo(((Enumeration)other).Value);
    }
  }
}
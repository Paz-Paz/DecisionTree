using EntscheidungsbaumLernen.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace EntscheidungsbaumLernen.Helper
{
  #region STATIC CLASS KlassenHelper .....................................................................................

  /// <summary>
  /// Helfer für alles das mit dem Handling von Klassen zu tun hat.
  /// </summary>
  /// <remarks>
  /// <br /><b>Versionen:</b><br />
  /// V1.0 06.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  internal static class KlassenHelper
  {

    #region Paket-Interne Methoden .........................................................................................

    /// <inheritdoc cref="IHelper.ErstellAttributliste{TBsp, TResult}"/>
    internal static List<Type> ErstellAttributliste<TBsp, TResult>() where TBsp : class where TResult : Enum
    {
      List<Type> response = new List<Type>();

      PropertyInfo[] properties = typeof(TBsp).GetProperties();
      foreach (PropertyInfo propertyInfo in properties)
      {
        if (propertyInfo.PropertyType == typeof(TResult))
        {
          continue;
        }
        if (!propertyInfo.CanRead)
        {
          continue;
        }
        if (propertyInfo.PropertyType.IsEnum)
        {
          response.Add(propertyInfo.PropertyType);
        }
      };

      return response;
    }


    /// <summary>
    /// Liefert den Wert des Attributs mit dem übergebenen Typen.
    /// </summary>
    /// <typeparam name="T">Typ des auszulesenden Objekts.</typeparam>
    /// <param name="auszulesen">Auszulesendes Objekt.</param>
    /// <param name="enumType">Typ der Eigenschaft die Ausgelesn werden soll.</param>
    /// <returns>Ausgelesener Wert, oder 'null' wenn nicht auslesbar oder auffindbar.</returns>
    /// <exception cref="ArgumentException">Wenn der übergebe <paramref name="enumType"/> kein <see cref="Enum"/> ist.</exception>
    internal static object GetValue<T>(T auszulesen, Type enumType) where T : class
    {
      Checks.EnumCheck(enumType);

      PropertyInfo[] propertyInfos = auszulesen.GetType().GetProperties();
      foreach (PropertyInfo propertyInfo in propertyInfos)
      {
        if (propertyInfo.PropertyType != enumType)
        {
          continue;
        }

        if (!propertyInfo.CanRead)
        {
          Console.WriteLine($"Die Eigenschaft '{propertyInfo}' vom Objekt '{typeof(T)}' ist nicht lesbar.");
          continue;
        }

        return propertyInfo.GetValue(auszulesen);
      }

      return null;
    }

    #endregion .............................................................................................................
  }

  #endregion .............................................................................................................
}

using System;
using System.Collections.Generic;
using System.Reflection;

namespace EntscheidungsbaumLernen.Helper
{
  #region STATIC CLASS KlassenHelper .....................................................................................

  internal static class KlassenHelper
  {

    #region Paket-Interne Methoden .........................................................................................

    /// <summary>
    /// Erstellt eine Liste aller lesbaren Enum-Attribute des übergebenen Typs.
    /// </summary>
    /// <typeparam name="TBsp">Typ dessen Attribute ausgelesen werden sollen.</typeparam>
    /// <typeparam name="TResult">Typ der Eigenschaft, die für die Listenerstellung ignoriert werden soll.</typeparam>
    /// <returns>Liste der Attribute des Typs. (Kann auch eine leere Liste sein)</returns>
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

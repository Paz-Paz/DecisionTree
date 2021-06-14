using System;

namespace EntscheidungsbaumLernen.Helper
{
  #region STATIC CLASS Checks ............................................................................................

  /// <summary>
  /// Klasse für diverse Prüfungen.
  /// </summary>
  /// <remarks>
  /// <br /><b>Versionen:</b><br />
  /// V1.0 06.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  internal static class Checks
  {
    #region Paket-Interne Methoden .........................................................................................

    /// <summary>
    /// Prüft ob der übergebene Typ ein Enum ist. Wenn nicht wird eine <see cref="ArgumentException"/> ausgelöst.
    /// </summary>
    /// <param name="type">Zu prüfender Type.</param>
    /// <exception cref="ArgumentException">Wenn der übergebe <paramref name="type"/> kein <see cref="Enum"/> ist.</exception>
    internal static void EnumCheck(Type type)
    {
      if (!type.IsEnum)
      {
        throw new ArgumentException($"'{type.GetType()}' ist kein Enum.");
      }
    }

    #endregion .............................................................................................................
  }

  #endregion .............................................................................................................
}

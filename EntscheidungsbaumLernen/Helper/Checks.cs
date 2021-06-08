using System;

namespace EntscheidungsbaumLernen.Helper
{
  #region STATIC CLASS Checks ............................................................................................

  internal static class Checks
  {
    #region Paket-Interne Methoden .........................................................................................

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

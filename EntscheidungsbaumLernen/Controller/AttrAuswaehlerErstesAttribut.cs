using EntscheidungsbaumLernen.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntscheidungsbaumLernen.Controller
{
  #region CLASS AttrAuswaehlerDummy<T> ...................................................................................

  internal class AttrAuswaehlerErstesAttribut<TBsp, TResult> : IAttributAuswaehler<TBsp, TResult> where TBsp : class where TResult : Enum
  {
    #region Eigenschaften ..................................................................................................

    private Type _letztes = null;

    #endregion .............................................................................................................
    #region Konstruktor ....................................................................................................
    public AttrAuswaehlerErstesAttribut()
    {
      Console.WriteLine("\nVerwendeter Attributsauswahlalgorithmus: Dummy-Auswahl");

    }

    #endregion .............................................................................................................
    #region Oeffentliche Methoden ..........................................................................................

    /// <inheritdoc/>
    public Type ChoosAttribute(in List<TBsp> beispielliste, in List<Type> attributsliste)
    {
      return attributsliste.First();
      //if (this._letztes == null)
      //{
      //  this._letztes = typeof(Gruppe);
      //  return typeof(Gruppe);
      //}
      //if (this._letztes == typeof(Gruppe))
      //{
      //  this._letztes = typeof(Kategorie);
      //  return typeof(Kategorie);
      //}
      //return typeof(Wetter);
    }

    #endregion .............................................................................................................
  }

  #endregion .............................................................................................................
}

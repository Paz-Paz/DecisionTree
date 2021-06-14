using EntscheidungsbaumLernen.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntscheidungsbaumLernen.Controller
{
  #region CLASS AttrAuswaehlerDummy<T> ...................................................................................

  /// <summary>
  /// Attributauswähler, welcher einfach immer das erste verfügbare Attribut zurückgibt.
  /// </summary>
  /// <remarks>
  /// Überbleibsel aus der Entwicklung des Systems, aber für eventuelle Sonderfälle aufgehoben.<br />
  /// Schnelle Laufzeit ;) <br />
  /// <br /><b>Versionen:</b><br />
  /// V1.0 06.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  /// <typeparam name="TBsp">Typ der Klasse der Beispieldaten.</typeparam>
  /// <typeparam name="TResult">Typ der Eigenschaft des Ergebnisses</typeparam>
  internal class AttrAuswaehlerErstesAttribut<TBsp, TResult> : IAttributAuswaehler<TBsp, TResult> where TBsp : class where TResult : Enum
  {
    #region Konstruktor ....................................................................................................

    /// <summary>
    /// Liefert ein <see cref="AttrAuswaehlerErstesAttribut{TBsp, TResult}"/>-Obekt.
    /// </summary>
    internal AttrAuswaehlerErstesAttribut()
    {
      Console.WriteLine("\nVerwendeter Attributsauswahlalgorithmus: Erstes Attribut-Auswahl");
    }

    #endregion .............................................................................................................
    #region Oeffentliche Methoden ..........................................................................................

    /// <inheritdoc/>
    public Type ChoosAttribute(in List<TBsp> beispielliste, in List<Type> attributsliste)
    {
      return attributsliste.First();
    }

    #endregion .............................................................................................................
  }

  #endregion .............................................................................................................
}

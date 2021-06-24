using System;

namespace EntscheidungsbaumLernen.Interfaces
{
  #region CLASS IDialogAnwenden ..........................................................................................

  /// <summary>
  /// Interface um "Anwendern" eine einfache Schnittstelle für die Nutzung des Entscheidungsbaumes anzubieten.
  /// </summary>
  /// <remarks>
  /// <br /><b>Versionen:</b><br />
  /// V1.0 14.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  /// <typeparam name="TEingabe">Typ der Klasse, die für die EIngabe verwendet wird.</typeparam>
  /// <typeparam name="TResult">Typ der Eigenschaft in <typeparamref name="TEingabe"/> welche das Ergebnis darstellt.</typeparam>
  public interface IDialogAnwenden<TEingabe, TResult> where TEingabe : class where TResult : Enum
  {
    #region Oeffentliche Methoden ..........................................................................................

    /// <summary>
    /// Stellt dem Benutzer Fragen und anhand der Antworten wird dann das Ergebnis generiert.
    /// </summary>
    /// <returns>Ergebnis der Auswertung.</returns>
    public TResult Abfragen();

    /// <summary>
    /// Wertet die übergebene <paramref name="eingabe"/> aus.
    /// </summary>
    /// <param name="eingabe">Eingabe-Objekt, welches ausgewerte werden soll.</param>
    /// <returns>Ergebnis der Auswertung.</returns>
    public TResult EingabeAuswerten(in TEingabe eingabe);

    #endregion .............................................................................................................
  }

  #endregion .............................................................................................................
}

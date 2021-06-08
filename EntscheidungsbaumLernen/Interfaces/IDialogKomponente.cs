using System;

namespace EntscheidungsbaumLernen.Interfaces
{
  /// <summary>
  /// Intefrace für die Klasse, welche die Dialogkomponente zum Benutzer darstellt.
  /// </summary>
  /// <remarks>
  /// <br /><b>Versionen:</b><br />
  /// V1.0 06.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  public interface IDialogKomponente
  {
    /// <summary>
    /// Methode zum Abfragen eines einzelnen Beispiels.
    /// </summary>
    /// <typeparam name="TEingabe">Typ des Eingabe-Objekts.</typeparam>
    /// <typeparam name="TResult">Eigenschaft die als Ergebniss erwartet wird.</typeparam>
    /// <param name="entscheidungsbaumWurzel">Wurzel der gespeicherten Entscheidungsbaum-Struktur.</param>
    /// <param name="auszulesen">Objekt, welches ausgewertet werden soll.</param>
    /// <returns></returns>
    public TResult Abfragen<TEingabe, TResult>(in IEntscheidungsbaumWurzel entscheidungsbaumWurzel, in TEingabe auszulesen) where TEingabe : class where TResult : Enum;
  }
}

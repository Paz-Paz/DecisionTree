using EntscheidungsbaumLernen.Controller;
using EntscheidungsbaumLernen.Interfaces;

namespace EntscheidungsbaumLernen.Factorys
{
  /// <summary>
  /// Factory zum Erzeugen der Klasse der Dialogkomponente(n)
  /// </summary>
  /// <remarks>
  /// <br /><b>Versionen:</b><br />
  /// V1.0 06.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  public class DialogKomponenteFactory
  {
    /// <summary>
    /// Erzeugt ein <see cref="IDialogKomponente"/>-Objekt aus den zuvor gemachten Einstellungen und gibt es zurück.
    /// </summary>
    /// <returns>Erzeugtes Objekt</returns>
    public IDialogKomponente Build()
    {
      return new DialogKomponente();
    }
  }
}

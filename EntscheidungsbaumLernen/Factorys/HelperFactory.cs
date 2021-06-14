using EntscheidungsbaumLernen.Controller;
using EntscheidungsbaumLernen.Interfaces;

namespace EntscheidungsbaumLernen.Factorys
{
  /// <summary>
  /// Factory zum Erzeugen der Klasse des Helpers
  /// </summary>
  /// <remarks>
  /// <br /><b>Versionen:</b><br />
  /// V1.0 06.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  public class HelperFactory
  {
    /// <summary>
    /// Erzeugt ein <see cref="IHelper"/>-Objekt aus den zuvor gemachten Einstellungen und gibt es zurück.
    /// </summary>
    /// <returns>Erzeugtes Objekt</returns>
    public IHelper Build()
    {
      return new HelperKlasse();
    }
  }
}

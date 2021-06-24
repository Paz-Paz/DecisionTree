using System;

namespace EntscheidungsbaumLernen.Interfaces
{
  #region CLASS IWissensspeicher .........................................................................................

  /// <summary>
  /// Inteface für die externe Darstellung des Wissensspeichers.
  /// </summary>
  /// <remarks>
  /// <br /><b>Versionen:</b><br />
  /// V1.1 14.06.2021 - Paz-Paz - 'Next', 'SetNaechsteInstanz' u. 'SpeichereBaum' hinzugefügt.<br />
  /// V1.0 06.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  internal interface IWissensspeicher
  {
    #region Getter/Setter ..................................................................................................

    /// <summary>
    /// Nächster Wissensspeicher in der Kette, oder Null wenn keiner mehr gespeichert ist.
    /// </summary>
    public IWissensspeicher Next { get; }

    #endregion .............................................................................................................
    #region Oeffentliche Methoden ..........................................................................................

    /// <summary>
    /// Lädet den gespeicherten Baum.
    /// </summary>
    /// <remarks>
    /// Dabei werden, die hinzugefügten einzelnen Wissensspeicher (z.B: "RAM", "Datei", ...) in der Reihenfolge des Hinzufüens ausgelesen bis das erste Ergebnis geliefert wird.
    /// </remarks>
    /// <returns>Den gefundenen Baum. (Bzw. dessen Wurzel <see cref="IEntscheidungsbaumWurzel"/>.</returns>
    /// <exception cref="InvalidCastException">Wenn nichts gespeichertes gefunden weden konnte.</exception>
    public IEntscheidungsbaumWurzel LadeBaum();

    /// <summary>
    /// Setzt den nächsten Wissensspeicher in der Speicher-Kette.
    /// </summary>
    /// <param name="wissensspeicher">Zu hinterlegendes Speicher-Objekt.</param>
    /// <exception cref="InvalidCastException">Wenn zuvor schon ein Wissensspeicher hinzugefügt wurde.</exception>
    public void SetNaechsteInstanz(IWissensspeicher wissensspeicher);

    /// <summary>
    /// Speichert den Baum.
    /// </summary>
    /// <remarks>
    /// Nach dem Speichern <b>muss</b> der nächste Wissensspeicher in der Kette aufgerufen werden, sofern es einen gibt!
    /// </remarks>
    /// <param name="wurzel">Wurzel des zu speichernden Baumes.</param>
    public void SpeichereBaum(IEntscheidungsbaumWurzel wurzel);

    #endregion .............................................................................................................

  }

  #endregion .............................................................................................................
}

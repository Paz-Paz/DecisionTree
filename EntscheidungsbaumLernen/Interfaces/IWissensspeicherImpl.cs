using System;

namespace EntscheidungsbaumLernen.Interfaces
{
  /// <summary>
  /// Interface für die Implementierung eines Wissensspeichers.
  /// </summary>
  /// <remarks>
  /// Wurde vom <see cref="IWissensspeicher"/> extrahiert um  später ein eigenes Erstellen von Wissensspeichern zu ermöglichen.<br />
  /// <br /><b>Versionen:</b><br />
  /// V1.0 06.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  internal interface IWissensspeicherImpl : IWissensspeicher
  {

    /// <summary>
    /// Nächster Wissensspeicher in der Reihe.
    /// </summary>
    internal IWissensspeicherImpl Next { get; }

    /// <summary>
    /// Setzt den nächsten Wissensspeicher in der Speicher-Kette.
    /// </summary>
    /// <param name="wissensspeicher">Zu hinterlegendes Speicher-Objekt.</param>
    /// <exception cref="InvalidCastException">Wenn zuvor schon ein Wissensspeicher hinzugefügt wurde.</exception>
    public void SetNaechsteInstanz(IWissensspeicherImpl wissensspeicher);

    /// <summary>
    /// Speichert den Baum.
    /// </summary>
    /// <remarks>
    /// Nach dem Speichern <b>muss</b> der nächste Wissensspeicher in der Kette aufgerufen werden, sofern es einen gibt!
    /// </remarks>
    /// <param name="wurzel">Wurzel des zu speichernden Baumes.</param>
    public void SpeichereBaum(IEntscheidungsbaumWurzel wurzel);

  }
}

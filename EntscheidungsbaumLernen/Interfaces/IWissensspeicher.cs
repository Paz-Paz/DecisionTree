using System;

namespace EntscheidungsbaumLernen.Interfaces
{
  /// <summary>
  /// Inteface für die externe Darstellung des Wissensspeichers.
  /// </summary>
  /// <remarks>
  /// <br /><b>Versionen:</b><br />
  /// V1.0 06.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  public interface IWissensspeicher
  {
    /// <summary>
    /// Lädet den gespeicherten Baum.
    /// </summary>
    /// <remarks>
    /// Dabei werden, die hinzugefügten einzelnen Wissensspeicher (z.B: "RAM", "Datei", ...) in der Reihenfolge des Hinzufüens ausgelesen bis das erste Ergebnis geliefert wird.
    /// </remarks>
    /// <returns>Den gefundenen Baum. (Bzw. dessen Wurzel <see cref="IEntscheidungsbaumWurzel"/>.</returns>
    /// <exception cref="InvalidCastException">Wenn nichts gespeichertes gefunden weden konnte.</exception>
    public IEntscheidungsbaumWurzel LadeBaum();


  }
}

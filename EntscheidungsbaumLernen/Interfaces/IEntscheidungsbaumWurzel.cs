using System;

namespace EntscheidungsbaumLernen.Interfaces
{
  /// <summary>
  /// Interface für die Darstellung des Entscheidungsbaums nach außen.
  /// </summary>
  /// <remarks>
  /// <br /><b>Versionen:</b><br />
  /// V1.0 06.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  public interface IEntscheidungsbaumWurzel
  {
    /// <summary>
    /// Typ des aktuellen Knotens.
    /// </summary>
    public Type KnotenTyp { get; }

    /// <summary>
    /// Liefert das zur <paramref name="kategorie"/> passende Kind.
    /// </summary>
    /// <param name="kategorie">Kategorie die zurückgegeben werden soll.</param>
    /// <returns>Das übergebene Kind, wenn es zur Kategorie eines gibt.</returns>
    /// <exception cref="ArgumentException">Wenn die übergebene Kategorie nicht existiert.</exception>
    public object GetKind(in string kategorie);

    /// <summary>
    /// Liefert ob das Kind zur übergebenen Kategorie ein Blatt ist und das Ergebniss enthält, oder wieder eine <see cref="IEntscheidungsbaumWurzel"/> ist.
    /// </summary>
    /// <param name="kategorie">Kategorie, die geprüft werden soll.</param>
    /// <returns><i>True</i> wenn das Kind zur <paramref name="kategorie"/> ein Blatt ist, ansonsten <i>false</i>.</returns>
    /// <exception cref="ArgumentException">Wenn die übergebene Kategorie nicht existiert.</exception>
    public bool IstBlatt(in string kategorie);
  }
}

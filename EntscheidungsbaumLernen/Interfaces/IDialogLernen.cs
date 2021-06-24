using System;
using System.Collections.Generic;

namespace EntscheidungsbaumLernen.Interfaces
{
  #region INTERFACE IDialogLernen<TBsp, TResult> .........................................................................

  /// <summary>
  /// Interface um "Anwendern" eine einfache Schnittstelle für die Nutzung des Entscheidungsbaumes anzubieten.
  /// </summary>
  /// <remarks>
  /// <br /><b>Versionen:</b><br />
  /// V1.0 14.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  /// <typeparam name="TBsp">Typ der Klasse, die für als Beispiele verwendet wird.</typeparam>
  /// <typeparam name="TResult">Typ der Eigenschaft in <typeparamref name="TResult"/> welche das Ergebnis darstellt.</typeparam>
  public interface IDialogLernen<TBsp, TResult> where TBsp : class where TResult : Enum
  {
    #region Getter/Setter ..................................................................................................

    /// <summary>
    /// Wert, welcher verwendet wird, wenn einem Blatt im Entscheidungsbaum kein Wert zugeordnet werden kann. (Default: '<i>default(TResult)</i>')
    /// </summary>
    /// <remarks>
    /// <i>Beispiel</i>: Ein Element hat 2 Kinder, das gewählte Attribut aber 3 Eigenschaften.
    /// </remarks>
    public TResult DefaultWert { get; set; }


    #endregion .............................................................................................................
    #region Oeffentliche Methoden ..........................................................................................

    /// <summary>
    /// Gibt den aktuell gelernten Baum auf der Konsole aus.
    /// </summary>
    /// <remarks>
    /// Achtung, falls noch keine Lern-Funktion aufgerufen wurde, wird eine Fehlermeldung ausgegeben.
    /// </remarks>
    /// <exception cref="InvalidCastException"></exception>
    public void AusgabeBaumstruktur();

    /// <summary>
    /// Gibt die Liste der zu lernenden Beispiele auf der Konsole aus.
    /// </summary>
    /// <remarks>
    /// Dabei wird die Breite des Konsolenfensters, bei Bedarf, angepasst.
    /// </remarks>
    public void AusgabeLerndaten();

    /// <summary>
    /// Fügt eine Liste an Beispielen zum Lernen hinzu.
    /// </summary>
    /// <param name="beispiele">Liste der Beispiele die hinzugefügt werden sollen.</param>
    public void BeispielHinzufuegen(in IEnumerable<TBsp> beispiele);

    /// <summary>
    /// Fügt ein Beispiel zum Lernen hinzu.
    /// </summary>
    /// <param name="beispiel">Beispiel das hinzugefügt werden soll.</param>
    public void BeispielHinzufuegen(in TBsp beispiel);

    /// <summary>
    /// Erstellt eine Liste aller Attribute aus <typeparamref name="TBsp"/> die zum 'anlernen' verwendet werden können.
    /// </summary>
    /// <returns></returns>
    public List<Type> ErstelleListeAllerAttribute();

    /// <inheritdoc cref="LerneBeispiele(in List{Type}, in TResult)"/>
    public void LerneBeispiele();

    /// <inheritdoc cref="LerneBeispiele(in List{Type}, in TResult)"/>
    public void LerneBeispiele(in List<Type> attributsliste);

    /// <inheritdoc cref="LerneBeispiele(in List{Type}, in TResult)"/>
    public void LerneBeispiele(in TResult defaultWert);

    /// <summary>
    /// Wandelt die Liste der Beispiele in einen Entscheidungsbaum um.
    /// </summary>
    /// <param name="attributsliste">Liste der Attribute, die zur Bestimmung der Knoten verwendet werdern soll.</param>
    /// <param name="defaultWert">Wert welcher verwendet wird, wenn ein Blatt "leer" bleiben sollte.</param>
    public void LerneBeispiele(in List<Type> attributsliste, in TResult defaultWert);

    #endregion .............................................................................................................
  }

  #endregion .............................................................................................................
}

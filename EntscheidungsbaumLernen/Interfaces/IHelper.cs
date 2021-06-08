using System;
using System.Collections.Generic;

namespace EntscheidungsbaumLernen.Interfaces
{
  /// <summary>
  /// Inteface um alle Helper-Funktionen nach extern mappen zu können.
  /// </summary>
  /// <remarks>
  /// <br /><b>Versionen:</b><br />
  /// V1.0 06.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  public interface IHelper
  {
    /// <summary>
    /// Liefert eine Liste aller <see cref="Enum"/>-Attribute der Klasse <typeparamref name="TBsp"/>. Dabei wird die Eigenschaft <typeparamref name="TResult"/> ignoriert.
    /// </summary>
    /// <typeparam name="TBsp">Klasse, deren Attribute aufgelistet werden sollen.</typeparam>
    /// <typeparam name="TResult">Eigenschaft, welche beim Auflisten ignoriert werden soll, da sie dem Ergebnis entspricht.</typeparam>
    /// <returns>Liste an Typen, die die Klasse <typeparamref name="TBsp"/> als Eigenschaften besitzt.</returns>
    public List<Type> ErstellAttributliste<TBsp, TResult>() where TBsp : class where TResult : Enum;

    /// <summary>
    /// Schreibt die Liste der übergebenen Attribute auf die Konsole.
    /// </summary>
    /// <remarks>
    /// <b>Beispiel:</b><br />
    /// <example>
    /// <code>
    /// Attributliste:<br />
    /// *) Attraktiv<br />
    /// *) Preis<br />
    /// *) Loge<br />
    /// *) Wetter<br />
    /// *) ...
    ///  </code>
    /// </example>
    /// </remarks>
    /// <param name="liste">Liste der auszugebenen Attribute.</param>
    public void GibAttributlisteAufKonsoleAus(in List<Type> liste);

    /// <summary>
    /// Gibt den Baum auf der Konsole aus.
    /// </summary>
    /// <remarks>
    /// <b>Beispiel:</b><br />
    /// <example>
    /// <code>
    /// Baumstruktur:<br />
    /// Gruppe<br />
    ///  ├ Freunde: Kategorie<br />
    ///  │  ├ AC: Ja<br />
    ///  │  ├ KO: Attraktiv<br />
    /// .....
    ///  </code>
    /// </example>
    /// </remarks>
    /// <param name="wurzel">Wurzelknoten des Entscheidungsbaumes.</param>
    public void GibBaumAus(in IEntscheidungsbaumWurzel wurzel);

    /// <summary>
    /// Gibt die übergebene Beispielliste als Tabelle auf der Konsole aus.
    /// </summary>
    /// <typeparam name="TBsp">Klasse der Beispiel-Elemente.</typeparam>
    /// <typeparam name="TResult">Eigenschaft des Ergebnisses, um es ganz rechts darzustellen.</typeparam>
    /// <param name="beispielliste">Liste der auszugebenden Beispiele.</param>
    public void GibBeispiellisteAufKonsoleAus<TBsp, TResult>(in List<TBsp> beispielliste) where TBsp : class where TResult : Enum;
  }
}

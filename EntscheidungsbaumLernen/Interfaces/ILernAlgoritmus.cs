using System;
using System.Collections.Generic;

namespace EntscheidungsbaumLernen.Interfaces
{
  /// <summary>
  /// Interface für den Algorithmus zum Lernen des Entscheidungsbaumes.
  /// </summary>
  /// <typeparam name="TBsp">Typ der Beispiel-Klasse.</typeparam>
  /// <typeparam name="TResult">Typ der Eigenschaft in der <typeparamref name="TBsp"/>, der das Ergebnis darstellt.</typeparam>
  /// <remarks>
  /// <br /><b>Versionen:</b><br />
  /// V1.0 06.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  public interface ILernAlgoritmus<TBsp, TResult> where TBsp : class where TResult : Enum
  {
    /// <inheritdoc cref="Lerne(in List{TBsp}, in List{Type}, in TResult)"/>
    /// <remarks>
    /// Es werden alle Attribute von <typeparamref name="TBsp"/> verwendet die <see cref="Enum"/>s sind. Außer <typeparamref name="TResult"/>.
    /// </remarks>
    public IWissensspeicher Lerne(in List<TBsp> beispielliste);

    /// <inheritdoc cref="Lerne(in List{TBsp}, in List{Type}, in TResult)"/>
    public IWissensspeicher Lerne(in List<TBsp> beispielliste, in List<Type> attributsliste);

    /// <summary>
    /// Erzeugt aus der Beispielliste einen Entscheidungsbaum, welcher im <see cref="IWissensspeicher"/> abgelegt wird.
    /// </summary>
    /// <param name="beispielliste">Liste an Beispielen, von denen gelernt werden soll.</param>
    /// <param name="attributsliste">List der Attribute aus <typeparamref name="TBsp"/> die zum Lernen verwendet werden soll.</param>
    /// <param name="defaultWert">Wert welcher verwendet wird, wenn ein Ergebnis nicht eindeutig zugewiesen werden kann.</param>
    /// <returns>Einen <see cref="IWissensspeicher"/> in dem der erlernte Baum gespeichert ist.</returns>
    public IWissensspeicher Lerne(in List<TBsp> beispielliste, in List<Type> attributsliste, in TResult defaultWert);
  }
}

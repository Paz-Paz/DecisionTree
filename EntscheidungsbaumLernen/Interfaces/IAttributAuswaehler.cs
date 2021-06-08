using System;
using System.Collections.Generic;

namespace EntscheidungsbaumLernen.Interfaces
{
  /// <summary>
  /// Interface für die Klasse, welche für das Auswählen des nächsten Attributs zuständig ist.
  /// </summary>
  /// <typeparam name="TBsp">Typ der Objekte, welche in der Beispielliste enthalten sind.</typeparam>
  /// <typeparam name="TResult">Typ der Eigenschaft von <typeparamref name="TBsp"/>, die als Ergebniss verwendet wird.</typeparam>
  /// <remarks>
  /// <br /><b>Versionen:</b><br />
  /// V1.0 06.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  internal interface IAttributAuswaehler<TBsp, TResult> where TBsp : class where TResult : Enum
  {
    /// <summary>
    /// Wählt anhand der Beispielliste und der Attributliste ein (hoffentlich) optimales Attribut aus.
    /// </summary>
    /// <param name="beispielliste">Liste an Beispielen.</param>
    /// <param name="attributsliste">Liste an Attributen, aus denen ausgwählt werden kann.</param>
    /// <returns>Den <see cref="Type"/> des ausgewählten Attributs.</returns>
    public Type ChoosAttribute(in List<TBsp> beispielliste, in List<Type> attributsliste);
  }
}

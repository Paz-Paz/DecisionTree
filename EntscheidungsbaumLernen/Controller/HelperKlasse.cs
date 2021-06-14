using EntscheidungsbaumLernen.Helper;
using EntscheidungsbaumLernen.Interfaces;
using System;
using System.Collections.Generic;

namespace EntscheidungsbaumLernen.Controller
{
  /// <summary>
  /// Klasse um alle Hilffunktionen zentral nach außen geben zu können.
  /// </summary>
  /// <remarks>
  /// <br /><b>Versionen:</b><br />
  /// V1.0 06.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  internal class HelperKlasse : IHelper
  {
    /// <inheritdoc/>
    public List<Type> ErstellAttributliste<TBsp, TResult>() where TBsp : class where TResult : Enum => KlassenHelper.ErstellAttributliste<TBsp, TResult>();

    /// <inheritdoc/>
    public void GibAttributlisteAufKonsoleAus(in List<Type> liste) => Ausgeber.GibAttributlisteAufKonsoleAus(liste);

    /// <inheritdoc/>
    public void GibBaumAus(in IEntscheidungsbaumWurzel wurzel) => Ausgeber.GibBaumAus(wurzel);

    /// <inheritdoc/>
    public void GibBeispiellisteAufKonsoleAus<TBsp, TResult>(in List<TBsp> beispielliste) where TBsp : class where TResult : Enum => Ausgeber.GibBeispiellisteAufKonsoleAus<TBsp, TResult>(beispielliste);
  }
}

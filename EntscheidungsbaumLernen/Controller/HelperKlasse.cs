using EntscheidungsbaumLernen.Helper;
using EntscheidungsbaumLernen.Interfaces;
using System;
using System.Collections.Generic;

namespace EntscheidungsbaumLernen.Controller
{
  internal class HelperKlasse : IHelper
  {
    /// <inheritdoc/>
    public List<Type> ErstellAttributliste<TBsp, TResult>() where TBsp : class where TResult : Enum => KlassenHelper.ErstellAttributliste<TBsp, TResult>();

    /// <inheritdoc/>
    public void GibAttributlisteAufKonsoleAus(in List<Type> liste) => Ausgeber.GibAttributlisteAufKonsoleAus(liste);

    /// <inheritdoc/>
    public void GibBaumAus(in IEntscheidungsbaumWurzel wurzel) => Ausgeber.GibBaumAus(wurzel);

    /// <inheritdoc/>
    public void GibBeispiellisteAufKonsoleAus<TBsp, TResult>(in List<TBsp> beispielliste) where TBsp : class where TResult : Enum => Ausgeber.GibLerndatenlisteAufKonsoleAus<TBsp, TResult>(beispielliste);
  }
}

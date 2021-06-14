using System;
using System.Collections.Generic;

namespace EntscheidungsbaumLernen.Interfaces
{
  #region INTERFACE IDialogLernen<TBsp, TResult> .........................................................................

  public interface IDialogLernen<TBsp, TResult> where TBsp : class where TResult : Enum
  {
    #region Oeffentliche Methoden ..........................................................................................

    /// <summary>
    /// Gibt den aktuell gelernten Baum auf der Konsole aus.
    /// </summary>
    /// <remarks>
    /// Achtung, falls noch keines der <see cref="LerneBeispiele"/> aufgerufen wurde, wird versucht einen vorherigen Baum zu laden!
    /// </remarks>
    /// <exception cref="InvalidCastException"></exception>
    public void AusgabeBaumstruktur();

    public void AusgabeLerndaten();

    public void BeispielHinzufuegen(in IEnumerable<TBsp> beispiele);

    public void BeispielHinzufuegen(in TBsp beispiel);

    public List<Type> ErstelleListeAllerAttribute();

    public void LerneBeispiele();


    public void LerneBeispiele(in List<Type> attributsliste);

    public void LerneBeispiele(in TResult defaultWert);


    public void LerneBeispiele(in List<Type> attributsliste, in TResult defaultWert);

    #endregion .............................................................................................................
    #region Paket-Interne Methoden .........................................................................................
    #endregion .............................................................................................................
  }

  #endregion .............................................................................................................
}

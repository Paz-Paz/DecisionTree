using EntscheidungsbaumLernen.Interfaces;
using System;

namespace EntscheidungsbaumLernen.Controller
{
  #region CLASS WissensspeicherRam .......................................................................................

  /// <summary>
  /// Speichert das Wissen nur im RAM, so dass es bei jedem Programmneustart neu generiert werden muss.
  /// </summary>
  internal class WissensspeicherRam : IWissensspeicherImpl
  {
    #region Eigenschaften ..................................................................................................

    private IEntscheidungsbaumWurzel _baumwurzel = null;

    private IWissensspeicherImpl _next = null;

    #endregion .............................................................................................................
    #region Oeffentliche Methoden ..........................................................................................

    /// <inheritdoc/>
    public IEntscheidungsbaumWurzel LadeBaum()
    {
      if (this._baumwurzel == null)
      {
        if (this._next == null)
        {
          throw new InvalidCastException("Noch kein Baum zum Laden gespeichert.");
        }
        else
        {
          this._baumwurzel = this._next.LadeBaum();
        }
      }
      return this._baumwurzel;
    }

    /// <inheritdoc/>
    public void SetNaechsteInstanz(IWissensspeicherImpl wissensspeicher)
    {
      if (this._next != null)
      {
        throw new InvalidCastException("Es wurde schon ein Wissensspeicher hinzugefügt.");
      }
      this._next = wissensspeicher;
    }

    /// <inheritdoc/>
    public void SpeichereBaum(IEntscheidungsbaumWurzel wurzel)
    {
      this._baumwurzel = wurzel;
      this._next?.SpeichereBaum(wurzel);
    }

    #endregion .............................................................................................................
  }

  #endregion .............................................................................................................
}

using EntscheidungsbaumLernen.Interfaces;
using System;

namespace EntscheidungsbaumLernen.Controller
{
  #region CLASS WissensspeicherRam .......................................................................................

  /// <summary>
  /// Speichert das Wissen nur im RAM, so dass es bei jedem Programmneustart neu generiert werden muss.
  /// </summary>
  /// <remarks>
  /// <br /><b>Versionen:</b><br />
  /// V1.0 06.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  internal class WissensspeicherRam : IWissensspeicherImpl
  {
    #region Eigenschaften ..................................................................................................

    /// <summary>
    /// Objekt, in dem die Wurzel des Entscheidungsbaumes gespeichert wird.
    /// </summary>
    private IEntscheidungsbaumWurzel _baumwurzel = null;

    /// <summary>
    /// Nächser Speicher in der Kette.
    /// </summary>
    private IWissensspeicherImpl _next = null;

    #endregion .............................................................................................................
    #region Getter/Setter ..................................................................................................

    /// <inheritdoc/>
    public IWissensspeicherImpl Next => this._next;

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
          Console.WriteLine("Im RAM war nichts gespeichert, nächste Instanz wird aufgerufen...");
          this._baumwurzel = this._next.LadeBaum();
        }
      }
      Console.WriteLine("Lade Baum aus RAM...");
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
      Console.WriteLine("Speichere Baum in RAM...");
      this._baumwurzel = wurzel;
      this._next?.SpeichereBaum(wurzel);
    }

    #endregion .............................................................................................................
  }

  #endregion .............................................................................................................
}

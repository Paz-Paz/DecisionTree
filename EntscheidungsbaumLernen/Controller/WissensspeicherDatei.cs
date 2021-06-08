using EntscheidungsbaumLernen.Interfaces;
using System;

namespace EntscheidungsbaumLernen.Controller
{
  internal class WissensspeicherDatei : IWissensspeicherImpl
  {
    #region Eigenschaften ..................................................................................................


    private IWissensspeicherImpl _next = null;

    private enum DUMMY { }

    private readonly string _datei;
    #endregion .............................................................................................................
    #region Konstruktor ....................................................................................................

    public WissensspeicherDatei(string datei)
    {
      this._datei = datei;
    }

    #endregion .............................................................................................................
    #region Getter/Setter ..................................................................................................

    /// <inheritdoc/>
    IWissensspeicherImpl IWissensspeicherImpl.Next => this._next;

    #endregion .............................................................................................................
    #region Oeffentliche Methoden ..........................................................................................

    /// <inheritdoc/>
    public IEntscheidungsbaumWurzel LadeBaum()
    {
      // TODO: Lade aus Datei fertig implementieren...

      if (this._next == null)
      {
        throw new InvalidCastException("Noch kein Baum zum Laden gespeichert.");
      }
      else
      {
        Console.WriteLine($"Datei '{this._datei}' konnte nicht gelesen werden, nächste Instanz wird aufgerufen...");
        return this._next.LadeBaum();
      }

      //JsonSerializer.Deserialize<IEntscheidungsbaumWurzel>()

      //Console.WriteLine($"Lade Baum aus Datei '{this._datei}'...");
      //throw new NotImplementedException();
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
      // TODO: Speichere in Datei fertig implementieren...

      // Erzeugt immer "object cycle", warum auch immer...
      //string json = JsonSerializer.Serialize(wurzel, new JsonSerializerOptions() { WriteIndented = true, MaxDepth = 100 ,IgnoreNullValues = true});
      //Console.WriteLine(json);
      Console.WriteLine($"Speichere Baum in Datei '{this._datei}'...");
      this._next?.SpeichereBaum(wurzel);
      //throw new NotImplementedException();
    }

    #endregion .............................................................................................................
  }

}

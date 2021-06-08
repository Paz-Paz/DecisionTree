using EntscheidungsbaumLernen.Interfaces;
using System;

namespace EntscheidungsbaumLernen.Controller
{
  internal class WissensspeicherDatei : IWissensspeicherImpl
  {
    #region Eigenschaften ..................................................................................................

    private IEntscheidungsbaumWurzel _baumwurzel = null;

    private IWissensspeicherImpl _next = null;

    #endregion .............................................................................................................
    #region Konstruktor ....................................................................................................

    public WissensspeicherDatei(string datei)
    {

    }

    #endregion .............................................................................................................
    #region Oeffentliche Methoden ..........................................................................................

    /// <inheritdoc/>
    public IEntscheidungsbaumWurzel LadeBaum()
    {
      //JsonSerializer.Deserialize<IEntscheidungsbaumWurzel>()
      throw new NotImplementedException();
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
      // Erzeugt immer "object cycle", warum auch immer...
      //string json = JsonSerializer.Serialize(wurzel, new JsonSerializerOptions() { WriteIndented = true, MaxDepth = 100 ,IgnoreNullValues = true});
      //Console.WriteLine(json);
      throw new NotImplementedException();
    }

    #endregion .............................................................................................................
  }

}

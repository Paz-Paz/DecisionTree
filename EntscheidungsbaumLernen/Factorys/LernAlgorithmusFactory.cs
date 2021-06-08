using EntscheidungsbaumLernen.Controller;
using EntscheidungsbaumLernen.Interfaces;
using System;

namespace EntscheidungsbaumLernen.Factorys
{
  public class LernAlgorithmusFactory<TBsp, TResult> where TBsp : class where TResult : Enum
  {

    private IAttributAuswaehler<TBsp, TResult> _attributAuswaehler = null;
    private WissensspeicherFactory _wissensspeicherFactory = new WissensspeicherFactory();

    public ILernAlgoritmus<TBsp, TResult> Build()
    {
      IWissensspeicherImpl wissensspeicher = this._wissensspeicherFactory.BuildImpl();
      IAttributAuswaehler<TBsp, TResult> attributAuswaehler = this._attributAuswaehler ?? new AttrAuswaehlerGainAbsolut<TBsp, TResult>();
      ILernAlgoritmus<TBsp, TResult> lernAlgoritmus = new LernAlgorithmusDT<TBsp, TResult>(attributAuswaehler, wissensspeicher);
      return lernAlgoritmus;
    }

    public LernAlgorithmusFactory<TBsp, TResult> AttributauswErstesAttribut()
    {
      if (this._attributAuswaehler != null)
      {
        Console.WriteLine($"WARNUNG - Attributauswähler '{this._attributAuswaehler.GetType().Name}' wird von '{typeof(AttrAuswaehlerErstesAttribut<TBsp, TResult>).Name}' überschrieben!");
      }
      this._attributAuswaehler = new AttrAuswaehlerErstesAttribut<TBsp, TResult>();
      return this;
    }

    public LernAlgorithmusFactory<TBsp, TResult> AttributauswGainAbsolut()
    {
      if (this._attributAuswaehler != null)
      {
        Console.WriteLine($"WARNUNG - Attributauswähler '{this._attributAuswaehler.GetType().Name}' wird von '{typeof(AttrAuswaehlerGainAbsolut<TBsp, TResult>).Name}' überschrieben!");
      }
      this._attributAuswaehler = new AttrAuswaehlerGainAbsolut<TBsp, TResult>();
      return this;
    }

    public LernAlgorithmusFactory<TBsp, TResult> SpeicherRam()
    {
      this._wissensspeicherFactory.AddRamSpeicher();
      return this;
    }

    public LernAlgorithmusFactory<TBsp, TResult> SpeicherDatei(in string pfad)
    {
      this._wissensspeicherFactory.AddDateiSpeicher(pfad);
      return this;
    }


  }
}

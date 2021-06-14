using EntscheidungsbaumLernen.Controller;
using EntscheidungsbaumLernen.Interfaces;
using System;

namespace EntscheidungsbaumLernen.Factorys
{
  public class DialogLernenFactory<TBsp, TResult> where TBsp : class where TResult : Enum
  {
    private readonly WissensspeicherFactory _wissensspeicherFactory = new WissensspeicherFactory();
    private IAttributAuswaehler<TBsp, TResult> _attributAuswaehler = null;


    public IDialogLernen<TBsp, TResult> Build()
    {
      IWissensspeicher wissensspeicher = this._wissensspeicherFactory.Build();
      ILernAlgoritmus<TBsp, TResult> lernAlgoritmus = new LernAlgorithmusDT<TBsp, TResult>(this._attributAuswaehler ?? new AttrAuswaehlerGainAbsolut<TBsp, TResult>());

      DialogLernen<TBsp, TResult> dialogLernen = new DialogLernen<TBsp, TResult>(lernAlgoritmus, wissensspeicher);
      return dialogLernen;

    }


    public DialogLernenFactory<TBsp, TResult> AddSpeicherDatei(in string dateiname)
    {
      this._wissensspeicherFactory.AddDateiSpeicher<TResult>(dateiname);
      return this;
    }

    public DialogLernenFactory<TBsp, TResult> AddSpeicherRam()
    {
      this._wissensspeicherFactory.AddRamSpeicher();
      return this;
    }

    public DialogLernenFactory<TBsp, TResult> ClearSpeicher()
    {
      this._wissensspeicherFactory.Clear();
      return this;
    }

    public DialogLernenFactory<TBsp, TResult> AttributauswaehlerErstesAttribut()
    {
      this._attributAuswaehler = new AttrAuswaehlerErstesAttribut<TBsp, TResult>();
      return this;
    }

    public DialogLernenFactory<TBsp, TResult> AttributauswaehlerGainAbsolut()
    {
      this._attributAuswaehler = new AttrAuswaehlerGainAbsolut<TBsp, TResult>();
      return this;
    }
  }
}

using EntscheidungsbaumLernen.Controller;
using EntscheidungsbaumLernen.Interfaces;
using System;

namespace EntscheidungsbaumLernen.Factorys
{
  public class DialogAnwendenFactory<TEingabe, TResult> where TEingabe : class where TResult : Enum
  {

    private readonly WissensspeicherFactory _wissensspeicherFactory = new WissensspeicherFactory();


    public IDialogAnwenden<TEingabe, TResult> Build()
    {
      return new DialogAnwenden<TEingabe, TResult>(this._wissensspeicherFactory.Build());
    }
    public DialogAnwendenFactory<TEingabe, TResult> AddSpeicherDatei(in string dateiname)
    {
      this._wissensspeicherFactory.AddDateiSpeicher<TResult>(dateiname);
      return this;
    }

    public DialogAnwendenFactory<TEingabe, TResult> AddSpeicherRam()
    {
      this._wissensspeicherFactory.AddRamSpeicher();
      return this;
    }

    public DialogAnwendenFactory<TEingabe, TResult> ClearSpeicher()
    {
      this._wissensspeicherFactory.Clear();
      return this;
    }
  }
}

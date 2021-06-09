using EntscheidungsbaumLernen.Controller;
using EntscheidungsbaumLernen.Interfaces;
using System;

namespace EntscheidungsbaumLernen.Factorys
{
  public class WissensspeicherFactory
  {
    internal IWissensspeicherImpl _response = null;

    public IWissensspeicher Build()
    {
      return this.BuildImpl();
    }

    public WissensspeicherFactory AddRamSpeicher()
    {
      this.AddSpeicher(new WissensspeicherRam());
      return this;
    }

    public WissensspeicherFactory AddDateiSpeicher<TResult>(in string dateipfad) where TResult : Enum
    {
      this.AddSpeicher(new WissensspeicherDatei<TResult>(dateipfad));
      return this;
    }


    private void AddSpeicher(IWissensspeicherImpl wissensspeicher)
    {
      if (this._response == null)
      {
        this._response = wissensspeicher;
        return;
      }

      IWissensspeicherImpl letzterSpeicher = this._response;
      while (letzterSpeicher.Next != null)
      {
        letzterSpeicher = letzterSpeicher.Next;
      }
      letzterSpeicher.SetNaechsteInstanz(wissensspeicher);
    }

    internal IWissensspeicherImpl BuildImpl()
    {
      if (this._response == null)
      {
        return new WissensspeicherRam();
      }
      else
      {
        return this._response;
      }
    }

  }
}

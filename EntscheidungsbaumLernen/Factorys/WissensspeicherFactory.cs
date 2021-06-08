using EntscheidungsbaumLernen.Controller;
using EntscheidungsbaumLernen.Interfaces;

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

    public WissensspeicherFactory AddDateiSpeicher(in string dateipfad)
    {
      this.AddSpeicher(new WissensspeicherDatei(dateipfad));
      return this;
    }


    private void AddSpeicher(IWissensspeicherImpl wissensspeicher)
    {
      if (this._response == null)
      {
        this._response = wissensspeicher;
      }
      else
      {
        this._response.SetNaechsteInstanz(wissensspeicher);
      }
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

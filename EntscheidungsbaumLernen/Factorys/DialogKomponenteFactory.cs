using EntscheidungsbaumLernen.Controller;
using EntscheidungsbaumLernen.Interfaces;

namespace EntscheidungsbaumLernen.Factorys
{
  public class DialogKomponenteFactory
  {
    public IDialogKomponente Build()
    {
      return new DialogKomponente();
    }
  }
}

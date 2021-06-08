using EntscheidungsbaumLernen.Controller;
using EntscheidungsbaumLernen.Interfaces;

namespace EntscheidungsbaumLernen.Factorys
{
  public class HelperFactory
  {
    public IHelper Build()
    {
      return new HelperKlasse();
    }
  }
}

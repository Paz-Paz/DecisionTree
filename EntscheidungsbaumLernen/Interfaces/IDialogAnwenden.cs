using System;

namespace EntscheidungsbaumLernen.Interfaces
{
  public interface IDialogAnwenden<TEingabe, TResult> where TEingabe : class where TResult : Enum
  {

    public TResult Abfragen();

    public TResult EingabeAuswerten(in TEingabe eingabe);
  }
}

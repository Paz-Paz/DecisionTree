using EntscheidungsbaumLernen.Helper;
using EntscheidungsbaumLernen.Interfaces;
using System;
using System.Reflection;

namespace EntscheidungsbaumLernen.Controller
{
  internal class DialogKomponente : IDialogKomponente
  {
    public TResult Abfragen<TEingabe, TResult>(in IEntscheidungsbaumWurzel entscheidungsbaumWurzel, in TEingabe auszulesen) where TEingabe : class where TResult : Enum
    {
      Console.Write("\nAuszuwerten: ");
      bool first = true;
      foreach (PropertyInfo propertyInfo in auszulesen.GetType().GetProperties())
      {
        if (!propertyInfo.PropertyType.IsEnum)
        {
          continue;
        }
        if (!first)
        {
          Console.Write(", ");
        }
        first = false;
        Console.Write($"{propertyInfo.Name}: {KlassenHelper.GetValue(auszulesen, propertyInfo.PropertyType)}");
      }
      Console.WriteLine("\n");
      return this.Auswerten<TEingabe, TResult>(entscheidungsbaumWurzel, auszulesen, 1);
    }


    private TResult Auswerten<TEingabe, TResult>(in IEntscheidungsbaumWurzel entscheidungsbaumWurzel, in TEingabe auszulesen, in int zaehler) where TEingabe : class where TResult : Enum
    {
      Type attribut = entscheidungsbaumWurzel.Wureltyp;
      object wert = KlassenHelper.GetValue(auszulesen, attribut);

      string kategorie = wert.ToString();
      object kind = entscheidungsbaumWurzel.GetKind(kategorie);

      Console.WriteLine($"{zaehler,3}. Auswahl: {attribut.Name}: {wert}");

      if (entscheidungsbaumWurzel.IstBlatt(kategorie))
      {
        return (TResult)kind;
      }

      return this.Auswerten<TEingabe, TResult>((IEntscheidungsbaumWurzel)kind, auszulesen, zaehler + 1);
    }
  }
}

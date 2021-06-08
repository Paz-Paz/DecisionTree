using EntscheidungsbaumLernen.Helper;
using EntscheidungsbaumLernen.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntscheidungsbaumLernen.Controller
{
  #region CLASS AttrAuswaehlerGainAbsolut<T> .............................................................................

  internal class AttrAuswaehlerGainAbsolut<TBsp, TResult> : IAttributAuswaehler<TBsp, TResult> where TBsp : class where TResult : Enum
  {

    #region Eigenschaften ..................................................................................................

    private readonly int _anzDezimalstellen;

    #endregion .............................................................................................................
    #region Konstruktor ....................................................................................................

    public AttrAuswaehlerGainAbsolut()
      : this(4)
    {
      /* nothing */
    }

    public AttrAuswaehlerGainAbsolut(int anzahlDezimalstellenAusgabe)
    {
      Console.WriteLine("\nVerwendeter Attributsauswahlalgorithmus: Gain Absolut");
      this._anzDezimalstellen = anzahlDezimalstellenAusgabe;
    }

    #endregion .............................................................................................................
    #region Oeffentliche Methoden ..........................................................................................

    /// <inheritdoc/>
    public Type ChoosAttribute(in List<TBsp> beispielliste, in List<Type> attributsliste)
    {
      Console.WriteLine($"\nBeispiele: {beispielliste.Count}, Attribute: {attributsliste.Count}:");

      Array elemente = Enum.GetValues(typeof(TResult));
      int[] zaehler = new int[elemente.Length];
      for (int i = 0; i < zaehler.Length; i++)
      {
        zaehler[i] = 0;
      }

      foreach (TBsp beispiel in beispielliste ?? Enumerable.Empty<TBsp>())
      {
        object wert = KlassenHelper.GetValue(beispiel, typeof(TResult));
        for (int i = 0; i < elemente.Length; i++)
        {
          if (elemente.GetValue(i).ToString() == wert.ToString())
          {
            zaehler[i]++;
          }
        }
      }

      double gain = 0;
      double anzahlBeispiele = beispielliste.Count;
      foreach (int anzahl in zaehler)
      {
        gain += this.BerechneWahscheinlichkeit(anzahl / anzahlBeispiele);
      }

      Console.WriteLine($"- Gain: {this.Runde(gain)} Bits");

      Type gewinner = attributsliste.First();
      double gainMax = double.MinValue;

      // Laengsten Attributnamen ermitteln (schöne Ausgabe)
      int laenge = 0;
      foreach (Type attribut in attributsliste)
      {
        if (attribut.Name.Length > laenge)
        {
          laenge = attribut.Name.Length;
        }
      }

      foreach (Type attribut in attributsliste)
      {
        if (!attribut.IsEnum)
        {
          throw new ArgumentException($"'{attribut.GetType()}' ist kein Enum!");
        }

        double gainAktuell = gain - this.GewinnAttribut(beispielliste, attribut);
        string ausgabe = $"- Gain({attribut.Name})";
        Console.WriteLine($"{ausgabe.PadRight(9 + laenge, ' ')} ~{this.Runde(gainAktuell).ToString().PadRight(2 + this._anzDezimalstellen, ' ')} Bits");

        if (gainAktuell > gainMax)
        {
          gainMax = gainAktuell;
          gewinner = attribut;
        }
      }

      Console.WriteLine($"Auswahl: {gewinner.Name} ({this.Runde(gainMax)} Bits).");
      return gewinner;
    }

    #endregion .............................................................................................................
    #region Private Methoden ...............................................................................................

    private double GewinnAttribut(in List<TBsp> beispielliste, Type attribut)
    {
      Checks.EnumCheck(attribut);

      double result = 0;

      Array attribute = Enum.GetValues(typeof(TResult));

      foreach (object item in attribut.GetEnumValues())
      {
        int[] zaehler = new int[attribute.Length];
        double anzahlGefunden = 0;
        foreach (TBsp beispiel in beispielliste)
        {
          object prop = KlassenHelper.GetValue(beispiel, attribut);
          if ((dynamic)prop == (dynamic)item)
          {
            object wert = KlassenHelper.GetValue(beispiel, typeof(TResult));
            for (int i = 0; i < attribute.Length; i++)
            {
              if (wert.ToString() == attribute.GetValue(i).ToString())
              {
                zaehler[i]++;
                anzahlGefunden++;
                break;
              }
            }
          }
        }

        if (anzahlGefunden != 0)
        {
          double ergebnis = 0;
          foreach (double anzahl in zaehler)
          {
            ergebnis += this.BerechneWahscheinlichkeit(anzahl / anzahlGefunden);
          }
          result += ergebnis * anzahlGefunden / beispielliste.Count;
        }
      }
      return result;
    }

    private double BerechneWahscheinlichkeit(double wert)
    {
      if (wert == 0)
      {
        return 0;
      }
      return (-1) * (wert) * Math.Log2(wert);
    }


    private double Runde(in double zuRunden)
    {
      double ergebnis = Math.Round(zuRunden, this._anzDezimalstellen);
      if (ergebnis == -0)
      {
        ergebnis = 0;
      }
      return ergebnis;
    }

    #endregion .............................................................................................................
  }

  #endregion .............................................................................................................
}

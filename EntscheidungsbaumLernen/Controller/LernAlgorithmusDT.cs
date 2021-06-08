using EntscheidungsbaumLernen.Helper;
using EntscheidungsbaumLernen.Interfaces;
using EntscheidungsbaumLernen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EntscheidungsbaumLernen.Controller
{
  #region CLASS LernAlgorithmusDT ........................................................................................

  internal class LernAlgorithmusDT<TBsp, TResult> : ILernAlgoritmus<TBsp, TResult> where TBsp : class where TResult : Enum
  {
    #region Eigenschaften ..................................................................................................

    private readonly IAttributAuswaehler<TBsp, TResult> _attributAuswaehler;

    private readonly IWissensspeicherImpl _wissensspeicher;

    #endregion .............................................................................................................
    #region Konstruktor ....................................................................................................

    public LernAlgorithmusDT(IAttributAuswaehler<TBsp, TResult> attributAuswaehler, IWissensspeicherImpl wissensspeicher)
    {
      this._attributAuswaehler = attributAuswaehler;
      this._wissensspeicher = wissensspeicher;
    }

    #endregion .............................................................................................................
    #region Getter/Setter ..................................................................................................
    #endregion .............................................................................................................
    #region Oeffentliche Methoden ..........................................................................................

    /// <inheritdoc/>
    public IWissensspeicher Lerne(in List<TBsp> beispielliste)
    {
      List<Type> attributliste = KlassenHelper.ErstellAttributliste<TBsp, TResult>();
      return this.Lerne(beispielliste, attributliste);
    }

    /// <inheritdoc/>
    public IWissensspeicher Lerne(in List<TBsp> beispielliste, in List<Type> attributsliste)
    {
      PropertyInfo[] propertyInfos = typeof(TBsp).GetProperties();
      if (propertyInfos?.Length < 2)
      {
        Console.WriteLine($"WARNING Beispiele vom Typ '{typeof(TBsp)}' haben <2 Eigenschaften, daher keine automatische Ergebniss-Atribut-Auswahl möglich. Leerer Wissensspeicher wird zurückgegeben.");
      }

      Type defaulWertType = null;
      for (int i = 0; i < propertyInfos.Length; i++)
      {
        PropertyInfo propertyInfo = propertyInfos[i];
        if (!propertyInfo.CanRead)
        {
          continue;
        }
        if (propertyInfo.PropertyType.IsEnum)
        {
          defaulWertType = propertyInfo.PropertyType;
          break;
        }
      }
      if (defaulWertType == null)
      {
        Console.WriteLine($"WARNING Typ '{typeof(TBsp)}' hat keine Enums, da her keine automatische Ergebniss-Attribut-Auswahl möglich. Leerer Wissensspeicher wird zurückgegeben.");
        return this._wissensspeicher;
      }
      Array werte = Enum.GetValues(defaulWertType);
      if (werte?.Length == 0)
      {
        Console.WriteLine($"WARNING Enum '{defaulWertType.Name}' besitzt keine Werte, daher kann auch kein Wert automatishc gewählt werden. Leerer Wissensspeicher wired zurückgegeben.");
      }
      object wert = werte.GetValue(0);
      Console.WriteLine($"Automatische Ergebniss-Attribut-Auswahl: {defaulWertType.Name}.{wert}");
      return this.Lerne(beispielliste, attributsliste, (TResult)wert);

    }

    public IWissensspeicher Lerne(in List<TBsp> beispielliste, in List<Type> attributsliste, in TResult defaultWert)
    {
      object response = this.LernrePrivat(beispielliste, attributsliste, defaultWert);
      if (response.GetType() != typeof(EntscheidungsbaumElement<TResult>))
      {
        throw new Exception("Es ergibt sich kein Baum, nur 'Ja', oder 'Nein'");
      }
      this._wissensspeicher.SpeichereBaum((EntscheidungsbaumElement<TResult>)response);

      return this._wissensspeicher;
    }

    #endregion .............................................................................................................
    #region Private Methoden ...............................................................................................

    private object LernrePrivat(in List<TBsp> beispielliste, in List<Type> attributsliste, in TResult defaultWert)
    {
      if (beispielliste?.Count == 0)
      {
        return defaultWert;
      }

      if (this.PruefeObAlleElementeGleichesResultHaben(beispielliste, out object result))
      {
        return result;
      }


      if (attributsliste?.Count == 0)
      {
        return null; // FEHLER
      }

      Type aktuellesAttribut = this._attributAuswaehler.ChoosAttribute(beispielliste, attributsliste);
      EntscheidungsbaumElement<TResult> entscheidungsbaumElement = new EntscheidungsbaumElement<TResult>(aktuellesAttribut);
      Dictionary<string, List<TBsp>> listen = this.TeileInListen(aktuellesAttribut, beispielliste);

      List<Type> neueAttributliste = new List<Type>(attributsliste);
      neueAttributliste.Remove(aktuellesAttribut);

      foreach (string kategorie in listen.Keys)
      {
        var response = this.LernrePrivat(listen[kategorie], neueAttributliste, defaultWert);
        if (response == null)
        {
          Console.WriteLine("Entscheidungsbaum-Lernen wegen wiedersprüchlichen Beispielen abgebrochen.");
          return entscheidungsbaumElement;
        }
        if (response.GetType() == typeof(EntscheidungsbaumElement<TResult>))
        {
          entscheidungsbaumElement.SetKind(kategorie, (EntscheidungsbaumElement<TResult>)response);
        }
        if (response.GetType() == typeof(TResult))
        {
          entscheidungsbaumElement.SetKind(kategorie, (TResult)response);
        }

      }

      return entscheidungsbaumElement;
    }

    private bool PruefeObAlleElementeGleichesResultHaben(in List<TBsp> beispielliste, out object result)
    {
      // Default-Wert zuweisen:
      result = null;

      // Liste der Enum-Werte:
      Array elemente = Enum.GetValues(typeof(TResult));

      // Zaehler initiieren:
      int[] zaehler = new int[elemente.Length];
      for (int i = 0; i < zaehler.Length; i++)
      {
        zaehler[i] = 0;
      }

      // Alle Beispiele durchzählen:
      foreach (TBsp beispiel in beispielliste ?? Enumerable.Empty<TBsp>())
      {
        object wert = KlassenHelper.GetValue(beispiel, typeof(TResult));
        for (int i = 0; i < elemente.Length; i++)
        {
          if (elemente.GetValue(i).ToString() == wert.ToString())
          {
            zaehler[i]++;
            break;
          }
        }
      }

      // Zähler prüfen:
      for (int i = 0; i < zaehler.Length; i++)
      {
        if (zaehler[i] == beispielliste.Count)
        {
          result = elemente.GetValue(i);
          return true;
        }
      }

      // Es sind nicht alle Beispiele vom gleichen Result.
      return false;
    }

    private Dictionary<string, List<TBsp>> TeileInListen(Type attribut, List<TBsp> zuTeilen)
    {
      Dictionary<string, List<TBsp>> listen = new Dictionary<string, List<TBsp>>();

      Array attributArray = Enum.GetValues(attribut);
      foreach (object objekt in attributArray)
      {
        listen.Add(objekt.ToString(), new List<TBsp>());
      }


      foreach (TBsp kinobesuch in zuTeilen)
      {
        object eigenschaft = KlassenHelper.GetValue(kinobesuch, attribut);
        listen[eigenschaft.ToString()].Add(kinobesuch);
      }

      return listen;

    }

    #endregion .............................................................................................................
  }

  #endregion .............................................................................................................
}

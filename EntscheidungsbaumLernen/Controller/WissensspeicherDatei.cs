using EntscheidungsbaumLernen.Interfaces;
using EntscheidungsbaumLernen.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace EntscheidungsbaumLernen.Controller
{
  internal class WissensspeicherDatei<TResult> : IWissensspeicherImpl where TResult : Enum
  {
    #region Eigenschaften ..................................................................................................


    private IWissensspeicherImpl _next = null;

    private enum DUMMY { }

    private readonly string _datei;
    #endregion .............................................................................................................
    #region Konstruktor ....................................................................................................

    public WissensspeicherDatei(string datei)
    {
      this._datei = datei;
    }

    #endregion .............................................................................................................
    #region Getter/Setter ..................................................................................................

    /// <inheritdoc/>
    IWissensspeicherImpl IWissensspeicherImpl.Next => this._next;

    #endregion .............................................................................................................
    #region Getter/Setter ..................................................................................................

    /// <inheritdoc/>
    public IWissensspeicherImpl Next => this._next;

    #endregion .............................................................................................................
    #region Oeffentliche Methoden ..........................................................................................

    /// <inheritdoc/>
    public IEntscheidungsbaumWurzel LadeBaum()
    {

      string json = null;
      try
      {
        if (File.Exists(this._datei))
        {
          json = File.ReadAllText(this._datei);
        }
      }
      catch (Exception e)
      {
        Console.WriteLine($"Exception beim Speichern in Datei: {e.Message}");
      }

      if (string.IsNullOrWhiteSpace(json))
      {
        if (this._next == null)
        {
          throw new InvalidCastException("Noch kein Baum zum Laden gespeichert.");
        }
        Console.WriteLine($"Datei '{this._datei}' konnte nicht gelesen werden, nächste Instanz wird aufgerufen...");
        return this._next?.LadeBaum();
      }

      SpeicherKnoten baum = JsonSerializer.Deserialize<SpeicherKnoten>(json);
      IEntscheidungsbaumWurzel wurzel = this.ErzeugeIEntscheidungsbaumWurzel(baum);
      return wurzel;

    }

    /// <inheritdoc/>
    public void SetNaechsteInstanz(IWissensspeicherImpl wissensspeicher)
    {
      if (this._next != null)
      {
        throw new InvalidCastException("Es wurde schon ein Wissensspeicher hinzugefügt.");
      }
      this._next = wissensspeicher;
    }

    /// <inheritdoc/>
    public void SpeichereBaum(IEntscheidungsbaumWurzel wurzel)
    {
      SpeicherKnoten speicherStruktur = this.ErzeugeSpeicherbaum(wurzel);
      string json = JsonSerializer.Serialize(speicherStruktur, new JsonSerializerOptions() { WriteIndented = true, MaxDepth = 100, IgnoreNullValues = true });

      try
      {
        if (File.Exists(this._datei))
        {
          File.Delete(this._datei);
        }
        File.WriteAllText(this._datei, json);
      }
      catch (Exception e)
      {
        Console.WriteLine($"Exception beim Speichern in Datei: {e.Message}");
      }

      Console.WriteLine($"Speichere Baum in Datei '{this._datei}'...");
      this._next?.SpeichereBaum(wurzel);
      //throw new NotImplementedException();
    }

    #endregion .............................................................................................................
    #region Private Methoden ...............................................................................................

    private EntscheidungsbaumElement<TResult> ErzeugeIEntscheidungsbaumWurzel(in SpeicherKnoten knoten)
    {
      Type wurzeltype = Type.GetType(knoten.Wureltyp);
      EntscheidungsbaumElement<TResult> wurzel = new EntscheidungsbaumElement<TResult>(wurzeltype);

      Array ergebnisse = Enum.GetValues(typeof(TResult));

      foreach (string kategorie in knoten.Kinder.Keys)
      {
        //object kind = Enum.Parse(wurzeltype, kategorie);
        object kind = knoten.Kinder[kategorie];

        bool gefunden = false;
        foreach (object erg in ergebnisse)
        {
          if (kind.ToString() == erg.ToString())
          {
            wurzel.SetKind(kategorie, (TResult)Enum.Parse(typeof(TResult), kind.ToString()));
            gefunden = true;
            break;
          }
        }

        if (gefunden)
        {
          continue;
        }

        SpeicherKnoten kindKnoten = JsonSerializer.Deserialize<SpeicherKnoten>(kind.ToString());
        wurzel.SetKind(kategorie, this.ErzeugeIEntscheidungsbaumWurzel(kindKnoten));
      }

      return wurzel;
    }

    private SpeicherKnoten ErzeugeSpeicherbaum(IEntscheidungsbaumWurzel wurzel)
    {
      SpeicherKnoten knoten = new SpeicherKnoten();
      knoten.Wureltyp = wurzel.Wureltyp.AssemblyQualifiedName;


      Array kategorien = Enum.GetValues(wurzel.Wureltyp);
      foreach (object kategorie in kategorien)
      {
        string katString = kategorie.ToString();
        object kind = wurzel.GetKind(katString);
        if (wurzel.IstBlatt(katString))
        {
          knoten.Kinder.Add(katString, kind.ToString());
        }
        else
        {
          knoten.Kinder.Add(katString, this.ErzeugeSpeicherbaum((IEntscheidungsbaumWurzel)kind));
        }
      }

      return knoten;
    }

    #endregion .............................................................................................................
    #region CLASS SpeicherBaum .............................................................................................
    public class SpeicherKnoten
    {
      public Dictionary<string, object> Kinder { get; set; } = new Dictionary<string, object>();

      public string Wureltyp { get; set; }

    }
    #endregion .............................................................................................................

  }

}

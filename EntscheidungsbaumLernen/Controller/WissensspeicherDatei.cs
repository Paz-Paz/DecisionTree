using EntscheidungsbaumLernen.Interfaces;
using EntscheidungsbaumLernen.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace EntscheidungsbaumLernen.Controller
{
  /// <summary>
  /// Nutzt als Speicherort für den Baum eine Datei auf der Festplatte.
  /// </summary>
  /// <typeparam name="TResult">Typ des Ergebnisses</typeparam>
  /// <remarks>
  /// <br /><b>Versionen:</b><br />
  /// V1.0 06.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  internal class WissensspeicherDatei<TResult> : IWissensspeicherImpl where TResult : Enum
  {
    #region Eigenschaften ..................................................................................................

    /// <summary>
    /// Nächser Speicher in der Kette.
    /// </summary>
    private IWissensspeicherImpl _next = null;

    /// <summary>
    /// Dateiname unter dem gespeichert / geladen werden soll.
    /// </summary>
    private readonly string _datei;

    /// <summary>
    /// Flag ob die Json-Datei leserlich (true) oder minimiert (false) gespeichert werden soll.
    /// </summary>
    private readonly bool _speichereLeserlich;

    #endregion .............................................................................................................
    #region Konstruktor ....................................................................................................

    /// <summary>
    /// Liefert ein <see cref="WissensspeicherDatei{TResult}"/>-Objekt.
    /// </summary>
    /// <param name="datei">Dateiname der verwendet werden soll.</param>
    /// <param name="speichereLeserlich">Wenn true wird die gespeicherte JSON-Datei 'schön' gespeichert, bei false wird sie minimiert gespeichert.</param>
    public WissensspeicherDatei(string datei, bool speichereLeserlich)
    {
      this._datei = datei;
      this._speichereLeserlich = speichereLeserlich;
    }

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
      Console.WriteLine($"Lade Baum aus Datei '{this._datei}'");
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
      string json = JsonSerializer.Serialize(speicherStruktur, new JsonSerializerOptions() { WriteIndented = this._speichereLeserlich, MaxDepth = 100, IgnoreNullValues = true });

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
    }

    #endregion .............................................................................................................
    #region Private Methoden ...............................................................................................

    /// <summary>
    /// Erzeugt aus einem <paramref name="knoten"/> und dessen Kinder ein <see cref="EntscheidungsbaumElement{TResult}"/> und dessen Kinder.
    /// </summary>
    /// <remarks>
    /// Wird benötigt, da sich die <see cref="EntscheidungsbaumElement{TResult}"/>-Klassen nicht einfach so sreialisieren lassen.
    /// </remarks>
    /// <param name="knoten">Wurzel des aktuell umzuwandelnden (Teil-) Baumes.</param>
    /// <returns>Erzeugtes Objekt.</returns>
    private EntscheidungsbaumElement<TResult> ErzeugeIEntscheidungsbaumWurzel(in SpeicherKnoten knoten)
    {
      Type wurzeltype = Type.GetType(knoten.Wureltyp);
      EntscheidungsbaumElement<TResult> wurzel = new EntscheidungsbaumElement<TResult>(wurzeltype);
      Array ergebnisse = Enum.GetValues(typeof(TResult));

      foreach (string kategorie in knoten.Kinder.Keys)
      {
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

    /// <summary>
    /// Wandelt ein <paramref name="wurzel"/> in ein Objekt vom Typ <see cref="SpeicherKnoten"/> um, um es dann auf die Platte zu speichern.
    /// </summary>
    /// <remarks>
    /// Wird benötigt, da sich die <see cref="EntscheidungsbaumElement{TResult}"/>-Klassen nicht einfach so sreialisieren lassen.
    /// </remarks>
    /// <param name="wurzel">Wurzel des aktuell umzuwandelnden (Teil-) Baumes.</param>
    /// <returns>Erzeugtes Objekt.</returns>
    private SpeicherKnoten ErzeugeSpeicherbaum(IEntscheidungsbaumWurzel wurzel)
    {
      SpeicherKnoten knoten = new SpeicherKnoten();
      knoten.Wureltyp = wurzel.KnotenTyp.AssemblyQualifiedName;


      Array kategorien = Enum.GetValues(wurzel.KnotenTyp);
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

    /// <summary>
    /// Klasse zum Abspeichern eines Knotens.
    /// </summary>
    /// <remarks>
    /// <br /><b>Versionen:</b><br />
    /// V1.0 06.06.2021 - Paz-Paz - erstellt<br />
    /// </remarks>
    private class SpeicherKnoten
    {
      /// <summary>
      /// Liste der Kinder.
      /// </summary>
      public Dictionary<string, object> Kinder { get; set; } = new Dictionary<string, object>();

      /// <summary>
      /// Voll-Qualifizierter Wurzeltyp. (<see cref="Type.AssemblyQualifiedName"/>)
      /// </summary>
      public string Wureltyp { get; set; }

    }
    #endregion .............................................................................................................

  }

}

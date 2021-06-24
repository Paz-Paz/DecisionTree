using EntscheidungsbaumLernen.Helper;
using EntscheidungsbaumLernen.Interfaces;
using System;
using System.Reflection;

namespace EntscheidungsbaumLernen.Controller
{
  #region CLASS DialogKomponente .........................................................................................

  /// <summary>
  /// Komponente für den Dialog mit dem Benutzer des Systems.
  /// </summary>
  /// <remarks>
  /// Aktuell kann man nur ein "kompletes" Ergebnis-Objek übergeben werden.<br />
  /// <br /><b>Versionen:</b><br />
  /// V1.0 14.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  /// <inheritdoc cref="IDialogAnwenden{TEingabe, TResult}"/>
  internal class DialogAnwenden<TEingabe, TResult> : IDialogAnwenden<TEingabe, TResult> where TEingabe : class where TResult : Enum
  {
    #region Eigenschaften ..................................................................................................

    /// <summary>
    /// Wissensspeicher zum Auslesen des Baums.
    /// </summary>
    private readonly IWissensspeicher _wissensspeicher;

    #endregion .............................................................................................................
    #region Konstruktor ....................................................................................................

    /// <summary>
    /// Liefert ein <see cref="DialogAnwenden{TEingabe, TResult}"/>-Objekt.
    /// </summary>
    /// <param name="wissensspeicher">Objekt zum Laden des Entscheidungsbaumes.</param>
    internal DialogAnwenden(IWissensspeicher wissensspeicher)
    {
      this._wissensspeicher = wissensspeicher ?? throw new ArgumentNullException(nameof(wissensspeicher));
    }

    #endregion .............................................................................................................
    #region Oeffentliche Methoden ..........................................................................................

    /// <inheritdoc/>
    public TResult Abfragen()
    {
      return this.Abfragen(this._wissensspeicher.LadeBaum(), true);
    }

    /// <inheritdoc/>
    public TResult EingabeAuswerten(in TEingabe eingabe)
    {
      Console.Write("\nAuszuwerten: ");
      bool first = true;
      foreach (PropertyInfo propertyInfo in eingabe.GetType().GetProperties())
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
        Console.Write($"{propertyInfo.Name}: {KlassenHelper.GetValue(eingabe, propertyInfo.PropertyType)}");
      }
      Console.WriteLine("\n");

      IEntscheidungsbaumWurzel wurzel = this._wissensspeicher.LadeBaum();
      return this.Auswerten(wurzel, eingabe, 1);
    }
    #endregion .............................................................................................................
    #region Private Methoden ...............................................................................................

    /// <summary>
    /// Wertet die übergebene <paramref name="eingabe"/> Anhand des übergebenen <paramref name="entscheidungsbaumWurzel"/> aus.
    /// </summary>
    /// <remarks>
    /// Funktion ist nicht viel mehr als eine rekursive Tiefensuche auf einen Baum.
    /// </remarks>
    /// <param name="entscheidungsbaumWurzel">Aktueller (Teil-) Baum zum Auswerten des Ausdrucks.</param>
    /// <param name="eingabe">Eingabe-Objekt, welches ausgewerte werden soll.</param>
    /// <param name="zaehler">Zähler, um Zahlen vor die Ausgewählten Attribute zu schreiben.</param>
    /// <returns>Ergebnis der Auswertung.</returns>
    private TResult Auswerten(in IEntscheidungsbaumWurzel entscheidungsbaumWurzel, in TEingabe eingabe, in int zaehler)
    {
      Type attribut = entscheidungsbaumWurzel.KnotenTyp;
      object wert = KlassenHelper.GetValue(eingabe, attribut);

      string kategorie = wert.ToString();
      object kind = entscheidungsbaumWurzel.GetKind(kategorie);

      Console.WriteLine($"{zaehler,3}. Auswahl: {attribut.Name}: {wert}");

      if (entscheidungsbaumWurzel.IstBlatt(kategorie))
      {
        return (TResult)kind;
      }

      return this.Auswerten((IEntscheidungsbaumWurzel)kind, eingabe, zaehler + 1);
    }

    /// <summary>
    /// Stellt dem Benutzer Fragen anhand derer dann das Ergebnis generiert wird.
    /// </summary>
    /// <param name="baumwuzel">Wurzel des aktuellen (Teil-) Baumes um die nächste Frage auszuwählen.</param>
    /// <param name="mitTitel">Flag um zu Steuern ob eine Titel-Zeile ausgegeben werden soll oder nicht.</param>
    /// <returns>Ergebnis der Auswertung.</returns>
    private TResult Abfragen(in IEntscheidungsbaumWurzel baumwuzel, bool mitTitel)
    {
      if (mitTitel)
      {
        Console.WriteLine("\nBitte Fragen beantworten um das Wissen abzufragen:");
      }
      string kategorie = this.GetKategorie(baumwuzel.KnotenTyp);
      object kind = baumwuzel.GetKind(kategorie);

      if (baumwuzel.IstBlatt(kategorie))
      {
        return (TResult)kind;
      }
      else
      {
        return this.Abfragen((IEntscheidungsbaumWurzel)kind, false);
      }
    }

    /// <summary>
    /// Fragt den Anwender nach dem Attribut und gibt die Kategorie, bzw. den Enum-Werte als String zurück.
    /// </summary>
    /// <param name="wurzelType">Aktueller (Teil-) Baum um den Dialog zu generieren und die Antwort auszuwerten.</param>
    /// <returns>Name der vom Benutzer ausgewählten Kategorie.</returns>
    private string GetKategorie(Type wurzelType)
    {
      // Kategorie-Liste erstellen:
      Console.WriteLine($"\nBitte {wurzelType.Name} auswählen:");
      Array kategorien = Enum.GetValues(wurzelType);
      for (int i = 0; i < kategorien.Length; i++)
      {
        Console.WriteLine($"{i + 1,2}. {kategorien.GetValue(i)}");
      }

      // Kategorie abfragen:
      do
      {
        Console.Write("\nBitte die Zahl vor der Auswahl eingeben u. [ENTER] drücken: ");
        string antwortString = Console.ReadLine();
        if (int.TryParse(antwortString, out int antwortInt))
        {
          if (antwortInt > 0 && antwortInt <= kategorien.Length)
          {
            string auswahl = kategorien.GetValue(antwortInt - 1).ToString();
            Console.WriteLine($"Die Option '{auswahl}' (Nr. {antwortInt}) wurde ausgewählt.");
            return auswahl;
          }
        }
        Console.WriteLine($"'{antwortString}' ist keine Korrekte Antwort, bitte nochmals probieren.");
      } while (true);

    }

    #endregion .............................................................................................................
  }

  #endregion .............................................................................................................
}

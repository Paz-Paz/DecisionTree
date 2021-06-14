using EntscheidungsbaumLernen.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace EntscheidungsbaumLernen.Helper
{
  #region STATIC CLASS Ausgeber ..........................................................................................

  /// <summary>
  /// Klasse um diverse Ausgaben an zentraler Stelle zu verwalten.
  /// </summary>
  /// <remarks>
  /// <br /><b>Versionen:</b><br />
  /// V1.0 06.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  internal static class Ausgeber
  {

    #region Paket-Interne Methoden .........................................................................................

    /// <summary>
    /// Schreibt die Liste der übergebenen Attribute auf die Konsole.
    /// </summary>
    /// <remarks>
    /// <b>Beispiel:</b><br />
    /// <example>
    /// <code>
    /// Attributliste:<br />
    /// *) Attraktiv<br />
    /// *) Preis<br />
    /// *) Loge<br />
    /// *) Wetter<br />
    /// *) ...
    ///  </code>
    /// </example>
    /// </remarks>
    /// <param name="liste">Liste der auszugebenen Attribute.</param>
    internal static void GibAttributlisteAufKonsoleAus(in List<Type> liste)
    {
      if (liste?.Count == 0)
      {
        return;
      }

      Console.WriteLine("\nAttributliste: ");
      foreach (Type type in liste)
      {
        Console.WriteLine($"*) {type.Name}");
      }
    }

    /// <summary>
    /// Gibt den Baum auf der Konsole aus.
    /// </summary>
    /// <remarks>
    /// <b>Beispiel:</b><br />
    /// <example>
    /// <code>
    /// Baumstruktur:<br />
    /// Gruppe<br />
    ///  ├ Freunde: Kategorie<br />
    ///  │  ├ AC: Ja<br />
    ///  │  ├ KO: Attraktiv<br />
    /// .....
    ///  </code>
    /// </example>
    /// </remarks>
    /// <param name="wurzel">Wurzelknoten des Entscheidungsbaumes.</param>
    internal static void GibBaumAus(in IEntscheidungsbaumWurzel wurzel)
    {
      Console.WriteLine("\nBaumstruktur:");
      Console.WriteLine(wurzel.KnotenTyp.Name);
      GibBaumAus(wurzel, new List<string>());
    }

    /// <summary>
    /// Gibt die übergebene Beispielliste als Tabelle auf der Konsole aus.
    /// </summary>
    /// <typeparam name="TBsp">Klasse der Beispiel-Elemente.</typeparam>
    /// <typeparam name="TResult">Eigenschaft des Ergebnisses, um es ganz rechts darzustellen.</typeparam>
    /// <param name="beispielliste">Liste der auszugebenden Beispiele.</param>
    internal static void GibBeispiellisteAufKonsoleAus<TBsp, TResult>(in List<TBsp> beispielliste) where TBsp : class where TResult : Enum
    {
      if (beispielliste?.Count == 0)
      {
        return;
      }

      Console.WriteLine("\nLern-Daten:");
      PropertyInfo[] propertyInfos = typeof(TBsp).GetProperties();
      List<PropertyInfo> properties = PropertiesErmitteln<TResult>(propertyInfos);
      List<string> titel = TitelErmitteln<TResult>(propertyInfos);
      int[] laengen = ErmittleBreiteAllerSpalten(beispielliste, titel, properties);

      PasseFensterbreiteAn(laengen);
      SchreibeTitel(titel, laengen);
      SchreibeQuerstrich(laengen, false);
      SchreibeInhalt<TBsp, TResult>(beispielliste, laengen, properties);
      SchreibeQuerstrich(laengen, true);

    }

    #endregion .............................................................................................................
    #region Private Methoden ...............................................................................................

    /// <inheritdoc cref="GibBaumAus(in IEntscheidungsbaumWurzel)"/>
    /// <param name="praefixliste">Liste an Praefixen, die vor den aktuellen Baum gesetzt werden sollen.</param>
    /// <param name="wurzel"><inheritdoc cref="GibBaumAus(in IEntscheidungsbaumWurzel)"/></param>
    private static void GibBaumAus(in IEntscheidungsbaumWurzel wurzel, in List<string> praefixliste)
    {
      Array attribute = Enum.GetValues(wurzel.KnotenTyp);

      for (int i = 0; i < attribute.Length; i++)
      {
        string prefix = (i == attribute.Length - 1) ? " └ " : " ├ ";

        object kind = attribute.GetValue(i);
        string kindNamen = kind.ToString();

        GibPraefixAus(praefixliste);
        Console.Write($"{prefix}{kindNamen}: ");

        if (wurzel.IstBlatt(kindNamen))
        {
          Console.WriteLine(wurzel.GetKind(kindNamen));
        }
        else
        {
          IEntscheidungsbaumWurzel blatt = (IEntscheidungsbaumWurzel)wurzel.GetKind(kindNamen);
          Console.WriteLine(blatt.KnotenTyp.Name);
          List<string> neueListe = new List<string>(praefixliste);
          if (i == attribute.Length - 1)
          {
            neueListe.Add("   ");
          }
          else
          {
            neueListe.Add(" │ ");
          }
          GibBaumAus(blatt, neueListe);
        }
      }

    }

    /// <summary>
    /// Schreibt die Liste der übergebenen Texte (ohne Zeilenumbruch) auf die Konsole.
    /// </summary>
    /// <param name="liste">Liste die geschrieben werden soll.</param>
    private static void GibPraefixAus(in List<string> liste)
    {
      foreach (string element in liste)
      {
        Console.Write(element);
      }
    }

    /// <summary>
    /// Ermittelt die Breite der auszugebenden Tabelle.
    /// </summary>
    /// <typeparam name="TBsp">Typ der Beispiel-Elemente.</typeparam>
    /// <param name="beispielliste">Liste an Beispielen die ausgegeben werden sollen.</param>
    /// <param name="titel">Liste der Titel zu den Beispielen.</param>
    /// <param name="properties">Liste der Eigenschaften aus <typeparamref name="TBsp"/> die ausgegeben werden sollen.</param>
    /// <returns>Die Breite der Tabelle.</returns>
    private static int[] ErmittleBreiteAllerSpalten<TBsp>(in List<TBsp> beispielliste, in List<string> titel, in List<PropertyInfo> properties) where TBsp : class
    {
      // Längen der Überschriften ermitteln:
      int[] laengen = new int[titel.Count];
      for (int i = 0; i < titel.Count; i++)
      {
        laengen[i] = titel[i].Length;
      }

      // Wenn die größte Ziffer länger ist als der Titel, dann anpassen:
      if (beispielliste.Count.ToString().Length > laengen[0])
      {
        laengen[0] = beispielliste.Count.ToString().Length;
      }

      // Längen aller Werte abgleichen:
      foreach (TBsp objekt in beispielliste)
      {
        for (int i = 1; i < laengen.Length - 1; i++)
        {
          object wert = KlassenHelper.GetValue(objekt, properties[i - 1].PropertyType);
          if (wert.ToString().Length > laengen[i])
          {
            laengen[i] = wert.ToString().Length;
          }
        }
      }

      return laengen;
    }

    /// <summary>
    /// Passt, bei Bedarf, die Breite des CMD-Fensters an die Breite der Tabelle an.
    /// </summary>
    /// <param name="laengen">Spaltenbreiten zum ermitteln der Tabellenbreite.</param>
    private static void PasseFensterbreiteAn(in int[] laengen)
    {
      // Fensterbreite anpassen:
      int sollBreite = 0;
      foreach (int laenge in laengen)
      {
        sollBreite += laenge + 2;
      }
      sollBreite += 6 + 1;
      if (Console.WindowWidth < sollBreite)
      {
        Console.WindowWidth = sollBreite;
      }
    }

    /// <summary>
    /// Filtert die übergebene Liste an Eigenschaften und gibt die Verwendbaren zurück.
    /// </summary>
    /// <typeparam name="TResult">Ergebnis-Typ, welcher nicht zurückgegeben werden soll.</typeparam>
    /// <param name="propertyInfos">Liste der zu filternden Eigenschaften.</param>
    /// <returns>Liste der Eigenschaften die nicht rausgefiltert wurden.</returns>
    private static List<PropertyInfo> PropertiesErmitteln<TResult>(PropertyInfo[] propertyInfos) where TResult : Enum
    {
      List<PropertyInfo> properties = new List<PropertyInfo>();
      foreach (PropertyInfo propertyInfo in propertyInfos)
      {
        if (!propertyInfo.CanRead)
        {
          continue;
        }

        if (!propertyInfo.PropertyType.IsEnum)
        {
          continue;
        }
        if (propertyInfo.PropertyType == typeof(TResult))
        {
          continue;
        }

        properties.Add(propertyInfo);

      }
      return properties;
    }

    /// <summary>
    /// Schreibt den Hinhalt von <paramref name="beispielliste"/> in Tabellenform in die Konsolenausgabe.
    /// </summary>
    /// <typeparam name="TBsp">Typ der Beispiele.</typeparam>
    /// <typeparam name="TResult">Typ des Ergebnisses.</typeparam>
    /// <param name="beispielliste">Liste der auszugebenden Beispiele.</param>
    /// <param name="laengen">Liste der Breiten der einzelnen Spalten.</param>
    /// <param name="properties">Liste der Eigenschaften die aus <typeparamref name="TBsp"/> ausgegeben werden sollen.</param>
    private static void SchreibeInhalt<TBsp, TResult>(in List<TBsp> beispielliste, in int[] laengen, in List<PropertyInfo> properties) where TBsp : class where TResult : Enum
    {
      for (int i = 0; i < beispielliste.Count; i++)
      {
        TBsp objekt = beispielliste[i];

        Console.Write($"  {i.ToString().PadLeft(laengen[0])}");
        Console.Write("  │");
        for (int j = 0; j < properties.Count; j++)
        {
          object wert = KlassenHelper.GetValue(objekt, properties[j].PropertyType);
          Console.Write($"  {wert.ToString().PadLeft(laengen[j + 1])}");
        }
        Console.Write("  │");
        object ergebnisWert = KlassenHelper.GetValue(objekt, typeof(TResult));
        Console.Write($"  {ergebnisWert.ToString().PadLeft(laengen[laengen.Length - 1])}");

        Console.WriteLine();
      }
    }

    /// <summary>
    /// Zeichnet eine Horizontale Line (─), wobei nach dem ersten Element und vor dem letzten Element ein ┼ hinzugefügt wrid.
    /// </summary>
    /// <param name="laengen">Liste der Breiten der einzelnen Spalten.</param>
    /// <param name="letzteZeile">Wenn hier <i>true</i> ist, wird ein ┴ statt dem ┼ verwendet.</param>
    private static void SchreibeQuerstrich(in int[] laengen, in bool letzteZeile)
    {
      // Horizontale Linie zeichnen:
      string kreuz = letzteZeile ? "┴" : "┼";
      for (int i = 0; i < laengen.Length; i++)
      {
        Console.Write("──");
        if (i == 1)
        {
          Console.Write(kreuz);
          Console.Write("──");
        }
        if (i == laengen.Length - 1)
        {
          Console.Write(kreuz);
          Console.Write("──");
        }
        Console.Write($"{"".PadLeft(laengen[i], '─')}");
      }
      Console.WriteLine();
    }

    /// <summary>
    /// Schreibt die Titel auf die Konsolenausgabe.
    /// </summary>
    /// <param name="titel">Liste der Titel.</param>
    /// <param name="laengen">Liste der Spaltenbreiten.</param>
    private static void SchreibeTitel(in List<string> titel, in int[] laengen)
    {
      // Titel schreiben:
      for (int i = 0; i < titel.Count; i++)
      {
        if (i == 1)
        {
          Console.Write("  │");
        }
        if (i == titel.Count - 1)
        {
          Console.Write("  │");
        }
        Console.Write($"  {titel[i].PadLeft(laengen[i])}");
      }
      Console.WriteLine();
    }

    /// <summary>
    /// Ermittelt anhand der übergebenen <paramref name="propertyInfos"/> den Namen der Titel.
    /// </summary>
    /// <typeparam name="TResult">Ergebnis-Eigenschaft, da deren Name nicht zurückgegeben wird.</typeparam>
    /// <param name="propertyInfos">Liste der Eigenschaften deren Namen ermittelt werden soll.</param>
    /// <returns>Liste der ermittelten Eigenschaftsnamen.</returns>
    private static List<string> TitelErmitteln<TResult>(PropertyInfo[] propertyInfos) where TResult : Enum
    {
      List<string> titel = new List<string>();
      titel.Add("Nr.");
      foreach (PropertyInfo propertyInfo in propertyInfos)
      {
        if (!propertyInfo.CanRead)
        {
          continue;
        }

        if (!propertyInfo.PropertyType.IsEnum)
        {
          continue;
        }
        if (propertyInfo.PropertyType == typeof(TResult))
        {
          continue;
        }

        titel.Add(propertyInfo.Name);
      }

      foreach (PropertyInfo propertyInfo in propertyInfos)
      {
        if (!propertyInfo.CanRead)
        {
          continue;
        }

        if (!propertyInfo.PropertyType.IsEnum)
        {
          continue;
        }
        if (propertyInfo.PropertyType == typeof(TResult))
        {
          titel.Add(propertyInfo.Name);
          break;
        }

      }
      return titel;
    }

    #endregion .............................................................................................................
  }

  #endregion .............................................................................................................
}

using EntscheidungsbaumLernen.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace EntscheidungsbaumLernen.Helper
{
  #region STATIC CLASS Ausgeber ..........................................................................................

  internal static class Ausgeber
  {

    #region Paket-Interne Methoden .........................................................................................

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

    internal static void GibBaumAus(in IEntscheidungsbaumWurzel wurzel)
    {
      Console.WriteLine("\nBaumstruktur:");
      Console.WriteLine(wurzel.Wureltyp.Name);
      GibBaumAus(wurzel, new List<string>());
    }

    internal static void GibLerndatenlisteAufKonsoleAus<TBsp, TResult>(in List<TBsp> beispielliste) where TBsp : class where TResult : Enum
    {
      if (beispielliste?.Count == 0)
      {
        return;
      }

      Console.WriteLine("\nLern-Daten:");
      PropertyInfo[] propertyInfos = typeof(TBsp).GetProperties();
      List<PropertyInfo> properties = PropertiesErmitteln<TResult>(propertyInfos);
      List<string> titel = TitelErmitteln<TResult>(propertyInfos);
      int[] laengen = LaengenErmitteln(beispielliste, titel, properties);

      PasseFensterbreiteAn(laengen);
      SchreibeTitel(titel, laengen);
      SchreibeQuerstrich(titel, laengen, false);
      SchreibeInhalt<TBsp, TResult>(beispielliste, laengen, properties);
      SchreibeQuerstrich(titel, laengen, true);

    }

    #endregion .............................................................................................................
    #region Private Methoden ...............................................................................................

    private static void GibBaumAus(in IEntscheidungsbaumWurzel wurzel, in List<string> praefixliste)
    {
      Array attribute = Enum.GetValues(wurzel.Wureltyp);

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
          Console.WriteLine(blatt.Wureltyp.Name);
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

    private static void GibPraefixAus(in List<string> liste)
    {
      foreach (string element in liste)
      {
        Console.Write(element);
      }
    }

    private static int[] LaengenErmitteln<T>(in List<T> beispielliste, in List<string> titel, in List<PropertyInfo> properties) where T : class
    {
      // Längen der Überschriften ermitteln:
      int[] laengen = new int[titel.Count];
      for (int i = 0; i < titel.Count; i++)
      {
        laengen[i] = titel[i].Length;
      }
      if (beispielliste.Count.ToString().Length > laengen[0])
      {
        laengen[0] = beispielliste.Count.ToString().Length;
      }
      //laengen[laengen.Length - 1] = 6;

      // Längen aller Werte abgleichen:
      foreach (T objekt in beispielliste)
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

    private static void SchreibeQuerstrich(in List<string> titel, in int[] laengen, in bool letzteZeile)
    {
      // Horizontale Linie zeichnen:
      string kreuz = letzteZeile ? "┴" : "┼";
      for (int i = 0; i < titel.Count; i++)
      {
        Console.Write("──");
        if (i == 1)
        {
          Console.Write(kreuz);
          Console.Write("──");
        }
        if (i == titel.Count - 1)
        {
          Console.Write(kreuz);
          Console.Write("──");
        }
        Console.Write($"{"".PadLeft(laengen[i], '─')}");
      }
      Console.WriteLine();
    }

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

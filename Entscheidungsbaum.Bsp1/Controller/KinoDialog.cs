// TODO: Abfrage-Komponente in EntscheidungsbaumLernen einbauen.
//using Entscheidungsbaum.Bsp1.Models;
//using EntscheidungsbaumLernen.Interfaces;
//using System;

//namespace Entscheidungsbaum.Bsp1.Controller
//{
//  #region CLASS DialogKomponente .........................................................................................

//  class KinoDialog
//  {
//    #region Oeffentliche Methoden ..........................................................................................

//    /// <inheritdoc/>
//    public Ergebniss Abfragen(in IEntscheidungsbaumWurzel baumwuzel)
//    {
//      return this.BesuchAbfragen(baumwuzel, true);
//    }

//    #endregion .............................................................................................................
//    #region Private Methoden ...............................................................................................

//    private Ergebniss BesuchAbfragen(in IEntscheidungsbaumWurzel baumwuzel, bool mitTitel)
//    {
//      if (mitTitel)
//      {
//        Console.WriteLine("\nBitte Fragen beantworten um das Wissen abzufragen:");
//      }
//      string kategorie = this.GetKategorie(baumwuzel.KnotenTyp);
//      object kind = baumwuzel.GetKind(kategorie);

//      if (baumwuzel.IstBlatt(kategorie))
//      {
//        return (Ergebniss)kind;
//      }
//      else
//      {
//        return this.BesuchAbfragen((IEntscheidungsbaumWurzel)kind, false);
//      }
//    }

//    private string GetKategorie(Type wurzelType)
//    {
//      if (wurzelType == typeof(Attraktiv))
//      {
//        return this.GetAttraktivitaet().ToString();
//      }
//      if (wurzelType == typeof(Preis))
//      {
//        return this.GetPreis().ToString();
//      }
//      if (wurzelType == typeof(Loge))
//      {
//        return this.GetLoge().ToString();
//      }
//      if (wurzelType == typeof(Wetter))
//      {
//        return this.GetWetter().ToString();
//      }
//      if (wurzelType == typeof(Warten))
//      {
//        return this.GetWarten().ToString();
//      }
//      if (wurzelType == typeof(Besetzung))
//      {
//        return this.GetBesetzung().ToString();
//      }
//      if (wurzelType == typeof(Kategorie))
//      {
//        return this.GetKategorie().ToString();
//      }
//      if (wurzelType == typeof(Land))
//      {
//        return this.GetLand().ToString();
//      }
//      if (wurzelType == typeof(Reserviert))
//      {
//        return this.GetReserviert().ToString();
//      }
//      if (wurzelType == typeof(Gruppe))
//      {
//        return this.GetGruppe().ToString();
//      }
//      throw new NotImplementedException($"Wurzeltype '{wurzelType}' ist nicht implementiert.");
//    }

//    private Attraktiv GetAttraktivitaet()
//    {
//      Console.Write("\nAttriaktivität (g(ering), m(ittel), h(och)): ");
//      Attraktiv attraktivitaet = Attraktiv.Gering;
//      bool gefunden;
//      do
//      {
//        gefunden = true;
//        string attraktivitaetString = Console.ReadLine();
//        if (attraktivitaetString?.Length == 0)
//        {
//          Console.Write($"'{attraktivitaetString}' ist zu kurz, bitte neu eingeben: ");
//          gefunden = false;
//          continue;
//        }
//        switch (attraktivitaetString.Substring(0, 1).ToLower())
//        {
//          case "g":
//            attraktivitaet = Attraktiv.Gering;
//            gefunden = true;
//            break;
//          case "m":
//            attraktivitaet = Attraktiv.Mittel;
//            break;
//          case "h":
//            attraktivitaet = Attraktiv.Hoch;
//            break;
//          default:
//            gefunden = false;
//            Console.WriteLine($"'{attraktivitaetString}' ist nicht gültig, bitte neu eingeben...");
//            break;
//        }
//      } while (!gefunden);

//      Console.WriteLine($"Attraktivität: {attraktivitaet}.");
//      return attraktivitaet;
//    }

//    private Preis GetPreis()
//    {
//      Console.Write("\nPreis (m(ittel), h(och)): ");
//      Preis preis = Preis.Hoch;
//      bool gefunden;
//      do
//      {
//        gefunden = true;
//        string preisString = Console.ReadLine();
//        if (preisString?.Length == 0)
//        {
//          Console.Write($"'{preisString}' ist zu kurz, bitte neu eingeben: ");
//          gefunden = false;
//          continue;
//        }
//        switch (preisString.Substring(0, 1).ToLower())
//        {
//          case "m":
//            preis = Preis.Mittel;
//            gefunden = true;
//            break;
//          case "h":
//            preis = Preis.Hoch;
//            break;
//          default:
//            gefunden = false;
//            Console.WriteLine($"'{preisString}' ist nicht gültig, bitte neu eingeben...");
//            break;
//        }
//      } while (!gefunden);

//      Console.WriteLine($"Preis: {preis}.");
//      return preis;
//    }

//    private Loge GetLoge()
//    {
//      Console.Write("\nLoge (j(a), n(ein)): ");
//      Loge loge = Loge.Ja;
//      bool gefunden;
//      do
//      {
//        gefunden = true;
//        string logeString = Console.ReadLine();
//        if (logeString?.Length == 0)
//        {
//          Console.Write($"'{logeString}' ist zu kurz, bitte neu eingeben: ");
//          gefunden = false;
//          continue;
//        }
//        switch (logeString.Substring(0, 1).ToLower())
//        {
//          case "j":
//            loge = Loge.Ja;
//            gefunden = true;
//            break;
//          case "n":
//            loge = Loge.Nein;
//            break;
//          default:
//            gefunden = false;
//            Console.WriteLine($"'{logeString}' ist nicht gültig, bitte neu eingeben...");
//            break;
//        }
//      } while (!gefunden);

//      Console.WriteLine($"Loge: {loge}.");
//      return loge;
//    }

//    private Wetter GetWetter()
//    {
//      Console.Write("\nWetter (s(ch)ö(n), mi(ttel), s(ch)l(echt)): ");
//      Wetter wetter = Wetter.Schlecht;
//      bool gefunden;
//      do
//      {
//        gefunden = true;
//        string wetterString = Console.ReadLine();
//        if (wetterString?.Length < 2)
//        {
//          gefunden = false;
//          Console.Write($"'{wetterString}' ist zu kurz, bitte neu eingeben: ");
//          continue;
//        }
//        switch (wetterString.Substring(0, 2).ToLower())
//        {
//          case "sö":
//            wetter = Wetter.Schoen;
//            gefunden = true;
//            break;
//          case "mi":
//            wetter = Wetter.Mittel;
//            break;
//          case "sl":
//            wetter = Wetter.Schlecht;
//            break;
//          default:
//            gefunden = false;
//            Console.WriteLine($"'{wetterString}' ist nicht gültig, bitte neu eingeben...");
//            break;
//        }
//      } while (!gefunden);

//      Console.WriteLine($"Wetter: {wetter}.");
//      return wetter;
//    }

//    private Warten GetWarten()
//    {
//      Console.Write("\nWarten (j(a), n(ein)): ");
//      Warten warten = Warten.Ja;
//      bool gefunden;
//      do
//      {
//        gefunden = true;
//        string wartenString = Console.ReadLine();
//        if (wartenString?.Length == 0)
//        {
//          Console.Write($"'{wartenString}' ist zu kurz, bitte neu eingeben: ");
//          gefunden = false;
//          continue;
//        }
//        switch (wartenString.Substring(0, 1).ToLower())
//        {
//          case "j":
//            warten = Warten.Ja;
//            gefunden = true;
//            break;
//          case "n":
//            warten = Warten.Nein;
//            break;
//          default:
//            gefunden = false;
//            Console.WriteLine($"'{wartenString}' ist nicht gültig, bitte neu eingeben...");
//            break;
//        }
//      } while (!gefunden);

//      Console.WriteLine($"Warten: {warten}.");
//      return warten;
//    }

//    private Besetzung GetBesetzung()
//    {
//      Console.Write("\nBesetzung (m(ittel), t(op)): ");
//      Besetzung besetzung = Besetzung.Mittel;
//      bool gefunden;
//      do
//      {
//        gefunden = true;
//        string besetzungString = Console.ReadLine();
//        if (besetzungString?.Length == 0)
//        {
//          Console.Write($"'{besetzungString}' ist zu kurz, bitte neu eingeben: ");
//          gefunden = false;
//          continue;
//        }
//        switch (besetzungString.Substring(0, 1).ToLower())
//        {
//          case "m":
//            besetzung = Besetzung.Mittel;
//            gefunden = true;
//            break;
//          case "t":
//            besetzung = Besetzung.Top;
//            break;
//          default:
//            gefunden = false;
//            Console.WriteLine($"'{besetzungString}' ist nicht gültig, bitte neu eingeben...");
//            break;
//        }
//      } while (!gefunden);

//      Console.WriteLine($"Besetzung: {besetzung}.");
//      return besetzung;
//    }

//    private Kategorie GetKategorie()
//    {
//      Console.Write("\nKategorie (a(ction), d(rama), k(omödie), s(ince fiction)): ");
//      Kategorie kategorie = Kategorie.AC;
//      bool gefunden;
//      do
//      {
//        gefunden = true;
//        string kategorieString = Console.ReadLine();
//        if (kategorieString?.Length == 0)
//        {
//          Console.Write($"'{kategorieString}' ist zu kurz, bitte neu eingeben: ");
//          gefunden = false;
//          continue;
//        }
//        switch (kategorieString.Substring(0, 1).ToLower())
//        {
//          case "a":
//            kategorie = Kategorie.AC;
//            gefunden = true;
//            break;
//          case "d":
//            kategorie = Kategorie.DR;
//            break;
//          case "k":
//            kategorie = Kategorie.KO;
//            break;
//          case "s":
//            kategorie = Kategorie.SF;
//            break;
//          default:
//            gefunden = false;
//            Console.WriteLine($"'{kategorieString}' ist nicht gültig, bitte neu eingeben...");
//            break;
//        }
//      } while (!gefunden);

//      Console.WriteLine($"Kategorie: {kategorie}.");
//      return kategorie;
//    }

//    private Land GetLand()
//    {
//      Console.Write("\nLand (i(nternational), n(ational)): ");
//      Land land = Land.International;
//      bool gefunden;
//      do
//      {
//        gefunden = true;
//        string landString = Console.ReadLine();
//        if (landString?.Length == 0)
//        {
//          Console.Write($"'{landString}' ist zu kurz, bitte neu eingeben: ");
//          gefunden = false;
//          continue;
//        }
//        switch (landString.Substring(0, 1).ToLower())
//        {
//          case "i":
//            land = Land.International;
//            gefunden = true;
//            break;
//          case "n":
//            land = Land.National;
//            break;
//          default:
//            gefunden = false;
//            Console.WriteLine($"'{landString}' ist nicht gültig, bitte neu eingeben...");
//            break;
//        }
//      } while (!gefunden);

//      Console.WriteLine($"Land: {land}.");
//      return land;
//    }

//    private Reserviert GetReserviert()
//    {
//      Console.Write("\nReserviert (j(a), n(ein)): ");
//      Reserviert reserviert = Reserviert.Ja;
//      bool gefunden;
//      do
//      {
//        gefunden = true;
//        string reserviertString = Console.ReadLine();
//        if (reserviertString?.Length == 0)
//        {
//          Console.Write($"'{reserviertString}' ist zu kurz, bitte neu eingeben: ");
//          gefunden = false;
//          continue;
//        }
//        switch (reserviertString.Substring(0, 1).ToLower())
//        {
//          case "j":
//            reserviert = Reserviert.Ja;
//            gefunden = true;
//            break;
//          case "n":
//            reserviert = Reserviert.Nein;
//            break;
//          default:
//            gefunden = false;
//            Console.WriteLine($"'{reserviertString}' ist nicht gültig, bitte neu eingeben...");
//            break;
//        }
//      } while (!gefunden);

//      Console.WriteLine($"Reserviert: {reserviert}.");
//      return reserviert;
//    }

//    private Gruppe GetGruppe()
//    {
//      Console.Write("\nGruppe (a(llein), p(aar), f(reunde)): ");
//      Gruppe gruppe = Gruppe.Allein;
//      bool gefunden;
//      do
//      {
//        gefunden = true;
//        string gruppeString = Console.ReadLine();
//        if (gruppeString?.Length == 0)
//        {
//          Console.Write($"'{gruppeString}' ist zu kurz, bitte neu eingeben: ");
//          gefunden = false;
//          continue;
//        }
//        switch (gruppeString.Substring(0, 1).ToLower())
//        {
//          case "a":
//            gruppe = Gruppe.Allein;
//            gefunden = true;
//            break;
//          case "f":
//            gruppe = Gruppe.Freunde;
//            break;
//          case "p":
//            gruppe = Gruppe.Paar;
//            break;
//          default:
//            gefunden = false;
//            Console.WriteLine($"'{gruppeString}' ist nicht gültig, bitte neu eingeben...");
//            break;
//        }
//      } while (!gefunden);

//      Console.WriteLine($"Gruppe: {gruppe}.");
//      return gruppe;
//    }

//    #endregion .............................................................................................................
//  }

//  #endregion .............................................................................................................
//}

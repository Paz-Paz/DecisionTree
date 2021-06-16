using Entscheidungsbaum.Bsp1.Helper;
using Entscheidungsbaum.Bsp1.Models;
using EntscheidungsbaumLernen.Factorys;
using EntscheidungsbaumLernen.Interfaces;
using System;
using System.Collections.Generic;

namespace KinoBeispiel
{
  #region CLASS Program ..................................................................................................

  class Program
  {
    #region MAIN ...........................................................................................................

    static void Main(string[] args)
    {
      Console.WriteLine("\n ---------- INITIIEREN ----------");
      Console.WindowHeight = 55;
      List<KinobesuchBeispiel> lerndaten = LerndatenErsteller.ErstelleLerndaten();
      //List<Type> attributliste = new List<Type>() { typeof(Attraktiv), typeof(Preis) };
      List<Type> attributliste = new List<Type>() { typeof(Attraktiv), typeof(Preis), typeof(Loge), typeof(Wetter), typeof(Warten), typeof(Besetzung), typeof(Kategorie), typeof(Land), typeof(Reserviert), typeof(Gruppe) };


      Console.WriteLine("\n ---------- LERNEN ----------");
      Program program = new Program();
      program.Lerne(lerndaten, attributliste, Ergebniss.Nein);


      Console.WriteLine("\n ---------- ANWENDEN ----------");
      KinobesuchEingabe eingabe = new KinobesuchEingabe(Attraktiv.Mittel, Preis.Mittel, Loge.Ja, Wetter.Mittel, Warten.Ja, Besetzung.Top, Kategorie.KO, Land.International, Reserviert.Nein, Gruppe.Freunde);
      program.WerteAus<KinobesuchEingabe, Ergebniss>(eingabe);


      
      
      


      Console.WriteLine();
      Console.WriteLine("[ENTER] beendet...");
      Console.ReadLine();
    }

    #endregion .............................................................................................................
    #region Private Methoden ...............................................................................................

    private void Lerne<TBeispiel, TReslt>(List<TBeispiel> lerndaten, List<Type> attributliste, TReslt defaultWert) where TBeispiel : class where TReslt : Enum
    {
      IDialogLernen<TBeispiel, TReslt> dialogLernen = new DialogLernenFactory<TBeispiel, TReslt>()
                                      .AddSpeicherRam()
                                      .AddSpeicherDatei(@"D:\d.json")
                                      .AttributauswaehlerErstesAttribut()
                                      .Build();
      dialogLernen.BeispielHinzufuegen(lerndaten);
      dialogLernen.AusgabeLerndaten();
      dialogLernen.LerneBeispiele(attributliste, defaultWert);
      dialogLernen.AusgabeBaumstruktur();

      return;
    }

    private void WerteAus<TEingabe, TResult>(TEingabe eingabe) where TEingabe : class where TResult : Enum
    {
      IDialogAnwenden<TEingabe, TResult> dialogAnwenden = new DialogAnwendenFactory<TEingabe, TResult>()
                                      .AddSpeicherRam()
                                      .AddSpeicherDatei(@"D:\d.json")
                                      .Build();

      TResult ergebniss = dialogAnwenden.EingabeAuswerten(eingabe);

      Console.WriteLine($"\nErgebnis: {ergebniss}");
    }

    #endregion .............................................................................................................
  }

  #endregion .............................................................................................................
}

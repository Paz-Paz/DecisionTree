using Entscheidungsbaum.Bsp1.Controller;
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
      IHelper helper = new HelperFactory().Build();

      Console.WriteLine("\n ---------- INITIIEREN ----------");
      Console.WindowHeight = 55;
      List<KinobesuchBeispiel> lerndaten = LerndatenErsteller.ErstelleLerndaten();
      List<Type> attributliste = helper.ErstellAttributliste<KinobesuchBeispiel, Ergebniss>();

      //attributliste.Remove(typeof(Gruppe)); // Test ob das (bewusste) Entefrenen einzelner Eigenschaften auch funktioniert.
      //attributliste.Remove(typeof(Attraktivitaegggt)); // Test ob das (bewusste) Entefrenen einzelner Eigenschaften auch funktioniert.

      helper.GibBeispiellisteAufKonsoleAus<KinobesuchBeispiel, Ergebniss>(lerndaten);
      helper.GibAttributlisteAufKonsoleAus(attributliste);

      Console.WriteLine("\n ---------- LERNEN ----------");
      Program program = new Program();
      IWissensspeicher wissensspeicher = program.Lerne<KinobesuchBeispiel, Ergebniss>(lerndaten, attributliste);

      Console.WriteLine("\n ---------- KONTROLLIEREN ----------");
      helper.GibBaumAus(wissensspeicher.LadeBaum());

      Console.WriteLine("\n ---------- ANWENDEN ----------");
      KinobesuchEingabe eingabe = new KinobesuchEingabe(Attraktiv.Mittel, Preis.Mittel, Loge.Ja, Wetter.Mittel, Warten.Ja, Besetzung.Top, Kategorie.KO, Land.International, Reserviert.Nein, Gruppe.Freunde);
      program.WerteAus<KinobesuchEingabe, Ergebniss>(wissensspeicher, eingabe);



      program.FrageAb(wissensspeicher);

      Console.WriteLine();
      Console.WriteLine("[ENTER] beendet...");
      Console.ReadLine();
    }

    #endregion .............................................................................................................
    #region Private Methoden ...............................................................................................

    private IWissensspeicher Lerne<TBeispiel, TReslt>(List<TBeispiel> lerndaten, List<Type> attributliste) where TBeispiel : class where TReslt : Enum
    {
      ILernAlgoritmus<TBeispiel, TReslt> lernAlgDT = new LernAlgorithmusFactory<TBeispiel, TReslt>().Build();
      return lernAlgDT.Lerne(lerndaten, attributliste);
    }

    private void WerteAus<TEingabe, TResult>(IWissensspeicher wissensspeicher, TEingabe eingabe) where TEingabe : class where TResult : Enum
    {
      IEntscheidungsbaumWurzel baum = wissensspeicher.LadeBaum();
      IDialogKomponente dialogKomponente = new DialogKomponenteFactory().Build();
      TResult ergebniss = dialogKomponente.Abfragen<TEingabe, TResult>(baum, eingabe);

      Console.WriteLine($"\nErgebnis: {ergebniss}");
    }

    private void FrageAb(IWissensspeicher wissensspeicher)
    {
      IEntscheidungsbaumWurzel baum = wissensspeicher.LadeBaum();
      Ergebniss ergebniss = new KinoDialog().Abfragen(baum);

      Console.WriteLine($"\nErgebnis: {ergebniss}");
    }

    #endregion .............................................................................................................
  }

  #endregion .............................................................................................................
}

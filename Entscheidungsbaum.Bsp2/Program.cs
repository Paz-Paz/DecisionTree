using Entscheidungsbaum.Bsp2.Helper;
using Entscheidungsbaum.Bsp2.Model;
using EntscheidungsbaumLernen.Factorys;
using EntscheidungsbaumLernen.Interfaces;
using System;
using System.Collections.Generic;

namespace Beispiel2
{
  class Program
  {
    static void Main(string[] args)
    {
      IHelper helper = new HelperFactory().Build();

      // Lerndaten erstellen:
      List<AuftragBeispiel> liste = LerndatenErsteller.ErstelleBeispielListe();
      helper.GibBeispiellisteAufKonsoleAus<AuftragBeispiel, AuftragAnnehmen>(liste);

      // Lernen:
      ILernAlgoritmus<AuftragBeispiel, AuftragAnnehmen> lernAlgoritmus = new LernAlgorithmusFactory<AuftragBeispiel, AuftragAnnehmen>()
                                                                .AttributauswErstesAttribut()
                                                                //.SpeicherRam()
                                                                .SpeicherDatei(@"d:\a.json", true)
                                                                .Build();
      IWissensspeicher wissensspeicher = lernAlgoritmus.Lerne(liste);

      helper.GibBaumAus(wissensspeicher.LadeBaum());

      // Eingeben auszuwertender Daten:
      AuftragEingabe auftragEingabe = new AuftragEingabe(-1, Bereich.OnlineShop, Aufwand.Gross, Attraktivitaet.Hoch, Bauchgefuehl.Gut);

      // Daten auswerten u. ausgeben:
      IDialogKomponente dialogKomponente = new DialogKomponenteFactory().Build();
      AuftragAnnehmen ergebniss = dialogKomponente.Abfragen<AuftragEingabe, AuftragAnnehmen>(wissensspeicher.LadeBaum(), auftragEingabe);
      Console.WriteLine($"\nErgebniss: {ergebniss}");

      // Warten auf ENTER
      Console.WriteLine("\n[ENTER] beendet...");
      Console.ReadLine();

    }
  }
}

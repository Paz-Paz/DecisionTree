using Entscheidungsbaum.Bsp2.Helper;
using Entscheidungsbaum.Bsp2.Model;
using EntscheidungsbaumLernen.Factorys;
using EntscheidungsbaumLernen.Interfaces;
using System;

namespace Beispiel2
{
  class Program
  {
    static void Main(string[] args)
    {
      const string dateiname = @"D:\c.json";

      // Anlernen:
      IDialogLernen<AuftragBeispiel, AuftragAnnehmen> dialogLernen = new DialogLernenFactory<AuftragBeispiel, AuftragAnnehmen>()
                                            .AddSpeicherRam()
                                            .AddSpeicherDatei(dateiname)
                                            .AttributauswaehlerGainAbsolut()
                                            .Build();

      dialogLernen.BeispielHinzufuegen(LerndatenErsteller.ErstelleBeispielListe());

      dialogLernen.AusgabeLerndaten();
      dialogLernen.AusgabeBaumstruktur();
      dialogLernen.LerneBeispiele();
      dialogLernen.AusgabeBaumstruktur();


      // Eingeben auszuwertender Daten:
      AuftragEingabe auftragEingabe = new AuftragEingabe(-1, Bereich.OnlineShop, Aufwand.Gross, Attraktivitaet.Hoch, Bauchgefuehl.Gut);

      // gelerntes nutzen:
      IDialogAnwenden<AuftragEingabe, AuftragAnnehmen> dialogAnwenden = new DialogAnwendenFactory<AuftragEingabe, AuftragAnnehmen>()
                                      .AddSpeicherRam()
                                      .AddSpeicherDatei(dateiname)
                                      .Build();

      AuftragAnnehmen ergebnis = dialogAnwenden.EingabeAuswerten(auftragEingabe);
      Console.WriteLine($"\nErgebniss: {ergebnis}");

      ergebnis = dialogAnwenden.Abfragen();
      Console.WriteLine($"\nErgebniss: {ergebnis}");

      // Warten auf ENTER
      Console.WriteLine("\n[ENTER] beendet...");
      Console.ReadLine();

    }
  }
}

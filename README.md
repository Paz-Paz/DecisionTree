# DecisionTree
Übung zum Entscheidungsbaum-Lernen, Entstanden aus meinem aktuellen Uni-Kurs.

![GitHub](https://img.shields.io/github/license/Paz-Paz/DecisionTree)
![GitHub Workflow Status (branch)](https://img.shields.io/github/workflow/status/Paz-Paz/DecisionTree/.NET/main)
![GitHub last commit](https://img.shields.io/github/last-commit/Paz-Paz/DecisionTree)

## Inhaltsverzeichniss
* [Zusammenfassung](#zusammenfassung)
* [Liste an Eigenschaften](#liste-an-eigenschaften)
* [Ideen die noch offen sind](#ideen-die-noch-offen-sind)
* [Anwendungsbeispiel](#anwendungsbeispiel)

## Zusammenfassung:
Einfache C#-Bibliothek welche eine Liste entgegen nimmt und daraus einen Entscheidungsbaum baut.

## Liste an Eigenschaften:
- Es ist möglich einen erzeuten Baum in eine Datei zu speichern und ihn später wieder aufzurufen.
- Aktuell ist noch keinerlei User-Eingabe möglich, sondern es wird in den Beispielen alles "Hardcoded" übergeben.
- Es werden nur Enums als Eigenschaft der "Lern-Klasse" erkannt. Text und Zahlen werden (noch?) nicht unterstützt.

## Ideen die noch offen sind:
Wobei hier aktuell einfach nicht genug Zeit ist.
- Schnittstelle nach außen verbessern (Statt den aktuell Interfaces + Factory's nur noch 2 (Lernen, Nutzen))
- ILogger hinzufügen. (Erzeugt aber zusätzliche Paket-Abhängigkeiten (`Microsoft.Extensions.Logging`)
- Dependenci Injection hinzufügen (Erzeugt aber zusätzliche Abhängigkeiten (`Microsoft.Extensions.DependencyInjection`))
- Funktion zum Hinzufügen von extern erzeugten Speicher-Methoden hinzufügen.
- Ein Beispuel umbauen, so dass CSV oder etwas ähnliches ausgelesen werden können.

## Anwendungsbeispiel:
Aus Entscheidungsbaum.Bsp2:
```
const string dateiname = @"baum.json";

// Anlernen:
IDialogLernen<AuftragBeispiel, AuftragAnnehmen> dialogLernen = new DialogLernenFactory<AuftragBeispiel, AuftragAnnehmen>()
                                      .AddSpeicherRam()
                                      .AddSpeicherDatei(dateiname)
                                      .AttributauswaehlerGainAbsolut()
                                      .Build();

dialogLernen.BeispielHinzufuegen(LerndatenErsteller.ErstelleBeispielListe());

dialogLernen.AusgabeLerndaten();
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
```

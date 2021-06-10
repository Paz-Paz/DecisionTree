# DecisionTree
 Übung zum Entscheidungsbaum-Lernen, Entstanden aus meinem aktuellen Uni-Kurs.

## Erste Tests mit Icons:
![GitHub](https://img.shields.io/github/license/Paz-Paz/DecisionTree)
![GitHub Workflow Status (branch)](https://img.shields.io/github/workflow/status/Paz-Paz/DecisionTree/.NET/main)
![GitHub last commit](https://img.shields.io/github/last-commit/Paz-Paz/DecisionTree)

![GitHub all releases](https://img.shields.io/github/downloads/Paz-Paz/DecisionTree/total)
![GitHub issues](https://img.shields.io/github/issues-raw/Paz-Paz/DecisionTree)
![GitHub closed issues](https://img.shields.io/github/issues-closed-raw/Paz-Paz/DecisionTree)
![GitHub pull requests](https://img.shields.io/github/issues-pr-raw/Paz-Paz/DecisionTree)
![GitHub closed pull requests](https://img.shields.io/github/issues-pr-closed-raw/Paz-Paz/DecisionTree)
![GitHub release (latest by date)](https://img.shields.io/github/v/release/Paz-Paz/DecisionTree)
![GitHub commit activity](https://img.shields.io/github/commit-activity/m/Paz-Paz/DecisionTree)

## Zusammenfassung:
Einfaches C#-Programm welches eine Liste entgegen nimmt und daraus einen Entscheidungsbaum baut.

## Liste an Eigenschaften:
- Es ist möglich einen erzeuten Baum in eine Datei zu speichern und ihn später wieder aufzurufen.
- Aktuell ist noch keinerlei User-Eingabe möglich, sondern es wird in den Beispielen alles "Hardcoded" übergeben.
- Es werden nur Enums als Eigenschaft der "Lern-Klasse" erkannt. Text und Zahlen werden (noch?) nicht unterstützt.

## Ideen die noch offen sind:
Wobei hier aktuell einfach nicht genug Zeit ist.
- Schnittstelle nach außen verbessern (Statt den aktuell Interfaces + Factory's nur noch 2 (Lernen, Nutzen))
- ILogger hinzufügen. (Erzeugt aber zusätzliche Paket-Abhängigkeiten (`Microsoft.Extensions.Logging`)
- Dependenci Injection hinzufügen (Erzeugt aber zusätzliche Abhängigkeiten (`Microsoft.Extensions.DependencyInjection`))
- Hinzufügen von extern erzeugen Speicher-Objekten.
- Ein Beispuel umbauen, so dass CSV oder etwas ähnliches ausgelesen werden können.

## Anwendungsbeispiel:
### Entscheidungsbaum.Bsp2:
```
IHelper helper = new HelperFactory().Build();

// Lerndaten erstellen:
List<AuftragBeispiel> liste = LerndatenErsteller.ErstelleBeispielListe();
helper.GibBeispiellisteAufKonsoleAus<AuftragBeispiel, AuftragAnnehmen>(liste);

// Lernen:
ILernAlgoritmus<AuftragBeispiel, AuftragAnnehmen> lernAlgoritmus = new LernAlgorithmusFactory<AuftragBeispiel, AuftragAnnehmen>()
                                                          .AttributauswGainAbsolut()
                                                          .SpeicherRam()
                                                          .SpeicherDatei(@"tree.json", true)
                                                          .Build();
IWissensspeicher wissensspeicher = lernAlgoritmus.Lerne(liste);

helper.GibBaumAus(wissensspeicher.LadeBaum());

// Eingeben auszuwertender Daten:
AuftragEingabe auftragEingabe = new AuftragEingabe(-1, Bereich.OnlineShop, Aufwand.Gross, Attraktivitaet.Hoch, Bauchgefuehl.Gut);

// Daten auswerten u. ausgeben:
IDialogKomponente dialogKomponente = new DialogKomponenteFactory().Build();
AuftragAnnehmen ergebniss = dialogKomponente.Abfragen<AuftragEingabe, AuftragAnnehmen>(wissensspeicher.LadeBaum(), auftragEingabe);
Console.WriteLine($"\nErgebniss: {ergebniss}");
```

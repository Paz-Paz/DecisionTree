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
- Man kann einen Dialog starten in welchem dem Benutzer ein paar Fragen gestellt werden, und anhand der Antworten wird dann ein Ergebnis bestimmt. Wobei die weiteren Fragen anhand der vorherigen Antworten ausgewählt werden.
- Man kann eine Liste an Eigenschaften übergeben, die dann verwendet wird, statt allen Eigenschaften der Beispiel-Klasse.
- Es ist möglich einen erzeuten Baum in eine Datei zu speichern und ihn später wieder aufzurufen.
- Es werden nur Enums als Eigenschaft der "Lern-Klasse" erkannt. Text und Zahlen werden (noch?) nicht unterstützt.

## Ideen die noch offen sind:
Wobei hier aktuell einfach nicht genug Zeit ist.
- ILogger hinzufügen. (Erzeugt aber zusätzliche Paket-Abhängigkeiten (`Microsoft.Extensions.Logging`)
- Dependenci Injection hinzufügen (Erzeugt aber zusätzliche Abhängigkeiten (`Microsoft.Extensions.DependencyInjection`))
- Funktion zum Hinzufügen von extern erzeugten Speicher-Methoden hinzufügen.
- Ein Beispuel umbauen, so dass CSV oder etwas ähnliches ausgelesen werden können.
- Es soll möglich sein, den erstellten Baum direkt vom "Lerndialog" zum "AnwendDialog" weiterzugeben ohne ihn dazwischen auf die Platte zu speichern. (Ist mit dem aktuellen Update leider raus geflogen.)

## Bekannte Bugs:
- Wenn die Liste der ausgewählten Eigenschaften so sehr eingeschränkt wird, dass kein eindeutiges Ergebnis zustande kommt, wird noch eine Null-Referenz-Exception geworfen.

## Anwendungsbeispiel:
Aus Entscheidungsbaum.Bsp2:
```
const string dateiname = "baum.json";

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
AuftragEingabe auftragEingabe = new AuftragEingabe(Bereich.OnlineShop, Aufwand.Gross, Attraktivitaet.Hoch, Bauchgefuehl.Gut);

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

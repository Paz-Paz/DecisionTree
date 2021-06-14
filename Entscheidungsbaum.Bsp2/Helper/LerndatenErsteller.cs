using Entscheidungsbaum.Bsp2.Model;
using System.Collections.Generic;

namespace Entscheidungsbaum.Bsp2.Helper
{
  internal static class LerndatenErsteller
  {

    internal static List<AuftragBeispiel> ErstelleBeispielListe()
    {
      List<AuftragBeispiel> liste = new List<AuftragBeispiel>()
      {
        new AuftragBeispiel(1, Bereich.Handwerker, Aufwand.Gross, Attraktivitaet.Gering, Bauchgefuehl.Gut, AuftragAnnehmen.Nein),
        new AuftragBeispiel(2, Bereich.Handwerker, Aufwand.Gering, Attraktivitaet.Gering, Bauchgefuehl.Neutral, AuftragAnnehmen.Nein),
        new AuftragBeispiel(3, Bereich.Handwerker, Aufwand.Mittel, Attraktivitaet.Mittel, Bauchgefuehl.Gut, AuftragAnnehmen.Ja),
        new AuftragBeispiel(4, Bereich.Handwerker, Aufwand.Mittel, Attraktivitaet.Mittel, Bauchgefuehl.Schlecht, AuftragAnnehmen.Nein),
        new AuftragBeispiel(5, Bereich.Beratunsnetz, Aufwand.Mittel, Attraktivitaet.Hoch, Bauchgefuehl.Neutral, AuftragAnnehmen.Ja),
        new AuftragBeispiel(6, Bereich.Beratunsnetz, Aufwand.Gering, Attraktivitaet.Mittel, Bauchgefuehl.Neutral, AuftragAnnehmen.Nein),
        new AuftragBeispiel(7, Bereich.Beratunsnetz, Aufwand.Gross, Attraktivitaet.Mittel, Bauchgefuehl.Schlecht, AuftragAnnehmen.Ja),
        new AuftragBeispiel(8, Bereich.Beratunsnetz, Aufwand.Mittel, Attraktivitaet.Gering, Bauchgefuehl.Gut, AuftragAnnehmen.Ja),
        new AuftragBeispiel(9, Bereich.OnlineShop, Aufwand.Gross, Attraktivitaet.Hoch, Bauchgefuehl.Schlecht, AuftragAnnehmen.Nein),
        new AuftragBeispiel(10, Bereich.OnlineShop, Aufwand.Mittel, Attraktivitaet.Mittel, Bauchgefuehl.Schlecht, AuftragAnnehmen.Nein),
        new AuftragBeispiel(11, Bereich.OnlineShop, Aufwand.Mittel, Attraktivitaet.Gering, Bauchgefuehl.Gut, AuftragAnnehmen.Ja),
        new AuftragBeispiel(12, Bereich.OnlineShop, Aufwand.Gross, Attraktivitaet.Hoch, Bauchgefuehl.Gut, AuftragAnnehmen.Ja),
      };
      return liste;
    }

  }
}

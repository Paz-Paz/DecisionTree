using Entscheidungsbaum.Bsp1.Models;
using System.Collections.Generic;

namespace Entscheidungsbaum.Bsp1.Helper
{
  #region STATIC CLASS LerndatenErsteller ................................................................................

  internal static class LerndatenErsteller
  {
    #region Oeffentliche Methoden ..........................................................................................

    internal static List<KinobesuchBeispiel> ErstelleLerndaten()
    {
      List<KinobesuchBeispiel> liste = new List<KinobesuchBeispiel>()
      {
        new KinobesuchBeispiel(Attraktiv.Hoch, Preis.Hoch, Loge.Ja, Wetter.Schlecht, Warten.Ja, Besetzung.Top, Kategorie.AC, Land.International, Reserviert.Ja, Gruppe.Freunde, Ergebniss.Ja),
        new KinobesuchBeispiel(Attraktiv.Mittel, Preis.Mittel, Loge.Ja, Wetter.Mittel, Warten.Nein, Besetzung.Mittel, Kategorie.KO, Land.International, Reserviert.Nein, Gruppe.Paar, Ergebniss.Ja),
        new KinobesuchBeispiel(Attraktiv.Mittel, Preis.Mittel, Loge.Nein, Wetter.Mittel, Warten.Ja, Besetzung.Mittel, Kategorie.DR, Land.International, Reserviert.Nein, Gruppe.Freunde, Ergebniss.Nein),
        new KinobesuchBeispiel(Attraktiv.Gering, Preis.Mittel, Loge.Ja, Wetter.Mittel, Warten.Ja, Besetzung.Mittel, Kategorie.SF, Land.International, Reserviert.Nein, Gruppe.Allein, Ergebniss.Nein),
        new KinobesuchBeispiel(Attraktiv.Mittel, Preis.Mittel, Loge.Ja, Wetter.Mittel, Warten.Nein, Besetzung.Mittel, Kategorie.DR, Land.International, Reserviert.Nein, Gruppe.Paar, Ergebniss.Ja),
        new KinobesuchBeispiel(Attraktiv.Hoch, Preis.Hoch, Loge.Ja, Wetter.Schoen, Warten.Nein, Besetzung.Top, Kategorie.SF, Land.International, Reserviert.Ja, Gruppe.Freunde, Ergebniss.Ja),
        new KinobesuchBeispiel(Attraktiv.Mittel, Preis.Mittel, Loge.Ja, Wetter.Schlecht, Warten.Nein, Besetzung.Mittel, Kategorie.KO, Land.National, Reserviert.Nein, Gruppe.Freunde, Ergebniss.Ja),
        new KinobesuchBeispiel(Attraktiv.Mittel, Preis.Mittel, Loge.Nein, Wetter.Schlecht, Warten.Ja, Besetzung.Mittel, Kategorie.AC, Land.International, Reserviert.Nein, Gruppe.Freunde, Ergebniss.Ja),
        new KinobesuchBeispiel(Attraktiv.Gering, Preis.Mittel, Loge.Ja, Wetter.Schoen, Warten.Nein, Besetzung.Mittel, Kategorie.KO, Land.National, Reserviert.Nein, Gruppe.Freunde, Ergebniss.Nein),
        new KinobesuchBeispiel(Attraktiv.Mittel, Preis.Mittel, Loge.Ja, Wetter.Schoen, Warten.Nein, Besetzung.Mittel, Kategorie.KO, Land.International, Reserviert.Nein, Gruppe.Paar, Ergebniss.Nein),
        new KinobesuchBeispiel(Attraktiv.Hoch, Preis.Mittel, Loge.Ja, Wetter.Mittel, Warten.Ja, Besetzung.Top, Kategorie.DR, Land.International, Reserviert.Nein, Gruppe.Paar, Ergebniss.Ja),
        new KinobesuchBeispiel(Attraktiv.Mittel, Preis.Mittel, Loge.Nein, Wetter.Schlecht, Warten.Ja, Besetzung.Mittel, Kategorie.AC, Land.National, Reserviert.Nein, Gruppe.Allein, Ergebniss.Nein),
        new KinobesuchBeispiel(Attraktiv.Hoch, Preis.Hoch, Loge.Ja, Wetter.Mittel, Warten.Ja, Besetzung.Mittel, Kategorie.SF, Land.International, Reserviert.Nein, Gruppe.Allein, Ergebniss.Nein),
        new KinobesuchBeispiel(Attraktiv.Mittel, Preis.Mittel, Loge.Ja, Wetter.Schoen, Warten.Ja, Besetzung.Top, Kategorie.DR, Land.International, Reserviert.Ja, Gruppe.Freunde, Ergebniss.Nein),
        new KinobesuchBeispiel(Attraktiv.Mittel, Preis.Mittel, Loge.Ja, Wetter.Schlecht, Warten.Nein, Besetzung.Mittel, Kategorie.AC, Land.International, Reserviert.Nein, Gruppe.Paar, Ergebniss.Ja),

        //new KinobesuchBeispiel(Attraktiv.Gering, Preis.Hoch, Loge.Nein, Wetter.Schoen, Warten.Ja, Besetzung.Mittel, Kategorie.AC, Land.National, Reserviert.Nein, Gruppe.Paar, Ergebniss.Nein),
        //new KinobesuchBeispiel(Attraktiv.Gering, Preis.Hoch, Loge.Nein, Wetter.Schoen, Warten.Ja, Besetzung.Mittel, Kategorie.AC, Land.National, Reserviert.Vielleicht, Gruppe.Paar, Ergebniss.Nein),
      };

      return liste;
    }

    #endregion .............................................................................................................
  }

  #endregion .............................................................................................................
}

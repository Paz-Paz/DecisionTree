namespace Entscheidungsbaum.Bsp1.Models
{
  #region CLASS PreisAnpasserTest ........................................................................................

  public class KinobesuchBeispiel : KinobesuchEingabe
  {
    #region Konstruktor ....................................................................................................

    internal KinobesuchBeispiel() { }

    public KinobesuchBeispiel(Attraktiv attraktivitaet, Preis preis, Loge loge, Wetter wetter, Warten warten, Besetzung besetzung, Kategorie kategorie, Land land, Reserviert reserviert, Gruppe gruppe, Ergebniss kinobesuch)
    : base(attraktivitaet, preis, loge, wetter, warten, besetzung, kategorie, land, reserviert, gruppe)
    {
      this.Ergebniss = kinobesuch;
    }

    #endregion .............................................................................................................
    #region Getter/Setter ..................................................................................................

    public Ergebniss Ergebniss { get; }

    #endregion .............................................................................................................
  }
  #endregion .............................................................................................................

  public enum Ergebniss
  {
    Ja,
    Nein,
  }
}

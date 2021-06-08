namespace Entscheidungsbaum.Bsp1.Models
{
  #region CLASS PreisAnpasserTest ........................................................................................

  public class KinobesuchEingabe
  {
    #region Konstruktor ....................................................................................................

    internal KinobesuchEingabe()
    {
      /* nothing */
    }

    public KinobesuchEingabe(Attraktiv attraktivitaet, Preis preis, Loge loge, Wetter wetter, Warten warten, Besetzung besetzung, Kategorie kategorie, Land land, Reserviert reserviert, Gruppe gruppe)
    {
      this.Attraktiv = attraktivitaet;
      this.Preis = preis;
      this.Loge = loge;
      this.Wetter = wetter;
      this.Warten = warten;
      this.Besetzung = besetzung;
      this.Kategorie = kategorie;
      this.Land = land;
      this.Reserviert = reserviert;
      this.Gruppe = gruppe;
    }

    #endregion .............................................................................................................
    #region Getter/Setter ..................................................................................................

    public Attraktiv Attraktiv { get; }
    public Preis Preis { get; }

    public Loge Loge { get; set; }
    public Wetter Wetter { get; }
    public Warten Warten { get; }
    public Besetzung Besetzung { get; }
    public Kategorie Kategorie { get; }
    public Land Land { get; }
    public Reserviert Reserviert { get; }
    public Gruppe Gruppe { get; }

    #endregion .............................................................................................................
  }
  #endregion .............................................................................................................
  #region Enums ..........................................................................................................

  public enum Attraktiv
  {
    Hoch,
    Mittel,
    Gering,
  }
  public enum Preis
  {
    Hoch,
    Mittel,
  }
  public enum Loge
  {
    Ja,
    Nein,
  }
  public enum Wetter
  {
    Schoen,
    Mittel,
    Schlecht,
  }
  public enum Warten
  {
    Ja,
    Nein,
  }
  public enum Besetzung
  {
    Top,
    Mittel,
  }
  public enum Kategorie
  {
    AC,
    KO,
    DR,
    SF,
  }
  public enum Land
  {
    International,
    National,
  }
  public enum Reserviert
  {
    Ja,
    Nein,
  }
  public enum Gruppe
  {
    Freunde,
    Paar,
    Allein,
  }

  public enum JaNein
  {
    Ja,
    Nein,
  }

  #endregion .............................................................................................................

}

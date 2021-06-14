namespace Entscheidungsbaum.Bsp2.Model
{
  public class AuftragEingabe
  {

    public AuftragEingabe(Bereich bereich, Aufwand aufwand, Attraktivitaet attraktivitaet, Bauchgefuehl bauchgefuehl)
      : this(-1, bereich, aufwand, attraktivitaet, bauchgefuehl)
    {
      /* nothing */
    }

    public AuftragEingabe(int kdnr, Bereich bereich, Aufwand aufwand, Attraktivitaet attraktivitaet, Bauchgefuehl bauchgefuehl)
    {
      this.KdNr = kdnr;
      this.Bereich = bereich;
      this.Aufwand = aufwand;
      this.Attraktivitaet = attraktivitaet;
      this.Bauchgefuehl = bauchgefuehl;
    }

    public int KdNr { get; }
    public Bereich Bereich { get; }
    public Aufwand Aufwand { get; }
    public Attraktivitaet Attraktivitaet { get; }
    public Bauchgefuehl Bauchgefuehl { get; }
  }

  public enum Bereich
  {
    Handwerker,
    Beratunsnetz,
    OnlineShop,
  }
  public enum Aufwand
  {
    Gross,
    Mittel,
    Gering,
  }
  public enum Attraktivitaet
  {
    Hoch,
    Mittel,
    Gering,
  }
  public enum Bauchgefuehl
  {
    Gut,
    Neutral,
    Schlecht,
  }
}

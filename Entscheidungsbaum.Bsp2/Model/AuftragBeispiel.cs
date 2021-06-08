namespace Entscheidungsbaum.Bsp2.Model
{
  public class AuftragBeispiel : AuftragEingabe
  {
    public AuftragBeispiel(int kdnr, Bereich bereich, Aufwand aufwand, Attraktivitaet attraktivitaet, Bauchgefuehl bauchgefuehl, AuftragAnnehmen ergebniss)
      : base(kdnr, bereich, aufwand, attraktivitaet, bauchgefuehl)
    {
      this.AuftrAnnehmen = ergebniss;
    }

    public AuftragAnnehmen AuftrAnnehmen { get; }
  }

  public enum AuftragAnnehmen
  {
    Ja,
    Nein,
    Vielleicht,
  }
}

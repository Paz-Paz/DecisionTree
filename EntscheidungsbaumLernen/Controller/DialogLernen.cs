using EntscheidungsbaumLernen.Helper;
using EntscheidungsbaumLernen.Interfaces;
using System;
using System.Collections.Generic;

namespace EntscheidungsbaumLernen.Controller
{
  #region CLASS DialogLernen .............................................................................................

  /// <summary>
  /// Klasse für die Kommunikation mit den Personen die das System anlernen sollen.
  /// </summary>
  /// <remarks>
  /// <br /><b>Versionen:</b><br />
  /// V1.0 14.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  internal class DialogLernen<TBsp, TResult> : IDialogLernen<TBsp, TResult> where TBsp : class where TResult : Enum
  {
    #region Eigenschaften ..................................................................................................

    private readonly List<TBsp> _beispielListe = new List<TBsp>();

    private readonly ILernAlgoritmus<TBsp, TResult> _lernAlgoritmus;

    private readonly IWissensspeicher _wissensspeicher;

    private bool _gelernt = false;

    #endregion .............................................................................................................
    #region Konstruktor ....................................................................................................

    internal DialogLernen(ILernAlgoritmus<TBsp, TResult> lernAlgoritmus, IWissensspeicher wissensspeicher)
    {
      this._lernAlgoritmus = lernAlgoritmus ?? throw new ArgumentNullException(nameof(lernAlgoritmus));
      this._wissensspeicher = wissensspeicher ?? throw new ArgumentNullException(nameof(wissensspeicher));
    }

    #endregion .............................................................................................................
    #region Getter/Setter ..................................................................................................

    /// <inheritdoc/>
    internal TResult DefaultWert { get; set; } = default(TResult);

    internal List<Type> Attributliste { get; set; } = null;

    #endregion .............................................................................................................
    #region Oeffentliche Methoden ..........................................................................................

    /// <inheritdoc/>
    public void AusgabeBaumstruktur()
    {
      if (_gelernt)
      {
        Ausgeber.GibBaumAus(this._wissensspeicher.LadeBaum());
      }
      else
      {
        Console.WriteLine("Es kann noch keine Baumstruktur ausgegeben werden, da noch nichts gelernt wurde.");
      }
    }

    public void AusgabeLerndaten()
    {
      Ausgeber.GibBeispiellisteAufKonsoleAus<TBsp, TResult>(this._beispielListe);
    }

    /// <inheritdoc/>
    public void BeispielHinzufuegen(in IEnumerable<TBsp> beispiele)
    {
      this._beispielListe.AddRange(beispiele);
    }

    /// <inheritdoc/>
    public void BeispielHinzufuegen(in TBsp beispiel)
    {
      this._beispielListe.Add(beispiel);
    }

    /// <inheritdoc/>
    public List<Type> ErstelleListeAllerAttribute()
    {
      return KlassenHelper.ErstellAttributliste<TBsp, TResult>();
    }

    /// <inheritdoc/>
    public void LerneBeispiele()
    {
      this._wissensspeicher.SpeichereBaum(this._lernAlgoritmus.Lerne(this._beispielListe));
      this._gelernt = true;
    }

    /// <inheritdoc/>
    public void LerneBeispiele(in List<Type> attributsliste)
    {
      this._wissensspeicher.SpeichereBaum(this._lernAlgoritmus.Lerne(this._beispielListe, attributsliste));
      this._gelernt = true;
    }

    /// <inheritdoc/>
    public void LerneBeispiele(in TResult defaultWert)
    {
      this._wissensspeicher.SpeichereBaum(this._lernAlgoritmus.Lerne(this._beispielListe, defaultWert));
      this._gelernt = true;
    }


    /// <inheritdoc/>
    public void LerneBeispiele(in List<Type> attributsliste, in TResult defaultWert)
    {
      this._wissensspeicher.SpeichereBaum(this._lernAlgoritmus.Lerne(this._beispielListe, attributsliste, defaultWert));
      this._gelernt = true;
    }

    #endregion .............................................................................................................
    #region Paket-Interne Methoden .........................................................................................
    #endregion .............................................................................................................
    #region Private Methoden ...............................................................................................
    #endregion .............................................................................................................
  }

  #endregion .............................................................................................................
}

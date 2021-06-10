using EntscheidungsbaumLernen.Controller;
using EntscheidungsbaumLernen.Interfaces;
using System;

namespace EntscheidungsbaumLernen.Factorys
{
  #region CLASS LernAlgorithmusFactory ...................................................................................

  /// <summary>
  /// Factory zum Erzeugen der Klasse des Lern-Algorithmus.
  /// </summary>
  /// <remarks>
  /// <br /><b>Versionen:</b><br />
  /// V1.0 06.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  public class LernAlgorithmusFactory<TBsp, TResult> where TBsp : class where TResult : Enum
  {
    #region Eigenschaften ..................................................................................................

    /// <summary>
    /// Zu verwendender Attributs-Auswähler.
    /// </summary>
    private IAttributAuswaehler<TBsp, TResult> _attributAuswaehler = null;

    /// <summary>
    /// Factory für den Wissensspeicher.
    /// </summary>
    private WissensspeicherFactory _wissensspeicherFactory = new WissensspeicherFactory();

    #endregion .............................................................................................................
    #region Oeffentliche Methoden ..........................................................................................

    /// <summary>
    /// Erzeugt ein <see cref="ILernAlgoritmus{TBsp, TResult}"/>-Objekt aus den zuvor gemachten Einstellungen und gibt es zurück.
    /// </summary>
    /// <returns>Erzeugtes Objekt</returns>
    public ILernAlgoritmus<TBsp, TResult> Build()
    {
      IWissensspeicherImpl wissensspeicher = this._wissensspeicherFactory.BuildImpl();
      IAttributAuswaehler<TBsp, TResult> attributAuswaehler = this._attributAuswaehler ?? new AttrAuswaehlerGainAbsolut<TBsp, TResult>();
      ILernAlgoritmus<TBsp, TResult> lernAlgoritmus = new LernAlgorithmusDT<TBsp, TResult>(attributAuswaehler, wissensspeicher);
      return lernAlgoritmus;
    }

    /// <summary>
    /// Setzt als Attributauswähler einen, der prinzipiell immer das erste Attribut dass er findet verwendet.
    /// </summary>
    /// <remarks>
    /// Sehr Laufzeit-Freundlich, dafür (sehr) schlechte Auswahl.
    /// </remarks>
    /// <returns>Die Factory um weitere Einstellungen vornehmen zu können.</returns>
    public LernAlgorithmusFactory<TBsp, TResult> AttributauswErstesAttribut()
    {
      if (this._attributAuswaehler != null)
      {
        Console.WriteLine($"WARNUNG - Attributauswähler '{this._attributAuswaehler.GetType().Name}' wird von '{typeof(AttrAuswaehlerErstesAttribut<TBsp, TResult>).Name}' überschrieben!");
      }
      this._attributAuswaehler = new AttrAuswaehlerErstesAttribut<TBsp, TResult>();
      return this;
    }

    /// <summary>
    /// Setzt als Attributauswähler einen, der prinzipiell immer das erste Attribut dass er findet verwendet.
    /// </summary>
    /// <remarks>
    /// Sehr Laufzeit-Freundlich, dafür (sehr) schlechte Auswahl.
    /// </remarks>
    /// <returns>Die Factory um weitere Einstellungen vornehmen zu können.</returns>
    public LernAlgorithmusFactory<TBsp, TResult> AttributauswGainAbsolut()
    {
      if (this._attributAuswaehler != null)
      {
        Console.WriteLine($"WARNUNG - Attributauswähler '{this._attributAuswaehler.GetType().Name}' wird von '{typeof(AttrAuswaehlerGainAbsolut<TBsp, TResult>).Name}' überschrieben!");
      }
      this._attributAuswaehler = new AttrAuswaehlerGainAbsolut<TBsp, TResult>();
      return this;
    }

    /// <inheritdoc cref="WissensspeicherFactory.Clear"/>
    public LernAlgorithmusFactory<TBsp, TResult> SpeicherClear()
    {
      this._wissensspeicherFactory.Clear();
      return this;
    }

    /// <inheritdoc cref="WissensspeicherFactory.AddRamSpeicher"/>
    public LernAlgorithmusFactory<TBsp, TResult> SpeicherRam()
    {
      this._wissensspeicherFactory.AddRamSpeicher();
      return this;
    }

    /// <inheritdoc cref="WissensspeicherFactory.AddDateiSpeicher(in string)"/>
    public LernAlgorithmusFactory<TBsp, TResult> SpeicherDatei(in string pfad)
    {
      this._wissensspeicherFactory.AddDateiSpeicher<TResult>(pfad);
      return this;
    }

    #endregion .............................................................................................................
  }

  #endregion .............................................................................................................
}

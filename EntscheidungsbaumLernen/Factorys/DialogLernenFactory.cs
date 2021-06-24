using EntscheidungsbaumLernen.Controller;
using EntscheidungsbaumLernen.Interfaces;
using System;

namespace EntscheidungsbaumLernen.Factorys
{
  #region CLASS DialogLernenFactory ......................................................................................

  /// <summary>
  /// Klasse um ein Dialog-Objekt für einen "Anlerner" des Wissensbaumes zu erstellen.
  /// </summary>
  /// <remarks>
  /// <br /><b>Versionen:</b><br />
  /// V1.0 14.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  /// <typeparam name="TBsp">Typ der Klasse, welche als Beispiel verwendet wird.</typeparam>
  /// <typeparam name="TResult">Typ der Eigenschaft, die als Ergebnis verwendet wird.</typeparam>
  public class DialogLernenFactory<TBsp, TResult> where TBsp : class where TResult : Enum
  {
    #region Eigenschaften ..................................................................................................

    /// <summary>
    /// Factory für das Erstellen eines Wissensspeichers.
    /// </summary>
    private readonly WissensspeicherFactory _wissensspeicherFactory = new WissensspeicherFactory();

    /// <summary>
    /// Interface um das Objekt zu speichern, welches das nächste Attribut auswählt.
    /// </summary>
    private IAttributAuswaehler<TBsp, TResult> _attributAuswaehler = null;

    /// <summary>
    /// Defaultwert, wenn keine eindeutige Zuordnung möglich ist.
    /// </summary>
    private TResult _defaultWert = default(TResult);

    #endregion .............................................................................................................
    #region Oeffentliche Methoden ..........................................................................................


    /// <summary>
    /// Erstellt ein <see cref="IDialogLernen{TBsp, TResult}"/>-Objekt mit den zuvor gemachten Einstellungen.
    /// </summary>
    /// <returns>Erstelltes Objekt.</returns>
    public IDialogLernen<TBsp, TResult> Build()
    {
      IWissensspeicher wissensspeicher = this._wissensspeicherFactory.Build();
      ILernAlgoritmus<TBsp, TResult> lernAlgoritmus = new LernAlgorithmusDT<TBsp, TResult>(this._attributAuswaehler ?? new AttrAuswaehlerGainAbsolut<TBsp, TResult>());

      DialogLernen<TBsp, TResult> dialogLernen = new DialogLernen<TBsp, TResult>(lernAlgoritmus, wissensspeicher);
      dialogLernen.DefaultWert = this._defaultWert;

      return dialogLernen;

    }

    /// <summary>
    /// Fügt eine Datei zum Speichern des Entscheidungsbaumes hinzu.
    /// </summary>
    /// <param name="dateiname">(Vollständiger) Dateiname im System.</param>
    /// <returns><see cref="DialogLernenFactory{TBsp, TResult}"/>, um weitere Einstellungen vornehmen zu können.</returns>
    public DialogLernenFactory<TBsp, TResult> AddSpeicherDatei(in string dateiname)
    {
      this._wissensspeicherFactory.AddDateiSpeicher<TResult>(dateiname);
      return this;
    }

    /// <summary>
    /// Fügt den "RAM" als zu speichernden Speicher hinzu.
    /// </summary>
    /// <returns><see cref="DialogLernenFactory{TBsp, TResult}"/>, um weitere Einstellungen vornehmen zu können.</returns>
    public DialogLernenFactory<TBsp, TResult> AddSpeicherRam()
    {
      this._wissensspeicherFactory.AddRamSpeicher();
      return this;
    }

    /// <summary>
    /// Löscht alle aktuell hinterlegten Speicher-Methoden.
    /// </summary>
    /// <remarks>
    /// Falls nach dem Löschen, kein anderer Speicher mehr hinzugefüt wird, wird intern der <see cref="AddSpeicherRam"/> aufgerufen, um überhaupt etwas zu haben.
    /// </remarks>
    /// <returns><see cref="DialogLernenFactory{TBsp, TResult}"/>, um weitere Einstellungen vornehmen zu können.</returns>
    public DialogLernenFactory<TBsp, TResult> ClearSpeicher()
    {
      this._wissensspeicherFactory.Clear();
      return this;
    }

    /// <summary>
    /// Setzt einen Wert, welcher verwendet wird, wenn einem Blatt im Entscheidungsbaum kein Wert zugeordnet werden kann. (Default: '<i>default(TResult)</i>')
    /// </summary>
    /// <remarks>
    /// <i>Beispiel</i>: Ein Element hat 2 Kinder, das gewählte Attribut aber 3 Eigenschaften.
    /// </remarks>
    /// <param name="defaultWert">Wert, welcher übernommen werden soll.</param>
    /// <returns><see cref="DialogLernenFactory{TBsp, TResult}"/>, um weitere Einstellungen vornehmen zu können.</returns>
    public DialogLernenFactory<TBsp, TResult> SetDefaultWert(TResult defaultWert)
    {
      this._defaultWert = defaultWert;
      return this;
    }

    /// <summary>
    /// Nutzt einen Attribut-Auswähler, welcher immer das erste zur Verfügung stehende Attribut zurückgibt.
    /// </summary>
    /// <remarks>
    /// Arbeitet schnell, liefert aber schlechtn Baum ;)
    /// </remarks>
    /// <returns><see cref="DialogLernenFactory{TBsp, TResult}"/>, um weitere Einstellungen vornehmen zu können.</returns>
    public DialogLernenFactory<TBsp, TResult> UseAttributauswaehlerErstesAttribut()
    {
      this._attributAuswaehler = new AttrAuswaehlerErstesAttribut<TBsp, TResult>();
      return this;
    }

    /// <summary>
    /// Nutzt einen Attribut-Auswähler, welcher ein Attribut nach dem "Gain ration - absolut" Verfahren auswählt.
    /// </summary>
    /// <remarks>
    /// Bei Eigenschaften, die immer nur 1x belegt sind, z.B Kundennummern ist dieser Algorithmus nicht sehr gut.
    /// </remarks>
    /// <returns><see cref="DialogLernenFactory{TBsp, TResult}"/>, um weitere Einstellungen vornehmen zu können.</returns>
    public DialogLernenFactory<TBsp, TResult> UseAttributauswaehlerGainAbsolut()
    {
      this._attributAuswaehler = new AttrAuswaehlerGainAbsolut<TBsp, TResult>();
      return this;
    }

    #endregion .............................................................................................................
  }

  #endregion .............................................................................................................
}

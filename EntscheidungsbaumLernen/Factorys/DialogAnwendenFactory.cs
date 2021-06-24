using EntscheidungsbaumLernen.Controller;
using EntscheidungsbaumLernen.Interfaces;
using System;

namespace EntscheidungsbaumLernen.Factorys
{
  #region CLASS DialogAnwendenFactory ....................................................................................

  /// <summary>
  /// Klasse um ein Dialog-Objekt für einen "Anwender" des Wissensbaumes zu erstellen.
  /// </summary>
  /// <remarks>
  /// <br /><b>Versionen:</b><br />
  /// V1.0 14.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  /// <typeparam name="TEingabe">Typ der vom Anwender genutzten Klasse für die Eingabe von Daten.</typeparam>
  /// <typeparam name="TResult">Typ der Eigenschaft, die als Ergebnis verwendet wird.</typeparam>
  public class DialogAnwendenFactory<TEingabe, TResult> where TEingabe : class where TResult : Enum
  {
    #region Eigenschaften ..................................................................................................

    /// <summary>
    /// Factory für das Erstellen eines Wissensspeichers.
    /// </summary>
    private readonly WissensspeicherFactory _wissensspeicherFactory = new WissensspeicherFactory();

    #endregion .............................................................................................................
    #region Oeffentliche Methoden ..........................................................................................

    /// <summary>
    /// Erstellt ein <see cref="IDialogAnwenden{TEingabe, TResult}"/>-Objekt mit den zuvor gemachten Einstellungen.
    /// </summary>
    /// <returns>Erstelltes Objekt.</returns>
    public IDialogAnwenden<TEingabe, TResult> Build()
    {
      return new DialogAnwenden<TEingabe, TResult>(this._wissensspeicherFactory.Build());
    }

    /// <summary>
    /// Fügt eine Datei zum Auslesen des Entscheidungsbaumes hinzu.
    /// </summary>
    /// <param name="dateiname">(Vollständiger) Dateiname im System.</param>
    /// <returns><see cref="DialogAnwendenFactory{TEingabe, TResult}"/>, um weitere Einstellungen vornehmen zu können.</returns>
    public DialogAnwendenFactory<TEingabe, TResult> AddSpeicherDatei(in string dateiname)
    {
      this._wissensspeicherFactory.AddDateiSpeicher<TResult>(dateiname);
      return this;
    }

    /// <summary>
    /// Fügt den "RAM" als auszulesenden Speicher hinzu.
    /// </summary>
    /// <returns><see cref="DialogAnwendenFactory{TEingabe, TResult}"/>, um weitere Einstellungen vornehmen zu können.</returns>
    public DialogAnwendenFactory<TEingabe, TResult> AddSpeicherRam()
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
    /// <returns><see cref="DialogAnwendenFactory{TEingabe, TResult}"/>, um weitere Einstellungen vornehmen zu können.</returns>
    public DialogAnwendenFactory<TEingabe, TResult> ClearSpeicher()
    {
      this._wissensspeicherFactory.Clear();
      return this;
    }

    #endregion .............................................................................................................
  }

  #endregion .............................................................................................................
}

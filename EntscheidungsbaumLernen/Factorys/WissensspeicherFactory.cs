using EntscheidungsbaumLernen.Controller;
using EntscheidungsbaumLernen.Interfaces;
using System;

namespace EntscheidungsbaumLernen.Factorys
{
  #region CLASS WissensspeicherFactory ...................................................................................

  /// <summary>
  /// Factory zum Erzeugen der Klasse des Wissensspeichers
  /// </summary>
  /// <remarks>
  /// <br /><b>Versionen:</b><br />
  /// V1.0 06.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  public class WissensspeicherFactory
  {
    #region Eigenschaften ..................................................................................................

    /// <summary>
    /// Wissensspeicher an sich.
    /// </summary>
    private IWissensspeicherImpl _response = null;

    #endregion .............................................................................................................
    #region Oeffentliche Methoden ..........................................................................................

    /// <summary>
    /// Erzeugt ein <see cref="IWissensspeicher"/>-Objekt aus den zuvor gemachten Einstellungen und gibt es zurück.
    /// </summary>
    /// <returns>Erzeugtes Objekt</returns>
    public IWissensspeicher Build()
    {
      return this.BuildImpl();
    }

    /// <summary>
    /// Entfernt alle bisher hinterlegten Wissensspeicher.
    /// </summary>
    /// <remarks>
    /// Falls keiner mehr hinzugefügt wird, wir der "RAM" als Default trotzdem verwendet.
    /// </remarks>
    /// <returns>Die Factory um weitere Einstellungen vornehmen zu können.</returns>
    public WissensspeicherFactory Clear()
    {
      this._response = null;
      return this;
    }

    /// <summary>
    /// Fügt einen Speicher hinzu, welcher die Daten (nur) im RAM ablegt und nirgends persistent speichert.
    /// </summary>
    /// <returns>Die Factory um weitere Einstellungen vornehmen zu können.</returns>
    public WissensspeicherFactory AddRamSpeicher()
    {
      this.AddSpeicher(new WissensspeicherRam());
      return this;
    }

    /// <summary>
    /// Fügt einen Speicher hinzu, welcher aus Datein lesen und in sie schreiben kann.
    /// </summary>
    /// <param name="pfad">Pfad zur Datei. Falls die Datei nicht existiert wird versucht sie beim ersten Schreiben anzulegen.</param>
    /// <param name="speichereLeserlich">Wenn true wird die gespeicherte JSON-Datei 'schön' gespeichert, bei false wird sie minimiert gespeichert.</param>
    /// <returns>Die Factory um weitere Einstellungen vornehmen zu können.</returns>
    /// <exception cref="NotImplementedException">Datei-Speicher wurde noch (nicht fertig) implementiert.</exception>
    public WissensspeicherFactory AddDateiSpeicher<TResult>(in string pfad, in bool speichereLeserlich = false) where TResult : Enum
    {
      this.AddSpeicher(new WissensspeicherDatei<TResult>(pfad, speichereLeserlich));
      return this;
    }

    #endregion .............................................................................................................
    #region Paket-Interne Methoden .........................................................................................

    /// <summary>
    /// Fügt einen beliebigen, auch extern erzeugen, Wissensspeicher hinzu.
    /// </summary>
    /// <remarks>
    /// Vorbereitet um in Zukunft das externe Erstellen von Wissensspeichern per <see cref="IWissensspeicherImpl"/> zu ermöglichen.
    /// </remarks>
    /// <param name="wissensspeicher">Hinzuzufügender Wissensspeicher.</param>
    /// <returns>Die Factory um weitere Einstellungen vornehmen zu können.</returns>
    internal WissensspeicherFactory AddEigenenSpeicher(in IWissensspeicherImpl wissensspeicher)
    {
      this.AddSpeicher(wissensspeicher);
      return this;
    }

    /// <summary>
    /// Erzeugt die interne Wissensspeicher-Implementierung, welche auch speichern kann.
    /// </summary>
    /// <returns>Erzeuges Objekt.</returns>
    internal IWissensspeicherImpl BuildImpl()
    {
      if (this._response == null)
      {
        return new WissensspeicherRam(); ;
      }

      return this._response;
    }

    #endregion .............................................................................................................
    #region Private Methoden ...............................................................................................

    /// <summary>
    /// Fügt den übergebenen Wissensspeicher hinzu.
    /// </summary>
    /// <param name="wissensspeicher"></param>
    private void AddSpeicher(IWissensspeicherImpl wissensspeicher)
    {
      if (this._response == null)
      {
        this._response = wissensspeicher;
      }
      else
      {
        IWissensspeicherImpl letzter = this._response;
        while (letzter.Next != null)
        {
          letzter = letzter.Next;
        }
        letzter.SetNaechsteInstanz(wissensspeicher);
      }
    }

    #endregion .............................................................................................................
  }

  #endregion .............................................................................................................
}

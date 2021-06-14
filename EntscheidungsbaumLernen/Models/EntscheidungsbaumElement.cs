using EntscheidungsbaumLernen.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("EntscheidungsbaumLernen.UnitTests")]
namespace EntscheidungsbaumLernen.Models
{
  #region CLASS EntscheidungsbaumElement .................................................................................

  /// <summary>
  /// Interne Repräsentation eines Entscheidungsbaum-Knotens.
  /// </summary>
  /// <typeparam name="TResult">Typ des Ergebnisses.</typeparam>
  /// <remarks>
  /// <br /><b>Versionen:</b><br />
  /// V1.0 06.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  internal class EntscheidungsbaumElement<TResult> : IEntscheidungsbaumWurzel where TResult : Enum
  {
    #region Eigenschaften ..................................................................................................

    /// <summary>
    /// Liste der Kinder.
    /// </summary>
    /// <remarks>
    /// Als Dictionary für direkten Zugriff.
    /// </remarks>
    private readonly Dictionary<string, object> _kinder = new Dictionary<string, object>();

    #endregion .............................................................................................................
    #region Konstruktor ....................................................................................................

    /// <summary>
    /// Liefert ein <see cref="EntscheidungsbaumElement{TResult}"/>-Objekt mit leeren Liste an Kindern.
    /// </summary>
    /// <param name="knotenTyp">Typ des Knotens. (Muss ein Enum sein)</param>
    /// <exception cref="ArgumentNullException">Wenn <i>null</i> übergeben wurde.</exception>
    /// <exception cref="ArgumentException">Wenn kein Enum übergeben wurde.</exception>
    internal EntscheidungsbaumElement(Type knotenTyp)
    {
      if (knotenTyp == null)
      {
        throw new ArgumentNullException(nameof(knotenTyp), "Übergebener Type darf nicht null sein.");
      }
      if (!knotenTyp.IsEnum)
      {
        throw new ArgumentException(nameof(knotenTyp), "Übergebener Type muss ein Enum sein.");
      }

      this.KnotenTyp = knotenTyp;
      Array attributArray = Enum.GetValues(knotenTyp);
      foreach (object objekt in attributArray)
      {
        this._kinder.Add(objekt.ToString(), null);
      }
    }

    #endregion .............................................................................................................
    #region Getter/Setter ..................................................................................................

    /// <inheritdoc/>
    public Type KnotenTyp { get; }

    #endregion .............................................................................................................
    #region Oeffentliche Methoden ..........................................................................................

    /// <inheritdoc/>
    public object GetKind(in string kategorie)
    {
      this.KeyPruefung(kategorie);
      return this._kinder[kategorie];
    }

    /// <inheritdoc/>
    public bool IstBlatt(in string kategorie)
    {
      this.KeyPruefung(kategorie);
      return this._kinder[kategorie].GetType() == typeof(TResult);
    }

    #endregion .............................................................................................................
    #region Paket-Interne Methoden .........................................................................................

    /// <summary>
    /// Setzt das übergebene <paramref name="entscheidungsbaumElement"/> als Kind mit dem Namen <paramref name="kategorie"/>.
    /// </summary>
    /// <param name="kategorie">Namen der Kategorie des Kindes. Muss einer der Werte des Enums <typeparamref name="TResult"/> sein.</param>
    /// <param name="entscheidungsbaumElement">Objekt, welches gesetzt werden soll.</param>
    /// <exception cref="ArgumentException">Wenn die übergebene <paramref name="kategorie"/> in <see cref="_kinder"/> nicht vorkommt.</exception>
    internal void SetKind(in string kategorie, in EntscheidungsbaumElement<TResult> entscheidungsbaumElement)
    {
      this.KeyPruefung(kategorie);
      this._kinder[kategorie] = entscheidungsbaumElement;
    }

    /// <summary>
    /// Setzte den übergebenen Wert von <paramref name="ergebnis"/> als Kind mit dem Namen <paramref name="kategorie"/>.
    /// </summary>
    /// <param name="kategorie">Namen der Kategorie des Kindes. Muss einer der Werte des Enums <typeparamref name="TResult"/> sein.</param>
    /// <param name="ergebnis">Objekt, welches gesetzt werden soll.</param>
    /// <exception cref="ArgumentException">Wenn die übergebene <paramref name="kategorie"/> in <see cref="_kinder"/> nicht vorkommt.</exception>
    internal void SetKind(in string kategorie, in TResult ergebnis)
    {
      this.KeyPruefung(kategorie);
      this._kinder[kategorie] = ergebnis;
    }

    #endregion .............................................................................................................
    #region Private Methoden ...............................................................................................

    /// <summary>
    /// Prüft ob die übegebene <paramref name="kategorie"/> ein gülter Key in <see cref="_kinder"/> ist.
    /// </summary>
    /// <param name="kategorie">Zu prüfende Kategorie.</param>
    /// <exception cref="ArgumentException">Wenn die übergebene <paramref name="kategorie"/> in <see cref="_kinder"/> nicht vorkommt.</exception>
    /// <exception cref="ArgumentException">Wenn die übergebene <paramref name="kategorie"/> in <see cref="_kinder"/> nicht vorkommt.</exception>
    private void KeyPruefung(in string kategorie)
    {
      if (!this._kinder.ContainsKey(kategorie))
      {
        throw new ArgumentException($"Kategorie '{kategorie} existiert nicht.");
      }
    }

    #endregion .............................................................................................................
  }

  #endregion .............................................................................................................
}

using EntscheidungsbaumLernen.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("EntscheidungsbaumLernen.UnitTests")]
namespace EntscheidungsbaumLernen.Models
{
  #region CLASS EntscheidungsbaumElement .................................................................................

  internal class EntscheidungsbaumElement<TResult> : IEntscheidungsbaumWurzel where TResult : Enum
  {
    #region Eigenschaften ..................................................................................................

    private readonly Dictionary<string, object> _kinder = new Dictionary<string, object>();

    #endregion .............................................................................................................
    #region Konstruktor ....................................................................................................

    internal EntscheidungsbaumElement(Type wurzeltyp)
    {
      if (wurzeltyp == null)
      {
        throw new ArgumentNullException(nameof(wurzeltyp), "Übergebener Type darf nicht null sein.");
      }
      if (!wurzeltyp.IsEnum)
      {
        throw new ArgumentException(nameof(wurzeltyp), "Übergebener Type muss ein Enum sein.");
      }

      this.Wureltyp = wurzeltyp;
      Array attributArray = Enum.GetValues(wurzeltyp);
      foreach (object objekt in attributArray)
      {
        this._kinder.Add(objekt.ToString(), null);
      }
    }

    #endregion .............................................................................................................
    #region Getter/Setter ..................................................................................................

    /// <inheritdoc/>
    public Type Wureltyp { get; }

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

    internal void SetKind(in string kategorie, in EntscheidungsbaumElement<TResult> entscheidungsbaumElement)
    {
      this.KeyPruefung(kategorie);
      this._kinder[kategorie] = entscheidungsbaumElement;
    }

    internal void SetKind(in string kategorie, in TResult kinobesuch)
    {
      this.KeyPruefung(kategorie);
      this._kinder[kategorie] = kinobesuch;
    }

    #endregion .............................................................................................................
    #region Private Methoden ...............................................................................................

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

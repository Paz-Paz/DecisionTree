using EntscheidungsbaumLernen.Helper;
using EntscheidungsbaumLernen.Interfaces;
using System;
using System.Reflection;

namespace EntscheidungsbaumLernen.Controller
{
  #region CLASS DialogKomponente .........................................................................................

  /// <summary>
  /// Komponente für den Dialog mit dem User.
  /// </summary>
  /// <remarks>
  /// Aktuell kann man nur ein "kompletes" Ergebnis-Objek übergeben werden.<br />
  /// <br /><b>Versionen:</b><br />
  /// V1.0 06.06.2021 - Paz-Paz - erstellt<br />
  /// </remarks>
  internal class DialogKomponente : IDialogKomponente
  {
    #region Oeffentliche Methoden ..........................................................................................

    /// <inheritdoc/>
    public TResult Abfragen<TEingabe, TResult>(in IEntscheidungsbaumWurzel entscheidungsbaumWurzel, in TEingabe eingabe) where TEingabe : class where TResult : Enum
    {
      Console.Write("\nAuszuwerten: ");
      bool first = true;
      foreach (PropertyInfo propertyInfo in eingabe.GetType().GetProperties())
      {
        if (!propertyInfo.PropertyType.IsEnum)
        {
          continue;
        }
        if (!first)
        {
          Console.Write(", ");
        }
        first = false;
        Console.Write($"{propertyInfo.Name}: {KlassenHelper.GetValue(eingabe, propertyInfo.PropertyType)}");
      }
      Console.WriteLine("\n");
      return this.Auswerten<TEingabe, TResult>(entscheidungsbaumWurzel, eingabe, 1);
    }

    #endregion .............................................................................................................
    #region Private Methoden ...............................................................................................

    /// <inheritdoc cref="IDialogKomponente.Abfragen{TEingabe, TResult}(in IEntscheidungsbaumWurzel, in TEingabe)"/>
    /// <param name="entscheidungsbaumWurzel"><inheritdoc cref="IDialogKomponente.Abfragen{TEingabe, TResult}(in IEntscheidungsbaumWurzel, in TEingabe)"/></param>
    /// <param name="eingabe"><inheritdoc cref="IDialogKomponente.Abfragen{TEingabe, TResult}(in IEntscheidungsbaumWurzel, in TEingabe)"/></param>
    /// <param name="zaehler">Nächste Nummer des auszugebenden Entscheidungsgrundes.</param>
    private TResult Auswerten<TEingabe, TResult>(in IEntscheidungsbaumWurzel entscheidungsbaumWurzel, in TEingabe eingabe, in int zaehler) where TEingabe : class where TResult : Enum
    {
      Type attribut = entscheidungsbaumWurzel.KnotenTyp;
      object wert = KlassenHelper.GetValue(eingabe, attribut);

      string kategorie = wert.ToString();
      object kind = entscheidungsbaumWurzel.GetKind(kategorie);

      Console.WriteLine($"{zaehler,3}. Auswahl: {attribut.Name}: {wert}");

      if (entscheidungsbaumWurzel.IstBlatt(kategorie))
      {
        return (TResult)kind;
      }

      return this.Auswerten<TEingabe, TResult>((IEntscheidungsbaumWurzel)kind, eingabe, zaehler + 1);
    }

    #endregion .............................................................................................................
  }

  #endregion .............................................................................................................
}

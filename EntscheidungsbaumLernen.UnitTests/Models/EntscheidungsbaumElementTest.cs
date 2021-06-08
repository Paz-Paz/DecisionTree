using EntscheidungsbaumLernen.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Models
{
  [TestClass]
  public class EntscheidungsbaumElementTest
  {
    private enum EigenschaftEnum { A, B }
    private enum ResultEnum { Z, Y }



    [TestMethod]
    public void ErstellenErfolgreichTest()
    {
      // vorbereiten & testen:
      EntscheidungsbaumElement<ResultEnum> element = new EntscheidungsbaumElement<ResultEnum>(typeof(EigenschaftEnum));
    }

    [TestMethod]
    public void ErstellteKategorienTest()
    {
      // vorbereiten:
      EntscheidungsbaumElement<ResultEnum> element = new EntscheidungsbaumElement<ResultEnum>(typeof(EigenschaftEnum));
      const string falscheKat = "falschekategorie";
      // ausführen & testen:
      element.GetKind(EigenschaftEnum.A.ToString());
      element.GetKind(EigenschaftEnum.B.ToString());
      ArgumentException exception = Assert.ThrowsException<ArgumentException>(() => element.GetKind(falscheKat));
      Assert.AreEqual<string>($"Kategorie '{falscheKat} existiert nicht.", exception.Message);
    }

    [TestMethod]
    public void ErstellenExceptionKeinEnum()
    {
      // vorbereiten & testen:
      ArgumentException exception = Assert.ThrowsException<ArgumentException>(() => new EntscheidungsbaumElement<ResultEnum>(typeof(string)));

      // auswerten:
      Assert.AreEqual<string>("wurzeltyp (Parameter 'Übergebener Type muss ein Enum sein.')", exception.Message);
    }

    [TestMethod]
    public void ErstellenExceptionNull()
    {
      // vorbereiten & testen:
      ArgumentNullException exception = Assert.ThrowsException<ArgumentNullException>(() => new EntscheidungsbaumElement<ResultEnum>(null));

      // auswerten:
      Assert.AreEqual<string>("Übergebener Type darf nicht null sein. (Parameter 'wurzeltyp')", exception.Message);
    }

    [TestMethod]
    public void GetWurzeltypTest()
    {
      // vorbereiten:
      EntscheidungsbaumElement<ResultEnum> element = new EntscheidungsbaumElement<ResultEnum>(typeof(EigenschaftEnum));

      // testen & auswerten:
      Assert.AreEqual<Type>(typeof(EigenschaftEnum), element.Wureltyp, "Wurzeltyp stimmt nicht überein.");
    }

    [TestMethod]
    public void SetBlattTest()
    {
      // vorbereiten:
      EntscheidungsbaumElement<ResultEnum> element = new EntscheidungsbaumElement<ResultEnum>(typeof(EigenschaftEnum));
      string kategorie = EigenschaftEnum.A.ToString();

      // ausführen:
      element.SetKind(kategorie, ResultEnum.Z);

      // testen & auswerten:
      Assert.IsTrue(element.IstBlatt(kategorie));
    }

    [TestMethod]
    public void SetKindTest()
    {
      // vorbereiten:
      EntscheidungsbaumElement<ResultEnum> wurzel = new EntscheidungsbaumElement<ResultEnum>(typeof(EigenschaftEnum));
      EntscheidungsbaumElement<ResultEnum> kind = new EntscheidungsbaumElement<ResultEnum>(typeof(EigenschaftEnum));
      string kategorie = EigenschaftEnum.A.ToString();

      // ausführen:
      wurzel.SetKind(kategorie, kind);

      // testen & auswerten:
      Assert.IsFalse(wurzel.IstBlatt(kategorie));
    }

  }
}

using Ednevnik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testovi
{
    [TestClass]
    public class WhiteboxTests
    {
        private Nastavnik nastavnik;
        private Ucenik ucenik;

        [TestInitialize]
        public void TestInitialize()
        {
             nastavnik = new Nastavnik("Kenan", "Dizdarevic", "kdizdarevi1", "12345");
             ucenik = new Ucenik("Haris", "Dizdarevic", "harisd1", "1234");
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void NoviKomentar_UcenikNull_BacaException()
        {
            string opis = "Neprimjereno ponasanje u skolskom dvoristu!";
            nastavnik.NoviKomentar(null, opis);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void NoviKomentar_OpisNull_BacaException()
        {
            nastavnik.NoviKomentar(ucenik, null);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void NoviKomentar_OpisPrazanString_BacaException()
        {
            nastavnik.NoviKomentar(ucenik, "");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void NoviKomentar_KomentariUcenikaNull_BacaException()
        {
            ucenik.Komentari = null;
            string opis = "Kasni na cas!";
            nastavnik.NoviKomentar(ucenik, opis);
        }

        [TestMethod]
        public void UpisiUceniku_ValidanKomentar()
        {
            string opis = "Neprimjereno ponasanje u skolskom dvoristu!";
            Komentar komentar = new Komentar(nastavnik, ucenik, opis);
            nastavnik.NoviKomentar(ucenik, opis);
            Assert.IsTrue(ucenik.Komentari.Any(k => k.Opis == opis));
        }
    }
}

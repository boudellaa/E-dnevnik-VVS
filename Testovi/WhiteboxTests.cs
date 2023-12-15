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


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DajProsjekPredmeta_NemaRazredaSImenomPredmeta_BacaException()
        {

            var predmet = new Predmet("Historija");

            var prosjek = predmet.DajProsjekPredmeta();

        }

        

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DajProsjekPredmeta_NemaUcenikaURazredu_BacaException()
        {

            var predmet = new Predmet("Hemija");
            var razred = new Razred("3B");
            var razredPredmet = new Razred_Predmet(razred, predmet);
            predmet.Razredi_Predmeti.Add(razredPredmet);

            var prosjek = predmet.DajProsjekPredmeta();

            Assert.AreEqual(0, prosjek);
        }


        [TestMethod]
        public void DajProsjekPredmeta_JedanRazredSocjenama_VracaProsjekOcjena()
        {
            var predmet = new Predmet("Matematika");
            var razred = new Razred("4C");
            var razredPredmet = new Razred_Predmet(razred, predmet);
            predmet.Razredi_Predmeti.Add(razredPredmet);

            var ucenik1 = new Ucenik("Ana", "Anić", "ana12345", "12345");
            ucenik1.Ocjene.Add(new Ocjena(4, ucenik1, predmet, DateTime.Now));
            ucenik1.Ocjene.Add(new Ocjena(5, ucenik1, predmet, DateTime.Now));
            razred.Ucenici.Add(ucenik1);

            var prosjek = predmet.DajProsjekPredmeta();

            Assert.AreEqual(4.5, prosjek);
        }

        [TestMethod]
        public void DajProsjekRazreda_VracaTacanProsjek()
        {
            var razred = new Razred("r1");
            var ucenik1 = new Ucenik("Test1", "Test1", "Test1", "Test1", razred);
            var ucenik2 = new Ucenik("Test2", "Test2", "Test2", "Test2", razred);

            ucenik1.Ocjene.Add(new Ocjena(4, ucenik1, new Predmet("Math"), DateTime.Now));
            ucenik2.Ocjene.Add(new Ocjena(5, ucenik2, new Predmet("Math"), DateTime.Now));

            razred.DodajUcenika(ucenik1);
            razred.DodajUcenika(ucenik2);

            var prosjek = razred.DajProsjekRazreda();

            Assert.AreEqual(4.5, prosjek, 0.01);
        }

        [TestMethod]
        public void DajProsjekRazreda_NemaOcjena_Izuzetak()
        {
            var razred = new Razred("r1");

            Assert.ThrowsException<Exception>(() => razred.DajProsjekRazreda(), "Razred nema ocjena!");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Ocjena mora imati vrijednost od 1 do 5!")]
        public void DajProsjekRazreda_NevalidnaOcjena_Izuzetak()
        {
            var razred = new Razred("r1");
            var ucenik = new Ucenik("Test", "Test", "Test", "Test", razred);
            ucenik.Ocjene.Add(new Ocjena(6, ucenik, new Predmet("Math"), DateTime.Now));

            var prosjek = razred.DajProsjekRazreda();
        }
    }
}

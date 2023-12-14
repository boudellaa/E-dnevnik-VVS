using E_dnevnik;
using Ednevnik;
using Moq;
using System.Text.Json;

namespace Testovi
{
    [TestClass]
    public class UnitTest2
    {
        private E_Dnevnik E_Dnevnik;
        private Ucenik ucenik;
        private Razred razred;
        private Predmet predmet;
        private Nastavnik nastavnik;

        [TestInitialize]
        public void Test2Initialize()
        {
            E_Dnevnik = new E_Dnevnik();
            predmet = new Predmet("TestPredmet");
            ucenik = new Ucenik("Test", "Test", "Test", "Test");
            razred = new Razred("TestRazred");
            nastavnik = new Nastavnik("test", "test", "test", "test");
            E_Dnevnik.DodajRazred(razred);
            E_Dnevnik.DodajUcenikaURazred(ucenik, razred);
            E_Dnevnik.SpojiRazredIPredmet(razred, predmet);
            E_Dnevnik.SpojiNastavnikPredmet(nastavnik, predmet);
        }

        // relaciona veza nastavnik-predmet
        [TestMethod]
        public void TestMethod1()
        {
            Test2Initialize();

            Assert.AreEqual(nastavnik, predmet.Nastavnik);
            Assert.AreEqual(predmet, nastavnik.Predmet);
        }

        // dohvacanje svih razreda i ucenika na predmetu
        [TestMethod]
        public void TestMethod2()
        {
            Test2Initialize();
            var razredi = predmet.DajSveRazredeNaPredmetu();
            var ucenici = predmet.DajSveUcenikeNaPredmetu();

            CollectionAssert.AllItemsAreUnique(razredi);
            CollectionAssert.AreEqual(razred.Ucenici, ucenici);
        }

        // azuriranje postojeceg nastavnika sa zamjenskim objektom
        [TestMethod]
        public void TestMethod3()
        {
            Test2Initialize();
            var mockNastavnik = new Mock<Nastavnik>("ime", "prezime", "korisnickoIme", "sifra").Object;

            predmet.DodijeliNastavnika(mockNastavnik);

            Assert.AreEqual(mockNastavnik, predmet.Nastavnik);
        }


        // prosjek predmeta i citanje iz json datoteke
        [TestMethod]
        public void TestMethod4()
        {
            Test2Initialize();

            var jsonString = File.ReadAllText("../../../Ocjene.json");
            var jsonOcjene = JsonSerializer.Deserialize<List<String>>(jsonString);
            Console.WriteLine(jsonOcjene);
            foreach (var ocjena in jsonOcjene)
            {
                ucenik.Ocjene.Add(new Ocjena(Convert.ToInt32(ocjena), ucenik, predmet, DateTime.Now));
            }

            var razredPredmet = new Razred_Predmet(razred, predmet);
            predmet.Razredi_Predmeti.Add(razredPredmet);

            var prosjek = predmet.DajProsjekPredmeta();

            Assert.AreEqual(3.0, prosjek);
        }



        // test prosjek predmeta kada nema ucenika
        [TestMethod]
        public void TestMethod5()
        {
            
            predmet.Razredi_Predmeti.Clear(); 

            Assert.AreEqual(0, predmet.DajProsjekPredmeta());
        }


        // dodjela null nastavnika
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMethod6()
        {
            var predmet = new Predmet("TestPredmet");

            predmet.DodijeliNastavnika(null);

        }

        // test prosjek razreda
        [TestMethod]
        public void TestMethod7()
        {
            Test2Initialize();

            ucenik.Ocjene.Add(new Ocjena(5, ucenik, predmet, DateTime.Now));
            ucenik.Ocjene.Add(new Ocjena(4, ucenik, predmet, DateTime.Now));

            var razredPredmet = new Razred_Predmet(razred, predmet);
            predmet.Razredi_Predmeti.Add(razredPredmet);

            var prosjek = predmet.DajProsjekPredmeta();

            Assert.AreEqual(4.5, prosjek);
        }



    }
}
  
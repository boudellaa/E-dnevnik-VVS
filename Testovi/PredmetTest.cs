using E_dnevnik;
using Ednevnik;
using Moq;
using System.Text.Json;

namespace Testovi
{
    [TestClass]
    public class PredmetTest
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

        
        [TestMethod]
        public void SpojiNastavnikPredmet_ProvjeraVezeNastavnikPredmet()
        {
            Test2Initialize();

            Assert.AreEqual(nastavnik, predmet.Nastavnik);
            Assert.AreEqual(predmet, nastavnik.Predmet);
        }

       
        [TestMethod]
        public void DajSveRazredeIUcenike_ProvjeraRazredaIUcenika()
        {
            Test2Initialize();
            var razredi = predmet.DajSveRazredeNaPredmetu();
            var ucenici = predmet.DajSveUcenikeNaPredmetu();

            CollectionAssert.AllItemsAreUnique(razredi);
            CollectionAssert.AreEqual(razred.Ucenici, ucenici);
        }

        
        [TestMethod]
        public void DodijeliNastavnika_NastavnikUspjesnoDodijeljen()
        {
            Test2Initialize();
            var mockNastavnik = new Mock<Nastavnik>("ime", "prezime", "korisnickoIme", "sifra").Object;

            predmet.DodijeliNastavnika(mockNastavnik);

            Assert.AreEqual(mockNastavnik, predmet.Nastavnik);
        }


        [TestMethod]
        public void DajProsjekPredmeta_VracaProsjek()
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



        [TestMethod]
        public void DajProsjekPredmeta_VracaProsjek2()
        {
            
            predmet.Razredi_Predmeti.Clear(); 

            Assert.AreEqual(0, predmet.DajProsjekPredmeta());
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DodijeliNastavnika_Izuzetak()
        {
            var predmet = new Predmet("TestPredmet");

            predmet.DodijeliNastavnika(null);

        }

      
        [TestMethod]
        public void DajProsjekPredmeta_VracaProsjek3()
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
  
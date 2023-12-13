using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Ednevnik;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using E_dnevnik;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;

namespace Testovi
{
    [TestClass]
    public class NastavnikTest
    {
        private  E_Dnevnik ednevnik;
        private  Nastavnik nastavnik;
        private  Predmet predmet;
        private  Ucenik ucenik;
        private  Razred razred;

        [TestInitialize]
        public void TestInitializeAttribute()
        {
            Console.WriteLine("POCELI TESTOVI!");
            ednevnik = new E_Dnevnik();
            nastavnik = new Nastavnik("Kenan", "Dizdarevic", "kenankd", "12345678");
            ucenik = new Ucenik("kenan", "dzd", "kenandzddd", "12345");
            predmet = new Predmet("Bosanski");
            razred = new Razred("IV-3");
            ednevnik.SpojiNastavnikPredmet(nastavnik, predmet);
            ednevnik.SpojiRazredIPredmet(razred, predmet);
            ednevnik.DodajUcenikaURazred(ucenik, razred);
        }

        [TestMethod]
        public void UpisiUceniku_ValidnuOcjenu()
        {
            Console.WriteLine("POCELI TESTOVI!");
            int vrijednost = 4;
            nastavnik.UpisiOcjenu(ucenik, vrijednost);
            Assert.AreEqual(1, ucenik.Ocjene.Count);
            Assert.AreEqual(vrijednost, ucenik.Ocjene[0].Vrijednost);
            TimeSpan difference = DateTime.Now - ucenik.Ocjene[0].Datum;
            Assert.IsTrue(difference.TotalSeconds < 5);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void UpisiUceniku_NevalidnuOcjenu_BacaException()
        {
            int vrijednost = 6;
            nastavnik.UpisiOcjenu(ucenik, vrijednost);
        }

        [TestMethod]
        public void UpisiUceniku_ValidanKomentar()
        {
            string opis = "Neprimjereno ponasanje u skolskom dvoristu!";
            Komentar komentar = new Komentar(nastavnik,ucenik,opis);
            nastavnik.NoviKomentar(ucenik, opis);
            Assert.IsTrue(ucenik.Komentari.Any(k => k.Opis == opis));
        }

        public static IEnumerable<object[]> ReadJSONUcenika()
        {
            List<object[]> ucenici = new List<object[]>();
            string jsonContent = File.ReadAllText("../../../ucenici.json");
            List<Ucenik> ucenikLista = JsonConvert.DeserializeObject<List<Ucenik>>(jsonContent);
            foreach(Ucenik ucenik in ucenikLista)
            {
                ucenici.Add(new object[]
                {
                    ucenik
                });
            }
            return ucenici;
        }

        [TestMethod]
        [DynamicData(nameof(ReadJSONUcenika),DynamicDataSourceType.Method)]
        public void Dohvati_SveNastavnikoveUcenike(Ucenik u)
        {
            razred.Ucenici.Add(u);
            List<Ucenik> nastavnikoviUcenici = nastavnik.DajSveUcenikeNastavnika();
            Assert.IsTrue(nastavnikoviUcenici.Any(uc => uc.Ime==u.Ime && uc.KorisnickoIme==u.KorisnickoIme));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Dohvati_PrazanRazred_BacaException()
        {
            razred.Ucenici.Clear();
            List<Ucenik> ucenici = nastavnik.DajSveUcenikeNastavnika();
        }

        public static IEnumerable<object[]> OcjeneData()
        {
                return new[]
                {
                    new object[] 
                    {
                        new List<Ocjena>
                        {
                            new Ocjena(4,new UcenikDummy(),new Predmet(""),DateTime.Now),
                            new Ocjena(3,new UcenikDummy(),new Predmet(""),DateTime.Now),
                            new Ocjena(2,new UcenikDummy(),new Predmet(""),DateTime.Now),
                            new Ocjena(5,new UcenikDummy(),new Predmet(""),DateTime.Now),
                            new Ocjena(2,new UcenikDummy(),new Predmet(""),DateTime.Now),
                        },
                        16.0/5 
                    },
                    new object[]
                    {
                        new List<Ocjena> 
                        { 
                            new Ocjena(4,new UcenikDummy(),new Predmet(""),DateTime.Now),
                            new Ocjena(4,new UcenikDummy(),new Predmet(""),DateTime.Now),
                            new Ocjena(4,new UcenikDummy(),new Predmet(""),DateTime.Now),
                            new Ocjena(4,new UcenikDummy(),new Predmet(""),DateTime.Now),
                            new Ocjena(4,new UcenikDummy(),new Predmet(""),DateTime.Now),
                            new Ocjena(4,new UcenikDummy(),new Predmet(""),DateTime.Now),
                            new Ocjena(4,new UcenikDummy(),new Predmet(""),DateTime.Now),
                        },
                        4.0
                    }
                };
        }

        [TestMethod]
        [DynamicData(nameof(OcjeneData),DynamicDataSourceType.Method)]
        public void Izracunaj_ProsjekRazreda_VracaProsjek(List<Ocjena> ocjene, double expected)
        {
            ucenik.Ocjene.AddRange(ocjene);
            Double prosjek = nastavnik.IzracunajProsjekRazreda();
            Assert.AreEqual(expected, prosjek);
        }

        [TestMethod]
        public void Prisustvo_UspjesnoZabiljezeno()
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            };

            List<Ucenik> ucenici = new List<Ucenik>() { ucenik};
            nastavnik.NapraviPrisustvo(ucenici);

            List<Tuple<string, string>> records;
            using (var reader = new StreamReader("../../../../E-Dnevnik/prisustvo.csv"))
            using (var csv = new CsvReader(reader, csvConfig))
            {
                records = csv.GetRecords<Tuple<string, string>>().ToList();
            }
            Assert.AreEqual(ucenik.Ime,records[1].Item1 );
            Assert.AreEqual(ucenik.Prezime,records[1].Item2);
        }
    }
}

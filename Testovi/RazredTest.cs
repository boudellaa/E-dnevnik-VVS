using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using E_dnevnik;
using Ednevnik;
using System.Xml;
using System.Xml.Linq;

namespace Testovi
{
    [TestClass]
    public class RazredTests
    {
        public TestContext TestContext
        {
            get;set;
        }
        
        private Ucenik ucenik;
        private Razred razred;
        

        public static IEnumerable<object[]> ReadXML()
        {

            var path = "../../../RazredTest.xml";

            var xml = XDocument.Load(path);
            List<object[]> ucenici = new List<object[]>();
            foreach (var ucenikElement in xml.Descendants("ucenik"))
            {
                string ime = ucenikElement.Attribute("Ime")?.Value ?? string.Empty;
                string prezime = ucenikElement.Attribute("Prezime")?.Value ?? string.Empty;
                string korisnickoIme = ucenikElement.Attribute("KorisnickoIme")?.Value ?? string.Empty;
                string sifra = ucenikElement.Attribute("Sifra")?.Value ?? string.Empty;

                Ucenik ucenik = new Ucenik(ime, prezime, korisnickoIme, sifra);
                ucenici.Add(new object[] { ucenik });
            }
            return ucenici;
        }

        static IEnumerable<object[]> UceniciXML
        {
            get
            {
                return ReadXML();
            }
        }


        // DDT TEST

        [DynamicData(nameof(UceniciXML))]
        [TestMethod]
        public void DodajUcenika_True(Ucenik u)
        {
            razred = new Razred("r1");
            razred.DodajUcenika(u);

            Assert.IsTrue(razred.Ucenici.Any(ucenik => ucenik.Ime == u.Ime && ucenik.Prezime == u.Prezime));
        }

        public static IEnumerable<object[]> ReadXML2()
        {
            var path = "../../../RazredTest2.xml";
            var xml = XDocument.Load(path);
            List<object[]> ocjene = new List<object[]>();

            var uceniciElements = xml.Descendants("ucenik");

            foreach (var ucenikElement in uceniciElements)
            {
                var ocjeneElement = ucenikElement.Element("ocjene");
                if (ocjeneElement != null)
                {
                    List<Ocjena> ocjeneUcenika = new List<Ocjena>();

                    foreach (var ocjenaElement in ocjeneElement.Elements("ocjena"))
                    {
                        int vrijednost = int.Parse(ocjenaElement.Element("vrijednost")?.Value ?? "0");

                        Ucenik dummyUcenik = new Ucenik("test", "test", "test", "test");
                        Predmet dummyPredmet = new Predmet("test");

                        Ocjena o = new Ocjena(vrijednost, dummyUcenik, dummyPredmet, DateTime.Now);
                        ocjeneUcenika.Add(o);
                    }

                    ocjene.Add(new object[] { ocjeneUcenika });
                }
            }

            return ocjene;
        }

        static IEnumerable<object[]> OcjeneXML
        {
            get
            {
                return ReadXML2();
            }
        }

        [DynamicData(nameof(OcjeneXML))]
        [TestMethod]
        public void DajProsjekRazreda_VracaProsjek(List<Ocjena> ocjeneUcenika)
        {
            razred = new Razred("r2");
            var ucenik1 = new Ucenik("Test", "Test", "Test", "Test", razred);
            razred.DodajUcenika(ucenik1);

            ucenik1.Ocjene.AddRange(ocjeneUcenika);

            var prosjek = razred.DajProsjekRazreda();

            Assert.AreEqual(3.75, prosjek, 0.01);
        }


        [TestMethod]
        public void DajProsjekRazreda_Izuzetak()
        {
            var razred = new Razred("1A");
            var ucenik1 = new Ucenik("Test", "Test", "Test", "Test", razred);
            var ucenik2 = new Ucenik("Test", "Test", "Test", "Test", razred);

            ucenik1.Ocjene.Add(new Ocjena(4, new UcenikDummy(), new Predmet("Test"), DateTime.Now));

            razred.DodajUcenika(ucenik1);

            Assert.ThrowsException<Exception>(() =>
            {
                ucenik2.Ocjene.Add(new Ocjena(6, new UcenikDummy(), new Predmet("Test"), DateTime.Now));

                var prosjek = razred.DajProsjekRazreda();
            });
        }

        [TestMethod]
        public void DodajUcenika_ProvjeraRazreda()
        {
            razred = new Razred("TestRazred");
            ucenik = new Ucenik("Test", "Test", "Test", "Test", razred);

            razred.DodajUcenika(ucenik);

            CollectionAssert.Contains(razred.Ucenici, ucenik);
            Assert.AreEqual(razred, ucenik.Razred);
        }

        [TestMethod]
        public void DajProsjekRazreda_Izuzetak2()
        {
            Razred razred = new Razred("TreciRazred");
            Ucenik ucenik1 = new Ucenik("Test", "Test", "Test", "Test", razred);
            Ucenik ucenik2 = new Ucenik("Test", "Test", "Test", "Test", razred);
            Ucenik ucenik3 = new Ucenik("Test", "Test", "Test", "Test", razred);

            Assert.ThrowsException<Exception>(() => razred.DajProsjekRazreda(), "Razred nema ocjena!");
        }

        [TestMethod]
        public void DajProsjekRazreda_VracaProsjek2()
        {
            Razred razred = new Razred("CetvrtiRazred");
            Ucenik ucenik1 = new Ucenik("Test", "Test", "Test", "Test", razred);
            ucenik1.Ocjene.Add(new Ocjena(4, new UcenikDummy(), new Predmet("Test"), DateTime.Now));
            Ucenik ucenik2 = new Ucenik("Test", "Test", "Test", "Test", razred);
            ucenik2.Ocjene.Add(new Ocjena(5, new UcenikDummy(), new Predmet("Test"), DateTime.Now));

            razred.Ucenici.AddRange(new List<Ucenik> { ucenik1, ucenik2 });

            double prosjek = razred.DajProsjekRazreda();

            Assert.AreEqual(4.5, prosjek, 0.01);
        }

        [TestMethod]
        public void DodajUcenika_Izuzetak()
        {
            Razred razred = new Razred("PetiRazred");
            Ucenik ucenik = new Ucenik("Test", "Test", "Test", "Test", razred);

            razred.DodajUcenika(ucenik);

            Assert.ThrowsException<Exception>(() => razred.DodajUcenika(ucenik), "Ucenik već postoji u razredu!");
        }
    }
}
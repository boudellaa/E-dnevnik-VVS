using Ednevnik;
using E_dnevnik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Moq;

namespace Testovi
{
    [TestClass]
    public class E_DnevnikTests
    {
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }
        private E_Dnevnik eDnevnik;

        [TestInitialize]
        public void Setup()
        {
            eDnevnik = new E_Dnevnik();
        }

        [TestMethod]
        public void DodajRazred_ShouldAddRazred()
        {
            Setup();
            var razred = new Razred("II-1");
            eDnevnik.DodajRazred(razred);
            Assert.IsTrue(eDnevnik.Razredi.Contains(razred));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PrikaziRazrede_ShouldThrowExceptionIfNoRazred()
        {
            var eDnevnik2 = new E_Dnevnik();
            eDnevnik2.Razredi.Clear();
            eDnevnik2.PrikaziRazrede();
        }

        [TestMethod]
        public void PrikaziRazrede_ShouldDisplayRazredi()
        {
            Setup();
            var razred = new Razred("II-1");
            eDnevnik.Razredi.Add(razred);
            try
            {
                eDnevnik.PrikaziRazrede();
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, got: " + ex.Message);
            }
        }

        [TestMethod]
        public void ValidirajLoginNastavnika_ShouldReturnNastavnikIfValid()
        {
            Setup();
            var username = "bera";
            var password = "12345";
            var result = eDnevnik.ValidirajLoginNastavnika(eDnevnik, username, password);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ValidirajLoginUcenika_ShouldReturnUcenikIfValid()
        {
            Setup();
            var username = "kenankd";
            var password = "123456";
            var result = eDnevnik.ValidirajLoginUcenika(eDnevnik, username, password);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DajOcjene_ShouldAddOcjeneToUcenik()
        {
            Setup();
            var ucenik = new Ucenik("Test", "Ucenik", "testucenik", "test123");
            eDnevnik.DajOcjene(ucenik);
        }

        [TestMethod]
        public void SpojiRazredIPredmet_ShouldConnectRazredAndPredmet()
        {
            Setup();
            var razred = new Razred("II-1");
            var predmet = new Predmet("TestPredmet");
            eDnevnik.SpojiRazredIPredmet(razred, predmet);
            Assert.IsTrue(razred.Razredi_Predmeti.Any(rp => rp.Predmet == predmet));
        }

        [TestMethod]
        public void SpojiNastavnikPredmet_ShouldConnectNastavnikAndPredmet()
        {
            Setup();
            var nastavnik = new Nastavnik("Test", "Nastavnik", "testnastavnik", "test123");
            var predmet = new Predmet("TestPredmet");
            eDnevnik.SpojiNastavnikPredmet(nastavnik, predmet);
            Assert.AreEqual(predmet, nastavnik.Predmet);
        }

        [TestMethod]
        public void DodajUcenikaURazred_ShouldAddUcenikToRazred()
        {
            Setup();
            var razred = new Razred("II-1");
            var ucenik = new Ucenik("Test", "Ucenik", "testucenik", "test123");
            eDnevnik.DodajUcenikaURazred(ucenik, razred);
            Assert.IsTrue(razred.Ucenici.Contains(ucenik));
        }

        private static IEnumerable<object[]> GetTestDataFromXml()
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load("../../../TestDataEDnevnik.xml");

            var testCases = new List<object[]>();

            foreach (XmlNode testCaseNode in xmlDocument.SelectNodes("/TestData/TestCase"))
            {
                var ime = testCaseNode.SelectSingleNode("Ime").InnerText;
                var prezime = testCaseNode.SelectSingleNode("Prezime").InnerText;
                var password = testCaseNode.SelectSingleNode("Password").InnerText;

                testCases.Add(new object[] { ime, prezime, password });
            }

            return testCases;
        }

        static IEnumerable<object[]> UceniciXML
        {
            get
            {
                return GetTestDataFromXml();
            }
        }

        [TestMethod]
        [DynamicData(nameof(UceniciXML))]
        public void RegistrujUcenika_ShouldRegisterUcenik(string ime, string prezime, string password)
        {
            Setup();
            var username = eDnevnik.RegistrujUcenika(ime, prezime, password);
            var username2 = ime.Substring(0, 1) + prezime.ToLower() + "1";

            // Assert
            Assert.IsNotNull(username);
            Assert.AreEqual(username, ime.Substring(0, 1).ToLower() + prezime.ToLower() + "1");
        }

        [TestMethod]
        public void HashPasswordAndVerifyPassword_ShouldMatch()
        {
            Setup();
            var password = "test123";
            var salt = new byte[64];
            var hashedPassword = eDnevnik.HashPassword(password, out salt);
            var result = eDnevnik.VerifyPassword(password, hashedPassword, salt);
            Assert.IsTrue(result);
        }

        public static IEnumerable<object[]> RezrediData
        {
            get
            {
                return new[]
                {
                    new object[] {"I-1"},
                    new object[] {"I-2"},
                    new object[] {"I-3"},
                    new object[] {"I-4"}
                };
            }
        }

        [TestMethod]
        [DynamicData(nameof(RezrediData))]
        public void DodajUcenikaURazred_ShouldChangeRazredSize(String razredName)
        {
            Setup();
            var ucenik = new Mock<Ucenik>(null, null, null, null);
            var razred = eDnevnik.Razredi.Where(r => r.Ime == razredName).FirstOrDefault();
            var numStudentsBefore = eDnevnik.Razredi.FirstOrDefault(r => r.Ime == razredName)?.Ucenici.Count ?? 0;
            eDnevnik.DodajUcenikaURazred(ucenik.Object, razred);
            var numStudentsAfter = eDnevnik.Razredi.FirstOrDefault(r => r.Ime == razredName)?.Ucenici.Count ?? 0;
            Assert.AreEqual(numStudentsBefore + 1, numStudentsAfter);
        }

    }

}
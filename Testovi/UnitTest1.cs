
using E_dnevnik;
using Ednevnik;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Testovi
{
	[TestClass]
	public class UnitTest1
	{
		private TestContext testContextInstance;
		public TestContext TestContext
		{
			get { return testContextInstance; }
			set { testContextInstance = value; }
		}
		private XmlReaderSettings settings = new XmlReaderSettings();
		

		private E_Dnevnik E_Dnevnik;
		private Ucenik ucenik;
		private Razred razred;
		private Predmet predmet;

		[TestInitialize]
		public void Test1Initialize()
		{
			E_Dnevnik = new E_Dnevnik();
			predmet = new Predmet("TestPredmet");
			ucenik = new Ucenik("Test", "Test", "Test", "Test");
			razred = new Razred("TestRazred");
			E_Dnevnik.DodajRazred(razred);
			E_Dnevnik.DodajUcenikaURazred(ucenik, razred);
			E_Dnevnik.SpojiRazredIPredmet(razred, predmet);

		}

		// Prosjek na predmetu
		[TestMethod]
		public void TestMethod1()
		{
			Test1Initialize();
			double zbir = 0;
			Random rnd = new Random();
			
			
			for(int i = 1; i <= 10; i++)
			{
				int ocjena = rnd.Next(1, 5);
				zbir += ocjena;
				ucenik.Ocjene.Add(new Ocjena(ocjena, ucenik, predmet, DateTime.Now));
			}

			Assert.AreEqual(zbir/10, ucenik.DajProsjekUcenikaNaPredmetu(predmet));

		}

		
		public static IEnumerable<object[]> ReadXML()
		{
			
			var path = "C:\\Users\\Nedim Krupalija\\E-dnevnik-VVS\\Testovi\\TestData.xml";
			var xml = XDocument.Load(path);
			List<object[]> predmeti = new List<object[]>();
			IEnumerable<XElement> lista = xml.Root.Descendants("predmet");
			foreach( XElement element in lista)
			{
				predmeti.Add(new object[]
				{
					element.Value
				}); 
			}

			
				
			
			return predmeti;
		}

		static IEnumerable<object[]> PredmetiXML
		{
			get
			{
				return ReadXML();
			}
		}

		

		// DDT TEST

		[DynamicData(nameof(PredmetiXML))]
		[TestMethod]
		public void TestMethod2(string imePredmeta)
		{
			E_Dnevnik = new E_Dnevnik();
			Console.WriteLine("PREDMET: "+imePredmeta);
			
			razred = new Razred("TestRazred");
			ucenik = new Ucenik("Test", "Test", "Test", "Test",razred);
			E_Dnevnik.DodajRazred(razred);
			E_Dnevnik.DodajUcenikaURazred(ucenik, razred);

			
			predmet = new Predmet(imePredmeta);
			E_Dnevnik.SpojiRazredIPredmet(razred, predmet);

			Assert.IsTrue(ucenik.DajMojePredmete().Contains(predmet));

		}

		//Ucenik nema ocjena nikako
		[TestMethod]
		public void test3()
		{
			Test1Initialize();
			Assert.AreEqual(0, ucenik.DajProsjekUcenikaNaPredmetu(predmet));
			Assert.AreEqual(0, ucenik.DajUkupanProsjekUcenika());
		}

		//Test ocjena nije validna
		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void test4()
		{
			var ocjena = new Ocjena(55, new UcenikDummy(), new Predmet("Test"), DateTime.Now);
		}


		// Ukupan prosjek ucenika ddt js/csv mozda
		[TestMethod]
		public void test5()
		{
			Test1Initialize();
			
		}

		public static IEnumerable<object[]> OcjeneData
		{
			get
			{
				return new[]
				{
					new object[] {new List<Ocjena> { new Ocjena(1,new UcenikDummy(),new Predmet(""),DateTime.Now),
									new Ocjena(1,new UcenikDummy(),new Predmet(""),DateTime.Now),
					new Ocjena(1,new UcenikDummy(),new Predmet(""),DateTime.Now),
					new Ocjena(1,new UcenikDummy(),new Predmet(""),DateTime.Now),
					new Ocjena(1,new UcenikDummy(),new Predmet(""),DateTime.Now),
					new Ocjena(1,new UcenikDummy(),new Predmet(""),DateTime.Now),
					new Ocjena(1,new UcenikDummy(),new Predmet(""),DateTime.Now)},
						1.0 },
					new object[]
					{
						new List<Ocjena> { new Ocjena(1,new UcenikDummy(),new Predmet(""),DateTime.Now),
						new Ocjena(2,new UcenikDummy(),new Predmet(""),DateTime.Now),
						new Ocjena(3,new UcenikDummy(),new Predmet(""),DateTime.Now),
						new Ocjena(4,new UcenikDummy(),new Predmet(""),DateTime.Now),
						new Ocjena(5,new UcenikDummy(),new Predmet(""),DateTime.Now),
						new Ocjena(5,new UcenikDummy(),new Predmet(""),DateTime.Now),
						new Ocjena(4,new UcenikDummy(),new Predmet(""),DateTime.Now),
						},
						24.0/7
					},
					new object[]
					{
						new List<Ocjena> { new Ocjena(5,new UcenikDummy(),new Predmet(""),DateTime.Now),
						new Ocjena(4,new UcenikDummy(),new Predmet(""),DateTime.Now),
						new Ocjena(5,new UcenikDummy(),new Predmet(""),DateTime.Now),
						new Ocjena(4,new UcenikDummy(),new Predmet(""),DateTime.Now),
						new Ocjena(5,new UcenikDummy(),new Predmet(""),DateTime.Now),
						new Ocjena(4,new UcenikDummy(),new Predmet(""),DateTime.Now),
						new Ocjena(5,new UcenikDummy(),new Predmet(""),DateTime.Now)},
						32.0/7
					}

				};
			}
		}
		//test ukupan prosjek ucenika
		[TestMethod]
		[DynamicData(nameof(OcjeneData))]
		public void test6(List<Ocjena> ocjene, double ocekivano)
		{
			Test1Initialize();
			ucenik.Ocjene = ocjene;
			Assert.AreEqual("Test", ucenik.Ime);
			Assert.AreEqual("Test", ucenik.Prezime);
			Assert.AreEqual("Test", ucenik.Sifra);
			Assert.AreEqual("Test", ucenik.KorisnickoIme);
			Assert.AreEqual(ocekivano, ucenik.DajUkupanProsjekUcenika());

			
		}
	}
}
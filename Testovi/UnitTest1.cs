
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

			
				
			Console.WriteLine("#"+predmeti.ToString());
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


	}
}
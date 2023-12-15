using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Ednevnik
{
	public class E_Dnevnik
	{

		public List<Razred> Razredi { get; set; }

		public List<Ucenik> Ucenici { get; set; }

		public List<Nastavnik> Nastavnici { get; set; }

		public List<Predmet> Predmeti { get; set; }

		private const int _saltSize = 16; // 128 bits
		private const int _keySize = 32; // 256 bits
		private const int _iterations = 50000;
		private static readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA256;

		private const char segmentDelimiter = ':';


		public E_Dnevnik()
		{



			Razredi = new List<Razred>
		{
			new Razred("I-1"),
			new Razred("I-2"),
			new Razred("I-3"),
			new Razred("I-4"),
			new Razred("I-5"),
			new Razred("I-6"),
		 };

			Ucenici = new List<Ucenik> { new Ucenik("Kenan", "Dizdarević", "kenankd", HashPassword("123456")),
			new Ucenik("Nedim", "Krupalija", "neda", HashPassword("12345")) ,
			new Ucenik("John", "Doe", "john", HashPassword("12345")),

			new Ucenik("Vitoria","Lilllie","Jenna",HashPassword("12345")),

			new Ucenik("Xylina","Zora","Michaelina",HashPassword("12345")),

			new Ucenik("Florinda","Kipp","Antonina",HashPassword("12345")),

			new Ucenik("Didi","Aindrea","Sybila",HashPassword("12345")),

			new Ucenik("Nathalie","Corina","Eilis",HashPassword("12345")),

			new Ucenik("Betteann","Bird","Margalit",HashPassword("12345")),

			new Ucenik("Clarinda","Edith","Lanny",HashPassword("12345")),

			new Ucenik("Fawne","Britteny","Stacee",HashPassword("12345")),

			new Ucenik("Jada","Catlaina","Yovonnda",HashPassword("12345")),

			new Ucenik("Amity","Randi","Lenora",HashPassword("12345")),

			new Ucenik("Kayley","Meredithe","Concettina",HashPassword("12345")),

			new Ucenik("Luelle","Rosie","Godiva",HashPassword("12345")),

			new Ucenik("Emmaline","Zena","Deedee",HashPassword("12345")),

			new Ucenik("Audre","Odetta","Frank",HashPassword("12345")),

			new Ucenik("Mariann","Giulia","Allyn",HashPassword("12345")),

			new Ucenik("Clarette","Colline","Rae",HashPassword("12345")),

			new Ucenik("Henrieta","Suzette","Nady",HashPassword("12345")),

			new Ucenik("Kathleen","Rozanna","Ki",HashPassword("12345")),

			new Ucenik("Andy","Constantine","Tildy",HashPassword("12345")),

			new Ucenik("Klarika","Sileas","Bernetta",HashPassword("12345")),

			new Ucenik("Juditha","Laure","Fifine",HashPassword("12345")),

			new Ucenik("Cybil","Lilith","Pearla",HashPassword("12345")),

			new Ucenik("Ines","Jazmin","Fifine",HashPassword("12345")),

			new Ucenik("Mariele","Kyrstin","Shena",HashPassword("12345")),

			new Ucenik("Tommy","Robinette","Odille",HashPassword("12345")),

			new Ucenik("Andi","Brooke","Merilee",HashPassword("12345")),

			new Ucenik("Abigael","Pen","Shelly",HashPassword("12345")),

			new Ucenik("Moyna","Kellia","Melesa",HashPassword("12345")),

			new Ucenik("Wendeline","Allix","Lethia",HashPassword("12345")),

			new Ucenik("Dion","Lindy","Rora",HashPassword("12345"))



			};
			Nastavnici = new List<Nastavnik> {
				new Nastavnik("Berin", "Karahodžić", "bera",HashPassword("12345")),
				new Nastavnik("Nedim", "Hošić", "hosa", HashPassword("12345")) ,
				new Nastavnik("Ali", "Boudellaa", "buda", HashPassword("12345")),
				new Nastavnik("Mujo", "Mujic", "mujo", HashPassword("12345")),
				new Nastavnik("Haso", "Hasic", "haso", HashPassword("12345")),
				new Nastavnik("Buba", "Corelli", "kora", HashPassword("12345")),
				new Nastavnik("Halid", "Beslic", "halid", HashPassword("12345"))

			};
			Predmeti = new List<Predmet>{
			new Predmet("Matematika"),
			new Predmet("Fizika"),
			new Predmet("Programiranje"),
			new Predmet("Osnove eleketrotehnike"),
			new Predmet("Verifikacija i validacija softvera"),
			new Predmet("Bosanski jezik"),
			new Predmet("Engleski jezik")
		};

			for (int i = 0; i < Nastavnici.Count; i++)
			{
				SpojiNastavnikPredmet(Nastavnici[i], Predmeti[i]);
			}
			var random = new Random();

			for (int i = 0; i < Ucenici.Count; i++)
			{
				DodajUcenikaURazred(Ucenici[i], Razredi[random.Next(Razredi.Count - 1)]);

			}

			for (int i = 0; i < Razredi.Count; i++)
			{
				for (int j = 0; Razredi[i].Razredi_Predmeti.Count < 3; j++)
				{
					int brojac = random.Next(Predmeti.Count - 1);
					var tempPredmet = Predmeti[brojac];
					if (Razredi[i].Razredi_Predmeti.Any(p => p.Predmet.Ime == tempPredmet.Ime)) continue;
					SpojiRazredIPredmet(Razredi[i], Predmeti[brojac]);
				}

			}

			foreach (var ucenik in Ucenici)
			{
				DajOcjene(ucenik);
			}

		}

		public Nastavnik? ValidirajLoginNastavnika(E_Dnevnik EDnevnik, String username, String password)
		{
			Nastavnik? nastavnik = EDnevnik.Nastavnici.SingleOrDefault(nastavnik => nastavnik.KorisnickoIme == username && VerifyPassword(password, nastavnik.Sifra));

			if (nastavnik != null)
			{
				return nastavnik;
			}
			return null;
		}

		public Ucenik? ValidirajLoginUcenika(E_Dnevnik EDnevnik, String username, String password)
		{
			Ucenik? ucenik = EDnevnik.Ucenici.SingleOrDefault(ucenik => ucenik.KorisnickoIme == username && VerifyPassword(password, ucenik.Sifra));
			if (ucenik != null)
			{
				return ucenik;
			}
			return null;
		}
		public void DajOcjene(Ucenik ucenik)
		{
			if (ucenik == null || Ucenici.All(u => u != ucenik))
			{
				throw new Exception("Nepoznat učenik");
			}
			var random = new Random();
			foreach (var predmet in Predmeti)
			{
				bool ucenikImaPredmet = ucenik.Razred.Razredi_Predmeti.Any(rp => rp.Predmet == predmet);

				if (!ucenikImaPredmet)
				{
					continue;
				}
				int brojOcjena = random.Next(5, 11);
				for (int i = 0; i < brojOcjena; i++)
				{
					int ocjena = random.Next(1, 6);
					var datum = NasumicniDatum(random);
					ucenik.Ocjene.Add(new Ocjena(ocjena, ucenik, predmet, datum));
				}
			}
		}

		private DateTime NasumicniDatum(Random x)
		{
			DateTime start = new DateTime(2022, 9, 1);
			DateTime end = new DateTime(2023, 6, 30);
			int range = (end - start).Days;
			return start.AddDays(x.Next(range));
		}

		public void SpojiRazredIPredmet(Razred razred, Predmet predmet)
		{
			var razred_predmet = new Razred_Predmet(razred, predmet);
			razred.Razredi_Predmeti.Add(razred_predmet);
			predmet.Razredi_Predmeti.Add(razred_predmet);
		}

		public void SpojiNastavnikPredmet(Nastavnik nastavnik, Predmet predmet)
		{
			nastavnik.Predmet = predmet;
			predmet.Nastavnik = nastavnik;
		}

		public void DodajUcenikaURazred(Ucenik ucenik, Razred razred)
		{
			ucenik.Razred = razred;
			razred.Ucenici.Add(ucenik);
		}

		public void DodajRazred(Razred razred)
		{
			Razredi.Add(razred);
			Console.WriteLine($"Razred {razred.Ime} dodan.");
		}

		public void PrikaziRazrede()
		{
			if (Razredi.Count == 0)
			{
				throw new InvalidOperationException("U školi nema razreda.");
			}

			foreach (var razred in Razredi)
			{
				Console.WriteLine($"{razred.Ime}");
			}
		}



		public String HashPassword(String input)
		{
			byte[] salt = RandomNumberGenerator.GetBytes(_saltSize);
			byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
				input,
				salt,
				_iterations,
				_algorithm,
				_keySize
			);
			return string.Join(
				segmentDelimiter,
				Convert.ToHexString(hash),
				Convert.ToHexString(salt),
				_iterations,
				_algorithm
			);
		}

		public bool VerifyPassword(string input, string hashString)
		{
			string[] segments = hashString.Split(segmentDelimiter);
			byte[] hash = Convert.FromHexString(segments[0]);
			byte[] salt = Convert.FromHexString(segments[1]);
			int iterations = int.Parse(segments[2]);
			HashAlgorithmName algorithm = new HashAlgorithmName(segments[3]);
			byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
				input,
				salt,
				iterations,
				algorithm,
				hash.Length
			);
			return CryptographicOperations.FixedTimeEquals(inputHash, hash);
		}


		public string RegistrujUcenika(string ime, string prezime, string password)
		{
			if(string.IsNullOrEmpty(ime) || string.IsNullOrEmpty(ime.Trim()))
			{
				throw new ArgumentNullException("Polje ime ne može biti prazno.");
			}
			else if(string.IsNullOrEmpty(prezime) || string.IsNullOrEmpty(prezime.Trim()))
            {
                throw new ArgumentNullException("Polje prezime ne može biti prazno.");
            }
			else if(string.IsNullOrEmpty(password) || string.IsNullOrEmpty(password.Trim()))
            {
                throw new ArgumentNullException("Polje password ne može biti prazno.");
            }
			if (!ime.All(char.IsLetter))
			{
				throw new ArgumentException("Polje ime ne može sadržavati znakove ili brojeve.");
			}
			else if (!prezime.All(char.IsLetter))
            {
                throw new ArgumentException("Polje prezime ne može sadržavati znakove ili brojeve.");
            }
            string username = ime[0] + prezime;
			if (username.Length > 10)
				username = username.Substring(0, 10);
			username = username + "1";
			username = username.ToLower();
			int br = 2;
			while (Ucenici.Exists(u => u.KorisnickoIme == username))
			{
				username = username.Substring(0, username.Length - 1);
				username = username + br.ToString();
				br++;
			}
			var ucenik = new Ucenik((char.ToUpper(ime[0]) + ime.Substring(1).ToLower()), (char.ToUpper(prezime[0]) + prezime.Substring(1).ToLower()), username, HashPassword(password));
			var random = new Random();
			Ucenici.Add(ucenik);
			DodajUcenikaURazred(ucenik, Razredi[random.Next(Razredi.Count - 1)]);
			return username;
		}
	}






}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace E_dnevnik
{
    public class E_Dnevnik
    {
        
        public List<Razred> Razredi { get; set; }

        public List<Ucenik> Ucenici { get; set; }

        public List<Nastavnik> Nastavnici { get; set; }

        public List<Predmet> Predmeti { get; set; }


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

			Ucenici = new List<Ucenik> { new Ucenik("Kenan", "Dizdarević", "kenankd", "123456"),
			new Ucenik("Nedim", "Krupalija", "neda", "12345") ,
			new Ucenik("John", "Doe", "john", "12345"),

			new Ucenik("Vitoria","Lilllie","Jenna","8419"),

			new Ucenik("Xylina","Zora","Michaelina","7058"),

			new Ucenik("Florinda","Kipp","Antonina","6057"),

			new Ucenik("Didi","Aindrea","Sybila","5706"),

			new Ucenik("Nathalie","Corina","Eilis","1288"),

			new Ucenik("Betteann","Bird","Margalit","7202"),

			new Ucenik("Clarinda","Edith","Lanny","8575"),

			new Ucenik("Fawne","Britteny","Stacee","4350"),

			new Ucenik("Jada","Catlaina","Yovonnda","6523"),

			new Ucenik("Amity","Randi","Lenora","4510"),

			new Ucenik("Kayley","Meredithe","Concettina","2887"),

			new Ucenik("Luelle","Rosie","Godiva","8832"),

			new Ucenik("Emmaline","Zena","Deedee","2726"),

			new Ucenik("Audre","Odetta","Frank","274"),

			new Ucenik("Mariann","Giulia","Allyn","6965"),

			new Ucenik("Clarette","Colline","Rae","8886"),

			new Ucenik("Henrieta","Suzette","Nady","328"),

			new Ucenik("Kathleen","Rozanna","Ki","891"),

			new Ucenik("Andy","Constantine","Tildy","3189"),

			new Ucenik("Klarika","Sileas","Bernetta","5089"),

			new Ucenik("Juditha","Laure","Fifine","7055"),

			new Ucenik("Cybil","Lilith","Pearla","2816"),

			new Ucenik("Ines","Jazmin","Fifine","2634"),

			new Ucenik("Mariele","Kyrstin","Shena","9563"),

			new Ucenik("Tommy","Robinette","Odille","4401"),

			new Ucenik("Andi","Brooke","Merilee","2423"),

			new Ucenik("Abigael","Pen","Shelly","1057"),

			new Ucenik("Moyna","Kellia","Melesa","1547"),

			new Ucenik("Wendeline","Allix","Lethia","1010"),

			new Ucenik("Dion","Lindy","Rora","5244")



			};
			Nastavnici = new List<Nastavnik> {
				new Nastavnik("Berin", "Karahodžić", "bera", "12345"),
				new Nastavnik("Nedim", "Hošić", "hosa", "loslos") ,
				new Nastavnik("Ali", "Boudellaa", "buda", "12345"),
				new Nastavnik("Mujo", "Mujic", "mujo", "12345"),
				new Nastavnik("Haso", "Hasic", "haso", "12345"),
				new Nastavnik("Buba", "Corelli", "kora", "12345"),
				new Nastavnik("Halid", "Beslic", "halid", "12345")

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
            Nastavnik? nastavnik = EDnevnik.Nastavnici.SingleOrDefault(nastavnik => nastavnik.KorisnickoIme == username && nastavnik.Sifra == password);
            if (nastavnik != null)
            {
                return nastavnik;
            }
            return null;
        }

        public Ucenik? ValidirajLoginUcenika(E_Dnevnik EDnevnik, String username, String password)
        {
            Ucenik? ucenik = EDnevnik.Ucenici.SingleOrDefault(ucenik => ucenik.KorisnickoIme == username && ucenik.Sifra == password);
            if (ucenik != null)
            {
                return ucenik;
            }
            return null;
        }
        public void DajOcjene(Ucenik ucenik)
        {
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

        

        public String HashPassword(String password, out byte[] salt)
        {
			HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
			salt = RandomNumberGenerator.GetBytes(64);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                350000,
                hashAlgorithm,
                64);

            return Convert.ToHexString(hash);
        }

        public bool VerifyPassword(string password, string hash, byte[] salt)
        {
			HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
			var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, 350000, hashAlgorithm, 64);
			return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
		}


	
			public string RegistrujUcenika(string ime, string prezime, string password)
	{
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
		var ucenik = new Ucenik((char.ToUpper(ime[0]) + ime.Substring(1).ToLower()), (char.ToUpper(prezime[0]) + prezime.Substring(1).ToLower()), username, password);
		var random = new Random();
		Ucenici.Add(ucenik);
		DodajUcenikaURazred(ucenik, Razredi[random.Next(Razredi.Count - 1)]);
		return username;
	}
	}






}

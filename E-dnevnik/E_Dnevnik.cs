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

			for(int i=0;i<Nastavnici.Count;i++)
			{
				SpojiNastavnikPredmet(Nastavnici[i], Predmeti[i]);
			}
			var random = new Random();

			for(int i=0;i<Ucenici.Count;i++)
			{
				DodajUcenikaURazred(Ucenici[i], Razredi[random.Next(6)]);
			}

			for(int i=0;i<Razredi.Count;i++)
			{
				for(int j=0;Razredi[i].Razredi_Predmeti.Count<3;j++)
				{
					var tempPredmet = Predmeti[random.Next(Predmeti.Count)];
					if (Razredi[i].Razredi_Predmeti.Any(p => p.Predmet.Ime == tempPredmet.Ime)) continue;
					SpojiRazredIPredmet(Razredi[i], Predmeti[random.Next(Predmeti.Count)]);
				}
				
			}




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


		public void PrikaziUcenickiMeni(Ucenik ucenik)
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine("Dobrodošli " + ucenik.Ime + " " + ucenik.Prezime + "!" + " Vi pripadate razredu " + ucenik.Razred.Ime);
				Console.WriteLine("Unesite: ");
				Console.WriteLine("1 za pregled vaših predmeta");
				Console.WriteLine("2 za pregled prosjeka po predmetima");
				Console.WriteLine("3 za pregled ukupnog prosjeka");
				Console.WriteLine("0 za povratak unazad");
				string opcija = Console.ReadLine();
				switch (opcija)
				{
					case "0":
						return;
					case "1":
						PrikaziUcenikovePredmete(ucenik);
						break;

				}
			}
		}

		public void PrikaziUcenikovePredmete(Ucenik ucenik)
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine("Dobrodošli " + ucenik.Ime + " " + ucenik.Prezime + "!");
				Console.WriteLine("Vaši predmeti: ");
				List<Predmet> ucenikoviPredmeti = ucenik.DajMojePredmete();
				for (int i = 0; i < ucenikoviPredmeti.Count; i++)
				{
					Console.WriteLine(i + 1 + ". predmet: " + ucenikoviPredmeti[i].Ime);
				}
				Console.WriteLine("Unesite redni broj predmeta za prikaz ocjena ili 0 za nazad: ");
				int broj = Convert.ToInt32(Console.ReadLine());
				if (broj == 0) return;
				if (broj < 0 || broj > ucenikoviPredmeti.Count)
				{
					Console.WriteLine("Odabrali ste nepostojeci predmet!");
					continue;
				}
				PrikaziUcenikovPredmet(ucenik, ucenikoviPredmeti[broj - 1]);
			}
		}


		public void PrikaziUcenikovPredmet(Ucenik ucenik, Predmet predmet)
		{
			Console.Clear();
			Console.WriteLine(ucenik.Ime + " " + ucenik.Prezime + ", dobrodošli na predmet " + predmet.Ime + "!");
			Console.WriteLine("Nastavnik: " + predmet.Nastavnik.Ime + " " + predmet.Nastavnik.Prezime);
			Console.WriteLine("Vaše ocjene: ");
			Console.WriteLine("Vaš prosjek: ");
			Console.WriteLine("Unesi 0 za povratak nazad");
			while (true)
			{
				string opcija = Console.ReadLine();
				if (opcija.Equals("0")) return;
				else Console.WriteLine("Neispravan unos!");
			}
		}

		public void PrikaziNastavnickiMeni(Nastavnik nastavnik)
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine("Dobrodošli nastavniče " + nastavnik.Ime + " " + nastavnik.Prezime);
				Console.WriteLine("Unesite: ");
				Console.WriteLine("1 za pregled predmeta koje predajete");
				Console.WriteLine("2 za pregled razreda kojima predajete");
				Console.WriteLine("0 za povratak unazad");
				string opcija = Console.ReadLine();
				switch (opcija)
				{
					case "0":
						return;
					case "1":
						PrikaziNastavnikovePredmete(nastavnik);
						break;
					case "2":
						PrikaziNastavnikoveRazrede(nastavnik);
						break;
				}
			}

		}

		 public void PrikaziNastavnikovePredmete(Nastavnik nastavnik)
		{

			while (true)
			{
				Console.Clear();
				Console.WriteLine("Dobrodošli nastavniče " + nastavnik.Ime + " " + nastavnik.Prezime);
				Console.WriteLine("Ovo je vaš predmeti: ");
				
				Console.WriteLine(nastavnik.Predmet.Ime);

			}
		}
		

		private static void PrikaziNastavnikoveRazrede(Nastavnik nastavnik)
		{
			throw new NotImplementedException();
		}



	}

}

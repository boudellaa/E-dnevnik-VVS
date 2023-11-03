using System;
using System.Collections.Generic;
using System.Linq;
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


        public Razred_Predmet Razred_Predmet { get; set; }


        public E_Dnevnik()
        {

		

			Razredi = new List<Razred>
		{
			new Razred("II-4"),
			new Razred("III-2")
		 };

			Ucenici = new List<Ucenik> { new Ucenik("Kenan", "Dizdarević", "kenankd", "123456"), new Ucenik("Nedim", "Krupalija", "neda", "12345") };
			Nastavnici = new List<Nastavnik> { new Nastavnik("Berin", "Karahodžić", "bera", "12345"), new Nastavnik("Nedim", "Hošić", "hosa", "loslos") };
			Predmeti = new List<Predmet>{
			new Predmet("Matematika", Nastavnici[0]),
			new Predmet("Fizika", Nastavnici[1])
		};

			new Razred_Predmet(Razredi[0], Predmeti[0]);

			Razredi[0].DodajUcenika(Ucenici[0]);
			Razredi[1].DodajUcenika(Ucenici[1]);


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
				Console.WriteLine("Dobrodošli " + ucenik.Ime + " " + ucenik.Prezime + "!");
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

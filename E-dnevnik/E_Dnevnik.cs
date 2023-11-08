﻿using System;
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


		public void PrikaziUcenickiMeni(Ucenik ucenik)
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine("Dobrodošli " + ucenik.Ime + " " + ucenik.Prezime + "!" + " Vi pripadate razredu " + ucenik.Razred.Ime);
				Console.WriteLine("Unesite: ");
				Console.WriteLine("1 za pregled vaših predmeta");
				Console.WriteLine("2 za pregled ukupnog prosjeka");
				Console.WriteLine("3 za pregled vasih komentara");
				Console.WriteLine("0 za povratak unazad");
				string opcija = Console.ReadLine();
				switch (opcija)
				{
					case "0":
						return;
					case "1":
						PrikaziUcenikovePredmete(ucenik);
						break;
					case "2":
						PrikaziProsjekUcenika(ucenik);
						break;
					case "3":
						PrikaziKomentareUcenika(ucenik);
						break;
				}
			}
		}


		public void PrikaziKomentareUcenika(Ucenik ucenik)
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine("Ovdje se nalaze svi vaši komentari: ");
				int i = 0;
				foreach(var komentar in ucenik.Komentari)
				{
					Console.WriteLine(i+1 + ". Komentar napisao: " + komentar.Nastavnik.Ime);
					Console.WriteLine("Opis komentara: " + komentar.Opis);
				}

				try
				{
					int broj = Convert.ToInt32(Console.ReadLine());
					if (broj == 0) return;
					Console.WriteLine("Unesite 0 za povratak nazad");
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					continue;
				}
			}
		}

		public void PrikaziProsjekUcenika(Ucenik ucenik)
		{
			while(true)
			{
				Console.Clear();
				Console.WriteLine("Vaš trenutni prosjek je: " + ucenik.DajUkupanProsjekUcenika());
				try
				{
					int broj = Convert.ToInt32(Console.ReadLine());
					if (broj == 0) return;
					Console.WriteLine("Unesite 0 za povratak nazad");
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					continue;
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
			
			while (true)
			{
				Console.Clear();
				Console.WriteLine(ucenik.Ime + " " + ucenik.Prezime + ", dobrodošli na predmet " + predmet.Ime + "!");
				Console.WriteLine("Nastavnik: " + predmet.Nastavnik.Ime + " " + predmet.Nastavnik.Prezime);
				Console.WriteLine("Vaše ocjene: ");
				foreach (var ocjena in ucenik.DajOcjeneIzPredmeta(predmet))
					Console.WriteLine(ocjena.Vrijednost + ", datum: " + ocjena.Datum.ToShortDateString());
				Console.WriteLine("Vaš prosjek: " + ucenik.DajProsjekUcenikaNaPredmetu(predmet));

				Console.WriteLine("1. sortiranje ocjena po vrijednosti");
				Console.WriteLine("0. povratak nazad");

				string opcija = Console.ReadLine();
				switch (opcija)
				{
					case "0":
						return;
					case "1":
						while (true)
						{
							Console.Clear();
							Console.WriteLine("Vase ocjena sortirane po vrijednosti glase: ");
							foreach (var ocjena in ucenik.DajSortiraneOcjenePoVrijednosti(ucenik.DajOcjeneIzPredmeta(predmet)))
								Console.WriteLine(ocjena.Vrijednost + ", datum: " + ocjena.Datum.ToShortDateString());
							Console.WriteLine("0. nazad");
							if (Console.ReadLine() == "0") return;
						}
						break;

					default:
						Console.WriteLine("Neispravan unos!");
						break;
				}
				
					
			}
		}

		private static void PrikaziSortiraneOcjene(Ucenik ucenik, Predmet predmet)
		{
			Console.Clear();
			Console.WriteLine("Vase ocjena sortirane po vrijednosti glase: ");
			foreach(var ocjena in ucenik.DajSortiraneOcjenePoVrijednosti(ucenik.DajOcjeneIzPredmeta(predmet)))
				Console.WriteLine(ocjena.Vrijednost + ", datum: " + ocjena.Datum.ToShortDateString());

			Console.WriteLine("0. za nazad");


		}

		public void PrikaziNastavnickiMeni(Nastavnik nastavnik)
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine("Dobrodošli nastavniče " + nastavnik.Ime + " " + nastavnik.Prezime + ", predmet koji predajete je " + nastavnik.Predmet.Ime + ".");
				Console.WriteLine("Unesite: ");
				Console.WriteLine("1 za pregled razreda kojima predajete");
				Console.WriteLine("2 za pretragu ucenika po imenu");
				Console.WriteLine("0 za povratak unazad");
				string opcija = Console.ReadLine();
				switch (opcija)
				{
					case "0":
						return;
					case "1":
						PrikaziNastavnikoveRazrede(nastavnik);
						break;
					case "2":
						PretragaUcenika(nastavnik);
						break;
					default:
						Console.WriteLine("Pogresan unos!");
						break;
				}
			}

		}

		private static void PretragaUcenika(Nastavnik nastavnik)
		{
			Console.Clear();
			Console.WriteLine("Unesite ime ucenika za pretragu: ");
			string ime = Console.ReadLine();
			Console.WriteLine("Unesite prezime ucenika za pretragu: ");
			string prezime = Console.ReadLine();

			Ucenik ucenik = nastavnik.Predmet.DajSveUcenikeNaPredmetu().Where(p => p.Ime.Contains(ime) && p.Prezime.Contains(prezime)).First();
			PrikaziUcenika(ucenik, nastavnik);

		}

		private static void PrikaziNastavnikoveRazrede(Nastavnik nastavnik)
		{
			while (true)
			{
				Console.Clear ();
                List<Razred> nastavnikoviRazredi = nastavnik.Predmet.DajSveRazredeNaPredmetu();

                if (nastavnikoviRazredi.Count == 0)
                {
                    Console.WriteLine("Trenutno ne predajete nijednom razredu.");
                }
                else
                {
                    Console.WriteLine("Razredi kojima predajete:");
                    for (int i = 0; i < nastavnikoviRazredi.Count; i++)
                    {
                        Console.WriteLine(i + 1 + ". " + nastavnikoviRazredi[i].Ime);
                    }
                }
				Console.WriteLine("0 za povratak nazad");
                int broj;
                while (true)
                {
                    Console.WriteLine("Unesite broj pored razreda da izaberete zeljeni razred ili 0 za nazad: ");
                    broj = Convert.ToInt32(Console.ReadLine());
					if (broj == 0) return;
                    if (broj < 0 || broj > nastavnikoviRazredi.Count)
                        Console.WriteLine("Pogresan unos!");
					
                    else break;
                }
                PrikaziNastavnikovRazred(nastavnikoviRazredi[broj - 1], nastavnik);
            }
		}
		public static void PrikaziNastavnikovRazred(Razred razred,Nastavnik nastavnik)
        {
			while (true)
			{
                Console.Clear();
                Console.WriteLine("Izabrali ste razred: " + razred.Ime);
				Console.WriteLine("1 za prosjek razreda");
				Console.WriteLine("2 za pregled svih učenika u razreda");
				Console.WriteLine("0 za povratak unazad");
				Console.Write("Unesite vaš izbor: ");
				string izbor = Console.ReadLine();
				switch (izbor)
				{
					case "0":
						return;
					case "1":
						PrikaziProsjekRazreda(razred);
						break;
					case "2":

						PrikaziUcenikeRazreda(razred, nastavnik);
						break;

				}
			}
        }


		private static void PrikaziUcenikeRazreda(Razred razred, Nastavnik nastavnik)
		{
			List<Ucenik> ucenici = new();
			try
			{
				ucenici = razred.DajSveUcenike();
			}
			catch (Exception)
			{
				Console.WriteLine("Razred nema ucenika!");
			}

			for (int i = 0; i < ucenici.Count; i++)
			{
				Console.WriteLine(i + 1 + ". " + ucenici[i].Ime + " " + ucenici[i].Prezime);
			}
			int broj;
			while (true)
			{
				Console.WriteLine("Unesite broj pored ucenika da izaberete zeljenog ucenika ili 0 za nazad: ");
				broj = Convert.ToInt32(Console.ReadLine());
				if (broj == 0) return;
				if (broj < 0 || broj > ucenici.Count)
					Console.WriteLine("Pogresan unos!");
				else break;
			}
			PrikaziUcenika(ucenici[broj - 1], nastavnik);
		}
        
        private static void PrikaziUcenika(Ucenik ucenik, Nastavnik nastavnik)
        {
			

			while (true)
			{
				Console.Clear();
				Console.WriteLine("Ovo je ucenik " + ucenik.Ime + " " + ucenik.Prezime);
				Console.WriteLine("Trenutno je clan razreda" + ucenik.Razred.Ime);
				Console.WriteLine("Ovo su njegove ocijene iz Vašeg predmeta: ");
				foreach (Ocjena ocjena in ucenik.DajOcjeneIzPredmeta(nastavnik.Predmet))
				{
					Console.WriteLine(ocjena.Vrijednost + " na datum " + ocjena.Datum);
				}
				Console.WriteLine(" ");
				Console.WriteLine("1. Upisi novu ocjenu");
				Console.WriteLine("2. Dodaj novi komentar");
				Console.WriteLine("0. za povratak nazad");
                string opcija = Console.ReadLine();

				switch (opcija)
				{
					case "0":
						return;
					case "1":
						UpisiOcjenu(ucenik, nastavnik);
						break;
					case "2":
						UnesiNoviKomentar(ucenik, nastavnik);
						break;
					default:
						  Console.WriteLine("Neispravan unos!");
						break;
				}
				
               
            }
        }
		private static void UpisiOcjenu(Ucenik ucenik, Nastavnik nastavnik)
		{
			Console.Clear();
			Console.WriteLine("Vrijednost(1-5) ili 0 za prekid: ");
			int ocjena = Convert.ToInt32(Console.ReadLine());
			if (ocjena == 0) return;
			while (true)
			{
				try
				{
					nastavnik.UpisiOcjenu(ucenik, ocjena);
					return;
				}
				catch (Exception ex)
				{

					Console.WriteLine(ex.Message);
				}
			}
			
		}

		private static void UnesiNoviKomentar(Ucenik ucenik, Nastavnik nastavnik)
		{
			Console.Clear();

			Console.WriteLine("Unesite vas komentar ili 0 za prekid: ");
			String komentar = Console.ReadLine();
			if (komentar.Equals("0")) return;
			else ucenik.Komentari.Add(new Komentar(nastavnik, ucenik, komentar));


		}

		private static void PrikaziProsjekRazreda(Razred razred)
		{
			int broj;
			try
			{
				Console.WriteLine("Prosjek ovog razreda je " + razred.DajProsjekRazreda());
			}
			catch (Exception ex)
			{

				Console.WriteLine(ex.Message);
			}
			string izbor = Console.ReadLine();
			switch (izbor)
			{
				case "0":
					return;
					
			}
			while (true)
			{
				Console.WriteLine("0 za nazad: ");
				broj = Convert.ToInt32(Console.ReadLine());
				if (broj == 0) return;
				if (broj != 0)
					Console.WriteLine("Pogresan unos!");
				else break;
			}
		}

		public string RegistrujUcenika(string ime, string prezime, string password)
		{
			string username = ime[0] + prezime;
			if(username.Length > 10)
				username = username.Substring(0, 10);
			username = username + "1";
            username = username.ToLower();
            while (Ucenici.Exists(u => u.KorisnickoIme == username))
			{
				int br = 2;
				username = username.Substring(0, username.Length - 1);
				username = username + br.ToString();
				br++;
            }
			var ucenik = new Ucenik((char.ToUpper(ime[0])+ime.Substring(1).ToLower()), (char.ToUpper(prezime[0]) + prezime.Substring(1).ToLower()), username, password);
			var random = new Random();
			Ucenici.Add(ucenik);
            DodajUcenikaURazred(ucenik, Razredi[random.Next(Razredi.Count - 1)]);
			return username;
        }

    }


	

}

using E_dnevnik;
using System.Runtime.CompilerServices;

public class Program
{

	public static void Main()
	{

		var EDnevnik = new E_Dnevnik();
		Nastavnik TrenutniNastavnik = null;
		Ucenik TrenutniUcenik = null;

		//pocetak aplikacije 

		string korisnickoIme = "";
		string sifra = "";
		string izbor = "";

		while (true)
		{
			do
			{
				Console.Clear();
				Console.WriteLine("Dobrodošli u E-Dnevnik!");
				Console.WriteLine("Napišite 1 za login ili 2 za registraciju.");
				izbor = Console.ReadLine();
				if (izbor != "1" && izbor != "2")
					Console.WriteLine("Pogrešan unos.");
			} while (izbor != "1" && izbor != "2");

			if (izbor == "1")
			{
				do
				{
					Console.Clear();
					do
					{
						Console.WriteLine("Ulogujte se kako bi nastavili");
						Console.WriteLine("Unesite korisničko ime: ");
						korisnickoIme = Console.ReadLine();
					} while (korisnickoIme.Length == 0);

					do
					{
						Console.WriteLine("Unesite Šifru: ");
						sifra = Console.ReadLine();
					} while (sifra.Length == 0);

					Nastavnik? nastavnik = EDnevnik.Nastavnici.SingleOrDefault(nastavnik => nastavnik.KorisnickoIme == korisnickoIme && nastavnik.Sifra == sifra);
					if (nastavnik != null)
					{
						TrenutniNastavnik = nastavnik;
						PrikaziNastavnickiMeni(nastavnik);

					}

					else
					{
						Ucenik? ucenik = EDnevnik.Ucenici.SingleOrDefault(ucenik => ucenik.KorisnickoIme == korisnickoIme && ucenik.Sifra == sifra);
						if (ucenik != null)
						{
							TrenutniUcenik = ucenik;
							PrikaziUcenickiMeni(ucenik);
							break;
						}
						else
						{
							Console.WriteLine("Pogrešni podaci. Napišite 0 za povratak ili bilo šta drugo za ponovni unos.");
							string izbor2 = Console.ReadLine();
							if (izbor2 == "0")
								break;
						}
					}
				} while (true);
			}
			else if (izbor == "2")
			{
				string ime;
				string prezime;
				string username;
				string password;
				do
				{
					Console.Clear();
					Console.WriteLine("Unesite vaše podatke");
					Console.WriteLine("Ime: ");
					ime = Console.ReadLine();
				} while (ime.Length == 0);
				do
				{
					Console.WriteLine("Prezime: ");
					prezime = Console.ReadLine();
				} while (prezime.Length == 0);
				do
				{
					Console.WriteLine("Password: ");
					password = Console.ReadLine();
				} while (password.Length == 0);
				username = EDnevnik.RegistrujUcenika(ime, prezime, password);
				Console.WriteLine("Vaš username je " + username + "\nPritisnite bilo šta da nastavite");
				izbor = Console.ReadLine();
			}
		}

	






}

private static void PrikaziUcenickiMeni(Ucenik ucenik)
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


	private static void PrikaziKomentareUcenika(Ucenik ucenik)
	{
		while (true)
		{
			Console.Clear();
			Console.WriteLine("Ovdje se nalaze svi vaši komentari: ");
			int i = 0;
			foreach (var komentar in ucenik.Komentari)
			{
				Console.WriteLine(i + 1 + ". Komentar napisao: " + komentar.Nastavnik.Ime);
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

	private static void PrikaziProsjekUcenika(Ucenik ucenik)
	{
		while (true)
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

	public static void PrikaziUcenikovePredmete(Ucenik ucenik)
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


	private static void PrikaziUcenikovPredmet(Ucenik ucenik, Predmet predmet)
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
		foreach (var ocjena in ucenik.DajSortiraneOcjenePoVrijednosti(ucenik.DajOcjeneIzPredmeta(predmet)))
			Console.WriteLine(ocjena.Vrijednost + ", datum: " + ocjena.Datum.ToShortDateString());

		Console.WriteLine("0. za nazad");


	}

	private static void PrikaziNastavnickiMeni(Nastavnik nastavnik)
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
			Console.Clear();
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
	public static void PrikaziNastavnikovRazred(Razred razred, Nastavnik nastavnik)
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









}
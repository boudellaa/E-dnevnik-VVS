using E_dnevnik;

public class Program
{
    public static List<Ucenik> ucenici;
    public static List<Nastavnik> nastavnici;
    public static List<Predmet> predmeti;
    public static void Main()
    {
        ucenici = new List<Ucenik> { new Ucenik("Kenan", "Dizdarević", "kenankd", "123456"), new Ucenik("Nedim", "Krupalija", "neda", "12345") };
        nastavnici = new List<Nastavnik> { new Nastavnik("Berin", "Karahodžić", "bera", "12345"), new Nastavnik("Nedim", "Hošić", "hosa", "loslos") };
        predmeti = new List<Predmet>{
            new Predmet("Matematika", ucenici, nastavnici[0]),
            new Predmet("Fizika", new List<Ucenik>{ucenici[0]}, nastavnici[1])
        };
        Razred razred1 = new Razred("II-4");
        Razred razred2 = new Razred("III-2");

        razred1.DodajUcenika(ucenici[0]);
        razred2.DodajUcenika(ucenici[1]);

        Skola skola = new Skola("Treća gimnazija Sarajevo");

        skola.DodajRazred(razred1);
        skola.DodajRazred(razred2);

        skola.PrikaziRazrede();

        //pocetak aplikacije 

        string korisnickoIme ="";
        string sifra = "";
        while (true)
        {
            Console.Clear();
            do
            {
                Console.WriteLine("Unesite korisničko ime: ");
                korisnickoIme = Console.ReadLine();
            } while (korisnickoIme.Length == 0);

            do
            {
                Console.WriteLine("Unesite Šifru: ");
                sifra = Console.ReadLine();
            } while (sifra.Length == 0);

            Nastavnik? nastavnik = nastavnici.SingleOrDefault(nastavnik => nastavnik.KorisnickoIme == korisnickoIme && nastavnik.Sifra == sifra);
            if (nastavnik != null)
                prikaziNastavnickiMeni(nastavnik);
            else
            {
                Ucenik? ucenik = ucenici.SingleOrDefault(ucenik => ucenik.KorisnickoIme == korisnickoIme && ucenik.Sifra == sifra);
                if (ucenik != null)
                    prikaziUcenickiMeni(ucenik);
            }
        }
    }

    private static void prikaziUcenickiMeni(Ucenik ucenik)
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
                    prikaziUcenikovePredmete(ucenik);
                    break;
                
            }
        }
    }

    private static void prikaziUcenikovePredmete(Ucenik ucenik)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Dobrodošli " + ucenik.Ime + " " + ucenik.Prezime + "!");
            Console.WriteLine("Vaši predmeti: ");
            List<Predmet> ucenikoviPredmeti = ucenik.DajMojePredmete(predmeti);
            for(int i = 0; i < ucenikoviPredmeti.Count; i++)
            {
                Console.WriteLine(i + 1 + ". predmet: " + ucenikoviPredmeti[i].Ime);
            }
            Console.WriteLine("Unesite redni broj predmeta za prikaz ocjena ili 0 za nazad: ");
            int broj = Convert.ToInt32(Console.ReadLine());
            if (broj == 0) return;
            if(broj <0 || broj > ucenikoviPredmeti.Count)
            {
                Console.WriteLine("Odabrali ste nepostojeci predmet!");
                continue;
            }
            prikaziUcenikovPredmet(ucenik,ucenikoviPredmeti[broj-1]);
        }
    }

    private static void prikaziUcenikovPredmet(Ucenik ucenik,Predmet predmet)
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

    private static void prikaziNastavnickiMeni(Nastavnik nastavnik)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Dobrodošli nastavniče " + nastavnik.Ime + " " + nastavnik.Prezime);
            Console.WriteLine("Unesite: ");
            Console.WriteLine("1 za pregled razreda kojima predajete");
            Console.WriteLine("2 za pregled predmeta koje predajete");
            Console.WriteLine("0 za povratak unazad");
            string opcija = Console.ReadLine();
            switch (opcija)
            {
                case "0":
                    return;
                case "1":
                    prikaziNastavnikoveRazrede(nastavnik);
                    break;
                case "2":
                    prikaziNastavnikovePredmete(nastavnik);
                    break;
            }
        }

    }

    private static void prikaziNastavnikovePredmete(Nastavnik nastavnik)
    {
        Console.Clear();
        Console.WriteLine("Dobrodošli nastavniče " + nastavnik.Ime + " " + nastavnik.Prezime);
        

    }

    private static void prikaziNastavnikoveRazrede(Nastavnik nastavnik)
    {
        throw new NotImplementedException();
    }
}
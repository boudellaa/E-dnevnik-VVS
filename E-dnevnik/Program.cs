using E_dnevnik;

public class Program
{
    public static void Main()
    {
        List<Ucenik> ucenici = new List<Ucenik> { new Ucenik("Kenan", "Dizdarević", "kenankd", "123456"), new Ucenik("Nedim", "Krupalija", "neda", "12345") };
        List<Nastavnik> nastavnici = new List<Nastavnik> { new Nastavnik("Berin", "Karahodžić", "bera", "12345"), new Nastavnik("Nedim", "Hošić", "hosa", "loslos") };
        Predmet predmet1 = new Predmet("Matematika", nastavnici[0]);
        Predmet predmet2 = new Predmet("Fizika", nastavnici[1]);

        predmet1.DodajUcenika(ucenici[0]);
        predmet1.DodajUcenika(ucenici[1]);
        predmet2.DodajUcenika(ucenici[0]);

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
            Console.WriteLine("Dobrodošli " + ucenik.Ime + " " + ucenik.Prezime);
            Console.WriteLine("Unesite: ");
            Console.WriteLine("1 za pregled vaših predmeta");
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
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    private static void prikaziNastavnikoveRazrede(Nastavnik nastavnik)
    {
        throw new NotImplementedException();
    }
}
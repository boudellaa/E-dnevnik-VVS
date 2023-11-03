using E_dnevnik;

public class Program
{
    public static List<Ucenik> ucenici;
    public static List<Nastavnik> nastavnici;
    public static List<Predmet> predmeti;
    public static void Main()
    {
        Razred razred1 = new Razred("II-4");
        Razred razred2 = new Razred("III-2");
        ucenici = new List<Ucenik> { new Ucenik("Kenan", "Dizdarević", "kenankd", "123456"), new Ucenik("Nedim", "Krupalija", "neda", "12345") };
        nastavnici = new List<Nastavnik> { new Nastavnik("Berin", "Karahodžić", "bera", "12345"), new Nastavnik("Nedim", "Hošić", "hosa", "loslos") };
        predmeti = new List<Predmet>{
            new Predmet("Matematika", nastavnici[0]),
            new Predmet("Fizika", nastavnici[1])
        };
        razred1.Predmeti.AddRange(predmeti);
        razred2.Predmeti.Add(predmeti[0]);
        ucenici[0].Razred = razred1;
        ucenici[1].Razred= razred2;
        nastavnici[0].Predmeti.AddRange(predmeti);
        nastavnici[1].Predmeti.Add(predmeti[0]);
        

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
            List<Predmet> ucenikoviPredmeti = ucenik.DajMojePredmete();
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
            Console.WriteLine("1 za pregled predmeta koje predajete");
            Console.WriteLine("2 za pregled razreda kojima predajete");
            Console.WriteLine("0 za povratak unazad");
            string opcija = Console.ReadLine();
            switch (opcija)
            {
                case "0":
                    return;
                case "1":
                    prikaziNastavnikovePredmete(nastavnik);
                    break;
                case "2":
                    prikaziNastavnikoveRazrede(nastavnik);
                    break;
            }
        }

    }

    private static void prikaziNastavnikovePredmete(Nastavnik nastavnik)
    {
        
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Dobrodošli nastavniče " + nastavnik.Ime + " " + nastavnik.Prezime);
            Console.WriteLine("Ovo su vaši predmeti: ");
            for (int i = 0; i < nastavnik.Predmeti.Count; i++)
            {
                Console.WriteLine(i + 1 + ". predmet: " + nastavnik.Predmeti[i].Ime);
            }
        
                if (predmeti.Count > 0)
                    Console.WriteLine("Unesite redni broj predmeta za prikaz predmeta ili 0 za nazad");
                else
                {
                    Console.WriteLine("Trenutno ne predajete ni jedan predmet!");
                    Console.WriteLine("Unesite 0 za nazad");
                }
                int broj = Convert.ToInt32(Console.ReadLine());
                if (broj == 0) return;
                if (broj < 0 || broj > predmeti.Count)
                {
                    Console.WriteLine("Odabrali ste nepostojeci predmet!");
                    continue;
                }
                else prikaziNastavnikovPredmet(predmeti[broj-1]);
        }
    }
    private static void prikaziNastavnikovPredmet(Predmet predmet)
    {
        Console.Clear();
        while (true)
        {
            Console.WriteLine("Dobrodošli nastavniče " + predmet.Nastavnik.Ime + " " + predmet.Nastavnik.Prezime);
            Console.WriteLine("Ovo je predmet " + predmet.Ime);
            Console.WriteLine("Ovo su učenici koji slušaju ovaju predmet: ");
            List<Ucenik> ucenici = predmet.DajSveUcenike();
            for (int i = 0; i < ucenici.Count; i++)
            {
                Console.WriteLine(i + 1 + ". ucenik: " + ucenici[i].Ime + " " +ucenici[i].Prezime);
            }
            while (true)
            {
                Console.WriteLine("Unesite 0 za nazad");
                int broj = Convert.ToInt32(Console.ReadLine());
                if (broj == 0) return;
                else Console.WriteLine("Neispravan unos! ");
            }
            
        }
    }


    private static void prikaziNastavnikoveRazrede(Nastavnik nastavnik)
    {
        throw new NotImplementedException();
    }
}
using E_dnevnik;

public class Program
{
   
    public static void Main()
    {

		var EDnevnik = new E_Dnevnik();
		Nastavnik TrenutniNastavnik = null;
		Ucenik TrenutniUcenik = null;

		//pocetak aplikacije 

		 string korisnickoIme ="";
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
                        EDnevnik.PrikaziNastavnickiMeni(nastavnik);

                    }

                    else
                    {
                        Ucenik? ucenik = EDnevnik.Ucenici.SingleOrDefault(ucenik => ucenik.KorisnickoIme == korisnickoIme && ucenik.Sifra == sifra);
                        if (ucenik != null)
                        {
                            TrenutniUcenik = ucenik;
                            EDnevnik.PrikaziUcenickiMeni(ucenik);
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
                }while(true);
            }
            else if(izbor == "2")
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









}
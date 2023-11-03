using E_dnevnik;

public class Program
{
    public static List<Ucenik> ucenici;
    public static List<Nastavnik> nastavnici;
    public static List<Predmet> predmeti;
    public static void Main()
    {

		var EDnevnik = new E_Dnevnik();

        

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

            Nastavnik? nastavnik = EDnevnik.Nastavnici.SingleOrDefault(nastavnik => nastavnik.KorisnickoIme == korisnickoIme && nastavnik.Sifra == sifra);
            if (nastavnik != null)
                EDnevnik.PrikaziNastavnickiMeni(nastavnik);
            else
            {
                Ucenik? ucenik = EDnevnik.Ucenici.SingleOrDefault(ucenik => ucenik.KorisnickoIme == korisnickoIme && ucenik.Sifra == sifra);
                if (ucenik != null)
                    EDnevnik.PrikaziUcenickiMeni(ucenik);
            }
        }
    }

   

   

    


   
}
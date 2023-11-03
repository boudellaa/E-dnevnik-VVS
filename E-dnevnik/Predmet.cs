using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_dnevnik
{
    public class Predmet
    {
        public string Ime { get; set; }
        public List<Ucenik> Ucenici { get; set; }
        public Nastavnik Nastavnik { get; set; }

        public Predmet(string ime, Nastavnik nastavnik)
        {
            Ime = ime;
            Ucenici = new List<Ucenik>();
            Nastavnik = nastavnik;
         }
        public Predmet(string ime, List<Ucenik> ucenici,Nastavnik nastavnik)
        {
            Ime = ime;
            Ucenici = ucenici;
            Nastavnik = nastavnik;
        }


        public void DodajUcenika(Ucenik ucenik)
        {
            Ucenici.Add(ucenik);
            Console.WriteLine($"Učenik {ucenik.Ime} {ucenik.Prezime} dodan na predmet {Ime}.");
        }

        public List<Ucenik> DajSveUcenike()
        {
            if (Ucenici.Count == 0)
            {
                throw new Exception("Na predmetu nema prijavljenih ucenika!");
            }
            return Ucenici;
        }

        public void PrikaziUcenike()
        {
            if (Ucenici.Count == 0)
            {
                throw new InvalidOperationException("Na predmetu nema učenika.");
            }
            Console.WriteLine($"Učenici na predmetu {Ime}:");
            foreach (var ucenik in Ucenici)
            {
                Console.WriteLine($"{ucenik.Ime} {ucenik.Prezime}");
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_dnevnik
{
    public class Ucenik
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public string KorisnickoIme { get; set; }

        public string Sifra {  get; set; }
        public List<int> Ocjene { get; set; }

        public Ucenik(string ime, string prezime, string korisnickoIme, string sifra)
        {
            Ime = ime;
            Prezime = prezime;
            Ocjene = new List<int>();
            KorisnickoIme = korisnickoIme;
            Sifra = sifra;
        }

        public void DodajOcjenu(int ocjena)
        {
            if (ocjena < 1 || ocjena > 5)
            {
                throw new ArgumentOutOfRangeException("Ocjena mora biti između 1 i 5.");
            }
            Ocjene.Add(ocjena);
            Console.WriteLine($"Ocjena {ocjena} dodana učeniku {Ime} {Prezime}.");
        }

        public void PrikaziOcjene()
        {
            if (Ocjene.Count == 0)
            {
                throw new InvalidOperationException("Učenik nema ocjena.");
            }
            Console.WriteLine($"Ocjene učenika {Ime} {Prezime}: {string.Join(", ", Ocjene)}");
        }
    }

}

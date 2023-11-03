using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_dnevnik
{

    public class Nastavnik
    {
        public string Ime { get; set; }

        public string Prezime { get; set; }


        public string KorisnickoIme { get; set; }

        public string Sifra { get; set; }

        public List<Predmet> predmeti { get; set; }

        public Nastavnik(string ime, string prezime,string korisnickoIme, string sifra)
        {
            Ime = ime;
            Prezime = prezime;
            predmeti = new List<Predmet>();
            KorisnickoIme = korisnickoIme;
            Sifra = sifra;
        }

        public void DodajOcjenuUceniku(Ucenik ucenik, int ocjena)
        {
            ucenik.DodajOcjenu(ocjena);
        }

    
        /*
        public void PrikaziSvojeUcenike()
        {
            if (Ucenici.Count == 0)
            {
                throw new InvalidOperationException("Nastavnik nema učenika.");
            }
            Console.WriteLine($"Učenici nastavnika {Ime} {Prezime}:");
            foreach (var ucenik in Ucenici)
            {
                Console.WriteLine($"{ucenik.Ime} {ucenik.Prezime}");
            }
        }
        */

        /*public void IzracunajProsjekRazreda()
        {
            if (Ucenici.Count == 0)
            {
                throw new InvalidOperationException("Nastavnik nema učenika.");
            }
            double prosjek = Ucenici.Average(u => u.Ocjene.Average());
            Console.WriteLine($"Prosjek ocjena razreda nastavnika {Ime} {Prezime} je {prosjek:F2}.");
        }*/
    }

}

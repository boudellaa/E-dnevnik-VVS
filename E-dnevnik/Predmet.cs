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
        public List<Razred> Razredi { get; set; }
        public Nastavnik Nastavnik { get; set; }

        public Predmet(string ime, Nastavnik nastavnik)
        {
            Ime = ime;
            Razredi = new List<Razred>();
            Nastavnik = nastavnik;
         }
        public Predmet(string ime, List<Razred> razredi,Nastavnik nastavnik)
        {
            Ime = ime;
            Razredi = razredi;
            Nastavnik = nastavnik;
        }


        public void DodajRazred(Razred razred)
        {
            Razredi.Add(razred);
            Console.WriteLine($"Razred {razred.Ime} dodan na predmet {Ime}.");
        }

        public List<Ucenik> DajSveUcenike()
        {
            List<Ucenik> ucenici = new List<Ucenik>();
            foreach (Razred razred in Razredi)
            {
                ucenici.AddRange(razred.Ucenici);
            }
            return ucenici;
        }

        /*public void PrikaziUcenike()
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
        */
    }

}

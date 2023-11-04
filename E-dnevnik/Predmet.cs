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
       
        public Nastavnik Nastavnik { get; set; }

        public List<Razred_Predmet> Razredi_Predmeti { get; set; } = new();

        

        public Predmet(string ime)
        {
            Ime = ime;
            
        }

        public void DodijeliNastavnika(Nastavnik nastavnik)
        {
            Nastavnik = nastavnik;
            nastavnik.Predmet = this;
        }

        //public void DodajRazred(Razred razred)
        //{
        //    Razredi.Add(razred);
        //    Console.WriteLine($"Razred {razred.Ime} dodan na predmet {Ime}.");
        //}

        //public List<Ucenik> DajSveUcenike()
        //{
        //    List<Ucenik> ucenici = new List<Ucenik>();
        //    foreach (Razred razred in Razredi)
        //    {
        //        ucenici.AddRange(razred.Ucenici);
        //    }
        //    return ucenici;
        //}

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

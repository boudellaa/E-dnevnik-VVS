using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_dnevnik
{
    public class Skola
    {
        public string Ime { get; set; }
        public List<Razred> Razredi { get; set; }

        public Skola(string ime)
        {
            Ime = ime;
            Razredi = new List<Razred>();
        }

        public void DodajRazred(Razred razred)
        {
            Razredi.Add(razred);
            Console.WriteLine($"Razred {razred.Ime} dodan u školu {Ime}.");
        }

        public void PrikaziRazrede()
        {
            if (Razredi.Count == 0)
            {
                throw new InvalidOperationException("U školi nema razreda.");
            }
            Console.WriteLine($"Razredi u školi {Ime}:");
            foreach (var razred in Razredi)
            {
                Console.WriteLine($"{razred.Ime}");
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_dnevnik
{
    public class Razred
    {
        public string Ime { get; set; }
        public List<Ucenik> Ucenici { get; set; }

        public Razred(string ime)
        {
            Ime = ime;
            Ucenici = new List<Ucenik>();
        }

        public void DodajUcenika(Ucenik ucenik)
        {
            Ucenici.Add(ucenik);
            Console.WriteLine($"Učenik {ucenik.Ime} {ucenik.Prezime} dodan u razred {Ime}.");
        }

        public void PrikaziUcenike()
        {
            if (Ucenici.Count == 0)
            {
                throw new InvalidOperationException("U razredu nema učenika.");
            }
            Console.WriteLine($"Učenici u razredu {Ime}:");
            foreach (var ucenik in Ucenici)
            {
                Console.WriteLine($"{ucenik.Ime} {ucenik.Prezime} ");
            }
        }
    }


}

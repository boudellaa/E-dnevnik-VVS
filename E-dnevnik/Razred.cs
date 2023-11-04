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

        public List<Razred_Predmet> Razredi_Predmeti { get; set; } = new();


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

        //public void DodajPredmet(Predmet predmet)
        //{
        //    Predmeti.Add(predmet);
        //    //Console.WriteLine($"Učenik {ucenik.Ime} {ucenik.Prezime} dodan u razred {Ime}.");
        //}

        public void IzbaciUcenika(Ucenik ucenik)
        {
            Ucenici.Remove(ucenik);
            Console.WriteLine($"Učenik {ucenik.Ime} {ucenik.Prezime} izbačen iz razreda {Ime}.");
        }

        //public void IzbaciPredmet(Predmet predmet)
        //{
        //    Predmeti.Remove(predmet);
        //}
    }


}

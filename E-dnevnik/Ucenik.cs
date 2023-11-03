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

        public string Sifra { get; set; }

        public List<Ocjena> Ocjene { get; set; }

        public List<Komentar> Komentari { get; set; }

        public Razred Razred { get; set; }

       

        public Ucenik( string ime, string prezime, string korisnickoIme, string sifra)
        {    
            Ime = ime;
            Prezime = prezime;
            KorisnickoIme = korisnickoIme;
            Sifra = sifra;
            Ocjene = new List<Ocjena>();
            Komentari = new List<Komentar>();
        }
        public Ucenik(string ime, string prezime, string korisnickoIme, string sifra, Razred razred)
        {
            Ime = ime;
            Prezime = prezime;
            KorisnickoIme = korisnickoIme;
            Sifra = sifra;
            Ocjene = new List<Ocjena>();
            Komentari = new List<Komentar>();
            Razred = razred;
        }

        public void DodajOcjenu(Ocjena ocjena)
        {
            if (ocjena.Vrijednost < 1 || ocjena.Vrijednost > 5)
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
        public List<Predmet> DajMojePredmete()
        {
            var temp = new List<Predmet>();

            
            foreach(var x in Razred.Razredi_Predmeti.Where(p => p.Razred == Razred))
            {
                temp.Add(x.Predmet);
            }
            return temp;

        }
    }

}

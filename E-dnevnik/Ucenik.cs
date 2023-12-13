using E_dnevnik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ednevnik
{
    public class Ucenik : IUcenik
    {


        public string Ime { get; set; }
        public string Prezime { get; set; }

        public string KorisnickoIme { get; set; }

        public string Sifra { get; set; }

        public List<Ocjena> Ocjene { get; set; } = new();

        public List<Komentar> Komentari { get; set; } = new();

        public Razred Razred { get; set; }

       
        
        public Ucenik( string ime, string prezime, string korisnickoIme, string sifra)
        {    
            Ime = ime;
            Prezime = prezime;
            KorisnickoIme = korisnickoIme;
            Sifra = sifra;
            Ocjene = new List<Ocjena>();
         
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
        public override string ToString()
        {
            return $"Ime: {Ime}, Prezime: {Prezime}, KorisnickoIme: {KorisnickoIme}, Sifra: {Sifra}";
        }

        public List<Ocjena> DajSortiraneOcjenePoVrijednosti(List<Ocjena> ocjene)
        {
            return (List<Ocjena>)ocjene.OrderBy(p => p.Vrijednost).ToList();
        }

        public Double DajProsjekUcenikaNaPredmetu(Predmet predmet)
        {
            Double temp = 0;
            int brojOcjena = 0;
            foreach(var ocjena in Ocjene)
            {
                if(ocjena.Predmet.Ime == predmet.Ime)
                {
                    temp += ocjena.Vrijednost;
                    brojOcjena++;
                }
            }
            if (brojOcjena > 0) return temp / brojOcjena;
            return 0;
        }


        public Double DajUkupanProsjekUcenika()
        {
            Double temp = 0;
            foreach (var ocjena in Ocjene)
                temp += ocjena.Vrijednost;
            if(Ocjene.Count>0)
            return temp / Ocjene.Count;
            return 0;
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

        public List<Ocjena> DajOcjeneIzPredmeta(Predmet predmet)
        {
            return this.Ocjene.Where(oc => oc.Predmet.Ime.Equals(predmet.Ime)).ToList();
        }
    }

}

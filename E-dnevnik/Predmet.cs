using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ednevnik
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

        public Double DajProsjekPredemta()
        {
            Double temp = 0;
            double brojac = 0;
            foreach(var x in Razredi_Predmeti)
            {
                if(x.Predmet.Ime == this.Ime)
                {
                    foreach(var ucenik in x.Razred.Ucenici)
                    {
                        
                        temp = ucenik.DajProsjekUcenikaNaPredmetu(this);
                        if (Convert.ToInt32(temp) != 0) brojac++;
                    }
                }
            }
            if (brojac != 0) return temp / brojac;
            return 0;
        }


        public List<Ucenik> DajSveUcenikeNaPredmetu()
        {
            var temp = new List<Ucenik>();
            foreach (var x in Razredi_Predmeti)
            {
                if(x.Predmet.Ime == Ime)
                {
                    temp.AddRange(x.Razred.Ucenici);
                }
            }
            return temp;
        }

        public List<Razred> DajSveRazredeNaPredmetu()
        {
            var razredi = new List<Razred>();
            foreach (var x in Razredi_Predmeti)
            {
                if (!razredi.Contains(x.Razred))
                {
                    razredi.Add(x.Razred);
                }
            }

            return razredi;
        }

        



      
    }

}

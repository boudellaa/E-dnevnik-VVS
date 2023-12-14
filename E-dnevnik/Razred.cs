using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ednevnik
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

        public Double DajProsjekRazreda()
        {
            Double temp = 0;
            int brojac = 0;
            List<double> prosjekRazreda = new List<double>();
            foreach (var x in Ucenici)
            {
                temp = 0;
                brojac = 0;
                foreach(var ocjena in x.Ocjene)
                {
                    if (!Ocjena.ValidirajOcjenu(ocjena.Vrijednost))
                    {
                        throw new Exception("Ocjena mora imati vrijednost od 1 do 5!");
                    }
                    if (ocjena.Vrijednost != 0)
                    {
						temp += ocjena.Vrijednost;
                        brojac++;
					}

                }
                prosjekRazreda.Add(temp/brojac);
            }
            if (brojac == 0) throw new Exception("Razred nema ocjena!"); 
            return prosjekRazreda.Average();
        }

        public void DodajUcenika(Ucenik ucenik)
        {
            for(var i=0;i<Ucenici.Count;i++)
            {
                if (Ucenici[i].KorisnickoIme == ucenik.KorisnickoIme)
                    throw new Exception("Ucenik već postoji u razredu!");
            }
			ucenik.Razred = this;
			Ucenici.Add(ucenik);   
        }

        public void IzbaciUcenika(Ucenik ucenik)
        {
            
        }
        
        
    }


}

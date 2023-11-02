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
        
        public List<Predmet> Predmeti { get; set; } 

        public Nastavnik(string ime, string prezime)
        {
            Ime = ime;
            Prezime = prezime;
            
        }

        public void DodajOcjenuUceniku(Ucenik ucenik, int ocjena)
        {
            ucenik.DodajOcjenu(ocjena);
        }


		public IEnumerable<Ucenik> DajSvojeUcenike()
		{
			List<Ucenik> temp = new List<Ucenik>();
			if (Predmeti.Count == 0)
			{
				throw new InvalidOperationException("Nastavnik nema predmeta.");
			}

			Console.WriteLine($"Učenici nastavnika {Ime} {Prezime}:");

			foreach (var predmet in Predmeti)
			{
				try
				{
					foreach (var ucenik in predmet.DajSveUcenike())
					{
						temp.Add(ucenik);
					}
				}
				catch (Exception e)
				{
					continue;
				}
			}
			if (temp.Count == 0) throw new Exception("Nastavnik nema učenika!");
			return temp;
		}

		/*      public void PrikaziSvojeUcenike()
			  {
				  if (Predmeti.Count == 0)
				  {
					  throw new InvalidOperationException("Nastavnik nema predmeta.");
				  }

				  Console.WriteLine($"Učenici nastavnika {Ime} {Prezime}:");
				  foreach (var ucenik in Ucenici)
				  {
					  Console.WriteLine($"{ucenik.Ime} {ucenik.Prezime}");
				  }
			  }
		*/
		public Double IzracunajProsjekRazreda()
        {
			try
			{
				var ucenici = DajSvojeUcenike();
				return ucenici.Average(u => u.Ocjene.Average());
			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}
            
        }

        public Komentar UnesiKomentar(String opis, Ucenik ucenik)
        {
            if (opis == null)
            {
                throw new Exception("Komentar ne može biti prazan!");
            }
            return new Komentar(this, ucenik, opis);
        }
    }

}

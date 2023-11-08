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

        public Predmet Predmet {  get; set; }

		

        public Nastavnik(string ime, string prezime,string korisnickoIme, string sifra)
        {
			
            Ime = ime;
            Prezime = prezime;
            
            KorisnickoIme = korisnickoIme;
            Sifra = sifra;
			

        }

		public void UpisiOcjenu(Ucenik ucenik, int vrijednost)
		{
			if (vrijednost < 1 || vrijednost > 5) throw new Exception("Ocjena mora biti u opsegu 1 do 5!");
			ucenik.Ocjene.Add(new Ocjena(vrijednost, ucenik, Predmet, DateTime.Now));
		}


		public void NoviKomentar(Ucenik ucenik, string opis)
		{
			if (opis == null) throw new Exception("Komentar ne moze biti prazan!");
			ucenik.Komentari.Add(new Komentar(this, ucenik, opis));
		}

        
		
		      
		
		public Double IzracunajProsjekRazreda()
        {
			try
			{
				var ucenici = Predmet.DajSveUcenikeNaPredmetu();
				return ucenici.Average(u => u.DajUkupanProsjekUcenika());
			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}
            
        }
		
       
		
    }

}

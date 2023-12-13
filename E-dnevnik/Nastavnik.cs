using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ednevnik
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
			try
			{
                Ocjena.ValidirajOcjenu(vrijednost);
            }
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			if (ucenik == null)
				throw new Exception("Ucenik ne postoji!");
			ucenik.Ocjene.Add(new Ocjena(vrijednost, ucenik, Predmet, DateTime.Now));
		}
		
		public void NoviKomentar(Ucenik ucenik, string opis)
		{
			if (opis == null) throw new Exception("Komentar ne moze biti prazan!");
			if (ucenik == null) throw new Exception("Ucenik nije definisan!");
			if (ucenik.Komentari == null) throw new Exception("Komentari ucenika nisu definisani!");
			ucenik.Komentari.Add(new Komentar(this, ucenik, opis));
		}

        public void NapraviPrisustvo(List<Ucenik> ucenici)
		{
			var zapis = new List<Tuple<string, string>>();
			foreach(var ucenik in ucenici)
			{
				zapis.Add(new Tuple<string, string>(ucenik.Ime, ucenik.Prezime));
			}
            using (var sr = new StreamWriter("../../../../E-dnevnik/prisustvo.csv", false, Encoding.UTF8))
			{
				using (var csv = new CsvWriter(sr, CultureInfo.InvariantCulture))
				{
                    csv.WriteField("item1");
                    csv.WriteField("item2");
                    csv.NextRecord();
                    csv.WriteRecords(zapis);
				}
			}
		}
		public List<Ucenik> DajSveUcenikeNastavnika()
		{
			foreach(var x in Predmet.Razredi_Predmeti)
			{
				if(x.Predmet.Nastavnik.Ime==Ime)
				{
					if(x.Razred.Ucenici==null||x.Razred.Ucenici.Count==0)
						throw new Exception("Nastavnik nema ucenika!");
					return x.Razred.Ucenici;
				}
			}
			throw new Exception("Nastavnik nema ucenika!");
		}
		
		public Double IzracunajProsjekRazreda()
        {
				var ucenici = Predmet.DajSveUcenikeNaPredmetu();
				return ucenici.Average(u => u.DajUkupanProsjekUcenika());
        }
    }
}

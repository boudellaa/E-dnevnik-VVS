using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ednevnik
{
	public class Komentar
	{
		public Ucenik Ucenik { get; set; }

		public Nastavnik Nastavnik { get; set; }

		public String Opis{ get; set; }

		

		public Komentar(Nastavnik nastavnik, Ucenik ucenik, String opis)
		{
			Nastavnik = nastavnik;
			Ucenik = ucenik;
			Opis = opis;
		}

		public Komentar()
		{
			
		}	
		 
		public static bool ValidirajKomentar(String opis)
		{
			if (opis == null || opis.Length == 0)
				throw new Exception("Komentar ne smije biti prazan!");
			return true;
		}
	}
}

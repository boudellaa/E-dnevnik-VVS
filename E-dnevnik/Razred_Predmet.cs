using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ednevnik
{
	public class Razred_Predmet
	{
		public Predmet Predmet { get; set; }

		public Razred Razred { get; set; }

		public Razred_Predmet(Razred razred, Predmet predmet)
		{
			Predmet = predmet;
			Razred = razred;
		}
	}
}

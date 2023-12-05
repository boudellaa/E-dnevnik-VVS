using Ednevnik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_dnevnik
{
	public class UcenikDummy : IUcenik
	{
		public UcenikDummy() { }

		public List<Predmet> DajMojePredmete()
		{
			throw new NotImplementedException();
		}

		public List<Ocjena> DajOcjeneIzPredmeta(Predmet predmet)
		{
			throw new NotImplementedException();
		}

		public double DajProsjekUcenikaNaPredmetu(Predmet predmet)
		{
			throw new NotImplementedException();
		}

		public List<Ocjena> DajSortiraneOcjenePoVrijednosti(List<Ocjena> ocjene)
		{
			throw new NotImplementedException();
		}

		public double DajUkupanProsjekUcenika()
		{
			throw new NotImplementedException();
		}
	}
}

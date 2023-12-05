using Ednevnik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_dnevnik
{
	public interface IUcenik
	{
		public List<Ocjena> DajSortiraneOcjenePoVrijednosti(List<Ocjena> ocjene);
		public Double DajProsjekUcenikaNaPredmetu(Predmet predmet);
		public Double DajUkupanProsjekUcenika();
		public List<Predmet> DajMojePredmete();
		public List<Ocjena> DajOcjeneIzPredmeta(Predmet predmet);


	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_dnevnik
{
    public class Ocjena
    {
        public int Vrijednost { get; set; }
        
        public Ucenik Ucenik { get; set; }

        public Predmet Predmet { get; set; }


        public Ocjena() { }

        public Ocjena(int vrijednost,Ucenik ucenik, Predmet predmet)
        {
            Vrijednost = vrijednost;
            Ucenik = ucenik;
            Predmet = predmet;
        }
    }
}

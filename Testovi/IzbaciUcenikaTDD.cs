using Ednevnik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testovi
{
    [TestClass]
    public class IzbaciUcenikaTDD
    {
        private E_Dnevnik ednevnik;
        private Razred razred;
        private List<Ucenik> ucenici;
        
        [TestInitialize]
        public void TestInitialize()
        {
            ednevnik = new E_Dnevnik();
            razred = new Razred("Test");
             ucenici = new List<Ucenik>() { new Ucenik("Test1", "Test1", "test", "test", razred), new Ucenik("test2", "test2", "test2", "test2", razred), new Ucenik("test3", "test3", "test3", "test3")};

            razred.DodajUcenika(ucenici[0]);
            razred.DodajUcenika(ucenici[1]);
            
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IzbaciUcenika_KojiNijeURazredu_BacaException()
        {
            razred.IzbaciUcenika(ucenici[2]);
        }

        [TestMethod]
        public void IzbaciUcenika_UspjesnoIzbacenUcenik()
        {
            Assert.IsTrue(razred.Ucenici.Contains(ucenici[1]));
            razred.IzbaciUcenika(ucenici[1]);
            Assert.IsTrue(!razred.Ucenici.Contains(ucenici[1]));
        }

    }
}

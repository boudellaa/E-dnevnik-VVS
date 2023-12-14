using Ednevnik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Testovi
{
    [TestClass]
    public class hashTDD
    {
        private E_Dnevnik ednevnik;

        [TestInitialize]
        public void TestInitialize()
        {
            ednevnik = new E_Dnevnik();

        }
        [TestMethod]
        public void HashPassword_ReturnCorrectPassword()
        {
            Assert.AreEqual("1",ednevnik.HashPassword("", new byte[64]));
        }
    }
}

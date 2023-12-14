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
		public void HashPassword_ReturnEmptyString()
		{
			var salt = new byte[64];
			Assert.AreEqual("", ednevnik.HashPassword(""));
		}


		[TestMethod]
		public void HashPassword_ReturnCorrectHash()
		{
			var password = "test123";
			var salt = new byte[64];
			var hashedPassword = ednevnik.HashPassword(password);
			var result = ednevnik.VerifyPassword(password, hashedPassword);
			Assert.IsTrue(result);
		}


	}
}
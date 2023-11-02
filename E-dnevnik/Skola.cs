using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace E_dnevnik
{
    public class Skola
    {
        public string Ime { get; set; }
        public List<Razred> Razredi { get; set; }

        public List<Ucenik> Ucenici { get; set; }

        public List<Nastavnik> Nastavnici { get; set; }

        public Skola(string ime)
        {
            Ime = ime;
            Razredi = new List<Razred>();
            Ucenici = new List<Ucenik>();
            Nastavnici = new List<Nastavnik>();
        }

        public void DodajRazred(Razred razred)
        {
            Razredi.Add(razred);
            Console.WriteLine($"Razred {razred.Ime} dodan u školu {Ime}.");
        }

        public void PrikaziRazrede()
        {
            if (Razredi.Count == 0)
            {
                throw new InvalidOperationException("U školi nema razreda.");
            }
            Console.WriteLine($"Razredi u školi {Ime}:");
            foreach (var razred in Razredi)
            {
                Console.WriteLine($"{razred.Ime}");
            }
        }

        

        public String HashPassword(String password, out byte[] salt)
        {
			HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
			salt = RandomNumberGenerator.GetBytes(64);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                350000,
                hashAlgorithm,
                64);

            return Convert.ToHexString(hash);
        }

        public bool VerifyPassword(string password, string hash, byte[] salt)
        {
			HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
			var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, 350000, hashAlgorithm, 64);
			return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
		}

    }

}

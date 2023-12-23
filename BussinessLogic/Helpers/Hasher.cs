using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Helpers
{
    public static class Hasher
    {
        public static byte[] GetHash(string input)
        {
            using (HashAlgorithm algorithm = SHA256.Create()) 
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
        }

        public static string GetHashString(string input)
        {
            StringBuilder sb = new();
            foreach (byte b in GetHash(input))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}

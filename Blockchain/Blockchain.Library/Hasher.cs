using System;
using System.Security.Cryptography;
using System.Text;

namespace Blockchain.Library
{
    public class Hasher
    {
        public static string Hash(string input)
        {
            HashAlgorithm hasher = SHA256.Create();
            var hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
            var hashedString = BitConverter.ToString(hash).Replace("-", String.Empty);
            return hashedString;
        }
    }

}

using System.Security.Cryptography;

namespace Balances.Utilities
{
    public class HashSHA256
    {
        public static string CalcularHash(string dato)
        {
            var hashbinario = GetHashSha256(dato);

            return BytesToString(hashbinario);

        }

        // Compute the file's hash.
        private static byte[] GetHashSha256(string dato)
        {
            using (FileStream stream = File.OpenRead(dato))
            {
                var hash = SHA256.HashData(stream);
                return hash;
            }
        }

        // Return a byte array as a sequence of hex values.
        private static string BytesToString(byte[] bytes)
        {
            string result = "";
            foreach (byte b in bytes) result += b.ToString("x2");
            return result;
        }
    }
}

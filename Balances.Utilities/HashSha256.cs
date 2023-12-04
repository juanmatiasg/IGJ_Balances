using System.Security.Cryptography;

namespace Balances.Utilities
{
    public class HashSha256
    {
        public static string CalcularHash(string filename)
        {
            var hashbinario = GetHashSha256(filename);

            return BytesToString(hashbinario);

        }

        // Compute the file's hash.
        private static byte[] GetHashSha256(string filename)
        {
            using (FileStream stream = File.OpenRead(filename))
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

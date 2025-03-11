using System.Security.Cryptography;
using System.Text;

namespace HPHA.UiPath.Core.Generators
{
    public static class DeterministicIdGenerator
    {
        /// <summary>
        /// Generates a short deterministic ID from the input string.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string GenerateShortDeterministicID(string input, int size = 16)
        {
            byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(input));
            string base64 = Convert.ToBase64String(hash);

            // Make it URL-safe and take only first N(size) characters for compactness
            return base64[..size].Replace("/", "_").Replace("+", "-");
        }
    }
}

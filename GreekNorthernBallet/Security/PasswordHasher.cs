using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Text;

namespace GreekNorthernBallet.Security
{

  public static class PasswordHasher
  {
    private const string SEPARATOR = "#";

    public static string hashPassword(string password, byte[] salt = null)
    {
     
      if (salt==null)
      {
        // generate a 128-bit salt using a secure PRNG
        salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
          rng.GetBytes(salt);
        }
      }

      // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
      string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
          password: password,
          salt: salt,
          prf: KeyDerivationPrf.HMACSHA256,
          iterationCount: 10000,
          numBytesRequested: 256 / 8));

      string stringSalt = byteArrayToString(salt);

      return $"{stringSalt}{SEPARATOR}{hashed}";
    }

    public static bool validatePassword(string password, string hash)
    {
      var separatorIndex = hash.IndexOf(SEPARATOR);
      var salt = hash.Substring(0, separatorIndex);

      string hashed = hashPassword(password, stringToByteArray(salt));

      return hashed == hash;
    }

    public static byte[] stringToByteArray(string someString)
    {
      return Convert.FromBase64String(someString);
    }

    public static string byteArrayToString(byte[] someByte)
    {
      return Convert.ToBase64String(someByte);
    }
  }
      
}

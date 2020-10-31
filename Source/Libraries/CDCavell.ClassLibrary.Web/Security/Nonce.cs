using Org.BouncyCastle.Security;
using System;
using System.Security.Cryptography;

namespace CDCavell.ClassLibrary.Web.Security
{
    /// <summary>
    /// Nonce - random value used to thwart replay attacks.
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 10/15/2020 | Initial build |~ 
    /// </revision>
    public static class Nonce
    {
        private static readonly SecureRandom Random = new SecureRandom();

        /// <summary>
        /// Generates random value used to thwart replay attacks.
        /// </summary>
        /// <returns>string</returns>
        public static string Calculate()
        {
            //Allocate a buffer
            var ByteArray = new byte[20];
            Random.NextBytes(ByteArray);
            //Base64 encode and then return
            return Convert.ToBase64String(ByteArray);
        }
    }
}

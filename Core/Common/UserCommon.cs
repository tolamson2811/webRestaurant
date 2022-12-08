using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Core.Common
{
    public class UserCommon
    {
        public static string CreateSaltKey()
        {
            byte[] data = new byte[32];
            new RNGCryptoServiceProvider().GetBytes(data);      
            return Convert.ToBase64String(data);
        }

        public static string CreateHashFromSaltAndPassword(string saltKey,string passWord)
        {
           var salt = Convert.FromBase64String(saltKey);
           return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: passWord,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace CKANSyncApplications.Utility.Security
{
    /// <summary>
    /// This object is used to encrypt and decrypt the input string.
    /// </summary>
    public class DataProtection
    {
        private static byte[] entropy = { 117, 201, 181, 49, 63, 153 }; //the entropy

        public static string Encrypt(string text)
        {
            // first, convert the text to byte array 
            byte[] originalText = Encoding.Unicode.GetBytes(text);

            // then use Protect() to encrypt your data 
            byte[] encryptedText = ProtectedData.Protect(originalText, entropy, DataProtectionScope.LocalMachine);

            //and return the encrypted message 
            return Convert.ToBase64String(encryptedText); 
        }

        public static string Decrypt(string text)
        {
            // the encrypted text, converted to byte array 
            byte[] encryptedText = Convert.FromBase64String(text);

            // calling Unprotect() that returns the original text 
            byte[] originalText = ProtectedData.Unprotect(encryptedText, entropy, DataProtectionScope.LocalMachine);

            // finally, returning the result 
            return Encoding.Unicode.GetString(originalText);
        }
    }
}

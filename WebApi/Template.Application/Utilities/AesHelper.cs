using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Template.Application.Utilities
{
    public class AesHelper
    {
        private readonly string _aesKey;

        public AesHelper(string aesKey)
        {
            _aesKey = aesKey;

        }
        public string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(_aesKey);
                aesAlg.IV = new byte[16]; // AES uses a 16-byte IV. This is just an example with a zero IV.

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public string Decrypt(string cipherText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(_aesKey);
                aesAlg.IV = new byte[16]; // Ensure the same IV is used for decryption.

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }

        // Encrypt and decrypt an int
        public string EncryptInt(int value)
        {
            string strValue = value.ToString();
            return Encrypt(strValue);
        }

        public int DecryptInt(string encryptedValue)
        {
            string strValue = Decrypt(encryptedValue);
            return int.Parse(strValue);
        }

        // Encrypt and decrypt a bool
        public string EncryptBool(bool value)
        {
            string strValue = value.ToString();
            return Encrypt(strValue);
        }

        public bool DecryptBool(string encryptedValue)
        {
            string strValue = Decrypt(encryptedValue);
            return bool.Parse(strValue);
        }

        // Encrypt and decrypt a DateTime
        public string EncryptDateTime(DateTime value)
        {
            string strValue = value.ToString("o"); // Use ISO 8601 format
            return Encrypt(strValue);
        }

        public DateTime DecryptDateTime(string encryptedValue)
        {
            string strValue = Decrypt(encryptedValue);
            return DateTime.Parse(strValue, null, System.Globalization.DateTimeStyles.RoundtripKind);
        }


    }
}

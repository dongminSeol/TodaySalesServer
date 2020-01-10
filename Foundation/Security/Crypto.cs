using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Foundation;

namespace Foundation.Security
{
    public class Crypto : IBaseCrypto
    {
        public string HmacSha512(string key, string text)
        {
            try
            {
                byte[] keyBytes = Convert.FromBase64String(key);
                byte[] textBytes = Encoding.UTF8.GetBytes(text);
                byte[] hasgBytes;
                using (HMACSHA512 hmac = new HMACSHA512(keyBytes))
                {
                    hasgBytes = hmac.ComputeHash(textBytes);
                };

                return Convert.ToBase64String(hasgBytes);
            }
            catch(Exception e)
            {
                return string.Empty;
            }

        }

        public string RsaDecrypt(string encryptedText, string privateKey)
        {
            try
            {
                byte[] outBytes = Convert.FromBase64String(encryptedText);

                string decryptResult;
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXml(privateKey);
                    byte[] decryptBytes = rsa.Decrypt(outBytes, false);
                    decryptResult = Encoding.UTF8.GetString(decryptBytes);
                }
                return decryptResult;
            }
            catch(Exception e)
            {
                return string.Empty;
            }
        }

        public string RsaEncrypt(string nomalText, string publicKey)
        {
            try
            {
                byte[] inBytes = Encoding.UTF8.GetBytes(nomalText);

                string encryptResult;
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(publicKey);
                    byte[] encryptBytes = rsa.Encrypt(inBytes, false);
                    encryptResult = Convert.ToBase64String(encryptBytes);
                };
                return encryptResult;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }
    }
}

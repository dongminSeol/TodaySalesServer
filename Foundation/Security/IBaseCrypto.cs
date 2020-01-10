using System;
using System.Collections.Generic;
using System.Text;

namespace Foundation.Security
{
    public interface IBaseCrypto
    {
        string RsaEncrypt(string nomalText, string publicKey);

        string RsaDecrypt(string encryptText, string privateKey);

        string HmacSha512(string key, string text);
    }
}

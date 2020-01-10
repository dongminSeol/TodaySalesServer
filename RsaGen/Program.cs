using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RsaGen
{
    class Program
    {
        static void Main(string[] args)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            // public key generate.
            string publicKey = rsa.ToXmlString(false);

            // private key generate.
            string privateKey = rsa.ToXmlString(true);

            Console.ReadKey();
        }
    }
}

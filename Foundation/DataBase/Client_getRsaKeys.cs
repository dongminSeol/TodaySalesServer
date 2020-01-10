using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Foundation.DataBase
{
    public class Client_getRsaKeys
    {
        [Key]
        public int Seq { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }
}

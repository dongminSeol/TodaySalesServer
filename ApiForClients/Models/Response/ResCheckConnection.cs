using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForClients.Models.Response
{
    public class ResCheckConnection
    {
        public string HashKey { get; set; }
        public int Nonce { get; set; }
    }
}

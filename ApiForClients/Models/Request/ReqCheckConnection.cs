using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForClients.Models.Request
{
    public class ReqCheckConnection
    {
        public int KeySeq { get; set; }
        public string HashGuid { get; set; }
        public long TimeStampUTC { get; set; }
    }
}

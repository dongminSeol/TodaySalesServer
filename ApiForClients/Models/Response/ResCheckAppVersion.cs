using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForClients.Models.Response
{
    public class ResCheckAppVersion
    {
        public string AppVersion { get; set; }
        public long TimeStampUTC { get; set; }
    }
}

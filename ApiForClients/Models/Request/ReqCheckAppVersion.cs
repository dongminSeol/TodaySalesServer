using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForClients.Models.Request
{
    public class ReqCheckAppVersion
    {
        public string AppVersion { get; set; }
        public string OsType { get; set; }
    }
}

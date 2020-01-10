using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForClients.Models.DataBase
{
    public class Client_getAppVersion
    {
        [Key]
        public int Seq { get; set; }
        public string OsType { get; set; }
        public string Version { get; set; }
    }
}

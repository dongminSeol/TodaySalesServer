using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForClients.Models.Response
{
    public class ResSignIn
    {
        public Guid UserIdCode { get; set; }
        public string UserName { get; set; }
        public string UserNickName { get; set; }
    }
}

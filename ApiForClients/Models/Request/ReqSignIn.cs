using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForClients.Models.Request
{
    public class ReqSignIn
    {
        [Required]
        public string UserId { get; set; }
        [Required, MinLength(8)]
        public string UserPassword { get; set; }
    }
}

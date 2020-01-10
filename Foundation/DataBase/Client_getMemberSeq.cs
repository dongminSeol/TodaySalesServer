using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Foundation.DataBase
{
    public class Client_getMemberSeq
    {
        [Key]
        public long MemberSeq { get; set; }
        public Guid MemberUniqueCode { get; set; }
    }
}

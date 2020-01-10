using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForClients.Models.DataBase
{
    public class Client_getMember
	{
		[Key]
		public long M_seq { get; set; }
		public Guid Uniqueidentifier { get; set; }
		public DateTime Mp_insertDateTime { get; set; }
		public DateTime Mp_updateDateTime { get; set; }
		public string C_code_applicationType { get; set; }
		public string Mp_id { get; set; }
	}
}

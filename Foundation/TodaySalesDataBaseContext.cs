using ApiForClients.Models.DataBase;
using Foundation.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Foundation
{
    public partial class TodaySalesDataBaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:todaysales.database.windows.net,1433;Initial Catalog=TodaySalesCore;Persist Security Info=False;User ID=todaysalesadmin;Password=todaysales1234!@#$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }

    // Model for clients.
    public partial class TodaySalesDataBaseContext
    {
        public virtual DbSet<Client_getAppVersion> GetAppVersion { get; set; }
        public virtual DbSet<Client_getRsaKeys> GetRsaKey { get; set; }
        public virtual DbSet<Client_getMemberSeq> GetMemberSeq { get; set; }
		public virtual DbSet<Client_getMember> GetMember { get; set; }
    }

    // Model for seller.
    public partial class TodaySalesDataBaseContext
    {

    }
}

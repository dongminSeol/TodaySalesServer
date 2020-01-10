using ApiForClients.Models.Response;
using Foundation;
using Foundation.Common;
using Foundation.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiForClients.Attributes
{
    public class ValidateSign : ActionFilterAttribute
    {
        private readonly bool isOnlyForSeller;

        public ValidateSign(bool isOnlyForSeller)
        {
            this.isOnlyForSeller = isOnlyForSeller;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var reqContext = context.HttpContext.Request;
                var session = context.HttpContext.Session;

                Guid memberUniqueCode;
                var resultConverted = Guid.TryParse(reqContext.Headers["MemberUniqueCode"].ToString().Trim(), out memberUniqueCode);
                if (!resultConverted) throw new HttpException("It can not find memberUniqueCode of guid.") { HttpCode = HttpStatusCode.NotFound };

                using (var db = new TodaySalesDataBaseContext())
                {
                    var dataResult = db.GetMemberSeq.FromSql($"exec sp_client_getMemberSeq @p_uniqueCode",
                        new SqlParameter("@p_uniqueCode", memberUniqueCode)
                    ).AsNoTracking().SingleOrDefault();

                    if (dataResult == null) throw new HttpException("It can not find data from DB.") { HttpCode = HttpStatusCode.NotFound };

                    context.HttpContext.Items["memberUniqueCode"] = dataResult.MemberSeq;
                }
            }
            catch (HttpException e)
            {
                context.Result = new StatusCodeResult((int)e.HttpCode);
                return;
            }
        }
    }
}

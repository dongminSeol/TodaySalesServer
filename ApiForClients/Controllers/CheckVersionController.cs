using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ApiForClients.Models.Request;
using ApiForClients.Models.Response;
using Foundation;
using Foundation.Request;
using Foundation.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ApiForClients.Controllers
{
    [ApiController, Route("Certify/[controller]"), Produces("application/json")]
    public class CheckVersionController : ControllerBase
    {
        private readonly TodaySalesDataBaseContext db;

        public CheckVersionController(TodaySalesDataBaseContext todaySalesDataBaseContext)
        {
            db = todaySalesDataBaseContext;
        }

        [HttpGet("{id}")]
        public ActionResult<IBaseResponse> Get(int id)
        {
            var appInfo = HttpContext.Request.Headers["appInfo"];
            var requestParameter = JsonConvert.DeserializeObject<Request<ReqCheckAppVersion>>(appInfo);

            var dataResult = db.GetAppVersion.FromSql($"exec sp_client_getAppVersion @p_osType",
                new SqlParameter("@p_osType", requestParameter.RequestData.OsType)
            ).AsNoTracking().SingleOrDefault();

            if (dataResult == null)
            {
                return new Response<ResCheckAppVersion>()
                {
                    ResultCode = (uint)ResponseCode.Error_notExistFromDB,
                    ResultMessage = ResponseCode.Error_notExistFromDB.GetDescription()
                };
            }

            var timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            HttpContext.Session.SetString("timeStamp", timeStamp.ToString());

            return new Response<ResCheckAppVersion>()
            {
                ResultCode = (int)ResponseCode.Ok,
                ResultMessage = ResponseCode.Ok.GetDescription(),
                ResultData = new ResCheckAppVersion()
                {
                    AppVersion = dataResult.Version,
                    TimeStampUTC = timeStamp
                }
            };
        }
    }
}
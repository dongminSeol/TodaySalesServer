using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiForClients.Models.Response;
using Foundation.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Foundation;
using Newtonsoft.Json;
using ApiForClients.Models.Request;
using Foundation.Request;
using Foundation.Security;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace ApiForClients.Controllers
{
    [ApiController, Route("Certify/[controller]"), Produces("application/json")]
    public class CheckConnectionController : ControllerBase
    {
        private readonly TodaySalesDataBaseContext db;

        public CheckConnectionController(TodaySalesDataBaseContext todaySalesDataBaseContext)
        {
            db = todaySalesDataBaseContext;
        }

        [HttpGet("{id}")]
        public ActionResult<IBaseResponse> Get(int id)
        {
            Crypto crypto = new Crypto();
            Random random = new Random();
            long currentTimeStampUTC = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            const int timeGapSeconds = 30;

            var securityInfo = HttpContext.Request.Headers["securityInfo"];
            var requestParameter = JsonConvert.DeserializeObject<Request<ReqCheckConnection>>(securityInfo);


            // To check timestamp.
            if (requestParameter.RequestData.TimeStampUTC < currentTimeStampUTC - timeGapSeconds)
            {
                return new Response<ResCheckConnection>()
                {
                    ResultCode = (uint)ResponseCode.Overtime_timeStamp,
                    ResultMessage = ResponseCode.Overtime_timeStamp.GetDescription()
                };
            }

            var dataResult = db.GetRsaKey.FromSql($"exec sp_client_getRsaKeys @p_seq",
                new SqlParameter("@p_seq", requestParameter.RequestData.KeySeq)
            ).AsNoTracking().SingleOrDefault();
            if (dataResult == null)
            {
                return new Response<ResCheckConnection>()
                {
                    ResultCode = (uint)ResponseCode.Error_notExistFromDB,
                    ResultMessage = ResponseCode.Error_notExistFromDB.GetDescription()
                };
            }

            // To check crypto of hashguid.
            var hashKeyReceived = crypto.RsaDecrypt(requestParameter.RequestData.HashGuid, dataResult.PrivateKey);
            if (hashKeyReceived.Length != 32)
            {
                return new Response<ResCheckConnection>()
                {
                    ResultCode = (uint)ResponseCode.Guid_invalid,
                    ResultMessage = ResponseCode.Guid_invalid.GetDescription()
                };
            }

            var hashKey = hashKeyReceived + HttpContext.Session.Id.Replace("-",string.Empty);
            HttpContext.Session.SetString("hashKey", hashKey);
            var nonce = random.Next(10000, 99999);
            HttpContext.Session.SetInt32("nonce", nonce);

            return new Response<ResCheckConnection>()
            {
                ResultCode = (uint)ResponseCode.Ok,
                ResultMessage = ResponseCode.Ok.GetDescription(),
                ResultData = new ResCheckConnection()
                {
                    HashKey = hashKey,
                    Nonce = nonce
                }
            };
        }
    }
}
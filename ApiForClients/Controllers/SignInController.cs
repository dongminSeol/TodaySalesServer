using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiForClients.Models;
using ApiForClients.Models.Request;
using ApiForClients.Models.Response;
using Foundation.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Foundation;
using Newtonsoft.Json;
using System.Data.SqlClient;
using Foundation.Request;
using Microsoft.EntityFrameworkCore;

namespace ApiForClients.Controllers
{
    [ApiController, Route("user/[controller]"), Produces("application/json")]
    public class SignInController : ControllerBase
    {

		private readonly TodaySalesDataBaseContext db;

		public SignInController(TodaySalesDataBaseContext todaySalesDataBaseContext)
		{
			db = todaySalesDataBaseContext;
		}

		[HttpGet("{id}")]
        public ActionResult<IBaseResponse> Get()
        {
            var user = new ResSignIn()
            {
                UserIdCode = Guid.NewGuid(),
                UserName = "김도토리",
                UserNickName = "도토리 아저씨 묵사발1"
            };
            var result = new Response<ResSignIn>()
            {
                ResultCode = (uint)ResponseCode.Ok,
                ResultMessage = ResponseCode.Ok.GetDescription(),
                ResultData = user
            };

            return result;
        }

        [HttpPost]
        public ActionResult<IBaseResponse> Post([FromBody]Response<ReqGoogleProfiles> signInRequest)
        {
            //var appInfo = HttpContext.Request.Headers["appInfo"];
            //var requestParameter = JsonConvert.DeserializeObject<Request<ReqGoogleProfiles>>(appInfo);

            //var dataResult = db.GetMember.FromSql($"exec sp_member_list @I_mp_password",
            //	new SqlParameter("@I_mp_password", requestParameter.RequestData.kind)
            //).AsNoTracking().SingleOrDefault();

            var dataResult = db.GetMember.FromSql($"exec sp_member_list @I_kind",
                new SqlParameter("@I_kind", signInRequest.ResultData.kind)
            ).AsNoTracking().SingleOrDefault();

            //유저 정보가 없다면 인서트
            if (dataResult == null)
            {
                dataResult = db.GetMember.FromSql($"exec sp_member_insert @I_c_code_applicationType,@I_mp_id,@I_mp_password",
                    new SqlParameter("I_c_code_applicationType", "Sample"),
                    new SqlParameter("@I_mp_id", signInRequest.ResultData.id),
                    new SqlParameter("@I_mp_password", signInRequest.ResultData.kind)
                ).AsNoTracking().SingleOrDefault();

                //return new Response<ReqGoogleProfiles>()
                //{
                //    ResultCode = (uint)ResponseCode.Error_notExistFromDB,
                //    ResultMessage = ResponseCode.Error_notExistFromDB.GetDescription()
                //};
            }

			var resGoogleProfiles = new ReqGoogleProfiles()
			{
				id = dataResult.Mp_id,
			};
			var result = new Response<ReqGoogleProfiles>()
			{
				ResultCode = (int)ResponseCode.Ok,
				ResultMessage = ResponseCode.Ok.GetDescription(),
				ResultData = resGoogleProfiles
			};
			return result;




			//if (signInRequest.UserId == "xamarin" && signInRequest.UserPassword == "12345678")
			//{
			//    var user = new ResSignIn()
			//    {
			//        UserIdCode = Guid.NewGuid(),
			//        UserName = "김도토리",
			//        UserNickName = "도토리 아저씨 묵사발"
			//    };
			//    return new Response<ResSignIn>()
			//    {
			//        ResultCode = (uint)ResponseCode.Ok,
			//        ResultMessage = ResponseCode.Ok.GetDescription(),
			//        ResultData = user
			//    };
			//}
			//else
			//{
			//    return new Response<ResSignIn>()
			//    {
			//        ResultCode = (uint)ResponseCode.SingIn_notMatched,
			//        ResultMessage = ResponseCode.SingIn_notMatched.GetDescription()
			//    };
			//}
		}
    }
}
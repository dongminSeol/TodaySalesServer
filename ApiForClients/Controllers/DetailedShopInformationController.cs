using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiForClients.Models.Request;
using ApiForClients.Repositories;
using CommonFoundation.Models;
using Foundation.Request;
using Foundation.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Foundation;
using CommonFoundation.Geography;

namespace ApiForClients.Controllers
{
    [ApiController, Route("user/[controller]"), Produces("application/json")]
    public class DetailedShopInformationController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Response<DetailedShopItem>> Post(Request<long> request)
        {
            DetailedShopItem item = RepositoryMockup.GetDetailedShopInformation(request.RequestData);
            
            return new Response<DetailedShopItem>()
            {
                ResultCode = (int)ResponseCode.Ok,
                ResultMessage = ResponseCode.Ok.GetDescription(),
                ResultData = item
            };
        }
    }
}
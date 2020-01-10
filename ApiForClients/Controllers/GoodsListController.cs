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
    public class GoodsListController : ControllerBase
    {
        [HttpPost]        
        public ActionResult<Response<List<GoodsItem>>> Post(Request<SearchParameters> request)
        {
            List<GoodsItem> items = RepositoryMockup.GetGoodsList(request.RequestData);

            return new Response<List<GoodsItem>>()
            {
                ResultCode = (int)ResponseCode.Ok,
                ResultMessage = ResponseCode.Ok.GetDescription(),
                ResultData = items
            };
        }
    }
}
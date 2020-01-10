using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiForClients.Models.Request;
using CommonFoundation.Models;
using Foundation.Request;
using Foundation.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Foundation;

namespace ApiForClients.Controllers
{
    [ApiController, Route("user/[controller]"), Produces("application/json")]
    public class ShopListController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Response<List<ShopItem>>> Post(Request<SearchParameters> request)
        {
            // do something
            List<ShopItem> shopList = new List<ShopItem>();
            shopList.Add(new ShopItem
                (
                    new ShopInfo(10000, "테스트1", "서울시 용산구 어쩌동", "029871234", new CommonFoundation.Geography.GeoPoint(129.23, 35.093)),
                    "",
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    1000
                )
            );
            return new Response<List<ShopItem>>()
            {
                ResultCode = (int)ResponseCode.Ok,
                ResultMessage = ResponseCode.Ok.GetDescription(),
                ResultData = shopList
            };
        }
    }
}
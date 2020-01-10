using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiForClients.Attributes;
using ApiForClients.Models.Request;
using Foundation.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiForClients.Controllers
{
    [ApiController, Route("test/[controller]")]
    public class HttpMethodCheckController : ControllerBase
    {
        [HttpGet("{id}"), ValidateSign(false)]
        public ActionResult<string> Get(int id)
        {
            var memberSeq = HttpContext.Items["memberUniqueCode"];
            return "Get Result";
        }

        [HttpPost]
        public ActionResult<string> Post(Request<ReqCheckAppVersion> request)
        {
            return "Post Result";
        }

        [HttpPut("{id}"), ValidateSign(false)]
        public ActionResult<string> Put(int id, [FromBody] Request<ReqCheckAppVersion> request)
        {
            return "Put Result";
        }

        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            return "Delete Result";
        }
    }
}
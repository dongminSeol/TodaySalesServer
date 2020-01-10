using ApiForClients.Models.Request;
using Foundation;
using Foundation.Request;
using Foundation.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Foundation.Response;
using Foundation.Common;
using System.IO;

namespace ApiForClients.Attributes
{
    public class ValidateCertification : ActionFilterAttribute
    {
        readonly IBaseCrypto crypto;
        string computeHash;
        string sessionHashKey;

        public ValidateCertification()
        {
            crypto = new Crypto();
        }

        private void SetComputeHash(string textForHash)
        {
            computeHash = crypto.HmacSha512(sessionHashKey, textForHash);
        }

        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var reqContext = context.HttpContext.Request;
                var session = context.HttpContext.Session;

                if (reqContext.Path.ToString().Contains("Certify/")) return;
                
                sessionHashKey = session.GetString("hashKey");
                if (string.IsNullOrEmpty(sessionHashKey)) throw new HttpException("Session is expried.") { HttpCode = HttpStatusCode.RequestTimeout };

                var hashValueResult = reqContext.Headers["HashResultValue"];
                if (string.IsNullOrEmpty(hashValueResult)) throw new HttpException("It can not get HashResultValue of Headers.") { HttpCode = HttpStatusCode.NotFound };

                var clientNonce = Convert.ToInt32(reqContext.Headers["Nonce"]);
                var serverNonce = (int)session.GetInt32("nonce") + 1;
                if (clientNonce != serverNonce) throw new HttpException("Nonce is not match.") { HttpCode = HttpStatusCode.NotFound };
                session.SetInt32("nonce", serverNonce);

                switch (reqContext.Method)
                {
                    case "GET":
                        this.SetComputeHash(string.Format("{0}?{1}", reqContext.Path, serverNonce));
                        break;
                    case "DELETE":
                        this.SetComputeHash(string.Format("{0}?{1}", reqContext.Path, serverNonce));
                        break;
                    case "POST":
                        if (reqContext.Body.CanRead)
                        {
                            reqContext.Body.Position = 0;
                            using (var reader = new StreamReader(reqContext.Body))
                            {
                                var readText = reader.ReadToEndAsync();
                                this.SetComputeHash(string.Format("{0}?{1}", readText.Result, serverNonce));
                            }
                        }
                        break;
                    case "PUT":
                        if (reqContext.Body.CanRead)
                        {
                            reqContext.Body.Position = 0;
                            using (var reader = new StreamReader(reqContext.Body))
                            {
                                var readText = reader.ReadToEndAsync();
                                this.SetComputeHash(string.Format("{0}?{1}", reqContext.Path + readText.Result, serverNonce));
                            }
                        }
                        break;
                    default:
                        throw new HttpException("It has not found method.") { HttpCode = HttpStatusCode.NotFound };
                }

                if (computeHash != hashValueResult) throw new HttpException("Hash of parameters is invalid.") { HttpCode = HttpStatusCode.NotFound };
                
            }
            catch (HttpException e)
            {
                context.Result = new StatusCodeResult((int)e.HttpCode);
                return;
            }
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Foundation.Common
{
    public class HttpException : Exception
    {
        public HttpStatusCode HttpCode { get; set; }
        public HttpException()
        {
        }

        public HttpException(string message)
            : base(message)
        {
        }

        public HttpException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}

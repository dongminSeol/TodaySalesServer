using System;
using System.Collections.Generic;
using System.Text;

namespace Foundation.Request
{
    public class Request<T>
    {
        public string RequestTime { get; set; }
        public T RequestData { get; set; }
    }
}

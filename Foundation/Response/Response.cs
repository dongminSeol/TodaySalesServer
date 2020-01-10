using System;
using System.Collections.Generic;
using System.Text;

namespace Foundation.Response
{
    public class Response<T> : IBaseResponse
    {
        public string CompleteDateTime => DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        public uint ResultCode { get; set; }
        public string ResultMessage { get; set; }

        public T ResultData { get; set; }
    }
}

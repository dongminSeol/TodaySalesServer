using System;
using System.Collections.Generic;
using System.Text;

namespace Foundation.Response
{
    public interface IBaseResponse
    {
        string CompleteDateTime { get; }
        uint ResultCode { get; set; }
        string ResultMessage { get; set; }
    }
}

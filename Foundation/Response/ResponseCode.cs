using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Foundation.Response
{
    public enum ResponseCode
    {
        [Description("Request is succeeded.")] Ok = 0,

        // 1000 ~ : related authorization.
        [Description("Signin information has not matched.")] SingIn_notMatched = 1001,
        [Description("Guid is not invalid.")] Guid_invalid = 1002,
        [Description("Overtime of timeStampUTC.")] Overtime_timeStamp = 1003,

        // 9000 ~ : related error.
        [Description("Data is not exist from DataBase.")] Error_notExistFromDB = 9001
    }
}

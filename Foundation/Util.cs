using System;
using System.Collections.Generic;
using System.Text;

namespace Foundation
{
    public class Util
    {
        public static string ByteToString(byte[] bytes)
        {
            if (bytes == null) return string.Empty;
            string values = Encoding.Default.GetString(bytes);
            return values;
        }

        public static byte[] StringToBytes(string values)
        {
            if (string.IsNullOrEmpty(values)) return null;
            byte[] bytes = Encoding.UTF8.GetBytes(values);
            return bytes;
        }
    }
}

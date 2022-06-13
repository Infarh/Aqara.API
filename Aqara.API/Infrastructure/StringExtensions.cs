using System.Security.Cryptography;
using System.Text;

namespace Aqara.API.Infrastructure;

public static class StringExtensions
{
    public static byte[] GetMD5(this string str, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;
        var bytes = encoding.GetBytes(str);
        var md5_bytes = MD5.HashData(bytes);
        return md5_bytes;
        //var result = new StringBuilder(md5_bytes.Length * 2);
        //for (var i = 0; i < md5_bytes.Length; i++)
        //    result.AppendFormat("{0:x2}", md5_bytes[i]);

        //return result.ToString();
    }

    public static string GetMd5String(this string str, Encoding? encoding = null)
    {
        var md5_bytes = str.GetMD5(encoding);

        var result = new StringBuilder(md5_bytes.Length * 2);
        for (var i = 0; i < md5_bytes.Length; i++)
            result.AppendFormat("{0:x2}", md5_bytes[i]);

        return result.ToString();
    }
}

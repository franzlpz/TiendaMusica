using System.Security.Cryptography;
using System.Text;

namespace System
{
    public static class Extenciones
    {
        public static string ObtenerMD5(this string cadena)
        {

            MD5 md5 = MD5.Create();
            byte[] hashData = md5.ComputeHash(Encoding.Default.GetBytes(cadena));

            StringBuilder value = new StringBuilder();

            for (int i = 0; i < hashData.Length; i++)
            {
                value.Append(hashData[i].ToString());
            }

            return value.ToString();
        }
    }
}
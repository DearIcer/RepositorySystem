using CommonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataFilling
{
    public  class Program
    {
        static void Main(string[] args)
        {
            GenerateMD5("123456");
        }

        public static string GenerateMD5(string text)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(text);
                byte[] newbuffer = mi.ComputeHash(buffer);
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < newbuffer.Length; i++)
                {
                    stringBuilder.Append(newbuffer[i].ToString("x2"));
                }
                return stringBuilder.ToString();
            }
        }
    }
}

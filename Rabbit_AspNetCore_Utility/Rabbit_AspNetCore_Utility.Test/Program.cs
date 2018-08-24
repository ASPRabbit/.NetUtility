using Rabbit_AspNetCore_Utility.Encrypt;
using System;
using System.IO;
using System.Text;

namespace Rabbit_AspNetCore_Utility.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            Console.WriteLine(EncryptHelper.MD5EncryptByString("admin"));


            var origional = Encoding.Default.GetBytes("Test MD5EncryptByBytes");

            Console.WriteLine(EncryptHelper.MD5EncryptByBytes(origional));

            var path = AppDomain.CurrentDomain.BaseDirectory + @"log\1.txt";
            var stream = new FileStream("1.txt",FileMode.OpenOrCreate);
            var ret = EncryptHelper.MD5EncryptByStream(stream);
            Console.WriteLine(ret);
            Console.ReadKey();
        }
    }
}

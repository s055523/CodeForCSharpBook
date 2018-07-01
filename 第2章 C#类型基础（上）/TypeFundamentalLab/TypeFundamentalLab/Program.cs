using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeFundamentalLab
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new ExampleRef();
            Console.WriteLine("First");
            Console.ReadKey();
        }
    }

    class testRef
    {
        public byte e = 1;
        public int e2 = 2;
        public byte e3 = 3;
        public int e4 = 4;
        public byte e5 = 5;
        public int e6 = 6;
        public byte e7 = 7;
        public int e8 = 8;
        public static string a;

        public void HideMethod()
        {

        }
    }

    class ExampleRef : testRef
    {
        private int a = 9;
        public string b = "test";
        private static string c = "static";
    }
}

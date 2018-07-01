using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locality
{
    class Program
    {
        public const int n = 2000;

        static void Main(string[] args)
        {
            int[,] s = new int[n, n];

            //初始化一个2000乘2000的数组
            for (int x = 0; x < n; x++)
            {
                for (int y = 0; y < n; y++)
                {
                    s[x, y] = 1;
                }
            }

            var sw = new Stopwatch();

            sw.Start();
            for (int i = 0; i < 100; i++)
                TotalFast(s);

            sw.Stop();
            Console.WriteLine(string.Format("时间：{0}", sw.Elapsed));

            var sw2 = new Stopwatch();
            sw2.Start();
            for (int i = 0; i < 100; i++)
                TotalSlow(s);

            sw2.Stop();
            Console.WriteLine(string.Format("时间：{0}", sw2.Elapsed));

            Console.ReadKey();
        }

        public static long TotalFast(int[,] source)
        {
            var ret = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    //A11,A12...
                    ret += source[i, j];
                }
            }
            return ret;
        }

        public static long TotalSlow(int[,] source)
        {
            var ret = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    //A11,A21...
                    ret += source[j, i];
                }
            }
            return ret;
        }
    }
}

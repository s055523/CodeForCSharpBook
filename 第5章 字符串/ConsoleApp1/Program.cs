using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            CodeTimer.Initialize();

            for (int i = 1; i <= 10000; i *= 10)
            {
                CodeTimer.Time(
                    String.Format("Normal Concat ({0})", i),
                    1,
                    () => UseNormalConcat(i));

                CodeTimer.Time(
                    String.Format("StringBuilder ({0})", i),
                    1,
                    () => UseStringBuilder(i));

                CodeTimer.Time(
                    String.Format("StringConcat ({0})", i),
                    1,
                    () => StringConcat(i));

            }
            Console.ReadKey();
        }

        static void ProveImmutable()
        {
            string a = "1";
            string b = a;

            //true
            Console.WriteLine(ReferenceEquals(a, b));

            b = "2";

            //false
            Console.WriteLine(ReferenceEquals(a, b));
        }

        static void Intern1()
        {
            string s1 = "abc";

            //常量默认驻留
            Console.WriteLine(string.IsInterned(s1) ?? null);

            //编译器优化为常量的相加
            string s2 = "ab";
            s2 += "c";
            Console.WriteLine(string.IsInterned(s2) ?? null);

            //变量和常量相加不驻留，不打印任何东西
            string s3 = s2 + "c";
            Console.WriteLine(string.IsInterned(s3) ?? null);
        }

        static void Intern2()
        {
            string s1 = "123";
            string s2 = "12";

            //变量和常量相加不探测驻留池
            string s3 = s2 + "3";

            //False
            Console.WriteLine(ReferenceEquals(s1, s3));

            //将s3拉入驻留池
            s3 = string.Intern(s3);

            //True
            Console.WriteLine(ReferenceEquals(s1, s3));
        }

        private static readonly string STR = "1234567890";
        private static string UseNormalConcat(int count)
        {
            var result = "";
            for (int i = 0; i < count; i++)
            {
                result += STR;
            }
            return result;
        }

        private static string UseStringBuilder(int count)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                builder.Append(STR);
            }
            return builder.ToString();
        }

        private static string StringConcat(int count)
        {
            var array = new string[count];
            for (int i = 0; i < count; i++)
            {
                array[i] = STR;
            }
            return string.Concat(array);
        }

        private static void TryChangeString()
        {
            const string str = "ABC";
            Console.WriteLine(str);

            //拿到str所在的句柄，并固定住句柄，此后它将不受GC管辖
            var handle = GCHandle.Alloc(str, GCHandleType.Pinned);

            try
            {
                //调用AddrOfPinnedObject需要固定句柄
                //直接操作内存
                Marshal.WriteInt16(handle.AddrOfPinnedObject(), 4, 'Z');
                
                //ABZ
                Console.WriteLine(str);
            }
            finally
            {
                handle.Free();
            }
        }
    }
}

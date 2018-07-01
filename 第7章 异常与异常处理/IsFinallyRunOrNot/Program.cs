using System;
using System.IO;

namespace IsFinallyRunOrNot
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Demo1();
            }
            //没有任何catch块捕捉除零异常，程序会中止
            catch (InvalidCastException ex)
            {
                Console.WriteLine("catch");
            }
            finally
            {
                //创建2.txt 
                var fs = new FileStream("2.txt", FileMode.Create);
                var sw = new StreamWriter(fs);
                sw.Write("hello");
                sw.Close();
                fs.Close();
            }
            Console.ReadKey();
        }

        static void Demo1()
        {
            try
            {
                throw new DivideByZeroException();
            }
            finally
            {
                //创建1.txt 
                var fs = new FileStream("1.txt", FileMode.Create);
                var sw = new StreamWriter(fs);
                sw.Write("hello");
                sw.Close();
                fs.Close();
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionLab
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Demo1();
            }
            catch (Exception ex)
            {
                Console.WriteLine("catch");
            }
            finally
            {
                Console.WriteLine("Finally");
            }
            Console.ReadKey();
        }

        static void Demo1()
        {
            try
            {
                throw new DivideByZeroException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Demo1catch");
                throw new DivideByZeroException();
            }
            finally
            {
                Console.WriteLine("Demo1Finally");
            }
        }

    }
}

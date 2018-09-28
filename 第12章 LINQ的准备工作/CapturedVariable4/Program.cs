using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapturedVariable4
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<MethodInvoker>();
            for (int index = 0; index < 5; index++)
            {
                var counter = index * 10;

                list.Add(delegate
                {
                    Console.WriteLine("{0}, {1}", counter, index);
                    counter++;
                });
            }

            list[0]();
            list[1]();
            list[2]();
            list[3]();
            list[4]();

            list[0]();
            list[0]();
            list[0]();

            Console.ReadKey();
        }

    }
}

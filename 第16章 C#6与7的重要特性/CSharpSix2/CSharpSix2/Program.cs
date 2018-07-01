using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSix2
{
    class Program
    {
        public DateTime DateOfBirth { get; set; }
        public TimeSpan Age => DateTime.UtcNow - DateOfBirth;
        private int count;
        public int Count => count++;

        static void Main(string[] args)
        {
            var a = new Program();

            //不能通过编译，属性具有不变性
            //a.Age = new TimeSpan.......;

            var p = new Person();
            Console.WriteLine(p.Count);
            Console.WriteLine(p.Count);
            Console.WriteLine(p.Count);
            Console.WriteLine(p.Count);
            Console.ReadKey();
        }
    }

    public class Person
    {
        private int count;
        public int Count => count++;
    }
}

using System;

namespace CSharpSix
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new ImmutableClass(100);
            a.ToString();

            Console.WriteLine(a.a);  //99
            Console.ReadKey();
        }
    }

    class ImmutableClass
    {
        private readonly int _a;
        public int a { get; private set; }
        public ImmutableClass(int a)
        {
            _a = a;
        }

        public override string ToString()
        {
            a = 99;
            return string.Empty;
        }
    }
}

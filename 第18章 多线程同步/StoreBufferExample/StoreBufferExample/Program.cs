using System;
using System.Threading;

namespace StoreBufferExample
{
    class Program
    {
        public static void Main()
        {
            for (int i = 0; i < 1000; i++)
            {
                var s = new StoreBufferExample();
                Thread t1 = new Thread(s.ThreadA);
                Thread t2 = new Thread(s.ThreadB);

                t1.Start();
                t2.Start();

                if(s.A_Won == true && s.B_Won == false)
                {
                    Console.Write("1");
                }
                if (s.A_Won == false && s.B_Won == true)
                {
                    Console.Write("2");
                }
                if (s.A_Won == false && s.B_Won == false)
                {
                    Console.Write("3");
                }
                if (s.A_Won == true && s.B_Won == true)
                {
                    Console.Write("4");
                }
            }
            Console.ReadKey();
        }
    }

    class StoreBufferExample
    {
        int A = 0;
        int B = 0;
        public bool A_Won = false;
        public bool B_Won = false;

        public void ThreadA()
        {
            A = 1;
            if (B == 0) A_Won = true;
        }
        public void ThreadB()
        {
            B = 1;
            if (A == 0) B_Won = true;
        }
    }
}

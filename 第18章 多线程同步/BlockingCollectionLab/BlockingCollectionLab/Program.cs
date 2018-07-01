using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BlockingCollectionLab
{
    class Program
    {
        //最多只能有3个对象的阻塞集合
        static BlockingCollection<double> bc = new BlockingCollection<double>(3);

        static Random r = new Random();

        static void Main(string[] args)
        {
            new Thread(Produce).Start();
            new Thread(Consume).Start();
            Console.ReadKey();
        }

        static void Produce()
        {
            while (true)
            {
                Thread.Sleep(1000);
                var next = r.NextDouble();

                if (bc.TryAdd(next))
                {
                    Console.WriteLine("生产了新产品，数据为" + next);
                }
                else
                {
                    Console.WriteLine("无法生产新产品，集合已满");
                }
            }
        }

        static void Consume()
        {
            while (true)
            {
                Thread.Sleep(5000);
                double product;
                if (!bc.TryTake(out product))
                {
                    Console.WriteLine("没有产品");
                }
                else
                {
                    product = bc.Take();
                    Console.WriteLine("拿走了产品，数据为" + product);
                }
            }
        }
    }

}

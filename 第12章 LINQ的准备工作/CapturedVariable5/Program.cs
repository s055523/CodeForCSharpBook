using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapturedVariable5
{
    class Program
    {
        static void Main()
        {
            var data = Enumerable.Range(1, 9);
            var actions = new List<Action>();
            foreach (int s in data)
            {               
                actions.Add(delegate
                {
                    Console.WriteLine(s);
                });
            }

            //匿名方法捕获变量s
            //理论上会打印出10个9
            //但在c# 5中，会打印出1到9
            foreach (Action action in actions)
            {
                action();
            }

            actions.Clear();

            for (int i = 0; i < 10; i++)
            {
                actions.Add(delegate
                {
                    Console.WriteLine(i);
                });
            }

            //for语句行为和之前一样，仍然需要注意捕获变量被共享的问题
            //打印出10个10（此处是打印i最新的值，因为i会再次自增1到10，然后才退出循环）
            foreach (Action action in actions)
            {
                action();
            }
            Console.ReadKey();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiningPhilosophers
{
    class Program
    {
        //叉子状态
        private readonly int[] _folks = { 0, 0, 0, 0, 0 };

        //随机数
        private readonly Random _next = new Random(DateTime.Now.Millisecond);

        //食物总量
        public static int NumOfFood = 10;
        public static object Locker = new object();

        static void Main()
        {
            //一开始全部是思考状态
            Philosophers[] p = {
                new Philosophers(State.Think, "老子(0)", 0),
                new Philosophers(State.Think, "庄子(1)", 1),
                new Philosophers(State.Think, "孔子(2)", 2),
                new Philosophers(State.Think, "孟子(3)", 3),
                new Philosophers(State.Think, "墨子(4)", 4)
            };
            var program = new Program();

            var tasks = new Task[5];
            for (var i = 0; i < 5; i++)
            {
                var i1 = i;
                tasks[i] = new Task(() =>
                {
                    //还有食物就一直模拟
                    while (NumOfFood > 0)
                    {
                        program.NextState(p[i1]);
                    }
                });
                tasks[i].Start();
            }

            Task.WaitAll(tasks);

            Console.WriteLine("本次模拟结束，食物都吃完了");
            Console.ReadKey();
        }

        public void NextState(Philosophers p)
        {
            Thread.Sleep(1000);

            //随机到的数字小于0.5
            if (_next.NextDouble() < 0.5)
            {
                lock (Locker)
                {
                    //放下叉子,如果之前的状态是吃饭
                    //如果之前的状态也是思考，就不需要放下叉子
                    if (p.CurrentState == State.Eat)
                    {
                        _folks[p.Index] = 0;
                        if (p.Index != 4) _folks[p.Index + 1] = 0;
                        else _folks[0] = 0;
                    }

                    //开始思考
                    p.CurrentState = State.Think;

                    Console.WriteLine("{0}开始思考。当前叉子状态: {1}", p.Name, string.Join(",", _folks));
                }
            }
            else
            {
                //正在吃就接着吃
                if (p.CurrentState == State.Eat || NumOfFood == 0)
                {
                    return;
                }

                //尝试开吃
                lock (Locker)
                {
                    //左边的叉子
                    var folkLeft = p.Index;

                    //右边的叉子
                    var folkRight = p.Index == 4 ? 0 : p.Index + 1;

                    //只有左右两边的叉子都没人用才可以开吃
                    if (_folks[folkLeft] == 0 && _folks[folkRight] == 0)
                    {
                        p.CurrentState = State.Eat;

                        _folks[folkLeft] = 1;
                        _folks[folkRight] = 1;

                        NumOfFood--;
                        Console.WriteLine("{0}开始吃饭，并拿起叉子{1}和{2}。食物总量: {3}, 当前叉子状态: {4}", p.Name, folkLeft, folkRight,
                            NumOfFood, string.Join(",", _folks));
                    }
                    else
                    {
                        Console.WriteLine("{0}无法开始吃饭，因为他需要拿起叉子{1}和{2}。当前叉子状态: {3}", p.Name, folkLeft, folkRight,
                            string.Join(",", _folks));
                    }
                }
            }
        }
    }

    public enum State
    {
        Eat, Think
    }

    //哲学家类
    public class Philosophers
    {
        public State CurrentState { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }

        public Philosophers(State s, string name, int index)
        {
            CurrentState = s;
            Name = name;
            Index = index;
        }
    }
}

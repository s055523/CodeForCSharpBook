using static System.Console;
using System.Collections.Generic;

namespace PatternMatching
{
    class People
    {
        public int TotalMoney { get; set; }
        public People(int a)
        {
            TotalMoney = a;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var peopleList = new List<People>() {
                new People(1),
                new People(1_000_000)
            };

            foreach (var p in peopleList)
            {
                //类型模式
                if (p is People)
                {
                    WriteLine("是人");
                }
                //常量模式
                if (p.TotalMoney > 500000)
                {
                    WriteLine("有钱");
                }
                //变量模式
                //假如你需要先判断一个变量p是否为People，如果是，则再取它的TotalMoney字段
                //那么过去必须要分开写
                if (p is People)
                {
                    var temp = (People)p;
                    if (temp.TotalMoney > 500000)
                    {
                        WriteLine("是人且有钱");
                    }
                }
                //变量模式允许你引入一个变量并立即使用它
                if (p is People ppl && ppl.TotalMoney > 500000)
                {
                    WriteLine("是人且有钱");
                }
            }

            var a = 13;

            switch (a)
            {
                //现在i就是a
                //由于现在case后面可以跟when子句的表达式，不同的case有机会相交
                case int i when i % 2 == 1:
                    WriteLine(i + "是奇数");
                    break;
                //只会匹配第一个case，所以这个分支无法到达
                case int i when i > 10:
                    WriteLine(i + "大于10");
                    break;
                //永远在最后被检查，即使它后面还有case子句
                default:
                    break;
            }

            ReadKey();
        }
    }

}

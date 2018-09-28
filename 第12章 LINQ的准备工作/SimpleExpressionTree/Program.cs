using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleExpressionTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var exp1 = Expression.Constant(1);
            var exp2 = Expression.Constant(2);
            var exp12 = Expression.Add(exp1, exp2);
            var exp3 = Expression.Constant(3);
            var exp123 = Expression.Add(exp12, exp3);

            //将任何类型的表达式转化为一个强类型的lambda表达式
            Expression<Func<int>> lambdaExp = Expression.Lambda<Func<int>>(exp123);

            //使用Compile方法将lambda表达式转化为委托，这里的委托是Func<int>类型的
            Func<int> ret = lambdaExp.Compile();

            //现在就可以调用委托了
            Console.WriteLine(ret());

            Console.ReadKey();
        }
    }
}

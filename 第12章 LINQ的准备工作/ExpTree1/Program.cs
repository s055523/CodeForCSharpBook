using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpTree1
{
    class Program
    {
        static void Main(string[] args)
        {
            BlockExpression blockExpr = Expression.Block(
                //通过反射获得MethodInfo
                Expression.Call(
                    null, //静态方法不需要实例
                    typeof(Console).GetMethod("Write", new[] { typeof(string) }),
                    Expression.Constant("Hello World!")
                   ),
                Expression.Constant(42)
            );

            var lambdaExp = Expression.Lambda<Func<int>>(blockExpr);

            //这里直接将委托给调用了，故会有两个括号
            lambdaExp.Compile()();

            Console.ReadKey();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvocationList
{
    class Program
    {
        //定义一个回调方法
        public delegate int CallBack(int i);
        static void Main(string[] args)
        {
            //建立委托链    
            var cb = new CallBack(CallBackAdd1);
            cb += CallBackAdd2;
            cb += CallBackAdd3;
            cb += CallBackAdd4;
            cb += CallBackAdd5;

            var sum = cb(0);
            //只会收到最后一个含有返回值的委托的返回值，前面的都白做了，输出错误答案5
            Console.WriteLine(sum);

            //通过GetInvocationList方法得到委托链上的所有方法
            var list = cb.GetInvocationList();
            var i = 0;
            foreach (var del in list)
            {
                //显式呼叫方法
                //静态方法不需要object
                i = (int)del.Method.Invoke(null, new object[] { i });
            }

            //正确答案15
            Console.WriteLine(i);
            Console.ReadKey();
        }

        private static int CallBackAdd1(int i)
        {
            return i + 1;
        }

        private static int CallBackAdd2(int i)
        {
            return i + 2;
        }

        private static int CallBackAdd3(int i)
        {
            return i + 3;
        }

        private static int CallBackAdd4(int i)
        {
            return i + 4;
        }

        private static int CallBackAdd5(int i)
        {
            return i + 5;
        }
    }

}

using System;

namespace DelegateLab
{
    public class TestClass
    {
        //静态方法
        public static void CallBackStatic(int i)
        {
            Console.WriteLine("回调静态方法:" + i);
        }

        //实例方法
        public void CallBackInstance(int i)
        {
            Console.WriteLine("回调实例方法:" + i);
        }
    }

    class Program
    {
        //定义一个委托
        public delegate void CallBack(int i);

        static void Main(string[] args)
        {
            var random = RandomNumber();
            Console.WriteLine("当运行完random函数之后，运行回调函数并传入random函数的结果");

            var info = typeof(TestClass).GetMethod("CallBackStatic");

            //回调静态函数不需要实例
            //通过反射传入一个方法的MethodInfo
            var cb = (CallBack)Delegate.CreateDelegate(typeof(CallBack), null, info);

            //传递一组可选的参数并使用DynamicInvoke调用
            cb.DynamicInvoke(random);

            random = RandomNumber();
            Console.WriteLine("当运行完random函数之后，运行回调函数并传入random函数的结果");

            info = typeof(TestClass).GetMethod("CallBackInstance");
            var p = new TestClass();

            //回调实例函数需要实例 
            cb = (CallBack)Delegate.CreateDelegate(typeof(CallBack), p, info);
            cb.DynamicInvoke(random);

            Console.ReadKey();
        }

        private static int RandomNumber()
        {
            var random = new Random();
            return random.Next(0, 100);
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using static System.Console;

namespace ReferenceReturnLab
{
    class Program
    {
        unsafe static void Main(string[] args)
        {
            var n = new Numbers(10);

            //现在s2的类型是'ref int'
            //但s2实际的类型是int*
            //GetType还是返回Int32，因为实质上是s2->GetType()
            ref int s2 = ref n.FindNumberRef(2);
            WriteLine(s2.GetType());
            WriteLine("使用引用返回：" + s2);

            s2 = 666;

            //改变原来数组的值,第2项将变为666
            WriteLine(n);

            //虽然返回引用，但选择不使用，现在s3就是一个普通的int，和数组没关系
            int s3 = n.FindNumberRef(3);
            WriteLine("使用引用返回：" + s3);

            //不会改变原来数组的值
            s3 = 999;

            WriteLine(n);

            ReadKey();
        }

        unsafe void Demo1()
        {
            var n = new Numbers(10);

            //s现在是一个指针，它指向n中number的一个成员的地址
            int* s = n.FindNumber(2);

            WriteLine("使用指针：" + *s);

            //*s可能会变成一个十分巨大的值，也有可能还是2
            WriteLine("使用指针之后，指针的行为不可预期：" + *s);

            ReadKey();
        }

        unsafe void Demo2()
        {
            var n = new Numbers(10);

            //固定住数组
            //ptr指向数组第一个成员的地址
            fixed (int* ptr = n.numbers)
            {
                //显示指针中数组地址和对应的值
                for (int i = 0; i < 10; i++)
                {
                    var index = i + 1;

                    //ptr + i
                    WriteLine("第{0}个成员的地址为{1}", index, (int)(ptr + i));
                    //从地址中访问值使用*运算
                    WriteLine("第{0}个成员的值为{1}", index, *(ptr + i));
                }
            }
            ReadKey();
        }

        public static double CalculateDistance(in Point x, in Point y)
        {
            //不能修改被in修饰的变量的成员的值：只读的
            //x.x = 1;

            //计算两点间的距离
            return Math.Sqrt(Math.Pow(x.x - y.x, 2) + Math.Pow(x.y - y.y, 2));
        }

        public static double CalculateDistanceToOrigin(in Point x)
        {
            //只读的，不能直接赋值
            //Point.Origin = new Point(1, 1);

            //不能直接对成员赋值
            //Point.Origin.x = 1;

            return CalculateDistance(x, Point.Origin);
        }

        public static void ThrowException()
        {
            object o = null;

            //在null合并表达式中抛出异常
            object a = o ?? throw new Exception();

            Action method = () =>
            {
                int b = 1;
                if (b > 2)
                {
                    throw new Exception();
                }
            };
        }
    }

    public struct Point
    {
        public double x;
        public double y;

        public Point(double a, double b)
        {
            x = a; y = b;
        }

        private static Point origin = new Point(0, 0);

        //返回一个只读的值类型的引用
        public static ref readonly Point Origin => ref origin;  
        
        public void ChangeOrigin()
        {
            //Origin.x = 1;
        }
    }
        
    public class Numbers
    {
        public int[] numbers;

        public Numbers(int N)
        {
            numbers = Enumerable.Range(1, N).ToArray();
        }

        //一个返回地址的不安全函数，它取得数组中的第target个成员，然后返回它的地址
        unsafe public int* FindNumber(int target)
        {
            int ret;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (target == numbers[i])
                {
                    ret = numbers[i];
                    
                    //&代表取得地址，它是int*类型的
                    return &ret;
                }
            }
            
            ret = numbers[0];
            return &ret;
        }

        //这个方法和上面方法所做的事情是相同的
        public ref int FindNumberRef(int target)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (target == numbers[i]) return ref numbers[i];
            }
            //不能返回常量，必须返回引用
            return ref numbers[0];
        }

        public override string ToString()
        {
            return string.Join(",", numbers);
        }
    }

    public static class ListExtension
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}

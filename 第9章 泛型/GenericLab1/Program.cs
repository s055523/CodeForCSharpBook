using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericLab
{
    //在object之下，泛型类型之上的一个中间过渡类型
    //如果省去它，则一个new List<TypedMember<T>>仍然只能包含一种类型
    class Member
    {
    }

    class TypedMember<T> : Member
    {
        private T data;
        public TypedMember(T d)
        {
            data = d;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var tom = new { Name = "Tom", Age = 15 };
            Console.WriteLine("{0}: {1}", tom.Name, tom.Age);

            Console.WriteLine(tom.GetType());

            Console.ReadKey();
        }

        static int Compare<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b);
        }

        //泛型方法，输入参数类型必须相同
        static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
            Console.WriteLine("Swap<T>");
        }

        //输入参数必须是两个int的非泛型方法
        static void Swap(ref int a, ref int b)
        {
            Console.WriteLine("swap");
        }

        //泛型方法，第一个输入参数任意，第二个必须为int
        static void Swap<T>(ref T lhs, ref int rhs)
        {
            Console.WriteLine("swap2");
        }

        //泛型方法，输入参数类型任意，因此，如果两个参数类型相同时，将和第一个方法完全相同
        //使用两个类型参数不意味着它们必须是不同的
        static void Swap<T,K>(ref T lhs, ref K rhs)
        {
            Console.WriteLine("Swap<T,K>");
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace TupleInCSharp7
{
    class Program
    {
        static void Main(string[] args)
        {
            var localVariableOne = 5;
            var localVariableTwo = "some text";

            //显式实现的字段名称覆盖变量名
            var tuple = (explicitFieldOne: localVariableOne, explicitFieldTwo: localVariableTwo);
            var a = tuple.explicitFieldOne;

            //没有指定字段名称，使用变量名（需要C# 7.1版本)
            var tuple2 = (localVariableOne, localVariableTwo);
            var b = tuple2.localVariableOne;

            //如果没有指明字段名称，又传入了常量，则只能使用Item1，Item2等访问元组的成员
            var tuple3 = (5, "some text");
            var c = tuple3.Item1;

            var ToString = "1";
            var Item1 = 2;
            var tuple4 = (ToString, Item1);

            //ToString不能用做元组字段名，强制改为Item1
            var d = tuple4.Item1; //"1"

            //Item1不能用做元组字段名，强制改为Item2
            var e = tuple4.Item2; //2
            ReadKey();
        }

        //使用元组作方法的参数和返回值
        (int, int) MultiplyAll(int multiplier, (int a, int b) members)
        {
            //元组没有实现IEnumerator接口，不能foreach
            //foreach(var a in members)

            //操作元组
            return (members.a * multiplier, members.b * multiplier);
        }

        static void Demo1()
        {
            var a = (first: "one", second: 1);
            WriteLine(a.GetType());
            var b = (a: "hello", b: 2);
            WriteLine(b.GetType());
            var c = (a: 3, b: "world");
            WriteLine(c.GetType());

            WriteLine(a.GetType() == b.GetType()); //True, 两个元组基数和类型相同
            WriteLine(a.GetType() == c.GetType()); //False，两个元组基数相同但类型不同

            (string a, int b) d = a;

            //属性first,second消失了，取而代之的是a和b
            WriteLine(d.a);

            //定义了一个新的元组，成员为string和object类型
            (string a, object b) e;

            //由于int可以被隐式转换为object,所以可以这样赋值
            e = a;

            ReadKey();
        }

        static void Demo2()
        {
            var data = new List<AClass>();

            //不使用元组
            data.Where(d => d.IsDone == false).Select(d => new { d.ID, d.Notes });

            //使用元组
            data.Where(d => d.IsDone == false).Select(d => (d.ID, d.Notes));

            ReadKey();
        }

        //包装成方法
        //重用时如果不使用元组，只能使用之前的Tuple<T1，T2>或者写ViewModel类
        IEnumerable<(int, string)> GetUndoneTasks(List<AClass> data)
        {
            return data.Where(d => d.IsDone == false).Select(d => (d.ID, d.Notes));
        }

    }

    //示例类型
    public class AClass
    {
        public int ID { get; set; }
        public bool IsDone { get; set; }
        public string Notes { get; set; }
    }
}

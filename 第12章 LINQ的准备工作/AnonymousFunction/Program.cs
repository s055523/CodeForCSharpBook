using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnonymousFunction
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    //数据源
        //    var data = new List<string> { "the", "quick", "brown", "fox", "jumped", "over", "the", "lazy", "dog" };
        //    var shortWordData = new List<string>();

        //    //泛型委托
        //    var func = new Func<string, bool>(Predicate);

        //    //调用泛型委托
        //    foreach (var word in data)
        //    {
        //        if (func(word))
        //        {
        //            shortWordData.Add(word);
        //        }
        //    }

        //    Console.WriteLine(string.Join(",", shortWordData));
        //    Console.ReadKey();
        //}

        static void Main(string[] args)
        {
            //数据源
            var data = new List<string> { "the", "quick", "brown", "fox", "jumped", "over", "the", "lazy", "dog" };
            var shortWordData = new List<string>();

            //内联了匿名方法的泛型委托
            var func = new Func<string, bool>(delegate (string word)
            {
                return word.Length <= 4;
            });

            //调用泛型委托
            foreach (var word in data)
            {
                if (func(word))
                {
                    shortWordData.Add(word);
                }
            }

            Console.WriteLine(string.Join(",", shortWordData));
            Console.ReadKey();
        }


        //使用泛型委托做where
        public static bool Predicate(string word)
        {
            return word.Length <= 4;
        }

    }
}

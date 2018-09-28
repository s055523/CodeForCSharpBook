using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortedDictionaryAndList
{
    class Program
    {
        static void Main(string[] args)
        {
            var intList = Enumerable.Range(0, 100000).ToList();

            //消除扩容行为
            var sortedlist_int = new SortedList<int, int>(100000);
            var dic_int = new SortedDictionary<int, int>();

            CodeTimer.Time("sortedList_Add_int", 1, () =>
            {
                foreach (var item in intList)
                {
                    if (sortedlist_int.ContainsKey(item) == false)
                        sortedlist_int.Add(item, item);
                }
            });

            CodeTimer.Time("sortedDictionary_Add_int", 1, () =>
            {
                foreach (var item in intList)
                {
                    if (dic_int.ContainsKey(item) == false)
                        dic_int.Add(item, item);
                }
            });

            var random = new Random();
            var array_count = 100000;
            intList = new List<int>();
            for (var i = 0; i <= array_count; i++)
            {
                var ran = random.Next();
                intList.Add(ran);
            }

            //消除扩容行为
            sortedlist_int = new SortedList<int, int>(array_count);
            dic_int = new SortedDictionary<int, int>();

            CodeTimer.Time("sortedList_Add_int", 1, () =>
            {
                foreach (var item in intList)
                {
                    if (sortedlist_int.ContainsKey(item) == false)
                        sortedlist_int.Add(item, item);
                }
            });

            CodeTimer.Time("sortedDictionary_Add_int", 1, () =>
            {
                foreach (var item in intList)
                {
                    if (dic_int.ContainsKey(item) == false)
                        dic_int.Add(item, item);
                }
            });

            Console.ReadKey();
        }
    }
}

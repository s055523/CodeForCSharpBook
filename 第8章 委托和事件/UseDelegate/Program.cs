using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseDelegate
{
    //1.定义
    public delegate List<int> SelectDelegate(List<int> aList, int threshold);

    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<int>();

            //数据源：-5至10的所有整数
            list.AddRange(Enumerable.Range(-5, 10));

            //2.指向
            SelectDelegate sd = Select;

            //3.调用
            list = sd.Invoke(list, 1);

            //只有大于1的整数被选中
            Console.WriteLine("列表中有{0}个数字.", list.Count);
            Console.ReadKey();
        }

        public static List<int> Select(List<int> aList, int threshold)
        {
            List<int> ret = new List<int>();
            foreach (var i in aList)
            {
                if (i > threshold)
                {
                    ret.Add(i);
                }
            }
            return ret;
        }
    }

}

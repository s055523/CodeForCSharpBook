using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpHashTable
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new string[] { "张三", "李四", "王五", "刘备", "关羽", "张飞", "颜良", "文丑", "吕布", "貂蝉" };
            var hashTable = new CSharpHashTable(11, new LinearHashCollectionSolver());
            foreach (string s in data)
            {
                hashTable.Insert(s);
            }

            hashTable.PrintAll();
            Console.ReadKey();
        }
    }
    public class CSharpHashTable
    {
        private static int _length;
        private static string[] _data;
        private IHashCollectionSolver _solver;
        private static int count;

        public CSharpHashTable(int length, IHashCollectionSolver solver)
        {
            //如果传入的值为0就设置为3
            _length = length == 0 ? 3 : length;
            _data = new string[_length];
            _solver = solver;
        }

        /// <summary>
        /// 获得对应的哈希值
        /// </summary>
        /// <returns></returns>
        static int SimpleHash(string key)
        {
            int total = key.ToCharArray().Sum(a => a);
            return total % _length;
        }

        public void Insert(string key)
        {
            var hashValue = SimpleHash(key);
            if (count == _length)
            {
                Console.WriteLine("哈希表已满！");
                //TODO：动态扩容
                return;
            }
            if (_data[hashValue] == null)
            {
                _data[hashValue] = key;
                count++;
                return;
            }
            
            while (true)
            {
                //解决冲入
                hashValue = _solver.GetNextOffset(_length, hashValue);
                if(_data[hashValue] == null)
                {
                    _data[hashValue] = key;
                    count++;
                    return;
                }
            }
        }

        public void Delete(string key)
        {
            var hashValue = SimpleHash(key);
            _data[hashValue] = null;
            count--;
        }

        /// <summary>
        /// 遍历
        /// </summary>
        public void PrintAll()
        {
            for (int i = 0; i < _length; i++)
            {
                Console.WriteLine(String.Format("槽位{0}，值为{1}", i, _data[i] ?? "无"));
            }
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Find(string key)
        {
            var hashValue = SimpleHash(key);
            if (_data[hashValue] == null)
            {
                Console.WriteLine(key + "不在表中");
            }
            return _data[hashValue];
        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy
{
    public class studentData
    {
        public string Name { set; get; }
        public string Course { set; get; }
        public int Score { set; get; }
        public string Term { set; get; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<studentData> {
                new studentData {Name="张三",Term="第一学期",Course="数学",Score=80},
                new studentData {Name="张三",Term="第一学期",Course="语文",Score=90},
                new studentData {Name="张三",Term="第一学期",Course="英语",Score=70},
                new studentData {Name="李四",Term="第一学期",Course="数学",Score=60},
                new studentData {Name="李四",Term="第一学期",Course="语文",Score=70},
                new studentData {Name="李四",Term="第一学期",Course="英语",Score=30},
                new studentData {Name="王五",Term="第一学期",Course="数学",Score=100},
                new studentData {Name="王五",Term="第一学期",Course="语文",Score=80},
                new studentData {Name="王五",Term="第一学期",Course="英语",Score=80},
                new studentData {Name="赵六",Term="第一学期",Course="数学",Score=90},
                new studentData {Name="赵六",Term="第一学期",Course="语文",Score=80},
                new studentData {Name="赵六",Term="第一学期",Course="英语",Score=70},
                new studentData {Name="张三",Term="第二学期",Course="数学",Score=100},
                new studentData {Name="张三",Term="第二学期",Course="语文",Score=80},
                new studentData {Name="张三",Term="第二学期",Course="英语",Score=70},
                new studentData {Name="李四",Term="第二学期",Course="数学",Score=90},
                new studentData {Name="李四",Term="第二学期",Course="语文",Score=50},
                new studentData {Name="李四",Term="第二学期",Course="英语",Score=80},
                new studentData {Name="王五",Term="第二学期",Course="数学",Score=90},
                new studentData {Name="王五",Term="第二学期",Course="语文",Score=70},
                new studentData {Name="王五",Term="第二学期",Course="英语",Score=80},
                new studentData {Name="赵六",Term="第二学期",Course="数学",Score=70},
                new studentData {Name="赵六",Term="第二学期",Course="语文",Score=60},
                new studentData {Name="赵六",Term="第二学期",Course="英语",Score=70}
            };

            //根据姓名分组
            var sum = from l in list
                          //g的类型是IGrouping<string, studentData>，它继承IEnumerable<studentData>
                          //所以，它除了有Key之外，还可以对它进行基于IEnumerable<T>的操作
                      group l by l.Name into g
                      select new
                      {
                          Name = g.Key,
                          Score = g.Sum(m => m.Score),
                          Average = g.Average(m => m.Score)
                      };

            //等价的写法
            var sum2 = list.GroupBy(m => m.Name)
                          .Select(g => new { Name = g.Key, Scores = g.Sum(l => l.Score), Average = g.Average(m => m.Score) });

            foreach (var s in sum)
            {
                Console.WriteLine("姓名：" + s.Name + ", 总分" + s.Score + ", 平均分" + s.Average);
            }

            var sum3 = from l in list
                          //当分组的字段是多个的时候，通常把这多个字段合并成一个匿名类型，然后group by这个匿名类型
                          //此时key的类型就变成了一个匿名类型
                      group l by new { l.Name, l.Term } into g
                      select new
                      {
                          Name = g.Key.Name,
                          Term = g.Key.Term,
                          Score = g.Sum(m => m.Score),
                          Average = g.Average(m => m.Score)
                      };

            //等价的写法
            var sum4 = list.GroupBy(m => new { m.Name, m.Term })
                           .Select(g => new { Name = g.Key.Name, Term = g.Key.Term, Scores = g.Sum(l => l.Score), Average = g.Average(m => m.Score) });

            foreach (var s in sum3)
            {
                Console.WriteLine("姓名：" + s.Name + ", 学期" + s.Term + ", 总分" + s.Score + ", 平均分" + s.Average);
            }

            var sum5 = from l in list
                       group l by new { l.Name, l.Term } into g
                       //相当于having语句
                       where g.Average(m => m.Score) >= 80
                       select new
                       {
                           Name = g.Key.Name,
                           Term = g.Key.Term,
                           Score = g.Sum(m => m.Score),
                           Average = g.Average(m => m.Score)
                       };

            //等价的写法
            var sum6 = list.GroupBy(m => new { m.Name, m.Term })
                            .Where(m => m.Average(x => x.Score) >= 80)
                           .Select(g => new {
                               Name = g.Key.Name,
                               Term = g.Key.Term,
                               Scores = g.Sum(l => l.Score),
                               Average = g.Average(m => m.Score)
                           });

            foreach (var s in sum5)
            {
                Console.WriteLine("姓名：" + s.Name + ", 学期" + s.Term + ", 总分" + s.Score + ", 平均分" + s.Average);
            }

            Console.ReadKey();
        }
    }

}

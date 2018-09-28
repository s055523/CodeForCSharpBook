using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIEnumerable
{
    class Program
    {
        static void Main(string[] args)
        {
            //新的Person数组
            Person[] peopleArray =
            {
                new Person("John", "Smith"),
                new Person("Jim", "Johnson"),
                new Person("Sue", "Rabon"),
            };

            //People类实现了IEnumerable
            var peopleList = new People(peopleArray);

            //枚举时先访问MoveNext方法
            //如果返回真，则获得当前对象，返回假，就退出此次枚举
            foreach (Person p in peopleList)
            {
                Console.WriteLine(p.FirstName + " " + p.LastName);
            }

            //foreach的本质
            var p2 = peopleList.GetEnumerator();
            while (p2.MoveNext())
            {
                var p3 = (Person)p2.Current;
                Console.WriteLine(p3.FirstName + " " + p3.LastName);
            }

            Console.ReadKey();
        }
    }
    
    //People类就是Person类的集合
    //但我们不能用List<Person>或Person[]，因为他们都实现了IEnumerable
    //我们要自己实现一个IEnumerable
    //所以请将People类想象成List<Person>或者类似物
    public class People : IEnumerable
    {
        private readonly Person[] _people;
        public People(Person[] pArray)
        {
            //构造一个Person的集合
            _people = new Person[pArray.Length];
            for (var i = 0; i < pArray.Length; i++)
            {
                _people[i] = pArray[i];
            }
        }

        //实现IEnumerable需要实现GetEnumerator方法，签名是固定的
        public IEnumerator GetEnumerator()
        {
            return new PeopleEnumerator(_people);
        }
    }

    public class PeopleEnumerator : IEnumerator
    {
        public Person[] People;

        //每次运行到MoveNext或Reset时，利用get方法自动更新当前位置指向的对象
        object IEnumerator.Current
        {
            get
            {
                try
                {
                    //当前位置的对象
                    return People[_position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        //当前位置
        private int _position = -1;

        public PeopleEnumerator(Person[] people)
        {
            People = people;
        }

        //当程序运行到foreach循环中的in时，就调用这个方法获得下一个person对象
        public bool MoveNext()
        {
            _position++;
            //返回一个布尔值，如果为真，则说明枚举没有结束。
            //如果为假，说明已经到集合的结尾，就结束此次枚举
            return (_position < People.Length);
        }

        //这里用了C# 6的新功能
        public void Reset() => _position = -1;
    }

    public class Person
    {
        public Person(string fName, string lName)
        {
            FirstName = fName;
            LastName = lName;
        }

        public string FirstName;
        public string LastName;
    }
}

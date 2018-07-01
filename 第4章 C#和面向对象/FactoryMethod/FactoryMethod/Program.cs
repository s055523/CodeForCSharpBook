using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            var f = new NagatoFactory();
            f.Create();

            Console.ReadKey();

        }
    }

    //一个抽象产品
    public class BattleShip
    {
    }

    //若干具体产品
    public class Nagato : BattleShip
    {
        public Nagato()
        {
            Console.WriteLine("我是战列舰长门。请多指教。和敌战列舰的战斗就交给我吧。");
        }
    }

    public class Mutsu : BattleShip
    {
        public Mutsu()
        {
            Console.WriteLine("长门级战舰二号舰的陆奥哟。请多关照。");
        }
    }

    public interface AbstractFactory
    {
        //抽象工厂类提供签名
        BattleShip Create();
    }

    //多个具体工厂
    public class MutsuFactory : AbstractFactory
    {
        public BattleShip Create()
        {
            return new Mutsu();
        }
    }

    public class NagatoFactory : AbstractFactory
    {
        public BattleShip Create()
        {
            return new Nagato();
        }
    }
}

using System;

namespace SimpleFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            var f = new Factory();
            f.CreateBattleShip("mutsu");
            f.CreateBattleShip("nagato");

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

    //一个具体工厂
    public class Factory
    {
        //根据传入值制造产品
        public BattleShip CreateBattleShip(string type)
        {
            //这是简单工厂的问题所在：扩展性差
            switch (type)
            {
                case "mutsu":
                    return new Mutsu();
                case "nagato":
                    return new Nagato();
                default:
                    throw new ArgumentException("该型号的战列舰不可用");
            }
        }
    }
}

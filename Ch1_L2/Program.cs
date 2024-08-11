using Ch1_L1_Checked;
namespace Ch1_L2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //#region 值类型和引用类型
            //// class是引用类型。
            //Person Person1 = new Person("Peter", 30, "男", new DateOnly(1990, 7, 29));
            //Person Person2 = new Person("Tom", 20, "男", new DateOnly(2000, 10, 1));
            ////Console.WriteLine($"Person1：{Person1}\nPerson2: {Person2}");
            ////Person1.Age += 1;
            ////Console.WriteLine(Person1);
            //// Person2的信息会不会随Person3的改变而改变？
            //Person Person3 = Person2;
            //Person3.Name = "Jack";
            //Console.WriteLine($"Person2: {Person2}\nPerson3: {Person3}");


            //CoordsClass pClass = new CoordsClass(1, 1);
            ////Console.WriteLine(p1);
            ////Console.WriteLine($"P1到原点的距离为：{p1.GetDistance()}");


            //// struct是值类型。
            //var p1 = new CoordsStruct(1, 1);
            //// WITH expression generate a new copy, only applicable to struct.
            //// 语法糖
            //// var p2 = new CoordsStruct(2, 1);
            //var p2 = p1 with { X = 2 };
            //var p3 = p2;
            //////修改p3的值，并不会造成p2的值的修改，这是值类型和引用类型的最大区别。
            //p3.X = 3;
            //Console.WriteLine($"p2:{p2};p3:{p3}");
            //#endregion

            //#region 静态成员
            //// 静态成员
            //var Rose = new Person("Rose", 10, "女", new DateOnly(2019, 3, 1));
            //Console.WriteLine($"{Rose.Name}的年龄是{Rose.Age}岁。她是一个人类，因此有{Person.GetLegCount()}个肢体。");
            //#endregion

            
        }
    }
}

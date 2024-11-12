//Linq

#region Linq(Language Integrated Query 语言集成查询)：Linq to Objects(内存对象集合)、Linq to Provider(XML、JSON、SQL、Entities)
//Linq语法分为两种形式：查询表达式(Query Expression)、链式表达式(Chained Expression)，后面以链式为主。

using MyUtilities;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

IEnumerable<int> intCollection1 = [90, 2, 23, 88, 9, 22, 7, 64, 39, 20];
//查询出大于20的偶数，并返回降序集合。

//传统方法：提前准备好容器，遍历后将符合逻辑的元素放入容器，然后执行Sort和Reverse。
//List<int> res1 = new List<int>();
//foreach (var item in intCollection1)
//{
//    if (item > 20 & item % 2 == 0)
//        res1.Add(item);
//}
//res1.Sort();
//res1.Reverse();
//foreach (int i in res1)
//    Console.Write($"{i} ");

//Console.WriteLine();

//Linq:查询表达式：构建query，消耗query。注意：查询行为在query被构建出来时，并没有执行，只有在for循环中query才被执行。
//被称为Linq的延迟执行与消耗。
//var query = from item in intCollection1
//            where item > 20 & item % 2 == 0
//            orderby item descending
//            select item;
//foreach (int i in query) //for循环就是对query的消耗。这里才真正执行查询语句。
//    Console.Write($"{i} ");
//Console.WriteLine();

//Linq:链式表达式
//var res2 = intCollection1.Where(x => x > 20 & x % 2 == 0).OrderByDescending(x => x);
//foreach (int i in res2) //同样，只有在for循环的时候才消耗res2.
//    Console.Write($"{i} ");
//Console.WriteLine();
#endregion

#region Linq的延迟执行(defer)和消耗(exhaust)，常用消耗方式：foreach、ToList()、ToArray()、ToDictionary()、Count（）、Max()、Sum()、Take()、First()
//假设在遍历集合后，对每个元素进行一个耗时非常长的计算。
//var res3 = intCollection1.Select(x =>
//{
//    Thread.Sleep(1000); //假设这里执行了一个耗时1s的计算。
//    Console.Write(x + " ");
//    return x * x;
//});
////这时直接运行程序，发现并没有执行以上代码。
////res3.ToArray()消耗了res3这个query，触发了代码的执行。
//res3.ToArray();
//Console.WriteLine("Finish"); //注释掉res3.ToArray();程序会直接打印Finish，说明res3在不被消耗的情况下是不会执行的。
#endregion

#region Linq基于扩展方法和委托(Lambda表达式)
//var res4 = intCollection1.MyWhere(x => x > 20 & x % 2 == 0);
//res4.Show();
#endregion

#region Linq提供多线程的并行计算方式：ParallelQuery；返回单线程使用AsSequential
//下面的代码会按顺序打出10个值，间隔1秒。
//Enumerable.Range(1, 10)
//          .Select(x =>
//                {
//                    Thread.Sleep(1000); //线程阻塞
//                    return x * x;
//                }).Show();

//使用AsParallel()后Linq可以使用多线程加速运算，缺点是由于不同线程运行速度不同，因此最后的结果往往是乱序，可能需要重新排序。
//可以在Select之后调用Order()，也可以在AsParallel()后调用AsOrdered()，使.Net以顺序的形式调用多线程处理数据集。
//由于我们自己写的Show()方法是线程不安全的，在执行后面的语句前最好调用AsSequential()返回单线程模式。
//Enumerable.Range(1, 10).AsParallel().AsOrdered()
//          .Select(x =>
//          {
//              Thread.Sleep(1000);
//              return x * x;
//          }).AsSequential().Show();

//Linq的返回值有多种类型：IEnumerable、IOrderedEnumerable、IQueryable、ParallelQuery等等，但是不论返回值如何，大多数
//情况下都可以使用链式表达式，无感的进行操作，不用过多关心每一步的返回值类型。
#endregion

#region Linq常见操作

#region 展平(Flatten)
//jagged array
var mat = new int[][]
{
    [1,2,3],
    [1,2,3,4,5],
    [1,2,3,4,5,6],
};

//普通方法
//int count = 0;
//foreach (var x in mat)
//{
//    count += x.Length;
//}
//int index = 0;
//int[] res = new int[count];
//foreach (var row in mat)
//{
//    foreach (var j in row)
//    {
//        res[index] = j;
//        index++;
//    }
//}
//res.Show();

//查询表达式
//(from row in mat
// from x in row
// select x)
//.Show();

//链式表达式
//mat.SelectMany(x => x).Show();
#endregion

#region 笛卡尔积
//var list1 = new List<string> { "a1", "b1", "c1" };
//var list2 = new List<string> { "a2", "b2" };
//var list3 = new List<string> { "a3", "b3", "c3" };
//var list4 = new List<string> { "a4", "b4", "c4", "d4" };

//求list1和list2的笛卡尔积
//传统方法：
//var res1 = new List<string>();
//foreach (var x in list1)
//    foreach (var y in list2)
//        res1.Add($"{x}{y}");
//res1.Show();

////Linq: SelectMany
//list1.SelectMany(x => list2.Select(y => x + y)).Show();

//求list1、list2和list3的笛卡尔积
//传统方法：
//var res2 = new List<string>();
//foreach (var x in list1)
//    foreach (var y in list2)
//        foreach (var z in list3)
//            res2.Add($"{x}{y}{z}");
//res2.Show();

//Linq: SelectMany第一种方式
//list1.SelectMany(x => list2.Select(y => x + y)).SelectMany(x => list3.Select(y => x + y)).Show();
//Linq: SelectMany第二种方式
//list1.element->l, list2.element->r          list1.element+list2.element->l, list3.element->r
//list1.SelectMany(r => list2, (l, r) => l + r).SelectMany(r => list3, (l, r) => l + r).Show();

//求list1、list2、list3和list4的笛卡尔积
//list1.SelectMany(r => list2, (l, r) => l + r).SelectMany(r => list3, (l, r) => l + r).SelectMany(r=>list4, (l, r)=>l+r).Show();
#endregion

#region 差集(Except)、交集(Intersect)、并集(Union)
int[] arr1 = [1, 2, 3, 4, 10];
int[] arr2 = [3, 8, 10, 2, 12];
Student[] students1 = [new Student("Peter", 10, 90, true), new Student("Sam", 12, 100, true)];
Student[] students2 = [new Student("May", 10, 90, false), new Student("Sam", 12, 100, true)];

//差集：普通方法
//List<int> res1 = new List<int>();
//foreach (int x in arr1)
//{
//    if (!arr2.Contains(x))
//        res1.Add(x);
//}
//res1.Show();

//差集：Linq query expression
//(from x in arr1
// where !arr2.Contains(x)
// select x).Show();

//由于Student是引用类型，判断引用类型是否相同是判断其地址，因此student1和student2里的Sam实际是两个对象有不同的地址
//这种情况下Contains()函数就失效了。这时需要使用Contains()方法的重载，传入一个IEqualityComparer<Student>对象，其定义了两个Student对象的比较方式。
var StudentComparer = new StudentComparer();
//(from x in students1
// where !students2.Contains(x, StudentComparer)
// select x).Show();

//差集：Linq Chained expression:Except
//arr1.Except(arr2).Show();
//students1.Except(students2, StudentComparer).Show();

//ExceptBy: 无需写StudentComparer类，优点是如果比较方式比较简单且只需调用一次的话更方便，
//如果要多次求差集，且比较方式比较复杂，写StudentComparer更好。
//students1.ExceptBy(students2.Select(x => (x.Name, x.Gender, x.BMI, x.Age)), y => (y.Name, y.Gender, y.BMI, y.Age)).Show();

//交集：Linq Chained expression
//arr1.Intersect(arr2).Show();
//students1.Intersect(students2, StudentComparer).Show();
////IntersectBy:
//students1.IntersectBy(students2.Select(x => (x.Name, x.Gender, x.BMI, x.Age)), y => (y.Name, y.Gender, y.BMI, y.Age)).Show();

//并集：Linq Chained expression，注意Union会自动去重
//arr1.Union(arr2).Show();
//students1.Union(students2, StudentComparer).Show();
//UnionBy:
//students1.UnionBy(students2, x => (x.Name, x.Gender, x.BMI, x.Age)).Show();
#endregion

#region 分组(GroupBy)
////根据奇偶性分成两组
//int[] list = [1, 2, 3, 4, 5, 6, 7, 8];
////这里使用var更好，因为Select后的对象是匿名对象，并不确定其类型，使用var让编译器自行推断更方便。
////x.Key的值是true和false，具体可以debug模式查看GroupBy的结果。
//var res = list.GroupBy(x => x % 2 == 0).Select(g => new { GroupName = g.Key ? "even" : "odd", Value = g.ToArray() });
//foreach (var x in res)
//{
//    Console.Write(x.GroupName + " ");
//    x.Value.Show();
//}

//统计集合里每个字母出现的次数并升序排列，出现次数相同时，按照字母顺序排列
//string[] words = ["tom", "jerry", "mikky", "tutor"];
////var a = words.SelectMany(x => x).GroupBy(x => x);
//words.SelectMany(x => x).GroupBy(x => x).Select(x => new { Word = x.Key, Count = x.Count() }).OrderBy(x => x.Count).ThenBy(x => x.Word).Show();
//Select语句中产生了一个匿名对象。
#endregion

#region First() FirstOrDefault() Last() Single() SingleOrDefault()

Student[] students = [new Student("Peter", 10, 90, true), new Student("Sam", 12, 100, true), new Student("Peter", 20, 80, false)];
////First()返回符合条件的第一个元素，如果不存在抛出异常。
//var res1 = students.First(x => x.Name == "May"); //会抛出一个异常，可以使用try catch去捕捉。
////FirstOrDefault()返回符合条件的第一个元素，如果不存在返回默认值。对于引用类型返回null，数值类型返回0。
//var res2 = students.FirstOrDefault(x => x.Name == "May");
//Console.WriteLine(res2);
//Console.WriteLine(res2 == null);

////返回符合条件的一个元素，如果存在多个元素或无符合条件的元素，抛出异常。
//var res3 = students.Single(x => x.Name == "Sam");
//Console.WriteLine(res3);
//var res4 = students.SingleOrDefault(x => x.Name == "Sami");
//Console.WriteLine(res4 == null);
#endregion

#region Inner join (join)
//Person magnus = new Person { Name = "Magnus" };
//Person terry = new Person { Name = "Terry" };
//Person charlotte = new Person { Name = "Charlotte" };
//Person arlene = new Person { Name = "Arlene" };
//Person rui = new Person { Name = "Rui" };

//Pet barley = new Pet { Name = "Barley", Owner = terry };
//Pet boots = new Pet { Name = "Boots", Owner = terry };
//Pet whiskers = new Pet { Name = "Whiskers", Owner = charlotte };
//Pet bluemoon = new Pet { Name = "Blue Moon", Owner = rui };
//Pet daisy = new Pet { Name = "Daisy", Owner = magnus };

//List<Person> people = new List<Person> { magnus, terry, charlotte, arlene, rui };
//List<Pet> pets = new List<Pet> { barley, boots, whiskers, bluemoon, daisy };

//Join()的第一个参数是内表，第二个参数是外表的键，第三个参数是内表的键，第四个参数是返回的结果。
//Join()会将内外两个表，依据内外键相同的关系，将内外两个表关联起来，使内外键相等的数据成为同一组数据。
//people.Join(pets, person => person, pet => pet.Owner, (person, pet) => new { OwnerName = person.Name, PetName = pet.Name }).Show();
//注意，Join()只返回成功关联的数据。
#endregion

#region skip & take:通常用来实现后端分页(Paginator)
//intCollection1.Where(x => x > 2).Order().Skip(2).Take(3).Show();
#endregion

#region Zip:将两个集合合并成一个，如果两个集合个数不同，多余的元素被忽略。
//int[] num = [1, 2, 3,4];
//string[] name = ["one", "two", "three"];
//num.Zip(name).Show(); //tuple元组
//num.Zip(name, (x, y) => $"{x}={y}").Show();
#endregion

#region OfType():返回集合中某个类型的元素
//var item0 = new Foo();
//var item1 = new Foo();
//var item2 = new Bar();
//var item3 = new Bar();
//var collection = new IFoo[] { item0, item1, item2, item3 };
//collection.OfType<Foo>().ToList().ForEach(x => Console.Write($"{x.GetType().FullName} ")); //ForEach()是List的扩展方法。
//Console.WriteLine();
//collection.OfType<Bar>().ToList().ForEach(x => Console.Write($"{x.GetType().FullName} "));
//Console.WriteLine();
//collection.OfType<IFoo>().ToList().ForEach(x => Console.Write($"{x.GetType().FullName} "));
//Console.WriteLine();
#endregion

#region ToLookup:创建一个查找对象，在被调用时才去查询。
//string[] array = { "OnE", "two", "three" };
////// 根据元素字符串长度创建一个查找对象
//var lookup1 = array.ToLookup(item => item.Length);
////// 查找字符串长度为 3 的元素
//lookup1[3].Show();

//////根据字符串大小写创建查找对象，输入true表示查询全部是小写的字符串；false表示查询全部是大写的字符串
//var LookupLowerCase = array.ToLookup(item => item.Select(x => char.IsLower(x)).All(x => x == true));
//LookupLowerCase[true].Show();
#endregion

#region Any()
//var rnd = new Random(1000);
////产生一个10000个随机数的集合，并判断其中是否有大于5000的数。
//var collection = Enumerable.Range(0, 100000000).Select(x => rnd.Next(10000));
//Stopwatch sw = Stopwatch.StartNew();
//sw.Start();
//Console.WriteLine(collection.Count(x => x > 5000) > 0); //Count会遍历所有collection，因此如果collection很大就会很耗时。时间复杂度总是O(n)。
//sw.Stop();
//Console.WriteLine(sw.ElapsedMilliseconds);
//sw.Restart();
//Console.WriteLine(collection.Any(x => x > 5000)); //Any则会在找到第一个大于5000的数时就返回true，不会继续遍历后面的元素，只有在最差的情况下才是O(n)。
//sw.Stop();
//Console.WriteLine(sw.ElapsedMilliseconds);
#endregion
#endregion

public static class CollectionExtension
{
    /// <summary>
    /// 自定义一个IEnumerable<T>类型的扩展方法，实现Where语句。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="resource"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static IEnumerable<T> MyWhere<T>(this IEnumerable<T> resource, Func<T, bool> func)
    {
        foreach (var item in resource)
        {
            if (func(item))
            {
                yield return item;
            }
        }
    }
}

/// <summary>
/// IEqualityComparer<T>共有两个方法，默认情况下，会先调用GetHashCode()方法去比较两个对象，如果相同，再调用Equals方法去比较。
/// 这是因为GetHashCode()方法比较起来更容易，效率更高。
/// 我们希望两个Student对象，如果其所有成员都一样，那么这两个对象就相同，这个逻辑在Equals()里写，为了让程序可以调用Equals()方法，
/// 必须使GetHashCode()返回相同的值，因此这里直接返回0即可。
/// </summary>
public class StudentComparer : IEqualityComparer<Student>
{
    public bool Equals(Student? x, Student? y)
    {
        if (x == null || y == null)
            return false;
        if (x.Name == y.Name && x.Age == y.Age && x.BMI == y.BMI && x.Gender == y.Gender)
            return true;
        return false;
    }

    public int GetHashCode([DisallowNull] Student obj)
    {
        return 0;
    }
}

class Person
{
    public string Name { get; set; } = null!;
}

class Pet
{
    public string Name { get; set; } = null!;
    public Person Owner { get; set; } = null!;
}

interface IFoo { }
class BaseFoo : IFoo { }
class Foo : BaseFoo { }
class Bar : IFoo { }

//反射

//允许程序在运行时Runtime检查和操作对象的类型信息，通过反射可以动态地创建对象、调用方法、访问字段和属性，无需在编译时显式地知道类型信息。
//类型信息存储在元数据中，反射技术允许通过一系列API在运行时访问这些元数据，从而获得类型信息。

#region 使用反射实现一个对object的序列化：Object对象 -> Json对象
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.Reflection;


Student Peter = new Student { Name = "Peter", Id = 001, Class = "1班", Gender = Gender.Male, Age = 23 };
//使用自己写的Serialize方法进行序列化
Console.WriteLine(Serialize(Peter));
//使用Newtonsoft.Json包提供的序列化方法
Console.WriteLine(JsonConvert.SerializeObject(Peter));

//Serialize方法对Object进行序列化，显示其属性，由于反射特性的使用，并不局限于某种数据类型
string Serialize(Object obj)
{
    var res = obj.GetType()
        .GetProperties(BindingFlags.Instance | BindingFlags.Public) //获取所有Instance&&Public的属性
        .Where(x =>
        {
            var attr = x.GetCustomAttribute<BrowsableAttribute>(); //通过反射拿到BrowsableAttribute然后根据值判断需不需要序列化
            if (attr is not null && attr.Browsable is false)
                return false;
            return true;
        })
        .Where(x =>
        {
            var attr = x.GetCustomAttribute<MyBrowsableAttribute>(); //通过反射拿到BrowsableAttribute然后根据值判断需不需要序列化
            if (attr is not null && attr.MyBrowsable is false)
                return false;
            return true;
        })
        .Select(x => $"{x.Name}: {x.GetValue(obj)}");
    return string.Join(Environment.NewLine, res);
}

class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    [MyBrowsable(false)] //Serialize方法使用，自定义一个attribute，作用和Browsable相同
    public int Age { get; set; }
    [JsonConverter(typeof(StringEnumConverter))] //Newtonsoft.Json使用，序列化string而不是int
    public Gender Gender { get; set; }
    [Browsable(false)] //Serialize方法使用。仅仅引入该特性无法起作用，框架必须通过反射操作才可以使该反射生效
    [JsonIgnore] //Newtonsoft.Json使用
    public string Class { get; set; } = null!;
}

[AttributeUsage(AttributeTargets.Property)] //限制MyBrowsableAttribute只能用于property，其他的例如class等无法使用
class MyBrowsableAttribute : Attribute
{
    public MyBrowsableAttribute(bool attr)
    {
        MyBrowsable = attr;
    }

    public bool MyBrowsable { get; set; }
}

enum Gender
{
    Male, Female
}
#endregion

#region 使用反射实现一个Benchmark

//using System.Diagnostics;
//using System.Reflection;

//BenchmarkRunnerV1<SimpleTester>();
//BenchmarkRunnerV2(typeof(SimpleTester));

//void BenchmarkRunnerV1<T>() where T : new()
//{
//    var obj = new T(); //将泛型通过空构造实例化
//    var methods = obj.GetType() //获取所有被BenchMark标记的methods;也可以写成typeof(T)
//        .GetMethods()
//        .Where(x => x.GetCustomAttribute<BenchMarkAttribute>() is not null);

//    //将每个method执行若干次，然后打印消耗的时间
//    foreach (var method in methods)
//    {
//        var sw = Stopwatch.GetTimestamp();

//        for (int i = 0; i < 10_000_000; i++)
//        {
//            method.Invoke(obj, null);
//        }

//        Console.WriteLine(Stopwatch.GetElapsedTime(sw).Milliseconds);
//    }
//}

////有时无法传入泛型，这时只能传入Type
//void BenchmarkRunnerV2(Type type, int count= 10_000_000)
//{
//    var obj = Activator.CreateInstance(type); //实例化
//    var methods = type //获取所有被BenchMark标记的methods
//        .GetMethods()
//        .Where(x => x.GetCustomAttribute<BenchMarkAttribute>() is not null);

//    //将每个method执行若干次，然后打印消耗的时间
//    foreach (var method in methods)
//    {
//        var sw = Stopwatch.GetTimestamp();

//        for (int i = 0; i < 10_000_000; i++)
//        {
//            method.Invoke(obj, null);
//        }

//        Console.WriteLine(Stopwatch.GetElapsedTime(sw).Milliseconds);
//    }
//}

//class SimpleTester
//{
//    private IEnumerable<int> testList = Enumerable.Range(1, 10).ToArray();

//    [BenchMark]
//    public int CalcMinByLINQ()
//    {
//        return testList.Min();
//    }

//    [BenchMark]
//    public int CalcMinNaive()
//    {
//        int min = int.MaxValue;
//        foreach (int i in testList)
//        {
//            if (i < min)
//                min = i;
//        }
//        return min;
//    }
//}

//[AttributeUsage(AttributeTargets.Method)]
//class BenchMarkAttribute : Attribute
//{}
#endregion

#region 案例
//需求，写两个方法，分别改变demo对象的属性和字段的值

//using System.Linq.Expressions;
//using System.Reflection;

//var demo = new Demo();
//demo.Prop = 1;
//demo.Field = 2;

////改变demo.Field
//ModifyField(ref demo.Field, 10);
//Console.WriteLine(demo.Field);

////改变demo.Prop
////ModifyProp(ref demo.Prop, 10); //报错，因为Property本质是一个方法而非变量
//ModifyPropByAction<int>(newValue => demo.Prop = newValue, 10);
//Console.WriteLine(demo.Prop);

////但这里的问题是，对于不同的类，属性名称不同，每次调用ModifyPropByAction方法是都要传入不同的action。
////使用反射解决该问题
//var method = typeof(Demo).GetProperty("Prop")!.GetSetMethod(); //获取Demo类的Prop属性的Set方法
//ModifyPropByReflection(method!, demo, 20);
//Console.WriteLine(demo.Prop);

////进一步将Action改成Expression以简化调用时的操作
//ModifyPropByExpression(x => x.Prop, demo, 40);
//Console.WriteLine(demo.Prop);

////改写demo.Field的值
//void ModifyField<T>(ref T field, T newValue)
//{
//    field = newValue;
//}

////类似的方法改写demo.Prop的值，会报错，因为Property本质是一个方法，而不是变量，需要使用action
//void ModifyProp<T>(ref T prop, T newValue)
//{
//    prop = newValue;
//}

//void ModifyPropByAction<T>(Action<T> act, T newValue)
//{
//    act.Invoke(newValue);
//}

//void ModifyPropByReflection<TInstance, TProp>(MethodInfo setMethod, TInstance target, TProp newValue)
//{
//    //实现：target.setMethod(newValue)这样的调用，这里的setMethod实际上是一个Property的set方法
//    setMethod.Invoke(target, [newValue]);
//}

//void ModifyPropByExpression<TInstance, TProp>(Expression<Func<TInstance, TProp>> expression, TInstance target, TProp newValue)
//{
//    //通过表达式树获取setMethod，然后和反射一样，通过反射调用该方法
//    var body = (MemberExpression) expression.Body;
//    var prop = (PropertyInfo) body.Member;
//    var setMethod = prop.GetSetMethod();
//    setMethod!.Invoke(target, [newValue]);
//}

//class Demo
//{
//    public int Prop { get; set; }
//    public int Field;
//}
#endregion


//参数

#region 索引器indexer
//Student stu1 = new Student();
//stu1.Name = "Tim";
//stu1["Math"] = 90;
//stu1["Math"] = 100;
//Console.WriteLine(stu1["Math"]);
#endregion

#region 值参数：没有任何关键字的参数；值参数会在方法内部创建参数的副本。
//UpdateStuName(stu1);
//Console.WriteLine($"{stu1.GetHashCode()}, {stu1.Name}"); //和UpdateName内部的形参的HashCode相同，表示修改的实际上是同一个对象。

//int x = 1;
//UpdateX(x);
//Console.WriteLine($"{x.GetHashCode()}, {x}"); //UpdateX函数内x的HashCode和外部x的HashCode不同，表示函数内外操作的是不同的对象，函数内部操作的是外部对象的副本。

////引用类型作为值参数
//static void UpdateStuName(Student stu)
//{
//	//stu会在函数内部创建参数的副本，但由于参数是一个地址，因此对地址所在对象进行操作实际上就是在操作外部传入的参数本身；
//	//如果传入的是值类型，那么在函数内部的操作，不会影响外部的值。
//	//对指针指向的对象进行修改，外部对象也会被修改。
//    stu.Name = "Tom";
//    Console.WriteLine($"{stu.GetHashCode()}, {stu.Name}");
//}
////值类型作为值参数
//static void UpdateX(int x)
//{
//	x = 100;
//    Console.WriteLine($"{x.GetHashCode()}, {x}");
//}
#endregion

#region 引用参数：ref修饰的参数；引用参数不会对传入的参数创建副本。
using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;

int y = 1;
UpdateXRef(ref y);
Console.WriteLine(y); //y的值被更改，表示UpdateXRef内部没有创建参数的副本，而是对参数本身进行操作。

Student outerStu = new Student() { Name = "Tom" };
Console.WriteLine($"{outerStu.GetHashCode()}, {outerStu.Name}");
UpdateStuRef(ref outerStu);
Console.WriteLine($"{outerStu.GetHashCode()}, {outerStu.Name}"); //outerStu变成了UpdateStuRef函数内部新创建的对象。

//值类型作为引用参数
static void UpdateXRef(ref int x)
{
	x = x + 1;
	Console.WriteLine($"{x.GetHashCode()}, {x}");
}

//引用类型作为引用参数
static void UpdateStuRef(ref Student stu)
{
	stu = new Student() { Name = "Tim" };
	//stu.Name = "Tim";
	Console.WriteLine($"{stu.GetHashCode()}, {stu.Name}");
	//这里new了一个新对象。
	//如果直接对ref stu的Name属性进行修改，那么HashCode不会改变，但是外部对象的Name会随之更改。
	//这和引用类型作为值参数传入的效果是一样的，因此可以省略ref关键字。
}
#endregion

#region ref return: 方法返回一个引用而不是值。
int v1 = 10, v2 = 20;
Console.WriteLine($"v1:{v1}, v2:{v2}");
//这里返回的max实际是v2的引用，即max和v2是同一个对象。
//调用时必须在等号左右分别标记ref，否则max依然是值类型。
ref int max = ref Max(ref v1, ref v2);
Console.WriteLine($"max:{max}");
max++;
//对ref max进行修改后，发现v2的值也随之改变。
Console.WriteLine($"v1:{v1}, v2:{v2}");

//返回形参的引用而不是值。
static ref int Max(ref int x, ref int y)
{
	if (x > y)
		return ref x;
	return ref y;
}
#endregion

#region 输出参数：out修饰的参数；输出参数不会对传入的参数创建副本。
//使用带有输出参数的内置函数
//Console.WriteLine("Please input a number:");
//string input = Console.ReadLine();
//double z = 0;
//bool flag = Double.TryParse(input, out z);
//if (!flag)
//{
//    Console.WriteLine("Input Error!");
//}
//else
//{
//	Console.WriteLine($"You have typed: {z}");
//}

////自定义一个带有输出参数的方法
//double x = 100;
//flag = DoubleParser.TryParse("a", out x);
//if (!flag)
//{
//    Console.WriteLine($"Input Error! x: {x}"); //注意如果解析失败，x的值会被覆盖为0。
//}
//else
//{
//    Console.WriteLine($"Parse: {x}");
//}

//引用类型作为输出参数，用法和上面的值类型相同。
#endregion

#region 数组参数：params修饰的参数；只能有一个数组参数，且位置必须是形参列表最后一个。
////在不使用params关键字的时候，需要先准备一个int[]，然后传入函数。
//int[] input = [1,2,3];
//int baseNumber = 100;
//bool flag = false;
//var res = CalculateSum(baseNumber,out flag, input);
//Console.WriteLine(res);

////使用params关键字后，可以将数组元素直接作为参数。
//res = CalculateSum(baseNumber, out flag, 1, 2, 3);

////内置的具有数组参数的方法
//string str = "A;B,C.D`E";
//string[] result = str.Split(',',';','.','`');
//foreach (string s in result)
//{
//    Console.WriteLine(s);
//}

////数组参数必须是在最后一个，且只能由一个。
//int CalculateSum(int baseNumber, out bool flag, params int[] offset)
//{
//	try
//	{
//		int sum = 0;
//		foreach (var item in offset)
//		{
//			sum += item;
//		}
//		flag = true;
//		return sum + baseNumber;
//	}
//	catch (Exception)
//	{
//		flag = false;
//		return 0;
//	}
//}
#endregion

#region 具名参数：调用时声明参数的名称。
//PrintInfo(Age: 10, Name: "Tom");
//PrintInfo("Tom", 10);
//void PrintInfo(string Name, int Age)
//{
//    Console.WriteLine($"{Name}-{Age}");
//}
#endregion

#region 默认参数
//PrintInfo();
//PrintInfo("Tom", 45);
//void PrintInfo(string Name= "Peter", int Age = 30)
//{
//	Console.WriteLine($"{Name}-{Age}");
//}
#endregion

#region 扩展方法：this修饰的参数；必须是public static；必须是形参列表的第一个。
//double x = 1.12345;
//double y = Math.Round(x,3);
//Console.WriteLine(y);
////当Round方法成为Double的扩展方法后：
//double z = x.Round(4); //这里会发现，Round(int digit)只有一个参数digit，因为经过this修饰后，double input这个参数就是x本身。
//Console.WriteLine(z);

////Linq是基于扩展方法的实现。
//List<int> inputList = [9, 10, 11, 12];
//Console.WriteLine(inputList.All(x => x > 10));
//List类中没有All方法的实现，因为所有Linq方法都是基于扩展方法实现的。
//进入All的实现，会发现All方法位于一个叫Enumerable的static类中，并且形参第一位用this关键字修饰，类型为this IEnumerable<TSource>，
//因此所有实现了IEnumerable这个接口的类，都可以通过该扩展方法调用All方法。
//进入List的实现，会发现List实现了IList接口，而IList实现了IEnumerable接口，因此List也就实现了IEnumerable接口，所以List类才可以通过扩展方法调用All方法。
#endregion


class Student
{
    private Dictionary<string, int> ScoreDict = new Dictionary<string, int>();

	/// <summary>
	/// 索引器，使Student实例可以通过索引访问某一科的成绩。注意返回值是int?。
	/// </summary>
	/// <param name="subject"></param>
	/// <returns></returns>
	public int? this[string subject]
	{
		get 
		{
			if (this.ScoreDict.ContainsKey(subject))
			{
				return this.ScoreDict[subject];
			}
			else 
			{
				return null;
			}
		}
		set 
		{
			// 首先判断传入的成绩是否为空。
			if (!value.HasValue)
			{
				throw new ArgumentException("Score can not be null!");
			}
			if (this.ScoreDict.ContainsKey(subject))
			{
				this.ScoreDict[subject] = value.Value;
			}
			else 
			{
				this.ScoreDict.Add(subject, value.Value);
			}
		}
	}

	public string Name { get; set; } = null!;
}

/// <summary>
/// 自己写一个DoubleParser.TryParse方法，实现和内置函数相同的功能。
/// </summary>
class DoubleParser
{
	public static bool TryParse(string input, out double res)
	{
		try
		{
			res = Double.Parse(input);
			return true;
		}
		catch (Exception)
		{
			res = 0;
			return false;
		}
	}
}

/// <summary>
/// Double类型的扩展方法
/// </summary>
/// 
static class DoubleExtension
{
	//this关键字修饰，表示Round是Double类的扩展方法。
	public static double Round(this double input, int digit)
	{
		return Math.Round(input, digit);
	}
}


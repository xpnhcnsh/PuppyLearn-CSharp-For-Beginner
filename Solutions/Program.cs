using MyUtilities;
using Solutions;

#region Quiz1:虚数四则运算
//Sol1.Imaginary v1 = new Sol1.Imaginary(3, 3);
////v1.Conjugate();
//Sol1.Imaginary v2 = new Sol1.Imaginary(3, -4);

////Console.WriteLine(Imaginary.Plus(v1, v2));
//Console.WriteLine(v1 + v2);
////Console.WriteLine(Imaginary.Minus(v1, v2));
//Console.WriteLine(v1 - v2);
////Console.WriteLine(Imaginary.Times(v1, v2));
//Console.WriteLine(v1 * v2);
////Console.WriteLine(Imaginary.Divide(v1, v2));
//Console.WriteLine(v1 / v2);

#endregion

#region Quiz2:打印九九乘法表
//Sol2.Haskell();
#endregion

#region Quiz3:十个数
//Sol3.Run();
#endregion

#region Quiz4:随机四则运算
//Sol4.RunV1();
//Sol4.RunV2();
#endregion

#region Quiz5:个位十位百位之和
//Sol5.Run();
#endregion

#region Quiz6:前n项和
//var Sol6Obj = new Sol6();
//if (Sol6Obj.SumOfFirstNs([3, 2, 3], 2, out dynamic res))
//    Console.WriteLine($"Result is {res}");
//else
//    Console.WriteLine(res);
#endregion

#region Quiz7:反转List
//try
//{
//    List<string> list = ["Peter", "Syd", "ButterFly", "Shit"];
//    list.Reverse();
//    //调用ReverseAList
//    var res1 = Sol7.ReverseAList(list, 1, 2);
//    Console.WriteLine(string.Join(",", res1));
//    //调用ReverseAListLinq
//    var res2 = list.ReverseAListLinq(1, 2);
//    Console.WriteLine(string.Join(",", res2));
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message); ;
//}
#endregion

#region Quiz8:修改最大值
//int[] foo = [6, 3, 1, 3, 56, 3, 1, 3, 100, 94];
//ref int max = ref Sol8.FindMax(foo);
//max = 0;
//foreach (int i in foo)
//{
//    Console.Write($"{i} ");
//}
#endregion

#region Quiz9:委托基本使用
//var Sol9 = new Sol9();
//Sol9.Run();
#endregion

#region Quiz10:自定义sort方法
//var roster = new Roster([new Student(1, 21, 180), new Student(4, 22, 165), new Student(3, 23, 175)]);
//roster.Sort(x=>x.Id);
//foreach (Student s in roster.Value)
//    Console.WriteLine(s);
#endregion

#region Quiz11:数组查重
//Console.WriteLine(Sol11.ContainsDuplicate<string>(["1", "2", "3", "4" ,"1"])); 
#endregion

#region Quiz12:第N大的数
//Console.WriteLine(Sol12.ThirdBiggest([1,2,3,4,1,2,3], 3));
#endregion

#region Quiz13:Linq
var Sol13 = new Sol13();
string[] Schools = ["Yale", "HeiBeiNormal", "Harvard", "Stanfords", "Cambridge", "TSINGHUA"];
#region 1 Reorder
Sol13.Reorder(Schools).Show();
#endregion

#region 2 GroupBy
Sol13.Census(Schools);
#endregion

#region 3 Shuffle
Sol13.Shuffle(Schools).Show();
#endregion

#region 4 FindCapitals
Sol13.FindCapitals(Schools);
#endregion
#endregion
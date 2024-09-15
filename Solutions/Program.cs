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
try
{
    List<string> list = ["Peter", "Syd", "ButterFly", "Shit"];
    list.Reverse();
    //调用ReverseAList
    var res1 = Sol7.ReverseAList(list, 1, 2);
    Console.WriteLine(string.Join(",", res1));
    //调用ReverseAListLinq
    var res2 = list.ReverseAListLinq(1, 2);
    Console.WriteLine(string.Join(",", res2));
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message); ;
}
#endregion

#region Quiz8:
//var Sol8 = new Sol8();
//Sol8.Run();
#endregion
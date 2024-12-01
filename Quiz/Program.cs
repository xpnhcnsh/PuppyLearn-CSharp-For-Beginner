Console.WriteLine();
//Quiz1:
//虚数加减乘除

//Quiz2:
//打印九九乘法表

//Quiz3:
//提示用户输入10个实数，可以是整数也可以是小数，可以是正也可以是负数，每个数之间用逗号分隔，
//然后显示出这10个数里最大的数，平均数，和总和，输入'exit'退出程序，否则无限循环出题。

//Quiz4:
//随机显示一个加减乘除的式子，例如10*10=，然后让用户输入一个结果，如果结果正确，就显示“结果正确”，如果错误，就显示“错误”，
//然后再次让用户输入，输入3次错误后，直接显示正确答案；重复以上直到用户输入了'exit'，程序退出。

//Quiz5:
//读取用户输入的100-999之间的整数，将其个位、十位、百位数值相加，显示其值。

//Quiz6:
//给一个List<T>，求前n项和。提示：使用递归。

//Quiz7: T->泛型
//反转一个List<T>，用户可以自定义需要反转的范围，例如输入0,2，表示只对index在[0,2]的sublist进行反转，其余部分不变；如果只
//输入一个List<T>，那么将所有内容反转。

//Quiz8:
//有一个int[] foo = [6, 3, 1, 3, 56, 3, 1, 3, 100, 94]
//使用Array.FindIndex()方法，找到其最大值(可以使用内置方法array.Max()方法，或自己写一个返回最大值的方法)，调用这个方法返回其最大值并用一个变量接收，修改这个变量的值为0，使这个array中的最大值这一位也变成0。
//注意：不能直接通过index修改，例如不能使用：foo[8] = 0;

//Quiz9:
//MyUtlities是一个Class Library， 其中有两个文件：MyClass.cs中定义了Student和MyClass两个类，其中MyClass类有一个
//静态方法，可以生成一个默认的MyClass对象，其中有5个学生；而MyDelegates.cs文件中声明了3个委托。仔细阅读MyClass.cs和MyDelegates.cs两个文件，
//查看MyClass类中需要用到哪些委托去计算其属性，然后在自己的Sol9.cs中，利用MyClass.cs和MyDelegates.cs实现：1.使用静态方法创建一个班级；
//2.根据MyDelegates.cs，自己写相应委托的方法，并完成委托绑定；3.调用ProcessBFR()方法。

//正确结果应为：1.实现对班级每个学生BFR的计算；2.实现班级平均BFR的计算；3.打印出处理前和处理后的数据。
//BFR计算公式为：
//男生：1.2 * BMI + 0.23 * Age - 5.4 - 10.8 * 1
//女生：1.2 * BMI + 0.23 * Age - 5.4 - 10.8 * 0

//Quiz10:
//写一个Student类，具有Id，Age和Height属性，均为int类型。
//Roster类，具有一个属性/字段，其为Student类的一个集合；该类还有一个Sort扩展方法，其接受一个委托参数Func<Student, int> myRule，
//该参数可自定义Roster对象中集合成员排序的规则，例如，如果myRule指定使用Age排序，那么调用Sort方法就会使Roster对象内部集合根据Age属性排序。Sort方法是一个inplace方法。
//注意：在Sort方法内部，可以使用Linq自带的OrderBy方法。如果不想使用OrderBy方法，需要自己写排序算法。

//Quiz11:
//输入一个非空数组nums，如果数组中具有重复的元素，则返回true；否则返回false。

//Quiz12：
//输入一个非空整数数组nums，返回其中第三大的整数；如果不存在第三大的整数，返回最大值；

//Quiz13:
//string[] Schools = ["Yale", "HeiBeiNormal", "Harvard", "Stanfords", "Cambridge", "TSINGHUA"];
//1:将Schools重新排序，要求按照字母个数从小到大排序，字母个数相同时，按照字母表顺序排列。
//2.统计Schools中每个字母出现的次数，并按照从少到多打印，个数相同的字母按照字母表顺序打印。
//3.乱序排列Schools。
//4.找到其中全大写的元素。
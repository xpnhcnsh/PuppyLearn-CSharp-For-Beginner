
using System.Diagnostics;
using static MyUtilities.MyDelegates;

namespace MyUtilities
{
    public class Student
    {
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public int BMI { get; set; }
        public bool Gender { get; set; } // 1 for male and 0 for female
        public double? BFR { get; set; }

        public Student(string name, int age, int bMI, bool gender)
        {
            Name = name;
            Age = age;
            BMI = bMI;
            Gender = gender;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}, BMI: {BMI}, Gender: {(Gender == true ? "male" : "female")}, BFR:{(BFR == null ? "N/A" : BFR)}";
        }
    }

    public class MyClass
    {
        public List<Student> Roster { get; set; } = null!;
        public double? AvgBFR = null;
        public DelegateCalculateBFR CalculateBFR = null!;
        public DelegateAvgBFR CalculateAvgBFR = null!;
        public DelegatePrint Print = null!;

        public static MyClass DefaultClass()
        {
            var Roster = new List<Student>
            {
            new Student("Peter", 30, 30, true),
            new Student("Cindy", 28, 45, false),
            new Student("Huk", 23, 38, true),
            new Student("Syd", 45, 31, true),
            new Student("Juliet", 25, 30, false),
            };
            return new MyClass(Roster) { AvgBFR = null };
        }

        public MyClass(List<Student> roster)
        {
            Roster = roster;
        }
        public void ProcessBFR()
        {
            Console.WriteLine(Print(this));
            CalculateBFR(this);
            CalculateAvgBFR(this);
            Console.WriteLine(Print(this));
        }
        public override string ToString()
        {
            return string.Join("\n", Roster);
        }
    }

    /// <summary>
    /// 使用using语句包装SimpleTimer，即可打印出作用域内执行的时间
    /// </summary>
    public class SimpleTimer : IDisposable
    {
        private readonly long _start;
        private readonly string _stopMsg;

        public SimpleTimer(string? startMsg = "", string stopMsg = "Elapsed time: {0:F3}s")
        {
            _stopMsg = stopMsg;
            if(!string.IsNullOrEmpty(startMsg))
                Console.WriteLine(startMsg);
            _start = Stopwatch.GetTimestamp();
        }

        public void Dispose()
        {
            Console.WriteLine(_stopMsg, Stopwatch.GetElapsedTime(_start).TotalSeconds);
        }
    }
}

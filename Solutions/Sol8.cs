using MyUtilities;

namespace Solutions
{
    public class Sol8
    {
        private MyClass _Class = MyClass.DefaultClass();
        public void Run()
        {
            _Class.CalculateBFR = GetBFR;
            _Class.CalculateAvgBFR = GetAvgBFR;
            _Class.Print = ToString;
            _Class.ProcessBFR();
        }

        private void GetBFR(MyClass value)
        {
            value.Roster.ForEach(x => x.BFR = x.Gender ? 1.2 * x.BMI + 0.23 * x.Age - 5.4 - 10.8 : 1.2 * x.BMI + 0.23 * x.Age - 5.4);
        }

        private void GetAvgBFR(MyClass value)
        {
            value.AvgBFR = value.Roster.Average(x => x.BFR)!.Value;
        }

        private string ToString(MyClass value)
        {
            return $"{value.ToString()}\nThe average BFR is:{(value.AvgBFR!=null?value.AvgBFR:"N/A")}\n";
        }
    }
}

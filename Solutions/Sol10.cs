namespace Solutions
{
    public class Student
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }

        public Student(int id, int age, int height)
        {
            Id = id;
            Age = age;
            Height = height;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Age:{Age}, Height:{Height}";
        }
    }

    public class Roster
    {
        private IList<Student> _collection;
        public IList<Student> Value { get { return _collection; } set { _collection = value; } }
        public Roster(IList<Student> collection)
        {
            _collection = collection;
        }
    }

    public static class RosterExtension 
    {
        public static void Sort(this Roster roster, Func<Student, int> myRule)
        {
            roster.Value = roster.Value.OrderBy(myRule).ToList();
        }
    }

}

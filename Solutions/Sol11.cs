namespace Solutions
{
    internal class Sol11
    {
        public static bool ContainsDuplicate<T>(T[] foo)
        {
            return foo.ToHashSet().Count() != foo.Count();
        }
    }
}

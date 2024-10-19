namespace Solutions
{
    internal class Sol11
    {
        public static bool ContainsDuplicate<T>(T[] foo)
        {
            HashSet<T> set = new HashSet<T>();
            foreach (T item in foo) 
            {
                if (!set.Contains(item))
                {
                    set.Add(item);
                }
                else
                    return true;
            }
            return false;
        }
    }
}

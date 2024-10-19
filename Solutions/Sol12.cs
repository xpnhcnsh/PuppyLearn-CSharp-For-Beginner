namespace Solutions
{
    internal class Sol12
    {
        public static int ThirdBiggest(int[] nums)
        {
            SortedSet<int> container = new();
            foreach (int num in nums)
            {
                container.Add(num);
                if (container.Count > 3)
                {
                    container.Remove(container.Min);
                }
            }
            return container.Count == 3 ? container.Min : container.Max;
        }
    }
}

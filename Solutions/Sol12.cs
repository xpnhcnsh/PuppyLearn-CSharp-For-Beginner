using MyUtilities;

namespace Solutions
{
    internal class Sol12
    {
        public static int ThirdBiggest(int[] nums, int k)
        {
            var hashSet = nums.ToHashSet().OrderDescending();
            return hashSet.Count() >= 3 ? hashSet.ToList()[k-1]: hashSet.Max();
        }
    }
}

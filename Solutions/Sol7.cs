namespace Solutions
{
    //Quiz7:
    //反转一个List<T>，用户可以自定义需要反转的范围，例如输入0,2，表示只对index在[1,3]的sublist进行反转，其余部分不变；如果只
    //输入一个List<T>，那么将所有内容反转。
    public static class Sol7
    {
        /// <summary>
        /// Use in the Linq way, achieved by List extension method (Note to use extension, the class must be static).
        /// Reverse the whole input list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">Input list.</param>
        /// <returns>Return a new list.</returns>
        public static List<T> ReverseAListLinq<T>(this List<T> input)
        {
            return _ReverseAList(input, 0, input.Count - 1);
        }
        /// <summary>
        /// Use in the static function way. Reverse the whole input list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">Input list.</param>
        /// <returns>Return a new list.</returns>
        public static List<T> ReverseAList<T>(List<T> input)
        {
            return _ReverseAList(input, 0, input.Count - 1);
        }

        /// <summary>
        /// Use in the Linq way, achieved by List extension method (Note to use extension, the class must be static).
        /// Only reverse sublist defined by low and high index.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">Input list.</param>
        /// <param name="low">Low index.</param>
        /// <param name="high">High index.</param>
        /// <returns>Return a new reversed list</returns>
        public static List<T> ReverseAListLinq<T>(this List<T> input, int low, int high)
        {
            try
            {
                if (low > high)
                    throw new Exception("Low index can not exceed high index!");
                else
                    return _ReverseAList(input, low, high);
            }
            catch (Exception)
            {

                throw; ;
            }
        }

        /// <summary>
        /// Use in the static function way. Only reverse sublist defined by low and high index.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">Input list.</param>
        /// <param name="low">Low index.</param>
        /// <param name="high">High index.</param>
        /// <returns>Return a new reversed list</returns>
        public static List<T> ReverseAList<T>(List<T> input, int low, int high)
        {
            try
            {
                if (low > high)
                    throw new Exception("Low index can not exceed high index!");
                else
                    return _ReverseAList(input, low, high);
            }
            catch (Exception)
            {

                throw; ;
            }
            
        }

        /// <summary>
        /// Use recursion to reverse an input list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">Input list.</param>
        /// <param name="low">Low index.</param>
        /// <param name="high">High index.</param>
        /// <returns></returns>
        private static List<T> _ReverseAList<T>(List<T> input, int low, int high) 
        {
            if (low >= high)
            {
                return input;
            }
            else
            {
                T temp = input[low];
                input[low] = input[high];
                input[high] = temp;
                return _ReverseAList(input, low+1, high-1);
            }
        }
    }
}

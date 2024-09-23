namespace MyUtilities
{
    public static class MyExtensions
    {
        public static void Show<T>(this IEnumerable<T> collection)
        {
            foreach (T item in collection)
                Console.Write(item + " ");
            Console.WriteLine();
        }
    }
}

namespace Solutions
{
    public static class Sol2
    {
        public static void Haskell()
        {
            for (int i = 1; i < 10; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    Console.Write($"{i}*{j}={i * j}\t");
                }
                Console.WriteLine();
            }
        }
    }
}

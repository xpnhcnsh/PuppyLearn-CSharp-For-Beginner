namespace Solutions
{
    //Quiz5:
    //读取用户输入的100-999之间的整数，将其个位、十位、百位数值相加，显示其值。
    public static class Sol5
    {
        public static void Run()
        {
            while (true)
            {
                Console.WriteLine("Please input an integer between 100-999");
                string inputStr = Console.ReadLine();
                int input;
                if (string.IsNullOrEmpty(inputStr))
                {
                    Console.WriteLine("Input can not be empty!");
                    continue;
                }
                else if (inputStr.ToLower().Equals("exit"))
                {
                    Environment.Exit(0);
                }
                if (!int.TryParse(inputStr, out input) || input < 100 || input > 999)
                {
                    Console.WriteLine("Input must be an integer between 100-999");
                    continue;
                }
                int hundreds = input/100;
                int units = input % 10;
                int tens = (input - units)/10%10;
                int sum = hundreds + tens + units;
                Console.WriteLine($"Sum of units, tens and hundreds is: {sum}");
            }
        }
    }
}


using System.Data;

namespace seandiedie_submit
{
   
    internal class Sol4
    {

        
        
           public void Main()
            {
                Random random = new Random();
                DataTable table=new DataTable();

                bool isrunning = false;
                while (true)
                {
                    int A = random.Next(1, 11);
                    int B = random.Next(1, 11);
                    char[] operators = { '+', '-', '*', '/' };
                    char op = operators[random.Next(operators.Length)];
                    string expression = "";
                    switch (op)
                    {
                        case '+': expression = $"{A}+{B}"; break;
                        case '-': expression = $"{A}-{B}"; break;
                        case '*': expression = $"{A}*{B}"; break;
                        case '/': expression = $"{A}/{B}"; break;
                    }

                    Console.WriteLine($"{A}{op}{B}=?");

                var correctAnswer = table.Compute(expression,null);
                int attemps = 0;
                while (attemps < 3)
                {
                    string userInput = Console.ReadLine();
                    if (userInput.ToLower() == "exit")
                    {
                        isrunning = false; break;
                    }
                    if (userInput == correctAnswer.ToString())
                    {
                        Console.WriteLine("结果正确"); break;
                    }

                    else
                    {
                        Console.WriteLine("错误"); attemps++;
                        if (attemps == 3)
                            Console.WriteLine($"正确答案是：{correctAnswer}");
                    }
                }

            }
            
        }
    }
}


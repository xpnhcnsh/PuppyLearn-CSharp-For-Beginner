using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seandiedie_submit
{
    using System;
    using System.ComponentModel.Design;
    using System.Data;
    internal class sol4
    {

        class MyClass
        {
            static void Main()
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

                    Console.WriteLine($"{A}{operators}{B}=?");
                    
                    var correctAnswer = DataTable.Compute(expression);
                    int attemps = 0;
                    while (attemps < 3)
                    {
                        string userInput = Console.ReadLine();
                        if (userInput.ToLower() == "exit")
                        {
                            isrunning = false; break;
                        }
                        if (userInput == correctAnswer.Tostring())
                        { 
                        Console.WriteLine("结果正确"); break;
                            }

                    else
                        { Console.WriteLine("错误"); attemps++;
                            if (attemps == 3)
                          Console.WriteLine($"正确答案是：{correctAnswer}");
                        }
                    }

                }
            }
        }
    }
}


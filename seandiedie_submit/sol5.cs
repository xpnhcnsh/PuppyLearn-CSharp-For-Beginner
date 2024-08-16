using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seandiedie_submit
{
    internal class Sol5
    {
        
            public  void Main()
            {
                Console.WriteLine("请输入100到999之间的整数");

                int inputNumber;
                if(int.TryParse(Console.ReadLine(), out inputNumber) && inputNumber >= 100 && inputNumber <= 999)
                {
                    int hundreds = inputNumber / 100;
                    int tens = (inputNumber / 10) % 10;
                    int units = inputNumber % 10;
                    int sum =hundreds+tens+units;
                    Console.WriteLine(sum);
                }
            }
        

    }
}

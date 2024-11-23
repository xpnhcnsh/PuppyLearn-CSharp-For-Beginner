using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions
{
    class Sol13
    {
        public static string[] Shuffle(string[] array)
        {
            return array.OrderBy(x => Random.Shared.Next()).ToArray();
        }

        public static string[] Reorder(string[] array) 
        {
            return array.OrderBy(x=>x.Length).ThenBy(x=>x).ToArray();
        }

        public static void Census(string[] array)
        {
            //string[] flatten = [.. array]; //使用集合表达式进行展开，注意这里必须指明数据类型，不能使用var推断。
            //var res = flatten.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count()).OrderBy(x => x.Value).ThenBy(x => x.Key);
            var res = array.SelectMany(x => x).GroupBy(x=>x).ToDictionary(g=>g.Key, g=>g.Count()).OrderBy(x=>x.Value).ThenBy(x=>x.Key);
            foreach (var (key, value) in res)
            {
                Console.WriteLine($"{key}: {value}");
            }
        }

        public static void FindCapitals(string[] array) 
        {
            Console.WriteLine(string.Join(", ", array.Where(x => x.Equals(x.ToUpper())).Select(x => x)));
        }
    }
}

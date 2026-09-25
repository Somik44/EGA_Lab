using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EGA_1
{
    internal class Monte_Carlo
    {
        static private Random rnd = new Random();

        public static void Algorithm(Dictionary<string, decimal> map, int N, out decimal max, out string maxS)
        {
            maxS="";
            max = 0;
            decimal oldMax = 0;
            bool flag = false;

            List<string> keys = map.Keys.ToList();

            for (int i =0; i<N; i++)
            {
                int index = rnd.Next(keys.Count);
                var nowKey = keys[index];
                decimal nowValue = map[nowKey];

                if (i == 0)
                {
                    maxS = nowKey; 
                    max = nowValue;
                }
                else
                {
                    if (max < nowValue)
                    {
                        oldMax = max;
                        maxS = nowKey;
                        max = nowValue;
                        flag = true;
                    }
                }
                if (i == 0) Console.Write($"\n\t{i}. Set: Value = {max} Code = {maxS}\n");
                else Console.Write($"\n\t{i}. MAX: {max} - {maxS}; Now Value: {nowValue} - {nowKey};");
                
                if (flag) { 
                    Console.Write($" {oldMax} -> {max}");
                    flag = false;
                }
                
            }

            Console.WriteLine($"\n\n\tИтоговое решение: {maxS} - {max}");
        }

    }
}

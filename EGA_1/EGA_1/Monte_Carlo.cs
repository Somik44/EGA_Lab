using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EGA_1
{
    internal class Monte_Carlo
    {
        public void GetMap(Dictionary<string, int> map, int L)
        {
            Random random = new Random();
            Console.WriteLine("Ландшафт приспособленности:");
            string nowValue;
            int i;
            for (i = 0; i < Math.Pow(2,L); i++)
            {
                nowValue = Convert.ToString(i, 2).PadLeft(L, '0');
                map[nowValue] = random.Next(1, 101);
            }

            i = 0;
            foreach(KeyValuePair<string, int> pair in map)
            {
                Console.WriteLine($"{pair.Key} - {pair.Value}");
                i++;
                if (i == 32) break;
            }
        }

        public void Algorithm(Dictionary<string, int> map, int N)
        {
            string maxS="";
            int max=0, oldMax = 0;
            bool flag = false;
            Random rnd = new Random();

            List<string> keys = map.Keys.ToList();

            for (int i =0; i<N+1; i++)
            {
                int index = rnd.Next(keys.Count);
                var nowKey = keys[index];
                int nowValue = map[nowKey];

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
                if (i == 0) Console.Write($"\nSet: Value = {max} Code = {maxS}\n");
                else Console.Write($"\n{i}) MAX: {max} - {maxS}; Now Value: {nowValue} - {nowKey};");
                
                if (flag) { 
                    Console.Write($" {oldMax} -> {max}");
                    flag = false;
                }
                
            }

            Console.WriteLine($"\n\nИтоговое решение: {maxS} - {max}");
        }

    }
}

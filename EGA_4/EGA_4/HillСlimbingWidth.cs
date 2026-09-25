using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGA_2
{
    internal class HillСlimbingWidth
    {
        static private Random rnd = new Random();

        static private List<string> GetVicinity(string key)
        {
            var vicinity = new List<string>();
            char[] chars = key.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = chars[i] == '0' ? '1' : '0';
                vicinity.Add(new string(chars));
                chars[i] = chars[i] == '0' ? '1' : '0';

            }

            return vicinity;
        }

        static public void Climbing(Dictionary<string, decimal> map, int N, out decimal max, out string maxS)
        {
            int i = 0;
            decimal oldMax;
            string oldMaxS;
            bool flag = false;
            var pairs = map.ToList();

            var pair = pairs[rnd.Next(pairs.Count)];
            maxS = pair.Key;
            max = pair.Value;
            List<string> vicinity = GetVicinity(pair.Key);

            Console.WriteLine($"\n\n\t{i}) SET: {maxS} - {max}; {{ {Format(vicinity, map)} }}");

            while (i < N)
            {
                i++;

                int ind = SeeingVicinity(vicinity, map);
                string chosen = vicinity[ind];
                decimal chosenFit = map[chosen];

                oldMax = max;
                oldMaxS = maxS;
                flag = chosenFit > max;

                if (flag)
                {
                    maxS = chosen;
                    max = chosenFit;
                    vicinity = GetVicinity(chosen);
                }

                Console.Write($"\t{i}) Выбрали: {chosen} - {chosenFit}; ");
                if (flag)
                {
                    Console.Write($"MAX: {oldMax} -> {max} ({oldMaxS} -> {maxS}); ");
                }
                else
                {
                    Console.Write($"MAX не изменился: {max} - {maxS}; ");
                }
                Console.WriteLine($"vicinity = {{ {Format(vicinity, map)} }} \n");

                if (!flag) break;
            }


            Console.WriteLine($"\n\tЛучшее решение: {maxS} - {max}");
        }

        static private int SeeingVicinity(List<string> vicinity, Dictionary<string, decimal> map)
        {
            int id = 0;
            for (int i = 1; i < vicinity.Count; i++)
            {
                if (map[vicinity[i]] > map[vicinity[id]]) id = i;
            }

            return id;
        }

        static private string Format(List<string> vicinity, Dictionary<string, decimal> map) => string.Join(", ", vicinity.Select(k => $"{k} - {map[k]}"));
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGA_2
{
    internal class HillСlimbing
    {
        Random rnd = new Random();

        public void CreateMap(Dictionary<string, int> map, int L)
        {
            string key;
            int i;

            for (i = 0; i < Math.Pow(2, L); i++)
            {
                key = Convert.ToString(i, 2).PadLeft(L, '0');
                map[key] = (int)Math.Pow((i - Math.Pow(2, L-1)), 2);
            }
            Console.WriteLine("Ландшафт приспособленности:");

            i = 0;
            foreach (KeyValuePair<string, int> pair in map)
            {
                Console.WriteLine($"{pair.Key} - {pair.Value}");
                i++;
            }
        }

        private List<string> GetVicinity(string key)
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

        public void Climbing(Dictionary<string, int> map, int N)
        {
            int oldMax, i = 0;
            string oldMaxS;
            bool flag = false;
            var pairs = map.ToList();

            var pair = pairs[rnd.Next(pairs.Count)];
            string maxS = pair.Key;
            int max = pair.Value;
            List<string> vicinity = GetVicinity(pair.Key);

            Console.WriteLine($"\n\n{i}) SET: {maxS} - {max}; {{ {Format(vicinity, map)} }}");

            while (i < N)
            {
                i++;

                int ind = SeeingVicinity(vicinity, map);
                string chosen = vicinity[ind];
                int chosenFit = map[chosen];

                oldMax = max;
                oldMaxS = maxS;
                flag = chosenFit > max;

                if (flag)
                {
                    maxS = chosen;
                    max = chosenFit;
                    vicinity = GetVicinity(chosen);
                }

                Console.Write($"{i}) Выбрали: {chosen} - {chosenFit}; ");
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


            Console.WriteLine($"\nЛучшее решение: {maxS} - {max}");
        }

        private int SeeingVicinity(List<string> vicinity, Dictionary<string, int> map)
        {
            int id = 0;
            for (int i = 1; i < vicinity.Count; i++)
            {
                if (map[vicinity[i]] > map[vicinity[id]]) id = i;
            }

            return id;
        }

        private string Format(List<string> vicinity, Dictionary<string, int> map) => string.Join(", ", vicinity.Select(k => $"{k} - {map[k]}"));
    }
}
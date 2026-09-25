using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGA_4
{
    internal class CreateMap
    {
        static public Dictionary<string, decimal> Create(int L)
        {
            Dictionary<string, decimal> map = new Dictionary<string, decimal>();
            string key;
            int i;

            for (i = 0; i < Math.Pow(2, L); i++)
            {
                key = Convert.ToString(i, 2).PadLeft(L, '0');
                if (i == 0) map[key] = 0.00m;
                else map[key] = Math.Round((decimal)(5*Math.Sin(i) + Math.Log(i)), 2);
            }
            Console.WriteLine("Ландшафт приспособленности:");

            i = 0;
            foreach (KeyValuePair<string, decimal> pair in map)
            {
                Console.WriteLine($"{pair.Key} - {pair.Value}");
                i++;
                if (i == 32) break;
            }

            return map;
        }
    }
}

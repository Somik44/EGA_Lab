using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace EGA_1
{
    internal class Monte_Carlo
    {
        ConnectionMultiplexer redis;
        IDatabase db;
        Random rnd;

        public Monte_Carlo()
        {
            redis = ConnectionMultiplexer.Connect("localhost:6379");
            db = redis.GetDatabase();
            rnd = new Random();
        }

        public void GetMap(Dictionary<string, int> map, int L)
        {

            bool exists = db.KeyExists("landscape:15");
            int total = Convert.ToInt32(Math.Pow(2, L));
            int i = 0;

            if (!exists)
            {
                var entries = new HashEntry[total];
                for (i = 0; i < total; i++)
                {
                    string key = Convert.ToString(i, 2).PadLeft(L, '0');
                    int value = rnd.Next(1, 10000);
                    entries[i] = new HashEntry(key, value);
                }
                db.HashSet("landscape:15", entries);
            }

            var hashEntries = db.HashGetAll("landscape:15").OrderBy(t => Convert.ToInt32(t.Key, 2));
            foreach (var entry in hashEntries)
            {
                map[entry.Name.ToString()] = (int)entry.Value;
            }

            Console.WriteLine("Ландшафт приспособленности:");

            i = 0;
            foreach (KeyValuePair<string, int> pair in map)
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

            List<string> keys = map.Keys.ToList();

            for (int i =0; i<N; i++)
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

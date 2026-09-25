using EGA_1;
using EGA_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGA_4
{
    internal class MultipleLaunch
    {
        static private Random rnd = new Random();

        public static void Method(Dictionary<string, decimal> map, int N, int n)
        {
            decimal nowMax = 0, max = 0; 
            string nowMaxS = "", maxS = "";
            int topInd = -1;

            for (int i = 0; i < N; i++)
            {
                int indx = rnd.Next(0,3);
                Console.Write($"\n{i}) ");
                if (indx == 0)
                {
                    Console.Write($"Mонте-Карло: ");
                    Monte_Carlo.Algorithm(map, n, out nowMax, out nowMaxS);

                }
                else if (indx == 1)
                {
                    Console.Write($"В глубину: ");
                    HillСlimbingDeep.Climbing(map, n, out nowMax, out nowMaxS);
                }
                else
                {
                    Console.Write($"В ширину: ");
                    HillСlimbingWidth.Climbing(map, n, out nowMax, out nowMaxS);

                }

                Console.Write($"\nПолучили - {{ {nowMax} - {nowMaxS} }};");

                if (i == 0)
                {
                    max = nowMax;
                    maxS = nowMaxS;
                    topInd = indx;
                }
                else
                {
                    if (nowMax >  max)
                    {
                        Console.WriteLine($" {max} -> {nowMax} ({maxS} -> {nowMaxS})");
                        max = nowMax;
                        maxS = nowMaxS;
                        topInd = indx;
                    }
                    else
                    {
                        Console.WriteLine($" Смены не произошло ({max} - {maxS})");
                    }
                }

            }

            Console.WriteLine($"\nЛучшее решение: {max} - {maxS}");
            Console.Write($"\nЛучший алгоритм: ");
            switch (topInd)
            {
                case 0: Console.WriteLine("Монте-Карло"); break;
                case 1: Console.WriteLine("В глубину"); break;
                case 2: Console.WriteLine("В ширину"); break;
                default: break;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGA_9
{
    internal class TspNearestCitySolver
    {

        static public void TspNearestCity(List<List<double>> matrix)
        {
            Random rnd = new Random();
            List<int> way = new List<int>();

            int i = 0;

            way.Add(rnd.Next(0, matrix.Count()));
            Console.WriteLine($"\nНачали в горооде {way[0]}");


            while (way.Count() < matrix.Count())
            {
                var pairs = GetValues(matrix, way);
                var values = TspRoulette.Roulette(pairs, matrix);

                way.Insert(way.IndexOf(values.From) + 1, values.To);
                GetOutputString(way, matrix, pairs, values, i);
                i++;
            }


            way.Add(way[0]);

            Console.WriteLine($"\nИтоговый путь: {{ {string.Join(", ", way)} }} = {WayLength(way, matrix)}");
        }

        private static List<(int From, int To)> GetValues(List<List<double>> matrix, List<int> way)
        {
            List<(int From, int To)> pairs = new List<(int From, int To)>();

            for (int i = 0; i < way.Count; i++)
            {
                int indColRow = 0;
                double minRowValue = Double.MaxValue;
                var row = matrix[way[i]];

                for (int j = 0; j < row.Count; j++)
                {
                    if (row[j] != 0 && row[j] < minRowValue && !way.Contains(j))
                    {
                        minRowValue = row[j];
                        indColRow = j;
                    }
                }

                pairs.Add((way[i], indColRow));
            }


            return pairs;
        }

        private static double WayLength(List<int> way, List<List<double>> matrix)
        {
            double sum = 0;
            for (int i = 0; i < way.Count - 1; i++)
                sum += matrix[way[i]][way[i + 1]];
            return sum;
        }

        private static void GetOutputString(List<int> way, List<List<double>> matrix, List<(int From, int To)> pairs, (int From, int To) values, int step)
        {
            Console.WriteLine(
            $"{step}) Выбрали пару {{ {values.From} - {values.To} }}; " +
            $"Все кандидаты {{ {string.Join(", ", pairs.Select(p => $"из {p.From} в {p.To} = {matrix[p.From][p.To]:F2}"))} }}" +
            $"  ; {{ {string.Join(", ", way)} }} = {WayLength(way, matrix)}"
            );


        }
    }

}

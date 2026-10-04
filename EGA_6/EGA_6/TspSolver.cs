using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGA_5
{
    internal class TspSolver
    {

        static public void TspNearestCity(List<List<double>> matrix)
        {
            Random rnd = new Random();
            List<int> way = new List<int>();

            int i = 0;

            way.Add(rnd.Next(0, matrix.Count()));
            

            while (way.Count() < matrix.Count())
            {
                var values = GetValues(matrix, way);
                way.Insert(way.IndexOf(values.Ind) + 1, values.IndCol);
                GetOutputString(way, matrix, values, i);
                i++;
            }


            way.Add(way[0]);

            Console.WriteLine($"\nИтоговый путь: {{ {string.Join(", ", way)} }} = {WayLength(way, matrix)}");
        }

        private static (int Ind, int IndCol, List<(int From, int To)> Pairs) GetValues(List<List<double>> matrix, List<int> way)
        {
            int ind = 0, indCol = 0;
            List<(int From, int To)> pairs = new List<(int From, int To)>();
            double minValue = Double.MaxValue;

            for (int i=0; i<way.Count; i++)
            {
                int indColRow = 0;
                double minRowValue = Double.MaxValue;
                var row = matrix[way[i]];

                for (int j=0; j<row.Count; j++)
                {
                    if (row[j] != 0 && row[j]<minRowValue && !way.Contains(j))
                    {
                        minRowValue = row[j];
                        indColRow = j;
                    }
                }

                pairs.Add((way[i], indColRow));
                if (minRowValue < minValue)
                {
                    minValue = minRowValue;
                    ind = way[i];
                    indCol = indColRow;
                }
            }
            

            return (ind, indCol, pairs);
        }

        private static double WayLength(List<int> way, List<List<double>> matrix)
        {
            double sum = 0;
            for (int i = 0; i < way.Count - 1; i++)
                sum += matrix[way[i]][way[i + 1]];
            return sum;
        }

        private static void GetOutputString(List<int> way, List<List<double>> matrix, (int Ind, int IndCol, List<(int From, int To)> Pairs) values, int step)
        {
            if (step == 0)
            {
                Console.WriteLine($"\n{step}) Начали в городе {way[0]}; " +
                $"Выбрали город {values.IndCol}; " +
                $"Все кандидаты {{ {string.Join(", ", values.Pairs.Select(p => $"из {p.From} в {p.To} = {matrix[p.From][p.To]:F2}"))} }}" +
                $"  ; {{ {string.Join(", ", way)} }} = {WayLength(way, matrix)}"
                );
            }
            else
            {
                Console.WriteLine(
                $"{step}) Выбрали город {values.IndCol}; " +
                $"Все кандидаты {{ {string.Join(", ", values.Pairs.Select(p => $"из {p.From} в {p.To} = {matrix[p.From][p.To]:F2}"))} }}" +
                $"  ; {{ {string.Join(", ", way)} }} = {WayLength(way, matrix)}"
                );
            }

        }
    }

    }

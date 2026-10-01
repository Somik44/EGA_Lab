using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGA_5
{
    internal class TspSolver
    {

        static public void Tsp(List<List<double>> matrix)
        {
            Random rnd = new Random();
            List<int> way = new List<int>();
            double wayLenght = 0;

            int ind = 0, indCol = 0, i = 0;
            while (way.Count() < matrix.Count())
            {
                if (i == 0)
                {
                    ind = rnd.Next(0, matrix.Count());
                    way.Add(ind);

                    indCol = GetIndCol(matrix[ind], way);
                    Console.Write($"\n{i}) Начали в городе {ind}; выбрали город {indCol}; Расстояние до выбранного {matrix[ind][indCol]}; ");
                    way.Add(indCol); wayLenght += matrix[ind][indCol];
                    Console.WriteLine($"{{ {string.Join(", ", way)} }} = {wayLenght}");
                    ind = indCol;
                }
                else
                {
                    indCol = GetIndCol(matrix[ind], way);
                    way.Add(indCol);
                    wayLenght += matrix[ind][indCol];
                    GetOutputString(way, matrix, wayLenght, indCol, matrix[ind][indCol], i);
                    ind = indCol;
                }
                i++;
            }

            way.Add(way[0]);
            wayLenght += matrix[way[^2]][way[^1]];

            Console.WriteLine($"\nИтоговый путь: {{ {string.Join(", ", way)} }} = {wayLenght}");
        }

        private static int GetIndCol(List<double> row, List<int> way)
        {
            int ind = 0;
            double minValue = Double.MaxValue;
            for (int i = 0; i < row.Count; i++) {
                if (row[i] != 0 && row[i] < minValue && !way.Contains(i))
                {
                    minValue = row[i];
                    ind = i;
                }
            }

            return ind;
        }

        private static void GetOutputString(List<int> way, List<List<double>> matrix, double wayLenght, int indCol, double value, int i) => Console.WriteLine(
            $"{i}) Выбрали город {indCol}; Расстояние до выбранного {value}; {{ {string.Join(", ", way)} }} = {wayLenght}"
            );
    }
}

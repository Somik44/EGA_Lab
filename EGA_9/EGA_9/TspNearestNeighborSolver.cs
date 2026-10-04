using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGA_9
{
    internal class TspNearestNeighborSolver
    {

        static public void TspTspNearestNeighbor(List<List<double>> matrix)
        {
            Random rnd = new Random();
            List<int> way = new List<int>();
            double wayLenght = 0;

            int ind = 0, indCol = 0, i = 0;


            ind = rnd.Next(0, matrix.Count());
            way.Add(ind);
            Console.WriteLine($"\nНачали в городе {ind};");

            while (way.Count() < matrix.Count())
            {
                indCol = TspRoulette.Roulette(way, matrix);
                way.Add(indCol);
                wayLenght += matrix[ind][indCol];
                GetOutputString(way, matrix, wayLenght, indCol, matrix[ind][indCol], i);
                ind = indCol;
                i++;
            }

            way.Add(way[0]);
            wayLenght += matrix[way[^2]][way[^1]];

            Console.WriteLine($"\nИтоговый путь: {{ {string.Join(", ", way)} }} = {wayLenght}");
        }

        private static void GetOutputString(List<int> way, List<List<double>> matrix, double wayLenght, int indCol, double value, int i) => Console.WriteLine(
            $"{i}) Выбрали город {indCol}; Расстояние до выбранного {value}; {{ {string.Join(", ", way)} }} = {wayLenght}"
            );
    }
}

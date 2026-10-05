using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGA_10
{
    internal class TspNearestNeighborSolver
    {

        static public List<int> Tsp(List<List<double>> matrix)
        {
            Random rnd = new Random();
            List<int> way = new List<int>();
            double wayLenght = 0;

            int ind = 0, indCol = 0, i = 0;


            ind = rnd.Next(0, matrix.Count());
            way.Add(ind);


            while (way.Count() < matrix.Count())
            {
                indCol = Roulette.TspRoulette(way, matrix);
                way.Add(indCol);
                wayLenght += matrix[ind][indCol];
                ind = indCol;
                i++;
            }

            return way;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGA_10
{
    internal class MatrixReader
    {
        public static List<List<double>> GetMatrix (string path)
        {
            List<List<double>> matrix = new List<List<double>>();

            foreach (string line in File.ReadLines(path))
            {
                List<double> row = new List<double>();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string part in parts)
                    {
                        row.Add(double.Parse(part, CultureInfo.InvariantCulture));
                    }
                    matrix.Add(row);
                }
            }

            return matrix;
        }

        public static void PrintMatrix(List<List<double>> matrix)
        {
            if (matrix.Count == 0) return;

            const int valueWidth = 7;
            int indexWidth = (matrix.Count - 1).ToString().Length + 1;

            Console.Write(new string(' ', indexWidth + 1));
            for (int j = 0; j < matrix.Count; j++) Console.Write($"{j})".PadLeft(valueWidth) + " ");
            Console.WriteLine();

            Console.WriteLine(new string('-', (indexWidth + 1) + (valueWidth + 1) * matrix.Count));

            for (int i = 0; i < matrix.Count; i++)
            {
                Console.Write($"{i})".PadLeft(indexWidth) + " ");
                foreach (double v in matrix[i])  Console.Write(v.ToString("F2", CultureInfo.InvariantCulture).PadLeft(valueWidth) + " ");
                Console.WriteLine();
            }
        }
    }
}

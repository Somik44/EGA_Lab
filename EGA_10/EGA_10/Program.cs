namespace EGA_10
{

    class Program()
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users\\Михаил\\Desktop\\ЭГА\\EGA_Lab\\TxtFiles\\Matrix.txt";

            List<List<double>> matrix = MatrixReader.GetMatrix(path);
            MatrixReader.PrintMatrix(matrix);
            int N, populationCount, lenghtStagnation;
            double mutation;

            Console.Write("Введите число популяций: ");
            N = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите размер популяции: ");
            populationCount = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите допустимое количество стагнаций популяции: ");
            lenghtStagnation = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите вероятность мутации ([0, 1]): ");
            mutation = Convert.ToDouble(Console.ReadLine());
            EGA.Algorithm(matrix, N, populationCount, lenghtStagnation, mutation);
        }
    }
}
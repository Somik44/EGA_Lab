namespace EGA_9 {

    class Program()
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users\\Михаил\\Desktop\\ЭГА\\EGA_Lab\\EGA_5\\EGA_5\\TxtFiles\\MatrixTest.txt";

            List<List<double>> matrix = MatrixReader.GetMatrix(path);
            MatrixReader.PrintMatrix(matrix);

            int value;
            while (true)
            {
                Console.Write("\nВыберете алгоритм (1 - ближайший сосед, 2 - ближайший город, 3 - выход): ");
                value = Convert.ToInt32(Console.ReadLine());
                switch (value) {
                    case 1:
                        TspNearestNeighborSolver.TspTspNearestNeighbor(matrix);
                        break;
                    case 2:
                        TspNearestCitySolver.TspNearestCity(matrix);
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        continue;
                }
            }


        }
    }
}
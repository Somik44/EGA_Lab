using EGA_6;

class Program()
{
    static void Main(string[] args)
    {
        string path = "C:\\Users\\Михаил\\Desktop\\ЭГА\\EGA_Lab\\TxtFiles\\MatrixTest.txt";

        List<List<double>> matrix = MatrixReader.GetMatrix(path);
        MatrixReader.PrintMatrix(matrix);

        TspSolver.TspNearestCity(matrix);
    }
}
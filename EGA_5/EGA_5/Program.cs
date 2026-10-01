using EGA_5;

class Program()
{
    static void Main(string[] args)
    {
        string path = "C:\\Users\\Михаил\\Desktop\\ЭГА\\EGA_Lab\\EGA_5\\EGA_5\\TxtFiles\\Matrix.txt";

        List<List<double>> matrix = MatrixReader.GetMatrix(path);
        MatrixReader.PrintMatrix(matrix);

        TspSolver.Tsp(matrix);
    }
}
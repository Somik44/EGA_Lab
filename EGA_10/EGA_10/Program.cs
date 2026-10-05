namespace EGA_10
{

    class Program()
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users\\Михаил\\Desktop\\учеба\\C#\\ЭГА\\EGA_Lab\\TxtFiles\\MatrixTest.txt";

            List<List<double>> matrix = MatrixReader.GetMatrix(path);
            MatrixReader.PrintMatrix(matrix);

            EGA.Algorithm(matrix);
        }
    }
}
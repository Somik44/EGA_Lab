namespace EGA_1
{
    class Program()
    {
        static void Main(string[] args)
        {
            Monte_Carlo carlo = new Monte_Carlo();
            const int L = 15, N = 32;

            Dictionary<string, int> map = new Dictionary<string, int>();
            carlo.GetMap(map, L);
            carlo.Algorithm(map, N);
        }
    }
}
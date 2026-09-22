using System.Globalization;
using EGA_2;

class Program()
{
    static void Main(string[] args)
    {
        HillСlimbing hill = new HillСlimbing();

        const int L = 5, N = 32;
        Dictionary<string, int> map = new Dictionary<string, int>();
        hill.CreateMap(map, L);
        hill.Climbing(map, N);
    }
}
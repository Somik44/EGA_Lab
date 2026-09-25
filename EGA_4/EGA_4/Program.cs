using EGA_4;

class Program
{
    static void Main(string[] args)
    {
        const int L = 7, N = 3, n=32;

        Dictionary<string, decimal> map = CreateMap.Create(L);
        MultipleLaunch.Method(map, N, n);

    }
}

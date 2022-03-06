using BenchmarkDotNet.Running;

namespace EvilStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmarks>();
        }
    }
}
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;

namespace EfektywnoscAlgorytmow
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<SortingAlgorithms>();
        }
    }
    
}
    




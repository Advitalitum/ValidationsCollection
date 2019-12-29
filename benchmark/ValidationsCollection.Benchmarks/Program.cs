using BenchmarkDotNet.Running;

namespace ValidationsCollection.Benchmarks
{
	public class Program
	{
		public static void Main(string[] args) => BenchmarkRunner.Run<InnValidationBenchmark>();
	}
}
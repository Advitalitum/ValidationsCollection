using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using ValidationsCollection.Snils;

namespace ValidationsCollection.Benchmarks
{
	[MemoryDiagnoser]
	[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
	public class SnilsValidationBenchmark
	{
		private readonly string _attemptSnilsValue;

		public SnilsValidationBenchmark()
		{
			_attemptSnilsValue = "08765430300";
		}

		[Benchmark]
		[BenchmarkCategory("IsValidSnils")]
		public bool IsValidSnils() => Validations.IsValidSnils(_attemptSnilsValue);

		[Benchmark(Baseline = true)]
		[BenchmarkCategory("IsValidSnils")]
		public bool IsValidSnilsOld() => OldSnilsValidations.IsValidSnils(_attemptSnilsValue);
	}
}
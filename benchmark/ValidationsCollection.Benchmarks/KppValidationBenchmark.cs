using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using ValidationsCollection.Kpp;

namespace ValidationsCollection.Benchmarks
{
	[MemoryDiagnoser]
	[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
	public class KppValidationBenchmark
	{
		private readonly string _attemptKppValue;

		public KppValidationBenchmark()
		{
			_attemptKppValue = "123123123";
		}

		[Benchmark]
		[BenchmarkCategory("IsValidKpp")]
		public bool IsValidKpp() => Validations.IsValidKpp(_attemptKppValue);

		[Benchmark(Baseline = true)]
		[BenchmarkCategory("IsValidKpp")]
		public bool IsValidKppOld() => OldKppValidations.IsValidKpp(_attemptKppValue);
	}
}
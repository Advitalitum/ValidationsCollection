using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using ValidationsCollection.Inn;

namespace ValidationsCollection.Benchmarks
{
	[MemoryDiagnoser]
	[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
	public class InnValidationBenchmark
	{
		private readonly string _attemptValueForEntity;

		private readonly string _attemptValueForIndividual;

		public InnValidationBenchmark()
		{
			_attemptValueForEntity = "7707083893";
			_attemptValueForIndividual = "132808730606";
		}

		[Benchmark]
		[BenchmarkCategory("IsValidInnForEntity")]
		public bool IsValidInnForEntity() => Validations.IsValidInn(_attemptValueForEntity);

		[Benchmark(Baseline = true)]
		[BenchmarkCategory("IsValidInnForEntity")]
		public bool IsValidInnOldForEntity() => OldInnValidations.IsValidInn(_attemptValueForEntity);

		[Benchmark]
		[BenchmarkCategory("IsValidInnForIndividual")]
		public bool IsValidInnForIndividual() => Validations.IsValidInn(_attemptValueForIndividual);

		[Benchmark(Baseline = true)]
		[BenchmarkCategory("IsValidInnForIndividual")]
		public bool IsValidInnOldForIndividual() => OldInnValidations.IsValidInn(_attemptValueForIndividual);
	}
}
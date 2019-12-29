using System;
using BenchmarkDotNet.Attributes;

namespace ValidationsCollection.Benchmarks
{
	public class InnValidationBenchmark
	{
		private readonly string _attemptValueForEntity;

		private readonly string _attemptValueForIndividual;

		public InnValidationBenchmark()
		{
			var random = new Random(42);

			_attemptValueForEntity = random.Next().ToString("0000000000");
			_attemptValueForIndividual = random.Next().ToString("000000000000");
		}

		[Benchmark]
		public bool IsValidInnForEntity() => Validations.IsValidInn(_attemptValueForEntity);

		[Benchmark]
		public bool IsValidInnOldForEntity() => Validations.IsValidInnOld(_attemptValueForEntity);

		[Benchmark]
		public bool IsValidInnForIndividual() => Validations.IsValidInn(_attemptValueForIndividual);

		[Benchmark]
		public bool IsValidInnOldForIndividual() => Validations.IsValidInnOld(_attemptValueForIndividual);
	}
}
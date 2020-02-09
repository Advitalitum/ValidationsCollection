using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using JetBrains.Annotations;
using ValidationsCollection.Inn;
using Xunit;

namespace ValidationsCollection.Tests
{
	public class InnValidationTests
	{
		[Theory]
		[MemberData(nameof(ValidInnForIndividualData))]
		[MemberData(nameof(ValidInnForEntityData))]
		public void IsInnValid_OnValidInnString_ReturnsTrue(string value) =>
			Validations.IsValidInn(value).Should().BeTrue();

		[Theory]
		[MemberData(nameof(InvalidData))]
		public void IsInnValid_OnInvalidInnString_ReturnsFalse([CanBeNull] string? value) =>
			Validations.IsValidInn(value).Should().BeFalse();

		[Theory]
		[MemberData(nameof(ValidInnForEntityData))]
		public void IsInnValidForEntity_OnValidInnString_ReturnsTrue(string value) =>
			Validations.IsValidInnForEntity(value).Should().BeTrue();

		[Theory]
		[MemberData(nameof(InvalidData))]
		[MemberData(nameof(ValidInnForIndividualData))]
		public void IsInnValidForEntity_OnInvalidInnString_ReturnsFalse([CanBeNull] string? value) =>
			Validations.IsValidInnForEntity(value).Should().BeFalse();

		[Theory]
		[MemberData(nameof(ValidInnForIndividualData))]
		public void IsInnValidForIndividual_OnValidInnString_ReturnsTrue(string value) =>
			Validations.IsValidInnForIndividual(value).Should().BeTrue();

		[Theory]
		[MemberData(nameof(InvalidData))]
		[MemberData(nameof(ValidInnForEntityData))]
		public void IsInnValidForIndividual_OnInvalidInnString_ReturnsFalse([CanBeNull] string? value) =>
			Validations.IsValidInnForIndividual(value).Should().BeFalse();

		[Theory]
		[MemberData(nameof(InvalidData))]
		public void ValidateInn_OnInvalidInnString_ReturnsCorrectValidationResult([CanBeNull] string? value)
		{
			var innValidationResult = Validations.ValidateInn(value);

			innValidationResult.IsValid.Should().BeFalse();
			innValidationResult.IsValidForEntity.Should().BeFalse();
			innValidationResult.IsValidForIndividual.Should().BeFalse();
			innValidationResult.InnType.Should().Be(InnType.Unknown);
		}

		[Theory]
		[MemberData(nameof(ValidInnForEntityData))]
		public void ValidateInn_OnValidForEntityInnString_ReturnsCorrectValidationResult([CanBeNull] string? value)
		{
			var innValidationResult = Validations.ValidateInn(value);

			innValidationResult.IsValid.Should().BeTrue();
			innValidationResult.IsValidForEntity.Should().BeTrue();
			innValidationResult.IsValidForIndividual.Should().BeFalse();
			innValidationResult.InnType.Should().Be(InnType.Entity);
		}

		[Theory]
		[MemberData(nameof(ValidInnForIndividualData))]
		public void ValidateInn_OnValidForIndividualInnString_ReturnsCorrectValidationResult([CanBeNull] string? value)
		{
			var innValidationResult = Validations.ValidateInn(value);

			innValidationResult.IsValid.Should().BeTrue();
			innValidationResult.IsValidForEntity.Should().BeFalse();
			innValidationResult.IsValidForIndividual.Should().BeTrue();
			innValidationResult.InnType.Should().Be(InnType.Individual);
		}

		public static IEnumerable<object?[]> InvalidData => InvalidStrings.Select(DataSelector);

		public static IEnumerable<object?[]> ValidInnForEntityData => ValidEntityInns.Select(DataSelector);

		public static IEnumerable<object?[]> ValidInnForIndividualData => ValidIndividualInns.Select(DataSelector);

		private static string[] ValidIndividualInns => new[]
		{
			"132808730606",
			"500100732259"
		};

		private static string[] ValidEntityInns => new[]
		{
			"7707083893",
			"7830002293"
		};

		private static string?[] InvalidStrings => new[]
		{
			"7707083893 ",
			"7830002294",
			"783¼002293",
			"132808730602",
			"1328087306¼6",
			"",
			"123",
			"Hello",
			null
		};

		private static object?[] DataSelector(string? str) => new object?[] { str };

	}
}
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace ValidationsCollection.Tests
{
	public class KppValidationTests
	{
		[Theory]
		[InlineData("123123123")]
		[InlineData("0000AZ000")]
		[InlineData("12313A123")]
		[InlineData("1231A3123")]
		[InlineData("12300A123")]
		[InlineData("1234A0023")]
		public void IsValidKpp_OnValidKppString_ReturnsTrue(string value) =>
			Validations.IsValidKpp(value).Should().BeTrue();

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("123")]
		[InlineData("hello")]
		[InlineData("123123 123")]
		[InlineData("123123123 ")]
		[InlineData("122AFF123")]
		[InlineData("12312312¼")]
		[InlineData("0000az000")]
		[InlineData("123100123")] // 00 is not valid reason for registration
		public void IsValidKpp_OnInvalidKppString_ReturnsFalse([CanBeNull] string? value) =>
			Validations.IsValidKpp(value).Should().BeFalse();
	}
}
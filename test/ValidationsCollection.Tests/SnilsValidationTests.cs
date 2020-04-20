using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace ValidationsCollection.Tests
{
	public class SnilsValidationTests
	{
		[Theory]
		[InlineData("00000000000")]
		[InlineData("08765430300")]
		[InlineData("64071744282")]
		[InlineData("00000000123")]
		// Numbers less than 001001998 are always valid
		[InlineData("00100199800")]
		[InlineData("00100199801")]
		[InlineData("00100199899")]
		[InlineData("00000199899")]
		[InlineData("83934935252")]
		[InlineData("97850148962")]
		[InlineData("06121848637")]
		[InlineData("00100199965")]
		[InlineData("08765430200")]
		public void IsValidKpp_OnValidSnilsString_ReturnsTrue(string value) =>
			Validations.IsValidSnils(value).Should().BeTrue();

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("123")]
		[InlineData("hello")]
		[InlineData("123123 123")]
		[InlineData("00100199900 ")]
		[InlineData("00100199900")]
		[InlineData("00100199901")]
		[InlineData("0010019990¼")]
		[InlineData("001001A9900")]
		[InlineData("11134534511")]
		[InlineData("57406481918")]
		[InlineData("66188235932")]
		public void IsValidKpp_OnInvalidSnilsString_ReturnsFalse([CanBeNull] string? value) =>
			Validations.IsValidSnils(value).Should().BeFalse();
	}
}
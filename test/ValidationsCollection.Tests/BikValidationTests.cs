using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace ValidationsCollection.Tests
{
	public class BikValidationTests
	{
		[Theory]
		[InlineData("123123123")]
		[InlineData("000000000")]
		[InlineData("221321321")]
		[InlineData("044525792")]
		[InlineData("044525000")]
		[InlineData("044525001")]
		[InlineData("044525002")]
		[InlineData("066666666")]
		public void IsValidBik_OnValidBikString_ReturnsTrue(string value) =>
			Validations.IsValidBik(value).Should().BeTrue();

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("123")]
		[InlineData("hello")]
		[InlineData("123123 123")]
		[InlineData("123123123 ")]
		[InlineData("321321321")]
		[InlineData("122AFF123")]
		[InlineData("12312312¼")]
		[InlineData("0000az000")]
		[InlineData("744525000")]
		public void IsValidBik_OnValidBikString_ReturnsFalse([CanBeNull] string? value) =>
			Validations.IsValidBik(value).Should().BeFalse();
	}
}
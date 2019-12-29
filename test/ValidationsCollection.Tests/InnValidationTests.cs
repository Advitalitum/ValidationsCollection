using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace ValidationsCollection.Tests
{
	public class InnValidationTests
	{
		[Theory]
		[InlineData("7707083893")]
		[InlineData("132808730606")]
		[InlineData("500100732259")]
		[InlineData("7830002293")]
		public void IsInnValid_OnValidInnString_ReturnsTrue(string value) =>
			Validations.IsValidInn(value).Should().BeTrue();

		[Theory]
		[InlineData("7707083893 ")]
		[InlineData("7830002294")]
		[InlineData("783¼002293")]
		[InlineData("132808730602")]
		[InlineData("1328087306¼6")]
		[InlineData("")]
		[InlineData("123")]
		[InlineData("Hello")]
		[InlineData((string?) null)]
		public void IsInnValid_OnInvalidInnString_ReturnsFalse([CanBeNull] string? value) =>
			Validations.IsValidInn(value).Should().BeFalse();
	}
}
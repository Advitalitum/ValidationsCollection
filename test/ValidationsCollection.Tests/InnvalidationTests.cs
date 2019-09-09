using FluentAssertions;
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
			ValidationsCollection.IsValidInn(value).Should().BeTrue();

		[Theory]
		[InlineData("7707083893 ")]
		[InlineData("783¼002293")]
		[InlineData("132808730602")]
		[InlineData("")]
		[InlineData("123")]
		[InlineData("Hello")]
		[InlineData((string) null)]
		public void IsInnValid_OnInValidInnString_ReturnsTrue(string value) =>
			ValidationsCollection.IsValidInn(value).Should().BeFalse();
	}
}
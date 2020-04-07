using System.Runtime.CompilerServices;

namespace ValidationsCollection
{
	internal static class ValidationsHelper
	{
		internal const byte NonDigitByte = 255;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static byte GetNumericValue(in char c) =>
			c switch
			{
				'0' => 0,
				'1' => 1,
				'2' => 2,
				'3' => 3,
				'4' => 4,
				'5' => 5,
				'6' => 6,
				'7' => 7,
				'8' => 8,
				'9' => 9,
				_ => NonDigitByte
			};
	}
}
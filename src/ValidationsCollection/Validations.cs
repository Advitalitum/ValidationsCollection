using System.Linq;
using JetBrains.Annotations;

namespace ValidationsCollection
{
	/// <summary>
	///     Collection of validations
	/// </summary>
	public static class Validations
	{
		private static readonly byte[] _n1ForEntitiesCoefficients = { 2, 4, 10, 3, 5, 9, 4, 6, 8 };

		private static readonly byte[] _n2ForIndividualsCoefficients =
			new byte[] { 7 }.Concat(_n1ForEntitiesCoefficients).ToArray();

		private static readonly byte[] _n1ForIndividualsCoefficients =
			new byte[] { 3 }.Concat(_n2ForIndividualsCoefficients).ToArray();

		/// <summary>
		///     Determines whether the specified inn string is valid.
		/// </summary>
		/// <param name = "innString"> The inn string. </param>
		/// <returns>
		///     <c> true </c> if is valid ; otherwise, <c> false </c>.
		/// </returns>
		/// <remarks> Valid inn string consists of 10 or 12 digits and has correct check digit </remarks>
		[Pure]
		[ContractAnnotation("null => false")]
		public static bool IsValidInn(string? innString)
		{
			if (innString is null || !innString.All(char.IsDigit))
			{
				return false;
			}

			var numbers = innString.Select(c => (byte) char.GetNumericValue(c)).ToArray();

			return numbers.Length == 10
				? CheckInnForEntities(numbers)
				: (numbers.Length == 12)
				&& CheckInnForIndividual(numbers);
		}

		private static bool CheckInnForIndividual(byte[] numbers) =>
			(CalculateCheckSum(numbers, _n1ForIndividualsCoefficients) == numbers[11])
			&& (CalculateCheckSum(numbers, _n2ForIndividualsCoefficients) == numbers[10]);

		private static bool CheckInnForEntities(byte[] numbers) =>
			CalculateCheckSum(numbers, _n1ForEntitiesCoefficients) == numbers[9];

		private static byte CalculateCheckSum(byte[] numbers, byte[] coefficients)
			=> (byte) (coefficients
					.Select((t, i) => numbers[i] * t)
					.Sum()
				% 11
				% 10);
	}
}
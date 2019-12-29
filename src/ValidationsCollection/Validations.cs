using System.Linq;
using JetBrains.Annotations;

namespace ValidationsCollection
{
	/// <summary>
	///     Collection of validations
	/// </summary>
	public static partial class Validations
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
		/// <remarks>
		///     Valid inn string consists of 10 or 12 digits and has correct check digit
		///     (https://ru.wikipedia.org/wiki/Контрольное_число#Номера_ИНН)
		/// </remarks>
		[Pure]
		[ContractAnnotation("null => false")]
		public static bool IsValidInn([CanBeNull] string? innString)
		{
			if (innString is null)
			{
				return false;
			}

			return innString.Length switch
			{
				10 => IsValidInnForEntity(innString),
				12 => IsValidInnForIndividual(innString),
				_ => false
			};
		}

		private static bool IsValidInnForIndividual(string innString)
		{
			var sum1 = 0;
			var sum2 = 0;

			for (var index = 0; index < 10; index++)
			{
				if (!char.IsDigit(innString[index]))
				{
					return false;
				}

				var value = GetNumericValue(innString[index]);

				sum1 += _n2ForIndividualsCoefficients[index] * value;
				sum2 += _n1ForIndividualsCoefficients[index] * value;
			}

			if (!char.IsDigit(innString[10]) || (sum1 % 11 % 10 != GetNumericValue(innString[10])))
			{
				return false;
			}

			sum2 += _n1ForIndividualsCoefficients[10] * GetNumericValue(innString[10]);

			return char.IsDigit(innString[11]) && (sum2 % 11 % 10 == GetNumericValue(innString[11]));
		}

		private static bool IsValidInnForEntity(string innString)
		{
			var sum = 0;

			for (var index = 0; index < innString.Length - 1; index++)
			{
				var current = innString[index];

				if (!char.IsDigit(current))
				{
					return false;
				}

				sum += _n1ForEntitiesCoefficients[index] * GetNumericValue(current);
			}

			return char.IsDigit(innString[9]) && (sum % 11 % 10 == GetNumericValue(innString[9]));
		}

		private static byte GetNumericValue(char c) => (byte) char.GetNumericValue(c);
	}
}
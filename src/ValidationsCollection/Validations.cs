using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace ValidationsCollection
{
	/// <summary>
	///     Collection of validations
	/// </summary>
	public static class Validations
	{
		private const int NonDigitByte = 255;

		private static readonly byte[] _n1ForEntitiesCoefficients = { 2, 4, 10, 3, 5, 9, 4, 6, 8 };

		private static readonly byte[] _n2ForIndividualsCoefficients = { 7, 2, 4, 10, 3, 5, 9, 4, 6, 8 };

		private static readonly byte[] _n1ForIndividualsCoefficients = { 3, 7, 2, 4, 10, 3, 5, 9, 4, 6, 8 };

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
		public static bool IsValidInn([CanBeNull] [NotNullWhen(true)] string? innString)
		{
			if (innString == null)
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsValidInnForIndividual(string innString)
		{
			var n2Sum = 0;
			var n1Sum = 0;
			
			for (var index = 0; index < 10; index++)
			{
				var currentNumericValue = GetNumericValue(innString[index]);

				if (currentNumericValue == NonDigitByte)
				{
					return false;
				}

				n2Sum += _n2ForIndividualsCoefficients[index] * currentNumericValue;
				n1Sum += _n1ForIndividualsCoefficients[index] * currentNumericValue;
			}

			var tenth = GetNumericValue(innString[10]);

			if ((tenth == NonDigitByte) || (n2Sum % 11 % 10 != tenth))
			{
				return false;
			}

			var eleventh = GetNumericValue(innString[11]);

			if (eleventh == NonDigitByte)
			{
				return false;
			}

			n1Sum += _n1ForIndividualsCoefficients[10] * tenth;

			return n1Sum % 11 % 10 == eleventh;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsValidInnForEntity(string innString)
		{
			var sum = 0;

			for (var index = 0; index < 9; index++)
			{
				var currentNumericValue = GetNumericValue(innString[index]);

				if (currentNumericValue == NonDigitByte)
				{
					return false;
				}

				sum += _n1ForEntitiesCoefficients[index] * currentNumericValue;
			}

			var ninth = GetNumericValue(innString[9]);

			return (ninth != NonDigitByte) && (sum % 11 % 10 == ninth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static byte GetNumericValue(in char c) =>
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
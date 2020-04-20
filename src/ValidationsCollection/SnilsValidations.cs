using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using static ValidationsCollection.ValidationsHelper;

namespace ValidationsCollection
{
	/// <summary>
	///     Collection of validations
	/// </summary>
	public static partial class Validations
	{
		private static readonly byte[] _minimalCheckableNumberDigits = { 0, 0, 1, 0, 0, 1, 9, 9, 8 };

		/// <summary>
		///     Determines whether the specified Snils string is valid
		/// </summary>
		/// <param name = "snilsString"> The Snils string </param>
		/// <returns>
		///     <c> true </c> if is valid ; otherwise, <c> false </c>.
		/// </returns>
		/// <remarks>
		///     Valid Snils string consists of 11 numbers
		///     (https://ru.wikipedia.org/wiki/Контрольное_число#Страховой_номер_индивидуального_лицевого_счёта_(Россия))
		/// </remarks>
		[Pure]
		[PublicAPI]
		[ContractAnnotation("null => false")]
		public static bool IsValidSnils([CanBeNull] [NotNullWhen(true)] string? snilsString)
		{
			if (snilsString is null || (snilsString.Length != 11))
			{
				return false;
			}

			var sum = 0;
			var noNeedToCheckSum = true;

			for (var index = 0; index < 9; index++)
			{
				var current = GetNumericValue(snilsString[index]);

				if (current == NonDigitByte)
				{
					return false;
				}

				noNeedToCheckSum &= current <= _minimalCheckableNumberDigits[index];

				sum += current * (9 - index);
			}

			if (noNeedToCheckSum)
			{
				return true;
			}

			var tenth = GetNumericValue(snilsString[9]);

			if (tenth == NonDigitByte)
			{
				return false;
			}

			var eleventh = GetNumericValue(snilsString[10]);

			if (eleventh == NonDigitByte)
			{
				return false;
			}

			var controlSum = tenth * 10 + eleventh;

			if (sum < 100)
			{
				return controlSum == sum;
			}

			if (sum == 100 || sum == 101)
			{
				return controlSum == 0;
			}

			sum %= 101;

			if (sum != 100)
			{
				return controlSum == sum;
			}

			return controlSum == 0;
		}
	}
}
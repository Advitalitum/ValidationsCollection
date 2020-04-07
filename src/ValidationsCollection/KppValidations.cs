using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using static ValidationsCollection.ValidationsHelper;

namespace ValidationsCollection
{
	public static partial class Validations
	{
		/// <summary>
		///     Determines whether the specified Kpp string is valid
		/// </summary>
		/// <param name = "kppString"> The Kpp string </param>
		/// <returns>
		///     <c> true </c> if is valid ; otherwise, <c> false </c>.
		/// </returns>
		/// <remarks>
		///     Valid Kpp string consists of 9 symbols, 1-4 are numbers, 5-6 may be number or upper case letter (except 00), 7-9 are digits
		///     (http://www.consultant.ru/document/cons_doc_LAW_134082/)
		/// </remarks>
		[Pure]
		[PublicAPI]
		[ContractAnnotation("null => false")]
		public static bool IsValidKpp([CanBeNull] [NotNullWhen(true)] string? kppString)
		{
			if (kppString is null || kppString.Length != 9)
			{
				return false;
			}

			for (var index = 0; index < 4; index++)
			{
				if (GetNumericValue(kppString[index]) == NonDigitByte)
				{
					return false;
				}
			}
			
			var fifthWasZero = false;
			for (var index = 4; index < 6; index++)
			{
				var symbol = kppString[index];
				var numericValue = GetNumericValue(symbol);
				if (!char.IsUpper(symbol) && (numericValue == NonDigitByte))
				{
					return false;
				}

				if (numericValue == 0)
				{
					if (fifthWasZero)
					{
						return false;
					}

					fifthWasZero = true;
				}
			}

			for (var index = 6; index < 9; index++)
			{
				if (GetNumericValue(kppString[index]) == NonDigitByte)
				{
					return false;
				}
			}

			return true;
		}
	}
}
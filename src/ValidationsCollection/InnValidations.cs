using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using ValidationsCollection.Inn;
using static ValidationsCollection.ValidationsHelper;

namespace ValidationsCollection
{
	/// <summary>
	///     Collection of validations
	/// </summary>
	public static partial class Validations
	{
		private static readonly byte[] _n1ForEntitiesCoefficients = { 2, 4, 10, 3, 5, 9, 4, 6, 8 };

		private static readonly byte[] _n2ForIndividualsCoefficients = { 7, 2, 4, 10, 3, 5, 9, 4, 6, 8 };

		private static readonly byte[] _n1ForIndividualsCoefficients = { 3, 7, 2, 4, 10, 3, 5, 9, 4, 6, 8 };

		/// <summary>
		///     Determines whether the specified Inn string is valid
		/// </summary>
		/// <param name = "innString"> The Inn string </param>
		/// <returns>
		///     <c> true </c> if is valid ; otherwise, <c> false </c>.
		/// </returns>
		/// <remarks>
		///     Valid Inn string consists of 10 or 12 digits and has correct check digit
		///     (https://ru.wikipedia.org/wiki/Контрольное_число#Номера_ИНН)
		/// </remarks>
		[Pure]
		[PublicAPI]
		[ContractAnnotation("null => false")]
		public static bool IsValidInn([CanBeNull] [NotNullWhen(true)] string? innString) =>
			ValidateInn(innString).IsValid;

		/// <summary>
		///     Determines whether the specified Inn string is valid for entity
		/// </summary>
		/// <param name = "innString"> The Inn string </param>
		/// <returns>
		///     <c> true </c> if is valid ; otherwise, <c> false </c>.
		/// </returns>
		/// <remarks>
		///     Valid Inn for entity string consists of 10 digits and has correct check digit
		///     (https://ru.wikipedia.org/wiki/Контрольное_число#Номера_ИНН)
		/// </remarks>
		[Pure]
		[PublicAPI]
		[ContractAnnotation("null => false")]
		public static bool IsValidInnForEntity([CanBeNull] [NotNullWhen(true)] string? innString) =>
			ValidateInn(innString).IsValidForEntity;

		/// <summary>
		///     Determines whether the specified Inn string is valid for individual
		/// </summary>
		/// <param name = "innString"> The Inn string </param>
		/// <returns>
		///     <c> true </c> if is valid ; otherwise, <c> false </c>.
		/// </returns>
		/// <remarks>
		///     Valid Inn for individual string consists of 12 digits and has correct check digit
		///     (https://ru.wikipedia.org/wiki/Контрольное_число#Номера_ИНН)
		/// </remarks>
		[Pure]
		[PublicAPI]
		[ContractAnnotation("null => false")]
		public static bool IsValidInnForIndividual([CanBeNull] [NotNullWhen(true)] string? innString) =>
			ValidateInn(innString).IsValidForIndividual;

		/// <summary>
		///     Validates the Inn string
		/// </summary>
		/// <param name = "innString"> The Inn string </param>
		/// <returns> Validation result </returns>
		[Pure]
		[PublicAPI]
		public static ref readonly InnValidationResult ValidateInn([CanBeNull] string? innString)
		{
			if (innString is null)
			{
				return ref InnValidationResult.Invalid;
			}

			switch (innString.Length)
			{
				case 10:
					return ref ValidateEntityInn(innString);
				case 12:
					return ref ValidateIndividualInn(innString);
				default:
					return ref InnValidationResult.Invalid;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ref readonly InnValidationResult ValidateIndividualInn(string innString)
		{
			var n2Sum = 0;
			var n1Sum = 0;

			for (var index = 0; index < 10; index++)
			{
				var currentNumericValue = GetNumericValue(innString[index]);

				if (currentNumericValue == NonDigitByte)
				{
					return ref InnValidationResult.Invalid;
				}

				n2Sum += _n2ForIndividualsCoefficients[index] * currentNumericValue;
				n1Sum += _n1ForIndividualsCoefficients[index] * currentNumericValue;
			}

			var tenth = GetNumericValue(innString[10]);

			if ((tenth == NonDigitByte) || (n2Sum % 11 % 10 != tenth))
			{
				return ref InnValidationResult.Invalid;
			}

			var eleventh = GetNumericValue(innString[11]);

			if (eleventh == NonDigitByte)
			{
				return ref InnValidationResult.Invalid;
			}

			n1Sum += _n1ForIndividualsCoefficients[10] * tenth;

			if (n1Sum % 11 % 10 == eleventh)
			{
				return ref InnValidationResult.ValidIndividual;
			}

			return ref InnValidationResult.Invalid;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ref readonly InnValidationResult ValidateEntityInn(string innString)
		{
			var sum = 0;

			for (var index = 0; index < 9; index++)
			{
				var currentNumericValue = GetNumericValue(innString[index]);

				if (currentNumericValue == NonDigitByte)
				{
					return ref InnValidationResult.Invalid;
				}

				sum += _n1ForEntitiesCoefficients[index] * currentNumericValue;
			}

			var ninth = GetNumericValue(innString[9]);

			if (ninth == NonDigitByte)
			{
				return ref InnValidationResult.Invalid;
			}

			if (sum % 11 % 10 == ninth)
			{
				return ref InnValidationResult.ValidEntity;
			}

			return ref InnValidationResult.Invalid;
		}
	}
}

using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using static ValidationsCollection.ValidationsHelper;

namespace ValidationsCollection
{
	public partial class Validations
	{
		/// <summary>
		///     Determines whether the specified Bik string is valid
		/// </summary>
		/// <param name = "bikString"> The Bik string </param>
		/// <returns>
		///     <c> true </c> if is valid ; otherwise, <c> false </c>.
		/// </returns>
		/// <remarks>
		///     Valid Bik string consists of 9 digits, where first digit equals "0", "1" or "2"
		///     (https://ru.wikipedia.org/wiki/Банковский_идентификационный_код)
		///     (http://www.consultant.ru/document/cons_doc_LAW_280683/24a8cab5291d2517c8fdaeee243a44901d717408/)
		/// </remarks>
		[Pure]
		[PublicAPI]
		[ContractAnnotation("null => false")]
		public static bool IsValidBik([CanBeNull] [NotNullWhen(true)] string? bikString)
		{
			if (bikString is null || (bikString.Length != 9))
			{
				return false;
			}

			var first = GetNumericValue(bikString[0]);

			if (first > 2)
			{
				return false;
			}

			for (var index = 1; index < 9; index++)
			{
				var current = GetNumericValue(bikString[index]);

				if (current == NonDigitByte)
				{
					return false;
				}
			}

			return true;
		}
	}
}
using System;
using JetBrains.Annotations;

namespace ValidationsCollection.Inn
{
	/// <summary>
	///     Result of Inn string validation
	/// </summary>
	[PublicAPI]
	public readonly struct InnValidationResult : IEquatable<InnValidationResult>
	{
		private static readonly InnValidationResult _invalid = new InnValidationResult(InnType.Unknown);

		private static readonly InnValidationResult _validEntity = new InnValidationResult(InnType.Entity);

		private static readonly InnValidationResult _validIndividual = new InnValidationResult(InnType.Individual);

		/// <summary>
		///     The Inn type
		/// </summary>
		[PublicAPI]
		public readonly InnType InnType;

		/// <summary>
		///     Is Inn valid
		/// </summary>
		[PublicAPI]
		public readonly bool IsValid;

		/// <summary>
		///     Is Inn valid for entity
		/// </summary>
		[PublicAPI]
		public readonly bool IsValidForEntity;

		/// <summary>
		///     Is Inn valid for individual
		/// </summary>
		[PublicAPI]
		public readonly bool IsValidForIndividual;

		internal static ref readonly InnValidationResult Invalid => ref _invalid;

		internal static ref readonly InnValidationResult ValidEntity => ref _validEntity;

		internal static ref readonly InnValidationResult ValidIndividual => ref _validIndividual;

		private InnValidationResult(InnType innType)
		{
			InnType = innType;
			IsValid = InnType != InnType.Unknown;
			IsValidForEntity = InnType == InnType.Entity;
			IsValidForIndividual = InnType == InnType.Individual;
		}

		/// <inheritdoc />
		public bool Equals(InnValidationResult other) => Equals(in other);

		/// <inheritdoc />
		[Pure]
		[PublicAPI]
		public override bool Equals(object obj) => obj is InnValidationResult other && Equals(other);

		/// <inheritdoc cref = "Equals(object)" />
		[Pure]
		[PublicAPI]
		public bool Equals(in InnValidationResult other) => InnType == other.InnType;

		/// <inheritdoc />
		[Pure]
		[PublicAPI]
		public override int GetHashCode() => (int) InnType;

		/// <summary>
		///     Implements the operator ==.
		/// </summary>
		/// <param name = "left"> The left. </param>
		/// <param name = "right"> The right. </param>
		/// <returns>
		///     The result of the operator.
		/// </returns>
		public static bool operator ==(InnValidationResult left, InnValidationResult right) => left.Equals(right);

		/// <summary>
		///     Implements the operator !=.
		/// </summary>
		/// <param name = "left"> The left. </param>
		/// <param name = "right"> The right. </param>
		/// <returns>
		///     The result of the operator.
		/// </returns>
		public static bool operator !=(InnValidationResult left, InnValidationResult right) => !left.Equals(right);
	}
}
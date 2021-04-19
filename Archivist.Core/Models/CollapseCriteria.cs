using System;
using System.Collections.Generic;
using System.Text;

namespace Archivist.Core.Models
{

	/// <summary>
	/// The option to 'collapse' results based on a field, or a substring of a field. Collapsing is done on adjacent 
	/// cdx lines where all captures after the first one that are duplicate are filtered out. This is useful for filtering 
	/// out captures that are 'too dense' or when looking for unique captures.
	/// </summary>
	public class CollapseCriteria
	{

		#region PUBLIC ACCESSORS


		/// <summary>
		/// Gets or sets the field to collapse on.
		/// </summary>
		public Field Field { get; set; }

		/// <summary>
		/// Gets or sets the length of characters starting at 1 to filter up until.
		/// </summary>
		/// <example>
		/// If the field is <code>timestamp</code> and the length is <code>4</code>, the 
		/// filter will only match year.
		/// </example>
		public Int32? Length { get; set; }


		#endregion PUBLIC ACCESSORS

		#region PUBLIC METHODS


		/// <summary>
		/// Converts to string.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		public override String ToString()
		{
			var field = Field.ToString().ToLower();
			return Length.HasValue
				? $"{field}:{Length.Value}"
				: field;
		}


		#endregion PUBLIC METHODS

	}

}

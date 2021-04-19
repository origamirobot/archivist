using System;

namespace Archivist.Core.Models
{

	/// <summary>
	/// Represents a clause that can be applied to a field to limit the results returned.
	/// </summary>
	public class FieldFilter
	{

		#region PUBLIC ACCESSORS


		/// <summary>
		/// Gets or sets a value indicating whether this filter should be inverted.
		/// <c>true</c> inverts the conditional which will return all results not 
		/// matching this filter.
		/// </summary>
		public Boolean Invert { get; set; }

		/// <summary>
		/// Gets or sets the field to filter on.
		/// </summary>
		public Field Field { get; set; }

		/// <summary>
		/// Gets or sets the filter to use. This can be any value
		/// and also any Java compliant regular expression.
		/// </summary>
		/// <see href="http://docs.oracle.com/javase/6/docs/api/java/util/regex/Pattern.html"/>
		public String Filter { get; set; }


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
			return Invert
				? $"!{field}:{Filter}"
				: $"{field}:{Filter}";
		}


		#endregion PUBLIC METHODS

	}

}

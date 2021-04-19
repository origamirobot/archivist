using System;
using System.Collections.Generic;
using System.Text;

namespace Archivist.Core.Models
{

	/// <summary>
	/// Represents a single record returned from the Wayback archive.
	/// </summary>
	public class Capture
	{

		/// <summary>
		/// Gets or sets the type of the MIME.
		/// </summary>
		public String MimeType { get; set; }

		/// <summary>
		/// Gets or sets the status code.
		/// </summary>
		public Int32 StatusCode { get; set; }

		/// <summary>
		/// Gets or sets the original.
		/// </summary>
		public String Original { get; set; }

		/// <summary>
		/// Gets or sets the digest.
		/// </summary>
		public String Digest { get; set; }

		/// <summary>
		/// Gets or sets the redirect.
		/// </summary>
		public String Redirect { get; set; }

		/// <summary>
		/// Gets or sets the name of the file.
		/// </summary>
		public String FileName { get; set; }

		/// <summary>
		/// Gets or sets the length.
		/// </summary>
		public Int64 Length { get; set; }

		/// <summary>
		/// Gets or sets the offset.
		/// </summary>
		public Int64 Offset { get; set; }

		/// <summary>
		/// Gets or sets the timestamp.
		/// </summary>
		public DateTime Timestamp { get; set; }

		/// <summary>
		/// Gets or sets the robot flags.
		/// </summary>
		public String RobotFlags { get; set; }

		/// <summary>
		/// Gets or sets the URL key.
		/// </summary>
		public String UrlKey { get; set; }

		/// <summary>
		/// Gets or sets the number of duplicates that were found in the result set
		/// for this particular capture. This value will only be populated if the 
		/// <see cref="SearchOptions.ShowDupeCount"/> field is set to <c>true</c>.
		/// </summary>
		public Int32? DupeCount { get; set; }

		/// <summary>
		/// Gets or sets the skip count.
		/// </summary>
		public Int32? SkipCount { get; set; }

		/// <summary>
		/// Gets or sets the operation.
		/// </summary>
		public String Operation { get; set; }

	}

}

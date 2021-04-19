using System;
using System.Collections.Generic;
using System.Text;

namespace Archivist.Core.Models
{

	/// <summary>
	/// The types of fields available for the Wayback Machine.
	/// </summary>
	public enum Field
	{
		/// <summary>
		/// The broken down URL in key format.
		/// </summary>
		UrlKey,
		/// <summary>
		/// The timestamp of the capture.
		/// </summary>
		Timestamp,
		/// <summary>
		/// The original URL of the capture.
		/// </summary>
		Original,
		/// <summary>
		/// The MIME type of the capture.
		/// </summary>
		MimeType,
		/// <summary>
		/// The status code received when the capture was made.
		/// </summary>
		StatusCode,
		/// <summary>
		/// The unique digest for this capture.
		/// </summary>
		Digest,
		/// <summary>
		/// The URL of any redirect that happened.
		/// </summary>
		Redirect,
		/// <summary>
		/// The robot flags
		/// </summary>
		RobotFlags,
		/// <summary>
		/// The length of the content captured.
		/// </summary>
		Length,
		/// <summary>
		/// The current record index offset.
		/// </summary>
		Offset,
		/// <summary>
		/// The file name
		/// </summary>
		FileName,
		/// <summary>
		/// The amount of duplicated that were counted for this capture.
		/// </summary>
		DupeCount,
		/// <summary>
		/// The amount of records that were skipped for this capture.
		/// </summary>
		SkipCount,
	}

}

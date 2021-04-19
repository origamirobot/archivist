using System;
using System.Collections.Generic;
using System.Text;

namespace Archivist.Core.Models
{

	/// <summary>
	/// The types of output the Wayback machine can return.
	/// </summary>
	public enum OutputFormat
	{
		/// <summary>
		/// A JSON array of results.
		/// </summary>
		JSON,
		/// <summary>
		/// The default response. Returns results in plain 
		/// text one per line separated by spaces.
		/// </summary>
		CDX,
	}

}

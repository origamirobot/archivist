using System;
using System.Collections.Generic;
using System.Text;

namespace Archivist.Core.Models
{

	/// <summary>
	/// The default behavior is to return matches for an exact url. However, the cdx server can also return results 
	/// matching a certain prefix, a certain host or all subdomains by using the matchType= param.
	/// </summary>
	/// <remarks>
	/// The matchType may also be set implicitly by using wildcard '*' at end or beginning of the url:
	/// <para>If url is ends in '/*', eg url = archive.org/* the query is equivalent to url=archive.org/&matchType=prefix</para>
	/// <para>if url starts with '*.', eg url=*.archive.org/ the query is equivalent to url=archive.org/&matchType=domain</para>
	/// </remarks>
	public enum MatchScope
	{
		/// <summary>
		/// This option omits the the matchType query parameter from the request.
		/// </summary>
		Default,
		/// <summary>
		/// Exact (default if omitted) will return results matching exactly archive.org/about/
		/// </summary>
		Exact,
		/// <summary>
		/// Prefix will return results for all results under the path archive.org/about/
		/// </summary>
		Prefix,
		/// <summary>
		/// Host will return results from host archive.org
		/// </summary>
		Host,
		/// <summary>
		/// Domain will return results from host archive.org and all subhosts *.archive.org
		/// </summary>
		Domain

	}

}

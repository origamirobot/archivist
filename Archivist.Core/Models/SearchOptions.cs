using System;
using System.Collections.Generic;

namespace Archivist.Core.Models
{

	/// <summary>
	/// Options that define how the Wayback search should be performed.
	/// </summary>
	/// <see href="https://github.com/internetarchive/wayback/tree/master/wayback-cdx-server#intro-and-usage"/>
	public class SearchOptions
	{

		/// <summary>
		/// Gets or sets a value indicating whether to return the results in GZip format.
		/// </summary>
		public Boolean DisableGZip { get; set; }

		/// <summary>
		/// Gets or sets the fields to include in the search results. If omitted all the available fields are returned by default.
		/// </summary>
		public List<String> Fields { get; set; }

		/// <summary>
		/// Gets or sets the format of the output to return.
		/// </summary>
		public OutputFormat Format { get; set; }

		/// <summary>
		/// Gets or sets the fields to collapse. This filters out adjacent results that are considered
		/// duplicated based on the items in this property.
		/// </summary>
		/// <remarks>
		/// The option to 'collapse' results based on a field, or a substring of a field. Collapsing is done on adjacent 
		/// cdx lines where all captures after the first one that are duplicate are filtered out. This is useful for filtering 
		/// out captures that are 'too dense' or when looking for unique captures.
		/// </remarks>
		public List<CollapseCriteria> Collapse { get; set; }

		/// <summary>
		/// Gets or sets the limit of results to return.
		/// </summary>
		/// <remarks>
		/// <para>The CDX server config provides a setting for absolute maximum length returned from a single query (currently set to 150000 by default).</para>
		/// <para>Set limit= N to return the first N results.</para>
		/// <para>Set limit= -N to return the last N results. The query may be slow as it begins reading from the beginning of the search space and skips all but last N results.</para>
		/// </remarks>
		public Int32? Limit { get; set; }

		/// <summary>
		/// Gets or sets the scope to match for the specified url.
		/// </summary>
		public MatchScope Scope { get; set; }

		/// <summary>
		/// Gets or sets the number of records to skip in the begining of the result set.
		/// </summary>
		/// <remarks>
		///	Offset doesn't scale well for large queries since the server needs to read through all the skipped
		///	results before returning the specified ones.
		/// </remarks>
		public Int32? Offset { get; set; }

		/// <summary>
		/// Gets or sets the resumption key to use only if you're attempting to resume returning results from a previous query.
		/// </summary>
		public String ResumptionKey { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to return the resumption key at the end of 
		/// the results if more results exist that haven't yet been returned.
		/// </summary>
		public Boolean ShowResumptionKey { get; set; }

		/// <summary>
		/// Gets or sets a value that specifies returning the latest first without having to
		/// go through each record.
		/// </summary>
		public Boolean FastLatest { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to track the number of duplicate records for each capture.
		/// </summary>
		public Boolean ShowDupeCount { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to return the number of records that were skipped
		/// due to filtering and collapsing.
		/// </summary>
		public Boolean ShowSkipCount { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to set the timestamp of each result to that of the
		/// latest record that was skipped for this result.
		/// </summary>
		public Boolean LastSkipTimestamp { get; set; }

		/// <summary>
		/// Gets or sets the filters to use to limit the results returned.
		/// </summary>
		public List<FieldFilter> Filters { get; set; }

		/// <summary>
		/// Gets or sets the date to which to include results.
		/// </summary>
		public DateTime? To { get; set; }

		/// <summary>
		/// Gets or sets the date from which to include results.
		/// </summary>
		public DateTime? From { get; set; }

	}

}

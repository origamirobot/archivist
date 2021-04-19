using Archivist.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Archivist.Core.Services
{

	/// <summary>
	/// Represents a client class for the Wayback Machine service.
	/// </summary>
	public interface IWaybackClient
	{

		/// <summary>
		/// Searches the Wayback Machine for the specified URL using the options provided.
		/// </summary>
		/// <param name="url">The URL to search the Wayback Machine for.</param>
		/// <param name="options">The options that help limit and dictate how the search should be performed.</param>
		/// <returns></returns>
		Task<List<Capture>> SearchAsync(String target, SearchOptions options, CancellationToken cancellationToken = default);

	}

}

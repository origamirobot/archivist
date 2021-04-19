using Archivist.Core.Models;
using Archivist.Core.Utilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Archivist.Core.Services
{

	/// <summary>
	/// Represents a client class for the Wayback Machine service.
	/// </summary>
	public class WaybackClient : IWaybackClient
	{

		#region CONSTANTS


		private const String SearchUrl = "http://web.archive.org/cdx/search/cdx";


		#endregion CONSTANTS

		#region PROTECTED PROPERTIES


		/// <summary>
		/// Gets the HTTP client.
		/// </summary>
		protected HttpClient HttpClient { get; private set; }

		/// <summary>
		/// Gets the logger.
		/// </summary>
		protected ILogger<WaybackClient> Logger { get; private set; }

		/// <summary>
		/// Gets the HTTP utility.
		/// </summary>
		protected IHttpUtility HttpUtility { get; private set; }

		/// <summary>
		/// Gets the json serializer.
		/// </summary>
		protected IJsonSerializer JsonSerializer { get; private set; }


		#endregion PROTECTED PROPERTIES

		#region CONSTRUCTORS


		/// <summary>
		/// Initializes a new instance of the <see cref="WaybackClient" /> class.
		/// </summary>
		/// <param name="settings">The settings.</param>
		/// <param name="httpClient">The HTTP client.</param>
		/// <param name="logger">The logger.</param>
		/// <param name="httpUtility">The HTTP utility.</param>
		/// <param name="jsonSerializer">The json serializer.</param>
		public WaybackClient(
			HttpClient httpClient,
			ILogger<WaybackClient> logger,
			IHttpUtility httpUtility, 
			IJsonSerializer jsonSerializer)
		{
			HttpClient = httpClient;
			Logger = logger;
			HttpUtility = httpUtility;
			JsonSerializer = jsonSerializer;
		}


		#endregion CONSTRUCTORS

		#region PROTECTED METHODS


		/// <summary>
		/// Takes the specified <see cref="SearchOptions"/> and generates the correct query
		/// string to use when calling the Wayback Machine service.
		/// </summary>
		/// <param name="options">The options to generate a query string from.</param>
		/// <returns></returns>
		protected String GenerateQueryString(SearchOptions options)
		{
			var qs = new List<String>();

			if (options.Limit.HasValue)
				qs.Add($"limit={options.Limit.Value}");

			if (options.FastLatest)
				qs.Add($"fastLatest=true");

			if (options.ShowDupeCount)
				qs.Add("showDupeCount=true");

			if (options.ShowSkipCount)
				qs.Add("showSkipCount=true");

			if (options.LastSkipTimestamp)
				qs.Add("lastSkipTimestamp=true");

			if (options.Offset.HasValue)
				qs.Add($"offset={options.Offset.Value}");

			if(options.Scope != MatchScope.Default)
				qs.Add($"matchType={options.Scope.ToString().ToLower()}");

			if (options.Format == OutputFormat.JSON)
				qs.Add("output=json");

			if (options.DisableGZip)
				qs.Add("gzip=false");

			if (options.Fields != null && options.Fields.Count > 0)
				qs.Add($"fl={String.Join(',', options.Fields)}");

			if (options.From.HasValue)
				qs.Add($"from={options.From.Value.ToString("yyyyMMddhhmmss")}");

			if (options.To.HasValue)
				qs.Add($"to={options.To.Value.ToString("yyyyMMddhhmmss")}");

			if(options.Filters != null && options.Filters.Count > 0)
			{
				foreach (var filter in options.Filters)
					qs.Add($"filter={filter}");
			}

			if(options.Collapse != null && options.Collapse.Count > 0)
			{
				foreach(var item in options.Collapse)
					qs.Add($"collapse={item}");
			}

			if (options.ShowResumptionKey)
				qs.Add("showResumeKey=true");

			if (!String.IsNullOrEmpty(options.ResumptionKey))
				qs.Add($"resumeKey={options.ResumptionKey}");



			return String.Join('&', qs);
		}

		/// <summary>
		/// Parses the specified string returned from the Wayback Machine into a <see cref="DateTime"/>.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		protected DateTime ParseDateTime(String text)
		{
			var year = Int32.Parse(text.Substring(0, 4));
			var month = Int32.Parse(text.Substring(4, 2));
			var day = Int32.Parse(text.Substring(6, 2));
			var hour = Int32.Parse(text.Substring(8, 2));
			var minute = Int32.Parse(text.Substring(10, 2));
			var second = Int32.Parse(text.Substring(12, 2));
			return new DateTime(year, month, day, hour, minute, second);
		}

		/// <summary>
		/// Assigns the property to the specified <see cref="Capture"/> object.
		/// </summary>
		/// <param name="capture">The capture to assign to.</param>
		/// <param name="key">The name of the property being assigned.</param>
		/// <param name="value">The value of the property being assinged.</param>
		protected void AssignProperty(Capture capture, String key, String value)
		{
			try
			{
				switch (key)
				{
					case "urlkey": capture.UrlKey = value; break;
					case "timestamp": capture.Timestamp = ParseDateTime(value); break;
					case "original": capture.Original = value; break;
					case "mimetype": capture.MimeType = value; break;
					case "statuscode":
						Int32.TryParse(value, out var status);
						capture.StatusCode = status;
						break;
					case "digest": capture.Digest = value; break;
					case "redirect": capture.Redirect = value; break;
					case "robotflags": capture.RobotFlags = value; break;
					case "length": capture.Length = Int64.Parse(value); break;
					case "offset": capture.Offset = Int64.Parse(value); break;
					case "filename": capture.FileName = value; break;
					case "dupecount ": capture.DupeCount = Int32.Parse(value); break;
					case "skipcount": capture.SkipCount = Int32.Parse(value); break;
					default: Logger.LogWarning($"Unknown field returned with the result set: {key}"); break;
				}
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Parses the json result.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="resumptionKey">The resumption key.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentOutOfRangeException">An error occurred while parsing the response data from the Wayback Machine</exception>
		protected List<Capture> ParseJsonResult(String text, Boolean checkForResumption, out String resumptionKey)
		{
			resumptionKey = String.Empty;
			var result = new List<Capture>();
			var json = JsonSerializer.Deserialize<List<String[]>>(text);

			if (json == null || json.Count < 2)
				return result;

			// THE FIRST ROW RETURNED IS ALWAYS THE COLUMNS PROVIDED IN THE RESULT SET
			var keys = json[0];

			// START ON THE SECOND ROW AND PARSE ALL OF THE VALUES INTO Capture OBJECTS.
			for (var i = 1; i < json.Count; i++)
			{

				// EMPTY ARRAYS CAN HAPPEN IF REQUESTING A RESUMPTION KEY. JUST THROW IT AWAY FOR NOW
				if (json[i].Length == 0)
					continue;

				// CHECK IF THE ARRAY IS AT THE END AND WE'RE EXPECTING A RESUMPTION
				if(json[i].Length == 1 && checkForResumption && (i + 1) == json.Count)
				{
					resumptionKey = json[i][0];
					break;
				}

				var item = new Capture();

				// LOOP EACH KEY SO WE KNOW WHAT IS INSIDE EACH ITEM ARRAY
				for (var j = 0; j < keys.Length; j++)
				{
					var key = keys[j];
					var value = json[i][j];
					AssignProperty(item, key, value);
				}
				item.Operation = "Waiting";
				result.Add(item);
			}
			return result;
		}

		/// <summary>
		/// Parses the CDX result and returns a list of <see cref="Capture"/> objects.
		/// </summary>
		/// <param name="text">The text to parse.</param>
		/// <returns></returns>
		protected List<Capture> ParseCdxResult(String text, Boolean checkForResumption, out String resumptionKey)
		{
			resumptionKey = String.Empty;
			var result = new List<Capture>();
			var lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);

			for(var i = 0; i < lines.Length; i++)
			{
				var line = lines[i];
				var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

				if(checkForResumption && parts.Length == 1 && (i + 1) == lines.Length)
				{
					resumptionKey = parts[0];
					break;
				}

				var capture = new Capture();
				capture.UrlKey = parts[0];
				capture.Timestamp = ParseDateTime(parts[1]);
				capture.Original = parts[2];
				capture.MimeType = parts[3];
				capture.StatusCode = Int32.Parse(parts[4]);
				capture.Digest = parts[5];
				capture.Length = Int32.Parse(parts[6]);

				if (parts.Length > 7)
					capture.DupeCount = Int32.Parse(parts[7]);

				result.Add(capture);
			}

			return result;
		}


		#endregion PROTECTED METHODS

		#region PUBLIC METHODS


		/// <summary>
		/// Searches the Wayback Machine for the specified URL using the options provided.
		/// </summary>
		/// <param name="url">The URL to search the Wayback Machine for.</param>
		/// <param name="options">The options that help limit and dictate how the search should be performed.</param>
		/// <returns></returns>
		public async Task<List<Capture>> SearchAsync(String target, SearchOptions options, CancellationToken cancellationToken = default)
		{
			if (options == null)
				throw new ArgumentNullException(nameof(options), "Options parameter cannot be null");

			List<Capture> result = null;
			var stopwatch = new Stopwatch();
			String url = null;
			try
			{
				stopwatch.Start();
				var qs = GenerateQueryString(options);
				target = HttpUtility.UrlEncode(target);
				url = $"{SearchUrl}?url={target}&{qs}";
				Logger.LogInformation($"Sending Wayback Machine request to {url}");
				var response = await HttpClient.GetAsync(url, cancellationToken);
				var text = await response.Content.ReadAsStringAsync();
				Logger.LogDebug($"Parsing response received from Wayback Machine");
				switch(options.Format)
				{
					case OutputFormat.JSON:
						result = ParseJsonResult(text, options.ShowResumptionKey, out var jsonResumptionKey);
						options.ResumptionKey = jsonResumptionKey;
						break;
					case OutputFormat.CDX:
						result = ParseCdxResult(text, options.ShowResumptionKey, out var cdxResumptionKey);
						options.ResumptionKey = cdxResumptionKey;
						break;
				}
				Logger.LogInformation($"Received {result.Count} results from the Wayback Machine request");
				return result;
			}
			catch (Exception ex)
			{
				Logger.LogError("An error occurred while searching the Wayback Machine", ex);
				throw;
			}
			finally
			{
				stopwatch.Stop();
				Logger.LogDebug($"GET request to {url} finished in {stopwatch.ElapsedMilliseconds}ms");
			}
		}


		#endregion PUBLIC METHODS

	}

}

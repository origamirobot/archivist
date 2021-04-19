using Archivist.Core.Extensions;
using Archivist.Core.IO;
using Archivist.Core.Models;
using Archivist.Core.Services;
using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.Rendering;
using System.CommandLine.Rendering.Views;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Archivist
{

	/// <summary>
	/// Entry point into the Archivist application.
	/// </summary>
	public class Program
	{

		static IConsole Console { get; set; }
		static ScreenView Screen { get; set; }
		static ConsoleRenderer Renderer { get; set; }
		static Region Region { get; set; }
		static IIoUtility IoUtility { get; set; }
		static HttpClient HttpClient { get; set; }
		static IWaybackClient WaybackClient { get; set; }
		const Int32 PageSize = 30;


		/// <summary>
		/// Entry point into the Archivist application.
		/// </summary>
		/// <param name="args">The arguments passed in from the command line.</param>
		static Int32 Main(string[] args)
		{
			Bootstrapper.Bootstrap();
			IoUtility = ServiceLocator.Current.GetInstance<IIoUtility>();
			HttpClient = ServiceLocator.Current.GetInstance<HttpClient>();
			WaybackClient = ServiceLocator.Current.GetInstance<IWaybackClient>();


			var rootCmd = new RootCommand()
			{
				new Option<String>("--target", description: "The target your would like to query the Internet Archive for"),
				new Option<String>("--dest", description: "The destination where you would like to save files to", getDefaultValue: () => ".\\" ),
				new Option<String>("--filetype", description: "The mime type or regular expression to filter for", getDefaultValue:() => "(image/(gif|p?jpeg|(x-)?png)|application/x-shockwave-flash|application/pdf)")
			};
			rootCmd.Description = "Archivist";
			rootCmd.Handler = CommandHandler.Create<InvocationContext, String, String, String>(MainAsync);
			return rootCmd.InvokeAsync(args).Result;
		}

		/// <summary>
		/// Entry point into the Archivist application.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="target">The target.</param>
		/// <param name="dest">The dest.</param>
		/// <param name="filetype">The filetype.</param>
		static async Task MainAsync(InvocationContext context, String target, String dest, String filetype)
		{
			Console = context.Console;
			Renderer = new ConsoleRenderer(context.Console, mode: OutputMode.Ansi, resetAfterRender: true);
			Region = new Region(0, 0, System.Console.WindowWidth, System.Console.WindowHeight, true);
			
			var criteria = new SearchOptions()
			{
				Limit = 9999999,
				Format = OutputFormat.JSON,
				ShowResumptionKey = true,
				Filters = new List<FieldFilter>() { new FieldFilter(){ Field = Field.MimeType, Filter = filetype }, },
				Scope = MatchScope.Domain,
				Collapse = new List<CollapseCriteria>() { new CollapseCriteria() { Field = Field.FileName } }
			};

			var results = await WaybackClient.SearchAsync(target, criteria);

			if (results.Count == 0)
			{
				System.Console.WriteLine($"No results were returned for {target.LightGreen()}");
				return;
			}

			var totalPages = ((results.Count - 1) / PageSize) + 1;

			var destFolder = IoUtility.Path.Combine(dest, target);
			if (!IoUtility.Directory.Exists(destFolder))
				IoUtility.Directory.CreateDirectory(destFolder);


			for(var pageIndex = 0; pageIndex < totalPages; pageIndex++)
			{
				var page = results.Skip(pageIndex * PageSize).Take(PageSize).ToList();
				UpdateUi(page);

				foreach (var item in page)
				{
					try
					{
						item.Operation = "Downloading";
						UpdateUi(page);

						var fileName = IoUtility.Path.EncodeFileName(item.Original.Split('/').LastOrDefault());
						var path = IoUtility.Path.Combine(destFolder, $"{item.Timestamp.ToString("yyyyMMddhhmmss")}_{fileName}");

						if (IoUtility.File.Exists(path))
						{
							item.Operation = "Exists";
							UpdateUi(page);
							continue;
						}

						var waybackUrl = $"https://web.archive.org/web/{item.Timestamp.ToString("yyyyMMddhhmmss")}if_/{item.Original}";
						var response = await HttpClient.GetAsync(waybackUrl);
						if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
						{
							item.Operation = "Saving";
							UpdateUi(page);
							var data = await response.Content.ReadAsByteArrayAsync();
							IoUtility.File.WriteAllBytes(path, data);
							item.Operation = "Done";
							UpdateUi(page);
						}
						else
						{
							item.Operation = "Failed";
							UpdateUi(page);
							continue;
						}

						Thread.Sleep(1000);
					}
					catch (Exception)
					{
						item.Operation = "Error";
						UpdateUi(page);
					}
				}
			}












			System.Console.ReadLine();
		}


		static void UpdateUi(List<Capture> items)
		{
			var captureView = new CaptureView(items);
			Screen = new ScreenView(Renderer, Console) { Child = captureView };
			Screen.Render(Region);
		}

	}

}

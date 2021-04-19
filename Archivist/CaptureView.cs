using Archivist.Core.Extensions;
using Archivist.Core.Models;
using System;
using System.Collections.Generic;
using System.CommandLine.Rendering.Views;
using System.Text;

namespace Archivist
{
	internal class CaptureView : StackLayoutView
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="CaptureView"/> class.
		/// </summary>
		public CaptureView(List<Capture> captures)
		{
			Add(new ContentView(""));
			Add(new ContentView("Captures"));
			Add(new ContentView(""));

			var table = new TableView<Capture>() { Items = captures };
			table.AddColumn(p => p.UrlKey, new ContentView("Url".PadRight(60).Underline()));
			table.AddColumn(p => p.MimeType, new ContentView("MimeType".PadRight(30).Underline()));
			table.AddColumn(p => p.Length, new ContentView("Length".Underline()));
			table.AddColumn(p =>
			{
				if (String.IsNullOrEmpty(p.Operation))
					return "--".PadRight(15).Red();

				switch (p.Operation)
				{
					case "Waiting": return p.Operation.PadRight(15).LightYellow();
					case "Downloading": return p.Operation.PadRight(15).Magenta();
					case "Exists": return p.Operation.PadRight(15).Yellow();
					case "Saving": return p.Operation.PadRight(15).Green();
					case "Done": return p.Operation.PadRight(15).LightGreen();
					case "Error": return p.Operation.PadRight(15).Red();
					default: return p.Operation.PadRight(15).White();
				}
			}, new ContentView("Status".Underline()));

			Add(table);
		}

	}

}

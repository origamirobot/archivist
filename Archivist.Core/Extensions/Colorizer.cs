using System.CommandLine.Rendering;


namespace Archivist.Core.Extensions
{
	public static class Colorizer
	{
		public static TextSpan Underline(this string value) =>
			new ContainerSpan(StyleSpan.UnderlinedOn(),
							  new ContentSpan(value),
							  StyleSpan.UnderlinedOff());


		public static TextSpan Rgb(this string value, byte r, byte g, byte b) =>
			new ContainerSpan(ForegroundColorSpan.Rgb(r, g, b),
							  new ContentSpan(value),
							  ForegroundColorSpan.Reset());

		public static TextSpan LightGreen(this string value) =>
			new ContainerSpan(ForegroundColorSpan.LightGreen(),
							  new ContentSpan(value),
							  ForegroundColorSpan.Reset());

		public static TextSpan Green(this string value) =>
			new ContainerSpan(ForegroundColorSpan.Green(),
							  new ContentSpan(value),
							  ForegroundColorSpan.Reset());

		public static TextSpan White(this string value) =>
			new ContainerSpan(ForegroundColorSpan.White(),
							  new ContentSpan(value),
							  ForegroundColorSpan.Reset());

		public static TextSpan Yellow(this string value) =>
			new ContainerSpan(ForegroundColorSpan.Yellow(),
							  new ContentSpan(value),
							  ForegroundColorSpan.Reset());

		public static TextSpan LightYellow(this string value) =>
			new ContainerSpan(ForegroundColorSpan.LightYellow(),
							  new ContentSpan(value),
							  ForegroundColorSpan.Reset());

		public static TextSpan Magenta(this string value) =>
			new ContainerSpan(ForegroundColorSpan.Magenta(),
							  new ContentSpan(value),
							  ForegroundColorSpan.Reset());

		public static TextSpan Red(this string value) =>
			new ContainerSpan(ForegroundColorSpan.Red(),
							  new ContentSpan(value),
							  ForegroundColorSpan.Reset());
	}
}

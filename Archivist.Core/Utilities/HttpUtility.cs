using System;
using System.Web;

namespace Archivist.Core.Utilities
{

	/// <summary>
	/// Wrapper class around the <see cref="System.Web.HttpUtility"/> class.
	/// </summary>
	/// <seealso cref="Archivist.Core.Utilities.IHttpUtility" />
	public class HttpUtilityWrapper : IHttpUtility
	{

		/// <summary>
		/// Encodes a URL string.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public String UrlEncode(String text)
		{
			return HttpUtility.UrlEncode(text);
		}

		/// <summary>
		/// Converts a string that has been encoded for transmission in a URL into a decoded string.
		/// </summary>
		/// <param name="text">The string to decode.</param>
		/// <returns></returns>
		public String UrlDecode(String text)
		{
			return HttpUtility.UrlDecode(text);
		}

	}
}

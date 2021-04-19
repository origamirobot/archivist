using System;
using System.Web;

namespace Archivist.Core.Utilities
{

	/// <summary>
	/// Defines a contact to use in place of <see cref="HttpUtility"/> for testibility purposes.
	/// </summary>
	public interface IHttpUtility
	{

		/// <summary>
		/// Encodes a URL string.
		/// </summary>
		/// <param name="input">The text to encode.</param>
		/// <returns></returns>
		String UrlEncode(String text);

		/// <summary>
		/// Converts a string that has been encoded for transmission in a URL into a decoded string.
		/// </summary>
		/// <param name="text">The string to decode.</param>
		/// <returns></returns>
		String UrlDecode(String text);

	}

}

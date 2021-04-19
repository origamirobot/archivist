using System;

namespace Archivist.Core.Utilities
{

	/// <summary>
	/// Defines a contract that all JSON serializers must implement.
	/// </summary>
	public interface IJsonSerializer
	{

		/// <summary>
		/// Converts the specified json back into an object.
		/// </summary>
		/// <typeparam name="T">The type of object to deserialize to</typeparam>
		/// <param name="json">The JSON text to turn into an object</param>
		/// <returns></returns>
		T Deserialize<T>(String json);

		/// <summary>
		/// Serializes the specified object into a JSON string.
		/// </summary>
		/// <typeparam name="T">The type of object being serialized</typeparam>
		/// <param name="obj">The object to serialize.</param>
		/// <returns>Returns a string of JSON representing this object.</returns>
		String Serialize<T>(T obj);

		/// <summary>
		/// Returns the specified JSON in a much prettier, more-readable way.
		/// </summary>
		/// <param name="json">The json to prettify.</param>
		/// <returns>Returns prettified JSON.</returns>
		String Prettify(String json);

		/// <summary>
		/// Minifies the specified json.
		/// </summary>
		/// <param name="json">The json.</param>
		/// <returns></returns>
		String Minify(String json);

	}

}

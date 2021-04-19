using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Archivist.Core.Utilities
{

	/// <summary>
	/// JSON Serializer implementation using the Newtonsoft JSON serializer.
	/// </summary>
	/// <seealso cref="SkateLurker.Core.Serialization.IJsonSerializer" />
	public class NewtonsoftSerializer : IJsonSerializer
	{

		#region PROTECTED PROPERTIES


		/// <summary>
		/// Gets the settings.
		/// </summary>
		protected JsonSerializerSettings DeserializationSettings { get; private set; }

		/// <summary>
		/// Gets the serialization settings.
		/// </summary>
		protected JsonSerializerSettings SerializationSettings { get; private set; }


		#endregion PROTECTED PROPERTIES

		#region CONSTRUCTORS


		/// <summary>
		/// Initializes a new instance of the <see cref="NewtonsoftSerializer"/> class.
		/// </summary>
		public NewtonsoftSerializer()
		{
			SerializationSettings = new JsonSerializerSettings() { Formatting = Formatting.Indented };
			DeserializationSettings = new JsonSerializerSettings() { Formatting = Formatting.Indented };
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NewtonsoftSerializer" /> class.
		/// </summary>
		/// <param name="deserializationSettings">The settings.</param>
		/// <param name="serializationSettings">The serialization settings.</param>
		public NewtonsoftSerializer(
			JsonSerializerSettings deserializationSettings,
			JsonSerializerSettings serializationSettings)
		{
			DeserializationSettings = deserializationSettings;
			SerializationSettings = serializationSettings;
		}


		#endregion CONSTRUCTORS

		#region PUBLIC METHODS


		/// <summary>
		/// Converts the specified json back into an object.
		/// </summary>
		/// <typeparam name="T">The type of object to deserialize to</typeparam>
		/// <param name="json">The JSON text to turn into an object</param>
		/// <returns></returns>
		public T Deserialize<T>(String json)
		{
			return String.IsNullOrEmpty(json)
				? default
				: JsonConvert.DeserializeObject<T>(json, DeserializationSettings);
		}

		/// <summary>
		/// Serializes the specified object into a JSON string.
		/// </summary>
		/// <typeparam name="T">The type of object being serialized</typeparam>
		/// <param name="obj">The object to serialize.</param>
		/// <returns>
		/// Returns a string of JSON representing this object.
		/// </returns>
		public String Serialize<T>(T obj)
		{
			return JsonConvert.SerializeObject(obj, SerializationSettings);
		}

		/// <summary>
		/// Returns the specified JSON in a much prettier, more-readable way.
		/// </summary>
		/// <param name="json">The json to prettify.</param>
		/// <returns>
		/// Returns prettified JSON.
		/// </returns>
		/// <see href="http://weblog.west-wind.com/posts/2015/Mar/31/Prettifying-a-JSON-String-in-NET"/>
		public String Prettify(String json)
		{
			return JToken.Parse(json).ToString(Formatting.Indented);
		}

		/// <summary>
		/// Minifies the specified json.
		/// </summary>
		/// <param name="json">The json.</param>
		/// <returns></returns>
		public String Minify(String json)
		{
			return JToken.Parse(json).ToString(Formatting.None);
		}


		#endregion PUBLIC METHODS

	}

}

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WatsonTcp
{
	internal static class SerializationHelper
	{
		private static readonly JsonSerializerSettings HardenedSerializerSettings = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.None
		};

		private static readonly JsonSerializerSettings SerializerDefaults = new JsonSerializerSettings
		{
			NullValueHandling = NullValueHandling.Ignore,
			DateTimeZoneHandling = DateTimeZoneHandling.Local
		};

		internal static void InstantiateConverter()
		{
			try
			{
				Activator.CreateInstance<StringEnumConverter>();
			}
			catch (Exception)
			{
			}
		}

		internal static T DeserializeJson<T>(string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				throw new ArgumentNullException("json");
			}
			return JsonConvert.DeserializeObject<T>(json, HardenedSerializerSettings);
		}

		internal static string SerializeJson(object obj, bool pretty)
		{
			if (obj == null)
			{
				return null;
			}
			if (pretty)
			{
				return JsonConvert.SerializeObject(obj, Formatting.Indented, SerializerDefaults);
			}
			return JsonConvert.SerializeObject(obj, SerializerDefaults);
		}
	}
}

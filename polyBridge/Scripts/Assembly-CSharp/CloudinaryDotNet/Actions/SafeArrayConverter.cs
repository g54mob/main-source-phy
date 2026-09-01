using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CloudinaryDotNet.Actions
{
	public class SafeArrayConverter : JsonConverter
	{
		public override bool CanWrite => false;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JToken jToken = JToken.Load(reader);
			if (jToken.Type != JTokenType.Array)
			{
				return jToken.ToString().Split(',');
			}
			return jToken.ToObject<string[]>();
		}

		public override bool CanConvert(Type objectType)
		{
			if (!(objectType == typeof(string)))
			{
				return objectType == typeof(string[]);
			}
			return true;
		}

		public override void WriteJson(JsonWriter writer, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException("Unnecessary because of using just for Deserialization");
		}
	}
}

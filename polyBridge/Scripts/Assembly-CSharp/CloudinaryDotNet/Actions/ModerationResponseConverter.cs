using System;
using Newtonsoft.Json;

namespace CloudinaryDotNet.Actions
{
	public class ModerationResponseConverter : JsonConverter
	{
		public override bool CanWrite => false;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.StartObject)
			{
				return null;
			}
			return serializer.Deserialize(reader, objectType);
		}

		public override bool CanConvert(Type objectType)
		{
			return true;
		}

		public override void WriteJson(JsonWriter writer, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException("Unnecessary because of using just for Deserialization");
		}
	}
}

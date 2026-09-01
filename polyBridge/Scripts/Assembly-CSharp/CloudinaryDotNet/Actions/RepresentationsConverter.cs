using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CloudinaryDotNet.Actions
{
	internal class RepresentationsConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return true;
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			Transformation transformation = new Transformation();
			JArray jArray = JArray.Load(reader);
			if (jArray.Count > 0)
			{
				foreach (JProperty item in (IEnumerable<JToken>)jArray[0])
				{
					transformation.Add(item.Name, item.Value);
				}
			}
			return transformation;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteValue(((Transformation)value).ToString());
		}
	}
}

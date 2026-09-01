using System;
using System.Globalization;
using Newtonsoft.Json;

namespace CloudinaryDotNet.Actions
{
	public class SafeBooleanConverter : JsonConverter
	{
		public override bool CanWrite => false;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			object value = reader.Value;
			if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
			{
				return false;
			}
			return (value.ToString() == "0" || value.ToString() == "1") ? Convert.ToBoolean(Convert.ToInt16(value, CultureInfo.InvariantCulture)) : Convert.ToBoolean(value, CultureInfo.InvariantCulture);
		}

		public override bool CanConvert(Type objectType)
		{
			if (!(objectType == typeof(string)))
			{
				return objectType == typeof(bool);
			}
			return true;
		}

		public override void WriteJson(JsonWriter writer, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException("Unnecessary because of using just for Deserialization");
		}
	}
}

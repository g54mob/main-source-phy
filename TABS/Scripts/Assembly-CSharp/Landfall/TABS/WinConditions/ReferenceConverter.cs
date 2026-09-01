using System;
using Newtonsoft.Json;

namespace Landfall.TABS.WinConditions
{
	public class ReferenceConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			RuntimeReference reference = (RuntimeReference)value;
			string value2 = JsonConvert.SerializeObject(new SerializedRuntimeReference(reference));
			writer.WriteValue(value2);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			SerializedRuntimeReference serializedRuntimeReference = (SerializedRuntimeReference)JsonConvert.DeserializeObject((string)reader.Value, typeof(SerializedRuntimeReference));
			Type type = Type.GetType(serializedRuntimeReference.ReferenceType);
			Type type2 = ((!serializedRuntimeReference.IsRequest) ? typeof(ReferenceType<>).MakeGenericType(type) : typeof(ReferenceRequest<>).MakeGenericType(type));
			RuntimeReference obj = (RuntimeReference)Activator.CreateInstance(type2, Guid.Parse(serializedRuntimeReference.Guid).ToString());
			obj.ReferenceType = Type.GetType(serializedRuntimeReference.ReferenceType);
			return obj;
		}

		public override bool CanConvert(Type objectType)
		{
			return true;
		}
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

namespace Sirenix.Serialization.AOTGenerated
{
	[Preserve]
	internal static class PreventCodeStrippingViaReferences
	{
		static PreventCodeStrippingViaReferences()
		{
			bool flag = false;
			new Dictionary<string, float>();
			new DictionaryFormatter<string, float>();
			new Sirenix.Serialization.DerivedDictionaryFormatter<Dictionary<string, float>, string, float>();
			new GenericCollectionFormatter<Dictionary<string, float>, KeyValuePair<string, float>>();
			new SerializableFormatter<Dictionary<string, float>>();
			new ReflectionFormatter<Dictionary<string, float>>();
			new ComplexTypeSerializer<Dictionary<string, float>>();
			new List<string>();
			new ListFormatter<string>();
			new GenericCollectionFormatter<List<string>, string>();
			new ReflectionFormatter<List<string>>();
			new ComplexTypeSerializer<List<string>>();
			new HashSet<string>();
			new HashSetFormatter<string>();
			new GenericCollectionFormatter<HashSet<string>, string>();
			new SerializableFormatter<HashSet<string>>();
			new ReflectionFormatter<HashSet<string>>();
			new ComplexTypeSerializer<HashSet<string>>();
			new HashSet<PopUpWarningCategory>();
			new HashSetFormatter<PopUpWarningCategory>();
			new GenericCollectionFormatter<HashSet<PopUpWarningCategory>, PopUpWarningCategory>();
			new SerializableFormatter<HashSet<PopUpWarningCategory>>();
			new ReflectionFormatter<HashSet<PopUpWarningCategory>>();
			new ComplexTypeSerializer<HashSet<PopUpWarningCategory>>();
			Vector3 vector = default(Vector3);
			new Vector3Formatter();
			new ReflectionFormatter<Vector3>();
			ComplexTypeSerializer<Vector3> complexTypeSerializer = new ComplexTypeSerializer<Vector3>();
			if (flag)
			{
				complexTypeSerializer.ReadValueWeak(null);
				complexTypeSerializer.WriteValueWeak(null, null, null);
			}
			Vector2 vector2 = default(Vector2);
			new Vector2Formatter();
			new ReflectionFormatter<Vector2>();
			ComplexTypeSerializer<Vector2> complexTypeSerializer2 = new ComplexTypeSerializer<Vector2>();
			if (flag)
			{
				complexTypeSerializer2.ReadValueWeak(null);
				complexTypeSerializer2.WriteValueWeak(null, null, null);
			}
			new Dictionary<string, string>();
			new DictionaryFormatter<string, string>();
			new Sirenix.Serialization.DerivedDictionaryFormatter<Dictionary<string, string>, string, string>();
			new GenericCollectionFormatter<Dictionary<string, string>, KeyValuePair<string, string>>();
			new SerializableFormatter<Dictionary<string, string>>();
			new ReflectionFormatter<Dictionary<string, string>>();
			new ComplexTypeSerializer<Dictionary<string, string>>();
			PointOfViewType pointOfViewType = default(PointOfViewType);
			EnumSerializer<PointOfViewType> enumSerializer = new EnumSerializer<PointOfViewType>();
			if (flag)
			{
				enumSerializer.ReadValueWeak(null);
				enumSerializer.WriteValueWeak(null, null, null);
			}
			Quaternion quaternion = default(Quaternion);
			new QuaternionFormatter();
			new ReflectionFormatter<Quaternion>();
			ComplexTypeSerializer<Quaternion> complexTypeSerializer3 = new ComplexTypeSerializer<Quaternion>();
			if (flag)
			{
				complexTypeSerializer3.ReadValueWeak(null);
				complexTypeSerializer3.WriteValueWeak(null, null, null);
			}
			new PrimitiveArrayFormatter<byte>();
			new PrimitiveArrayFormatter<byte>();
			new ReflectionFormatter<byte[]>();
			new ComplexTypeSerializer<byte[]>();
			DateTime dateTime = default(DateTime);
			new DateTimeFormatter();
			new SerializableFormatter<DateTime>();
			new ReflectionFormatter<DateTime>();
			ComplexTypeSerializer<DateTime> complexTypeSerializer4 = new ComplexTypeSerializer<DateTime>();
			if (flag)
			{
				complexTypeSerializer4.ReadValueWeak(null);
				complexTypeSerializer4.WriteValueWeak(null, null, null);
			}
			new Dictionary<int, DateTime>();
			new DictionaryFormatter<int, DateTime>();
			new Sirenix.Serialization.DerivedDictionaryFormatter<Dictionary<int, DateTime>, int, DateTime>();
			new GenericCollectionFormatter<Dictionary<int, DateTime>, KeyValuePair<int, DateTime>>();
			new SerializableFormatter<Dictionary<int, DateTime>>();
			new ReflectionFormatter<Dictionary<int, DateTime>>();
			new ComplexTypeSerializer<Dictionary<int, DateTime>>();
			new Dictionary<string, CampaignLevelStatus>();
			new DictionaryFormatter<string, CampaignLevelStatus>();
			new Sirenix.Serialization.DerivedDictionaryFormatter<Dictionary<string, CampaignLevelStatus>, string, CampaignLevelStatus>();
			new GenericCollectionFormatter<Dictionary<string, CampaignLevelStatus>, KeyValuePair<string, CampaignLevelStatus>>();
			new SerializableFormatter<Dictionary<string, CampaignLevelStatus>>();
			new ReflectionFormatter<Dictionary<string, CampaignLevelStatus>>();
			new ComplexTypeSerializer<Dictionary<string, CampaignLevelStatus>>();
			new Dictionary<string, int>();
			new DictionaryFormatter<string, int>();
			new Sirenix.Serialization.DerivedDictionaryFormatter<Dictionary<string, int>, string, int>();
			new GenericCollectionFormatter<Dictionary<string, int>, KeyValuePair<string, int>>();
			new SerializableFormatter<Dictionary<string, int>>();
			new ReflectionFormatter<Dictionary<string, int>>();
			new ComplexTypeSerializer<Dictionary<string, int>>();
		}
	}
}

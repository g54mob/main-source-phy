using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Battlehub.Utils
{
	public static class XmlUtility
	{
		private static readonly Dictionary<RuntimeTypeHandle, XmlSerializer> ms_serializers = new Dictionary<RuntimeTypeHandle, XmlSerializer>();

		public static string ToXml<T>(T value, Formatting formatting = Formatting.None) where T : new()
		{
			XmlSerializer value2 = GetValue(typeof(T));
			using MemoryStream memoryStream = new MemoryStream();
			using XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, new UTF8Encoding());
			xmlTextWriter.Formatting = formatting;
			value2.Serialize(xmlTextWriter, value);
			return Encoding.UTF8.GetString(memoryStream.ToArray());
		}

		public static void ToXml<T>(T value, Stream stream) where T : new()
		{
			GetValue(typeof(T)).Serialize(stream, value);
		}

		public static T FromXml<T>(string srcString) where T : new()
		{
			XmlSerializer value = GetValue(typeof(T));
			using StringReader input = new StringReader(srcString);
			using XmlReader xmlReader = new XmlTextReader(input);
			return (T)value.Deserialize(xmlReader);
		}

		public static T FromXml<T>(Stream source) where T : new()
		{
			return (T)GetValue(typeof(T)).Deserialize(source);
		}

		private static XmlSerializer GetValue(Type type)
		{
			if (!ms_serializers.TryGetValue(type.TypeHandle, out var value))
			{
				lock (ms_serializers)
				{
					if (!ms_serializers.TryGetValue(type.TypeHandle, out value))
					{
						value = new XmlSerializer(type);
						ms_serializers.Add(type.TypeHandle, value);
					}
				}
			}
			return value;
		}
	}
}

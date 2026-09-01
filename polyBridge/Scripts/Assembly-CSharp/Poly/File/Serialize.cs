using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine;

namespace Poly.File
{
	public static class Serialize
	{
		public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
		{
			using Stream serializationStream = System.IO.File.Open(filePath, append ? FileMode.Append : FileMode.Create);
			new BinaryFormatter().Serialize(serializationStream, objectToWrite);
		}

		public static T ReadFromBinaryFile<T>(string filePath)
		{
			using Stream serializationStream = System.IO.File.Open(filePath, FileMode.Open);
			return (T)new BinaryFormatter().Deserialize(serializationStream);
		}

		public static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
		{
			TextWriter textWriter = null;
			try
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
				textWriter = new StreamWriter(filePath, append);
				xmlSerializer.Serialize(textWriter, objectToWrite);
			}
			finally
			{
				textWriter?.Close();
			}
		}

		public static T ReadFromXmlFile<T>(string filePath) where T : new()
		{
			TextReader textReader = null;
			try
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
				textReader = new StreamReader(filePath);
				return (T)xmlSerializer.Deserialize(textReader);
			}
			finally
			{
				textReader?.Close();
			}
		}

		public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false, bool prettyPrint = false) where T : new()
		{
			TextWriter textWriter = null;
			try
			{
				string value = JsonUtility.ToJson(objectToWrite, prettyPrint);
				textWriter = new StreamWriter(filePath, append);
				textWriter.Write(value);
			}
			finally
			{
				textWriter?.Close();
			}
		}

		public static T ReadFromJsonFile<T>(string filePath) where T : new()
		{
			TextReader textReader = null;
			try
			{
				textReader = new StreamReader(filePath);
				return JsonUtility.FromJson<T>(textReader.ReadToEnd());
			}
			finally
			{
				textReader?.Close();
			}
		}

		private static void UsageExample<T>(T object1)
		{
			WriteToBinaryFile("C:\\someClass.txt", object1);
			object1 = ReadFromBinaryFile<T>("C:\\someClass.txt");
		}
	}
}

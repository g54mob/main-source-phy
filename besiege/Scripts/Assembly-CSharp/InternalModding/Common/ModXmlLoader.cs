using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using InternalModding.Misc;
using Modding.Serialization;

namespace InternalModding.Common
{
	public static class ModXmlLoader
	{
		public static T Deserialize<T>(string path, bool validate) where T : Element
		{
			try
			{
				using (FileStream stream = new FileStream(path, FileMode.Open))
				{
					using (StreamReader reader = new StreamReader(stream))
					{
						XmlSerializer serializer = new XmlSerializer(typeof(T));
						XDocument xDoc = XDocument.Load(reader, LoadOptions.SetLineInfo);
						string name = new FileInfo(path).Name;
						return Deserialize<T>(xDoc, serializer, validate, name, 0);
					}
				}
			}
			catch (Exception ex)
			{
				MLog.Error("Error loading mod XML file: " + new FileInfo(path).Name);
				MLog.Error(ex.ToString());
				return (T)null;
			}
		}

		public static T Deserialize<T>(string content, bool validate, string fileName, int lineOffset, Type serializerType = null) where T : Element
		{
			try
			{
				XmlSerializer serializer = ((serializerType != null) ? new XmlSerializer(serializerType) : new XmlSerializer(typeof(T)));
				XDocument xDoc = XDocument.Load(new StringReader(content), LoadOptions.SetLineInfo);
				return Deserialize<T>(xDoc, serializer, validate, fileName, lineOffset);
			}
			catch (Exception ex)
			{
				MLog.Error("Error loading mod XML file: " + fileName);
				MLog.Error(ex.ToString());
				return (T)null;
			}
		}

		private static T Deserialize<T>(XDocument xDoc, XmlSerializer serializer, bool validate, string fileName, int lineOffset) where T : Element
		{
			PreProcess(xDoc.Root, fileName, lineOffset);
			serializer.UnknownNode += delegate(object sender, XmlNodeEventArgs args)
			{
				if (args.Text == ">")
				{
					MLog.WarnFormat("In {0}: There appears to be an extraneous '>' character somewhere in the file.This can cause deserialization of some elements to silently fail!", fileName);
				}
			};
			T val = (T)serializer.Deserialize(xDoc.CreateReader());
			if (val == null)
			{
				return (T)null;
			}
			if (validate && !val.InvokeValidate())
			{
				MLog.Error("Error loading " + fileName);
				return (T)null;
			}
			return val;
		}

		private static void PreProcess(XElement element, string fileName, int lineOffset = 0)
		{
			string[] array = (from a in element.Attributes()
				select a.Name.LocalName).ToArray();
			string[] array2 = (from e in element.Elements()
				select e.Name.LocalName).ToArray();
			if (!string.IsNullOrEmpty(element.Value))
			{
				if (element.Value == "True")
				{
					element.Value = "true";
				}
				else if (element.Value == "False")
				{
					element.Value = "false";
				}
				string text = element.Value.TrimStart('\n', '\r').TrimEnd();
				if (text != element.Value)
				{
					element.Value = text;
				}
			}
			foreach (XAttribute item in element.Attributes())
			{
				if (item.Value == "True")
				{
					item.Value = "true";
				}
				else if (item.Value == "False")
				{
					element.Value = "false";
				}
			}
			foreach (XElement item2 in element.Elements())
			{
				PreProcess(item2, fileName, lineOffset);
			}
			element.SetAttributeValue("lineNumber", ((IXmlLineInfo)element).LineNumber + lineOffset);
			element.SetAttributeValue("linePosition", ((IXmlLineInfo)element).LinePosition);
			element.SetAttributeValue("fileName", fileName);
			if (array2.Length > 0)
			{
				element.SetAttributeValue("elementsUsed", string.Join("|", array2));
			}
			if (array.Length > 0)
			{
				element.SetAttributeValue("attributesUsed", string.Join("|", array));
			}
		}
	}
}

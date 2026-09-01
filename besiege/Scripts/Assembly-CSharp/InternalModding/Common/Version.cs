using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace InternalModding.Common
{
	[Serializable]
	public class Version : IXmlSerializable
	{
		public int Major { get; set; }

		public int Minor { get; set; }

		public int Build { get; set; }

		public Version(int major, int minor, int build = 0)
		{
			Major = major;
			Minor = minor;
			Build = build;
		}

		public Version(System.Version o)
			: this(o.Major, o.Minor, o.Build)
		{
		}

		public Version()
			: this(1, 0)
		{
		}

		public override string ToString()
		{
			return string.Format("{0}.{1}.{2}", Major, Minor, Build);
		}

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader reader)
		{
			reader.ReadStartElement();
			string text = reader.ReadContentAsString();
			reader.ReadEndElement();
			if (string.IsNullOrEmpty(text))
			{
				throw new InvalidDataException("Version text is empty!");
			}
			string[] array = text.Split('.');
			if (array.Length != 3)
			{
				throw new InvalidDataException("Version is not of the correct format!");
			}
			Major = int.Parse(array[0]);
			Minor = int.Parse(array[1]);
			Build = int.Parse(array[2]);
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteString(ToString());
		}

		public static implicit operator System.Version(Version v)
		{
			return new System.Version(v.Major, v.Minor, v.Build);
		}

		public static implicit operator Version(System.Version v)
		{
			return new Version(v.Major, v.Minor, v.Build);
		}
	}
}

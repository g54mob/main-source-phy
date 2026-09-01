using System;
using System.ComponentModel;
using System.Xml.Serialization;
using InternalModding.Common;

namespace Modding.Serialization
{
	[Serializable]
	public class Element : IValidatable
	{
		internal static string[] SpecialAttributeNames = new string[5] { "lineNumber", "linePosition", "attributesUsed", "elementsUsed", "fileName" };

		[XmlAttribute("lineNumber")]
		public int LineNumber { get; private set; }

		[XmlAttribute("linePosition")]
		public int LinePosition { get; private set; }

		[DefaultValue("")]
		[XmlAttribute("attributesUsed")]
		public string AttributesUsed { get; private set; }

		[XmlAttribute("elementsUsed")]
		[DefaultValue("")]
		public string ElementsUsed { get; private set; }

		[XmlAttribute("fileName")]
		[DefaultValue("unknown")]
		public string FileName { get; private set; }

		public Element()
		{
			AttributesUsed = string.Empty;
			ElementsUsed = string.Empty;
			FileName = "unknown";
		}

		internal bool InvokeValidate()
		{
			return Validate();
		}

		internal bool InvokeValidate(string elementName)
		{
			return Validate(elementName);
		}

		protected virtual bool Validate()
		{
			return Validate(GetType().Name);
		}

		protected virtual bool Validate(string elementName)
		{
			return InternalModding.Common.Serialization.Validate(elementName, this);
		}

		protected bool MissingElement(string elemName, string missing)
		{
			return InternalModding.Common.Serialization.MissingElement(elemName, missing, this);
		}

		protected bool MissingAttribute(string elemName, string missing)
		{
			return InternalModding.Common.Serialization.MissingAttribute(elemName, missing, this);
		}

		protected bool InvalidData(string elemName, string error)
		{
			return InternalModding.Common.Serialization.InvalidData(elemName, error, this);
		}

		protected void Warn(string elemName, string warning)
		{
			InternalModding.Common.Serialization.Warn(elemName, warning, this);
		}
	}
}

using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Modding.Serialization
{
	[Serializable]
	public abstract class MapperTypeDefinition : Element
	{
		[XmlAttribute("key")]
		public string Key;

		[XmlAttribute("displayName")]
		public string DisplayName;

		[XmlAttribute("showInMapper")]
		[DefaultValue(true)]
		public bool ShowInMapper = true;

		public abstract MapperType Create(SaveableDataHolder holder);
	}
}

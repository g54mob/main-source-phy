using System;
using System.Xml.Serialization;

namespace Modding.Serialization
{
	[Serializable]
	public abstract class MapperTypeReference : Element
	{
		[XmlAttribute("key")]
		public string Key;
	}
}

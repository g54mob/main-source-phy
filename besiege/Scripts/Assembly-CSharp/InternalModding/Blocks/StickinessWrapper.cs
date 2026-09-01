using System.ComponentModel;
using System.Xml.Serialization;
using Modding.Serialization;

namespace InternalModding.Blocks
{
	public class StickinessWrapper : Element
	{
		[XmlAttribute("enabled")]
		public bool Enabled;

		[DefaultValue(1f)]
		[XmlAttribute("radius")]
		public float Radius = 1f;

		[XmlIgnore]
		public bool RadiusSpecified;
	}
}

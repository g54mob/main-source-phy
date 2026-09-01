using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Modding.Serialization;

namespace InternalModding.Common
{
	[Serializable]
	public class ModIdPair : Element
	{
		[DefaultValue("")]
		[XmlElement("ModID")]
		public string ModIdStr;

		[XmlIgnore]
		public bool ModIdStrSpecified;

		[XmlElement("LocalID")]
		public int LocalId;

		[XmlIgnore]
		public Guid ModId
		{
			get
			{
				return new Guid(ModIdStr);
			}
		}

		[XmlIgnore]
		public bool ModIdSpecified
		{
			get
			{
				return ModIdStrSpecified;
			}
		}
	}
}

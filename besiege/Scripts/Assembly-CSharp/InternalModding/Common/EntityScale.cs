using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Modding.Serialization;

namespace InternalModding.Common
{
	[Serializable]
	public class EntityScale : Element
	{
		[DefaultValue(true)]
		[XmlAttribute("canScale")]
		public bool CanScale = true;

		[DefaultValue(false)]
		[XmlAttribute("uniformScale")]
		public bool UniformScale;

		protected override bool Validate(string name)
		{
			return true;
		}
	}
}

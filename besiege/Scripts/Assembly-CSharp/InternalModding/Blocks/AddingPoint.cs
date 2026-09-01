using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Modding.Serialization;

namespace InternalModding.Blocks
{
	[Serializable]
	public class AddingPoint : Element
	{
		[XmlElement("Stickiness")]
		[DefaultValue(null)]
		[RequireToValidate]
		public StickinessWrapper Stickiness;

		[XmlElement("Position")]
		public Vector3 Position { get; internal set; }

		[XmlElement("Rotation")]
		public Vector3 Rotation { get; internal set; }

		[XmlIgnore]
		public bool Sticky
		{
			get
			{
				return Stickiness != null && Stickiness.Enabled;
			}
		}

		[XmlIgnore]
		public float Radius
		{
			get
			{
				return (Stickiness == null) ? 0f : Stickiness.Radius;
			}
		}

		public AddingPoint()
		{
			Stickiness = null;
		}
	}
}

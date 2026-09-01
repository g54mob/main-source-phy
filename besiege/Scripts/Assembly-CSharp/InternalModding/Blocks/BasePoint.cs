using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Modding.Serialization;
using UnityEngine;

namespace InternalModding.Blocks
{
	[Serializable]
	public class BasePoint : Element
	{
		public class MotionWrapper : Element
		{
			[XmlAttribute("x")]
			public bool X;

			[XmlAttribute("y")]
			public bool Y;

			[XmlAttribute("z")]
			public bool Z;
		}

		[XmlAttribute("hasAddingPoint")]
		public bool HasAddingPoint { get; internal set; }

		[RequireToValidate]
		[XmlElement("Stickiness")]
		public StickinessWrapper Stickiness { get; set; }

		[XmlAttribute("breakForce")]
		[DefaultValue(6187f)]
		public float BreakForce { get; internal set; }

		[XmlIgnore]
		public bool Sticky
		{
			get
			{
				return Stickiness.Enabled;
			}
		}

		[XmlIgnore]
		public float Radius
		{
			get
			{
				return Stickiness.Radius;
			}
		}

		[RequireToValidate]
		[XmlElement("Motion")]
		[DefaultValue(null)]
		public MotionWrapper Motion { get; set; }

		[XmlIgnore]
		public bool HasMotion
		{
			get
			{
				return Motion != null && (Motion.X || Motion.Y || Motion.Z);
			}
		}

		[XmlIgnore]
		private bool HasX
		{
			get
			{
				return HasMotion && Motion.X;
			}
		}

		[XmlIgnore]
		private bool HasY
		{
			get
			{
				return HasMotion && Motion.Y;
			}
		}

		[XmlIgnore]
		private bool HasZ
		{
			get
			{
				return HasMotion && Motion.Z;
			}
		}

		[XmlIgnore]
		public ConfigurableJointMotion MotionX
		{
			get
			{
				return (ConfigurableJointMotion)(2 * Convert.ToInt16(HasX));
			}
		}

		[XmlIgnore]
		public ConfigurableJointMotion MotionY
		{
			get
			{
				return (ConfigurableJointMotion)(2 * Convert.ToInt16(HasY));
			}
		}

		[XmlIgnore]
		public ConfigurableJointMotion MotionZ
		{
			get
			{
				return (ConfigurableJointMotion)(2 * Convert.ToInt16(HasZ));
			}
		}

		public BasePoint()
		{
			Motion = null;
			BreakForce = 6187f;
		}

		protected override bool Validate(string elemName)
		{
			if (!base.Validate(elemName))
			{
				return false;
			}
			if (!Stickiness.Enabled && Motion != null)
			{
				return InvalidData(elemName, "Cannot contain Motion element if the point is not sticky!");
			}
			return true;
		}
	}
}

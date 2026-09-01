using System.ComponentModel;
using System.Xml.Serialization;
using Modding.Serialization;

namespace Modding.Modules.Official
{
	[XmlRoot("Steering")]
	[Reloadable]
	public class SteeringModule : BlockModule
	{
		[XmlElement]
		[RequireToValidate]
		public MKeyReference LeftKey;

		[XmlElement]
		[RequireToValidate]
		public MKeyReference RightKey;

		[XmlElement]
		[RequireToValidate]
		public MSliderReference SpeedSlider;

		[XmlElement]
		[RequireToValidate]
		public MToggleReference AutomaticToggle;

		[XmlElement]
		public Direction Axis;

		[Reloadable]
		[XmlElement]
		public float MaxAngularSpeed;

		[XmlElement]
		[Reloadable]
		public float TargetAngleSpeed;

		[XmlElement]
		public bool HasLimits;

		[DefaultValue(null)]
		[RequireToValidate]
		[Reloadable]
		[XmlElement]
		public TransformValues LimitsDisplay;

		[XmlElement]
		[DefaultValue(0)]
		public float LimitsDefaultMin;

		[XmlIgnore]
		public bool LimitsDefaultMinSpecified;

		[XmlElement]
		public float LimitsDefaultMax;

		[XmlIgnore]
		public bool LimitsDefaultMaxSpecified;

		[Reloadable]
		[XmlElement]
		public float LimitsHighestAngle;

		[Reloadable]
		[XmlIgnore]
		public bool LimitsHighestAngleSpecified;

		protected override bool Validate(string elemName)
		{
			if (!base.Validate(elemName))
			{
				return false;
			}
			if (HasLimits)
			{
				if (LimitsDisplay == null)
				{
					return MissingElement(elemName, "LimitsDisplay");
				}
				if (!LimitsDefaultMinSpecified)
				{
					return MissingElement(elemName, "LimitsDefaultMin");
				}
				if (!LimitsDefaultMaxSpecified)
				{
					return MissingElement(elemName, "LimitsDefaultMax");
				}
				if (!LimitsHighestAngleSpecified)
				{
					return MissingElement(elemName, "LimitsHighestAngle");
				}
			}
			return true;
		}
	}
}

using System.ComponentModel;
using System.Xml.Serialization;
using Modding.Serialization;

namespace Modding.Modules.Official
{
	[XmlRoot("Spinning")]
	[Reloadable]
	public class SpinningModule : BlockModule
	{
		[XmlElement]
		[RequireToValidate]
		public MKeyReference Forward;

		[RequireToValidate]
		[XmlElement]
		public MKeyReference Backward;

		[RequireToValidate]
		[XmlElement]
		public MSliderReference SpeedSlider;

		[XmlElement]
		[RequireToValidate]
		public MSliderReference AccelerationSlider;

		[RequireToValidate]
		[XmlElement]
		public MToggleReference AutomaticToggle;

		[RequireToValidate]
		[XmlElement]
		public MToggleReference ToggleModeToggle;

		[Reloadable]
		[XmlElement]
		public Direction Axis;

		[Reloadable]
		[DefaultValue(null)]
		[XmlElement]
		public float MaxAngularSpeed = 50f;
	}
}

using System.ComponentModel;
using System.Xml.Serialization;
using Modding.Serialization;

namespace Modding.Modules.Official
{
	[XmlRoot("Spewing")]
	[Reloadable]
	public class SpewingModule : BlockModule
	{
		[RequireToValidate]
		[XmlElement]
		public MKeyReference TriggerKey;

		[XmlElement]
		[RequireToValidate]
		public MSliderReference RangeSlider;

		[RequireToValidate]
		[XmlElement]
		public MToggleReference HoldToFireToggle;

		[DefaultValue(0f)]
		[XmlElement]
		public float ToggleTimeLimit;

		[XmlIgnore]
		public bool ToggleTimeLimitSpecified;

		[Reloadable]
		[XmlElement]
		public float BaseAmmo;

		[Reloadable]
		[XmlElement]
		[DefaultValue(false)]
		public bool AcceptFireAmmo;

		[XmlArrayItem("Steam", typeof(ParticleHelper.SteamParticles))]
		[XmlArrayItem("Water", typeof(ParticleHelper.WaterParticles))]
		[XmlArrayItem("Fire", typeof(ParticleHelper.FireParticles))]
		[RequireToValidate]
		[CanBeEmpty]
		[XmlArray]
		[Reloadable]
		[XmlArrayItem("Custom", typeof(ParticleHelper.CustomParticles))]
		public ParticleHelper.ParticleDefinition[] ParticleSystems;
	}
}

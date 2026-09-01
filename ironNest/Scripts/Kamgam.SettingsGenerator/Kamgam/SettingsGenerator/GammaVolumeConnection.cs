using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Kamgam.SettingsGenerator
{
	public class GammaVolumeConnection : Connection<float>
	{
		protected LiftGammaGain _effect;

		public Vector4 _defaultValue;

		public void UpdateDefaultValue()
		{
		}

		public override float Get()
		{
			return 0f;
		}

		public override void Set(float gamma)
		{
		}
	}
}

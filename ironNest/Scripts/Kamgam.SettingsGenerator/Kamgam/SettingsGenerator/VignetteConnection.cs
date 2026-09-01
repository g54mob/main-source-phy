using UnityEngine.Rendering.Universal;

namespace Kamgam.SettingsGenerator
{
	public class VignetteConnection : Connection<bool>
	{
		protected Vignette _vignette;

		public override bool Get()
		{
			return false;
		}

		public override void Set(bool enable)
		{
		}
	}
}

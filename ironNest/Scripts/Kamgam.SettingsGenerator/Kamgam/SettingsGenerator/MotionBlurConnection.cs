using UnityEngine.Rendering.Universal;

namespace Kamgam.SettingsGenerator
{
	public class MotionBlurConnection : Connection<bool>
	{
		protected MotionBlur _blur;

		public override bool Get()
		{
			return false;
		}

		public override void Set(bool enable)
		{
		}
	}
}

using UnityEngine.Rendering.Universal;

namespace Kamgam.SettingsGenerator
{
	public class DepthOfFieldConnection : Connection<bool>
	{
		protected DepthOfField _dof;

		public override bool Get()
		{
			return false;
		}

		public override void Set(bool enable)
		{
		}
	}
}

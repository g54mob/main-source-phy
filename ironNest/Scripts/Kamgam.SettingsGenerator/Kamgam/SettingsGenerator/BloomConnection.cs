using UnityEngine.Rendering.Universal;

namespace Kamgam.SettingsGenerator
{
	public class BloomConnection : Connection<bool>
	{
		protected Bloom _bloom;

		public override bool GetDefault()
		{
			return false;
		}

		public override bool Get()
		{
			return false;
		}

		public override void Set(bool enable)
		{
		}
	}
}

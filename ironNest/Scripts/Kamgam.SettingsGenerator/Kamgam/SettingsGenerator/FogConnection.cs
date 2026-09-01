namespace Kamgam.SettingsGenerator
{
	public class FogConnection : Connection<bool>
	{
		public override bool Get()
		{
			return false;
		}

		public override void Set(bool enable)
		{
		}
	}
}

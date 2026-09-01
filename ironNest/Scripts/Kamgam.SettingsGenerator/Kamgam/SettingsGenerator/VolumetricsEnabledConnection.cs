namespace Kamgam.SettingsGenerator
{
	public class VolumetricsEnabledConnection : Connection<bool>
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

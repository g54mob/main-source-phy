namespace Kamgam.SettingsGenerator
{
	public class PauseOnFocusLossConnection : Connection<bool>
	{
		public override bool Get()
		{
			return false;
		}

		public override void Set(bool value)
		{
		}
	}
}

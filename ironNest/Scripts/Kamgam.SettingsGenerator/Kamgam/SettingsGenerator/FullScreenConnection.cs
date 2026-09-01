namespace Kamgam.SettingsGenerator
{
	public class FullScreenConnection : Connection<bool>
	{
		protected bool? lastKnownFullScreen;

		protected int lastSetFrame;

		public override bool Get()
		{
			return false;
		}

		public override void Set(bool fullScreen)
		{
		}
	}
}

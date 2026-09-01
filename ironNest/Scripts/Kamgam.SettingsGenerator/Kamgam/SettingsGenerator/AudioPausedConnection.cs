namespace Kamgam.SettingsGenerator
{
	public class AudioPausedConnection : Connection<bool>
	{
		public bool Invert;

		public AudioPausedConnection(bool invert = true)
		{
		}

		public override bool Get()
		{
			return false;
		}

		public override void Set(bool pause)
		{
		}
	}
}

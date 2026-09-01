namespace Kamgam.SettingsGenerator
{
	public class MasterAudioMuteEverythingConnection : Connection<bool>
	{
		public bool Invert;

		public MasterAudioMuteEverythingConnection(bool invert)
		{
		}

		public override bool Get()
		{
			return false;
		}

		public override void Set(bool mute)
		{
		}
	}
}

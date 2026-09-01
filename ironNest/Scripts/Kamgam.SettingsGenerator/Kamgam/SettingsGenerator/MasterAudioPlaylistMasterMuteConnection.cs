namespace Kamgam.SettingsGenerator
{
	public class MasterAudioPlaylistMasterMuteConnection : Connection<bool>
	{
		public bool Invert;

		public MasterAudioPlaylistMasterMuteConnection(bool invert)
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

namespace Kamgam.SettingsGenerator
{
	public class MasterAudioPlaylistMuteConnection : Connection<bool>
	{
		public bool Invert;

		public string PlaylistName;

		public MasterAudioPlaylistMuteConnection(string playlistName, bool invert)
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

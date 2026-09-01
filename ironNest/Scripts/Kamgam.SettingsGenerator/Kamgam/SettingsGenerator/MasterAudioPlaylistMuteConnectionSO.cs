using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "MasterAudioPlaylistMuteConnection", menuName = "SettingsGenerator/Connection/MasterAudio/PlaylistMuteConnection", order = 4)]
	public class MasterAudioPlaylistMuteConnectionSO : BoolConnectionSO
	{
		public string PlaylistName;

		public bool Invert;

		protected MasterAudioPlaylistMuteConnection _connection;

		public override IConnection<bool> GetConnection()
		{
			return null;
		}

		public void Create()
		{
		}

		public override void DestroyConnection()
		{
		}
	}
}

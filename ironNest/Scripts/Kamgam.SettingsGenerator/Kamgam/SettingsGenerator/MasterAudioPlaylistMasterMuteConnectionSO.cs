using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "MasterAudioPlaylistMasterMuteConnection", menuName = "SettingsGenerator/Connection/MasterAudio/PlaylistMasterMuteConnection", order = 4)]
	public class MasterAudioPlaylistMasterMuteConnectionSO : BoolConnectionSO
	{
		public bool Invert;

		protected MasterAudioPlaylistMasterMuteConnection _connection;

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

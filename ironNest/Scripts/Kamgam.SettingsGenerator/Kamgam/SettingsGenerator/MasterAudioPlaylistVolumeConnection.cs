using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class MasterAudioPlaylistVolumeConnection : Connection<float>
	{
		public string PlaylistName;

		public Vector2 InputRange;

		public MasterAudioPlaylistVolumeConnection(Vector2 inputRange, string playlistName)
		{
		}

		public override float Get()
		{
			return 0f;
		}

		public override void Set(float volume)
		{
		}
	}
}

using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "MasterAudioMuteEverythingConnection", menuName = "SettingsGenerator/Connection/MasterAudio/MuteEverythingConnection", order = 4)]
	public class MasterAudioMuteEverythingConnectionSO : BoolConnectionSO
	{
		public bool Invert;

		protected MasterAudioMuteEverythingConnection _connection;

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

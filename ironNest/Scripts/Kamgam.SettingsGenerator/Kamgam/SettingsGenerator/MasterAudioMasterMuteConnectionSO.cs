using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "MasterAudioMasterMuteConnection", menuName = "SettingsGenerator/Connection/MasterAudio/MasterMuteConnection", order = 4)]
	public class MasterAudioMasterMuteConnectionSO : BoolConnectionSO
	{
		public bool Invert;

		protected MasterAudioMasterMuteConnection _connection;

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

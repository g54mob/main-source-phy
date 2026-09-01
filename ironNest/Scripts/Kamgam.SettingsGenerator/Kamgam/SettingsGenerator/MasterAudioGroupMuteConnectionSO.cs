using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "MasterAudioGroupMuteConnection", menuName = "SettingsGenerator/Connection/MasterAudio/GroupMuteConnection", order = 4)]
	public class MasterAudioGroupMuteConnectionSO : BoolConnectionSO
	{
		public string GroupName;

		public bool Invert;

		protected MasterAudioGroupMuteConnection _connection;

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

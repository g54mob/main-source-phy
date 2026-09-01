using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "MasterAudioBusMuteConnection", menuName = "SettingsGenerator/Connection/MasterAudio/BusMuteConnection", order = 4)]
	public class MasterAudioBusMuteConnectionSO : BoolConnectionSO
	{
		public string BusName;

		public bool Invert;

		protected MasterAudioBusMuteConnection _connection;

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

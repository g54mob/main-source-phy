using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "VolumetricsEnabledConnection", menuName = "SettingsGenerator/Connection/VolumetricsEnabledConnection", order = 4)]
	public class VolumetricsEnabledConnectionSO : BoolConnectionSO
	{
		protected VolumetricsEnabledConnection _connection;

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

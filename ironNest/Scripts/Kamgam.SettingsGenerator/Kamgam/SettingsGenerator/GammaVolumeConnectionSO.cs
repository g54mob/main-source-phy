using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "GammaVolumeConnection", menuName = "SettingsGenerator/Connection/GammaVolumeConnection", order = 4)]
	public class GammaVolumeConnectionSO : FloatConnectionSO
	{
		protected GammaVolumeConnection _connection;

		public override IConnection<float> GetConnection()
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

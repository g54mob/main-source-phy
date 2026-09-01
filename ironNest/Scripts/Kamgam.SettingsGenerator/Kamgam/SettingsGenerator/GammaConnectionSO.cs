using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "GammaConnection", menuName = "SettingsGenerator/Connection/GammaConnection", order = 4)]
	public class GammaConnectionSO : FloatConnectionSO
	{
		protected GammaConnection _connection;

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

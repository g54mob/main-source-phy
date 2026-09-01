using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "FogConnection", menuName = "SettingsGenerator/Connection/FogConnection", order = 4)]
	public class FogConnectionSO : BoolConnectionSO
	{
		protected FogConnection _connection;

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

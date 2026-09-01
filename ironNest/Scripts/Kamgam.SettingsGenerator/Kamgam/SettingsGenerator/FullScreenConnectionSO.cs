using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "FullScreenConnection", menuName = "SettingsGenerator/Connection/FullScreenConnection", order = 4)]
	public class FullScreenConnectionSO : BoolConnectionSO
	{
		protected FullScreenConnection _connection;

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

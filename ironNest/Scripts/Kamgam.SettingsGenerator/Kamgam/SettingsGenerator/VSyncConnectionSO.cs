using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "VSyncConnection", menuName = "SettingsGenerator/Connection/VSyncConnection", order = 1)]
	public class VSyncConnectionSO : BoolConnectionSO
	{
		protected VSyncConnection _connection;

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

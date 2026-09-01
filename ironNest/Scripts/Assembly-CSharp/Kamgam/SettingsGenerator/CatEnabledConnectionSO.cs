using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "CatEnabledConnection", menuName = "SettingsGenerator/Connection/Cat/CatEnabled")]
	public class CatEnabledConnectionSO : BoolConnectionSO
	{
		protected CatEnabledConnection _connection;

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

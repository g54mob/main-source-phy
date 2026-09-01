using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "CatBodyColorConnection", menuName = "SettingsGenerator/Connection/Cat/CatBodyColor")]
	public class CatBodyColorConnectionSO : IntConnectionSO
	{
		protected CatBodyColorConnection _connection;

		public override IConnection<int> GetConnection()
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

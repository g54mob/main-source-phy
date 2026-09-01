using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "CatHatConnection", menuName = "SettingsGenerator/Connection/Cat/CatHat")]
	public class CatHatConnectionSO : IntConnectionSO
	{
		protected CatHatConnection _connection;

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

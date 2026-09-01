using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "CatEyeColorConnection", menuName = "SettingsGenerator/Connection/Cat/CatEyeColor")]
	public class CatEyeColorConnectionSO : IntConnectionSO
	{
		protected CatEyeColorConnection _connection;

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

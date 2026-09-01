using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "CatTypeConnection", menuName = "SettingsGenerator/Connection/Cat/CatType")]
	public class CatTypeConnectionSO : BoolConnectionSO
	{
		protected CatTypeConnection _connection;

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

using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "CatFollowConnection", menuName = "SettingsGenerator/Connection/Cat/CatFollow")]
	public class CatFollowConnectionSO : BoolConnectionSO
	{
		protected CatFollowConnection _connection;

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

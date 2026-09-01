using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "ShadowConnection", menuName = "SettingsGenerator/Connection/ShadowConnection", order = 4)]
	public class ShadowConnectionSO : BoolConnectionSO
	{
		protected ShadowConnection _connection;

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

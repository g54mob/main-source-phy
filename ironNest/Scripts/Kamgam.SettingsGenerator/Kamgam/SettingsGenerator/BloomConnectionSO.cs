using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "BloomConnection", menuName = "SettingsGenerator/Connection/BloomConnection", order = 4)]
	public class BloomConnectionSO : BoolConnectionSO
	{
		protected BloomConnection _connection;

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

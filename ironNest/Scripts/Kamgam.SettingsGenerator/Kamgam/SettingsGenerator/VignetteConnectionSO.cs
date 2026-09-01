using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "VignetteConnection", menuName = "SettingsGenerator/Connection/VignetteConnection", order = 4)]
	public class VignetteConnectionSO : BoolConnectionSO
	{
		protected VignetteConnection _connection;

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

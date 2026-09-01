using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "AmbientOcclusionConnection", menuName = "SettingsGenerator/Connection/AmbientOcclusionConnection", order = 4)]
	public class AmbientOcclusionConnectionSO : BoolConnectionSO
	{
		protected AmbientOcclusionConnection _connection;

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

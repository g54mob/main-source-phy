using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "ShadowResolutionConnection", menuName = "SettingsGenerator/Connection/ShadowResolutionConnection", order = 4)]
	public class ShadowResolutionConnectionSO : OptionConnectionSO
	{
		protected ShadowResolutionConnection _connection;

		public override IConnectionWithOptions<string> GetConnection()
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

using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "AmbientOcclusionHConnection", menuName = "SettingsGenerator/Connection/AmbientOcclusionHConnection", order = 5)]
	public class AmbientOcclusionHConnectionSO : OptionConnectionSO
	{
		protected AmbientOcclusionHConnection _connection;

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

using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "MeshQualityConnection", menuName = "SettingsGenerator/Connection/MeshQualityConnection", order = 5)]
	public class MeshQualityConnectionSO : OptionConnectionSO
	{
		protected MeshQualityConnection _connection;

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

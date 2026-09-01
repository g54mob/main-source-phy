using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "MSAAConnection", menuName = "SettingsGenerator/Connection/MSAAConnection", order = 4)]
	public class MSAAConnectionSO : OptionConnectionSO
	{
		protected MSAAConnection _connection;

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

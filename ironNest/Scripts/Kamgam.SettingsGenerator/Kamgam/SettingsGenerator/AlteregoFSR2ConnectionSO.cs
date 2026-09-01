using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "AlteregoFSR2Connection", menuName = "SettingsGenerator/Connection/AlteregoFSR2Connection", order = 4)]
	public class AlteregoFSR2ConnectionSO : OptionConnectionSO
	{
		protected AlteregoFSR2Connection _connection;

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

using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "NakedDevSGSRConnection", menuName = "SettingsGenerator/Connection/NakedDevSGSRConnection", order = 4)]
	public class NakedDevSGSRConnectionSO : OptionConnectionSO
	{
		protected NakedDevSGSRConnection _connection;

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

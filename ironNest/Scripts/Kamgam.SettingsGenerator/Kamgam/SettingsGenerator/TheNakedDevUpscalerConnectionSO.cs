using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "TheNakedDevUpscalerConnection", menuName = "SettingsGenerator/Connection/TheNakedDevUpscalerConnection", order = 4)]
	public class TheNakedDevUpscalerConnectionSO : OptionConnectionSO
	{
		protected TheNakedDevUpscalerConnection _connection;

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

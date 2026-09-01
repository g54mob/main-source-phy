using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "WindowModePlatformSpecificConnection", menuName = "SettingsGenerator/Connection/WindowModePlatformSpecificConnection", order = 4)]
	public class WindowModePlatformSpecificConnectionSO : OptionConnectionSO
	{
		protected WindowModePlatformSpecificConnection _connection;

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

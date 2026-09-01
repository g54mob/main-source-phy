using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "WindowModeConnection", menuName = "SettingsGenerator/Connection/WindowModeConnection", order = 4)]
	public class WindowModeConnectionSO : OptionConnectionSO
	{
		protected WindowModeConnection _connection;

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

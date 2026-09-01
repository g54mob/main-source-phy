using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "MonitorConnection", menuName = "SettingsGenerator/Connection/MonitorConnection", order = 4)]
	public class MonitorConnectionSO : OptionConnectionSO
	{
		[Tooltip("If set to true then it will try to trigger a refresh of all resolvers depending on display settings.")]
		public bool RefreshResolversAfterCompletion;

		[Tooltip("If enabled then the game will be set to the closest resolution to the current one after monitor change.\n\nWhy is this needed? Default Unity behaviour is to se the game to the fullscreen resolution upon monitor change which may be unexpected.\n\nNOTICE: If the old resolution is greater than the new monitor resolution then the max resolution of the new monitor will be used at avoid windows that are too big (only in windowed mode).")]
		public bool TryToPreserveResolutionOnMonitorChange;

		protected MonitorConnection _connection;

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

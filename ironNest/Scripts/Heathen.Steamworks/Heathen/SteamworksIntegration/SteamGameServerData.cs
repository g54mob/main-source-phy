using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[AddComponentMenu("Steamworks/Game Server")]
	[HelpURL("https://heathen.group/kb/steam-features-authentication/")]
	public class SteamGameServerData : MonoBehaviour
	{
		private GameServerData _mData;

		private SteamGameServerEvents _mEvents;

		public GameServerData Data
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		private void Awake()
		{
		}
	}
}

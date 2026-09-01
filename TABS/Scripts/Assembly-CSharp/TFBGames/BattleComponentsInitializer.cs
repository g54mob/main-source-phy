using Photon.Bolt;
using UnityEngine;

namespace TFBGames
{
	public class BattleComponentsInitializer : MonoBehaviour
	{
		private void Awake()
		{
			InitializeOnlineMultiplayer();
		}

		private void InitializeOnlineMultiplayer()
		{
			if (BoltNetwork.IsRunning)
			{
				base.gameObject.AddComponent<NetworkBattleController>();
				base.gameObject.AddComponent<NetworkUnitsManager>();
				base.gameObject.AddComponent<NetworkProjectilesManager>();
				base.gameObject.AddComponent<PossessedNetworkUnitController>();
				base.gameObject.AddComponent<NetworkDisconnectMonitor>();
				base.gameObject.AddComponent<ProjectMarsBattleUserUIManager>();
				base.gameObject.AddComponent<NetworkBattleUICloser>();
			}
		}
	}
}

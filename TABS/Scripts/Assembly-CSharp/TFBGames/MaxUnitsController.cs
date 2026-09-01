using System.Collections.Generic;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS.Workshop;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TFBGames
{
	public class MaxUnitsController : MonoBehaviour, IService
	{
		private SettingsProfileManager m_settingsProfileManager;

		private NetworkBattleController m_networkBattleController;

		private GameModeService gameModeService;

		public void OnStart()
		{
			m_settingsProfileManager = ServiceLocator.GetService<SettingsProfileManager>();
			gameModeService = ServiceLocator.GetService<GameModeService>();
			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		public void UnRegister()
		{
			SceneManager.sceneLoaded -= OnSceneLoaded;
		}

		public void OnRegister()
		{
		}

		public void OnAwake()
		{
		}

		public void OnUpdate()
		{
		}

		public void OnFixedUpdate()
		{
		}

		public void OnLateUpdate()
		{
		}

		public int? GetMaxUnits()
		{
			if (CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.OnlineMultiplayer)
			{
				return GetMaxUnitsForMultiplayer();
			}
			int? result = null;
			SettingsInstance settingsInstance = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_OVERRIDE_PLACEMENT_LIMIT");
			if ((settingsInstance == null || settingsInstance.currentValue != 1) && m_settingsProfileManager != null && m_settingsProfileManager.CurrentSettingsProfile != null)
			{
				return m_settingsProfileManager.CurrentSettingsProfile.UserPlacementMaxUnits;
			}
			return result;
		}

		public int GetTeamUnitCount(Team team)
		{
			List<TABSCampaignLevelAsset.TABSLayoutUnit> teamLayout = gameModeService.CurrentGameMode.TeamLayouts.GetTeamLayout(team);
			return teamLayout.Count;
		}

		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			m_networkBattleController = ((CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.OnlineMultiplayer) ? ServiceLocator.GetService<NetworkBattleController>() : null);
		}

		private int? GetMaxUnitsForMultiplayer()
		{
			if (m_networkBattleController == null || !m_networkBattleController.DidGetRemoteMaxUnits)
			{
				return 0;
			}
			int? remoteMaxUnits = m_networkBattleController.RemoteMaxUnits;
			int? result = ((m_settingsProfileManager != null && m_settingsProfileManager.CurrentSettingsProfile != null) ? m_settingsProfileManager.CurrentSettingsProfile.CurrentMartians : ((int?)null));
			if (!remoteMaxUnits.HasValue)
			{
				return result;
			}
			if (!result.HasValue)
			{
				return remoteMaxUnits;
			}
			return Mathf.Min(result.Value, remoteMaxUnits.Value);
		}
	}
}

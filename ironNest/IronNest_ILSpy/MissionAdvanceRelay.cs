using UnityEngine;

public class MissionAdvanceRelay : MonoBehaviour
{
	public void EnterBrowsingMap()
	{
		MissionManager missionManager = MissionManager._003CInstance_003Ek__BackingField;
		if (missionManager.autoManageMainMenu)
		{
			missionManager.UnloadMainMenuIfLoaded();
		}
		missionManager.SetPhase(MissionManager.GamePhase.BrowsingMap);
	}

	public void FinishMission()
	{
		MissionManager._003CInstance_003Ek__BackingField.FinishMission();
	}

	public void RestartMission()
	{
		MissionManager missionManager = MissionManager._003CInstance_003Ek__BackingField;
		if (!(missionManager._003CCurrentMission_003Ek__BackingField == null))
		{
			missionManager.LoadMission(missionManager._003CCurrentMission_003Ek__BackingField, true);
		}
		else
		{
			Debug.LogWarning("[MissionManager] No current mission. Cannot reload.");
		}
	}

	public void ExitMission()
	{
		MissionManager missionManager = MissionManager._003CInstance_003Ek__BackingField;
		MissionManager._003CInstance_003Ek__BackingField.UnloadCurrentMissionSceneIfAny();
		if (missionManager._003CCurrentMission_003Ek__BackingField != null)
		{
			missionManager._003CCurrentMission_003Ek__BackingField.OnMissionUnloaded();
			missionManager._003CCurrentMission_003Ek__BackingField = null;
		}
		missionManager._003CCurrentOperation_003Ek__BackingField = null;
		if ((object)MutatorRuntime._003CInstance_003Ek__BackingField != null)
		{
			MutatorRuntime._003CInstance_003Ek__BackingField.ClearActiveMutators();
		}
		if (missionManager.autoManageMainMenu)
		{
			MissionManager._003CInstance_003Ek__BackingField.LoadMainMenu();
		}
	}
}

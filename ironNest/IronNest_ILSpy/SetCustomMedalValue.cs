using UnityEngine;

public class SetCustomMedalValue : MonoBehaviour
{
	public string Key;

	public float Value;

	public void SetValue()
	{
		MissionManager missionManager = MissionManager._003CInstance_003Ek__BackingField;
		MissionManager.MissionState currentMissionState = missionManager.CurrentMissionState;
		currentMissionState.TrackingValues.SetCustomValue(Key, Value);
	}

	public void SetValue(string id)
	{
		MissionManager missionManager = MissionManager._003CInstance_003Ek__BackingField;
		MissionManager.MissionState currentMissionState = missionManager.CurrentMissionState;
		currentMissionState.TrackingValues.SetCustomValue(id, Value);
	}
}

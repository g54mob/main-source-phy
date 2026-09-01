using System.Collections.Generic;
using SleepyNodes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MapCard : MonoBehaviour
{
	[Header("References")]
	public TMP_Text Text_Title;

	public TMP_Text Text_Description;

	[Header("Setup")]
	public OperationGraph Campaign;

	public MissionGraph Mission;

	public List<MissionCardMedalSlot> Medals;

	[Header("Mission Info Broadcast")]
	[Tooltip("The Unity tag used to locate the target GameObject that holds a MissionInfoDisplay component. The target scene must already be loaded additively before PopulateMissionInfo() is called. Must exactly match a tag defined in your project's Tag Manager.\n\nExample values: \"MissionInfoPanel\", \"HUD_MissionDisplay\"")]
	public string TargetTag;

	[Header("Events")]
	public UnityEvent OnState_NotUnlocked;

	public UnityEvent OnState_Unlocked_NotComplete;

	public UnityEvent OnState_Unlocked_Complete;

	public void Init(MissionGraph mission)
	{
	}

	public void PopulateMissionInfo()
	{
	}

	public void ActivateMission()
	{
	}
}

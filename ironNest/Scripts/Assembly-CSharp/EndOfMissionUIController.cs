using System;
using System.Collections.Generic;
using SleepyNodes;
using TMPro;
using UnityEngine;

public class EndOfMissionUIController : MonoBehaviour
{
	[Header("Stats")]
	public TMP_Text Text_MissionName;

	public Transform StatRoot;

	[Header("Medals")]
	public List<MissionCardMedalSlotUI> Medals;

	[Header("Punchcards")]
	public Transform PunchcardRoot;

	public UIPunchcard Prefab_Punchcard;

	[Header("Gif")]
	public Transform GifRoot;

	public UIImageByteCycler GifImageCycler;

	[Header("Other")]
	public GameObject Root_PressKey;

	public GameObject Root_PressKeyProgress;

	public Action<MissionGraph> OnMissionSummaryDismissed;

	private MissionGraph mission;

	private MissionManager.MissionState state;

	private MedalTrackedValues tracker;

	public float MissionTime => 0f;

	public float CounterBatteryTimeRemaining => 0f;

	public int Kills => 0;

	public int TargetKills => 0;

	public int EnemyKills => 0;

	public int AllyKills => 0;

	public int StarsKilled => 0;

	public int ShotsFiredAll => 0;

	public int ShotsFired => 0;

	public int ShotsHit => 0;

	public int STARUsed => 0;

	public float AverageImpactDistanceFromNearestTarget => 0f;

	public float FirstShotTime => 0f;

	public float LastTargetDestroyedTime => 0f;

	public int RequisitionPointsSpent => 0;

	public int ReconUsed => 0;

	public int ReconUsedAfterFirstShot => 0;

	public int LongestKillStreak => 0;

	public int MostKillsBySingleImpact => 0;

	public float BestThreeKillWindowSeconds => 0f;

	public float AccuracyPercent => 0f;

	public float MissPercent => 0f;

	public int ShotsMissed => 0;

	public float KillsPerShot => 0f;

	public float KillsPerHit => 0f;

	public float TargetsPerShot => 0f;

	public float FriendlyFirePercent => 0f;

	public float EnemyKillPercent => 0f;

	public float TargetKillPercent => 0f;

	public float AverageKillsPerImpact => 0f;

	public float AverageStarsPerKill => 0f;

	public float MissionTimeMinutes => 0f;

	public float TimeToFirstShot => 0f;

	public float TimeToLastTargetKill => 0f;

	public float TimeFromFirstShotToLastTargetKill => 0f;

	public float ShotsPerMinute => 0f;

	public float KillsPerMinute => 0f;

	public float RequisitionPerKill => 0f;

	public float RequisitionPerTarget => 0f;

	public float ReconAfterFirstShotPercent => 0f;

	public float STARUsagePercent => 0f;

	public bool PerfectAccuracy => false;

	public bool NoFriendlyFire => false;

	public bool NoReconUsed => false;

	public bool NoSTARUsed => false;

	public int MultiKillShots => 0;

	public int TripleKillShots => 0;

	public void OnEnable()
	{
	}

	public void OnDisable()
	{
	}

	[Button(null)]
	public void Init()
	{
	}

	public void UpdateUI()
	{
	}
}

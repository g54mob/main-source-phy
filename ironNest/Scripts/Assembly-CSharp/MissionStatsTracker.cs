using System;
using UnityEngine;

public class MissionStatsTracker : MonoBehaviour
{
	[Serializable]
	public class Stats
	{
		[Header("Core Mission/Campaign Stats (mission = live round; campaign = accumulated)")]
		public int shotsFired;

		public int targetsDestroyed;

		public int hitsOnTargets;

		public int missedShots;

		public float missionTime;

		[Range(0f, 1f)]
		public float accuracy;

		public int directHits;

		public int hitStreak;

		public int maxHitStreak;
	}

	public static MissionStatsTracker Instance;

	[Header("Mission Stats (Live / resets each mission)")]
	public Stats mission;

	[Header("Campaign Stats (Accumulated across missions except requisition points)")]
	public Stats campaign;

	[Header("Global Requisition Points (single pool)")]
	[Tooltip("Total requisition points the player has (persists across missions).")]
	[SerializeField]
	private int requisitionPoints;

	private ProtectedInt reqPoints;

	private bool requisitionPointsTampered;

	[Header("Summary UI/Console (optional)")]
	public GameObject summaryDisplay;

	[Header("MISSION Odometer Displays (optional)")]
	public OdometerDisplay shotsFiredOdometer_mission;

	public OdometerDisplay targetsDestroyedOdometer_mission;

	public OdometerDisplay hitsOnTargetsOdometer_mission;

	public OdometerDisplay missedShotsOdometer_mission;

	public OdometerDisplay missionTimeOdometer_mission;

	public OdometerDisplay accuracyOdometer_mission;

	public OdometerDisplay directHitsOdometer_mission;

	public OdometerDisplay hitStreakOdometer_mission;

	public OdometerDisplay maxHitStreakOdometer_mission;

	public OdometerDisplay requisitionPointsOdometer_mission;

	[Header("CAMPAIGN Odometer Displays (optional)")]
	public OdometerDisplay shotsFiredOdometer_campaign;

	public OdometerDisplay targetsDestroyedOdometer_campaign;

	public OdometerDisplay hitsOnTargetsOdometer_campaign;

	public OdometerDisplay missedShotsOdometer_campaign;

	public OdometerDisplay missionTimeOdometer_campaign;

	public OdometerDisplay accuracyOdometer_campaign;

	public OdometerDisplay directHitsOdometer_campaign;

	public OdometerDisplay hitStreakOdometer_campaign;

	public OdometerDisplay maxHitStreakOdometer_campaign;

	public OdometerDisplay requisitionPointsOdometer_campaign;

	[Header("Direct Hit Settings")]
	[Tooltip("Distance (in local units) for what counts as a direct hit on a target.")]
	public float directHitRadius;

	[Header("Default Impact Radius (if unavailable from ImpactLocation)")]
	[Tooltip("Used only if the event data does not provide a radius.")]
	public float defaultImpactRadius;

	private bool timerRunning;

	private float timerValue;

	private bool missionEnded;

	public int ShotsFired_Mission => 0;

	public int TargetsDestroyed_Mission => 0;

	public int HitsOnTargets_Mission => 0;

	public int MissedShots_Mission => 0;

	public float MissionTime_Mission => 0f;

	public float Accuracy_Mission => 0f;

	public int DirectHits_Mission => 0;

	public int HitStreak_Mission => 0;

	public int MaxHitStreak_Mission => 0;

	public int ShotsFired_Campaign => 0;

	public int TargetsDestroyed_Campaign => 0;

	public int HitsOnTargets_Campaign => 0;

	public int MissedShots_Campaign => 0;

	public float MissionTime_Campaign => 0f;

	public float Accuracy_Campaign => 0f;

	public int DirectHits_Campaign => 0;

	public int MaxHitStreak_Campaign => 0;

	public int RequisitionPoints => 0;

	public bool RQT => false;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
	}

	public void OnImpact(Vector2 ImpactLocalPosition, float ImpactRadius)
	{
	}

	public void AddRequisitionPoints(int amount, string source = null)
	{
	}

	public void SetRequisitionPoints(int amount, bool inital = true)
	{
	}

	public bool SpendPoints(int amount)
	{
		return false;
	}

	public void EndMission(bool applyBaseFormula = true)
	{
	}

	public void CommitMissionStatsToCampaign()
	{
	}

	private int CalculateBaseMissionRequisitionPoints()
	{
		return 0;
	}

	public void UpdateMissionOdometers()
	{
	}

	public void UpdateCampaignOdometers()
	{
	}
}

using System;
using System.Collections.Generic;

[Serializable]
public class MedalTrackedValues
{
	public class Data_KilledEntity
	{
		public MapEntity Entity;

		public float KilledAtTime;

		public string ShellInstanceId;
	}

	public class Data_ShellFired
	{
		public string ShellInstanceId;

		public ShellDefinition Shell;

		public List<MapEntity> Hits;

		public float? DistanceFromNearestTarget;

		public float ShotAtTime;
	}

	public class Data_PunchcardUsed
	{
		public PunchcardDefinitionV2 Punchcard;

		public float UsedAtTime;
	}

	private const string KillsByRolePrefix = "KillsByRole_";

	private const string KillsByStatePrefix = "KillsByState_";

	public List<Data_KilledEntity> Data_KilledEntities;

	public List<Data_ShellFired> Data_ShellsFired;

	public List<Data_PunchcardUsed> Data_PunchcardsUsed;

	public float MissionStartTime;

	public float MissionCompleteTime;

	public float MissionEndTime;

	public float CounterBatteryTimeRemaining;

	[NonSerialized]
	public Dictionary<string, float> CustomValues;

	public int Kills => 0;

	public int TargetKills => 0;

	public int EnemyKills => 0;

	public int AllyKills => 0;

	public int StarsKilled => 0;

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

	public float GetValue(MedalTrackedValue valueId)
	{
		return 0f;
	}

	public void SetValue(MedalTrackedValue valueId, float value)
	{
	}

	public float GetCustomValue(string key)
	{
		return 0f;
	}

	public void SetCustomValue(string key, float value)
	{
	}

	public void TrackKill(Data_KilledEntity value)
	{
	}

	public void TrackShell(Data_ShellFired value)
	{
	}

	public void TrackPunchcard(Data_PunchcardUsed value)
	{
	}

	public float GetBestXKillsInSeconds(int count)
	{
		return 0f;
	}

	public int GetKillsByRole(EntityRoles role)
	{
		return 0;
	}

	public int GetKillsByState(MapEntityStates state)
	{
		return 0;
	}
}

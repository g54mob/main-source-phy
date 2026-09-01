using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

[JsonConverter(typeof(StringEnumConverter))]
public enum MedalTrackedValue
{
	ShotsFired = 0,
	ShotsHit = 1,
	STARUsed = 2,
	Kills = 3,
	TargetKills = 4,
	EnemyKills = 5,
	AllyKills = 6,
	StarsKilled = 7,
	RequisitionPointsSpent = 8,
	ReconUsed = 9,
	ReconUsedAfterFirstShot = 10,
	AverageImpactDistanceFromNearestTarget = 11,
	MissionStartTime = 12,
	MissionCompleteTime = 13,
	MissionEndTime = 14,
	FirstShotTime = 15,
	LastTargetDestroyedTime = 16,
	CounterBatteryTimeRemaining = 17,
	LongestKillStreak = 18,
	MostKillsBySingleImpact = 19,
	BestThreeKillWindowSeconds = 20
}

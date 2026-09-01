using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Achievements/Trigger/LevelAchievementTrigger")]
internal class LevelAchievementTrigger : AchievementTrigger
{
	internal static Dictionary<int, AchievementTrigger> levelAchievements = new Dictionary<int, AchievementTrigger>();

	public static Action<int> OnLevelAchievement;

	public AudioSource audioSource;

	internal override int AchievementId
	{
		get
		{
			return 0;
		}
	}

	protected virtual int LevelIndex
	{
		get
		{
			return 0;
		}
	}

	protected virtual void Awake()
	{
		AddToLevelChieves();
	}

	protected void AddToLevelChieves()
	{
		levelAchievements.Add(LevelIndex, this);
	}

	protected override void Trigger()
	{
		base.Trigger();
		if (StatMaster.GodTools.HasBeenUsed && StatMaster.Bounding.Enabled)
		{
			if (!Completed() && !audioSource.isPlaying)
			{
				audioSource.Play();
			}
			if (OnLevelAchievement != null)
			{
				OnLevelAchievement(LevelIndex);
			}
			LevelObjectiveFileManager.UpdateCompletedObjectives();
		}
	}

	internal override bool Completed()
	{
		if (AchievementId < 0)
		{
			Debug.LogError("Achievement ID < 0!");
			return false;
		}
		return base.Completed();
	}
}

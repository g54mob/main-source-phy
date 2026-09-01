using System;
using UnityEngine;

[AddComponentMenu("Achievements/Trigger/Level Specific/BurningBirdsAchievement")]
internal class BurningBirdsAchievement : AchievementTrigger
{
	private const int SouthernShrineIndex = 16;

	private SimpleBirdAI[] birdScripts;

	private int fireKills;

	internal override int AchievementId
	{
		get
		{
			return 16;
		}
	}

	public BurningBirdsAchievement()
	{
		Debug.LogError("[Achievement] outdated achievment");
	}

	public override void OnEnterGlobalSimulation(int levelIndex)
	{
		if (levelIndex == 16)
		{
			fireKills = 0;
			BirdFlockController componentInChildren = ReferenceMaster.physicsGoalInstance.GetComponentInChildren<BirdFlockController>();
			birdScripts = componentInChildren.GetComponentsInChildren<SimpleBirdAI>();
			for (int i = 0; i < birdScripts.Length; i++)
			{
				SimpleBirdAI obj = birdScripts[i];
				obj.onFireKill = (Action)Delegate.Combine(obj.onFireKill, new Action(OnFireKill));
			}
		}
	}

	public override void OnExitGlobalSimulation(int levelIndex)
	{
		if (levelIndex == 16)
		{
			for (int i = 0; i < birdScripts.Length; i++)
			{
				SimpleBirdAI obj = birdScripts[i];
				obj.onFireKill = (Action)Delegate.Remove(obj.onFireKill, new Action(OnFireKill));
			}
		}
	}

	private void OnFireKill()
	{
		fireKills++;
	}

	public override void OnSinglePlayerLevelComplete(int levelIndex, float completionTime, Machine machine)
	{
		if (levelIndex != 16)
		{
			return;
		}
		int num = 0;
		for (int i = 0; i < birdScripts.Length; i++)
		{
			if (birdScripts[i].popped)
			{
				num++;
			}
		}
		if (fireKills > num / 2)
		{
			Trigger();
		}
	}
}

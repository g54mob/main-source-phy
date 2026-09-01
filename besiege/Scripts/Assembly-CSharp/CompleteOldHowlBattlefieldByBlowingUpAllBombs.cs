using UnityEngine;

[AddComponentMenu("Achievements/Trigger/LevelSpecific/CompleteOldHowlBattlefieldByBlowingUpAllBombs")]
internal class CompleteOldHowlBattlefieldByBlowingUpAllBombs : LevelAchievementTrigger
{
	private const int OldHowlBattlefieldIndex = 2;

	private ExplodeOnCollide[] bombScripts;

	internal override int AchievementId
	{
		get
		{
			return 18;
		}
	}

	protected override int LevelIndex
	{
		get
		{
			return 2;
		}
	}

	public override void OnEnterGlobalSimulation(int levelIndex)
	{
		if (levelIndex == 2)
		{
			bombScripts = ReferenceMaster.physicsGoalInstance.GetComponentsInChildren<ExplodeOnCollide>();
		}
	}

	public override void OnSinglePlayerLevelComplete(int levelIndex, float completionTime, Machine machine)
	{
		if (levelIndex != 2)
		{
			return;
		}
		for (int i = 0; i < bombScripts.Length; i++)
		{
			if (!bombScripts[i].hasExploded)
			{
				return;
			}
		}
		Trigger();
	}
}

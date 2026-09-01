using UnityEngine;

[AddComponentMenu("Achievements/Trigger/Level Specific/CompleteDukesKnowledgeUnder120Seconds")]
internal class CompleteDukesKnowledgeUnder120Seconds : LevelAchievementTrigger
{
	private const int DukesKnowledgeIndex = 30;

	private const float MaxCompletionTime = 120f;

	internal override int AchievementId
	{
		get
		{
			return 12;
		}
	}

	protected override int LevelIndex
	{
		get
		{
			return 30;
		}
	}

	public CompleteDukesKnowledgeUnder120Seconds()
	{
		Debug.LogError("[Achievement] outdated achievment");
	}

	public override void OnSinglePlayerLevelComplete(int levelIndex, float completionTime, Machine machine)
	{
		if (levelIndex == 30 && completionTime < 120f)
		{
			Trigger();
		}
	}
}

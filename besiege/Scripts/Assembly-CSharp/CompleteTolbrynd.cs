using UnityEngine;

[AddComponentMenu("Achievements/Trigger/CompleteTolbrynd")]
internal class CompleteTolbrynd : CompleteIsland
{
	internal override Island TargetIsland
	{
		get
		{
			return Island.Tolbrynd;
		}
	}

	internal override int AchievementId
	{
		get
		{
			return 20;
		}
	}
}

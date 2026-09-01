using UnityEngine;

[AddComponentMenu("Achievements/Trigger/CompleteTolbrynd")]
internal class CompleteValfross : CompleteIsland
{
	internal override Island TargetIsland
	{
		get
		{
			return Island.Valfross;
		}
	}

	internal override int AchievementId
	{
		get
		{
			return 21;
		}
	}
}

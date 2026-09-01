using UnityEngine;

[AddComponentMenu("Achievements/Trigger/CompleteKrolmar")]
internal class CompleteKrolmar : CompleteIsland
{
	internal override Island TargetIsland
	{
		get
		{
			return Island.Krolmar;
		}
	}

	internal override int AchievementId
	{
		get
		{
			return 31;
		}
	}
}

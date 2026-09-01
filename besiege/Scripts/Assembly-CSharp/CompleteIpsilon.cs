using UnityEngine;

[AddComponentMenu("Achievements/Trigger/CompleteIpsilon")]
internal class CompleteIpsilon : CompleteIsland
{
	internal override Island TargetIsland
	{
		get
		{
			return Island.Ipsilon;
		}
	}

	internal override int AchievementId
	{
		get
		{
			return 19;
		}
	}
}

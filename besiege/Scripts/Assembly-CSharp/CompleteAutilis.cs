using UnityEngine;

[AddComponentMenu("Achievements/Trigger/CompleteAutilis")]
internal class CompleteAutilis : CompleteIsland
{
	internal override Island TargetIsland
	{
		get
		{
			return Island.Water;
		}
	}

	internal override int AchievementId
	{
		get
		{
			return 41;
		}
	}
}

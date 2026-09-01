using UnityEngine;

public class StampFanfareController : MonoBehaviour, ILevelCompletionAnim
{
	public static bool endOfIslandLevel;

	public StampFanfare stampNormal;

	public StampFanfareBig stampBig;

	public AchievementCubeFanfare cubeSkinUnlock;

	public ZoneCompleteFanfare ZoneCompleteFanfareNormal;

	public ZoneCompleteFanfare ZoneCompleteFanfareBig;

	public void LevelCompleted()
	{
		if (endOfIslandLevel)
		{
			stampBig.LevelCompleted();
			ZoneCompleteFanfareBig.StartAnimation();
		}
		else
		{
			stampNormal.LevelCompleted();
			ZoneCompleteFanfareNormal.StartAnimation();
		}
		if ((bool)cubeSkinUnlock)
		{
			cubeSkinUnlock.LevelCompleted();
		}
	}

	public void SetObjectiveParent(Transform objectives)
	{
		objectives.parent = ((!endOfIslandLevel) ? stampNormal.stampObj : stampBig.stamp[0].transform);
	}

	public void LevelReset()
	{
		if (endOfIslandLevel)
		{
			stampBig.LevelReset();
			ZoneCompleteFanfareBig.Disable();
		}
		else
		{
			stampNormal.LevelReset();
			ZoneCompleteFanfareNormal.Disable();
		}
		if ((bool)cubeSkinUnlock)
		{
			cubeSkinUnlock.LevelReset();
		}
	}
}

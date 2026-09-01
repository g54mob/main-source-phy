using UnityEngine;

public class IslandCompleteController : MonoBehaviour
{
	public static bool completedIsland1;

	public static bool completedIsland2;

	public static bool completedIsland3;

	private void Start()
	{
		CheckCompletedIslands();
	}

	private void CheckCompletedIslands()
	{
		if (LEVELLORD.levelsComplete[14] == 1)
		{
			completedIsland1 = true;
		}
		else
		{
			completedIsland1 = false;
		}
	}
}

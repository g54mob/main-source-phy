using UnityEngine;

public class AchievementsManager : MonoBehaviour
{
	[SerializeField]
	private AchievementsService achievementsService;

	public static AchievementsManager Instance;

	private bool initialised;

	public void Start()
	{
	}

	private void Update()
	{
	}

	private void OnApplicationQuit()
	{
	}

	public void RequestSend()
	{
	}

	[ContextMenu("ResetAchievements")]
	private void ResetAchievements()
	{
	}

	public bool IsUnlocked(AchievementType achievementType)
	{
		return false;
	}

	public void WorldMapManager_OnGameSaved()
	{
	}

	public void EventManager_OnAchievementUpdateStat(AchievementUpdateStatEvent e)
	{
	}

	public void EventManager_OnAchievementSetStat(AchievementSetStatEvent e)
	{
	}

	public void EventManager_OnAchievementUnlocked(AchievementUnlockEvent e)
	{
	}
}

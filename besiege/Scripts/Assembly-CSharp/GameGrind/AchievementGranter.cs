using UnityEngine;

namespace GameGrind
{
	[DisallowMultipleComponent]
	public class AchievementGranter : MonoBehaviour
	{
		private BaseAchievementSystem achievementSystem;

		protected void Awake()
		{
			if (SteamManager.Initialized)
			{
				achievementSystem = base.transform.parent.gameObject.AddComponent<SteamAchievementSystem>();
			}
			else
			{
				achievementSystem = GetComponent<AchievementUIPopup>();
			}
			achievementSystem.OnAchievementsLoad();
		}
	}
}

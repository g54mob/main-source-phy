using GameGrind;
using UnityEngine;

[AddComponentMenu("Achievements/BaseAchievementSystem")]
public abstract class BaseAchievementSystem : MonoBehaviour
{
	public static BaseAchievementSystem Instance;

	protected abstract void Awake();

	public abstract void OnAchievementGrant(Achievement achievement);

	public virtual void Init()
	{
		Journal.Create();
		Instance = this;
	}

	public virtual void OnAchievementsLoad()
	{
		Init();
		Journal.Load();
		AchievementEvents.OnAchievementGrant += OnAchievementGrant;
		AchievementEvents.OnAchievementChange += OnAchievementChanged;
	}

	public virtual void OnAchievementChanged(Achievement achievement)
	{
	}
}

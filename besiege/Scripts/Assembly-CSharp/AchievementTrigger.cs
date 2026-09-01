using GameGrind;
using UnityEngine;

[AddComponentMenu("Achievements/Trigger/AchievementTrigger")]
internal abstract class AchievementTrigger : MonoBehaviour, IAchievementTrigger
{
	internal abstract int AchievementId { get; }

	protected virtual void Start()
	{
		if (!SingleInstance<AchievementManager>.hasInstance())
		{
			Object.Destroy(this);
		}
		else
		{
			SingleInstance<AchievementManager>.Instance.Register(this);
		}
	}

	private void Destroy()
	{
		if (!SingleInstance<AchievementManager>.hasInstance())
		{
			SingleInstance<AchievementManager>.Instance.Unregister(this);
		}
	}

	public virtual void OnUpdate(int levelIndex)
	{
	}

	public virtual void OnEnterGlobalSimulation(int levelIndex)
	{
	}

	public virtual void OnExitGlobalSimulation(int levelIndex)
	{
	}

	public virtual void OnSinglePlayerLevelComplete(int levelIndex, float completionTime, Machine machine)
	{
	}

	protected void Increment(int amount)
	{
		Increment(AchievementId, amount);
	}

	protected void Increment(int id, int amount)
	{
		AchievementHelper.Increment(id, amount);
	}

	protected void SetValue(int value)
	{
		SetValue(AchievementId, value);
	}

	protected void SetValue(int id, int value)
	{
		AchievementHelper.SetValue(id, value);
	}

	internal virtual bool Completed()
	{
		return Completed(AchievementId);
	}

	internal bool Completed(int id)
	{
		return AchievementHelper.Completed(id);
	}

	protected virtual void Trigger()
	{
		Increment(1);
	}

	internal int GetProgress()
	{
		return GetProgress(AchievementId);
	}

	internal int GetProgress(int id)
	{
		Achievement achievement = Journal.GetAchievement(id);
		if (achievement == null)
		{
			return -1;
		}
		return achievement.value;
	}
}

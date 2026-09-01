using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Steamworks;
using UnityEngine;

public class AchievementsService : MonoBehaviour
{
	public static AchievementsService Instance;

	protected Callback<UserStatsReceived_t> userStatsReceivedCallback;

	protected Callback<UserStatsStored_t> userStatsStoredCallback;

	protected Callback<UserAchievementStored_t> userAchievementStoredCallback;

	private CGameID gameId;

	private bool storeStatsRequested;

	private bool loadStatsRequested;

	private bool isInitialized;

	private float updateStatsTimer;

	private Dictionary<AchievementType, bool> achievementsState;

	private Dictionary<UserStat, int> statsChangesCache;

	private Dictionary<UserStat, int> statsState;

	private const int RefreshRate = 180;

	private bool requestLocked;

	public event Action<AchievementType> OnAchievementUnlocked
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	public void Initialize()
	{
	}

	private void OnEnable()
	{
	}

	private void Update()
	{
	}

	public void RequestUpdate()
	{
	}

	public void SendChanges()
	{
	}

	public void SendChangesForced()
	{
	}

	public void UpdateStat(UserStat stat, int deltaValue)
	{
	}

	public void SetStat(UserStat stat, int value)
	{
	}

	public void TryUnlockAchievement(AchievementType achievement)
	{
	}

	public bool HasUnlockedAchievement(AchievementType achievement)
	{
		return false;
	}

	public int GetStat(UserStat stat)
	{
		return 0;
	}

	public void ResetAll()
	{
	}

	private void StoreStats()
	{
	}

	private void OnUserStatsReceived(UserStatsReceived_t param)
	{
	}

	private void OnUserStatsStored(UserStatsStored_t param)
	{
	}

	private void OnAchievementStored(UserAchievementStored_t param)
	{
	}
}

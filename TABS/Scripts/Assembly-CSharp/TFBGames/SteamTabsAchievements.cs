using System;
using BitCode.Platform;
using BitCode.Platform.Steamworks;
using BitCode.Users;
using Steamworks;
using UnityEngine;

namespace TFBGames
{
	public class SteamTabsAchievements : ITabsAchievements
	{
		private struct SteamAchievement : ISteamAchievement, IAchievement
		{
			public string AchievementId { get; private set; }

			public bool ShowProgressOverlay => false;

			public int DisplayOverlayInterval => 0;

			public uint MaxProgressValue => 100u;

			public SteamAchievement(string id)
			{
				AchievementId = id;
			}
		}

		private readonly IAchievementManager achievementManager;

		private readonly AccountManager accountManager;

		private AchievementDataServiceAsset achievementDataServiceAsset;

		private ILocalAccount ActiveAccount => accountManager.ActiveAccount;

		public SteamTabsAchievements()
		{
			achievementManager = ServiceLocator.GetService<IPlatformManager>().Services.AchievementManager as SteamAchievementManager;
			achievementDataServiceAsset = ServiceLocator.GetService<AchievementDataServiceAsset>();
			accountManager = ServiceLocator.GetService<AccountManager>();
		}

		public void UnlockAchievement(string id)
		{
			SteamAchievement steamAchievement = new SteamAchievement(id);
			try
			{
				achievementManager.GetAchievementAsync(steamAchievement, accountManager.ActiveAccount, delegate(IAchievement achievement, float progress, bool awarded, Exception exception)
				{
					if (exception != null)
					{
						LogException(exception);
					}
					else
					{
						_ = ((SteamAchievement)(object)achievement).MaxProgressValue;
						achievementManager.UpdateAchievementAsync(achievement, ActiveAccount, 1f, OnUpdatedAchievement);
					}
				});
			}
			catch (SteamApiException e)
			{
				LogException(e);
			}
		}

		public void AdvanceAchievementProgress(string id, int progressAmount)
		{
			if (achievementDataServiceAsset == null)
			{
				return;
			}
			TABSAchievement tabsAchievement = achievementDataServiceAsset.GetAchievementForKey(id);
			if (tabsAchievement == null)
			{
				return;
			}
			string pchName = "API_" + id;
			if (!SteamUserStats.GetStat(pchName, out int pData))
			{
				return;
			}
			int newProgress = pData + progressAmount;
			if (!SteamUserStats.SetStat(pchName, newProgress))
			{
				return;
			}
			if (achievementManager is SteamAchievementManager steamAchievementManager)
			{
				steamAchievementManager.EnqueueStoreStats();
			}
			SteamAchievement steamAchievement = new SteamAchievement(id);
			try
			{
				achievementManager.GetAchievementAsync(steamAchievement, accountManager.ActiveAccount, delegate(IAchievement achievement, float progress, bool awarded, Exception exception)
				{
					if (exception != null)
					{
						LogException(exception);
					}
					else if (newProgress >= tabsAchievement.Data.MaxValue && awarded)
					{
						achievementManager.UpdateAchievementAsync(achievement, ActiveAccount, 1f, OnUpdatedAchievement);
					}
				});
			}
			catch (SteamApiException e)
			{
				LogException(e);
			}
		}

		private void OnUpdatedAchievement(IAchievement achievement, float progress, bool awarded, Exception exception)
		{
			if (exception != null)
			{
				LogException(exception);
			}
		}

		private void LogException(Exception e)
		{
			Debug.LogError(e);
		}

		public void ResetStats()
		{
		}
	}
}

using System;
using Cpp2ILInjected;
using Heathen.SteamworksIntegration.API;
using Steamworks;
using UnityEngine;

public class AchievementsManager : MonoBehaviour
{
	private AchievementsService achievementsService;

	public static AchievementsManager Instance;

	private bool initialised;

	public void Start()
	{
		Instance = this;
		if (App._003CInitialised_003Ek__BackingField)
		{
			if ((object)achievementsService != null)
			{
				achievementsService.Initialize();
				initialised = true;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A7620");
			}
		}
	}

	private void Update()
	{
		if (!initialised && App._003CInitialised_003Ek__BackingField)
		{
			if ((object)achievementsService != null)
			{
				achievementsService.Initialize();
				initialised = true;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A7620");
			}
		}
	}

	private void OnApplicationQuit()
	{
		if (!initialised)
		{
			return;
		}
		AchievementsService achievementsService = this.achievementsService;
		if ((object)this.achievementsService != null)
		{
			if (achievementsService.isInitialized)
			{
				achievementsService.requestLocked = false;
				this.achievementsService.StoreStats();
			}
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A7620");
		}
	}

	public void RequestSend()
	{
		AchievementsService achievementsService = this.achievementsService;
		if (achievementsService.isInitialized)
		{
			achievementsService.storeStatsRequested = true;
		}
	}

	private void ResetAchievements()
	{
		if ((object)achievementsService != null)
		{
			achievementsService.ResetAll();
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A7620");
		}
	}

	public bool IsUnlocked(AchievementType achievementType)
	{
		if ((object)achievementsService != null)
		{
			return achievementsService.HasUnlockedAchievement(achievementType);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A7620");
		bool result = default(bool);
		return result;
	}

	public void WorldMapManager_OnGameSaved()
	{
		AchievementsService achievementsService = this.achievementsService;
		if ((object)this.achievementsService != null)
		{
			if (achievementsService.isInitialized)
			{
				this.achievementsService.StoreStats();
			}
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A7620");
		}
	}

	public unsafe void EventManager_OnAchievementUpdateStat(AchievementUpdateStatEvent e)
	{
		if (e != null)
		{
			AchievementsService achievementsService = this.achievementsService;
			if ((object)this.achievementsService != null)
			{
				if (!achievementsService.isInitialized)
				{
					return;
				}
				if (achievementsService.statsChangesCache != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311C0");
					object obj = default(object);
					UserStat userStat = default(UserStat);
					achievementsService.statsChangesCache.set_Item((UserStat)(int)(&obj), (int)(&userStat));
					return;
				}
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A7620");
	}

	public unsafe void EventManager_OnAchievementSetStat(AchievementSetStatEvent e)
	{
		AchievementsService achievementsService = this.achievementsService;
		if (achievementsService.isInitialized)
		{
			object obj = default(object);
			object obj2 = default(object);
			achievementsService.statsChangesCache.set_Item((UserStat)(int)(&obj), (int)(&obj2));
		}
	}

	public unsafe void EventManager_OnAchievementUnlocked(AchievementUnlockEvent e)
	{
		//IL_0041: Expected O, but got Ref
		AchievementsService achievementsService = this.achievementsService;
		if (achievementsService.isInitialized && !achievementsService.HasUnlockedAchievement(e._003CAchievementType_003Ek__BackingField))
		{
			object obj = default(object);
			string achievement = ((Enum)(&obj)).ToString();
			bool flag = SteamUserStats.SetAchievement(achievement);
			achievementsService.storeStatsRequested = true;
			achievementsService.updateStatsTimer = 0f;
		}
	}
}

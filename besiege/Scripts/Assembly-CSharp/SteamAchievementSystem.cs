using System.Collections;
using GameGrind;
using Steamworks;
using UnityEngine;

[AddComponentMenu("Achievements/SteamAchievementSystem")]
public class SteamAchievementSystem : BaseAchievementSystem
{
	internal string[] achievementNames = new string[56]
	{
		"A_SWIFT_SIEGE",
		"THE_HANDYMAN",
		"THE_FLASH",
		"THE_CREATOR",
		"SHARING_IS_CARING",
		"THE_COPYCAT",
		"THUNDERSTRUCK",
		"IT_WAS_HOT",
		"CARNAGE",
		"PYROMANIAC",
		"RAW_FODDER",
		"PILOTING_101",
		string.Empty,
		"BELL_THING",
		string.Empty,
		"BARELY_STANDING",
		"GRILLED_BIRBS",
		"ANTI_AIRCRAFT",
		"BOMB_BATTLEFIELD",
		"LORD_OF_THE_LYRE",
		"THE_HIGH_DUKE",
		"MONARCH_OF_FROST",
		"RUBE_GOLDBERG",
		string.Empty,
		string.Empty,
		string.Empty,
		"THROUGH_THROUGH",
		string.Empty,
		"DODGER",
		"TREE_HUGGER",
		"ATLAS_CHALLENGE",
		"EMPEROR_OF_SAND",
		"CONQUEROR",
		"MULTIVERSAL",
		"DEMOLITION_EXPERT",
		"FREEZING_FRONTIER",
		"UP_HILL_STRUGGLE",
		"FROZEN_GOODS",
		"IRON_WEAVER",
		"WHERES_WOOLLY",
		"SWORD_BUSTER",
		"MASTER_OF_TIDES",
		"COLD_AS_ICE",
		"TARGET_PRACTICE",
		"SHELL_SHOCK",
		"SHIP_CARGO",
		"KROLMAR_CRATES",
		"CAGED_SHARK",
		"MINE_CHESTS",
		"HIDDEN_SHRINE",
		"SPAWN_CAMP",
		"COMPLETIONIST",
		"COMPLETIONIST_SS",
		"NEGOTIATIONS",
		"NONE_ALIVE",
		"MORTISSIMO"
	};

	private readonly string[] statNames = new string[9] { "AI_KILLED", "IPS_PROGRESS", "TOL_PROGRESS", "VAL_PROGRESS", "KROL_PROGRESS", "AUT_PROGRESS", "BIRDS_BURNED", "CAMPAIGN_SECONDARIES", "WATER_SECONDARIES" };

	private IEnumerator storeCoroutine;

	public override void OnAchievementsLoad()
	{
		Init();
		Journal.Load();
	}

	public void ProcessStats(UserStatsReceived_t stats)
	{
		Debug.Log("[SteamAchievementSystem]: Process Stats: " + stats.m_eResult);
		if (SteamUserStats.GetNumAchievements() == 0)
		{
			Debug.LogError("[SteamAchievementSystem]: missing all steam achievement");
			AchievementEvents.OnAchievementGrant += OnAchievementGrant;
			AchievementEvents.OnAchievementChange += OnAchievementChanged;
			return;
		}
		for (uint num = 0u; num < achievementNames.Length; num++)
		{
			bool flag = false;
			string text = achievementNames[num];
			if (string.IsNullOrEmpty(text))
			{
				continue;
			}
			bool pbAchieved;
			if (!SteamUserStats.GetAchievement(text, out pbAchieved))
			{
				Debug.LogError("[SteamAchievementSystem]: missing steam achievement: " + text);
				continue;
			}
			Achievement achievement = Journal.GetAchievement((int)num);
			if (achievement == null)
			{
				Debug.LogError("[SteamAchievementSystem]: missing local achievement: " + num);
				continue;
			}
			string text2 = string.Empty;
			switch (text)
			{
			case "CARNAGE":
				text2 = statNames[0];
				break;
			case "LORD_OF_THE_LYRE":
				text2 = statNames[1];
				break;
			case "THE_HIGH_DUKE":
				text2 = statNames[2];
				break;
			case "MONARCH_OF_FROST":
				text2 = statNames[3];
				break;
			case "EMPEROR_OF_SAND":
				text2 = statNames[4];
				break;
			case "MASTER_OF_TIDES":
				text2 = statNames[5];
				break;
			case "GRILLED_BIRBS":
				text2 = statNames[6];
				break;
			case "COMPLETIONIST":
				text2 = statNames[7];
				break;
			case "COMPLETIONIST_SS":
				text2 = statNames[8];
				break;
			}
			bool flag2 = !string.IsNullOrEmpty(text2);
			int pData;
			if (!flag2)
			{
				pData = (pbAchieved ? 1 : 0);
			}
			else if (!SteamUserStats.GetStat(text2, out pData))
			{
				if (BesiegeLogFilter.logDebug)
				{
					Debug.Log("Couldn't find stat " + text2 + "!");
				}
				flag2 = false;
			}
			int num2 = pData;
			if (achievement.value > pData)
			{
				pData = achievement.value;
			}
			if (pData >= achievement.neededValue || pbAchieved || achievement.completed)
			{
				pData = achievement.neededValue;
				flag = true;
			}
			if (num2 != pData && flag2)
			{
				SteamUserStats.SetStat(text2, pData);
			}
			if (achievement.value != pData)
			{
				Journal.SetValue(achievement, pData, false);
			}
			if (!pbAchieved && flag)
			{
				SteamUserStats.SetAchievement(text);
				pbAchieved = true;
			}
		}
		AchievementEvents.OnAchievementGrant += OnAchievementGrant;
		AchievementEvents.OnAchievementChange += OnAchievementChanged;
	}

	public override void OnAchievementChanged(Achievement achievement)
	{
		int num = -1;
		switch (achievement.id)
		{
		case 8:
			num = 0;
			break;
		case 16:
			num = 6;
			break;
		case 19:
			num = 1;
			break;
		case 20:
			num = 2;
			break;
		case 21:
			num = 3;
			break;
		case 31:
			num = 4;
			break;
		case 41:
			num = 5;
			break;
		case 51:
			num = 7;
			break;
		case 52:
			num = 8;
			break;
		}
		if (num != -1 && SteamUserStats.SetStat(statNames[num], achievement.value))
		{
			StoreAchievements();
		}
	}

	private void StoreAchievements()
	{
		if (storeCoroutine == null)
		{
			storeCoroutine = IEStoreAchievements();
			StartCoroutine(storeCoroutine);
		}
	}

	private IEnumerator IEStoreAchievements()
	{
		yield return new WaitForSeconds(0.5f);
		if (SteamManager.Initialized)
		{
			SteamUserStats.StoreStats();
		}
		storeCoroutine = null;
	}

	public override void OnAchievementGrant(Achievement achievement)
	{
		if (!SteamManager.Initialized)
		{
			return;
		}
		int id = achievement.id;
		if (id >= 0 && id < achievementNames.Length)
		{
			string text = achievementNames[id];
			if (!string.IsNullOrEmpty(text))
			{
				SteamUserStats.SetAchievement(text);
				StoreAchievements();
			}
		}
		else
		{
			Debug.LogError("Couldn't grant achievement " + achievement.title + " (id=" + achievement.id + ") since it hasn't been added to SteamAchievementSystem!");
		}
	}

	protected override void Awake()
	{
		BaseAchievementSystem.Instance = this;
	}
}

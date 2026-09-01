using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.Serialization;
using Steamworks;
using Steamworks.Ugc;
using UnityEngine;

public class Workshop
{
	public static Dictionary<string, SteamItemInfo> m_SubscribedItems = new Dictionary<string, SteamItemInfo>();

	public static HashSet<SteamId> m_SteamIdsWithRequestedInfo = new HashSet<SteamId>();

	public static readonly string LEVEL_LAYOUT_FILENAME = "steam_workshop_level.layout";

	public static readonly string LEVEL_PREVIEW_FILENAME = "steam_workshop_preview.png";

	public static readonly int TITLE_CHAR_LIMIT = 64;

	public static readonly int DESCRIPTION_CHAR_LIMIT = 2048;

	public static WorkshopItem m_LastPlayedWorkshopItem;

	public static string m_ForceWorkshopID;

	private static readonly string SUBSCRIBED_ITEMS_FILENAME = ".subscribeditems";

	public static void Init()
	{
		if (SteamManager.IsLoggedOn())
		{
			DownloadSubscribedItems();
			return;
		}
		RemoveOrphanModsFromProfile();
		RemoveCampaignModsFromProfile();
		Mods.SetActiveModsFromProfile();
		Mods.LoadModsFromProfile(null);
	}

	public static void AddToSubscribedItems(SteamItemInfo steamItemInfo)
	{
		if (!string.IsNullOrEmpty(steamItemInfo.m_ID) && !m_SubscribedItems.ContainsKey(steamItemInfo.m_ID) && Directory.Exists(steamItemInfo.m_InstallPath))
		{
			m_SubscribedItems.Add(steamItemInfo.m_ID, steamItemInfo);
		}
	}

	public static void RemoveFromSubscribedItems(string id)
	{
		if (!string.IsNullOrEmpty(id) && m_SubscribedItems.ContainsKey(id))
		{
			m_SubscribedItems.Remove(id);
		}
	}

	public static SteamItemInfo GetSubscibedItem(string id)
	{
		if (string.IsNullOrEmpty(id) || !m_SubscribedItems.ContainsKey(id))
		{
			return null;
		}
		return m_SubscribedItems[id];
	}

	public static bool PlayLevel(WorkshopItem item, string layoutPath, GameSubMode submode)
	{
		SandboxLayoutData sandboxLayoutData = SandboxLayout.Load(layoutPath);
		if (sandboxLayoutData == null)
		{
			return false;
		}
		GameStateManager.SwitchToStateImmediate(GameState.LOADING_LEVEL_IMMEDIATE);
		bool num = LoadLevelFromLayout(sandboxLayoutData, item.GetId(), item.IsAutoPlay());
		if (num)
		{
			Game.AddLevelChecksum(item.GetId(), sandboxLayoutData.GenerateChecksum());
			m_LastPlayedWorkshopItem = item;
			SandboxSettings.m_Title = m_LastPlayedWorkshopItem.GetTitle();
			SandboxSettings.m_Description = m_LastPlayedWorkshopItem.GetDescription();
			GameManager.SetGameMode(GameMode.WORKSHOP, submode);
			Campaign.m_CurrentLevel = null;
			GameStateManager.SwitchToState(GameState.BUILD);
			Prefabs.m_Instance.UnloadAssetsNotInLayout(layoutPath);
			WorkshopRecentlyPlayed.SaveLevel(item.GetId());
		}
		return num;
	}

	public static bool LoadLevelFromLayout(SandboxLayoutData sandboxLayoutData, string levelId, bool loadBridge)
	{
		if (sandboxLayoutData == null)
		{
			Debug.LogWarningFormat("Could not load Workshop item: {0}", levelId);
			return false;
		}
		CampaignWorld worldWithLevelId = CampaignWorlds.m_Instance.GetWorldWithLevelId(levelId);
		string text = ((worldWithLevelId != null) ? worldWithLevelId.m_ThemePreloadStub.m_ID : sandboxLayoutData.m_ThemeStubId);
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		BridgeSaveSlots.ClearSlots();
		BridgeSaveSlots.LoadSlots(Utils.GenerateCaseInsenstiveString(levelId), Profiles.GetActiveProfileName());
		Sandbox.Clear();
		BridgeSaveSlotData autoSave = BridgeSaveSlots.GetAutoSave();
		Sandbox.Load(text, sandboxLayoutData, loadBridge);
		PointsOfView.OnLayoutLoaded(levelId);
		SandboxUndo.Clear();
		SandboxUndo.SnapShot();
		if (!loadBridge && Profiles.m_ActiveProfile.m_AutomatiallyLoadAutoSave && autoSave != null)
		{
			BridgeSaveData bridgeSaveData = Bridge.ClearAndLoadBinary(autoSave.m_Bridge);
			if (bridgeSaveData != null)
			{
				BridgeCheat.CheckForCheating(Sandbox.m_CurrentLayoutData, bridgeSaveData, levelId);
				Bridge.Sanitize();
			}
			Budget.MaybeApplyForcedBudgets(autoSave.m_UsingUnlimitedBudget, autoSave.m_UsingUnlimitedMaterials);
		}
		return true;
	}

	public static string GetLocalPlayerId()
	{
		return SteamUtils.GetSteamId();
	}

	public static string GetLocalPlayerDisplayName()
	{
		return SteamUtils.GetLocalSteamDisplayName();
	}

	public static void LoadSubscribedItemsFromDisk()
	{
		string fullPath = Path.Combine(Application.persistentDataPath, SUBSCRIBED_ITEMS_FILENAME);
		if (!Utils.FileExists(fullPath))
		{
			fullPath = Path.Combine(Profiles.GetProfileRootDirectory(), SUBSCRIBED_ITEMS_FILENAME);
		}
		if (!Utils.FileExists(fullPath))
		{
			return;
		}
		byte[] array = Utils.ReadAllBytes(fullPath);
		if (array != null && array.Length != 0)
		{
			try
			{
				m_SubscribedItems = SerializationUtility.DeserializeValue<Dictionary<string, SteamItemInfo>>(array, DataFormat.JSON);
			}
			catch (Exception ex)
			{
				Debug.LogWarning("Exception parsing " + SUBSCRIBED_ITEMS_FILENAME + ": " + ex.Message);
			}
		}
	}

	public static void SaveSubscribedItemsToDisk()
	{
		byte[] bytes = SerializationUtility.SerializeValue(m_SubscribedItems, DataFormat.JSON);
		Utils.WriteBytes(Path.Combine(Application.persistentDataPath, SUBSCRIBED_ITEMS_FILENAME), bytes);
	}

	public static string GetForceWorkshopIDWarningMessage()
	{
		return "Upload will " + GameUI.MarkupForGold("force") + " use of workshop ID " + GameUI.MarkupForGold(m_ForceWorkshopID) + "\n(You can disable this with " + GameUI.MarkupForGold("force_worshop_id_clear") + ")\n\nAre you sure you want to upload?";
	}

	public static bool IsUserUGCQuery(WorkshopSortOrder sortOrder)
	{
		if (sortOrder != WorkshopSortOrder.BY_NAME && sortOrder != WorkshopSortOrder.SUBSCRIBED_BY_ME && sortOrder != WorkshopSortOrder.CREATED_BY_ME)
		{
			return sortOrder == WorkshopSortOrder.FAVORITED_BY_ME;
		}
		return true;
	}

	public static bool TextIsWorkshopID(string text)
	{
		if (ulong.TryParse(text, out var _))
		{
			return text.Length >= 9;
		}
		return false;
	}

	private static async void DownloadSubscribedItems()
	{
		int i = 1;
		while (true)
		{
			ResultPage? resultPage = await Query.All.WhereUserSubscribed().WithMetadata(b: true).WithLongDescription(b: true)
				.GetPageAsync(i);
			if (!resultPage.HasValue || !resultPage.HasValue || resultPage.Value.ResultCount == 0)
			{
				break;
			}
			if (i == 1)
			{
				m_SubscribedItems.Clear();
			}
			foreach (Item entry in resultPage.Value.Entries)
			{
				if (entry.Result == Result.OK)
				{
					AddToSubscribedItems(new SteamItemInfo(entry));
				}
			}
			i++;
		}
		SaveSubscribedItemsToDisk();
		if (Profiles.m_ActiveProfile != null)
		{
			RemoveOrphanModsFromProfile();
			RemoveCampaignModsFromProfile();
			Mods.SetActiveModsFromProfile();
			Mods.LoadModsFromProfile(null);
		}
	}

	private static void RemoveOrphanModsFromProfile()
	{
		List<string> list = new List<string>();
		foreach (string activeModDirectory in Profiles.m_ActiveProfile.m_ActiveModDirectories)
		{
			if (string.IsNullOrEmpty(Mods.GetPathToMod(activeModDirectory)))
			{
				list.Add(activeModDirectory);
			}
		}
		foreach (string item in list)
		{
			Profiles.m_ActiveProfile.m_ActiveModDirectories.Remove(item);
		}
		if (Profiles.m_ActiveProfile.m_ActiveModDirectories.RemoveAll((string item) => item == null) > 0 || list.Count > 0)
		{
			Profiles.SaveActiveProfile();
		}
	}

	private static void RemoveCampaignModsFromProfile()
	{
		List<string> list = new List<string>();
		foreach (string activeModDirectory in Profiles.m_ActiveProfile.m_ActiveModDirectories)
		{
			string pathToMod = Mods.GetPathToMod(activeModDirectory);
			if (!string.IsNullOrEmpty(pathToMod) && ModApi.CheckForWorkshopCampaignFunctions(Mods.GetLuaFilesInMod(pathToMod)))
			{
				list.Add(activeModDirectory);
			}
		}
		foreach (string item in list)
		{
			Profiles.m_ActiveProfile.m_ActiveModDirectories.Remove(item);
		}
		if (list.Count > 0)
		{
			Profiles.SaveActiveProfile();
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.Serialization;
using Steamworks;
using Steamworks.Data;
using Steamworks.Ugc;
using UnityEngine;
using UnityEngine.Networking;

public class WeeklyChallenges
{
	public static List<WeeklyChallengeStub> m_Stubs = new List<WeeklyChallengeStub>();

	public static readonly int NUM_CHALLENGES_PER_SEASON = 10;

	private static Dictionary<string, WorkshopItem> m_Items = new Dictionary<string, WorkshopItem>();

	private static readonly string STUBS_FILENAME = "manifest";

	private static Dictionary<string, SteamId> m_OriginalCreators = new Dictionary<string, SteamId>();

	private static Action<bool> m_Callback;

	public static void Download()
	{
		if (m_Stubs.Count == 0)
		{
			LoadStubsLocal();
		}
		DownloadStubsAsync(OnLoadStubsRemoteComplete);
	}

	public static void UpdateManual()
	{
	}

	public static async void BatchDownloadWorkshopItems(List<string> itemIds, int week, Action<bool, int> callback)
	{
		if (itemIds.Count == 0)
		{
			return;
		}
		PublishedFileId[] array = new PublishedFileId[itemIds.Count];
		for (int i = 0; i < itemIds.Count; i++)
		{
			array[i] = default(PublishedFileId);
			ulong.TryParse(itemIds[i], out var result);
			array[i].Value = result;
		}
		ResultPage? resultPage = await Query.All.WithFileId(array).WithMetadata(b: true).WithLongDescription(b: true)
			.GetPageAsync(1);
		if (!resultPage.HasValue)
		{
			callback?.Invoke(arg1: false, week);
			return;
		}
		foreach (Item entry in resultPage.Value.Entries)
		{
			if (entry.Result == Result.OK)
			{
				string text = entry.Id.Value.ToString();
				if (m_Items.ContainsKey(text))
				{
					m_Items[text].m_SteamItem = entry;
				}
				else
				{
					WorkshopItem value = new WorkshopItem(entry);
					m_Items.Add(text, value);
				}
				RequestUserInformation(text);
			}
		}
		callback?.Invoke(arg1: true, week);
	}

	public static void RequestUserInformation(string itemID)
	{
		if (!GameManager.IsSteamOffline() && !m_OriginalCreators.ContainsKey(itemID))
		{
			WeeklyChallengeStub weeklyChallengeStubByItemId = GetWeeklyChallengeStubByItemId(itemID);
			if (weeklyChallengeStubByItemId != null && !string.IsNullOrEmpty(weeklyChallengeStubByItemId.m_AuthorSteamID))
			{
				SteamId value = SteamUtils.SteamIdFromString(weeklyChallengeStubByItemId.m_AuthorSteamID);
				m_OriginalCreators.Add(itemID, value);
				SteamPersonas.RequestUserInfo(weeklyChallengeStubByItemId.m_AuthorSteamID);
			}
		}
	}

	public static WorkshopItem GetWeeklyChallengeByItemId(string itemId)
	{
		if (m_Items.ContainsKey(itemId))
		{
			return m_Items[itemId];
		}
		return null;
	}

	public static WeeklyChallengeStub GetWeeklyChallengeStubByItemId(string itemId)
	{
		foreach (WeeklyChallengeStub stub in m_Stubs)
		{
			if (stub.m_ItemID == itemId)
			{
				return stub;
			}
		}
		return null;
	}

	public static WorkshopItem GetMostRecentWeeklyChallenge()
	{
		return GetWeeklyChallenge(GetWeekWithMostRecentChallenge());
	}

	public static WorkshopItem GetWeeklyChallenge(int week)
	{
		foreach (WeeklyChallengeStub stub in m_Stubs)
		{
			if (stub.m_Week == week && m_Items.ContainsKey(stub.m_ItemID))
			{
				return m_Items[stub.m_ItemID];
			}
		}
		return null;
	}

	public static WeeklyChallengeStub GetWeeklyChallengeStub(int week)
	{
		foreach (WeeklyChallengeStub stub in m_Stubs)
		{
			if (stub.m_Week == week)
			{
				return stub;
			}
		}
		return null;
	}

	public static List<string> GetWeeklyChallengeIdsForSeason(int season)
	{
		List<string> list = new List<string>();
		foreach (WeeklyChallengeStub stub in m_Stubs)
		{
			if (season == GetSeasonForWeek(stub.m_Week) && !list.Contains(stub.m_ItemID))
			{
				list.Add(stub.m_ItemID);
			}
		}
		return list;
	}

	public static int GetWeekWithMostRecentChallenge()
	{
		return GetHighestWeekFromStubs();
	}

	public static List<string> GetAllWeeklyChallengeIds()
	{
		List<string> list = new List<string>();
		foreach (WeeklyChallengeStub stub in m_Stubs)
		{
			if (!list.Contains(stub.m_ItemID))
			{
				list.Add(stub.m_ItemID);
			}
		}
		return list;
	}

	public static bool IsAWeeklyChallenge(string levelID)
	{
		foreach (WeeklyChallengeStub stub in m_Stubs)
		{
			if (stub.m_ItemID == levelID)
			{
				return true;
			}
		}
		return false;
	}

	public static bool IsALiveWeeklyChallenge(string itemId)
	{
		int currentWeek = GetCurrentWeek();
		foreach (WeeklyChallengeStub stub in m_Stubs)
		{
			if (stub.m_ItemID == itemId && stub.m_Week == currentWeek)
			{
				return true;
			}
		}
		return false;
	}

	public static int GetCurrentWeek()
	{
		return GetHighestWeekFromStubs();
	}

	public static int GetWeekForLevel(string levelID)
	{
		foreach (WeeklyChallengeStub stub in m_Stubs)
		{
			if (stub.m_ItemID == levelID)
			{
				return stub.m_Week;
			}
		}
		return 0;
	}

	public static string GetOriginalCreatorDisplayName(string id)
	{
		if (!m_OriginalCreators.ContainsKey(id))
		{
			return string.Empty;
		}
		return SteamPersonas.GetDisplayName(m_OriginalCreators[id]);
	}

	public static int GetSeasonForWeek(int week)
	{
		return Mathf.CeilToInt((float)week / (float)NUM_CHALLENGES_PER_SEASON);
	}

	public static int GetSeasonNumber(string itemID)
	{
		return GetSeasonForWeek(GetWeekForLevel(itemID));
	}

	public static int GetWeekWithinSeasonForItem(string itemID)
	{
		return GetWeekWithinSeason(GetWeekForLevel(itemID));
	}

	public static int GetWeekWithinSeason(int week)
	{
		int num = week % NUM_CHALLENGES_PER_SEASON;
		if (num != 0)
		{
			return num;
		}
		return NUM_CHALLENGES_PER_SEASON;
	}

	public static void DownloadStubsAsync(Action<bool> callback)
	{
		m_Callback = callback;
		UnityWebRequest unityWebRequest = UnityWebRequest.Get(Game.AMAZON_S3_URL + "weeklies/manifest");
		unityWebRequest.timeout = Game.DOWNLOAD_TIMEOUT_SECONDS;
		unityWebRequest.useHttpContinue = false;
		unityWebRequest.SendWebRequest().completed += DownloadStubsComplete;
	}

	public static int GetNumberPassedWeeksInSeason(int season)
	{
		int num = 0;
		foreach (WeeklyChallengeStub stub in m_Stubs)
		{
			if (GetSeasonForWeek(stub.m_Week) == season && WeeklyChallengesProgress.HasCompletedLevel(stub.m_ItemID))
			{
				num++;
			}
		}
		return num;
	}

	public static int GetNumberWeeksInSeason(int season)
	{
		int num = 0;
		foreach (WeeklyChallengeStub stub in m_Stubs)
		{
			if (GetSeasonForWeek(stub.m_Week) == season)
			{
				num++;
			}
		}
		return num;
	}

	public static int GetBudget(string levelId)
	{
		WorkshopItem weeklyChallengeByItemId = GetWeeklyChallengeByItemId(levelId);
		if (weeklyChallengeByItemId == null)
		{
			return 0;
		}
		return GetBudgetFromEncodedDescription(weeklyChallengeByItemId.GetDescription());
	}

	public static string GetDescriptionFromEncodedDescription(string description)
	{
		int num = description.IndexOf('_');
		if (num >= 0)
		{
			return description.Substring(num + 1);
		}
		return string.Empty;
	}

	public static int GetBudgetFromEncodedDescription(string description)
	{
		int num = description.IndexOf('_');
		if (num < 0)
		{
			return 0;
		}
		string text = description.Substring(0, num);
		if (string.IsNullOrEmpty(text))
		{
			return 0;
		}
		return WorkshopMetaData.GetBudget(text);
	}

	public static List<int> GetMaterialCountsFromEncodedDescription(string description)
	{
		string metaDataFromEncodedDescription = GetMetaDataFromEncodedDescription(description);
		if (string.IsNullOrEmpty(metaDataFromEncodedDescription))
		{
			return null;
		}
		return WorkshopMetaData.GetMaterialCounts(metaDataFromEncodedDescription);
	}

	public static string GetMetaDataFromEncodedDescription(string description)
	{
		int num = description.IndexOf('_');
		if (num >= 0)
		{
			return description.Substring(0, num);
		}
		return string.Empty;
	}

	private static void LoadStubsLocal()
	{
		string stubsLocalFullPath = GetStubsLocalFullPath();
		byte[] array = Utils.ReadAllBytes(stubsLocalFullPath);
		if (array == null || array.Length == 0)
		{
			return;
		}
		try
		{
			List<WeeklyChallengeStub> list = SerializationUtility.DeserializeValue<List<WeeklyChallengeStub>>(array, DataFormat.JSON);
			if (list != null)
			{
				m_Stubs = list;
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Exception parsing " + stubsLocalFullPath + ": " + ex.Message);
		}
	}

	private static void OnLoadStubsRemoteComplete(bool success)
	{
		if (success && GameStateManager.GetState() == GameState.MAIN_MENU)
		{
			GameUI.m_Instance.m_MainMenuNew.ForceWeeklyChallengeRefresh();
		}
	}

	private static void DownloadStubsComplete(AsyncOperation asyncOperation)
	{
		UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = (UnityWebRequestAsyncOperation)asyncOperation;
		if (unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.LogWarningFormat("Download weekly challenge manifest failed with error '{0}'", unityWebRequestAsyncOperation.webRequest.error);
			LoadStubsLocal();
			m_Callback?.Invoke(obj: false);
		}
		else if (unityWebRequestAsyncOperation.webRequest.downloadHandler != null && unityWebRequestAsyncOperation.webRequest.downloadHandler.text != null)
		{
			string text = Path.Combine(Application.persistentDataPath, STUBS_FILENAME);
			try
			{
				CreateStubsFromRawText(unityWebRequestAsyncOperation.webRequest.downloadHandler.text);
				m_Callback?.Invoke(obj: true);
			}
			catch (Exception ex)
			{
				Debug.LogWarning("Failed to write: " + text + " due to " + ex.Message);
				m_Callback?.Invoke(obj: false);
			}
			SaveStubsLocal();
		}
	}

	private static void CreateStubsFromRawText(string text)
	{
		m_Stubs.Clear();
		string[] array = text.Split('\n');
		foreach (string text2 in array)
		{
			if (!string.IsNullOrEmpty(text2))
			{
				string[] array2 = text2.Split();
				if (array2.Length == 3 && int.TryParse(array2[2], out var result))
				{
					m_Stubs.Add(new WeeklyChallengeStub(array2[0], array2[1], result));
				}
			}
		}
		m_Stubs.Sort(SortByWeek);
	}

	private static int SortByWeek(WeeklyChallengeStub a, WeeklyChallengeStub b)
	{
		return a.m_Week.CompareTo(b.m_Week);
	}

	private static void SaveStubsLocal()
	{
		if (m_Stubs.Count > 0)
		{
			byte[] bytes = SerializationUtility.SerializeValue(m_Stubs, DataFormat.JSON);
			Utils.WriteBytes(GetStubsLocalFullPath(), bytes);
		}
	}

	private static string GetStubsLocalFullPath()
	{
		return Path.Combine(Application.persistentDataPath, STUBS_FILENAME);
	}

	private static int GetHighestWeekFromStubs()
	{
		int num = 0;
		foreach (WeeklyChallengeStub stub in m_Stubs)
		{
			if (stub.m_Week > num)
			{
				num = stub.m_Week;
			}
		}
		return num;
	}
}

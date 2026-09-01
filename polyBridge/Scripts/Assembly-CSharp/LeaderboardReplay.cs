using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class LeaderboardReplay
{
	private static bool m_Active;

	private static string m_LevelId;

	private static string m_SteamId;

	private static string m_Name;

	private static int m_Score;

	private static string m_LeaderboardKey;

	private static byte[] m_SaveBytes;

	private static readonly string KEYS_FILENAME = "keys";

	private static string AES_key;

	private static string AES_iv;

	public static void Init()
	{
		string text = Path.Combine(Application.persistentDataPath, KEYS_FILENAME);
		if (Utils.FileExists(text))
		{
			StreamReader streamReader = new StreamReader(text);
			AES_key = streamReader.ReadLine();
			AES_iv = streamReader.ReadLine();
			streamReader.Close();
		}
		m_LevelId = string.Empty;
		m_SteamId = string.Empty;
		m_LeaderboardKey = string.Empty;
	}

	public static void SetActive(bool active)
	{
		m_Active = active;
	}

	public static bool IsActive()
	{
		return m_Active;
	}

	public static string GetSteamId()
	{
		return m_SteamId;
	}

	public static string GetLevelId()
	{
		return m_LevelId;
	}

	public static string GetName()
	{
		return m_Name;
	}

	public static string GetLeaderboardKey()
	{
		return m_LeaderboardKey;
	}

	public static string GetFormattedScore()
	{
		if (m_LeaderboardKey.EndsWith("stress"))
		{
			return Utils.FormatStress((float)m_Score / 100f);
		}
		return Utils.FormatCash(m_Score);
	}

	public static void Run(string levelId, string steamId, string name, string leaderboardKey, int score)
	{
		PopUpMessage.DisplayLoading("Downloading save...");
		m_LevelId = levelId;
		m_SteamId = steamId;
		m_Name = name;
		m_LeaderboardKey = leaderboardKey;
		m_Score = score;
		DownloadAsync(Path.Combine(Game.CLOUDFLARE_LEADERBOARDS_URL, leaderboardKey + "-" + steamId + ".bin"));
	}

	public static void DownloadAsync(string url)
	{
		UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);
		unityWebRequest.timeout = Game.DOWNLOAD_TIMEOUT_SECONDS;
		unityWebRequest.useHttpContinue = false;
		unityWebRequest.SendWebRequest().completed += DownloadComplete;
	}

	public static string AES_Encrypt(string input)
	{
		AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider();
		aesCryptoServiceProvider.BlockSize = 128;
		aesCryptoServiceProvider.KeySize = 256;
		aesCryptoServiceProvider.Key = Encoding.ASCII.GetBytes(AES_key);
		aesCryptoServiceProvider.IV = Encoding.ASCII.GetBytes(AES_iv);
		aesCryptoServiceProvider.Mode = CipherMode.CBC;
		aesCryptoServiceProvider.Padding = PaddingMode.PKCS7;
		byte[] bytes = Encoding.ASCII.GetBytes(input);
		return Convert.ToBase64String(aesCryptoServiceProvider.CreateEncryptor(aesCryptoServiceProvider.Key, aesCryptoServiceProvider.IV).TransformFinalBlock(bytes, 0, bytes.Length));
	}

	public static byte[] AES_Decrypt(byte[] input)
	{
		return new AesCryptoServiceProvider
		{
			BlockSize = 128,
			KeySize = 256,
			Key = Encoding.ASCII.GetBytes(AES_key),
			IV = Encoding.ASCII.GetBytes(AES_iv),
			Mode = CipherMode.CBC,
			Padding = PaddingMode.PKCS7
		}.CreateDecryptor().TransformFinalBlock(input, 0, input.Length);
	}

	private static void DownloadComplete(AsyncOperation asyncOperation)
	{
		UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = (UnityWebRequestAsyncOperation)asyncOperation;
		if (unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ProtocolError)
		{
			LaunchLevelWithSave(null);
		}
		else if (unityWebRequestAsyncOperation.webRequest.downloadHandler != null && unityWebRequestAsyncOperation.webRequest.downloadHandler.data != null)
		{
			LaunchLevelWithSave(unityWebRequestAsyncOperation.webRequest.downloadHandler.data);
		}
		else
		{
			LaunchLevelWithSave(null);
		}
	}

	private static void LaunchLevelWithSave(byte[] bytes)
	{
		PopUpMessage.Close();
		m_SaveBytes = ((bytes != null) ? AES_Decrypt(bytes) : null);
		if (m_LevelId.Length == 3)
		{
			LaunchCampaignLevel(m_LevelId);
		}
		else
		{
			LaunchWorkshopLevel(m_LevelId);
		}
	}

	private static void LaunchCampaignLevel(string levelId)
	{
		CampaignLevel levelFromId = CampaignWorlds.m_Instance.GetLevelFromId(m_LevelId);
		if (levelFromId == null)
		{
			PopUpMessage.DisplayErrorOkOnly("Unable to get CampaignLevel for " + m_LevelId);
			return;
		}
		BridgeCheat.Clear();
		BridgeCheat.m_ForceUnlimitedBudget = levelFromId.m_UnlimitedBudget;
		BridgeCheat.m_ForceUnlimitedMaterial = levelFromId.m_UnlimitedMaterial;
		Campaign.m_LevelBeingPreloaded = levelFromId;
		GameStatePreloadingAssets.PreloadLevel(levelFromId.GetLayoutPath(), null, CampaignLevelLoadedCallback);
		GameUI.m_Instance.m_Campaign.Close();
		SetActive(active: true);
	}

	public static void CampaignLevelLoadedCallback(string layoutFilename, FileSlot slot)
	{
		CampaignLevel levelFromId = CampaignWorlds.m_Instance.GetLevelFromId(m_LevelId);
		if (Campaign.LoadLevel(levelFromId))
		{
			if (m_SaveBytes != null)
			{
				Bridge.ClearAndLoadBinary(m_SaveBytes);
			}
			else
			{
				Bridge.Clear();
			}
			GameManager.SetGameMode(GameMode.CAMPAIGN, GameSubMode.NONE);
			GameStateManager.SwitchToState(GameState.BUILD);
			GameUI.m_Instance.m_LeaderboardsPanel.Close();
		}
		else
		{
			PopUpMessage.DisplayErrorOkOnly($"Failed to load {levelFromId.GetLocalizedDisplayNameWithPrefix()}");
		}
		BridgeCheat.m_ForceUnlimitedBudget = false;
		BridgeCheat.m_ForceUnlimitedMaterial = false;
	}

	private static void LaunchWorkshopLevel(string levelId)
	{
		if (WeeklyChallenges.GetWeeklyChallengeByItemId(m_LevelId) == null)
		{
			PopUpMessage.DisplayErrorOkOnly("Unable to get Workshop Item associated with " + m_LevelId);
		}
		else if (GameUI.m_Instance.m_WeeklyChallenges.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_WeeklyChallenges.ForcePlay(WorkshopLevelLoadedCallback);
		}
	}

	private static void WorkshopLevelLoadedCallback()
	{
		if (m_SaveBytes != null)
		{
			Bridge.ClearAndLoadBinary(m_SaveBytes);
		}
		else
		{
			Bridge.Clear();
		}
		GameUI.m_Instance.m_WeeklyChallenges.CloseAfterPlay();
		SetActive(active: true);
	}
}

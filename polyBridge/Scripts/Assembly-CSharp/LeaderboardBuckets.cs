using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class LeaderboardBuckets
{
	public delegate void OnDownloadSingleLevelDelegate(long responseCode);

	public static Dictionary<string, LeaderboardBucketsForLevel> m_Levels = new Dictionary<string, LeaderboardBucketsForLevel>();

	public static readonly string BUCKETS_DIRECTORYNAME = "buckets";

	public static OnDownloadSingleLevelDelegate m_OnDownloadSingleLevelDelegate;

	public static bool m_Downloading;

	private static bool m_DownloadFailed;

	private static float m_NextDownloadTime;

	private static readonly string CAMPAIGN_BUCKETS_FILENAME = "campaign.bin";

	private static Action<string, byte[]> m_Callback;

	public static void Init()
	{
		DownloadAsync(CAMPAIGN_BUCKETS_FILENAME, CampaignBucketsDownloadComplete);
	}

	public static void UpdateManual()
	{
		if (m_DownloadFailed && Time.unscaledTime > m_NextDownloadTime)
		{
			m_DownloadFailed = false;
			DownloadAsync(CAMPAIGN_BUCKETS_FILENAME, CampaignBucketsDownloadComplete);
		}
	}

	public static bool CacheIsEmpty(string levelId)
	{
		if (!m_Levels.ContainsKey(levelId))
		{
			return true;
		}
		return false;
	}

	public static void Cache(string levelId, LeaderboardBucketsForLevel buckets)
	{
		if (m_Levels.ContainsKey(levelId))
		{
			m_Levels[levelId] = buckets;
		}
		else
		{
			m_Levels.Add(levelId, buckets);
		}
	}

	public static void DownloadAsync(string filename, Action<string, byte[]> callback)
	{
		m_Downloading = true;
		m_Callback = callback;
		UnityWebRequest unityWebRequest = UnityWebRequest.Get(Game.AMAZON_S3_URL + "buckets/" + filename);
		unityWebRequest.timeout = Game.DOWNLOAD_TIMEOUT_SECONDS;
		unityWebRequest.useHttpContinue = false;
		unityWebRequest.SendWebRequest().completed += DownloadCompleteBinary;
	}

	public static void PopulateLeaderboardBuckets(byte[] bytes)
	{
		int offset = 0;
		do
		{
			string key = ByteSerializer.DeserializeString(bytes, ref offset);
			int num = ByteSerializer.DeserializeInt(bytes, ref offset) - 1;
			int num2 = ByteSerializer.DeserializeInt(bytes, ref offset);
			int num3 = ByteSerializer.DeserializeInt(bytes, ref offset);
			int num4 = ByteSerializer.DeserializeInt(bytes, ref offset);
			int num5 = ByteSerializer.DeserializeInt(bytes, ref offset);
			int num6 = ByteSerializer.DeserializeInt(bytes, ref offset);
			int num7 = ByteSerializer.DeserializeInt(bytes, ref offset);
			int num8 = ByteSerializer.DeserializeInt(bytes, ref offset);
			int num9 = ByteSerializer.DeserializeInt(bytes, ref offset);
			int num10 = ByteSerializer.DeserializeInt(bytes, ref offset);
			if (!m_Levels.ContainsKey(key))
			{
				m_Levels.Add(key, new LeaderboardBucketsForLevel());
			}
			if (num >= 0 && num < LeaderboardBucketArrays.BUCKETS_PER_ARRAY)
			{
				m_Levels[key].m_Score.m_Start[num] = num2;
				m_Levels[key].m_Score.m_End[num] = num3;
				m_Levels[key].m_Score.m_Count[num] = num4;
				m_Levels[key].m_UnbreakingScore.m_Start[num] = num5;
				m_Levels[key].m_UnbreakingScore.m_End[num] = num6;
				m_Levels[key].m_UnbreakingScore.m_Count[num] = num7;
				m_Levels[key].m_StressScore.m_Start[num] = num8;
				m_Levels[key].m_StressScore.m_End[num] = num9;
				m_Levels[key].m_StressScore.m_Count[num] = num10;
			}
		}
		while (offset < bytes.Length);
	}

	private static void DownloadCompleteBinary(AsyncOperation asyncOperation)
	{
		m_Downloading = false;
		UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = (UnityWebRequestAsyncOperation)asyncOperation;
		if (unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.LogWarningFormat("Download leaderboard buckets failed with error '{0}'", unityWebRequestAsyncOperation.webRequest.error);
			m_DownloadFailed = true;
			m_NextDownloadTime = Time.unscaledTime + (float)Game.DOWNLOAD_ATTEMPT_INTERVAL_SECONDS;
			m_Callback?.Invoke(Path.GetFileNameWithoutExtension(unityWebRequestAsyncOperation.webRequest.url), null);
		}
		else if (unityWebRequestAsyncOperation.webRequest.downloadHandler != null && unityWebRequestAsyncOperation.webRequest.downloadHandler.data != null)
		{
			m_Callback?.Invoke(Path.GetFileName(unityWebRequestAsyncOperation.webRequest.url), unityWebRequestAsyncOperation.webRequest.downloadHandler.data);
		}
		else
		{
			m_Callback?.Invoke(Path.GetFileName(unityWebRequestAsyncOperation.webRequest.url), null);
		}
	}

	private static void CampaignBucketsDownloadComplete(string filename, byte[] data)
	{
		if (data == null)
		{
			string fullPath = Path.Combine(Application.persistentDataPath, BUCKETS_DIRECTORYNAME, CAMPAIGN_BUCKETS_FILENAME);
			byte[] array = null;
			if (Utils.FileExists(fullPath))
			{
				array = Utils.ReadAllBytes(fullPath);
			}
			if (array != null && array.Length != 0)
			{
				PopulateLeaderboardBuckets(array);
			}
			else
			{
				m_NextDownloadTime = Time.unscaledTime + (float)Game.DOWNLOAD_ATTEMPT_INTERVAL_SECONDS;
			}
		}
		else
		{
			string text = Path.Combine(Application.persistentDataPath, BUCKETS_DIRECTORYNAME);
			Utils.CreateDirectory(text);
			string text2 = Path.Combine(text, CAMPAIGN_BUCKETS_FILENAME);
			try
			{
				File.WriteAllBytes(text2, data);
			}
			catch (Exception ex)
			{
				Debug.LogWarning("Failed to write: " + text2 + " due to " + ex.Message);
			}
			PopulateLeaderboardBuckets(data);
		}
	}
}

using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class StatHistogram : MonoBehaviour
{
	public const float MAX_PCT = 0.3f;

	[SerializeField]
	private LeaderboardDataType dataType = LeaderboardDataType.Histogram;

	public ScoreHandler[] scorehandlers;

	private int[] histogramInfo;

	private float[] binCounts;

	private int timeAtDataRetrival;

	internal SteamLeaderboardDataHandle leaderboardHistogram;

	public bool skipUpdatingScoreboard;

	private int sceneInt;

	private bool[] numberOfScoreHandlersDoneUploading = new bool[3];

	private void Awake()
	{
		sceneInt = SceneManager.GetActiveScene().buildIndex;
	}

	internal void Init()
	{
		if (!SteamManager.Initialized || StatMaster.GetCurrentIsland() == Island.None || LevelAttributes.instance.sandBoxLevel)
		{
			base.enabled = false;
			return;
		}
		leaderboardHistogram = new SteamLeaderboardDataHandle(dataType, WinCondition.Instance.name);
		UpdateBoard();
	}

	private void UpdateBoard()
	{
		if (!skipUpdatingScoreboard)
		{
			leaderboardHistogram.GetLeaderboardData(OnHistogramDataReceived, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, 1, 1);
		}
	}

	private void OnHistogramDataReceived(LeaderboardScoresDownloaded_t leaderboardData)
	{
		if (BesiegeEntryPoint.loadingLevel || sceneInt != SceneManager.GetActiveScene().buildIndex)
		{
			return;
		}
		int[] array = new int[5];
		histogramInfo = new int[scorehandlers.Length * 7];
		LeaderboardEntry_t pLeaderboardEntry;
		SteamUserStats.GetDownloadedLeaderboardEntry(leaderboardData.m_hSteamLeaderboardEntries, 0, out pLeaderboardEntry, histogramInfo, histogramInfo.Length);
		for (int i = 0; i < scorehandlers.Length; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				array[j] = histogramInfo[i * 7 + j];
			}
			scorehandlers[i].histogramPcts = FloatPacker.UnpackIntsToFloats(array, 16);
			scorehandlers[i].PopulateHistogram(scorehandlers[i].histogramPcts);
			scorehandlers[i].SetMinMax(histogramInfo[i * 7 + 5], histogramInfo[i * 7 + 6]);
		}
		timeAtDataRetrival = (int)SteamUtils.GetServerRealTime();
	}

	public void GenerateHistogramFromSource(Action histogramDone)
	{
		for (int i = 0; i < scorehandlers.Length; i++)
		{
			scorehandlers[i].leaderboardData.GetFullLeaderboardData(1, i, delegate(int response, List<LeaderboardEntry_t> data)
			{
				GenerateHistogramFromSource(response, data, histogramDone);
			});
		}
	}

	private void GenerateHistogramFromSource(int shIndex, List<LeaderboardEntry_t> data, Action callback)
	{
		ScoreHandler scoreHandler = scorehandlers[shIndex];
		if (data.Count > 0)
		{
			LeaderboardEntry_t leaderboardEntry_t = data[0];
			LeaderboardEntry_t leaderboardEntry_t2 = data[data.Count - 1];
			if (data.Count > 30)
			{
				LeaderboardEntry_t leaderboardEntry_t3 = data[Mathf.CeilToInt((float)(data.Count - 1) * 0.95f)];
				if ((float)leaderboardEntry_t3.m_nScore * 5f < (float)leaderboardEntry_t2.m_nScore)
				{
					leaderboardEntry_t2 = leaderboardEntry_t3;
				}
			}
			if (scoreHandler.leaderboardData.GetSortMethod() == ELeaderboardSortMethod.k_ELeaderboardSortMethodAscending)
			{
				HistogramBins(data, scoreHandler.histogram.Length, scoreHandler.minValue, scoreHandler.maxValue);
			}
			else
			{
				HistogramBins(data, scoreHandler.histogram.Length, scoreHandler.maxValue, scoreHandler.minValue);
			}
			scoreHandler.PopulateHistogram(binCounts);
		}
		if (callback != null)
		{
			callback();
		}
	}

	internal void Upload(int score, Action<LeaderboardScoreUploaded_t> response)
	{
		int[] array = new int[scorehandlers.Length * 7];
		int[] array2 = new int[5];
		for (int i = 0; i < scorehandlers.Length; i++)
		{
			array2 = FloatPacker.PackFloatsToInts(scorehandlers[i].histogramPcts, 16);
			for (int j = 0; j < 5; j++)
			{
				array[i * 7 + j] = array2[j];
			}
			array[i * 7 + 5] = scorehandlers[i].minValue;
			array[i * 7 + 6] = scorehandlers[i].maxValue;
			if (scorehandlers[i].leaderboardData.GetLeaderBoardEntryCount() < 10)
			{
				Debug.LogWarning(string.Concat("Skipped Submitting histogram due to ", scorehandlers[i].dataType, ", ", scorehandlers[i].leaderboardData.GetLeaderBoardEntryCount(), " not being above 10"));
				return;
			}
		}
		int num = 0;
		for (int k = 0; k < array2.Length; k++)
		{
			num += array2[k];
		}
		if (num != 0)
		{
			leaderboardHistogram.UploadPlayerScore(score, array, delegate(LeaderboardScoreUploaded_t uploadResp)
			{
				OnUploadReponseRecieved(uploadResp, response);
			});
		}
		else
		{
			Debug.LogWarning("Skipped Submitting 0 score histogram");
		}
	}

	private void OnUploadReponseRecieved(LeaderboardScoreUploaded_t uploadResp, Action<LeaderboardScoreUploaded_t> callback)
	{
		if (sceneInt == SceneManager.GetActiveScene().buildIndex && (uploadResp.m_nGlobalRankPrevious == 0 || uploadResp.m_bScoreChanged != 0))
		{
			UpdateBoard();
			if (callback != null)
			{
				callback(uploadResp);
			}
		}
	}

	internal void HistogramBins(List<LeaderboardEntry_t> leaderboardData, int numBins, int minValue, int maxValue)
	{
		maxValue++;
		Vector2 vector = FilterByStandardDeviationMinMax(ref leaderboardData, 3.0, false);
		minValue = Mathf.Max(minValue, Mathf.CeilToInt(vector.x));
		maxValue = Mathf.Min(maxValue, Mathf.FloorToInt(vector.y));
		if (leaderboardData.Count < 10)
		{
			numBins = leaderboardData.Count;
		}
		binCounts = new float[numBins];
		int num = (maxValue - minValue) / numBins;
		bool flag = num == 0;
		if (flag)
		{
			maxValue /= 10;
			minValue /= 10;
			num = (maxValue - minValue) / numBins;
		}
		int[] array = new int[numBins];
		for (int i = 0; i < numBins; i++)
		{
			if (i == numBins - 1)
			{
				array[i] = int.MaxValue;
			}
			else
			{
				array[i] = minValue + (i + 1) * num;
			}
		}
		float num2 = 0f;
		bool flag2 = false;
		float num3 = 0f;
		int num4 = 0;
		for (int j = 0; j < numBins; j++)
		{
			int num5 = FindFirstIndex(ref leaderboardData, array[j], (!flag) ? 1 : 10);
			binCounts[j] = (float)(num5 - num4) / (float)leaderboardData.Count;
			if (binCounts[j] > 0.3f)
			{
				binCounts[j] = 0.3f;
				flag2 = true;
			}
			num2 += binCounts[j];
			num3 = ((!(num3 < binCounts[j])) ? num3 : binCounts[j]);
			Debug.Log(num4 + " - " + num5 + " numbe of people = " + (num5 - num4) + " result: " + binCounts[j] + " limit: " + array[j]);
			num4 = num5;
		}
		if (!flag2)
		{
			return;
		}
		int num6 = 0;
		while (flag2 && num6 < 10)
		{
			flag2 = false;
			float num7 = 0f;
			for (int k = 0; k < numBins; k++)
			{
				binCounts[k] /= num2;
				if (binCounts[k] > 0.3f)
				{
					binCounts[k] = 0.3f;
					flag2 = true;
				}
				num7 += binCounts[k];
			}
			num2 = num7;
			num6++;
		}
	}

	public static Vector2 FilterByStandardDeviationMinMax(ref List<LeaderboardEntry_t> leaderboardData, double stdDevs, bool filterBottom = true, bool filterTop = true)
	{
		if (leaderboardData.Count == 0)
		{
			throw new ArgumentException("Dataset cannot be null or empty.");
		}
		int count = leaderboardData.Count;
		double num = 0.0;
		double num2 = 0.0;
		for (int i = 0; i < count; i++)
		{
			int nScore = leaderboardData[i].m_nScore;
			num += (double)nScore;
			num2 += (double)nScore * (double)nScore;
		}
		double num3 = num / (double)count;
		double d = num2 / (double)count - num3 * num3;
		double num4 = Math.Sqrt(d);
		int nScore2 = leaderboardData[0].m_nScore;
		int nScore3 = leaderboardData[count - 1].m_nScore;
		double a = ((!filterBottom) ? ((double)nScore2) : (num3 - stdDevs * num4));
		double d2 = ((!filterTop) ? ((double)nScore3) : (num3 + stdDevs * num4));
		int num5 = ((!filterBottom) ? nScore2 : Math.Max(nScore2, (int)Math.Ceiling(a)));
		int num6 = ((!filterTop) ? nScore3 : Math.Min(nScore3, (int)Math.Floor(d2)));
		Debug.LogWarning(nScore2 + " -> " + num5 + " | " + nScore3 + " -> " + num6);
		return new Vector2(num5, num6);
	}

	private static int FindFirstIndex(ref List<LeaderboardEntry_t> leaderboardData, int target, int divisor)
	{
		int num = 0;
		int num2 = leaderboardData.Count;
		while (num < num2)
		{
			int num3 = (num + num2) / 2;
			if (leaderboardData[num3].m_nScore / divisor <= target)
			{
				num = num3 + 1;
			}
			else
			{
				num2 = num3;
			}
		}
		if (num == 0)
		{
			return leaderboardData.Count;
		}
		return num;
	}

	private static LeaderboardEntry_t LookupInLeaderboard(ref LeaderboardScoresDownloaded_t leaderboardData, int index)
	{
		LeaderboardEntry_t pLeaderboardEntry;
		SteamUserStats.GetDownloadedLeaderboardEntry(leaderboardData.m_hSteamLeaderboardEntries, index, out pLeaderboardEntry, null, 0);
		return pLeaderboardEntry;
	}

	internal void ScoreHandlerDoneUploading(int c)
	{
		numberOfScoreHandlersDoneUploading[c] = true;
		for (int i = 0; i < numberOfScoreHandlersDoneUploading.Length; i++)
		{
			if (!numberOfScoreHandlersDoneUploading[i])
			{
				return;
			}
		}
		numberOfScoreHandlersDoneUploading = new bool[3];
		bool flag = false;
		for (int j = 0; j < scorehandlers.Length; j++)
		{
			if ((bool)scorehandlers[j])
			{
				if (scorehandlers[j].histogramNeedsUpload)
				{
					flag = true;
				}
				scorehandlers[j].histogramNeedsUpload = false;
			}
		}
		if (flag)
		{
			if (timeAtDataRetrival == 0)
			{
				timeAtDataRetrival = (int)SteamUtils.GetServerRealTime();
			}
			Upload(timeAtDataRetrival, null);
		}
	}
}

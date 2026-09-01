using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

internal class SteamLeaderboardDataHandle
{
	public int entriesRecievedBack;

	public LeaderboardScoreUploaded_t uploadDataStored;

	private LeaderboardDataType leaderboardDataType;

	private string leaderboardName;

	private SteamLeaderboard_t currentLeaderboard;

	private bool findLeaderboardFailed;

	private int scIndex;

	private List<Action> quedRequests = new List<Action>();

	private List<LeaderboardEntry_t> fullEntryList = new List<LeaderboardEntry_t>();

	private CallResult<LeaderboardFindResult_t> findLeaderboardCallResult;

	private CallResult<LeaderboardScoresDownloaded_t> downloadLeaderboardCallResult;

	private CallResult<LeaderboardScoreUploaded_t> uploadLeaderboardCallResult;

	public SteamLeaderboardDataHandle(LeaderboardDataType scoreType, string levelName)
	{
		leaderboardDataType = scoreType;
		levelName = levelName.Replace(" ", "_");
		leaderboardName = levelName + "_" + leaderboardDataType;
		RequestLeaderBoard();
	}

	private void RequestLeaderBoard()
	{
		SteamAPICall_t hAPICall = SteamUserStats.FindLeaderboard(leaderboardName);
		findLeaderboardCallResult = CallResult<LeaderboardFindResult_t>.Create();
		findLeaderboardCallResult.Set(hAPICall, OnFindLeaderboard);
	}

	public void GetLeaderboardData(Action<LeaderboardScoresDownloaded_t> resultCallback, ELeaderboardDataRequest requestType, int start, int end)
	{
		if (findLeaderboardFailed)
		{
			return;
		}
		if (currentLeaderboard.m_SteamLeaderboard == 0L)
		{
			quedRequests.Add(delegate
			{
				GetLeaderboardData(resultCallback, requestType, start, end);
			});
			return;
		}
		SteamAPICall_t hAPICall = SteamUserStats.DownloadLeaderboardEntries(currentLeaderboard, requestType, start, end);
		downloadLeaderboardCallResult = CallResult<LeaderboardScoresDownloaded_t>.Create();
		downloadLeaderboardCallResult.Set(hAPICall, delegate(LeaderboardScoresDownloaded_t response, bool failure)
		{
			OnLeaderboardDataDownloaded(response, failure, resultCallback);
		});
	}

	public void GetFullLeaderboardData(int start, int index, Action<int, List<LeaderboardEntry_t>> resultCallback)
	{
		if (findLeaderboardFailed)
		{
			return;
		}
		if (currentLeaderboard.m_SteamLeaderboard == 0L)
		{
			quedRequests.Add(delegate
			{
				GetFullLeaderboardData(start, index, resultCallback);
			});
			return;
		}
		int num = SteamUserStats.GetLeaderboardEntryCount(currentLeaderboard);
		if (num - start > 300000)
		{
			num = 300000;
		}
		SteamAPICall_t hAPICall = SteamUserStats.DownloadLeaderboardEntries(currentLeaderboard, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, start, num);
		downloadLeaderboardCallResult = CallResult<LeaderboardScoresDownloaded_t>.Create();
		scIndex = index;
		downloadLeaderboardCallResult.Set(hAPICall, delegate(LeaderboardScoresDownloaded_t response, bool failure)
		{
			OnFullLeaderboardDataDownloaded(response, failure, index, resultCallback);
		});
	}

	private void OnFullLeaderboardDataDownloaded(LeaderboardScoresDownloaded_t leaderboardData, bool bIOFailure, int index, Action<int, List<LeaderboardEntry_t>> resultCallback)
	{
		if (!bIOFailure)
		{
			Debug.Log(string.Concat("OnFullLeaderboardDataDownloaded: ", leaderboardDataType, ", ", leaderboardData.m_cEntryCount));
			for (int i = 0; i < leaderboardData.m_cEntryCount; i++)
			{
				int[] array = new int[0];
				LeaderboardEntry_t pLeaderboardEntry;
				SteamUserStats.GetDownloadedLeaderboardEntry(leaderboardData.m_hSteamLeaderboardEntries, i, out pLeaderboardEntry, null, 0);
				fullEntryList.Add(pLeaderboardEntry);
			}
			entriesRecievedBack = fullEntryList.Count;
			if (SteamUserStats.GetLeaderboardEntryCount(currentLeaderboard) - fullEntryList.Count > 100)
			{
				Debug.Log("Getting more data from " + leaderboardData.m_cEntryCount + 1);
				GetFullLeaderboardData(leaderboardData.m_cEntryCount + 1, index, resultCallback);
			}
			else
			{
				resultCallback(index, fullEntryList);
				fullEntryList.Clear();
			}
		}
	}

	private void OnLeaderboardDataDownloaded(LeaderboardScoresDownloaded_t leaderboardData, bool bIOFailure, Action<LeaderboardScoresDownloaded_t> resultCallback)
	{
		if (!bIOFailure)
		{
			entriesRecievedBack = leaderboardData.m_cEntryCount;
			resultCallback(leaderboardData);
		}
	}

	private void OnFindLeaderboard(LeaderboardFindResult_t newLeaderboard, bool bIOFailure)
	{
		if (newLeaderboard.m_bLeaderboardFound == 0 || bIOFailure)
		{
			Debug.LogError("Leaderboard could not be found\n");
			findLeaderboardFailed = true;
			quedRequests.Clear();
		}
		else
		{
			currentLeaderboard = newLeaderboard.m_hSteamLeaderboard;
			foreach (Action quedRequest in quedRequests)
			{
				if (quedRequest != null)
				{
					quedRequest();
				}
			}
		}
		if (findLeaderboardCallResult != null)
		{
			findLeaderboardCallResult.Dispose();
		}
	}

	public ELeaderboardSortMethod GetSortMethod()
	{
		return SteamUserStats.GetLeaderboardSortMethod(currentLeaderboard);
	}

	public int GetLeaderBoardEntryCount()
	{
		return SteamUserStats.GetLeaderboardEntryCount(currentLeaderboard);
	}

	public void UploadPlayerScore(int score, int[] details, Action<LeaderboardScoreUploaded_t> resultCallback)
	{
		if (findLeaderboardFailed)
		{
			return;
		}
		if (currentLeaderboard.m_SteamLeaderboard == 0L)
		{
			quedRequests.Add(delegate
			{
				UploadPlayerScore(score, details, resultCallback);
			});
			return;
		}
		ELeaderboardUploadScoreMethod eLeaderboardUploadScoreMethod = ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest;
		if (Input.GetKey(KeyCode.Delete))
		{
			eLeaderboardUploadScoreMethod = ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodForceUpdate;
		}
		SteamAPICall_t hAPICall = SteamUserStats.UploadLeaderboardScore(currentLeaderboard, eLeaderboardUploadScoreMethod, score, details, (details != null) ? details.Length : 0);
		uploadLeaderboardCallResult = CallResult<LeaderboardScoreUploaded_t>.Create();
		uploadLeaderboardCallResult.Set(hAPICall, delegate(LeaderboardScoreUploaded_t response, bool failure)
		{
			OnUploadCompleted(response, failure, resultCallback);
		});
	}

	private void OnUploadCompleted(LeaderboardScoreUploaded_t uploadResponse, bool bIOFailure, Action<LeaderboardScoreUploaded_t> resultCallback)
	{
		if (bIOFailure || uploadResponse.m_bSuccess == 0)
		{
			Debug.Log("Failed to upload score to steam: " + bIOFailure);
			return;
		}
		uploadDataStored = uploadResponse;
		resultCallback(uploadResponse);
	}
}

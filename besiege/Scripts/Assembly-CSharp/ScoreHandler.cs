using System;
using System.Text.RegularExpressions;
using Localisation;
using Steamworks;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class ScoreHandler : MonoBehaviour
{
	[Serializable]
	public class ScoreboardEntry
	{
		public TextMesh place;

		public TextMesh user;

		public ulong userID;

		public TextMesh value;

		internal void Set(int place, ulong id, string user, string value)
		{
			if (place <= 0)
			{
				this.place.text = string.Empty;
			}
			else
			{
				this.place.text = string.Format(LocalisationManager.GetTranslation(4898), place);
			}
			userID = id;
			this.user.text = CleanUsername(user);
			this.value.text = value;
		}

		internal void Set(int place, ulong id, string value)
		{
			this.place.text = string.Format(LocalisationManager.GetTranslation(4898), place);
			userID = id;
			user.text = InfoBoxController.GetName();
			this.value.text = value;
		}

		internal void SetUsername(string user)
		{
			this.user.text = CleanUsername(user);
		}

		internal void Set(string place, string user, string value)
		{
			this.place.text = string.Format(LocalisationManager.GetTranslation(4898), place);
			this.user.text = CleanUsername(user);
			this.value.text = value;
		}
	}

	[SerializeField]
	internal LeaderboardDataType dataType = LeaderboardDataType.BlockScore;

	public Transform[] histogram;

	public DynamicText count;

	public TextMesh tooltipTop;

	public TextMesh tooltipCount;

	public Transform line;

	public Tooltip tooltip;

	[SerializeField]
	internal ScoreboardEntry[] scoreboard;

	internal LeaderboardEntry_t[] scoreEntries = new LeaderboardEntry_t[9];

	internal float[] histogramPcts = new float[10];

	internal bool histogramNeedsUpload;

	private Action[] getLeaderboardDataCalls;

	private int callCount;

	private int entryIndex;

	public bool skipUpdatingScoreboard;

	private bool hasBoardUpdated;

	private int numBins;

	private float pct = 1f;

	private bool scoreboardSet;

	internal int minValue;

	internal int maxValue = 10000;

	private int prevTextIndex = -1;

	internal static string UsernamePattern = "[^\\p{L}\\p{M}\\p{N}_\\-@!$€ت▧]";

	internal static string steamFormatPattern = "<(/?(i|b|u|strike|#([0-9a-fA-F]{1,6})))>";

	internal static string replacement = "\u200a";

	internal static string EmojiPattern = "\r\n        (?<face>   \\uD83D[\\uDE00-\\uDE4F] )\r\n      | (?<other>  (?:\r\n            \\uD83C[\\uDF00-\\uDFFF]      # U+1F300–1F3FF\r\n          | \\uD83D[\\uDC00-\\uDCFF]      # U+1F400–1F5FF\r\n          | \\uD83D[\\uDE80-\\uDEFF]      # U+1F680–1F6FF\r\n          | \\uD83E[\\uDC00-\\uDC7F]      # U+1F700–1F77F\r\n          | \\uD83E[\\uDD00-\\uDDFF]      # U+1F900–1F9FF\r\n          | \\uD83E[\\uDE70-\\uDEFF]      # U+1FA70–1FAFF\r\n          | [\\u2600-\\u26FF]            # U+2600–26FF\r\n          | [\\u2700-\\u27BF]            # U+2700–27BF\r\n        ) )\r\n    ";

	[SerializeField]
	internal SteamLeaderboardDataHandle leaderboardData;

	private int sceneInt;

	internal static string CleanUsername(string username)
	{
		username = Regex.Replace(username, "[\\uD835][\\uDC00-\\uDFFF]", MapMathematicalSymbolsToRegularChars);
		username = Regex.Replace(username, steamFormatPattern, " ");
		username = Regex.Replace(username, "\\s{2,}", " ").Trim();
		username = Regex.Replace(username, EmojiPattern, (Match m) => (!m.Groups["face"].Success) ? "▧" : "ت", RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
		username = Regex.Replace(username, UsernamePattern, replacement);
		string text = Regex.Replace(username, "[ت▧\\s]+", string.Empty);
		username = ((text.Length <= 0) ? username : text);
		if (string.IsNullOrEmpty(Regex.Replace(username, "[\\s]+", string.Empty)))
		{
			return InfoBoxController.GetName();
		}
		if (username.Length > 23)
		{
			username = username.Substring(0, 21) + "•••";
		}
		return username;
	}

	private static string MapMathematicalSymbolsToRegularChars(Match match)
	{
		int num = match.Value[0];
		int num2 = match.Value[1];
		int num3 = (num - 55296) * 1024 + (num2 - 56320) + 65536;
		if (num3 >= 119808 && num3 <= 119833)
		{
			return ((char)(65 + (num3 - 119808))).ToString();
		}
		if (num3 >= 119834 && num3 <= 119859)
		{
			return ((char)(97 + (num3 - 119834))).ToString();
		}
		return match.Value;
	}

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
		leaderboardData = new SteamLeaderboardDataHandle(dataType, WinCondition.Instance.name);
		ClearScoreboard();
	}

	internal void ShowTopBoard()
	{
		UpdateBoard();
	}

	private void UpdateBoard()
	{
		if (skipUpdatingScoreboard)
		{
			return;
		}
		hasBoardUpdated = true;
		callCount = (entryIndex = 0);
		if (leaderboardData.uploadDataStored.m_nGlobalRankNew > 10003)
		{
			getLeaderboardDataCalls = NewLeaderboardCallbacks(1, 1000, 10000);
		}
		else if (leaderboardData.uploadDataStored.m_nGlobalRankNew > 1003)
		{
			getLeaderboardDataCalls = NewLeaderboardCallbacks(1, 100, 1000);
		}
		else if (leaderboardData.uploadDataStored.m_nGlobalRankNew > 103)
		{
			getLeaderboardDataCalls = NewLeaderboardCallbacks(1, 10, 100);
		}
		else if (leaderboardData.uploadDataStored.m_nGlobalRankNew > 13)
		{
			getLeaderboardDataCalls = NewLeaderboardCallbacks(1, 10);
		}
		else
		{
			if (leaderboardData.uploadDataStored.m_nGlobalRankNew <= 8)
			{
				getLeaderboardDataCalls = new Action[0];
				callCount = 1;
				if (leaderboardData.uploadDataStored.m_nGlobalRankNew == 0)
				{
					leaderboardData.GetLeaderboardData(ReceivedDefaultRankingData, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser, 0, 0);
				}
				else
				{
					leaderboardData.GetLeaderboardData(RecievedSpecificRankingData, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, 1, 9);
				}
				return;
			}
			getLeaderboardDataCalls = NewLeaderboardCallbacks(1);
		}
		GetSpecificRankingData();
	}

	private Action[] NewLeaderboardCallbacks(params int[] indeces)
	{
		Action[] array = new Action[indeces.Length + 1];
		int max = scoreboard.Length - 1;
		for (int i = 0; i < indeces.Length; i++)
		{
			int index = indeces[i];
			array[i] = delegate
			{
				leaderboardData.GetLeaderboardData(RecievedSpecificRankingData, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, index, index);
			};
			max--;
		}
		int min = max / 2;
		max -= min;
		array[indeces.Length] = delegate
		{
			leaderboardData.GetLeaderboardData(RecievedSpecificRankingData, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser, -min, max);
		};
		return array;
	}

	private void ReceivedDefaultRankingData(LeaderboardScoresDownloaded_t leaderboardDataDownloaded)
	{
		if (leaderboardData != null && getLeaderboardDataCalls != null && !BesiegeEntryPoint.loadingLevel && sceneInt == SceneManager.GetActiveScene().buildIndex)
		{
			int[] pDetails = new int[0];
			LeaderboardEntry_t pLeaderboardEntry;
			if (SteamUserStats.GetDownloadedLeaderboardEntry(leaderboardDataDownloaded.m_hSteamLeaderboardEntries, 0, out pLeaderboardEntry, pDetails, 1))
			{
				leaderboardData.uploadDataStored.m_nGlobalRankNew = pLeaderboardEntry.m_nGlobalRank;
			}
			else
			{
				leaderboardData.uploadDataStored.m_nGlobalRankNew = 100;
			}
			UpdateBoard();
		}
	}

	private void GetSpecificRankingData()
	{
		if (leaderboardData == null || getLeaderboardDataCalls == null || BesiegeEntryPoint.loadingLevel)
		{
			return;
		}
		if (callCount < getLeaderboardDataCalls.Length)
		{
			if (getLeaderboardDataCalls[callCount] != null)
			{
				getLeaderboardDataCalls[callCount]();
			}
			callCount++;
		}
		else
		{
			SetScoreBoard();
			callCount = int.MinValue;
		}
	}

	private void RecievedSpecificRankingData(LeaderboardScoresDownloaded_t leaderboardDataDownloaded)
	{
		if (leaderboardData == null || getLeaderboardDataCalls == null || BesiegeEntryPoint.loadingLevel || scoreEntries == null || sceneInt != SceneManager.GetActiveScene().buildIndex)
		{
			return;
		}
		int[] pDetails = new int[0];
		for (int i = 0; i < leaderboardDataDownloaded.m_cEntryCount; i++)
		{
			LeaderboardEntry_t pLeaderboardEntry;
			if (SteamUserStats.GetDownloadedLeaderboardEntry(leaderboardDataDownloaded.m_hSteamLeaderboardEntries, i, out pLeaderboardEntry, pDetails, 1))
			{
				scoreEntries[entryIndex] = pLeaderboardEntry;
			}
			entryIndex++;
		}
		GetSpecificRankingData();
	}

	internal void SetScoreBoard()
	{
		if (skipUpdatingScoreboard || BesiegeEntryPoint.loadingLevel)
		{
			return;
		}
		int num = -1;
		bool flag = true;
		if (leaderboardData.uploadDataStored.m_bScoreChanged == 0)
		{
			num = leaderboardData.uploadDataStored.m_nScore;
			flag = false;
		}
		scoreboardSet = true;
		ResetColorText();
		int num2 = 1;
		ulong steamID = SteamUser.GetSteamID().m_SteamID;
		int num3 = 0;
		for (int i = 0; i < scoreboard.Length; i++)
		{
			LeaderboardEntry_t leaderboardEntry_t = scoreEntries[num3];
			string value = ValueToString(leaderboardEntry_t.m_steamIDUser.m_SteamID, leaderboardEntry_t.m_nScore);
			if (!flag && num > 0 && (num < leaderboardEntry_t.m_nScore || i + 1 == scoreboard.Length || leaderboardEntry_t.m_steamIDUser.m_SteamID == 0L))
			{
				flag = true;
				CSteamID steamID2 = SteamUser.GetSteamID();
				scoreboard[i].Set(0, steamID2.m_SteamID, SteamFriends.GetPersonaName(), ValueToString(1uL, num));
				ColorIndex(i);
				i++;
				if (i >= scoreboard.Length)
				{
					break;
				}
			}
			if (leaderboardEntry_t.m_steamIDUser.m_SteamID == 0L)
			{
				num2++;
				scoreboard[i].Set(num2, ulong.MaxValue, value);
			}
			else if (SteamFriends.RequestUserInformation(leaderboardEntry_t.m_steamIDUser, true))
			{
				scoreboard[i].Set(leaderboardEntry_t.m_nGlobalRank, leaderboardEntry_t.m_steamIDUser.m_SteamID, value);
				num2 = leaderboardEntry_t.m_nGlobalRank;
			}
			else
			{
				string user = ((!ValidateScore(num3)) ? InfoBoxController.GetName() : SteamFriends.GetFriendPersonaName(leaderboardEntry_t.m_steamIDUser));
				scoreboard[i].Set(leaderboardEntry_t.m_nGlobalRank, leaderboardEntry_t.m_steamIDUser.m_SteamID, user, value);
				num2 = leaderboardEntry_t.m_nGlobalRank;
				if (steamID == leaderboardEntry_t.m_steamIDUser.m_SteamID && (leaderboardEntry_t.m_nScore == num || leaderboardData.uploadDataStored.m_bScoreChanged == 1))
				{
					int num4 = -1;
					if (i != 0)
					{
						for (int num5 = i - 1; num5 >= 0; num5--)
						{
							if (scoreboard[num5].value.text == leaderboardEntry_t.m_nScore + string.Empty)
							{
								num4 = num5;
							}
						}
					}
					if (num4 != -1)
					{
						scoreboard[i].Set(scoreboard[num4].place.text, scoreboard[num4].user.text, scoreboard[num4].value.text);
						scoreboard[num4].Set(leaderboardEntry_t.m_nGlobalRank, leaderboardEntry_t.m_steamIDUser.m_SteamID, user, value);
						ColorIndex(num4);
					}
					else
					{
						ColorIndex(i);
					}
					flag = true;
				}
			}
			num3++;
		}
	}

	internal void ClearScoreboard()
	{
		if (!skipUpdatingScoreboard)
		{
			for (int i = 0; i < scoreboard.Length; i++)
			{
				scoreboard[i].Set(i, ulong.MaxValue, ValueToString(1uL, 1f + (float)i * 0f));
			}
		}
	}

	private void ColorIndex(int i)
	{
		Color c = new Color(0.07450981f, 1f, 0.80784315f);
		int num = 3 * i;
		tooltip.SetSpecificTextMeshColor(c, num, false);
		tooltip.SetSpecificTextMeshColor(c, num + 1, false);
		tooltip.SetSpecificTextMeshColor(c, num + 2, false);
		prevTextIndex = num;
	}

	private void ResetColorText()
	{
		if (prevTextIndex != -1)
		{
			tooltip.ResetSpecificTextMeshColor(prevTextIndex);
			tooltip.ResetSpecificTextMeshColor(prevTextIndex + 1);
			tooltip.ResetSpecificTextMeshColor(prevTextIndex + 2);
		}
	}

	private string ValueToString(ulong id, float val)
	{
		if (val <= 0f)
		{
			return string.Empty;
		}
		val -= 1f;
		if (id == 0L)
		{
			val = ((dataType != LeaderboardDataType.Time) ? 9999f : 5999.99f);
		}
		else if (dataType != LeaderboardDataType.BlockScore)
		{
			val *= 0.001f;
		}
		string label;
		return GetTextFormat(val, out label);
	}

	internal bool ValidateScore(int index)
	{
		if (scoreEntries[index].m_nScore <= 0)
		{
			return false;
		}
		double num = (double)scoreEntries[index].m_nScore * 100.0 % 1.0;
		if ((num > 0.009999999776482582 && num < 0.9900000095367432) || (float)scoreEntries[index].m_nScore <= 0f)
		{
			return false;
		}
		return true;
	}

	private void OnPersonaStateChange(PersonaStateChange_t pCallback)
	{
		if (sceneInt != SceneManager.GetActiveScene().buildIndex || !scoreboardSet || (pCallback.m_nChangeFlags & EPersonaChange.k_EPersonaChangeName) == 0)
		{
			return;
		}
		ulong ulSteamID = pCallback.m_ulSteamID;
		string friendPersonaName = SteamFriends.GetFriendPersonaName(new CSteamID(ulSteamID));
		for (int i = 0; i < scoreboard.Length; i++)
		{
			if (scoreboard[i].userID == ulSteamID)
			{
				scoreboard[i].SetUsername(friendPersonaName);
				break;
			}
			if ((scoreboard[i].userID & 0xFFFFFFFFu) == (ulSteamID & 0xFFFFFFFFu))
			{
				scoreboard[i].SetUsername(friendPersonaName);
				break;
			}
		}
	}

	internal void Upload(int score, Action<LeaderboardScoreUploaded_t, float, ScoreHandler> response)
	{
		int[] details = new int[1] { (int)SteamUtils.GetServerRealTime() };
		leaderboardData.UploadPlayerScore(score + 1, details, delegate(LeaderboardScoreUploaded_t uploadResp)
		{
			OnUploadResponseRecieved(uploadResp, response);
		});
	}

	private void OnUploadResponseRecieved(LeaderboardScoreUploaded_t uploadResp, Action<LeaderboardScoreUploaded_t, float, ScoreHandler> response)
	{
		if (sceneInt == SceneManager.GetActiveScene().buildIndex)
		{
			SetLine(uploadResp.m_nScore);
			if (uploadResp.m_bScoreChanged != 0)
			{
				UpdateHistogramData(uploadResp.m_nScore, uploadResp.m_nGlobalRankNew);
				UpdateBoard();
			}
			else if (!hasBoardUpdated)
			{
				UpdateBoard();
			}
			else
			{
				SetScoreBoard();
			}
			if (response != null)
			{
				response(uploadResp, pct, this);
			}
		}
	}

	private void UpdateHistogramData(int newScore, int newRank)
	{
		int num = 1;
		bool flag = false;
		CSteamID steamID = SteamUser.GetSteamID();
		for (int i = 0; i < scoreEntries.Length; i++)
		{
			if (scoreEntries[i].m_steamIDUser == steamID)
			{
				num = scoreEntries[i].m_nScore;
				flag = true;
			}
		}
		numBins = 10;
		int leaderBoardEntryCount = leaderboardData.GetLeaderBoardEntryCount();
		if (leaderBoardEntryCount < 10)
		{
			numBins = leaderBoardEntryCount;
		}
		float num2 = maxValue / numBins;
		int num3 = Mathf.Clamp(Mathf.FloorToInt((float)num / num2), 0, 9);
		int num4 = Mathf.Clamp(Mathf.FloorToInt((float)newScore / num2), 0, 9);
		if (num3 == num4)
		{
			return;
		}
		float num5 = 0f;
		if (leaderBoardEntryCount != 0)
		{
			for (int j = 0; j < histogramPcts.Length; j++)
			{
				if (histogramPcts[j] > num5)
				{
					num5 = histogramPcts[j];
				}
			}
			Debug.Log(string.Concat(dataType, " bucket Width: ", num2, " number of people: ", leaderBoardEntryCount));
			Debug.Log("Removing from bucket: " + num3 + " with " + histogramPcts[num3] * (float)leaderBoardEntryCount + " people. Based on score: " + num);
			if (flag)
			{
				histogramPcts[num3] = Mathf.Clamp01((histogramPcts[num3] * (float)leaderBoardEntryCount - 1f) / (float)leaderBoardEntryCount);
			}
			Debug.Log("Adding to bucket: " + num4 + " with " + histogramPcts[num4] * (float)leaderBoardEntryCount + " people. Based on score: " + newScore);
			histogramPcts[num4] = Mathf.Clamp((histogramPcts[num4] * (float)leaderBoardEntryCount + 1f) / (float)leaderBoardEntryCount, 0f, 0.3f);
		}
		else
		{
			histogramPcts[0] = 1f;
			Debug.Log("this should not happen unless stuff is out of sync and we dont have any leaderboard data stored");
		}
		PopulateHistogram(histogramPcts);
		histogramNeedsUpload = true;
	}

	internal void SetMinMax(int min, int max)
	{
		minValue = min;
		maxValue = max;
		switch (dataType)
		{
		case LeaderboardDataType.Time:
			maxValue = Mathf.Clamp(maxValue, 10, 1800000);
			minValue = Mathf.Clamp(minValue, 10, Mathf.Min(maxValue - 1, 300000));
			break;
		case LeaderboardDataType.DamageTaken:
			maxValue = Mathf.Clamp(maxValue, 1, 1000000);
			minValue = Mathf.Clamp(minValue, 0, maxValue - 1);
			break;
		default:
			maxValue = Mathf.Clamp(maxValue, 10, 5000);
			minValue = Mathf.Clamp(minValue, 2, maxValue - 1);
			break;
		}
	}

	public void ResetLine()
	{
		if (!skipUpdatingScoreboard)
		{
			line.transform.localPosition = new Vector3(1000f, 0.5f, -0.1f);
		}
	}

	private void SetLine(int score)
	{
		if (!skipUpdatingScoreboard)
		{
			float num = Mathf.Max(0f, score);
			float num2 = minValue;
			float num3 = (float)maxValue - num2;
			pct = (num - num2) / num3;
			if (leaderboardData.GetSortMethod() == ELeaderboardSortMethod.k_ELeaderboardSortMethodDescending)
			{
				pct = 1f - pct;
			}
			numBins = 10;
			int leaderBoardEntryCount = leaderboardData.GetLeaderBoardEntryCount();
			if (leaderBoardEntryCount < 10)
			{
				numBins = leaderBoardEntryCount;
			}
			if (numBins == 0)
			{
				Debug.LogError("[ScoreHandler.SetLine]: No Bins found for line alignment");
			}
			float value = pct * (float)numBins / 10f;
			line.transform.localPosition = new Vector3(Mathf.Clamp01(value), 0.5f, -0.1f);
		}
	}

	public void SetPct(float p)
	{
		if (float.IsNaN(p))
		{
			string text = " " + WinScreen.ValidHash(Machine.Active());
			if (text == " 0\u200a0\u200a0\u200a0\u200a0\u200a0\u200a0\u200a0\u200a0\u200a0")
			{
				text = string.Empty;
			}
			tooltipTop.text = LocalisationManager.GetTranslation(4900) + text;
		}
		else
		{
			tooltipTop.text = string.Format(LocalisationManager.GetTranslation(4897), (Mathf.Clamp(p, 0.001f, 1f) * 100f).ToString("0.0"));
		}
	}

	private void SetScore(string label, string value, string secondary)
	{
		string text = string.Format(LocalisationManager.GetTranslation(4899), label, value, secondary);
		tooltipCount.text = text;
	}

	public string GetTextFormat(float number, out string label)
	{
		if (dataType == LeaderboardDataType.Time)
		{
			int num = Mathf.FloorToInt(number / 60f);
			int num2 = Mathf.FloorToInt(number - (float)(num * 60));
			int num3 = Mathf.RoundToInt(number * 100f % 100f);
			label = LocalisationManager.GetTranslation(889);
			return string.Format("{0:0}:{1:00}.{2:00}", num, num2, num3);
		}
		if (dataType == LeaderboardDataType.DamageTaken)
		{
			label = LocalisationManager.GetTranslation(4892);
			return number.ToString("0.00");
		}
		label = LocalisationManager.GetTranslation(4893);
		return number.ToString("0");
	}

	public void SetDynText(float number)
	{
		string label;
		string textFormat = GetTextFormat(number, out label);
		ReferenceMaster.SetDynamicText(count, textFormat);
		string secondary = string.Empty;
		if (dataType == LeaderboardDataType.BlockScore)
		{
			int displayBlockCount = Machine.Active().DisplayBlockCount;
			textFormat = GetTextFormat(displayBlockCount, out label);
			secondary = "+" + (number - (float)displayBlockCount);
		}
		SetScore(label, textFormat, secondary);
	}

	internal void PopulateHistogram(float[] pct)
	{
		float num = 0f;
		for (int i = 0; i < pct.Length; i++)
		{
			if (pct[i] > num)
			{
				num = pct[i];
			}
		}
		if (float.IsNaN(num) || histogram[0] == null || BesiegeEntryPoint.loadingLevel)
		{
			return;
		}
		for (int j = 0; j < pct.Length; j++)
		{
			histogramPcts[j] = pct[j];
			float num2 = 0f;
			if (j < pct.Length)
			{
				num2 = pct[j] / num;
			}
			if (num2 <= 0.05f || float.IsNaN(num2))
			{
				num2 = 0.05f;
			}
			if (histogram[j] != null)
			{
				histogram[j].localScale = new Vector3(0.1f, num2, 1f);
				histogram[j].localPosition = new Vector3(0.05f + (float)j * 0.1f, num2 * 0.5f, 0f);
			}
		}
		if (pct.Length >= histogram.Length)
		{
			return;
		}
		for (int k = pct.Length - 1; k < histogram.Length; k++)
		{
			if (histogram[k] != null)
			{
				histogram[k].localScale = new Vector3(0.1f, 0.05f, 1f);
				histogram[k].localPosition = new Vector3(0.05f + (float)k * 0.1f, 0.025f, 0f);
			}
		}
	}
}

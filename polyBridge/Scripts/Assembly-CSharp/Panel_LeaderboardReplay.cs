using System;
using System.Collections.Generic;
using System.Net.Http;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_LeaderboardReplay : MonoBehaviour
{
	public TextMeshProUGUI m_BodyText;

	public Button m_CopySteamIdButton;

	public Button m_RemoveScoreButton;

	public Button m_BanButton;

	private void Start()
	{
		m_CopySteamIdButton.onClick.AddListener(OnCopySteamId);
		m_RemoveScoreButton.onClick.AddListener(OnRemoveScore);
		m_BanButton.onClick.AddListener(OnBan);
	}

	private void OnEnable()
	{
		m_BodyText.text = "\nLeaderboardReplayMode\n\n" + LeaderboardReplay.GetLeaderboardKey() + ": " + LeaderboardReplay.GetFormattedScore() + "\nSteamID: " + LeaderboardReplay.GetSteamId() + "\nName: " + LeaderboardReplay.GetName();
	}

	private void OnCopySteamId()
	{
		InterfaceAudio.Play("ui_menu_select");
		GameUI.CopyToClipboard(LeaderboardReplay.GetSteamId());
	}

	private void OnRemoveScore()
	{
		PopUpMessage.DisplayWarning("Are you sure you want to <#FCAB0C>delete<#FFFFFF> player scores for this level?", useYesNoLables: true, OnRemoveScoreConfirmed);
	}

	private void OnBan()
	{
		PopupInputField.Display("Ban Player and Delete Scores", "Enter ban reason...", isFilename: false, isDirectory: false, OnBanConfirmed);
	}

	private async void OnRemoveScoreConfirmed()
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("steamid", LeaderboardReplay.AES_Encrypt(LeaderboardReplay.GetSteamId()));
		dictionary.Add("levelid", LeaderboardReplay.GetLevelId());
		dictionary.Add("scoretype", "All");
		try
		{
			FormUrlEncodedContent content = new FormUrlEncodedContent(dictionary);
			GameUI.m_Instance.m_Status.Open("Deleting scores.");
			HttpResponseMessage httpResponseMessage = await Game.m_HttpClient.PostAsync(Game.ADMIN_DELETE_SCORE_URL, content);
			if (httpResponseMessage.IsSuccessStatusCode)
			{
				GameUI.m_Instance.m_Status.Complete("Scores deleted!");
				SteamLeaderboardScoresCache.ClearAll();
			}
			else
			{
				GameUI.m_Instance.m_Status.Complete($"Failed with {httpResponseMessage.StatusCode}");
			}
		}
		catch (Exception ex)
		{
			Debug.Log("RemoveScore failed due to exception: " + ex.Message);
		}
	}

	private async void OnBanConfirmed(string banreason)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("steamid", LeaderboardReplay.AES_Encrypt(LeaderboardReplay.GetSteamId()));
		dictionary.Add("banreason", banreason);
		try
		{
			FormUrlEncodedContent content = new FormUrlEncodedContent(dictionary);
			GameUI.m_Instance.m_Status.Open("Banning and deleting all scores.");
			HttpResponseMessage httpResponseMessage = await Game.m_HttpClient.PostAsync(Game.ADMIN_BAN_URL, content);
			if (httpResponseMessage.IsSuccessStatusCode)
			{
				GameUI.m_Instance.m_Status.Complete("User banned and histogram scores removed. Steam leaderboard removal will continue on backend and take about 3 minutes.");
				SteamLeaderboardScoresCache.ClearAll();
			}
			else
			{
				GameUI.m_Instance.m_Status.Complete($"Failed with {httpResponseMessage.StatusCode}");
			}
		}
		catch (Exception ex)
		{
			Debug.Log("Ban failed due to exception: " + ex.Message);
		}
	}
}

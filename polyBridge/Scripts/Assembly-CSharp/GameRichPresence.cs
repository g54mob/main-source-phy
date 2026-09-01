using System;
using Discord;
using UnityEngine;

public class GameRichPresence
{
	private static string m_LastSentStatus = string.Empty;

	private static string m_LastSentCampaignLevel = string.Empty;

	private static global::Discord.Discord m_Discord;

	private static ActivityManager m_DiscordActvityManager;

	private static Activity m_DiscordActivity;

	public static void Init()
	{
		SteamRichPresence.Clear();
		try
		{
			m_Discord = new global::Discord.Discord(975403183373418518L, 1uL);
			if (m_Discord != null)
			{
				m_DiscordActvityManager = m_Discord.GetActivityManager();
				m_DiscordActivity = new Activity
				{
					State = string.Empty,
					Details = string.Empty
				};
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("HANDLED: " + ex.Message);
		}
	}

	public static void Shutdown()
	{
		if (m_Discord != null)
		{
			m_DiscordActvityManager.ClearActivity(null);
		}
	}

	public static void UpdateManual()
	{
		string empty = string.Empty;
		string text = string.Empty;
		string empty2 = string.Empty;
		if (AtMainMenu())
		{
			empty = "#Status_MainMenu";
			empty2 = "Main Menu";
		}
		else if (GameManager.GetGameMode() == GameMode.SANDBOX)
		{
			empty = "#Status_Sandbox";
			empty2 = "Sandbox";
		}
		else if (GameManager.GetGameMode() == GameMode.WORKSHOP)
		{
			empty = "#PlayingCampaignLevel";
			text = GetWorkshopTitle();
			empty2 = "Playing " + text;
		}
		else
		{
			empty = "#PlayingCampaignLevel";
			text = Game.GetLevelTitle();
			empty2 = "Playing " + text;
		}
		if (m_LastSentCampaignLevel != text || m_LastSentStatus != empty)
		{
			SteamRichPresence.Clear();
			SteamRichPresence.SetCampaignLevel(text);
			SteamRichPresence.Set(empty);
			if (m_Discord != null)
			{
				try
				{
					m_DiscordActivity.Details = empty2;
					m_DiscordActvityManager.UpdateActivity(m_DiscordActivity, delegate(Result res)
					{
						if (res != Result.Ok)
						{
							Debug.LogWarning("Unable to set Discord Rich Presence");
						}
					});
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					Debug.LogWarning("HANDLED: " + ex.Message);
				}
			}
			m_LastSentCampaignLevel = text;
			m_LastSentStatus = empty;
		}
		if (m_Discord != null)
		{
			try
			{
				m_Discord.RunCallbacks();
			}
			catch (Exception)
			{
			}
		}
	}

	private static bool AtMainMenu()
	{
		if (GameStateManager.GetState() != GameState.BUILD && GameStateManager.GetState() != GameState.SIM)
		{
			return GameStateManager.GetState() != GameState.SANDBOX;
		}
		return false;
	}

	private static string GetWorkshopTitle()
	{
		string empty = string.Empty;
		string levelId = Game.GetLevelId();
		if (WeeklyChallenges.IsAWeeklyChallenge(levelId) && Workshop.m_LastPlayedWorkshopItem != null)
		{
			string text = Localize.Get("UI_SEASON_NUMBER", WeeklyChallenges.GetSeasonNumber(levelId).ToString());
			string text2 = Localize.Get("UI_WEEK", WeeklyChallenges.GetWeekWithinSeasonForItem(levelId).ToString());
			return text + " " + text2;
		}
		return Game.GetLevelTitle() + " (" + Game.GetLevelId() + ")";
	}
}

using System;
using System.Collections.Generic;
using Discord;
using Steamworks;
using UnityEngine;

public class RichPresence : MonoBehaviour
{
	public static RichPresence instance;

	private static long CLIENT_ID = 1430137637955567678L;

	private static Dictionary<string, string> levelDescriptions = new Dictionary<string, string>();

	private static bool usingDiscord;

	private static bool usingSteam;

	private global::Discord.Discord discord;

	private Activity currentActivity;

	public static void init(string descriptionsText)
	{
		if (!(instance == null))
		{
			return;
		}
		string[] array = descriptionsText.Split('\r');
		foreach (string text in array)
		{
			if (text.Contains("#"))
			{
				string[] array2 = text.Split(new string[1] { "\"\t\"" }, StringSplitOptions.None);
				string key = array2[0].Substring(array2[0].IndexOf("\"#") + 2);
				string value = array2[1].Substring(0, array2[1].IndexOf("\""));
				levelDescriptions.Add(key, value);
			}
		}
		instance = new GameObject("Rich Presence").AddComponent<RichPresence>();
		UnityEngine.Object.DontDestroyOnLoad(instance.gameObject);
	}

	private void Awake()
	{
		try
		{
			discord = new global::Discord.Discord(CLIENT_ID, 1uL);
			usingDiscord = true;
		}
		catch (Exception)
		{
			usingDiscord = false;
		}
		usingSteam = true;
		Debug.Log($"Initing ES Rich Presence: (Discord, {usingDiscord}) (Steam, {usingSteam})");
	}

	public void setRichPressence(string levelId)
	{
		string state = (levelDescriptions.ContainsKey(levelId) ? levelDescriptions[levelId] : "");
		if (usingDiscord)
		{
			ActivityManager activityManager = discord.GetActivityManager();
			currentActivity = new Activity
			{
				Timestamps = new ActivityTimestamps
				{
					Start = DateTimeOffset.Now.ToUnixTimeSeconds()
				},
				State = state,
				ApplicationId = CLIENT_ID,
				Assets = new ActivityAssets
				{
					LargeImage = "icon"
				}
			};
			activityManager.UpdateActivity(currentActivity, delegate
			{
			});
		}
		if (usingSteam)
		{
			SteamFriends.SetRichPresence("steam_display", "#" + levelId);
		}
	}

	public void setRichPressenceCustomLevel(string name)
	{
		if (usingDiscord)
		{
			ActivityManager activityManager = discord.GetActivityManager();
			currentActivity = new Activity
			{
				Timestamps = new ActivityTimestamps
				{
					Start = DateTimeOffset.Now.ToUnixTimeSeconds()
				},
				State = name,
				ApplicationId = CLIENT_ID,
				Assets = new ActivityAssets
				{
					LargeImage = "icon"
				}
			};
			activityManager.UpdateActivity(currentActivity, delegate
			{
			});
		}
		if (usingSteam)
		{
			SteamFriends.SetRichPresence("steam_display", "#CustomLevel");
		}
	}

	private void Update()
	{
		if (usingDiscord)
		{
			try
			{
				discord.RunCallbacks();
			}
			catch (Exception ex)
			{
				Debug.Log("Stopping discord rich presence.");
				Debug.Log(ex.Message);
				Debug.Log(ex.StackTrace);
				usingDiscord = false;
				discord.Dispose();
			}
		}
	}

	private void OnDestroy()
	{
		if (usingDiscord)
		{
			discord.Dispose();
		}
	}
}

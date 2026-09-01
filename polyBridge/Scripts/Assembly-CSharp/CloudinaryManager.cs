using System;
using System.Collections.Generic;
using System.Net.Http;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Newtonsoft.Json;
using Steamworks;
using UnityEngine;

public class CloudinaryManager
{
	private static Cloudinary m_Cloudinary;

	private static readonly int CLOUDINARY_CACHE_TIME_MINUTES = 15;

	public static void Init()
	{
		m_Cloudinary = new Cloudinary(new Account("dry-cactus"));
	}

	public static async void DeleteVideoAsync(string publicID, string resourceType, Action<string> callback)
	{
		try
		{
			if (SteamManager.IsLoggedOn() && !SteamManager.HasAuthTicket())
			{
				AuthTicket authTicket = await SteamUser.GetAuthSessionTicketAsync();
				if (authTicket != null)
				{
					SteamManager.RegisterTicket(authTicket);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Caught exception trying to get AuthSessionTicket: " + ex.Message);
		}
		if (!SteamManager.IsLoggedOn() || !SteamManager.HasAuthTicket())
		{
			callback?.Invoke("Not Authenticated");
			return;
		}
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("publicID", publicID);
		dictionary.Add("resourceType", resourceType);
		if (SteamManager.IsLoggedOn())
		{
			dictionary.Add("steamid", SteamUtils.GetSteamId());
			dictionary.Add("ticket", SteamManager.GetTicket());
		}
		try
		{
			FormUrlEncodedContent content = new FormUrlEncodedContent(dictionary);
			HttpResponseMessage httpResponseMessage = await Game.m_HttpClient.PostAsync(Game.GALLERY_DELETE_URL, content);
			if (httpResponseMessage.IsSuccessStatusCode)
			{
				JsonConvert.DeserializeObject<SearchResult>(await httpResponseMessage.Content.ReadAsStringAsync());
				callback?.Invoke(string.Empty);
			}
			else
			{
				callback?.Invoke($"Failed with {httpResponseMessage.StatusCode}");
			}
		}
		catch (Exception ex2)
		{
			Debug.Log("DeleteVideoAsync failed due to exception: " + ex2.Message);
			callback?.Invoke("Failed with " + ex2.Message);
		}
	}

	public static void SearchAsyncExampleSolutions(Action<string, int> failCallback, Action<SearchResult, int> successCallback, int maxResults, string excludeSteamId, string levelId, string includeTags, string excludeTags)
	{
		string expression = BuildExpressionForSearch(excludeSteamId, null, string.Empty, levelId, includeTags, excludeTags);
		ExecuteSearch(0, failCallback, successCallback, expression, maxResults, string.Empty, string.Empty, string.Empty);
	}

	public static void SearchAsync(int pageIndex, Action<string, int> failCallback, Action<SearchResult, int> successCallback, int maxResults, string nextCursor, string sortField, string sortDirection, List<string> steamIds, string worldId, string levelId, string includeTags, string excludeTags)
	{
		string expression = BuildExpressionForSearch(string.Empty, steamIds, worldId, levelId, includeTags, excludeTags);
		ExecuteSearch(pageIndex, failCallback, successCallback, expression, maxResults, sortField, sortDirection, nextCursor);
	}

	public static string GeneratePublicId(int budget)
	{
		budget = Mathf.Clamp(budget, 0, 99999999);
		return budget.ToString("D8") + "_" + Utils.GenerateUniqueId();
	}

	private static string BuildExpressionForSearch(string excludeSteamId, List<string> steamIds, string worldId, string levelId, string includeTags, string excludeTags)
	{
		string text = "(resource_type:image OR resource_type:video)";
		if (!string.IsNullOrEmpty(levelId))
		{
			text = text + " AND context.LEVEL_ID=" + levelId;
		}
		else if (!string.IsNullOrEmpty(worldId))
		{
			text = text + " AND context.WORLD_ID=" + worldId;
		}
		if (!string.IsNullOrEmpty(includeTags))
		{
			string[] array = includeTags.Split(',');
			foreach (string text2 in array)
			{
				text = text + " AND tags:" + text2;
			}
		}
		if (steamIds != null)
		{
			for (int j = 0; j < steamIds.Count; j++)
			{
				text = ((j != 0) ? (text + " OR context.STEAM_ID=" + steamIds[j]) : (text + " AND (context.STEAM_ID=" + steamIds[j]));
			}
			if (steamIds.Count > 0)
			{
				text += ")";
			}
		}
		if (!string.IsNullOrEmpty(excludeTags))
		{
			string[] array2 = excludeTags.Split(',');
			text = text + " AND NOT (tags:" + array2[0];
			for (int k = 1; k < array2.Length; k++)
			{
				text = text + " OR tags:" + array2[k];
			}
			if (!string.IsNullOrEmpty(excludeSteamId))
			{
				text = text + " OR context.STEAM_ID=" + excludeSteamId;
			}
			text += ")";
		}
		else if (!string.IsNullOrEmpty(excludeSteamId))
		{
			text = text + " AND NOT context.STEAM_ID=" + excludeSteamId;
		}
		return text;
	}

	private static async void ExecuteSearch(int pageIndex, Action<string, int> failCallback, Action<SearchResult, int> successCallback, string expression, int maxResults, string sortField, string sortDirection, string nextCursor)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("expression", expression);
		dictionary.Add("maxResults", maxResults.ToString());
		dictionary.Add("sortField", sortField);
		dictionary.Add("sortDirection", sortDirection);
		dictionary.Add("nextCursor", nextCursor);
		dictionary.Add("skipCache", (GalleryCurate.CURATE_MODE || Replays.ReplayUploadedInLastMinutes(CLOUDINARY_CACHE_TIME_MINUTES) || Gallery.GalleryItemDeletedInLastMinutes(CLOUDINARY_CACHE_TIME_MINUTES)) ? "true" : "false");
		dictionary.Add("steamid", string.Empty);
		dictionary.Add("ticket", string.Empty);
		try
		{
			FormUrlEncodedContent content = new FormUrlEncodedContent(dictionary);
			HttpResponseMessage httpResponseMessage = await Game.m_HttpClient.PostAsync(Game.GALLERY_SEARCH_URL, content);
			if (httpResponseMessage.IsSuccessStatusCode)
			{
				SearchResult arg = JsonConvert.DeserializeObject<SearchResult>(await httpResponseMessage.Content.ReadAsStringAsync());
				successCallback?.Invoke(arg, pageIndex);
			}
			else
			{
				failCallback?.Invoke($"Failed with {httpResponseMessage.StatusCode}", pageIndex);
			}
		}
		catch (Exception ex)
		{
			Debug.Log("ExecuteSearch failed due to: " + ex.Message);
			failCallback?.Invoke("Failed with " + ex.Message, pageIndex);
		}
	}
}

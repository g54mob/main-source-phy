using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Steamworks.Ugc;
using UnityEngine;
using UnityEngine.Networking;

public class WorkshopItem
{
	public bool m_LoadingPreviewTexture;

	public Texture2D m_PreviewTexture;

	private Action<WorkshopItem> m_DownloadPreviewCallback;

	private bool m_UseUnlimitedBudget;

	private bool m_UseUnlimitedMaterials;

	public Item m_SteamItem;

	public WorkshopItem()
	{
	}

	private void AsyncLoadPreviewTexture(string url)
	{
		WebRequest.GetTexture(url).SendWebRequest().completed += OnLoadPreviewComplete;
	}

	private void OnLoadPreviewComplete(AsyncOperation asyncOperation)
	{
		UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = (UnityWebRequestAsyncOperation)asyncOperation;
		if (unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ProtocolError)
		{
			string errorMessage = WebRequest.GetErrorMessage(unityWebRequestAsyncOperation.webRequest);
			Debug.LogWarning("Load slot preview failed: " + errorMessage);
			m_PreviewTexture = null;
		}
		else
		{
			m_PreviewTexture = DownloadHandlerTexture.GetContent(unityWebRequestAsyncOperation.webRequest);
			PreviewCache.Cache(Utils.GetFileSafePreviewUrl(m_SteamItem.PreviewImageUrl), m_PreviewTexture);
		}
		m_LoadingPreviewTexture = false;
		m_DownloadPreviewCallback?.Invoke(this);
	}

	public WorkshopItem(Item steamItem)
	{
		m_SteamItem = steamItem;
	}

	public string GetId()
	{
		return m_SteamItem.Id.Value.ToString();
	}

	public ulong GetIdAsUlong()
	{
		return m_SteamItem.Id;
	}

	public string GetTitle()
	{
		return m_SteamItem.Title;
	}

	public string GetDescription()
	{
		return m_SteamItem.Description;
	}

	public string GetCreatorName()
	{
		return m_SteamItem.Owner.Name;
	}

	public string GetCreatorNameNoRichText()
	{
		Regex regex = new Regex("<[^>]*>");
		if (regex.IsMatch(m_SteamItem.Owner.Name))
		{
			return regex.Replace(m_SteamItem.Owner.Name, string.Empty);
		}
		return m_SteamItem.Owner.Name;
	}

	public ulong GetSteamId()
	{
		return m_SteamItem.Owner.Id;
	}

	public string GetMetadata()
	{
		return m_SteamItem.Metadata;
	}

	public DateTime GetLastUpdatedDate()
	{
		return m_SteamItem.Updated;
	}

	public DateTime GetCreatedDate()
	{
		return m_SteamItem.Created;
	}

	public bool HasTag(string tag)
	{
		return m_SteamItem.HasTag(tag);
	}

	public string GetDirectory()
	{
		return m_SteamItem.Directory;
	}

	public bool IsOwnedByMe()
	{
		return m_SteamItem.Owner.IsMe;
	}

	public bool IsInstalled()
	{
		return m_SteamItem.IsInstalled;
	}

	public bool IsSubscribed()
	{
		return m_SteamItem.IsSubscribed;
	}

	public bool IsAutoPlay()
	{
		return m_SteamItem.HasTag(WorkshopTags.AUTOPLAY_TAG);
	}

	public float GetNormalizedRating()
	{
		return m_SteamItem.Score;
	}

	public bool IsFeatured()
	{
		return m_SteamItem.HasTag(WorkshopTags.FEATURED_TAG);
	}

	public bool AllowFeatured()
	{
		return m_SteamItem.HasTag(WorkshopTags.ALLOWFEATURED_TAG);
	}

	public bool IsMod()
	{
		return m_SteamItem.HasTag(WorkshopTags.MOD_TAG);
	}

	public bool IsLevel()
	{
		return m_SteamItem.HasTag(WorkshopTags.LEVEL_TAG);
	}

	public bool IsCampaign()
	{
		return m_SteamItem.HasTag(WorkshopTags.CAMPAIGN_TAG);
	}

	public async void DownloadFromSteam(Action<bool> callback)
	{
		bool obj = await m_SteamItem.DownloadAsync();
		callback?.Invoke(obj);
	}

	public void DownloadPreviewFromSteam(Action<WorkshopItem> callback)
	{
		Texture2D texture2D = PreviewCache.Get(Utils.GetFileSafePreviewUrl(m_SteamItem.PreviewImageUrl));
		if (texture2D != null)
		{
			m_PreviewTexture = texture2D;
			m_LoadingPreviewTexture = false;
			callback?.Invoke(this);
		}
		else if (!string.IsNullOrEmpty(m_SteamItem.PreviewImageUrl))
		{
			m_DownloadPreviewCallback = callback;
			m_PreviewTexture = null;
			m_LoadingPreviewTexture = true;
			AsyncLoadPreviewTexture(m_SteamItem.PreviewImageUrl);
		}
		else
		{
			m_PreviewTexture = null;
			callback?.Invoke(this);
		}
	}

	public string GetLevelLayoutPathAndFilename()
	{
		try
		{
			return Path.Combine(GetDirectory(), Workshop.LEVEL_LAYOUT_FILENAME);
		}
		catch
		{
			return null;
		}
	}

	public void Play(bool useUnlimitedBudget, bool useUnlimitedMaterials)
	{
		List<string> inactiveModsInLayout = Mods.GetInactiveModsInLayout(Path.Combine(GetDirectory(), Workshop.LEVEL_LAYOUT_FILENAME));
		m_UseUnlimitedBudget = useUnlimitedBudget;
		m_UseUnlimitedMaterials = useUnlimitedMaterials;
		if (inactiveModsInLayout.Count > 0)
		{
			GameUI.m_Instance.m_ModsRequiredPopup.Open(inactiveModsInLayout, null, DoOpenLevel);
			return;
		}
		Mods.DeactivateAutoLoadedMods();
		DoOpenLevel(null);
	}

	private void DoOpenLevel(FileSlot slot)
	{
		GameStatePreloadingAssets.PreloadLevel(Path.Combine(GetDirectory(), Workshop.LEVEL_LAYOUT_FILENAME), slot, PreloadOpenLevelCallback);
	}

	private void PreloadOpenLevelCallback(string layoutPath, FileSlot slot)
	{
		BridgeCheat.Clear();
		BridgeCheat.m_ForceUnlimitedBudget = m_UseUnlimitedBudget;
		BridgeCheat.m_ForceUnlimitedMaterial = m_UseUnlimitedMaterials;
		if (Workshop.PlayLevel(this, layoutPath, GameSubMode.NONE))
		{
			GameAchievements.UnlockAchievement(GameAchievement.UI_WorkShopping);
			GameUI.m_Instance.m_Gallery.m_ReturnToMainMenu = false;
			GameUI.m_Instance.m_Gallery.m_ReturnToWorkshop = false;
			GameUI.m_Instance.m_Gallery.Close();
			if (GameUI.m_Instance.m_Workshop.m_WorkshopItemPanel.gameObject.activeInHierarchy)
			{
				GameUI.m_Instance.m_Workshop.m_WorkshopItemPanel.Close();
			}
			GameUI.m_Instance.m_Workshop.Close(suppressMainMenu: true);
			if (GameUI.m_Instance.m_MainMenuNew.gameObject.activeInHierarchy)
			{
				GameUI.m_Instance.m_MainMenuNew.Close();
			}
		}
	}
}

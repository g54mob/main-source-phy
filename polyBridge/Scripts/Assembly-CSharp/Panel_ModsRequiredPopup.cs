using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Steamworks.Data;
using Steamworks.Ugc;
using TMPro;
using UnityEngine;

public class Panel_ModsRequiredPopup : MonoBehaviour
{
	private enum ModDownloadStatus
	{
		PREPARING = 0,
		QUERYING = 1,
		SUBSCRIBING = 2,
		DOWNLOAD_PREPARING = 3,
		DOWNLOADING = 4,
		INSTALLING = 5,
		ACTIVATING = 6,
		FINISHED = 7
	}

	private class ModDownloadProgress
	{
		public ModDownloadStatus m_CurrentStatus;

		public int m_DownloadCurrentIndex;

		public int m_DownloadTotalItems;

		public long m_DownloadedBytes;

		public long m_DownloadedBytesTotal;

		public void Reset()
		{
			m_CurrentStatus = ModDownloadStatus.PREPARING;
			m_DownloadCurrentIndex = 0;
			m_DownloadTotalItems = 0;
			m_DownloadedBytes = 0L;
			m_DownloadedBytesTotal = 0L;
		}
	}

	public TextMeshProUGUI m_ResultsText;

	private List<string> m_ModList;

	private List<string> m_EmbeddedModList;

	private FileSlot m_FileSlot;

	private bool m_DownloadSucceeded;

	private Action<FileSlot> m_SuccessCallback;

	private ModDownloadProgress m_DownloadProgress = new ModDownloadProgress();

	private void Update()
	{
		UpdateProgressString();
	}

	private void OnEnable()
	{
		ActivePanels.Add(base.gameObject);
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
	}

	public void Open(List<string> modList, FileSlot slot, Action<FileSlot> successCallback)
	{
		m_ModList = modList;
		m_FileSlot = slot;
		m_SuccessCallback = successCallback;
		MaybeGetEmbeddedModList();
		OnDownload();
	}

	public void Close()
	{
		InterfaceAudio.Play("ui_menu_cancel");
		base.gameObject.SetActive(value: false);
	}

	private async void OnDownload()
	{
		Mods.DeactivateAutoLoadedMods();
		if (GetNotInstalledMods().Count > 0)
		{
			base.gameObject.SetActive(value: true);
			m_DownloadSucceeded = await DownloadMods();
		}
		else
		{
			GameUI.m_Instance.m_PreloadingObject.SetActive(value: true);
			m_DownloadSucceeded = true;
		}
		if (!m_DownloadSucceeded)
		{
			await Task.Delay(1000);
		}
		else
		{
			await ActivateMods();
		}
		m_DownloadProgress.m_CurrentStatus = ModDownloadStatus.FINISHED;
		OnFinished();
	}

	private void OnFinished()
	{
		Close();
		if (m_DownloadSucceeded && m_SuccessCallback != null)
		{
			m_SuccessCallback(m_FileSlot);
		}
	}

	private async Task<bool> DownloadMods()
	{
		int minWaitMilliseconds = 1000;
		m_DownloadProgress.Reset();
		UpdateProgressString();
		bool addedToSubscribedItems = false;
		List<string> notInstalledMods = GetNotInstalledMods();
		if (notInstalledMods.Count > 0)
		{
			List<PublishedFileId> list = new List<PublishedFileId>();
			foreach (string item3 in notInstalledMods)
			{
				PublishedFileId item = default(PublishedFileId);
				if (ulong.TryParse(item3, out item.Value))
				{
					list.Add(item);
				}
			}
			m_DownloadProgress.m_CurrentStatus = ModDownloadStatus.QUERYING;
			ResultPage? resultPage = await Query.All.WithFileId(list.ToArray()).GetPageAsync(1);
			if (!resultPage.HasValue)
			{
				m_ResultsText.text = Localize.Get("UI_MOD_DL_RESULT_QUERY_ERR");
				return false;
			}
			ResultPage value = resultPage.Value;
			m_DownloadProgress.m_DownloadTotalItems = value.ResultCount;
			m_DownloadProgress.m_DownloadCurrentIndex = 0;
			foreach (Item item2 in value.Entries)
			{
				m_DownloadProgress.m_DownloadedBytes = 0L;
				m_DownloadProgress.m_DownloadedBytesTotal = 0L;
				m_DownloadProgress.m_CurrentStatus = ModDownloadStatus.SUBSCRIBING;
				if (!(await item2.Subscribe()))
				{
					m_ResultsText.text = Localize.Get("UI_MOD_DL_RESULT_SUB_FAIL");
					return false;
				}
				if (!item2.Download(highPriority: true))
				{
					m_ResultsText.text = Localize.Get("UI_MOD_DL_RESULT_DL_FAIL");
					return false;
				}
				for (int i = 0; i < 600; i++)
				{
					minWaitMilliseconds -= 100;
					await Task.Delay(100);
					if (item2.IsDownloadPending)
					{
						m_DownloadProgress.m_CurrentStatus = ModDownloadStatus.DOWNLOAD_PREPARING;
						continue;
					}
					if (item2.IsDownloading)
					{
						m_DownloadProgress.m_CurrentStatus = ModDownloadStatus.DOWNLOADING;
						m_DownloadProgress.m_DownloadedBytes = item2.DownloadBytesDownloaded;
						m_DownloadProgress.m_DownloadedBytesTotal = item2.DownloadBytesTotal;
						continue;
					}
					if (item2.IsInstalled)
					{
						break;
					}
					m_DownloadProgress.m_CurrentStatus = ModDownloadStatus.INSTALLING;
				}
				Workshop.AddToSubscribedItems(new SteamItemInfo(item2));
				addedToSubscribedItems = true;
				m_DownloadProgress.m_DownloadCurrentIndex++;
			}
		}
		if (minWaitMilliseconds > 0)
		{
			await Task.Delay(minWaitMilliseconds);
		}
		if (addedToSubscribedItems)
		{
			Workshop.SaveSubscribedItemsToDisk();
		}
		return true;
	}

	private async Task ActivateMods()
	{
		for (int i = 0; i < 100; i++)
		{
			if (Profiles.m_ActiveProfile.m_DidCrashOnModLoad)
			{
				await Task.Delay(100);
			}
		}
		List<string> list = new List<string>(m_ModList);
		for (int num = list.Count - 1; num >= 0; num--)
		{
			if (Mods.ModIsActive(list[num]))
			{
				list.Remove(list[num]);
			}
		}
		if (m_EmbeddedModList.Count > 0)
		{
			list.AddRange(m_EmbeddedModList);
		}
		Mods.AddAutoLoadedMods(list);
		Mods.ActivateAutoLoadedMods();
		Mods.LoadModsFromProfile(null);
		m_DownloadProgress.m_CurrentStatus = ModDownloadStatus.ACTIVATING;
		for (int i = 0; i < 100; i++)
		{
			await Task.Delay(100);
			if (ModApi.GetNumModsLoadingAddressables() <= 0)
			{
				break;
			}
		}
		m_ResultsText.text = Localize.Get("UI_MOD_DL_RESULT_SUCCESS");
	}

	private List<string> GetNotInstalledMods()
	{
		List<string> list = new List<string>(m_ModList);
		foreach (KeyValuePair<string, SteamItemInfo> subscribedItem in Workshop.m_SubscribedItems)
		{
			string key = subscribedItem.Key;
			if (list.Contains(key) && Directory.Exists(Workshop.m_SubscribedItems[key].m_InstallPath))
			{
				list.Remove(key);
			}
		}
		return list;
	}

	private void UpdateProgressString()
	{
		if (m_DownloadProgress.m_CurrentStatus == ModDownloadStatus.FINISHED)
		{
			return;
		}
		float num = 0f;
		float num2 = 0f;
		switch (m_DownloadProgress.m_CurrentStatus)
		{
		case ModDownloadStatus.PREPARING:
			num = 0.01f;
			break;
		case ModDownloadStatus.QUERYING:
			num = 0.05f;
			break;
		case ModDownloadStatus.SUBSCRIBING:
		case ModDownloadStatus.DOWNLOAD_PREPARING:
			num = 0.1f;
			if (m_DownloadProgress.m_DownloadTotalItems > 0)
			{
				num2 = (float)m_DownloadProgress.m_DownloadCurrentIndex / (float)m_DownloadProgress.m_DownloadTotalItems * 0.8f;
			}
			break;
		case ModDownloadStatus.DOWNLOADING:
		case ModDownloadStatus.INSTALLING:
			num = 0.1f;
			if (m_DownloadProgress.m_DownloadTotalItems > 0)
			{
				num2 = (float)m_DownloadProgress.m_DownloadCurrentIndex / (float)m_DownloadProgress.m_DownloadTotalItems * 0.8f;
				if (m_DownloadProgress.m_DownloadedBytesTotal > 0)
				{
					float num3 = (float)m_DownloadProgress.m_DownloadedBytes / (float)m_DownloadProgress.m_DownloadedBytesTotal;
					num2 += num3 / (float)m_DownloadProgress.m_DownloadTotalItems;
				}
			}
			break;
		case ModDownloadStatus.ACTIVATING:
			num = 0.9f;
			break;
		}
		string parameter = Mathf.FloorToInt((num + num2) * 100f).ToString();
		string text = Localize.Get("UI_MOD_DL_STATUS_" + m_DownloadProgress.m_CurrentStatus);
		string text2 = Localize.Get("UI_MOD_DL_PROGRESS", parameter);
		m_ResultsText.text = text + "\n\n" + text2;
	}

	private void MaybeGetEmbeddedModList()
	{
		m_EmbeddedModList = new List<string>();
		foreach (string mod in m_ModList)
		{
			if (!mod.EndsWith(Mods.EMBEDDED_MODS_FILENAME))
			{
				continue;
			}
			if (File.Exists(mod))
			{
				string directoryName = Path.GetDirectoryName(mod);
				string[] array = File.ReadAllLines(mod);
				foreach (string path in array)
				{
					string text = Path.Combine(directoryName, path);
					if (Directory.Exists(text))
					{
						m_EmbeddedModList.Add(text);
					}
				}
			}
			m_ModList.Remove(mod);
			break;
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using Steamworks;
using UnityEngine;

public class ESSteamUGC : ESUGC
{
	private Callback<RemoteStoragePublishedFileSubscribed_t> itemSubscribedCallback;

	private Callback<DownloadItemResult_t> downloadItemResultCallback;

	private Callback<RemoteStoragePublishedFileUnsubscribed_t> itemUnsubscribedCallback;

	private Callback<ItemInstalled_t> itemInstalledCallback;

	private Callback<RemoteStoragePublishedFileUpdated_t> itemNeedsUpdateCallback;

	private List<IDisposable> gcDontKill = new List<IDisposable>();

	private Action<string, ESUGCChange, ESUGC> onChange;

	private List<string> installingRooms = new List<string>();

	private List<string> uninstallingRooms = new List<string>();

	protected override void internalInit()
	{
		itemSubscribedCallback = Callback<RemoteStoragePublishedFileSubscribed_t>.Create(onItemSubscribed);
		downloadItemResultCallback = Callback<DownloadItemResult_t>.Create(downloadItemResult);
		itemUnsubscribedCallback = Callback<RemoteStoragePublishedFileUnsubscribed_t>.Create(onItemUnsubscribed);
		itemInstalledCallback = Callback<ItemInstalled_t>.Create(onItemInstalled);
		itemNeedsUpdateCallback = Callback<RemoteStoragePublishedFileUpdated_t>.Create(onItemNeedsUpdate);
	}

	public override void init(Action<string, ESUGCChange, ESUGC> onChange)
	{
		this.onChange = onChange;
	}

	public override void dispose()
	{
		itemSubscribedCallback.Dispose();
		itemUnsubscribedCallback.Dispose();
		itemInstalledCallback.Dispose();
		itemNeedsUpdateCallback.Dispose();
		downloadItemResultCallback.Dispose();
		itemSubscribedCallback = null;
		itemUnsubscribedCallback = null;
		itemInstalledCallback = null;
		itemNeedsUpdateCallback = null;
		downloadItemResultCallback = null;
		for (int i = 0; i < gcDontKill.Count; i++)
		{
			gcDontKill[i].Dispose();
		}
		gcDontKill.Clear();
	}

	public override void update()
	{
	}

	public override void refreshAllInstalledRooms(Action<RefreshRoomData> onGetNameAndURL)
	{
		uint numSubscribedItems = SteamUGC.GetNumSubscribedItems();
		PublishedFileId_t[] pvecPublishedFileID = new PublishedFileId_t[numSubscribedItems];
		uint subscribedItems = SteamUGC.GetSubscribedItems(pvecPublishedFileID, numSubscribedItems);
		UGCQueryHandle_t handle = SteamUGC.CreateQueryUGCDetailsRequest(pvecPublishedFileID, subscribedItems);
		SteamUGC.SetReturnMetadata(handle, bReturnMetadata: true);
		SteamAPICall_t hAPICall = SteamUGC.SendQueryUGCRequest(handle);
		CallResult<SteamUGCQueryCompleted_t> callResult = CallResult<SteamUGCQueryCompleted_t>.Create(onDone);
		callResult.Set(hAPICall);
		gcDontKill.Add(callResult);
		void onDone(SteamUGCQueryCompleted_t data, bool failure)
		{
			for (uint num = 0u; num < data.m_unNumResultsReturned; num++)
			{
				if (SteamUGC.GetQueryUGCResult(data.m_handle, num, out var pDetails) && pDetails.m_eResult == EResult.k_EResultOK && SteamUGC.GetQueryUGCPreviewURL(data.m_handle, num, out var pchURL, 5000u))
				{
					SteamUGC.GetItemInstallInfo(pDetails.m_nPublishedFileId, out var _, out var pchFolder, 5000u, out var _);
					DateTime modified = DateTime.Now;
					if (Directory.Exists(pchFolder))
					{
						modified = Directory.GetCreationTime(pchFolder);
					}
					string text = pDetails.m_nPublishedFileId.ToString();
					ESUGC.cachedRoomData.Remove(text);
					ESUGC.cachedRoomData.Add(text, new CachedRoomData
					{
						name = pDetails.m_rgchTitle,
						imageUrl = pchURL,
						modified = modified,
						installLocation = pchFolder
					});
					onGetNameAndURL(new RefreshRoomData(text, pDetails.m_rgchTitle, pchURL, modified, this));
				}
			}
			SteamUGC.ReleaseQueryUGCRequest(data.m_handle);
		}
	}

	public override CustomRoomState customRoomState(string levelId)
	{
		if (!isCustomLevel(levelId))
		{
			Debug.Log("[customRoomState] level with id: " + levelId + " is not valid steam id! returning NotInstalled.");
			return CustomRoomState.NotInstalled;
		}
		CustomRoomState result = CustomRoomState.NotInstalled;
		PublishedFileId_t nPublishedFileID = new PublishedFileId_t(ulong.Parse(levelId));
		uint itemState = SteamUGC.GetItemState(nPublishedFileID);
		bool flag = (itemState & 1) == 1;
		ulong punSizeOnDisk;
		string pchFolder;
		uint punTimeStamp;
		bool flag2 = SteamUGC.GetItemInstallInfo(nPublishedFileID, out punSizeOnDisk, out pchFolder, 5000u, out punTimeStamp) && Directory.Exists(pchFolder);
		bool flag3 = (itemState & 4) == 4 && punSizeOnDisk != 0 && flag2 && flag;
		bool flag4 = (itemState & 0x10) == 16 || installingRooms.Contains(nPublishedFileID.m_PublishedFileId.ToString());
		bool flag5 = (itemState & 8) == 8;
		bool flag6 = (itemState & 0x20) == 32;
		if (!flag3 && flag6)
		{
			result = CustomRoomState.Installing;
		}
		else if (flag4)
		{
			result = CustomRoomState.Installing;
		}
		else if (flag5)
		{
			result = CustomRoomState.NeedsUpdate;
		}
		else if (flag3)
		{
			result = CustomRoomState.Installed;
		}
		return result;
	}

	public override bool installRoom(string levelId)
	{
		if (!isCustomLevel(levelId))
		{
			Debug.Log("[installRoom] level with id: " + levelId + " is not valid steam id! returning false.");
			return false;
		}
		PublishedFileId_t nPublishedFileID = new PublishedFileId_t(ulong.Parse(levelId));
		SteamUGC.SubscribeItem(nPublishedFileID);
		bool result = SteamUGC.DownloadItem(nPublishedFileID, bHighPriority: true);
		installingRooms.Add(nPublishedFileID.m_PublishedFileId.ToString());
		return result;
	}

	public override void uninstallRoom(string levelId)
	{
		if (!isCustomLevel(levelId))
		{
			Debug.Log("[uninstallRoom] level with id: " + levelId + " is not valid steam id! returning.");
			return;
		}
		PublishedFileId_t nPublishedFileID = new PublishedFileId_t(ulong.Parse(levelId));
		SteamUGC.UnsubscribeItem(nPublishedFileID);
		uninstallingRooms.Add(nPublishedFileID.m_PublishedFileId.ToString());
	}

	public override float getInstallProgress(string levelId)
	{
		if (!isCustomLevel(levelId))
		{
			Debug.Log("[getInstallProgress] level with id: " + levelId + " is not valid steam id! returning -1.");
			return -1f;
		}
		if (SteamUGC.GetItemDownloadInfo(new PublishedFileId_t(ulong.Parse(levelId)), out var punBytesDownloaded, out var punBytesTotal))
		{
			if (punBytesTotal != 0L)
			{
				return (float)punBytesDownloaded / (float)punBytesTotal;
			}
			return 0f;
		}
		return -1f;
	}

	public override bool isCustomLevel(string levelId)
	{
		if (string.IsNullOrEmpty(levelId))
		{
			return false;
		}
		ulong result;
		return ulong.TryParse(levelId, out result);
	}

	public override string getPath(string levelId)
	{
		if (!isCustomLevel(levelId))
		{
			Debug.Log("[getPath] level with id: " + levelId + " is not valid steam id! returning String.Empty.");
			return string.Empty;
		}
		SteamUGC.GetItemInstallInfo(new PublishedFileId_t(ulong.Parse(levelId)), out var _, out var pchFolder, 5000u, out var _);
		return pchFolder;
	}

	public override ulong getId(string levelId)
	{
		return ulong.Parse(levelId);
	}

	public override void getVersion(string levelId, Action<uint> onSuccess, Action<string> onError)
	{
		PublishedFileId_t publishedFileId_t = new PublishedFileId_t(getId(levelId));
		UGCQueryHandle_t handle = SteamUGC.CreateQueryUGCDetailsRequest(new PublishedFileId_t[1] { publishedFileId_t }, 1u);
		CallResult<SteamUGCQueryCompleted_t> callResult = CallResult<SteamUGCQueryCompleted_t>.Create();
		gcDontKill.Add(callResult);
		SteamAPICall_t hAPICall = SteamUGC.SendQueryUGCRequest(handle);
		callResult.Set(hAPICall, delegate(SteamUGCQueryCompleted_t callback, bool ioFailure)
		{
			try
			{
				SteamUGCDetails_t pDetails;
				if (ioFailure)
				{
					onError?.Invoke("Steam API I/O Failure while querying UGC.");
				}
				else if (callback.m_eResult == EResult.k_EResultOK && SteamUGC.GetQueryUGCResult(callback.m_handle, 0u, out pDetails))
				{
					onSuccess?.Invoke(pDetails.m_rtimeUpdated);
				}
				else
				{
					onError?.Invoke($"UGC Query failed with result: {callback.m_eResult}");
				}
			}
			finally
			{
				SteamUGC.ReleaseQueryUGCRequest(callback.m_handle);
				gcDontKill.Remove(callResult);
			}
		});
	}

	public override void refreshSpecificRooms(string[] roomIds, Action<RefreshRoomData> onGetNameAndURL)
	{
		PublishedFileId_t[] array = new PublishedFileId_t[roomIds.Length];
		for (int i = 0; i < roomIds.Length; i++)
		{
			if (ulong.TryParse(roomIds[i], out var result))
			{
				array[i] = new PublishedFileId_t(result);
			}
			else
			{
				Debug.Log("failed to parse " + roomIds[i]);
			}
		}
		UGCQueryHandle_t handle = SteamUGC.CreateQueryUGCDetailsRequest(array, (uint)array.Length);
		SteamUGC.SetReturnMetadata(handle, bReturnMetadata: true);
		SteamAPICall_t hAPICall = SteamUGC.SendQueryUGCRequest(handle);
		CallResult<SteamUGCQueryCompleted_t> callResult = CallResult<SteamUGCQueryCompleted_t>.Create(onDone);
		callResult.Set(hAPICall);
		gcDontKill.Add(callResult);
		void onDone(SteamUGCQueryCompleted_t data, bool failure)
		{
			for (uint num = 0u; num < data.m_unNumResultsReturned; num++)
			{
				if (SteamUGC.GetQueryUGCResult(data.m_handle, num, out var pDetails) && pDetails.m_eResult == EResult.k_EResultOK && SteamUGC.GetQueryUGCPreviewURL(data.m_handle, num, out var pchURL, 5000u))
				{
					SteamUGC.GetItemInstallInfo(pDetails.m_nPublishedFileId, out var _, out var pchFolder, 5000u, out var _);
					DateTime modified = DateTime.Now;
					if (Directory.Exists(pchFolder))
					{
						modified = Directory.GetCreationTime(pchFolder);
					}
					string text = pDetails.m_nPublishedFileId.ToString();
					ESUGC.cachedRoomData.Remove(text);
					ESUGC.cachedRoomData.Add(text, new CachedRoomData
					{
						name = pDetails.m_rgchTitle,
						imageUrl = pchURL,
						modified = modified,
						installLocation = pchFolder
					});
					onGetNameAndURL(new RefreshRoomData(text, pDetails.m_rgchTitle, pchURL, modified, this));
				}
			}
			SteamUGC.ReleaseQueryUGCRequest(data.m_handle);
		}
	}

	private void onItemNeedsUpdate(RemoteStoragePublishedFileUpdated_t param)
	{
		onChange(param.m_nPublishedFileId.m_PublishedFileId.ToString(), ESUGCChange.Modify, this);
	}

	private void onItemInstalled(ItemInstalled_t param)
	{
		string id = param.m_nPublishedFileId.m_PublishedFileId.ToString();
		onChange(id, ESUGCChange.Installed, this);
		installingRooms.RemoveAll((string x) => x == id);
	}

	private void onItemUnsubscribed(RemoteStoragePublishedFileUnsubscribed_t param)
	{
		string id = param.m_nPublishedFileId.m_PublishedFileId.ToString();
		onChange(id, ESUGCChange.Uninstalled, this);
		uninstallingRooms.RemoveAll((string x) => x == id);
	}

	private void downloadItemResult(DownloadItemResult_t param)
	{
		string id = param.m_nPublishedFileId.m_PublishedFileId.ToString();
		onChange(id, ESUGCChange.Installed, this);
		installingRooms.RemoveAll((string x) => x == id);
	}

	private void onItemSubscribed(RemoteStoragePublishedFileSubscribed_t param)
	{
		string id = param.m_nPublishedFileId.m_PublishedFileId.ToString();
		onChange(id, ESUGCChange.Modify, this);
		installingRooms.RemoveAll((string x) => x == id);
	}
}

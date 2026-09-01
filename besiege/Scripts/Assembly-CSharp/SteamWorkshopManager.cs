using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using BesiegeDlc;
using GameGrind;
using InternalModding.Workshop;
using Localisation;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Internal;
using PlayFab.Party;
using Steamworks;
using UnityEngine;

public class SteamWorkshopManager : WorkshopManager
{
	public class SteamItem : WorkshopItem
	{
		public PublishedFileId_t publishedFileId;

		public UGCHandle_t preview;

		public SteamItem(string title, PublishedFileId_t publishedFileId, UGCHandle_t preview)
			: base(title)
		{
			WorkshopId = publishedFileId.m_PublishedFileId;
			this.publishedFileId = publishedFileId;
			this.preview = preview;
		}
	}

	private class PendingUpload
	{
		public PublishedFileId_t fileId;

		public string title;

		public string path;

		public string thumbnailPath;

		public List<string> tags;

		public ItemTypes itemType;

		public FileInfo file;

		public FileInfo thumbnail;

		public string name;

		public UploadVisibility visibility;

		public uint dlcDependencyMask;
	}

	private static Dictionary<ulong, PendingUpload> pendingUploads = new Dictionary<ulong, PendingUpload>();

	protected AppId_t appid = new AppId_t(346010u);

	protected Callback<RemoteStoragePublishedFileSubscribed_t> subCallback;

	protected Callback<UserStatsReceived_t> statsCallback;

	protected Callback<SteamServersConnected_t> connectedCallback;

	protected Callback<SteamServersDisconnected_t> disconnectedCallback;

	protected Callback<SteamServerConnectFailure_t> failedConnectionCallback;

	protected Callback<DownloadItemResult_t> downloadItemCallback;

	protected MultiCallResult<RemoteStorageFileWriteAsyncComplete_t> cloudWriteCallback;

	protected MultiCallResult<RemoteStorageFileReadAsyncComplete_t> cloudReadCallback;

	protected MultiCallResult<SteamUGCQueryCompleted_t> queryUserUGCResult;

	protected MultiCallResult<CreateItemResult_t> createItemCallResult;

	protected MultiCallResult<SubmitItemUpdateResult_t> submitItemResult;

	protected MultiCallResult<RemoteStorageUnsubscribePublishedFileResult_t> unsubscribeResult;

	protected MultiCallResult<RemoteStorageDownloadUGCResult_t> remoteStorageDownloadCallResult;

	private Queue<PublishedFileId_t> handleSubscribeItems = new Queue<PublishedFileId_t>();

	private PlayFabAuthenticationContext authenticationContext;

	private bool hasPlayfabId;

	private string playfabNetworkId;

	public static bool UpdateExistingContent = false;

	public override string Name
	{
		get
		{
			return "BesiegeWorkshopManager";
		}
	}

	protected override void Awake()
	{
		if (!SteamManager.Initialized)
		{
			Debug.Log("[SteamWorkshopManager] SteamManager not initialized, destroying myself.");
			UnityEngine.Object.Destroy(this);
		}
		else
		{
			base.Awake();
			RegisterCallbacks();
		}
	}

	protected override void Start()
	{
		base.Start();
		SteamUserStats.RequestCurrentStats();
		PlayFabWebRequest.SkipCertificateValidation();
		if (OptionsMaster.BesiegeConfig.CloudSaving)
		{
			SyncRemoteFiles();
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		UnregisterCallbacks();
	}

	public override void CreateWorkshopMod(UploadData uploadData)
	{
		CreateWorkshopContainer(ItemTypes.Mods, uploadData.Name, uploadData.Path, uploadData.ThumbnailPath, uploadData.IsFolder, uploadData.Tags, uploadData.Visibility, uploadData.DlcDependencyMask);
	}

	public override void CreateWorkshopSkin(UploadData uploadData)
	{
		CreateWorkshopContainer(ItemTypes.Skins, uploadData.Name, uploadData.Path, uploadData.ThumbnailPath, uploadData.IsFolder, uploadData.Tags, uploadData.Visibility, uploadData.DlcDependencyMask);
	}

	public override void CreateWorkshopMachine(UploadData uploadData)
	{
		CreateWorkshopContainer(ItemTypes.Machines, uploadData.Name, uploadData.Path, uploadData.ThumbnailPath, uploadData.IsFolder, uploadData.Tags, uploadData.Visibility, uploadData.DlcDependencyMask);
	}

	public override void CreateWorkshopLevel(UploadData uploadData)
	{
		CreateWorkshopContainer(ItemTypes.Levels, uploadData.Name, uploadData.Path, uploadData.ThumbnailPath, uploadData.IsFolder, uploadData.Tags, uploadData.Visibility, uploadData.DlcDependencyMask);
	}

	public override void Download(ulong workshopFileId)
	{
		PublishedFileId_t item = (PublishedFileId_t)workshopFileId;
		if (!handleSubscribeItems.Contains(item))
		{
			handleSubscribeItems.Enqueue(item);
		}
	}

	public override void Unsubscribe(ulong id)
	{
		SteamAPICall_t apiCallHandle = SteamUGC.UnsubscribeItem((PublishedFileId_t)id);
		unsubscribeResult.Set(apiCallHandle);
	}

	private MultiCallResult<SubmitItemUpdateResult_t>.APIDispatchDelegate GetUploadCallback(PublishedFileId_t fileId, string path, List<string> tags, ItemTypes itemType, string uploadFolder)
	{
		return delegate(SteamAPICall_t callHandle, SubmitItemUpdateResult_t pCallback, bool bIOFailure)
		{
			if (bIOFailure)
			{
				Debug.Log("IOFailure on OnUploadToWorkshop");
			}
			else if (pCallback.m_eResult != EResult.k_EResultOK)
			{
				Debug.Log(pCallback.m_eResult);
				DisplayErrorPopup(pCallback.m_eResult);
			}
			else
			{
				string pchURL = "http://steamcommunity.com/sharedfiles/filedetails/?id=" + fileId.m_PublishedFileId;
				try
				{
					if (itemType == ItemTypes.Machines && SingleInstance<AchievementManager>.hasInstance())
					{
						Journal.Increment(4, 1);
					}
					DisplayPopup(LocalisationManager.GetTranslation(2972));
					SteamFriends.ActivateGameOverlayToWebPage(pchURL);
				}
				catch (Exception ex)
				{
					DisplayPopup(LocalisationManager.GetTranslation(2971));
					Debug.Log("Workshop: Failed to upload: " + ex);
				}
				if (itemType == ItemTypes.Skins || (tags != null && tags.IndexOf("Skin Packs") >= 0))
				{
					Debug.Log("Writing to file: " + path + "/workshopid.txt with " + fileId.m_PublishedFileId);
					try
					{
						StreamWriter streamWriter = new StreamWriter(path + "/workshopid.txt", false);
						streamWriter.WriteLine(fileId.m_PublishedFileId);
						streamWriter.Close();
					}
					catch
					{
						Debug.LogWarning("Could not write workshopid.txt for item " + fileId.m_PublishedFileId);
					}
				}
			}
		};
	}

	public void CreateOrUpdateItem(PublishedFileId_t fileId, string itemName, string path, string thumbnailPath, bool isFolder, List<string> tags, ItemTypes itemType, UploadVisibility uploadVisibility, uint dlcDependencyMask)
	{
		if (SingleInstance<StatMaster>.Instance.DisableWorkShopUploads)
		{
			Debug.Log("Workshop Upload disabled");
			return;
		}
		try
		{
			FileInfo file = null;
			if (!string.IsNullOrEmpty(path))
			{
				file = new FileInfo(path);
			}
			FileInfo fileInfo = null;
			if (!string.IsNullOrEmpty(thumbnailPath))
			{
				Debug.Log("uploading thumbnail: " + thumbnailPath);
				fileInfo = new FileInfo(thumbnailPath);
			}
			CreateUploadFolder();
			string text = "missing";
			if (isFolder)
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(path);
				Directory.CreateDirectory(uploadFolder + "/" + directoryInfo.Name);
				DirectoryInfo destination = new DirectoryInfo(uploadFolder + "/" + directoryInfo.Name);
				text = directoryInfo.Name;
				if (File.Exists(path + "/workshopid.txt"))
				{
					File.Delete(path + "/workshopid.txt");
				}
				IOHelper.CopyFolderOverwrite(directoryInfo, destination);
			}
			else if (file != null)
			{
				if (!UpdateExistingContent)
				{
					text = Path.GetFileNameWithoutExtension(file.Name);
					file.CopyTo(uploadFolder + file.Name);
				}
				else
				{
					text = itemName;
					file.CopyTo(uploadFolder + text + file.Extension);
				}
			}
			else if (!UpdateExistingContent)
			{
				if (fileInfo != null)
				{
					text = fileInfo.Name.Replace(fileInfo.Extension, string.Empty);
				}
			}
			else
			{
				uint cchFolderSize = 1000u;
				ulong punSizeOnDisk;
				uint punTimeStamp;
				if (!SteamUGC.GetItemInstallInfo(fileId, out punSizeOnDisk, out path, cchFolderSize, out punTimeStamp) || !TransferFileToUpload(path, uploadFolder, ref file, ref text))
				{
					Debug.Log("start: " + fileId);
					PendingUpload pendingUpload = new PendingUpload();
					pendingUpload.fileId = fileId;
					pendingUpload.title = itemName;
					pendingUpload.path = path;
					pendingUpload.thumbnailPath = thumbnailPath;
					pendingUpload.tags = tags;
					pendingUpload.itemType = itemType;
					pendingUpload.file = file;
					pendingUpload.thumbnail = fileInfo;
					pendingUpload.name = text;
					pendingUpload.visibility = uploadVisibility;
					pendingUpload.dlcDependencyMask = dlcDependencyMask;
					PendingUpload value = pendingUpload;
					pendingUploads.Add(fileId.m_PublishedFileId, value);
					SteamUGC.DownloadItem(fileId, true);
					DisplayPopup(string.Format(LocalisationManager.GetTranslation(2975), itemName), true);
					return;
				}
			}
			UploadItem(fileId, itemName, path, thumbnailPath, tags, itemType, file, fileInfo, text, uploadVisibility, dlcDependencyMask);
		}
		catch (Exception ex)
		{
			DisplayPopup(LocalisationManager.GetTranslation(2971));
			Debug.LogError("Workshop: Error in creating item: " + ex);
		}
	}

	private void UploadItem(PublishedFileId_t fileId, string title, string path, string thumbnailPath, List<string> tags, ItemTypes itemType, FileInfo file, FileInfo thumbnail, string thumbnailFileName, UploadVisibility uploadVisibility, uint dlcDependencyMask)
	{
		MultiCallResult<SubmitItemUpdateResult_t>.APIDispatchDelegate uploadCallback = GetUploadCallback(fileId, path, tags, itemType, uploadFolder);
		if (thumbnail != null && thumbnail.Exists)
		{
			thumbnail.CopyTo(uploadFolder + thumbnailFileName + thumbnail.Extension);
		}
		UGCUpdateHandle_t uGCUpdateHandle_t = SteamUGC.StartItemUpdate(appid, fileId);
		if (title != null)
		{
			SteamUGC.SetItemTitle(uGCUpdateHandle_t, title);
		}
		if (!UpdateExistingContent)
		{
			int num = 0;
			if (num > 0)
			{
				SteamUGC.SetItemDescription(uGCUpdateHandle_t, "Block Count: " + num);
			}
		}
		ERemoteStoragePublishedFileVisibility storageVisibility = GetStorageVisibility(uploadVisibility);
		SteamUGC.SetItemVisibility(uGCUpdateHandle_t, storageVisibility);
		List<uint> dlcTypesFromMask = DlcManager.Instance.GetDlcTypesFromMask(~dlcDependencyMask);
		foreach (uint item in dlcTypesFromMask)
		{
			AppId_t appId_t = (AppId_t)DlcManager.Instance.GetDlcPlatformID(item);
			if (!AppId_t.Invalid.Equals(appId_t))
			{
				SteamUGC.RemoveAppDependency(fileId, appId_t);
			}
		}
		List<uint> dlcTypesFromMask2 = DlcManager.Instance.GetDlcTypesFromMask(dlcDependencyMask);
		foreach (uint item2 in dlcTypesFromMask2)
		{
			AppId_t appId_t2 = (AppId_t)DlcManager.Instance.GetDlcPlatformID(item2);
			if (!AppId_t.Invalid.Equals(appId_t2))
			{
				Debug.LogFormat("[SteamWorkshopManager] Adding dlc dependency id: {0}", appId_t2);
				SteamUGC.AddAppDependency(fileId, appId_t2);
			}
		}
		string metadataForItem = WorkshopManager.GetMetadataForItem(dlcDependencyMask);
		if (!string.IsNullOrEmpty(metadataForItem))
		{
			Debug.LogFormat("Setting metadata to: {0}", metadataForItem);
			SteamUGC.SetItemMetadata(uGCUpdateHandle_t, metadataForItem);
		}
		if (tags != null)
		{
			switch (itemType)
			{
			case ItemTypes.Machines:
				tags.Add("Machines");
				break;
			case ItemTypes.Skins:
				tags.Add("Skin Packs");
				break;
			case ItemTypes.Levels:
				tags.Add("Levels");
				break;
			case ItemTypes.Mods:
				tags.Add("Mods");
				break;
			}
			Debug.LogFormat("Using tags: {0}", string.Join(",", tags.ToArray()));
			SteamUGC.SetItemTags(uGCUpdateHandle_t, tags);
		}
		if (file != null)
		{
			SteamUGC.SetItemContent(uGCUpdateHandle_t, uploadFolder);
		}
		if (thumbnail != null)
		{
			SteamUGC.SetItemPreview(uGCUpdateHandle_t, thumbnailPath);
		}
		SteamAPICall_t apiCallHandle = SteamUGC.SubmitItemUpdate(uGCUpdateHandle_t, string.Empty);
		submitItemResult.Set(apiCallHandle, uploadCallback);
		DisplayPopup(string.Format(LocalisationManager.GetTranslation(2975), title), true);
	}

	private ERemoteStoragePublishedFileVisibility GetStorageVisibility(UploadVisibility visibility)
	{
		switch (visibility)
		{
		case UploadVisibility.Public:
			return ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPublic;
		case UploadVisibility.Private:
			return ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPrivate;
		case UploadVisibility.FriendsOnly:
			return ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityFriendsOnly;
		default:
			return ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPrivate;
		}
	}

	private static bool TransferFileToUpload(string path, string uploadFolder, ref FileInfo file, ref string name)
	{
		DirectoryInfo directoryInfo = new DirectoryInfo(path);
		if (!directoryInfo.Exists)
		{
			return false;
		}
		FileInfo[] files = directoryInfo.GetFiles();
		FileInfo[] array = files;
		foreach (FileInfo fileInfo in array)
		{
			if (fileInfo.Extension.ToLower() == ".bsg")
			{
				path = fileInfo.FullName;
				file = new FileInfo(path);
				name = file.Name.Replace(file.Extension, string.Empty);
				file.CopyTo(uploadFolder + file.Name);
				break;
			}
		}
		return true;
	}

	public void GetPreviewThumbnail(WorkshopItem item, Action<Texture2D> thumbnailDownloadedCallback)
	{
		GetPreviewThumbnail(item, thumbnailDownloadedCallback);
	}

	public void GetPreviewThumbnail(UGCHandle_t handle, Action<Texture2D> thumbnailDownloadedCallback)
	{
		if (thumbnailDownloadedCallback == null)
		{
			Debug.LogError("Invalid thumbnail");
			return;
		}
		MultiCallResult<RemoteStorageDownloadUGCResult_t>.APIDispatchDelegate handler = delegate(SteamAPICall_t callHandle, RemoteStorageDownloadUGCResult_t pCallback, bool bIOFailure)
		{
			if (bIOFailure)
			{
				Debug.Log("Workshop: IOFailure in GetPreviewThumbnail");
			}
			else if (pCallback.m_eResult == EResult.k_EResultOK)
			{
				byte[] array = new byte[pCallback.m_nSizeInBytes];
				try
				{
					SteamRemoteStorage.UGCRead(pCallback.m_hFile, array, array.Length, 0u, EUGCReadAction.k_EUGCRead_Close);
					Texture2D texture2D = new Texture2D(512, 512);
					texture2D.LoadImage(array);
					if (thumbnailDownloadedCallback != null)
					{
						thumbnailDownloadedCallback(texture2D);
					}
				}
				catch (Exception ex)
				{
					Debug.Log("Workshop: Error on downloading preview thumbnail: " + ex);
				}
			}
		};
		SteamAPICall_t apiCallHandle = SteamRemoteStorage.UGCDownload(handle, 0u);
		remoteStorageDownloadCallResult.Set(apiCallHandle, handler);
	}

	public override void GetSubscribedWorkshopItemsAsync(ItemTypes itemType, InstallType installType, Action<List<WorkshopItem>> callbackHandler)
	{
		GetAllWorkshopItems(itemType, ItemListing.Subscribed, installType, callbackHandler);
	}

	public override void GetPublishedWorkshopItemsAsync(ItemTypes itemType, Action<List<WorkshopItem>> callbackHandler)
	{
		GetAllWorkshopItems(itemType, ItemListing.Published, InstallType.All, callbackHandler);
	}

	private void GetAllWorkshopItems(ItemTypes itemType, ItemListing listType, InstallType installType, Action<List<WorkshopItem>> fetchItemsCallback)
	{
		if (BesiegeLogFilter.logDev)
		{
			Debug.LogFormat("[SteamWorkshopManager::GetAllWorkshopItems] itemType: {0}, listType: {1}", itemType, listType);
		}
		StartCoroutine(GetAllWorkshopItemsIE(itemType, listType, installType, fetchItemsCallback));
	}

	protected override void SetupWorkshopSkins()
	{
		if (SteamManager.Initialized && !loadedWorkshopSkinsAlready)
		{
			RefreshSkins();
			loadedWorkshopSkinsAlready = true;
		}
	}

	protected override void RefreshSkins()
	{
		GetAllWorkshopItems(ItemTypes.Skins, ItemListing.Subscribed, InstallType.Installed, base.PassToSkinLoader);
	}

	private void OnSubsubscribedCallback(RemoteStoragePublishedFileSubscribed_t pCallback)
	{
		handleSubscribeItems.Enqueue(pCallback.m_nPublishedFileId);
	}

	private void OnSteamDisconnectedCallback(SteamServersDisconnected_t pCallback)
	{
		WorkshopManager.Offline = true;
		DisplayPopup(LocalisationManager.GetTranslation(2973));
	}

	private void OnSteamConnectedCallback(SteamServersConnected_t pCallback)
	{
		Debug.Log("Connected to Steam");
		if (WorkshopManager.Offline)
		{
			DisplayPopup(LocalisationManager.GetTranslation(2974));
		}
		WorkshopManager.Offline = false;
	}

	private void OnSteamConnectionFailureCallback(SteamServerConnectFailure_t pCallback)
	{
		WorkshopManager.Offline = true;
		DisplayPopup(LocalisationManager.GetTranslation(2973));
	}

	private void OnUnsubscribedCallback(RemoteStorageUnsubscribePublishedFileResult_t pCallback, bool bIOFailure)
	{
		if (bIOFailure)
		{
			Debug.Log("Failed to unsubscribe from workshop item (IOFailure)");
		}
		else if (pCallback.m_eResult != EResult.k_EResultOK)
		{
			Debug.Log("Failed to unsubscribe from workshop item: " + pCallback.m_eResult);
		}
		else
		{
			Debug.Log("Successfully unsubscribed");
		}
	}

	private void GetNewerItem(PublishedFileId_t publishedFileId)
	{
		if (SteamUGC.DownloadItem(publishedFileId, true))
		{
			DisplayPopup(LocalisationManager.GetTranslation(2967), true);
			return;
		}
		Debug.Log("Item is already on disk");
		QueryWorkshopItemDetails(publishedFileId);
	}

	private void Update()
	{
		if (!gettingNewerItem && handleSubscribeItems.Count > 0)
		{
			GetNewerItem(handleSubscribeItems.Dequeue());
			gettingNewerItem = true;
		}
	}

	private void DisposeCallback(IDisposable callback)
	{
		if (callback != null)
		{
			callback.Dispose();
		}
	}

	private void OnUserStatsCallback(UserStatsReceived_t stats)
	{
		SteamAchievementSystem steamAchievementSystem = BaseAchievementSystem.Instance as SteamAchievementSystem;
		if (steamAchievementSystem != null)
		{
			steamAchievementSystem.ProcessStats(stats);
		}
	}

	private void OnItemDownloadResult(DownloadItemResult_t pCallback)
	{
		if (pCallback.m_eResult != EResult.k_EResultOK)
		{
			DisplayErrorPopup(pCallback.m_eResult);
			Debug.Log("Error in downloading item, please try again later");
			gettingNewerItem = false;
		}
		else
		{
			QueryWorkshopItemDetails(pCallback.m_nPublishedFileId);
		}
	}

	private void OnWorkshopItemQueried(SteamAPICall_t callHandle, SteamUGCQueryCompleted_t pCallback, bool bIOFailure)
	{
		SteamItem steamItem = null;
		if (bIOFailure)
		{
			MonoBehaviour.print("IOFailure in OnQueryReturn");
			gettingNewerItem = false;
			return;
		}
		if (pCallback.m_eResult != EResult.k_EResultOK)
		{
			Debug.Log("Workshop: Returned result is not ok, something went wrong");
			DisplayErrorPopup(pCallback.m_eResult);
			gettingNewerItem = false;
			return;
		}
		uint num = 0u;
		try
		{
			SteamUGCDetails_t pDetails;
			if (SteamUGC.GetQueryUGCResult(pCallback.m_handle, num, out pDetails))
			{
				string rgchTags = pDetails.m_rgchTags;
				string title = pDetails.m_rgchTitle.Replace('/', '\\');
				bool flag = pDetails.m_ulSteamIDOwner == SteamUser.GetSteamID().m_SteamID;
				string itemQueryMetadata = GetItemQueryMetadata(pCallback.m_handle, num);
				bool areDlcRequirementsMet = true;
				uint dlcDependencyMask;
				WorkshopManager.ParseItemMetadata(itemQueryMetadata, out dlcDependencyMask);
				if (dlcDependencyMask != 0)
				{
					areDlcRequirementsMet = DlcManager.Instance.HasPurchasedDlcMask(dlcDependencyMask);
				}
				SteamItem steamItem2 = new SteamItem(title, pDetails.m_nPublishedFileId, pDetails.m_hPreviewFile);
				steamItem2.IsOwner = flag;
				steamItem2.Author = pDetails.m_ulSteamIDOwner;
				steamItem2.SubscribeTime = ((!flag) ? pDetails.m_rtimeAddedToUserList : pDetails.m_rtimeCreated);
				steamItem2.Tags = rgchTags;
				steamItem2.DlcDependencyMask = dlcDependencyMask;
				steamItem2.AreDlcRequirementsMet = areDlcRequirementsMet;
				steamItem = steamItem2;
				if (rgchTags.IndexOf("Skin Packs") >= 0)
				{
					steamItem.ItemType = ItemTypes.Skins;
				}
				else if (rgchTags.IndexOf("Levels") >= 0)
				{
					steamItem.ItemType = ItemTypes.Levels;
				}
				else if (rgchTags.IndexOf("Mods") >= 0)
				{
					steamItem.ItemType = ItemTypes.Mods;
				}
				else
				{
					steamItem.ItemType = ItemTypes.Machines;
				}
				steamItem.Source = ItemListing.Subscribed;
			}
		}
		catch (Exception message)
		{
			Debug.Log(message);
		}
		if (steamItem != null)
		{
			if (!string.IsNullOrEmpty(steamItem.Title))
			{
				DisplayPopup(string.Format(LocalisationManager.GetTranslation(2968), steamItem.Title));
			}
			uint cchFolderSize = 1000u;
			ulong punSizeOnDisk;
			string pchFolder;
			uint punTimeStamp;
			if (SteamUGC.GetItemInstallInfo(steamItem.publishedFileId, out punSizeOnDisk, out pchFolder, cchFolderSize, out punTimeStamp))
			{
				steamItem.RootFolder = pchFolder;
				steamItem.IsInstalled = true;
				CheckPendingUploads(steamItem.publishedFileId.m_PublishedFileId, pchFolder);
			}
			if (steamItem.ItemType == ItemTypes.Skins)
			{
				NewSkinDownloaded(steamItem);
			}
			else if (steamItem.ItemType == ItemTypes.Mods)
			{
				ModWorkshopManager.OnNewModInstalled(steamItem);
			}
			else if (OnSubscribe != null)
			{
				OnSubscribe();
			}
		}
		else if (OnSubscribe != null)
		{
			OnSubscribe();
		}
		gettingNewerItem = false;
	}

	private void CheckPendingUploads(ulong id, string path)
	{
		if (pendingUploads.ContainsKey(id))
		{
			UploadPendingItem(pendingUploads[id], path);
			pendingUploads.Remove(id);
		}
	}

	private void UploadPendingItem(PendingUpload upload, string path)
	{
		if (TransferFileToUpload(path, uploadFolder, ref upload.file, ref upload.name))
		{
			UploadItem(upload.fileId, upload.title, path, upload.thumbnailPath, upload.tags, upload.itemType, upload.file, upload.thumbnail, upload.name, upload.visibility, upload.dlcDependencyMask);
		}
	}

	private void QueryWorkshopItemDetails(PublishedFileId_t publishedFileId)
	{
		PublishedFileId_t[] array = new PublishedFileId_t[1] { publishedFileId };
		UGCQueryHandle_t handle = SteamUGC.CreateQueryUGCDetailsRequest(array, (uint)array.Length);
		SteamUGC.SetReturnMetadata(handle, true);
		SteamAPICall_t apiCallHandle = SteamUGC.SendQueryUGCRequest(handle);
		queryUserUGCResult.Set(apiCallHandle, OnWorkshopItemQueried);
	}

	private IEnumerator GetAllWorkshopItemsIE(ItemTypes itemType, ItemListing listType, InstallType installType, Action<List<WorkshopItem>> fetchItemsCallback)
	{
		List<WorkshopItem> list = new List<WorkshopItem>();
		bool end = false;
		float endTime = Time.time + 3600f;
		int offset = 0;
		uint numTotalItemsProcessed = 0u;
		Action<List<WorkshopItem>, uint, uint> itemReturn = null;
		if (BesiegeLogFilter.logDev)
		{
			Debug.LogFormat("[SteamWorkshopManager::GetAllWorkshopItemsIE] called");
		}
		ItemTypes itemType2 = default(ItemTypes);
		ItemListing listType2 = default(ItemListing);
		InstallType installType2 = default(InstallType);
		itemReturn = delegate(List<WorkshopItem> itemList, uint numItemsReturned, uint numTotalResults)
		{
			numTotalItemsProcessed += numItemsReturned;
			if (BesiegeLogFilter.logDev)
			{
				Debug.LogFormat("[SteamWorkshopManager::GetAllWorkshopItemsIE::itemReturn] callback called, count: {0}, items returned: {1}, {2}/{3}", itemList.Count, numItemsReturned, numTotalItemsProcessed, numTotalResults);
			}
			foreach (WorkshopItem item in itemList)
			{
				list.Add(item);
			}
			if (numTotalItemsProcessed == numTotalResults || numItemsReturned == 0)
			{
				end = true;
			}
			else
			{
				offset++;
				GetWorkshopItems(offset, itemType2, listType2, installType2, itemReturn);
			}
		};
		GetWorkshopItems(offset, itemType, listType, installType, itemReturn);
		if (BesiegeLogFilter.logDev)
		{
			Debug.LogFormat("[SteamWorkshopManager::GetAllWorkshopItemsIE] started waiting...");
		}
		while (!end && Time.time < endTime)
		{
			yield return null;
		}
		end = true;
		if (BesiegeLogFilter.logDev)
		{
			Debug.LogFormat("[SteamWorkshopManager::GetAllWorkshopItemsIE] waiting is done");
		}
		fetchItemsCallback(list);
	}

	private void GetWorkshopItems(int offset, ItemTypes itemType, ItemListing listType, InstallType installType, Action<List<WorkshopItem>, uint, uint> fetchItemsCallback)
	{
		MultiCallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate handler = delegate(SteamAPICall_t callHandle, SteamUGCQueryCompleted_t pCallback, bool bIOFailure)
		{
			List<WorkshopItem> list = new List<WorkshopItem>();
			if (BesiegeLogFilter.logDev)
			{
				Debug.LogFormat("[SteamWorkshopManager::GetWorkshopItems::queryCallback] itemType: {0}, listType: {1}, bIOFailure: {2}, m_eResult: {3}, m_unNumResultsReturned: {4}, m_unTotalMatchingResults: {5}", itemType, listType, bIOFailure, pCallback.m_eResult, pCallback.m_unNumResultsReturned, pCallback.m_unTotalMatchingResults);
			}
			if (bIOFailure)
			{
				MonoBehaviour.print("IOFailure in OnQueryReturn");
			}
			else if (pCallback.m_eResult != EResult.k_EResultOK)
			{
				Debug.Log("Workshop: Returned result is not ok, something went wrong");
				DisplayErrorPopup(pCallback.m_eResult);
			}
			else
			{
				uint unTotalMatchingResults = pCallback.m_unTotalMatchingResults;
				uint unNumResultsReturned = pCallback.m_unNumResultsReturned;
				SteamUGCDetails_t pDetails;
				for (uint num = 0u; num < unNumResultsReturned && SteamUGC.GetQueryUGCResult(pCallback.m_handle, num, out pDetails); num++)
				{
					string rgchTags = pDetails.m_rgchTags;
					string title = pDetails.m_rgchTitle.Replace('/', '\\');
					bool flag = pDetails.m_ulSteamIDOwner == SteamUser.GetSteamID().m_SteamID;
					string itemQueryMetadata = GetItemQueryMetadata(pCallback.m_handle, num);
					uint dlcDependencyMask;
					WorkshopManager.ParseItemMetadata(itemQueryMetadata, out dlcDependencyMask);
					bool areDlcRequirementsMet = DlcManager.Instance.HasPurchasedDlcMask(dlcDependencyMask);
					SteamItem steamItem = new SteamItem(title, pDetails.m_nPublishedFileId, pDetails.m_hPreviewFile)
					{
						IsOwner = flag,
						Author = pDetails.m_ulSteamIDOwner,
						SubscribeTime = ((!flag) ? pDetails.m_rtimeAddedToUserList : pDetails.m_rtimeCreated),
						Tags = rgchTags,
						ItemType = itemType,
						Source = listType,
						DlcDependencyMask = dlcDependencyMask,
						AreDlcRequirementsMet = areDlcRequirementsMet
					};
					uint cchFolderSize = 1000u;
					ulong punSizeOnDisk;
					string pchFolder;
					uint punTimeStamp;
					if (SteamUGC.GetItemInstallInfo(pDetails.m_nPublishedFileId, out punSizeOnDisk, out pchFolder, cchFolderSize, out punTimeStamp))
					{
						steamItem.RootFolder = pchFolder;
						DirectoryInfo directoryInfo = new DirectoryInfo(pchFolder);
						if (directoryInfo.Exists)
						{
							steamItem.IsInstalled = directoryInfo.GetFileSystemInfos().Length > 0;
						}
					}
					if (!steamItem.IsInstalled)
					{
						if (installType == InstallType.Installed)
						{
							continue;
						}
					}
					else
					{
						if (installType == InstallType.NotInstalled)
						{
							continue;
						}
						if (listType == ItemListing.Subscribed)
						{
							ImportItemData(steamItem, pchFolder);
						}
					}
					if (listType == ItemListing.Published || itemType == ItemTypes.Levels || itemType == ItemTypes.Machines || itemType == ItemTypes.Skins || itemType == ItemTypes.Mods)
					{
						list.Add(steamItem);
					}
				}
				if (BesiegeLogFilter.logDev)
				{
					Debug.LogFormat("[SteamWorkshopManager::GetWorkshopItems::queryCallback] calling callback with {0} items", list.Count);
				}
				fetchItemsCallback(list, unNumResultsReturned, unTotalMatchingResults);
			}
		};
		CSteamID steamID = SteamUser.GetSteamID();
		if (offset >= 0)
		{
			UGCQueryHandle_t handle;
			if (listType == ItemListing.Published)
			{
				EUserUGCList eListType = EUserUGCList.k_EUserUGCList_Published;
				handle = SteamUGC.CreateQueryUserUGCRequest(steamID.GetAccountID(), eListType, EUGCMatchingUGCType.k_EUGCMatchingUGCType_Items, EUserUGCListSortOrder.k_EUserUGCListSortOrder_CreationOrderDesc, appid, appid, (uint)(1 + offset));
			}
			else
			{
				EUserUGCList eListType = EUserUGCList.k_EUserUGCList_Subscribed;
				handle = SteamUGC.CreateQueryUserUGCRequest(steamID.GetAccountID(), eListType, EUGCMatchingUGCType.k_EUGCMatchingUGCType_Items, EUserUGCListSortOrder.k_EUserUGCListSortOrder_SubscriptionDateDesc, appid, appid, (uint)(1 + offset));
			}
			SteamUGC.SetReturnMetadata(handle, true);
			if (itemType == ItemTypes.Skins)
			{
				SteamUGC.AddRequiredTag(handle, "Skin Packs");
			}
			else if (itemType == ItemTypes.Levels)
			{
				SteamUGC.AddRequiredTag(handle, "Levels");
			}
			else if (itemType == ItemTypes.Mods)
			{
				SteamUGC.AddRequiredTag(handle, "Mods");
			}
			else if (itemType == ItemTypes.Machines)
			{
				SteamUGC.AddExcludedTag(handle, "Skin Packs");
				SteamUGC.AddExcludedTag(handle, "Levels");
				SteamUGC.AddExcludedTag(handle, "Mods");
			}
			if (BesiegeLogFilter.logDev)
			{
				Debug.LogFormat("[SteamWorkshopManager::GetWorkshopItems] itemType: {0}, listType: {1}", itemType, listType);
			}
			SteamAPICall_t apiCallHandle = SteamUGC.SendQueryUGCRequest(handle);
			queryUserUGCResult.Set(apiCallHandle, handler);
		}
	}

	private string GetItemQueryMetadata(UGCQueryHandle_t queryHandle, uint queryItemIndex)
	{
		string pchMetadata;
		SteamUGC.GetQueryUGCMetadata(queryHandle, queryItemIndex, out pchMetadata, 5000u);
		if (pchMetadata == null)
		{
			return string.Empty;
		}
		return pchMetadata;
	}

	private void CreateWorkshopContainer(ItemTypes itemType, string title, string folderPath, string thumbnailPath, bool isFolder, List<string> tags, UploadVisibility uploadDataVisibility, uint dlcDependencyMask)
	{
		if (SingleInstance<StatMaster>.Instance.DisableWorkShopUploads)
		{
			Debug.Log("Workshop Upload disabled");
			return;
		}
		MultiCallResult<CreateItemResult_t>.APIDispatchDelegate handler = delegate(SteamAPICall_t callHandle, CreateItemResult_t pCallback, bool bIOFailure)
		{
			if (bIOFailure)
			{
				Debug.Log("IOFailure in OnCreateWorkshopItem");
			}
			else
			{
				if (pCallback.m_bUserNeedsToAcceptWorkshopLegalAgreement)
				{
					Debug.Log("Still needs to accept the workshop agreement...");
				}
				if (pCallback.m_bUserNeedsToAcceptWorkshopLegalAgreement)
				{
					DisplayPopup(LocalisationManager.GetTranslation(2969));
				}
				else if (pCallback.m_eResult != EResult.k_EResultOK)
				{
					DisplayErrorPopup(pCallback.m_eResult);
				}
				else
				{
					CreateOrUpdateItem(pCallback.m_nPublishedFileId, title, folderPath, thumbnailPath, isFolder, tags, itemType, uploadDataVisibility, dlcDependencyMask);
				}
			}
		};
		if (WorkshopManager.Offline)
		{
			DisplayPopup(LocalisationManager.GetTranslation(2970));
			return;
		}
		try
		{
			SteamAPICall_t apiCallHandle = SteamUGC.CreateItem(appid, EWorkshopFileType.k_EWorkshopFileTypeFirst);
			createItemCallResult.Set(apiCallHandle, handler);
		}
		catch (Exception exception)
		{
			if (Application.isPlaying)
			{
				DisplayPopup(LocalisationManager.GetTranslation(2971));
			}
			Debug.LogException(exception);
		}
	}

	private void DisplayErrorPopup(EResult result)
	{
		string message;
		switch (result)
		{
		case EResult.k_EResultNoConnection:
			message = LocalisationManager.GetTranslation(2976);
			break;
		case EResult.k_EResultInsufficientPrivilege:
			message = LocalisationManager.GetTranslation(2977);
			break;
		case EResult.k_EResultNotLoggedOn:
			message = LocalisationManager.GetTranslation(2978);
			break;
		case EResult.k_EResultTimeout:
			message = LocalisationManager.GetTranslation(2979);
			break;
		case EResult.k_EResultBanned:
			message = LocalisationManager.GetTranslation(2980);
			break;
		case EResult.k_EResultInvalidParam:
			message = LocalisationManager.GetTranslation(2981);
			break;
		case EResult.k_EResultFileNotFound:
			message = LocalisationManager.GetTranslation(2982);
			break;
		case EResult.k_EResultAccessDenied:
			message = LocalisationManager.GetTranslation(2983);
			break;
		case EResult.k_EResultServiceUnavailable:
			message = LocalisationManager.GetTranslation(2984);
			break;
		case EResult.k_EResultLimitExceeded:
			message = LocalisationManager.GetTranslation(2985);
			break;
		case EResult.k_EResultDuplicateRequest:
			message = LocalisationManager.GetTranslation(2986);
			break;
		case EResult.k_EResultIOFailure:
			message = LocalisationManager.GetTranslation(2987);
			break;
		case EResult.k_EResultServiceReadOnly:
			message = LocalisationManager.GetTranslation(2988);
			break;
		case EResult.k_EResultFail:
			message = LocalisationManager.GetTranslation(2989);
			break;
		default:
			message = string.Format(LocalisationManager.GetTranslation(2990), result);
			break;
		}
		Debug.Log(message);
		DisplayPopup(message);
	}

	public void RegisterCallbacks()
	{
		statsCallback = Callback<UserStatsReceived_t>.Create(OnUserStatsCallback);
		downloadItemCallback = Callback<DownloadItemResult_t>.Create(OnItemDownloadResult);
		failedConnectionCallback = Callback<SteamServerConnectFailure_t>.Create(OnSteamConnectionFailureCallback);
		disconnectedCallback = Callback<SteamServersDisconnected_t>.Create(OnSteamDisconnectedCallback);
		connectedCallback = Callback<SteamServersConnected_t>.Create(OnSteamConnectedCallback);
		subCallback = Callback<RemoteStoragePublishedFileSubscribed_t>.Create(OnSubsubscribedCallback);
		cloudWriteCallback = MultiCallResult<RemoteStorageFileWriteAsyncComplete_t>.Create();
		cloudReadCallback = MultiCallResult<RemoteStorageFileReadAsyncComplete_t>.Create();
		queryUserUGCResult = MultiCallResult<SteamUGCQueryCompleted_t>.Create();
		createItemCallResult = MultiCallResult<CreateItemResult_t>.Create();
		submitItemResult = MultiCallResult<SubmitItemUpdateResult_t>.Create();
		unsubscribeResult = MultiCallResult<RemoteStorageUnsubscribePublishedFileResult_t>.Create();
		remoteStorageDownloadCallResult = MultiCallResult<RemoteStorageDownloadUGCResult_t>.Create();
	}

	public override void GetRemoteFileList(Action<List<string>> onComplete)
	{
		List<string> list = new List<string>();
		int fileCount = SteamRemoteStorage.GetFileCount();
		for (int i = 0; i < fileCount; i++)
		{
			int pnFileSizeInBytes;
			string fileNameAndSize = SteamRemoteStorage.GetFileNameAndSize(i, out pnFileSizeInBytes);
			list.Add(fileNameAndSize);
		}
		onComplete(list);
	}

	public override void ReadRemoteFileAsync(string path, Action<string, bool, byte[]> onRemoteReadComplete)
	{
		MultiCallResult<RemoteStorageFileReadAsyncComplete_t>.APIDispatchDelegate handler = delegate(SteamAPICall_t callHandle, RemoteStorageFileReadAsyncComplete_t pCallback, bool bIOFailure)
		{
			byte[] array = null;
			bool flag = pCallback.m_eResult == EResult.k_EResultOK;
			if (flag)
			{
				array = new byte[pCallback.m_cubRead];
				if (!SteamRemoteStorage.FileReadAsyncComplete(callHandle, array, pCallback.m_cubRead))
				{
					flag = false;
				}
			}
			onRemoteReadComplete(path, flag, array);
		};
		cloudReadCallback.Set(SteamRemoteStorage.FileReadAsync(path, 0u, (uint)SteamRemoteStorage.GetFileSize(path)), handler);
	}

	public override void WriteRemoteFileAsync(string cloudPath, byte[] content, Action<string, bool> onRemoteWriteComplete)
	{
		MultiCallResult<RemoteStorageFileWriteAsyncComplete_t>.APIDispatchDelegate handler = delegate(SteamAPICall_t callHandle, RemoteStorageFileWriteAsyncComplete_t pCallback, bool bIOFailure)
		{
			if (pCallback.m_eResult == EResult.k_EResultOK)
			{
				Debug.Log("[SteamWorkshopManager] WriteRemoteFileAsync Wrote " + cloudPath + " (" + content.Length + " bytes)");
				onRemoteWriteComplete(cloudPath, true);
			}
			else
			{
				Debug.LogWarning("[SteamWorkshopManager] WriteRemoteFileAsync Write " + cloudPath + " failed!");
				onRemoteWriteComplete(cloudPath, false);
			}
		};
		cloudWriteCallback.Set(SteamRemoteStorage.FileWriteAsync(cloudPath, content, (uint)content.Length), handler);
	}

	public override void DeleteRemoteFileAsync(string cloudPath, Action<string, bool> onRemoteDeleteComplete)
	{
		bool arg = SteamRemoteStorage.FileDelete(cloudPath);
		onRemoteDeleteComplete(cloudPath, arg);
	}

	private string GetSteamAuthTicket()
	{
		byte[] array = new byte[1024];
		uint pcbTicket;
		SteamUser.GetAuthSessionTicket(array, array.Length, out pcbTicket);
		Array.Resize(ref array, (int)pcbTicket);
		StringBuilder stringBuilder = new StringBuilder();
		byte[] array2 = array;
		foreach (byte b in array2)
		{
			stringBuilder.AppendFormat("{0:x2}", b);
		}
		return stringBuilder.ToString();
	}

	public override string GetPlayerName()
	{
		if (!SteamManager.Initialized)
		{
			return LocalisationManager.GetTranslation(1947);
		}
		return Regex.Replace(SteamFriends.GetPersonaName(), "[^0-9a-zA-Z ]+", string.Empty);
	}

	public override void InitializePlayfabManager(PlayFabMultiplayerManager mpManager)
	{
		mpManager.Initialize();
	}

	public override void PlayfabSignin(Action<bool> onComplete)
	{
		if (pfSignedIn)
		{
			onComplete(true);
			return;
		}
		LoginWithSteamRequest loginWithSteamRequest = new LoginWithSteamRequest();
		loginWithSteamRequest.SteamTicket = GetSteamAuthTicket();
		loginWithSteamRequest.CreateAccount = true;
		LoginWithSteamRequest request = loginWithSteamRequest;
		PlayFabClientAPI.LoginWithSteam(request, delegate(LoginResult loginResult)
		{
			authenticationContext = new PlayFabAuthenticationContext(loginResult.SessionTicket, loginResult.EntityToken.ToString(), loginResult.PlayFabId, loginResult.EntityToken.Entity.Id, loginResult.EntityToken.Entity.Type);
			string playerName = GetPlayerName();
			UpdateUserTitleDisplayNameRequest request2 = new UpdateUserTitleDisplayNameRequest
			{
				AuthenticationContext = authenticationContext,
				DisplayName = playerName
			};
			PlayFabClientAPI.UpdateUserTitleDisplayName(request2, delegate(UpdateUserTitleDisplayNameResult result)
			{
				Debug.Log("[SteamWorkshopManager] PlayfabSignin t=" + Time.time + " Successfully changed display name to " + result.DisplayName + "!");
				pfSignedIn = true;
				onComplete(true);
			}, delegate(PlayFabError error)
			{
				Debug.LogError(string.Concat("[SteamWorkshopManager] PlayfabSignin t = ", Time.time, " Failed to update display name { error=", error.Error, " message=", error.ErrorMessage, " details=", error.ErrorDetails, " }!"));
				onComplete(false);
			});
			Debug.Log("[SteamWorkshopManager] PlayfabSignin t=" + Time.time + " Successfully logged in " + playerName + " (playfabId=" + loginResult.PlayFabId + ", sessionTicket=" + loginResult.SessionTicket + ", id=" + loginResult.EntityToken.Entity.Id + ", type=" + loginResult.EntityToken.Entity.Type + ")!");
		}, delegate(PlayFabError error)
		{
			Debug.LogError("[SteamWorkshopManager] PlayfabSignin t=" + Time.time + " Failed to log into PlayFab! Error: " + error.GenerateErrorReport());
			NetworkAuxAddPiece.Instance.hud.ShowMessage(LocalisationManager.GetTranslation(4153));
			onComplete(false);
		});
	}

	private bool SetConnectString(string playfabNetworkId)
	{
		string text = SteamUser.GetSteamID().ToString();
		bool flag = string.IsNullOrEmpty(playfabNetworkId);
		string text2 = (flag ? null : ("+pf_join " + text));
		if (!SteamFriends.SetRichPresence("connect", text2))
		{
			Debug.LogWarning("[SteamWorkshopManager] UpdateActivity Failed to set connection string to " + text2 + " (len=" + text2.Length + ")!");
			return false;
		}
		if (!flag)
		{
			int num = 200;
			string pchValue = playfabNetworkId.Substring(0, num);
			string pchValue2 = playfabNetworkId.Substring(num, playfabNetworkId.Length - num);
			SteamFriends.SetRichPresence("pfId", pchValue);
			SteamFriends.SetRichPresence("pfId2", pchValue2);
		}
		else
		{
			SteamFriends.SetRichPresence("pfId", null);
			SteamFriends.SetRichPresence("pfId2", null);
		}
		hasPlayfabId = !flag;
		return true;
	}

	public bool GetPlayfabNetworkId(CSteamID cSteamID, out string pfNetworkId)
	{
		string friendRichPresence = SteamFriends.GetFriendRichPresence(cSteamID, "pfId");
		string friendRichPresence2 = SteamFriends.GetFriendRichPresence(cSteamID, "pfId2");
		pfNetworkId = friendRichPresence + friendRichPresence2;
		Debug.Log(string.Concat("[SteamWorkshopManager] GetPlayfabNetworkId Got Playfab network ID from user ", cSteamID, ": ", pfNetworkId));
		return true;
	}

	public override void StartActivity(string pfNetworkId, bool isPublic)
	{
		int count = Playerlist.Players.Count;
		int maxPlayers = NetworkScene.ServerSettings.maxPlayers;
		playfabNetworkId = pfNetworkId;
		if (count < maxPlayers)
		{
			SetConnectString(playfabNetworkId);
		}
	}

	public override void UpdateActivity(bool isPublic)
	{
		int count = Playerlist.Players.Count;
		int maxPlayers = NetworkScene.ServerSettings.maxPlayers;
		if (count < maxPlayers)
		{
			if (!hasPlayfabId)
			{
				SetConnectString(playfabNetworkId);
			}
		}
		else if (hasPlayfabId)
		{
			SetConnectString(null);
		}
	}

	public override void DeleteActivity()
	{
		if (hasPlayfabId)
		{
			SetConnectString(null);
		}
	}

	public void UnregisterCallbacks()
	{
		DisposeCallback(statsCallback);
		statsCallback = null;
		DisposeCallback(downloadItemCallback);
		downloadItemCallback = null;
		DisposeCallback(failedConnectionCallback);
		failedConnectionCallback = null;
		DisposeCallback(disconnectedCallback);
		disconnectedCallback = null;
		DisposeCallback(connectedCallback);
		connectedCallback = null;
		DisposeCallback(subCallback);
		subCallback = null;
		DisposeCallback(cloudReadCallback);
		cloudReadCallback = null;
		DisposeCallback(cloudWriteCallback);
		cloudWriteCallback = null;
		DisposeCallback(queryUserUGCResult);
		queryUserUGCResult = null;
		DisposeCallback(createItemCallResult);
		createItemCallResult = null;
		DisposeCallback(submitItemResult);
		submitItemResult = null;
		DisposeCallback(unsubscribeResult);
		unsubscribeResult = null;
		DisposeCallback(remoteStorageDownloadCallResult);
		remoteStorageDownloadCallResult = null;
	}

	public override void UpdateWorkshopMachine(ulong workshopItemId, UploadData uploadData)
	{
		UpdateWorkshopItem(workshopItemId, uploadData);
	}

	public override void UpdateWorkshopSkin(ulong workshopItemId, UploadData uploadData)
	{
		UpdateWorkshopItem(workshopItemId, uploadData);
	}

	public override void UpdateWorkshopLevel(ulong workshopItemId, UploadData uploadData)
	{
		UpdateWorkshopItem(workshopItemId, uploadData);
	}

	public override void UpdateWorkshopMod(ulong workshopItemId, UploadData uploadData)
	{
		UpdateWorkshopItem(workshopItemId, uploadData);
	}

	private void UpdateWorkshopItem(ulong workshopItemId, UploadData uploadData)
	{
		if (!uploadData.UploadContent)
		{
			uploadData.Path = null;
		}
		if (!uploadData.UploadThumbnail)
		{
			switch (uploadData.ItemType)
			{
			case ItemTypes.Machines:
			case ItemTypes.Levels:
				uploadData.ThumbnailPath = null;
				break;
			case ItemTypes.Skins:
			case ItemTypes.Mods:
				if (!uploadData.UploadContent)
				{
					uploadData.ThumbnailPath = null;
				}
				break;
			default:
				uploadData.ThumbnailPath = null;
				break;
			}
		}
		UpdateExistingContent = uploadData.UploadContent;
		CreateOrUpdateItem((PublishedFileId_t)workshopItemId, uploadData.Name, uploadData.Path, uploadData.ThumbnailPath, uploadData.IsFolder, uploadData.Tags, uploadData.ItemType, uploadData.Visibility, uploadData.DlcDependencyMask);
		UpdateExistingContent = false;
	}
}

using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration.API
{
	public static class UserGeneratedContent
	{
		public static class Client
		{
			public class ImageData
			{
				public string Path;

				public byte[] Texture;
			}

			private struct ImageLoadRequest
			{
				public UGCHandle_t ImageFile;

				public Action<string, byte[]> Callback;
			}

			private static WorkshopDownloadedItemResultEvent _evtItemDownloaded;

			private static WorkshopItemInstalledEvent _evtItemInstalled;

			private static CallResult<AddAppDependencyResult_t> _addAppDependencyResults;

			private static CallResult<AddUGCDependencyResult_t> _addUgcDependencyResults;

			private static CallResult<UserFavoriteItemsListChanged_t> _userFavoriteItemsListChanged;

			private static CallResult<CreateItemResult_t> _createdItem;

			private static CallResult<DeleteItemResult_t> _deleteItem;

			private static CallResult<GetAppDependenciesResult_t> _appDependenciesResult;

			private static CallResult<GetUserItemVoteResult_t> _getUserItemVoteResult;

			private static CallResult<RemoveAppDependencyResult_t> _removeAppDependencyResult;

			private static CallResult<RemoveUGCDependencyResult_t> _removeDependencyResult;

			private static CallResult<SteamUGCRequestUGCDetailsResult_t> _steamUgcRequestUgcDetailsResult;

			private static CallResult<SteamUGCQueryCompleted_t> _steamUgcQueryCompleted;

			private static CallResult<SetUserItemVoteResult_t> _setUserItemVoteResult;

			private static CallResult<StartPlaytimeTrackingResult_t> _startPlaytimeTrackingResult;

			private static CallResult<StopPlaytimeTrackingResult_t> _stopPlaytimeTrackingResult;

			private static CallResult<SubmitItemUpdateResult_t> _submitItemUpdateResult;

			private static CallResult<RemoteStorageSubscribePublishedFileResult_t> _remoteStorageSubscribePublishedFileResult;

			private static CallResult<RemoteStorageUnsubscribePublishedFileResult_t> _remoteStorageUnsubscribePublishedFileResult;

			private static CallResult<WorkshopEULAStatus_t> _workshopEulaStatus;

			private static CallResult<RemoteStorageDownloadUGCResult_t> _remoteStorageDownloadUgcResult;

			private static Callback<DownloadItemResult_t> _downloadItem;

			private static Callback<ItemInstalled_t> _itemInstalled;

			private static readonly Dictionary<ulong, ImageData> MLoadedImages;

			private static readonly Queue<ImageLoadRequest> ImageLoadRequests;

			private static bool _imageProcessing;

			public static WorkshopDownloadedItemResultEvent OnItemDownloaded => null;

			public static WorkshopItemInstalledEvent OnWorkshopItemInstalled => null;

			[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
			private static void Init()
			{
			}

			public static void GetUgcImage(UGCHandle_t imageFile, Action<string, byte[]> callback)
			{
			}

			internal static void ImageWorker_Tick()
			{
			}

			public static bool CreateItem(WorkshopItemEditorData item, WorkshopItemPreviewFile[] additionalPreviews, string[] additionalYouTubeIds, WorkshopItemKeyValueTag[] additionalKeyValueTags, Action<WorkshopItemDataCreateStatus> completedCallback = null, Action<UGCUpdateHandle_t> uploadStartedCallback = null, Action<CreateItemResult_t> fileCreatedCallback = null)
			{
				return false;
			}

			public static bool UpdateItem(WorkshopItemEditorData item, WorkshopItemPreviewFile[] additionalPreviews, string[] additionalYouTubeIds, WorkshopItemKeyValueTag[] additionalKeyValueTags, Action<WorkshopItemDataUpdateStatus> callback = null, Action<UGCUpdateHandle_t> uploadStartedCallback = null)
			{
				return false;
			}

			public static void AddAppDependency(PublishedFileId_t fileId, AppId_t appId, Action<AddAppDependencyResult_t, bool> callback)
			{
			}

			public static void AddDependency(PublishedFileId_t parentFileId, PublishedFileId_t childFileId, Action<AddUGCDependencyResult_t, bool> callback)
			{
			}

			public static bool AddExcludedTag(UGCQueryHandle_t handle, string tagName)
			{
				return false;
			}

			public static bool AddItemKeyValueTag(UGCUpdateHandle_t handle, string key, string value)
			{
				return false;
			}

			public static bool AddItemPreviewFile(UGCUpdateHandle_t handle, string previewFile, EItemPreviewType type)
			{
				return false;
			}

			public static bool AddItemPreviewVideo(UGCUpdateHandle_t handle, string videoId)
			{
				return false;
			}

			public static void AddItemToFavorites(AppId_t appId, PublishedFileId_t fileId, Action<UserFavoriteItemsListChanged_t, bool> callback)
			{
			}

			public static bool AddRequiredKeyValueTag(UGCQueryHandle_t handle, string key, string value)
			{
				return false;
			}

			public static bool AddRequiredTag(UGCQueryHandle_t handle, string tagName)
			{
				return false;
			}

			public static void CreateItem(AppId_t appId, EWorkshopFileType type, Action<CreateItemResult_t, bool> callback)
			{
			}

			public static UGCQueryHandle_t CreateQueryAllRequest(EUGCQuery queryType, EUGCMatchingUGCType matchingFileType, AppId_t creatorAppId, AppId_t consumerAppId, uint page)
			{
				return default(UGCQueryHandle_t);
			}

			public static UGCQueryHandle_t CreateQueryDetailsRequest(PublishedFileId_t[] fileIds)
			{
				return default(UGCQueryHandle_t);
			}

			public static UGCQueryHandle_t CreateQueryDetailsRequest(List<PublishedFileId_t> fileIds)
			{
				return default(UGCQueryHandle_t);
			}

			public static UGCQueryHandle_t CreateQueryDetailsRequest(IEnumerable<PublishedFileId_t> fileIds)
			{
				return default(UGCQueryHandle_t);
			}

			public static UGCQueryHandle_t CreateQueryUserRequest(AccountID_t accountId, EUserUGCList listType, EUGCMatchingUGCType matchingType, EUserUGCListSortOrder sortOrder, AppId_t creatorAppId, AppId_t consumerAppId, uint page)
			{
				return default(UGCQueryHandle_t);
			}

			public static bool ReleaseQueryRequest(UGCQueryHandle_t handle)
			{
				return false;
			}

			public static void DeleteItem(PublishedFileId_t fileId, Action<DeleteItemResult_t, bool> callback)
			{
			}

			public static bool DownloadItem(PublishedFileId_t fileId, bool setHighPriority)
			{
				return false;
			}

			public static void GetAppDependencies(PublishedFileId_t fileId, Action<GetAppDependenciesResult_t, bool> callback)
			{
			}

			public static bool GetItemDownloadInfo(PublishedFileId_t fileId, out float completion)
			{
				completion = default(float);
				return false;
			}

			public static bool GetItemInstallInfo(PublishedFileId_t fileId, out ulong sizeOnDisk, out string folderPath, out DateTime timeStamp)
			{
				sizeOnDisk = default(ulong);
				folderPath = null;
				timeStamp = default(DateTime);
				return false;
			}

			public static bool GetItemInstallInfo(PublishedFileId_t fileId, out ulong sizeOnDisk, out string folderPath, uint folderSize, out DateTime timeStamp)
			{
				sizeOnDisk = default(ulong);
				folderPath = null;
				timeStamp = default(DateTime);
				return false;
			}

			public static EItemState GetItemState(PublishedFileId_t fileId)
			{
				return default(EItemState);
			}

			public static EItemUpdateStatus GetItemUpdateProgress(UGCUpdateHandle_t handle, out float completion)
			{
				completion = default(float);
				return default(EItemUpdateStatus);
			}

			public static bool GetQueryAdditionalPreview(UGCQueryHandle_t handle, uint index, uint previewIndex, out string urlOrVideoId, uint urlOrVideoSize, out string fileName, uint fileNameSize, out EItemPreviewType type)
			{
				urlOrVideoId = null;
				fileName = null;
				type = default(EItemPreviewType);
				return false;
			}

			public static bool GetQueryChildren(UGCQueryHandle_t handle, uint index, PublishedFileId_t[] fileIds, uint maxEntries)
			{
				return false;
			}

			public static bool GetQueryKeyValueTag(UGCQueryHandle_t handle, uint index, uint keyValueTagIndex, out string key, out string value)
			{
				key = null;
				value = null;
				return false;
			}

			public static bool GetQueryKeyValueTag(UGCQueryHandle_t handle, uint index, uint keyValueTagIndex, out string key, uint keySize, out string value, uint valueSize)
			{
				key = null;
				value = null;
				return false;
			}

			public static bool GetQueryMetadata(UGCQueryHandle_t handle, uint index, out string metadata, uint size)
			{
				metadata = null;
				return false;
			}

			public static uint GetQueryNumAdditionalPreviews(UGCQueryHandle_t handle, uint index)
			{
				return 0u;
			}

			public static uint GetQueryNumKeyValueTags(UGCQueryHandle_t handle, uint index)
			{
				return 0u;
			}

			public static bool GetQueryPreviewURL(UGCQueryHandle_t handle, uint index, out string url, uint urlSize)
			{
				url = null;
				return false;
			}

			public static bool GetQueryResult(UGCQueryHandle_t handle, uint index, out SteamUGCDetails_t details)
			{
				details = default(SteamUGCDetails_t);
				return false;
			}

			public static bool GetQueryStatistic(UGCQueryHandle_t handle, uint index, EItemStatistic statType, out ulong statValue)
			{
				statValue = default(ulong);
				return false;
			}

			public static void GetSubscribedItems(bool withLongDescription, bool withMetadata, bool withKeyValueTags, bool withAdditionalPreviews, uint withPlayTimeStatsInDays, Action<List<WorkshopItemDetails>> callback)
			{
			}

			public static void GetUserItemVote(PublishedFileId_t fileId, Action<GetUserItemVoteResult_t, bool> callback)
			{
			}

			public static void GetWorkshopEulaStatus(Action<WorkshopEULAStatus_t, bool> callback)
			{
			}

			public static bool ShowWorkshopEula()
			{
				return false;
			}

			public static void RemoveAppDependency(PublishedFileId_t fileId, AppId_t appId, Action<RemoveAppDependencyResult_t, bool> callback)
			{
			}

			public static void RemoveDependency(PublishedFileId_t parentFileId, PublishedFileId_t childFileId, Action<RemoveUGCDependencyResult_t, bool> callback)
			{
			}

			public static void RemoveItemFromFavorites(AppId_t appId, PublishedFileId_t fileId, Action<UserFavoriteItemsListChanged_t, bool> callback)
			{
			}

			public static bool RemoveItemKeyValueTags(UGCUpdateHandle_t handle, string key)
			{
				return false;
			}

			public static bool RemoveItemPreview(UGCUpdateHandle_t handle, uint index)
			{
				return false;
			}

			public static void RequestDetails(PublishedFileId_t fileId, uint maxAgeSeconds, Action<SteamUGCRequestUGCDetailsResult_t, bool> callback)
			{
			}

			public static void SendQueryUgcRequest(UGCQueryHandle_t handle, Action<SteamUGCQueryCompleted_t, bool> callback)
			{
			}

			public static bool SetAllowCachedResponse(UGCQueryHandle_t handle, uint maxAgeSeconds)
			{
				return false;
			}

			public static bool SetCloudFileNameFilter(UGCQueryHandle_t handle, string fileName)
			{
				return false;
			}

			public static bool SetItemContent(UGCUpdateHandle_t handle, string folder)
			{
				return false;
			}

			public static bool SetItemDescription(UGCUpdateHandle_t handle, string description)
			{
				return false;
			}

			public static bool SetItemMetadata(UGCUpdateHandle_t handle, string metadata)
			{
				return false;
			}

			public static bool SetItemPreview(UGCUpdateHandle_t handle, string previewFile)
			{
				return false;
			}

			public static bool SetItemTags(UGCUpdateHandle_t handle, List<string> tags)
			{
				return false;
			}

			public static bool SetItemTitle(UGCUpdateHandle_t handle, string title)
			{
				return false;
			}

			public static bool SetItemUpdateLanguage(UGCUpdateHandle_t handle, string language)
			{
				return false;
			}

			public static bool SetItemVisibility(UGCUpdateHandle_t handle, ERemoteStoragePublishedFileVisibility visibility)
			{
				return false;
			}

			public static bool SetLanguage(UGCQueryHandle_t handle, string language)
			{
				return false;
			}

			public static bool SetMatchAnyTag(UGCQueryHandle_t handle, bool anyTag)
			{
				return false;
			}

			public static bool SetRankedByTrendDays(UGCQueryHandle_t handle, uint days)
			{
				return false;
			}

			public static bool SetReturnAdditionalPreviews(UGCQueryHandle_t handle, bool additionalPreviews)
			{
				return false;
			}

			public static bool SetReturnChildren(UGCQueryHandle_t handle, bool returnChildren)
			{
				return false;
			}

			public static bool SetReturnKeyValueTags(UGCQueryHandle_t handle, bool tags)
			{
				return false;
			}

			public static bool SetReturnLongDescription(UGCQueryHandle_t handle, bool longDescription)
			{
				return false;
			}

			public static bool SetReturnMetadata(UGCQueryHandle_t handle, bool metadata)
			{
				return false;
			}

			public static bool SetReturnOnlyIDs(UGCQueryHandle_t handle, bool onlyIds)
			{
				return false;
			}

			public static bool SetReturnPlaytimeStats(UGCQueryHandle_t handle, uint days)
			{
				return false;
			}

			public static bool SetReturnTotalOnly(UGCQueryHandle_t handle, bool totalOnly)
			{
				return false;
			}

			public static bool SetSearchText(UGCQueryHandle_t handle, string text)
			{
				return false;
			}

			public static void SetUserItemVote(PublishedFileId_t fileID, bool voteUp, Action<SetUserItemVoteResult_t, bool> callback)
			{
			}

			public static UGCUpdateHandle_t StartItemUpdate(AppId_t appId, PublishedFileId_t fileID)
			{
				return default(UGCUpdateHandle_t);
			}

			public static void StartPlaytimeTracking(PublishedFileId_t[] fileIds, Action<StartPlaytimeTrackingResult_t, bool> callback)
			{
			}

			public static void StopPlaytimeTracking(PublishedFileId_t[] fileIds, Action<StopPlaytimeTrackingResult_t, bool> callback)
			{
			}

			public static void StopPlaytimeTrackingForAllItems(Action<StopPlaytimeTrackingResult_t, bool> callback)
			{
			}

			public static void SubmitItemUpdate(UGCUpdateHandle_t handle, string changeNote, Action<SubmitItemUpdateResult_t, bool> callback)
			{
			}

			public static void SubscribeItem(PublishedFileId_t fileId, Action<RemoteStorageSubscribePublishedFileResult_t, bool> callback)
			{
			}

			public static void SuspendDownloads(bool suspend)
			{
			}

			public static void UnsubscribeItem(PublishedFileId_t fileId, Action<RemoteStorageUnsubscribePublishedFileResult_t, bool> callback)
			{
			}

			public static bool UpdateItemPreviewFile(UGCUpdateHandle_t handle, uint index, string file)
			{
				return false;
			}

			public static bool UpdateItemPreviewVideo(UGCUpdateHandle_t handle, uint index, string videoId)
			{
				return false;
			}

			public static bool SetSubscriptionsLoadOrder(PublishedFileId_t[] publishedFileIDs, uint numPublishedFileIDs)
			{
				return false;
			}

			public static bool SetItemsDisabledLocally(PublishedFileId_t[] publishedFileIDs, uint numPublishedFileIDs, bool disabledLocally)
			{
				return false;
			}

			public static uint GetSubscribedItems(PublishedFileId_t[] fileIDs, uint maxEntries, bool includeLocallyDisabled = false)
			{
				return 0u;
			}

			public static PublishedFileId_t[] GetSubscribedItems(bool includeLocallyDisabled = false)
			{
				return null;
			}

			public static void GetSubscribedItems(Action<List<WorkshopItemDetails>> callback, bool includeLocallyDisabled = false)
			{
			}

			public static uint GetNumSubscribedItems(bool includeLocallyDisabled = false)
			{
				return 0u;
			}
		}

		public static bool ItemStateHasFlag(EItemState value, EItemState checkflag)
		{
			return false;
		}

		public static bool ItemStateHasAllFlags(EItemState value, params EItemState[] checkflags)
		{
			return false;
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	[HelpURL("https://kb.heathen.group/steam/features/workshop")]
	public class WorkshopItemDetails
	{
		protected SteamUGCDetails_t ItemDetails;

		public string metadata;

		public StringKeyValuePair[] keyValueTags;

		public string Title => null;

		public string Description => null;

		public AppData ConsumerApp => default(AppData);

		public PublishedFileId_t FileId => default(PublishedFileId_t);

		public UserData Owner => default(UserData);

		public DateTime TimeCreated => default(DateTime);

		public DateTime TimeUpdated => default(DateTime);

		public uint UpVotes => 0u;

		public uint DownVotes => 0u;

		public float VoteScore => 0f;

		public bool IsBanned => false;

		public bool IsTagsTruncated => false;

		public bool IsSubscribed => false;

		public bool IsNeedsUpdate => false;

		public bool IsInstalled => false;

		public bool IsDownloading => false;

		public bool IsDownloadPending => false;

		public float DownloadCompletion => 0f;

		public int FileSize => 0;

		public DirectoryInfo FolderPath => null;

		public EItemState StateFlags => default(EItemState);

		public ERemoteStoragePublishedFileVisibility Visibility => default(ERemoteStoragePublishedFileVisibility);

		public string[] Tags => null;

		public SteamUGCDetails_t SourceItemDetails => default(SteamUGCDetails_t);

		public WorkshopItemDetails(SteamUGCDetails_t itemDetails)
		{
		}

		public static void Get(PublishedFileId_t file, Action<WorkshopItemDetails> callback)
		{
		}

		public static UgcQuery Get(IEnumerable<PublishedFileId_t> files)
		{
			return null;
		}

		public static UgcQuery GetMyPublished()
		{
			return null;
		}

		public static UgcQuery GetMyPublished(AppData creatorApp, AppData consumerApp)
		{
			return null;
		}

		public static UgcQuery GetSubscribed()
		{
			return null;
		}

		public static UgcQuery GetPlayed()
		{
			return null;
		}

		public static UgcQuery GetPlayed(AppData creatorApp, AppData consumerApp)
		{
			return null;
		}

		public void UpdateTitle(string value, string changeNote, Action<SubmitItemUpdateResult_t, bool> callback)
		{
		}

		public void UpdateTitle(string value, LanguageCodes language, string changeNote, Action<SubmitItemUpdateResult_t, bool> callback)
		{
		}

		public void UpdateDescription(string value, string changeNote, Action<SubmitItemUpdateResult_t, bool> callback)
		{
		}

		public void UpdateDescription(string value, LanguageCodes language, string changeNote, Action<SubmitItemUpdateResult_t, bool> callback)
		{
		}

		public void UpdateContent(DirectoryInfo value, string changeNote, Action<SubmitItemUpdateResult_t, bool> callback)
		{
		}

		public void UpdatePreviewImage(FileInfo value, string changeNote, Action<SubmitItemUpdateResult_t, bool> callback)
		{
		}

		public void UpdateMetadata(string value, string changeNote, Action<SubmitItemUpdateResult_t, bool> callback)
		{
		}

		public void UpdateTags(string[] value, string changeNote, Action<SubmitItemUpdateResult_t, bool> callback)
		{
		}

		public bool GetPreviewImage(Action<string, byte[]> callback)
		{
			return false;
		}

		public void DeleteItem(Action<DeleteItemResult_t, bool> callback)
		{
		}

		public bool DownloadItem(bool highPriority)
		{
			return false;
		}

		public void Subscribe(Action<RemoteStorageSubscribePublishedFileResult_t, bool> callback)
		{
		}

		public void Unsubscribe(Action<RemoteStorageUnsubscribePublishedFileResult_t, bool> callback)
		{
		}

		public void SetVote(bool voteUp, Action<SetUserItemVoteResult_t, bool> callback)
		{
		}

		public void StartPlayTime(Action<StartPlaytimeTrackingResult_t, bool> callback)
		{
		}

		public void StopPlayTime(Action<StopPlaytimeTrackingResult_t, bool> callback)
		{
		}
	}
}

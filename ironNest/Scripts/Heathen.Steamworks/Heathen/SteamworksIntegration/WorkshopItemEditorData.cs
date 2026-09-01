using System;
using System.IO;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct WorkshopItemEditorData
	{
		public PublishedFileId_t? PublishedFileId;

		public AppData appId;

		public string title;

		public string description;

		public DirectoryInfo Content;

		public FileInfo Preview;

		public string metadata;

		public string[] tags;

		public ERemoteStoragePublishedFileVisibility visibility;

		public bool IsValid => false;

		public bool Create(Action<WorkshopItemDataCreateStatus> completedCallback = null, Action<UGCUpdateHandle_t> uploadStartedCallback = null, Action<CreateItemResult_t> fileCreatedCallback = null)
		{
			return false;
		}

		public bool Create(WorkshopItemPreviewFile[] additionalPreviews, string[] additionalYouTubeIds, WorkshopItemKeyValueTag[] additionalKeyValueTags, Action<WorkshopItemDataCreateStatus> completedCallback = null, Action<UGCUpdateHandle_t> uploadStartedCallback = null, Action<CreateItemResult_t> fileCreatedCallback = null)
		{
			return false;
		}

		public bool Update(Action<WorkshopItemDataUpdateStatus> completedCallback = null, Action<UGCUpdateHandle_t> uploadStartedCallback = null)
		{
			return false;
		}

		public bool Update(WorkshopItemPreviewFile[] additionalPreviews, string[] additionalYouTubeIds, WorkshopItemKeyValueTag[] additionalKeyValueTags, Action<WorkshopItemDataUpdateStatus> completedCallback = null, Action<UGCUpdateHandle_t> uploadStartedCallback = null)
		{
			return false;
		}
	}
}

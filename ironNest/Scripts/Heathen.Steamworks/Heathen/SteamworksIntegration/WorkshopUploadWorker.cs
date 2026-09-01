using System;
using System.IO;
using System.Runtime.CompilerServices;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	public class WorkshopUploadWorker
	{
		private WorkshopItemEditorData _itemData;

		private UGCUpdateHandle_t? _updateHandle;

		public PublishedFileId_t? FileId => null;

		public AppData AppId => default(AppData);

		public string Title => null;

		public string Description => null;

		public DirectoryInfo Content => null;

		public FileInfo Preview => null;

		public string Metadata => null;

		public string[] Tags => null;

		public ERemoteStoragePublishedFileVisibility Visibility => default(ERemoteStoragePublishedFileVisibility);

		public event EventHandler<WorkshopItemDataCreateStatus> Completed
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public event EventHandler<UGCUpdateHandle_t> UpdateStarted
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public event EventHandler<CreateItemResult_t> FileCreated
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static WorkshopUploadWorker Get(WorkshopItemEditorData data)
		{
			return null;
		}

		public bool RunCreate()
		{
			return false;
		}

		public bool RunCreate(WorkshopItemPreviewFile[] additionalPreviews, string[] additionalYouTubeIds, WorkshopItemKeyValueTag[] additionalKeyValueTags)
		{
			return false;
		}

		public EItemUpdateStatus GetUpdateProgress(out float progress)
		{
			progress = default(float);
			return default(EItemUpdateStatus);
		}

		private void CompletedHandler(WorkshopItemDataCreateStatus arg)
		{
		}

		private void UploadStartedHandler(UGCUpdateHandle_t arg)
		{
		}

		private void FileCreatedHandler(CreateItemResult_t arg)
		{
		}
	}
}

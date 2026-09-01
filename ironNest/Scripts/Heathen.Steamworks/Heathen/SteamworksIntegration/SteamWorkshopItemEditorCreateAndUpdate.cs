using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamWorkshopItemEditorData), "Create & Update", null)]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamWorkshopItemEditorData))]
	public class SteamWorkshopItemEditorCreateAndUpdate : MonoBehaviour
	{
		[SettingsField(0, false, "Optional")]
		public string metadata;

		[SettingsField(0, false, "Optional")]
		public string[] additionalYouTubeIds;

		[SettingsField(0, false, "Optional")]
		public WorkshopItemPreviewFile[] additionalPreviews;

		[SettingsField(0, false, "Optional")]
		public WorkshopItemKeyValueTag[] additionalKeyValueTags;

		[SettingsField(0, false, "Optional")]
		public string[] tags;

		private SteamWorkshopItemEditorData _inspector;

		private SteamWorkshopItemEditorDataEvents _events;

		private void Awake()
		{
		}

		public void CreateNew()
		{
		}

		public void CreateOrUpdate()
		{
		}

		private void HandleUpdateCompleted(WorkshopItemDataUpdateStatus status)
		{
		}

		private void HandleFileCreated(CreateItemResult_t t)
		{
		}

		private void HandleUploaded(UGCUpdateHandle_t t)
		{
		}

		private void HandleCompleted(WorkshopItemDataCreateStatus status)
		{
		}
	}
}

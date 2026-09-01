using Steamworks;
using TMPro;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamWorkshopItemDetailData), "Edit", null)]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamWorkshopItemDetailDataEvents))]
	[RequireComponent(typeof(SteamWorkshopItemDetailData))]
	public class SteamWorkshopItemDetailEdit : MonoBehaviour
	{
		[SettingsField(0, false, "Editor")]
		public SteamWorkshopItemEditorData component;

		[SettingsField(0, false, "Quick Edits")]
		public TMP_InputField changeNote;

		[SettingsField(0, false, "Quick Edits")]
		public TMP_InputField title;

		[SettingsField(0, false, "Quick Edits")]
		public TMP_InputField description;

		[SettingsField(0, false, "Quick Edits")]
		public TMP_InputField contentFolder;

		[SettingsField(0, false, "Quick Edits")]
		public TMP_InputField previewImageFile;

		[SettingsField(0, false, "Quick Edits")]
		public TMP_InputField metadata;

		private SteamWorkshopItemDetailData _mInspector;

		private SteamWorkshopItemDetailDataEvents _mEvents;

		private void Awake()
		{
		}

		private void HandleOnChanged()
		{
		}

		private string GetChangeNote()
		{
			return null;
		}

		public void SetEditor()
		{
		}

		public void UpdateTitle()
		{
		}

		public void UpdateDescription()
		{
		}

		public void UpdateContent()
		{
		}

		public void UpdatePreviewImage()
		{
		}

		public void UpdateMetadata()
		{
		}

		public void UpdateAll()
		{
		}

		private void HandleEditResult(SubmitItemUpdateResult_t t, bool arg2)
		{
		}
	}
}

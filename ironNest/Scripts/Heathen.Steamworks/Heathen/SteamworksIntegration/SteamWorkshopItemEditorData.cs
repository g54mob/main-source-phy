using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Heathen.SteamworksIntegration
{
	[AddComponentMenu("Steamworks/Workshop Item Editor")]
	[HelpURL("https://kb.heathen.group/steam/features/workshop")]
	public class SteamWorkshopItemEditorData : MonoBehaviour
	{
		public uint consumingAppId;

		public TMP_InputField title;

		public TMP_InputField description;

		public TMP_InputField contentFolderPath;

		public TMP_InputField previewFilePath;

		private WorkshopItemEditorData _mData;

		private SteamWorkshopItemEditorDataEvents _mEvents;

		[FormerlySerializedAs("m_Delegates")]
		[SerializeField]
		private List<string> mDelegates;

		public WorkshopItemEditorData Data
		{
			get
			{
				return default(WorkshopItemEditorData);
			}
			set
			{
			}
		}

		private void Awake()
		{
		}

		private void HandleTitleUpdate(string arg0)
		{
		}

		private void HandleDescriptionUpdate(string arg0)
		{
		}

		private void HandleContentFolderUpdate(string arg0)
		{
		}

		private void HandlePreviewFileUpdate(string arg0)
		{
		}
	}
}

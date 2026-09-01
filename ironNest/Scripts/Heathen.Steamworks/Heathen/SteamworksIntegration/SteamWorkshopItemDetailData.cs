using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Heathen.SteamworksIntegration
{
	[AddComponentMenu("Steamworks/Workshop Item")]
	[HelpURL("https://kb.heathen.group/steam/features/workshop")]
	public class SteamWorkshopItemDetailData : MonoBehaviour
	{
		private WorkshopItemDetails _mData;

		private SteamWorkshopItemDetailDataEvents _mEvents;

		[FormerlySerializedAs("m_Delegates")]
		[SerializeField]
		private List<string> mDelegates;

		public WorkshopItemDetails Data
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		private void Awake()
		{
		}

		public void Get(PublishedFileId_t fileId)
		{
		}

		public void LoadPreview()
		{
		}

		public void Subscribe()
		{
		}

		public void Unsubscribe()
		{
		}

		public void DownloadItem()
		{
		}

		public void DownloadItemHighPriority()
		{
		}

		public void Delete()
		{
		}

		public void UpVote()
		{
		}

		public void DownVote()
		{
		}

		public void StartPlaytime()
		{
		}

		public void StopPlaytime()
		{
		}

		private void HandleItemGet(WorkshopItemDetails details)
		{
		}

		private void HandleStartPlaytime(StartPlaytimeTrackingResult_t t, bool arg2)
		{
		}

		private void HandleEndPlaytime(StartPlaytimeTrackingResult_t t, bool arg2)
		{
		}

		private void HandleVoteSet(SetUserItemVoteResult_t t, bool arg2)
		{
		}

		private void HandleItemDelete(DeleteItemResult_t t, bool arg2)
		{
		}

		private void HandleUnsubscribe(RemoteStorageUnsubscribePublishedFileResult_t t, bool arg2)
		{
		}

		private void HandleSubscribed(RemoteStorageSubscribePublishedFileResult_t t, bool arg2)
		{
		}
	}
}

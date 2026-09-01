using Steamworks;
using UnityEngine;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[ModularEvents(typeof(SteamWorkshopItemDetailData))]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamWorkshopItemDetailData))]
	public class SteamWorkshopItemDetailDataEvents : MonoBehaviour
	{
		[EventField]
		public UnityEvent onChange;

		[EventField]
		public UnityEvent<PublishedFileId_t> onSubscribed;

		[EventField]
		public UnityEvent<PublishedFileId_t> onUnsubscribed;

		[EventField]
		public UnityEvent<PublishedFileId_t> onDelete;

		[EventField]
		public UnityEvent<PublishedFileId_t> onVoteSet;

		[EventField]
		public UnityEvent<PublishedFileId_t> onPlayStarted;

		[EventField]
		public UnityEvent<PublishedFileId_t> onPlayEnded;

		[EventField]
		public UnityEvent<PublishedFileId_t> onEdited;

		[EventField]
		public UnityEvent<EResult> onSubscribeFailed;

		[EventField]
		public UnityEvent<EResult> onUnsubscribeFailed;

		[EventField]
		public UnityEvent<EResult> onDeleteFailed;

		[EventField]
		public UnityEvent<EResult> onVoteSetFailed;

		[EventField]
		public UnityEvent<EResult> onPlayStartedFailed;

		[EventField]
		public UnityEvent<EResult> onPlayEndedFailed;

		[EventField]
		public UnityEvent<EResult> onEditFailed;

		[EventField]
		public UnityEvent<bool> onSetIsSubscribed;

		[EventField]
		public UnityEvent<bool> onSetIsNotSubscribed;

		[EventField]
		public UnityEvent<bool> onSetIsInstalled;

		[EventField]
		public UnityEvent<bool> onSetIsNotInstalled;

		[EventField]
		public UnityEvent<byte[]> onPreviewImageLoaded;

		private SteamWorkshopItemDetailData _mInspector;

		private void Awake()
		{
		}

		private void HandleOnChange()
		{
		}
	}
}

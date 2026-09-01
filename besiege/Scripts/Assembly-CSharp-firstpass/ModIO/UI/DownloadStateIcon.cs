using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	[AddComponentMenu("ModIO/UI/DownloadStateIcon")]
	public class DownloadStateIcon : MonoBehaviour, IModSubscriptionsUpdateReceiver, IDownloadViewElement
	{
		[SerializeField]
		private Image m_downloading;

		[SerializeField]
		private RectTransformSpinner spinner;

		[SerializeField]
		private MonoBehaviour avatar;

		private DownloadView d_view;

		private ModView m_view;

		private ModProfile m_profile;

		private int m_modId;

		private FileDownloadInfo download;

		private int state = -1;

		private void OnEnable()
		{
			ModManager.onModBinaryInstalled += OnModInstalled;
			if (m_profile != null)
			{
				DisplayModSubscribed(m_profile);
			}
		}

		private void OnDisable()
		{
			ModManager.onModBinaryInstalled -= OnModInstalled;
		}

		public void SetDownloadView(DownloadView view)
		{
			if (!(d_view == view))
			{
				if (d_view != null)
				{
					d_view.onDownloadInfoUpdated.RemoveListener(DisplayDownload);
				}
				d_view = view;
				SetModView(view.view);
				if (d_view != null)
				{
					d_view.onDownloadInfoUpdated.AddListener(DisplayDownload);
				}
			}
		}

		public void SetModView(ModView view)
		{
			if (!(m_view == view))
			{
				if (m_view != null)
				{
					m_view.onProfileChanged.RemoveListener(DisplayModSubscribed);
				}
				m_view = view;
				if (m_view != null)
				{
					m_view.onProfileChanged.AddListener(DisplayModSubscribed);
					DisplayModSubscribed(m_view.profile);
				}
				else
				{
					DisplayModSubscribed(null);
				}
			}
		}

		public void DisplayDownload(FileDownloadInfo download)
		{
			this.download = download;
			if (download == null)
			{
				return;
			}
			if (download.isDone)
			{
				SetState(2);
			}
			else if (download.bytesPerSecond == 0L)
			{
				if (spinner.enabled)
				{
					spinner.enabled = false;
				}
			}
			else if (!spinner.enabled)
			{
				spinner.enabled = true;
			}
		}

		public void DisplayModSubscribed(ModProfile profile)
		{
			int modId = 0;
			if (profile != null)
			{
				m_profile = profile;
				modId = profile.id;
				Modfile currentBuild = profile.currentBuild;
				download = DownloadClient.GetActiveModBinaryDownload(modId, (currentBuild != null) ? currentBuild.id : 0);
			}
			DisplayModSubscribed(modId);
		}

		public void DisplayModSubscribed(int modId)
		{
			bool isSubscribed = LocalUser.SubscribedModIds.Contains(modId);
			DisplayModSubscribed(modId, isSubscribed);
		}

		public void DisplayModSubscribed(int modId, bool isSubscribed)
		{
			m_modId = modId;
			if (isSubscribed)
			{
				if (download == null || download.isDone)
				{
					SetState(2);
				}
				else
				{
					SetState(1);
				}
			}
			else
			{
				SetState(0);
			}
		}

		public void OnModSubscriptionsUpdated(IList<int> addedSubscriptions, IList<int> removedSubscriptions)
		{
			if (d_view != null)
			{
				if (addedSubscriptions.Contains(m_modId))
				{
					SetState(1);
				}
				else if (removedSubscriptions.Contains(m_modId))
				{
					SetState(0);
				}
			}
		}

		private void OnModInstalled(ModfileIdPair idPair)
		{
			if (idPair.modId == m_modId)
			{
				SetState(2);
			}
		}

		private void SetState(int i)
		{
			if (i == state)
			{
				SetAvatar();
				return;
			}
			state = i;
			switch (i)
			{
			case 0:
				m_downloading.gameObject.SetActive(false);
				if (avatar != null)
				{
					avatar.gameObject.SetActive(false);
				}
				break;
			case 1:
				m_downloading.gameObject.SetActive(true);
				if (avatar != null)
				{
					avatar.gameObject.SetActive(false);
				}
				break;
			case 2:
				m_downloading.gameObject.SetActive(false);
				SetAvatar();
				break;
			}
		}

		private void SetAvatar()
		{
			if (avatar == null)
			{
				return;
			}
			if (m_view == null || m_view.profile == null || m_view.profile.submittedBy == null)
			{
				avatar.gameObject.SetActive(false);
			}
			else if (m_view.profile.submittedBy.id.Equals(LocalUser.UserId))
			{
				if (m_view.profile.submittedBy.username != LocalUser.Profile.username || m_view.profile.submittedBy.usernamePlatform != LocalUser.Profile.usernamePlatform)
				{
					MessageSystem.QueueMessage(MessageDisplayData.Type.Error, m_view.profile.name + " is showing as wrong owner state");
				}
				avatar.gameObject.SetActive(true);
			}
			else
			{
				avatar.gameObject.SetActive(false);
			}
		}
	}
}

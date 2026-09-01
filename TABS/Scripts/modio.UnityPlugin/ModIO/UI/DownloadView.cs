using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ModIO.UI
{
	[DisallowMultipleComponent]
	public class DownloadView : MonoBehaviour, IModViewElement
	{
		[Serializable]
		public class DownloadInfoUpdatedEvent : UnityEvent<FileDownloadInfo>
		{
		}

		public const float DOWNLOAD_SPEED_UPDATE_INTERVAL = 0.5f;

		public const float HIDE_DELAY_SECONDS = 1.5f;

		private FileDownloadInfo m_downloadInfo;

		public bool hideIfInactive = true;

		public DownloadInfoUpdatedEvent onDownloadInfoUpdated;

		private ModView m_view;

		private int m_modId;

		private Coroutine m_updateCoroutine;

		public FileDownloadInfo downloadInfo => m_downloadInfo;

		GameObject IModViewElement.gameObject => base.gameObject;

		protected virtual void Awake()
		{
			DownloadClient.modfileDownloadStarted += OnDownloadStarted;
		}

		protected virtual void Start()
		{
			IDownloadViewElement[] componentsInChildren = base.gameObject.GetComponentsInChildren<IDownloadViewElement>(includeInactive: true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].SetDownloadView(this);
			}
		}

		protected virtual void OnDestroy()
		{
			DownloadClient.modfileDownloadStarted -= OnDownloadStarted;
		}

		protected virtual void OnEnable()
		{
			if (m_downloadInfo != null)
			{
				if (m_updateCoroutine == null)
				{
					m_updateCoroutine = StartCoroutine(UpdateCoroutine());
				}
			}
			else if (hideIfInactive)
			{
				base.gameObject.SetActive(value: false);
			}
		}

		protected virtual void OnDisable()
		{
			if (m_updateCoroutine != null)
			{
				StopCoroutine(m_updateCoroutine);
				m_updateCoroutine = null;
			}
		}

		public void SetModView(ModView view)
		{
			if (!(m_view == view))
			{
				if (m_view != null)
				{
					m_view.onProfileChanged.RemoveListener(DisplayProfile);
				}
				m_view = view;
				if (m_view != null)
				{
					m_view.onProfileChanged.AddListener(DisplayProfile);
					DisplayProfile(m_view.profile);
				}
				else
				{
					DisplayProfile(null);
				}
			}
		}

		public void DisplayProfile(ModProfile profile)
		{
			int num = 0;
			if (profile != null)
			{
				num = profile.id;
			}
			if (m_modId == num)
			{
				return;
			}
			if (m_updateCoroutine != null)
			{
				StopCoroutine(m_updateCoroutine);
				m_updateCoroutine = null;
			}
			m_modId = num;
			m_downloadInfo = null;
			bool flag = false;
			if (num != 0)
			{
				foreach (KeyValuePair<ModfileIdPair, FileDownloadInfo> item in DownloadClient.modfileDownloadMap)
				{
					if (item.Key.modId == m_modId)
					{
						flag = true;
						OnDownloadStarted(item.Key, item.Value);
					}
				}
			}
			base.gameObject.SetActive(flag || !hideIfInactive);
		}

		protected virtual IEnumerator UpdateCoroutine()
		{
			float lastSpeedUpdate = Time.unscaledTime;
			while (this != null && m_downloadInfo != null && onDownloadInfoUpdated != null && !m_downloadInfo.isDone)
			{
				float unscaledTime = Time.unscaledTime;
				if (unscaledTime - lastSpeedUpdate >= 0.5f)
				{
					lastSpeedUpdate = unscaledTime;
				}
				onDownloadInfoUpdated.Invoke(m_downloadInfo);
				yield return null;
			}
			onDownloadInfoUpdated.Invoke(m_downloadInfo);
			if (hideIfInactive)
			{
				yield return new WaitForSecondsRealtime(1.5f);
				base.gameObject.SetActive(value: false);
			}
		}

		protected virtual void OnDownloadStarted(ModfileIdPair idPair, FileDownloadInfo downloadInfo)
		{
			if (m_modId == idPair.modId)
			{
				m_downloadInfo = downloadInfo;
				if (!base.isActiveAndEnabled && hideIfInactive)
				{
					base.gameObject.SetActive(value: true);
				}
				if (base.isActiveAndEnabled && m_updateCoroutine == null)
				{
					m_updateCoroutine = StartCoroutine(UpdateCoroutine());
				}
			}
		}
	}
}

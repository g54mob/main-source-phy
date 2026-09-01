using UnityEngine;

namespace ModIO.UI
{
	[RequireComponent(typeof(HorizontalProgressBar))]
	public class DownloadProgressBar : MonoBehaviour, IDownloadViewElement
	{
		private DownloadView m_view;

		public void SetDownloadView(DownloadView view)
		{
			if (!(m_view == view))
			{
				if (m_view != null)
				{
					m_view.onDownloadInfoUpdated.RemoveListener(DisplayDownload);
				}
				m_view = view;
				if (m_view != null)
				{
					m_view.onDownloadInfoUpdated.AddListener(DisplayDownload);
					DisplayDownload(m_view.downloadInfo);
				}
				else
				{
					DisplayDownload(null);
				}
			}
		}

		public void DisplayDownload(FileDownloadInfo download)
		{
			float percentComplete = 0f;
			if (download != null)
			{
				if (download.isDone)
				{
					percentComplete = 1f;
				}
				else if (download.request != null && download.fileSize > 0)
				{
					percentComplete = (float)download.request.downloadedBytes / (float)download.fileSize;
				}
			}
			GetComponent<HorizontalProgressBar>().percentComplete = percentComplete;
		}
	}
}

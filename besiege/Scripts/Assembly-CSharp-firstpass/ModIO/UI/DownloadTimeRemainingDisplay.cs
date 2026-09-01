using UnityEngine;

namespace ModIO.UI
{
	public class DownloadTimeRemainingDisplay : MonoBehaviour, IDownloadViewElement
	{
		private GenericTextComponent m_textComponent = default(GenericTextComponent);

		private DownloadView m_view;

		private FileDownloadInfo m_download;

		[SerializeField]
		private string m_unstartedText = "Initializing";

		[SerializeField]
		private string m_notDownloadingText = "Awaiting Connection";

		[SerializeField]
		private string m_completedText = "Download Complete";

		protected virtual void Awake()
		{
			Component textDisplayComponent = GenericTextComponent.FindCompatibleTextComponent(base.gameObject);
			m_textComponent.SetTextDisplayComponent(textDisplayComponent);
		}

		protected virtual void OnEnable()
		{
			DisplayDownload(m_download);
		}

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
			m_download = download;
			string text = string.Empty;
			if (download != null)
			{
				if (download.request == null || download.request.downloadedBytes == 0L)
				{
					text = m_unstartedText;
				}
				else if (download.isDone)
				{
					text = m_completedText;
				}
				else if (download.bytesPerSecond <= 1)
				{
					text = m_notDownloadingText;
				}
				else
				{
					int seconds = (int)((download.fileSize - (long)download.request.downloadedBytes) / download.bytesPerSecond);
					text = ValueFormatting.SecondsAsTime(seconds);
				}
			}
			m_textComponent.text = text;
		}
	}
}

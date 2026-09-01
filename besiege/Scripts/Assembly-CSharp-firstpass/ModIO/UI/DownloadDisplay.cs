using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	[Obsolete("Use ModBinaryDownloadDisplay instead.")]
	public class DownloadDisplay : DownloadDisplayComponent
	{
		[Header("UI Elements")]
		public Text percentageText;

		public Text bytesReceivedText;

		public Text bytesTotalText;

		public Text bytesPerSecondText;

		public Text timeRemainingText;

		public HorizontalProgressBar progressBar;

		[SerializeField]
		[Header("Display Data")]
		private DownloadDisplayData m_data;

		private FileDownloadInfo m_downloadInfo;

		private Coroutine m_updateCoroutine;

		public override DownloadDisplayData data
		{
			get
			{
				return m_data;
			}
			set
			{
				m_data = value;
				PresentData();
			}
		}

		public override event Action<DownloadDisplayComponent> onClick;

		private void PresentData()
		{
			float num = 0f;
			if (data.bytesTotal > 0)
			{
				num = (float)data.bytesReceived / (float)data.bytesTotal;
			}
			if (percentageText != null)
			{
				percentageText.text = (num * 100f).ToString("0.0") + "%";
			}
			if (progressBar != null)
			{
				progressBar.percentComplete = num;
			}
			if (bytesReceivedText != null)
			{
				bytesReceivedText.text = ValueFormatting.ByteCount(data.bytesReceived, "0.0");
			}
			if (bytesTotalText != null)
			{
				bytesTotalText.text = ValueFormatting.ByteCount(data.bytesTotal, "0.0");
			}
			if (bytesPerSecondText != null)
			{
				bytesPerSecondText.text = ValueFormatting.ByteCount(data.bytesPerSecond, "0.0") + "/s";
			}
			if (timeRemainingText != null)
			{
				TimeSpan timeSpan = TimeSpan.FromSeconds(0.0);
				timeRemainingText.text = timeSpan.TotalHours + ":" + timeSpan.Minutes + ":" + timeSpan.Seconds;
			}
		}

		private void OnEnable()
		{
			if (Application.isPlaying && m_downloadInfo != null && m_updateCoroutine == null)
			{
				m_updateCoroutine = StartCoroutine(UpdateCoroutine());
			}
		}

		public override void Initialize()
		{
		}

		public override void DisplayDownload(FileDownloadInfo downloadInfo)
		{
			if (m_updateCoroutine != null)
			{
				StopCoroutine(m_updateCoroutine);
			}
			m_downloadInfo = downloadInfo;
			long bytesReceived = (long)((downloadInfo.request != null) ? downloadInfo.request.downloadedBytes : 0);
			m_data = new DownloadDisplayData
			{
				bytesReceived = bytesReceived,
				bytesPerSecond = 0L,
				bytesTotal = downloadInfo.fileSize,
				isActive = !downloadInfo.isDone
			};
			if (Application.isPlaying && base.isActiveAndEnabled)
			{
				m_updateCoroutine = StartCoroutine(UpdateCoroutine());
			}
		}

		private IEnumerator UpdateCoroutine()
		{
			float timeStepElapsed = 0f;
			long timeStepStartByteCount = (long)((m_downloadInfo.request != null) ? m_downloadInfo.request.downloadedBytes : 0);
			while (m_downloadInfo != null && !m_downloadInfo.isDone)
			{
				if (m_data.bytesTotal <= 0)
				{
					m_data.bytesTotal = m_downloadInfo.fileSize;
				}
				if (m_downloadInfo.request != null)
				{
					m_data.bytesReceived = (long)m_downloadInfo.request.downloadedBytes;
				}
				if (timeStepElapsed >= 1f)
				{
					m_data.bytesPerSecond = (long)((float)(m_data.bytesReceived - timeStepStartByteCount) / timeStepElapsed);
					timeStepElapsed = 0f;
					timeStepStartByteCount = m_data.bytesReceived;
				}
				PresentData();
				yield return null;
				timeStepElapsed += Time.unscaledDeltaTime;
			}
			m_data.bytesReceived = m_data.bytesTotal;
			m_data.bytesPerSecond = 0L;
			m_data.isActive = false;
			m_downloadInfo = null;
			PresentData();
		}

		public void NotifyClick()
		{
			if (onClick != null)
			{
				onClick(this);
			}
		}
	}
}

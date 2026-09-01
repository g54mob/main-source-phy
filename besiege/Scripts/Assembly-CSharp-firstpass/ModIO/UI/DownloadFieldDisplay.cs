using UnityEngine;

namespace ModIO.UI
{
	public class DownloadFieldDisplay : MonoBehaviour, IDownloadViewElement
	{
		[MemberReference.DropdownDisplay(typeof(FileDownloadInfo), false, false, null, displayEnumerables = false, displayNested = true, membersToIgnore = new string[] { "error.webRequest" })]
		public MemberReference reference = new MemberReference("bytesPerSecond");

		public ValueFormatting formatting = default(ValueFormatting);

		private GenericTextComponent m_textComponent = default(GenericTextComponent);

		private DownloadView m_view;

		private FileDownloadInfo m_downloadInfo;

		protected virtual void Awake()
		{
			Component textDisplayComponent = GenericTextComponent.FindCompatibleTextComponent(base.gameObject);
			m_textComponent.SetTextDisplayComponent(textDisplayComponent);
		}

		protected virtual void OnEnable()
		{
			DisplayDownload(m_downloadInfo);
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

		public void DisplayDownload(FileDownloadInfo downloadInfo)
		{
			m_downloadInfo = downloadInfo;
			object value = reference.GetValue(m_downloadInfo);
			string text = ValueFormatting.FormatValue(value, formatting.method, formatting.toStringParameter);
			m_textComponent.text = text;
		}
	}
}

using UnityEngine;

namespace ModIO.UI
{
	public class PageNumberDisplay : MonoBehaviour, ISubscriptionsViewElement, IExplorerViewElement
	{
		private GenericTextComponent m_textComponent;

		private SubscriptionsView m_subsView;

		private ExplorerView m_explorerView;

		private int m_pageSize = 1;

		private int m_resultIndex;

		protected virtual void Awake()
		{
			Component textDisplayComponent = GenericTextComponent.FindCompatibleTextComponent(base.gameObject);
			m_textComponent.SetTextDisplayComponent(textDisplayComponent);
		}

		protected virtual void OnEnable()
		{
			Refresh();
		}

		public void SetSubscriptionsView(SubscriptionsView view)
		{
			if (!(m_subsView == view))
			{
				if (m_subsView != null)
				{
					m_subsView.onModPageChanged.RemoveListener(DisplayPageNumber);
				}
				if (m_explorerView != null)
				{
					m_explorerView.onModPageChanged.RemoveListener(DisplayPageNumber);
				}
				m_explorerView = null;
				m_subsView = view;
				if (m_subsView != null)
				{
					m_subsView.onModPageChanged.AddListener(DisplayPageNumber);
					DisplayPageNumber(m_subsView.modPage);
				}
				else
				{
					DisplayPageNumber(null);
				}
			}
		}

		public void SetExplorerView(ExplorerView view)
		{
			if (!(m_explorerView == view))
			{
				if (m_subsView != null)
				{
					m_subsView.onModPageChanged.RemoveListener(DisplayPageNumber);
				}
				if (m_explorerView != null)
				{
					m_explorerView.onModPageChanged.RemoveListener(DisplayPageNumber);
				}
				m_subsView = null;
				m_explorerView = view;
				if (m_explorerView != null)
				{
					m_explorerView.onModPageChanged.AddListener(DisplayPageNumber);
					DisplayPageNumber(m_explorerView.modPage);
				}
				else
				{
					DisplayPageNumber(null);
				}
			}
		}

		public void DisplayPageNumber(RequestPage<ModProfile> page)
		{
			m_pageSize = 0;
			m_resultIndex = 0;
			if (page != null)
			{
				m_pageSize = page.size;
				m_resultIndex = page.resultOffset;
			}
			Refresh();
		}

		public void Refresh()
		{
			if (base.isActiveAndEnabled)
			{
				int num = 0;
				if (m_pageSize > 0)
				{
					num = 1 + (int)Mathf.Floor((float)m_resultIndex / (float)m_pageSize);
				}
				m_textComponent.text = num.ToString();
			}
		}
	}
}

using UnityEngine;

namespace ModIO.UI
{
	public class PageCountDisplay : MonoBehaviour, ISubscriptionsViewElement, IExplorerViewElement
	{
		private GenericTextComponent m_textComponent;

		private SubscriptionsView m_subsView;

		private ExplorerView m_explorerView;

		private int m_resultCount;

		private int m_pageSize = 1;

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
					m_subsView.onModPageChanged.RemoveListener(DisplayPageCount);
				}
				if (m_explorerView != null)
				{
					m_explorerView.onModPageChanged.RemoveListener(DisplayPageCount);
				}
				m_explorerView = null;
				m_subsView = view;
				if (m_subsView != null)
				{
					m_subsView.onModPageChanged.AddListener(DisplayPageCount);
					DisplayPageCount(m_subsView.modPage);
				}
				else
				{
					DisplayPageCount(null);
				}
			}
		}

		public void SetExplorerView(ExplorerView view)
		{
			if (!(m_explorerView == view))
			{
				if (m_subsView != null)
				{
					m_subsView.onModPageChanged.RemoveListener(DisplayPageCount);
				}
				if (m_explorerView != null)
				{
					m_explorerView.onModPageChanged.RemoveListener(DisplayPageCount);
				}
				m_subsView = null;
				m_explorerView = view;
				if (m_explorerView != null)
				{
					m_explorerView.onModPageChanged.AddListener(DisplayPageCount);
					DisplayPageCount(m_explorerView.modPage);
				}
				else
				{
					DisplayPageCount(null);
				}
			}
		}

		public void DisplayPageCount(RequestPage<ModProfile> page)
		{
			m_resultCount = 0;
			m_pageSize = 1;
			if (page != null && page.size > 0)
			{
				m_resultCount = page.resultTotal;
				m_pageSize = page.size;
			}
			Refresh();
		}

		public void Refresh()
		{
			if (base.isActiveAndEnabled)
			{
				int num = (int)Mathf.Ceil((float)m_resultCount / (float)m_pageSize);
				m_textComponent.text = num.ToString();
			}
		}
	}
}

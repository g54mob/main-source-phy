using UnityEngine;

namespace ModIO.UI
{
	public class ResultCountDisplay : MonoBehaviour, ISubscriptionsViewElement, IExplorerViewElement
	{
		private GenericTextComponent m_textComponent;

		private SubscriptionsView m_subsView;

		private ExplorerView m_explorerView;

		private int m_resultCount;

		protected virtual void Awake()
		{
			Component textDisplayComponent = GenericTextComponent.FindCompatibleTextComponent(base.gameObject);
			m_textComponent.SetTextDisplayComponent(textDisplayComponent);
		}

		protected virtual void OnEnable()
		{
			m_textComponent.text = m_resultCount.ToString();
		}

		public void SetSubscriptionsView(SubscriptionsView view)
		{
			if (!(m_subsView == view))
			{
				if (m_subsView != null)
				{
					m_subsView.onModPageChanged.RemoveListener(DisplayPageTotal);
				}
				if (m_explorerView != null)
				{
					m_explorerView.onModPageChanged.RemoveListener(DisplayPageTotal);
				}
				m_explorerView = null;
				m_subsView = view;
				if (m_subsView != null)
				{
					m_subsView.onModPageChanged.AddListener(DisplayPageTotal);
					DisplayPageTotal(m_subsView.modPage);
				}
				else
				{
					DisplayPageTotal(null);
				}
			}
		}

		public void SetExplorerView(ExplorerView view)
		{
			if (!(m_explorerView == view))
			{
				if (m_subsView != null)
				{
					m_subsView.onModPageChanged.RemoveListener(DisplayPageTotal);
				}
				if (m_explorerView != null)
				{
					m_explorerView.onModPageChanged.RemoveListener(DisplayPageTotal);
				}
				m_subsView = null;
				m_explorerView = view;
				if (m_explorerView != null)
				{
					m_explorerView.onModPageChanged.AddListener(DisplayPageTotal);
					DisplayPageTotal(m_explorerView.modPage);
				}
				else
				{
					DisplayPageTotal(null);
				}
			}
		}

		public void DisplayPageTotal(RequestPage<ModProfile> page)
		{
			m_resultCount = 0;
			if (page != null)
			{
				m_resultCount = page.resultTotal;
			}
			if (base.isActiveAndEnabled)
			{
				m_textComponent.text = m_resultCount.ToString();
			}
		}
	}
}

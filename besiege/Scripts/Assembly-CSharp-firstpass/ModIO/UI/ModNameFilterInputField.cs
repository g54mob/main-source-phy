using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	[RequireComponent(typeof(InputField))]
	public class ModNameFilterInputField : MonoBehaviour, IExplorerViewElement, ISubscriptionsViewElement
	{
		private ExplorerView m_explorerView;

		private SubscriptionsView m_subscriptionsView;

		public void SetExplorerView(ExplorerView view)
		{
			if (!(m_explorerView == view))
			{
				if (m_explorerView != null)
				{
					m_explorerView.onRequestFilterChanged.RemoveListener(UpdateInputField);
					GetComponent<InputField>().onEndEdit.RemoveListener(SetExplorerViewFilter);
				}
				if (m_subscriptionsView != null)
				{
					m_subscriptionsView.onNameFieldFilterChanged.RemoveListener(UpdateInputField);
					GetComponent<InputField>().onValueChanged.RemoveListener(SetSubscriptionsViewFilter);
				}
				m_explorerView = view;
				m_subscriptionsView = null;
				if (m_explorerView != null)
				{
					m_explorerView.onRequestFilterChanged.AddListener(UpdateInputField);
					UpdateInputField(m_explorerView.requestFilter);
					GetComponent<InputField>().onEndEdit.AddListener(SetExplorerViewFilter);
				}
				else
				{
					UpdateInputField(string.Empty);
				}
			}
		}

		public void SetSubscriptionsView(SubscriptionsView view)
		{
			if (!(m_subscriptionsView == view))
			{
				if (m_explorerView != null)
				{
					m_explorerView.onRequestFilterChanged.RemoveListener(UpdateInputField);
					GetComponent<InputField>().onEndEdit.RemoveListener(SetExplorerViewFilter);
				}
				if (m_subscriptionsView != null)
				{
					m_subscriptionsView.onNameFieldFilterChanged.RemoveListener(UpdateInputField);
					GetComponent<InputField>().onValueChanged.RemoveListener(SetSubscriptionsViewFilter);
				}
				m_explorerView = null;
				m_subscriptionsView = view;
				if (m_subscriptionsView != null)
				{
					m_subscriptionsView.onNameFieldFilterChanged.AddListener(UpdateInputField);
					UpdateInputField(m_subscriptionsView.nameFieldFilter);
					GetComponent<InputField>().onValueChanged.AddListener(SetSubscriptionsViewFilter);
				}
				else
				{
					UpdateInputField(string.Empty);
				}
			}
		}

		public virtual void UpdateInputField(RequestFilter requestFilter)
		{
			string filterValue = string.Empty;
			List<IRequestFieldFilter> value;
			if (requestFilter != null && requestFilter.fieldFilterMap.TryGetValue("_q", out value) && value != null && value.Count > 0)
			{
				IRequestFieldFilter requestFieldFilter = value[0];
				switch (requestFieldFilter.filterMethod)
				{
				case FieldFilterMethod.Equal:
					filterValue = ((EqualToFilter<string>)requestFieldFilter).filterValue;
					break;
				case FieldFilterMethod.LikeString:
					filterValue = ((StringLikeFilter)requestFieldFilter).likeValue;
					break;
				}
			}
			UpdateInputField(filterValue);
		}

		public virtual void UpdateInputField(string filterValue)
		{
			base.gameObject.GetComponent<InputField>().text = filterValue;
		}

		protected virtual void SetExplorerViewFilter(string newValue)
		{
			if (m_explorerView != null)
			{
				m_explorerView.SetNameFieldFilter(newValue);
			}
		}

		protected virtual void SetSubscriptionsViewFilter(string newValue)
		{
			if (m_subscriptionsView != null)
			{
				m_subscriptionsView.SetNameFieldFilter(newValue);
			}
		}
	}
}

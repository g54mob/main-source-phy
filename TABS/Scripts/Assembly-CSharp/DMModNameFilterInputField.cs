using ModIO;
using ModIO.UI;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class DMModNameFilterInputField : MonoBehaviour, IExplorerViewElement, ISubscriptionsViewElement
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
				GetComponent<TMP_InputField>().onSubmit.RemoveListener(SetExplorerViewFilter);
				GetComponent<TMP_InputField>().onEndEdit.RemoveListener(SetExplorerViewFilter);
			}
			if (m_subscriptionsView != null)
			{
				m_subscriptionsView.onNameFieldFilterChanged.RemoveListener(UpdateInputField);
				GetComponent<TMP_InputField>().onValueChanged.RemoveListener(SetSubscriptionsViewFilter);
			}
			m_explorerView = view;
			m_subscriptionsView = null;
			if (m_explorerView != null)
			{
				m_explorerView.onRequestFilterChanged.AddListener(UpdateInputField);
				UpdateInputField(m_explorerView.requestFilter);
				GetComponent<TMP_InputField>().onSubmit.AddListener(SetExplorerViewFilter);
				GetComponent<TMP_InputField>().onEndEdit.AddListener(SetExplorerViewFilter);
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
				GetComponent<TMP_InputField>().onSubmit.RemoveListener(SetExplorerViewFilter);
				GetComponent<TMP_InputField>().onEndEdit.RemoveListener(SetExplorerViewFilter);
			}
			if (m_subscriptionsView != null)
			{
				m_subscriptionsView.onNameFieldFilterChanged.RemoveListener(UpdateInputField);
				GetComponent<TMP_InputField>().onValueChanged.RemoveListener(SetSubscriptionsViewFilter);
			}
			m_explorerView = null;
			m_subscriptionsView = view;
			if (m_subscriptionsView != null)
			{
				m_subscriptionsView.onNameFieldFilterChanged.AddListener(UpdateInputField);
				UpdateInputField(m_subscriptionsView.nameFieldFilter);
				GetComponent<TMP_InputField>().onValueChanged.AddListener(SetSubscriptionsViewFilter);
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
		if (requestFilter != null && requestFilter.fieldFilterMap.TryGetValue("_q", out var value) && value != null && value.Count > 0)
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
		base.gameObject.GetComponent<TMP_InputField>().text = filterValue;
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

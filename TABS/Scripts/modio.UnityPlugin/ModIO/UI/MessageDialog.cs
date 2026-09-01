using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class MessageDialog : MonoBehaviour, IBrowserView, ICancelHandler, IEventSystemHandler
	{
		public struct Data
		{
			public string header;

			public string message;

			public Action highlightButtonCallback;

			public string highlightButtonText;

			public Action warningButtonCallback;

			public string warningButtonText;

			public Action standardButtonCallback;

			public string standardButtonText;

			public Action onClose;
		}

		public GenericTextComponent headerText;

		public GenericTextComponent messageText;

		public Button highlightedButton;

		public GenericTextComponent highlightedButtonText;

		public Action highlightedButtonCallback;

		public Button warningButton;

		public GenericTextComponent warningButtonText;

		public Action warningButtonCallback;

		public Button standardButton;

		public GenericTextComponent standardButtonText;

		public Action standardButtonCallback;

		public Action onClose;

		private List<Selectable> m_buttonPriority;

		CanvasGroup IBrowserView.canvasGroup => base.gameObject.GetComponent<CanvasGroup>();

		bool IBrowserView.resetSelectionOnHide => true;

		bool IBrowserView.isRootView => false;

		List<Selectable> IBrowserView.onFocusPriority => m_buttonPriority;

		GameObject IBrowserView.gameObject => base.gameObject;

		private void Awake()
		{
			m_buttonPriority = new List<Selectable> { highlightedButton, standardButton, warningButton };
		}

		private void Start()
		{
			if (highlightedButton != null)
			{
				highlightedButton.onClick.AddListener(delegate
				{
					if (highlightedButtonCallback != null)
					{
						highlightedButtonCallback();
					}
				});
			}
			if (warningButton != null)
			{
				warningButton.onClick.AddListener(delegate
				{
					if (warningButtonCallback != null)
					{
						warningButtonCallback();
					}
				});
			}
			if (!(standardButton != null))
			{
				return;
			}
			standardButton.onClick.AddListener(delegate
			{
				if (standardButtonCallback != null)
				{
					standardButtonCallback();
				}
			});
		}

		private void OnDisable()
		{
			if (onClose != null)
			{
				onClose();
			}
		}

		public void ApplyData(Data data)
		{
			if (headerText.displayComponent != null)
			{
				headerText.text = data.header;
			}
			if (messageText.displayComponent != null)
			{
				messageText.text = data.message;
			}
			if (highlightedButtonText.displayComponent != null)
			{
				highlightedButtonText.text = data.highlightButtonText;
			}
			if (highlightedButton != null)
			{
				highlightedButtonCallback = data.highlightButtonCallback;
				highlightedButton.gameObject.SetActive(data.highlightButtonCallback != null);
			}
			if (warningButtonText.displayComponent != null)
			{
				warningButtonText.text = data.warningButtonText;
			}
			if (warningButton != null)
			{
				warningButtonCallback = data.warningButtonCallback;
				warningButton.gameObject.SetActive(data.warningButtonCallback != null);
			}
			if (standardButtonText.displayComponent != null)
			{
				standardButtonText.text = data.standardButtonText;
			}
			if (standardButton != null)
			{
				standardButtonCallback = data.standardButtonCallback;
				standardButton.gameObject.SetActive(data.standardButtonCallback != null);
			}
			onClose = data.onClose;
		}

		public void OnCancel(BaseEventData eventData)
		{
			Close();
		}

		public void Close()
		{
			ViewManager.instance.CloseWindowedView(this);
		}
	}
}

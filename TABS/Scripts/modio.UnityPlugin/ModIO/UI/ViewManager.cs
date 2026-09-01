using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class ViewManager : MonoBehaviour
	{
		[Serializable]
		public class ViewChangeEvent : UnityEvent<IBrowserView>
		{
		}

		public const int SORTORDER_SPACING = 2;

		private static ViewManager _instance;

		private ExplorerView m_explorerView;

		private SubscriptionsView m_subscriptionsView;

		private InspectorView m_inspectorView;

		private LoginDialog m_loginDialog;

		private MessageDialog m_messageDialog;

		private ReportDialog m_reportDialog;

		private bool m_viewsFound;

		public ViewChangeEvent onBeforeHideView = new ViewChangeEvent();

		public ViewChangeEvent onBeforeShowView = new ViewChangeEvent();

		public ViewChangeEvent onBeforeDefocusView = new ViewChangeEvent();

		public ViewChangeEvent onAfterFocusView = new ViewChangeEvent();

		private List<IBrowserView> m_viewStack = new List<IBrowserView>();

		private IBrowserView[] m_views;

		private int m_rootViewSortOrder;

		public static ViewManager instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = UIUtilities.FindComponentInAllScenes<ViewManager>(includeInactive: true);
					if (_instance == null)
					{
						_instance = new GameObject("View Manager").AddComponent<ViewManager>();
					}
					_instance.FindViews();
				}
				return _instance;
			}
		}

		public ExplorerView explorerView => m_explorerView;

		public SubscriptionsView subscriptionsView => m_subscriptionsView;

		public InspectorView inspectorView => m_inspectorView;

		public LoginDialog loginDialog => m_loginDialog;

		public MessageDialog messageDialog => m_messageDialog;

		public ReportDialog reportDialog => m_reportDialog;

		public IBrowserView currentFocus
		{
			get
			{
				if (m_viewStack == null || m_viewStack.Count == 0)
				{
					return null;
				}
				return m_viewStack[m_viewStack.Count - 1];
			}
		}

		private void Awake()
		{
			if (_instance == null)
			{
				_instance = this;
			}
		}

		private void Start()
		{
			FindViews();
			Canvas componentInParent = m_views[0].gameObject.transform.parent.GetComponentInParent<Canvas>();
			m_rootViewSortOrder = componentInParent.sortingOrder + 2;
			IBrowserView[] views = m_views;
			foreach (IBrowserView browserView in views)
			{
				if (browserView.gameObject.GetComponent<Canvas>() == null)
				{
					Canvas canvas = browserView.gameObject.AddComponent<Canvas>();
					canvas.overridePixelPerfect = false;
					canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.None;
				}
				if (browserView.gameObject.GetComponent<GraphicRaycaster>() == null)
				{
					GraphicRaycaster graphicRaycaster = browserView.gameObject.AddComponent<GraphicRaycaster>();
					graphicRaycaster.ignoreReversedGraphics = true;
					graphicRaycaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;
				}
			}
			List<IBrowserView> list = new List<IBrowserView>();
			if (explorerView != null && explorerView.isActiveAndEnabled)
			{
				list.Add(explorerView);
			}
			if (subscriptionsView != null && subscriptionsView.isActiveAndEnabled)
			{
				if (list.Count == 1)
				{
					list[0] = subscriptionsView;
					explorerView.gameObject.SetActive(value: false);
				}
				else
				{
					list.Add(subscriptionsView);
				}
			}
			if (list.Count == 0)
			{
				if (explorerView != null)
				{
					list.Add(explorerView);
					explorerView.gameObject.SetActive(value: true);
				}
				else if (subscriptionsView != null)
				{
					list.Add(subscriptionsView);
					subscriptionsView.gameObject.SetActive(value: true);
				}
			}
			if (inspectorView != null && inspectorView.isActiveAndEnabled)
			{
				list.Add(inspectorView);
			}
			if (loginDialog != null && loginDialog.isActiveAndEnabled)
			{
				list.Add(loginDialog);
			}
			StartCoroutine(DelayedViewFocusOnStart(list));
		}

		private IEnumerator DelayedViewFocusOnStart(List<IBrowserView> viewStack)
		{
			yield return null;
			if (this != null && viewStack != null && viewStack.Count > 0)
			{
				m_viewStack = viewStack;
				IBrowserView browserView;
				for (int i = 0; i < viewStack.Count - 1; i++)
				{
					browserView = viewStack[i];
					SetSortOrder(browserView, i);
					onBeforeDefocusView.Invoke(browserView);
				}
				browserView = viewStack[viewStack.Count - 1];
				SetSortOrder(browserView, viewStack.Count - 1);
				onAfterFocusView.Invoke(browserView);
			}
		}

		private void FindViews()
		{
			if (!m_viewsFound)
			{
				m_explorerView = GetComponentInChildren<ExplorerView>(includeInactive: true);
				m_subscriptionsView = GetComponentInChildren<SubscriptionsView>(includeInactive: true);
				m_inspectorView = GetComponentInChildren<InspectorView>(includeInactive: true);
				m_loginDialog = GetComponentInChildren<LoginDialog>(includeInactive: true);
				m_messageDialog = GetComponentInChildren<MessageDialog>(includeInactive: true);
				m_reportDialog = GetComponentInChildren<ReportDialog>(includeInactive: true);
				m_viewsFound = true;
				m_views = base.gameObject.GetComponentsInChildren<IBrowserView>(includeInactive: true);
			}
		}

		public void InspectMod(int modId)
		{
			if (!(m_inspectorView == null))
			{
				m_inspectorView.modId = modId;
				FocusView(m_inspectorView);
			}
		}

		public void ReportMod(int modId)
		{
			if (!(m_reportDialog == null))
			{
				m_reportDialog.SetModId(modId);
				FocusView(m_reportDialog);
			}
		}

		public void ActivateExplorerView()
		{
			if (!(m_explorerView == null))
			{
				FocusView(m_explorerView);
			}
		}

		public void ActivateSubscriptionsView()
		{
			if (!(m_subscriptionsView == null))
			{
				FocusView(m_subscriptionsView);
			}
		}

		public void ShowLoginDialog()
		{
			FocusView(m_loginDialog);
		}

		public void ShowMessageDialog(MessageDialog.Data messageData)
		{
			if (!(m_messageDialog == null))
			{
				m_messageDialog.ApplyData(messageData);
				FocusView(m_messageDialog);
			}
		}

		public void ShowReportDialog(int modId)
		{
			if (!(m_reportDialog == null))
			{
				FocusView(m_reportDialog);
			}
		}

		public void FocusView(IBrowserView view)
		{
			if (view == null || view == currentFocus)
			{
				return;
			}
			if (currentFocus != null)
			{
				onBeforeDefocusView.Invoke(currentFocus);
			}
			if (view.isRootView || m_viewStack.Contains(view))
			{
				while (m_viewStack.Count > 0 && currentFocus != view)
				{
					IBrowserView browserView = currentFocus;
					onBeforeHideView.Invoke(browserView);
					m_viewStack.RemoveAt(m_viewStack.Count - 1);
					browserView.gameObject.SetActive(value: false);
				}
			}
			if (currentFocus != view)
			{
				onBeforeShowView.Invoke(view);
				m_viewStack.Add(view);
				view.gameObject.SetActive(value: true);
				SetSortOrder(view, m_viewStack.Count - 1);
			}
			onAfterFocusView.Invoke(view);
		}

		public void CloseWindowedView(IBrowserView view)
		{
			if (view == null || !view.gameObject.activeSelf)
			{
				return;
			}
			int num = m_viewStack.IndexOf(view);
			if (num < 0)
			{
				return;
			}
			if (currentFocus == view)
			{
				PopView();
				return;
			}
			onBeforeHideView.Invoke(view);
			m_viewStack.RemoveAt(num);
			view.gameObject.SetActive(value: false);
			for (int i = num; i < m_viewStack.Count; i++)
			{
				SetSortOrder(m_viewStack[i], i);
			}
		}

		public void PushView(IBrowserView view)
		{
			if (!m_viewStack.Contains(view))
			{
				if (currentFocus != null)
				{
					onBeforeDefocusView.Invoke(currentFocus);
				}
				onBeforeShowView.Invoke(view);
				m_viewStack.Add(view);
				view.gameObject.SetActive(value: true);
				SetSortOrder(view, m_viewStack.Count - 1);
				onAfterFocusView.Invoke(view);
			}
		}

		public void PopView()
		{
			IBrowserView browserView = currentFocus;
			onBeforeDefocusView.Invoke(browserView);
			onBeforeHideView.Invoke(browserView);
			m_viewStack.RemoveAt(m_viewStack.Count - 1);
			browserView.gameObject.SetActive(value: false);
			if (currentFocus != null)
			{
				onAfterFocusView.Invoke(currentFocus);
			}
		}

		private void SetSortOrder(IBrowserView view, int stackIndex)
		{
			Canvas component = view.gameObject.GetComponent<Canvas>();
			component.overrideSorting = true;
			component.sortingOrder = m_rootViewSortOrder + stackIndex * 2;
		}
	}
}

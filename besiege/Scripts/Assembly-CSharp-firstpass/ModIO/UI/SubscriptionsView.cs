using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class SubscriptionsView : MonoBehaviour, IModSubscriptionsUpdateReceiver, IBrowserView
	{
		[Serializable]
		public class ModPageChanged : UnityEvent<RequestPage<ModProfile>>
		{
		}

		[Serializable]
		public class FilterChanged : UnityEvent<string>
		{
		}

		[Serializable]
		public class SortChanged : UnityEvent<Comparison<ModProfile>>
		{
		}

		public List<Selectable> onFocusPriority = new List<Selectable>();

		[Header("UI Components")]
		public ModContainer modContainer;

		[Tooltip("Object to display when there are no subscribed mods")]
		public GameObject noSubscriptionsDisplay;

		[Tooltip("Object to display when there are zero filtered results")]
		public GameObject noResultsDisplay;

		public StateToggleDisplay isActiveIndicator;

		[Header("Events")]
		public ModPageChanged onModPageChanged;

		public FilterChanged onNameFieldFilterChanged;

		public SortChanged onSortDelegateChanged;

		private RequestPage<ModProfile> m_modPage;

		private string m_nameFieldFilter = string.Empty;

		private Comparison<ModProfile> m_sortDelegate;

		[Obsolete("Use SubscriptionView.modContainer instead.")]
		[HideInInspector]
		public GameObject itemPrefab;

		[Obsolete]
		[HideInInspector]
		public ScrollRect scrollView;

		[Obsolete("Use ResultCountDisplay instead.")]
		[HideInInspector]
		public Text resultCount;

		bool IBrowserView.resetSelectionOnHide
		{
			get
			{
				return true;
			}
		}

		bool IBrowserView.isRootView
		{
			get
			{
				return true;
			}
		}

		List<Selectable> IBrowserView.onFocusPriority
		{
			get
			{
				return onFocusPriority;
			}
		}

		public RequestPage<ModProfile> modPage
		{
			get
			{
				return m_modPage;
			}
		}

		public string nameFieldFilter
		{
			get
			{
				return m_nameFieldFilter;
			}
		}

		public Comparison<ModProfile> sortDelegate
		{
			get
			{
				return m_sortDelegate;
			}
		}

		public CanvasGroup canvasGroup
		{
			get
			{
				return base.gameObject.GetComponent<CanvasGroup>();
			}
		}

		[Obsolete("No longer supported.")]
		public IEnumerable<ModView> modViews
		{
			get
			{
				return null;
			}
		}

		virtual GameObject IBrowserView.gameObject
		{
			get
			{
				return base.gameObject;
			}
		}

		[Obsolete("No longer necessary. Event is directly linked to ModBrowser.")]
		public event Action<ModView> inspectRequested;

		[Obsolete("No longer necessary. Event is directly linked to ModBrowser.")]
		public event Action<ModView> subscribeRequested;

		[Obsolete("No longer necessary. Event is directly linked to ModBrowser.")]
		public event Action<ModView> unsubscribeRequested;

		[Obsolete("No longer necessary. Event is directly linked to ModBrowser.")]
		public event Action<ModView> enableModRequested;

		[Obsolete("No longer necessary. Event is directly linked to ModBrowser.")]
		public event Action<ModView> disableModRequested;

		protected virtual void Start()
		{
			ISubscriptionsViewElement[] componentsInChildren = base.gameObject.GetComponentsInChildren<ISubscriptionsViewElement>(true);
			ISubscriptionsViewElement[] array = componentsInChildren;
			foreach (ISubscriptionsViewElement subscriptionsViewElement in array)
			{
				subscriptionsViewElement.SetSubscriptionsView(this);
			}
			DisplayProfiles(null);
			Refresh();
		}

		private void OnEnable()
		{
			if (isActiveIndicator != null)
			{
				isActiveIndicator.isOn = true;
			}
		}

		private void OnDisable()
		{
			if (isActiveIndicator != null)
			{
				isActiveIndicator.isOn = false;
			}
		}

		public void Refresh()
		{
			m_modPage = null;
			if (onModPageChanged != null)
			{
				onModPageChanged.Invoke(m_modPage);
			}
			if (noSubscriptionsDisplay != null)
			{
				noSubscriptionsDisplay.gameObject.SetActive(true);
			}
			if (noResultsDisplay != null)
			{
				noResultsDisplay.gameObject.SetActive(false);
			}
			IList<int> subscribedModIds = LocalUser.SubscribedModIds;
			ModManager.GetModProfiles(subscribedModIds, delegate(ModProfile[] profiles)
			{
				Refresh_OnGetModProfiles(profiles, m_nameFieldFilter, m_sortDelegate);
			}, delegate(WebRequestError requestError)
			{
				MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, "Failed to get subscription data from mod.io servers.\n" + requestError.displayMessage);
			});
		}

		protected virtual void Refresh_OnGetModProfiles(IList<ModProfile> modProfiles, string requestedTitleFilter, Comparison<ModProfile> requestedSortDelegate)
		{
			if (this == null || m_nameFieldFilter != requestedTitleFilter || m_sortDelegate != requestedSortDelegate)
			{
				return;
			}
			List<ModProfile> list = null;
			if (modProfiles == null || modProfiles.Count == 0)
			{
				list = new List<ModProfile>(0);
			}
			else
			{
				Func<ModProfile, bool> func = (ModProfile p) => true;
				if (!string.IsNullOrEmpty(requestedTitleFilter))
				{
					string filterString = requestedTitleFilter.ToUpper();
					func = (ModProfile p) => p.name.ToUpper().Contains(filterString);
				}
				list = new List<ModProfile>(modProfiles.Count);
				foreach (ModProfile modProfile in modProfiles)
				{
					if (func(modProfile))
					{
						list.Add(modProfile);
					}
				}
				if (requestedSortDelegate == null)
				{
					requestedSortDelegate = DefaultSortFunction;
				}
				list.Sort(requestedSortDelegate);
			}
			DisplayProfiles(list);
			if (noSubscriptionsDisplay != null)
			{
				noSubscriptionsDisplay.gameObject.SetActive(list.Count == 0 && string.IsNullOrEmpty(m_nameFieldFilter));
			}
			if (noResultsDisplay != null)
			{
				noResultsDisplay.gameObject.SetActive(list.Count == 0 && !string.IsNullOrEmpty(m_nameFieldFilter));
			}
			m_modPage = new RequestPage<ModProfile>
			{
				size = list.Count,
				resultOffset = 0,
				resultTotal = list.Count,
				items = list.ToArray()
			};
			if (onModPageChanged != null)
			{
				onModPageChanged.Invoke(m_modPage);
			}
		}

		protected virtual void DisplayProfiles(IList<ModProfile> profiles)
		{
			if (profiles == null)
			{
				profiles = new ModProfile[0];
			}
			int count = profiles.Count;
			ModProfile[] array = new ModProfile[count];
			ModStatistics[] array2 = new ModStatistics[count];
			for (int i = 0; i < count; i++)
			{
				ModProfile modProfile = profiles[i];
				ModStatistics modStatistics = null;
				if (modProfile != null)
				{
					modStatistics = modProfile.statistics;
				}
				array[i] = modProfile;
				array2[i] = modStatistics;
			}
			modContainer.DisplayMods(array, array2);
		}

		public void SetNameFieldFilter(string nameFieldFilter)
		{
			if (nameFieldFilter == null)
			{
				nameFieldFilter = string.Empty;
			}
			if (m_nameFieldFilter.ToUpper() != nameFieldFilter.ToUpper())
			{
				m_nameFieldFilter = nameFieldFilter;
				Refresh();
				if (onNameFieldFilterChanged != null)
				{
					onNameFieldFilterChanged.Invoke(m_nameFieldFilter);
				}
			}
		}

		public string GetTitleFilter()
		{
			return m_nameFieldFilter;
		}

		public void SetSortDelegate(Comparison<ModProfile> sortDelegate)
		{
			if (m_sortDelegate != sortDelegate)
			{
				m_sortDelegate = sortDelegate;
				Refresh();
			}
		}

		public Comparison<ModProfile> GetSortDelegate()
		{
			return m_sortDelegate;
		}

		public void OnModSubscriptionsUpdated(IList<int> addedSubscriptions, IList<int> removedSubscriptions)
		{
			Refresh();
		}

		protected virtual int DefaultSortFunction(ModProfile a, ModProfile b)
		{
			if (a == null)
			{
				return 1;
			}
			if (b == null)
			{
				return -1;
			}
			return a.id - b.id;
		}

		[Obsolete("No longer necessary. Initialization occurs in Start().")]
		public void Initialize()
		{
		}

		[Obsolete("No longer necessary. Event is directly linked to ModBrowser.")]
		public void NotifyInspectRequested(ModView view)
		{
			if (this.inspectRequested != null)
			{
				this.inspectRequested(view);
			}
		}

		[Obsolete("No longer necessary. Event is directly linked to ModBrowser.")]
		public void NotifySubscribeRequested(ModView view)
		{
			if (this.subscribeRequested != null)
			{
				this.subscribeRequested(view);
			}
		}

		[Obsolete("No longer necessary. Event is directly linked to ModBrowser.")]
		public void NotifyUnsubscribeRequested(ModView view)
		{
			if (this.unsubscribeRequested != null)
			{
				this.unsubscribeRequested(view);
			}
		}

		[Obsolete("No longer necessary. Event is directly linked to ModBrowser.")]
		public void NotifyEnableRequested(ModView view)
		{
			if (this.enableModRequested != null)
			{
				this.enableModRequested(view);
			}
		}

		[Obsolete("No longer necessary. Event is directly linked to ModBrowser.")]
		public void NotifyDisableRequested(ModView view)
		{
			if (this.disableModRequested != null)
			{
				this.disableModRequested(view);
			}
		}
	}
}

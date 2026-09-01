using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class ExplorerView : MonoBehaviour, IBrowserView
	{
		[Serializable]
		public class ModPageChanged : UnityEvent<RequestPage<ModProfile>>
		{
		}

		[Serializable]
		public class RequestFilterChanged : UnityEvent<RequestFilter>
		{
		}

		[Serializable]
		public struct SortMethod
		{
			public bool ascending;

			public string fieldName;
		}

		[Header("UI Components")]
		public ModContainer pageTemplate;

		public Button prevPageButton;

		public Button nextPageButton;

		[Tooltip("Object to display when no results were found.")]
		public GameObject noResultsDisplay;

		public StateToggleDisplay isActiveIndicator;

		[Header("Settings")]
		public SortMethod defaultSortMethod = new SortMethod
		{
			ascending = false,
			fieldName = "date_live"
		};

		public float pageTransitionTimeSeconds = 0.4f;

		public List<Selectable> onFocusPriority = new List<Selectable>();

		[Header("Events")]
		public ModPageChanged onModPageChanged;

		public RequestFilterChanged onRequestFilterChanged;

		private RequestPage<ModProfile> m_modPage;

		private RequestPage<ModProfile> m_transitionPage;

		private RequestFilter m_requestFilter = new RequestFilter();

		private ModContainer m_modPageContainer;

		private ModContainer m_transitionPageContainer;

		private bool m_isTransitioning;

		private bool refreshOnFocus;

		public List<string> DefaultTagsFilter;

		[HideInInspector]
		[Obsolete("Use ExplorerView.pageTemplate instead.")]
		public GameObject itemPrefab;

		[HideInInspector]
		[Obsolete("Use ExplorerView.defaultSortMethod instead.")]
		public string defaultSortString = string.Empty;

		[Obsolete("Use PageNumberDisplay component instead.")]
		[HideInInspector]
		public Text pageNumberText;

		[HideInInspector]
		[Obsolete("Use PageCountDisplay component instead.")]
		public Text pageCountText;

		[Obsolete("Use ResultCountDisplay component instead.")]
		[HideInInspector]
		public Text resultCountText;

		[HideInInspector]
		[Obsolete("No longer supported.")]
		public RectTransform currentPageContainer;

		[HideInInspector]
		[Obsolete("No longer supported.")]
		public RectTransform transitionPageContainer;

		[HideInInspector]
		[Obsolete("No longer supported.")]
		public GridLayoutGroup gridLayout;

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

		public RequestPage<ModProfile> transitionPage
		{
			get
			{
				return m_transitionPage;
			}
		}

		public RequestFilter requestFilter
		{
			get
			{
				return m_requestFilter;
			}
		}

		protected EqualToFilter<string> nameFieldFilter
		{
			get
			{
				List<IRequestFieldFilter> value = null;
				if (m_requestFilter.fieldFilterMap.TryGetValue("_q", out value) && value != null && value.Count > 0)
				{
					return value[0] as EqualToFilter<string>;
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					m_requestFilter.fieldFilterMap.Remove("_q");
					return;
				}
				List<IRequestFieldFilter> value2 = null;
				if (m_requestFilter.fieldFilterMap.TryGetValue("_q", out value2) && value2 != null && value2.Count > 0)
				{
					value2[0] = value;
				}
				else
				{
					m_requestFilter.AddFieldFilter("_q", value);
				}
			}
		}

		protected MatchesArrayFilter<string> tagMatchFieldFilter
		{
			get
			{
				List<IRequestFieldFilter> value = null;
				if (m_requestFilter.fieldFilterMap.TryGetValue("tags", out value) && value != null && value.Count > 0)
				{
					return value[0] as MatchesArrayFilter<string>;
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					m_requestFilter.fieldFilterMap.Remove("tags");
					return;
				}
				List<IRequestFieldFilter> value2 = null;
				if (m_requestFilter.fieldFilterMap.TryGetValue("tags", out value2) && value2 != null && value2.Count > 0)
				{
					value2[0] = value;
				}
				else
				{
					m_requestFilter.AddFieldFilter("tags", value);
				}
			}
		}

		public CanvasGroup canvasGroup
		{
			get
			{
				return base.gameObject.GetComponent<CanvasGroup>();
			}
		}

		[Obsolete("Use ExplorerView.modPage instead.")]
		public RequestPage<ModProfile> currentPage
		{
			get
			{
				return modPage;
			}
			set
			{
			}
		}

		[Obsolete("Use ExplorerView.transitionPage instead.")]
		public RequestPage<ModProfile> targetPage
		{
			get
			{
				return transitionPage;
			}
		}

		[HideInInspector]
		[Obsolete("No longer necessary.")]
		public RectTransform contentPane
		{
			get
			{
				if (m_modPageContainer != null)
				{
					return m_modPageContainer.transform.parent as RectTransform;
				}
				return null;
			}
			set
			{
			}
		}

		[Obsolete]
		public int itemsPerPage
		{
			get
			{
				return pageTemplate.itemLimit;
			}
		}

		[Obsolete("No longer supported.")]
		public IEnumerable<ModView> modViews
		{
			get
			{
				if (m_modPageContainer != null)
				{
					return m_modPageContainer.GetModViews();
				}
				return null;
			}
		}

		[Obsolete("Use ExplorerView.modPage.CalculatePageIndex() instead.")]
		public int CurrentPageNumber
		{
			get
			{
				if (modPage != null)
				{
					return 1 + modPage.CalculatePageIndex();
				}
				return 0;
			}
		}

		[Obsolete("Use ExplorerView.modPage.CalculatePageCount() instead.")]
		public int CurrentPageCount
		{
			get
			{
				if (modPage != null)
				{
					return modPage.CalculatePageCount();
				}
				return 0;
			}
		}

		virtual GameObject IBrowserView.gameObject
		{
			get
			{
				return base.gameObject;
			}
		}

		[Obsolete("No longer supported. Use ExplorerView.onRequestFilterChanged instead.", true)]
		public event Action<string[]> onTagFilterUpdated;

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

		protected virtual void Awake()
		{
			m_requestFilter = new RequestFilter
			{
				sortFieldName = defaultSortMethod.fieldName,
				isSortAscending = defaultSortMethod.ascending
			};
			SetDefaultTagsFilter();
		}

		private void SetDefaultTagsFilter()
		{
			if (DefaultTagsFilter != null && DefaultTagsFilter.Count != 0)
			{
				MatchesArrayFilter<string> matchesArrayFilter = new MatchesArrayFilter<string>();
				matchesArrayFilter.filterValue = new string[0];
				matchesArrayFilter.filterArray = DefaultTagsFilter.ToArray();
				List<IRequestFieldFilter> list = new List<IRequestFieldFilter>();
				list.Add(matchesArrayFilter);
				m_requestFilter.fieldFilterMap["tags"] = list;
			}
		}

		protected virtual void Start()
		{
			pageTemplate.gameObject.SetActive(false);
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(pageTemplate.gameObject, pageTemplate.transform.parent);
			gameObject.name = "Mod Page A";
			gameObject.SetActive(true);
			gameObject.transform.SetSiblingIndex(pageTemplate.transform.GetSiblingIndex() + 1);
			m_modPageContainer = gameObject.GetComponent<ModContainer>();
			m_modPageContainer.onItemLimitChanged += delegate
			{
				Refresh();
			};
			gameObject = (GameObject)UnityEngine.Object.Instantiate(pageTemplate.gameObject, pageTemplate.transform.parent);
			gameObject.name = "Mod Page B";
			gameObject.SetActive(false);
			gameObject.transform.SetSiblingIndex(pageTemplate.transform.GetSiblingIndex() + 2);
			m_transitionPageContainer = gameObject.GetComponent<ModContainer>();
			IExplorerViewElement[] componentsInChildren = base.gameObject.GetComponentsInChildren<IExplorerViewElement>(true);
			IExplorerViewElement[] array = componentsInChildren;
			foreach (IExplorerViewElement explorerViewElement in array)
			{
				explorerViewElement.SetExplorerView(this);
			}
			UpdateModPageDisplay();
			UpdatePageButtonInteractibility();
			Refresh();
		}

		private void OnEnable()
		{
			if (isActiveIndicator != null)
			{
				isActiveIndicator.isOn = true;
			}
			if (DefaultTagsFilter != null && DefaultTagsFilter.Count > 0)
			{
				SetTagFilter(DefaultTagsFilter);
			}
			ViewManager.instance.onAfterFocusView.AddListener(OnAfterFocusView);
		}

		private void OnDisable()
		{
			if (isActiveIndicator != null)
			{
				isActiveIndicator.isOn = false;
			}
			ViewManager.instance.onAfterFocusView.RemoveListener(OnAfterFocusView);
		}

		public void Refresh()
		{
			int num = 0;
			int num2 = m_modPageContainer.itemLimit;
			if (num2 < 0)
			{
				num2 = 100;
			}
			int resultOffset = num * num2;
			bool wasDisplayUpdated = false;
			RequestPage<ModProfile> filteredPage = new RequestPage<ModProfile>
			{
				size = num2,
				items = new ModProfile[num2],
				resultOffset = resultOffset,
				resultTotal = 0
			};
			m_modPage = filteredPage;
			ModManager.GetRangeOfModProfiles(m_requestFilter, resultOffset, num2, delegate(RequestPage<ModProfile> page)
			{
				if (this != null && m_modPage == filteredPage)
				{
					DisplayModPage(page);
					wasDisplayUpdated = true;
				}
			}, null);
			if (!wasDisplayUpdated)
			{
				m_modPage = null;
				DisplayModPage(filteredPage);
			}
		}

		private void OnAfterFocusView(IBrowserView view)
		{
			if (!(view as ExplorerView != this) && refreshOnFocus)
			{
				Refresh();
				refreshOnFocus = false;
			}
		}

		public void RefreshOnNextFocus()
		{
			refreshOnFocus = true;
		}

		public void UpdatePageButtonInteractibility()
		{
			if (prevPageButton != null)
			{
				prevPageButton.interactable = !m_isTransitioning && modPage != null && modPage.CalculatePageIndex() > 0;
			}
			if (nextPageButton != null)
			{
				nextPageButton.interactable = !m_isTransitioning && modPage != null && modPage.CalculatePageIndex() + 1 < modPage.CalculatePageCount();
			}
		}

		public void ChangePage(int pageDifferential)
		{
			if (m_isTransitioning)
			{
				return;
			}
			int num = m_modPageContainer.itemLimit;
			if (num < 0)
			{
				num = 100;
			}
			int num2 = m_modPage.CalculatePageIndex() + pageDifferential;
			int num3 = num2 * num;
			int num4 = m_modPage.CalculatePageCount();
			int num5 = Mathf.Min(num, m_modPage.resultTotal - num3);
			if (num2 >= num4)
			{
				num2 = num4 - 1;
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			if (num2 == m_modPage.CalculatePageIndex())
			{
				return;
			}
			RequestPage<ModProfile> transitionPlaceholder = new RequestPage<ModProfile>
			{
				size = num,
				items = new ModProfile[num5],
				resultOffset = num3,
				resultTotal = m_modPage.resultTotal
			};
			m_transitionPage = transitionPlaceholder;
			UpdateTransitionPageDisplay();
			ModManager.GetRangeOfModProfiles(m_requestFilter, num3, num, delegate(RequestPage<ModProfile> page)
			{
				if (m_transitionPage == transitionPlaceholder)
				{
					m_transitionPage = page;
					UpdateTransitionPageDisplay();
				}
				if (m_modPage == transitionPlaceholder)
				{
					DisplayModPage(page);
				}
			}, null);
			PageTransitionDirection direction = ((pageDifferential >= 0) ? PageTransitionDirection.FromRight : PageTransitionDirection.FromLeft);
			InitiateTargetPageTransition(direction, null);
			UpdatePageButtonInteractibility();
		}

		public void SetNameFieldFilter(string nameFilter)
		{
			EqualToFilter<string> equalToFilter = nameFieldFilter;
			if (nameFilter == null)
			{
				nameFilter = string.Empty;
			}
			string text = string.Empty;
			if (equalToFilter != null && equalToFilter.filterValue != null)
			{
				text = equalToFilter.filterValue;
			}
			if (text.ToUpper() != nameFilter.ToUpper())
			{
				EqualToFilter<string> equalToFilter2 = null;
				if (!string.IsNullOrEmpty(nameFilter))
				{
					EqualToFilter<string> equalToFilter3 = new EqualToFilter<string>();
					equalToFilter3.filterValue = nameFilter;
					equalToFilter2 = equalToFilter3;
				}
				SetFieldFilters("_q", equalToFilter2);
			}
		}

		public string GetTitleFilter()
		{
			EqualToFilter<string> equalToFilter = nameFieldFilter;
			if (equalToFilter == null)
			{
				return null;
			}
			return equalToFilter.filterValue;
		}

		public void SetSortMethod(SortMethod sortMethod)
		{
			if (sortMethod.fieldName == null)
			{
				sortMethod.fieldName = string.Empty;
			}
			if (m_requestFilter.sortFieldName.ToUpper() != sortMethod.fieldName.ToUpper() || m_requestFilter.isSortAscending != sortMethod.ascending)
			{
				m_requestFilter.sortFieldName = sortMethod.fieldName;
				m_requestFilter.isSortAscending = sortMethod.ascending;
				if (base.isActiveAndEnabled)
				{
					Refresh();
				}
				if (onRequestFilterChanged != null)
				{
					onRequestFilterChanged.Invoke(m_requestFilter);
				}
			}
		}

		public void SetSortMethod(bool ascending, string fieldName)
		{
			SortMethod sortMethod = new SortMethod
			{
				ascending = ascending,
				fieldName = fieldName
			};
			SetSortMethod(sortMethod);
		}

		public SortMethod GetSortMethod()
		{
			return new SortMethod
			{
				ascending = m_requestFilter.isSortAscending,
				fieldName = m_requestFilter.sortFieldName
			};
		}

		public void SetTagFilter(IList<string> tagFilter)
		{
			MatchesArrayFilter<string> matchesArrayFilter = tagMatchFieldFilter;
			if (tagFilter == null)
			{
				tagFilter = new string[0];
			}
			string[] array = new string[0];
			if (matchesArrayFilter != null)
			{
				array = matchesArrayFilter.filterArray;
			}
			bool flag = array.Length == tagFilter.Count;
			string[] array2 = new string[tagFilter.Count];
			if (tagFilter != array)
			{
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = tagFilter[i];
					flag = flag && array[i] == array2[i];
				}
			}
			if (!flag)
			{
				MatchesArrayFilter<string> matchesArrayFilter2 = null;
				if (array2.Length > 0)
				{
					MatchesArrayFilter<string> matchesArrayFilter3 = new MatchesArrayFilter<string>();
					matchesArrayFilter3.filterArray = array2;
					matchesArrayFilter2 = matchesArrayFilter3;
				}
				SetFieldFilters("tags", matchesArrayFilter2);
			}
		}

		public string[] GetTagFilter()
		{
			MatchesArrayFilter<string> matchesArrayFilter = tagMatchFieldFilter;
			if (matchesArrayFilter == null)
			{
				return null;
			}
			return matchesArrayFilter.filterArray;
		}

		public void AddTagToFilter(string tagName)
		{
			MatchesArrayFilter<string> matchesArrayFilter = tagMatchFieldFilter;
			if (matchesArrayFilter == null)
			{
				matchesArrayFilter = new MatchesArrayFilter<string>();
				matchesArrayFilter.filterValue = new string[0];
			}
			List<string> list = new List<string>(matchesArrayFilter.filterArray);
			if (!list.Contains(tagName))
			{
				list.Add(tagName);
				matchesArrayFilter.filterArray = list.ToArray();
				SetFieldFilters("tags", matchesArrayFilter);
			}
		}

		public void RemoveTagFromFilter(string tagName)
		{
			MatchesArrayFilter<string> matchesArrayFilter = tagMatchFieldFilter;
			if (matchesArrayFilter == null || matchesArrayFilter.filterArray == null || matchesArrayFilter.filterArray.Length == 0)
			{
				return;
			}
			List<string> list = new List<string>(matchesArrayFilter.filterArray);
			if (list.Contains(tagName))
			{
				list.Remove(tagName);
				if (list.Count == 0)
				{
					matchesArrayFilter = null;
				}
				else
				{
					matchesArrayFilter.filterArray = list.ToArray();
				}
				SetFieldFilters("tags", matchesArrayFilter);
			}
		}

		public void SetFieldFilters(string fieldName, params IRequestFieldFilter[] filters)
		{
			if (filters.Length == 0 || (filters.Length == 1 && filters[0] == null))
			{
				m_requestFilter.fieldFilterMap.Remove(fieldName);
			}
			else
			{
				m_requestFilter.fieldFilterMap[fieldName] = new List<IRequestFieldFilter>(filters);
			}
			if (base.isActiveAndEnabled)
			{
				Refresh();
			}
			if (onRequestFilterChanged != null)
			{
				onRequestFilterChanged.Invoke(m_requestFilter);
			}
		}

		public void UpdateModPageDisplay()
		{
			if (!(m_modPageContainer == null))
			{
				if (noResultsDisplay != null)
				{
					noResultsDisplay.SetActive(m_modPage == null || m_modPage.items == null || m_modPage.items.Length == 0);
				}
				IList<ModProfile> profileCollection = null;
				if (m_modPage != null)
				{
					profileCollection = m_modPage.items;
				}
				DisplayProfiles(profileCollection, m_modPageContainer);
			}
		}

		public void UpdateTransitionPageDisplay()
		{
			if (!(m_transitionPageContainer == null))
			{
				DisplayProfiles(m_transitionPage.items, m_transitionPageContainer);
			}
		}

		protected virtual void DisplayProfiles(IList<ModProfile> profileCollection, ModContainer modContainer)
		{
			if (profileCollection == null)
			{
				profileCollection = new ModProfile[0];
			}
			int count = profileCollection.Count;
			ModProfile[] array = new ModProfile[count];
			ModStatistics[] array2 = new ModStatistics[count];
			for (int i = 0; i < count; i++)
			{
				ModProfile modProfile = profileCollection[i];
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

		public void InitiateTargetPageTransition(PageTransitionDirection direction, Action onTransitionCompleted)
		{
			if (!m_isTransitioning)
			{
				float width = ((RectTransform)m_modPageContainer.transform.parent).rect.width;
				float num = width * ((direction != PageTransitionDirection.FromLeft) ? (-1f) : 1f);
				float num2 = num * -1f;
				m_modPageContainer.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
				m_transitionPageContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(num2, 0f);
				StartCoroutine(TransitionPageCoroutine(num, num2, pageTransitionTimeSeconds, onTransitionCompleted));
			}
		}

		private IEnumerator TransitionPageCoroutine(float mainPaneTargetX, float transitionPaneStartX, float transitionLength, Action onTransitionCompleted)
		{
			m_isTransitioning = true;
			m_transitionPageContainer.gameObject.SetActive(true);
			float transitionTime = 0f;
			while (transitionTime < transitionLength)
			{
				float transPos = Mathf.Lerp(0f, mainPaneTargetX, transitionTime / transitionLength);
				m_modPageContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(transPos, 0f);
				m_transitionPageContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(transPos + transitionPaneStartX, 0f);
				transitionTime += Time.unscaledDeltaTime;
				yield return null;
			}
			ModContainer tempContainer = m_modPageContainer;
			m_modPageContainer = m_transitionPageContainer;
			m_transitionPageContainer = tempContainer;
			RequestPage<ModProfile> tempPage = modPage;
			m_modPage = m_transitionPage;
			m_transitionPage = tempPage;
			m_modPageContainer.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			m_transitionPageContainer.gameObject.SetActive(false);
			m_isTransitioning = false;
			UpdatePageButtonInteractibility();
			if (onModPageChanged != null)
			{
				onModPageChanged.Invoke(m_modPage);
			}
			if (onTransitionCompleted != null)
			{
				onTransitionCompleted();
			}
		}

		public void ClearAllFilters()
		{
			if (nameFieldFilter != null || tagMatchFieldFilter != null || !GetSortMethod().Equals(defaultSortMethod))
			{
				m_requestFilter = new RequestFilter
				{
					sortFieldName = defaultSortMethod.fieldName,
					isSortAscending = defaultSortMethod.ascending
				};
				Refresh();
				if (onRequestFilterChanged != null)
				{
					onRequestFilterChanged.Invoke(m_requestFilter);
				}
			}
		}

		protected void DisplayModPage(RequestPage<ModProfile> newModPage)
		{
			if (m_modPage != newModPage)
			{
				m_modPage = newModPage;
				UpdateModPageDisplay();
				UpdatePageButtonInteractibility();
				if (onModPageChanged != null)
				{
					onModPageChanged.Invoke(newModPage);
				}
			}
		}

		[Obsolete("No longer necessary. Initialization occurs in Start().")]
		public void Initialize()
		{
		}

		[Obsolete("Use ExplorerView.UpdateModPageDisplay() instead.")]
		public void UpdateCurrentPageDisplay()
		{
			UpdateModPageDisplay();
		}

		[Obsolete("Use ExplorerView.UpdateTransitionPageDisplay() instead.")]
		public void UpdateTargetPageDisplay()
		{
			UpdateTransitionPageDisplay();
		}

		[Obsolete("Use ExplorerView.ClearAllFilters() instead.")]
		public void ClearFilters()
		{
			ClearAllFilters();
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

		[Obsolete("Use ExplorerView.SetSortMethod() instead.")]
		public void SetSortString(string sortString)
		{
			if (sortString == null)
			{
				sortString = string.Empty;
			}
			string fieldName = sortString;
			bool flag = true;
			if (sortString.StartsWith("-"))
			{
				flag = false;
				fieldName = ((sortString.Length <= 1) ? string.Empty : sortString.Substring(1));
			}
			SetSortMethod(flag, fieldName);
		}

		[Obsolete("Use ExplorerView.GetSortMethod() instead.")]
		public string GetSortString()
		{
			return ((!m_requestFilter.isSortAscending) ? "-" : string.Empty) + m_requestFilter.sortFieldName;
		}

		[Obsolete]
		public RequestFilter GenerateRequestFilter()
		{
			return m_requestFilter;
		}
	}
}

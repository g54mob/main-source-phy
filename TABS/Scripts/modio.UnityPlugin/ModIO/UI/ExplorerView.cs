using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class ExplorerView : MonoBehaviour, IBrowserView, IModSubscriptionsUpdateReceiver
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

		public enum ContentType
		{
			Battle = 0,
			Unit = 1,
			Campaign = 2,
			Faction = 3,
			Map = 4
		}

		public enum SearchMethod
		{
			All = 0,
			Subscriptions = 1,
			Creations = 2
		}

		public string defaultTab = "Battle";

		public string platformTag;

		public GenericTextComponent explorerTitleText;

		public GenericTextComponent explorerFiltersTitleText;

		public GenericTextComponent explorerFiltersTitleTemplate;

		private GenericTextComponent explorerFiltersTitleCurrent;

		public Transform explorerFiltersTitleTemplateContainer;

		public GameObject tabs;

		public GameObject updateTitle;

		public GameObject searchModeButtons;

		public GenericTextComponent modResultCountText;

		public GameObject defaultSelection;

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
			fieldName = "popular"
		};

		[Tooltip("Object to display when there is no internet connection.")]
		public GameObject noConnectionDisplay;

		public float pageTransitionTimeSeconds = 0.4f;

		public List<Selectable> onFocusPriority = new List<Selectable>();

		[Header("Events")]
		public ModPageChanged onModPageChanged;

		public RequestFilterChanged onRequestFilterChanged;

		private int displayPageIndex;

		private RequestPage<ModProfile> m_modPage;

		private RequestPage<ModProfile> m_transitionPage;

		private RequestFilter m_requestFilter = new RequestFilter();

		private ModContainer m_modPageContainer;

		private ModContainer m_transitionPageContainer;

		private bool m_isTransitioning;

		private Action<WebRequestError> refreshError;

		private readonly List<string> tempProhibitedTags = new List<string>();

		private SearchMethod searchMethod;

		[Obsolete("Use ExplorerView.pageTemplate instead.")]
		[HideInInspector]
		public GameObject itemPrefab;

		[Obsolete("Use ExplorerView.defaultSortMethod instead.")]
		[HideInInspector]
		public string defaultSortString = string.Empty;

		[Obsolete("Use PageNumberDisplay component instead.")]
		[HideInInspector]
		public Text pageNumberText;

		[Obsolete("Use PageCountDisplay component instead.")]
		[HideInInspector]
		public Text pageCountText;

		[Obsolete("Use ResultCountDisplay component instead.")]
		[HideInInspector]
		public Text resultCountText;

		[Obsolete("No longer supported.")]
		[HideInInspector]
		public RectTransform currentPageContainer;

		[Obsolete("No longer supported.")]
		[HideInInspector]
		public RectTransform transitionPageContainer;

		[Obsolete("No longer supported.")]
		[HideInInspector]
		public GridLayoutGroup gridLayout;

		public RequestPage<ModProfile> modPage => m_modPage;

		public RequestPage<ModProfile> transitionPage => m_transitionPage;

		public RequestFilter requestFilter => m_requestFilter;

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

		private ModProfileRequestManager profileManager => ModProfileRequestManager.instance;

		public CanvasGroup canvasGroup => base.gameObject.GetComponent<CanvasGroup>();

		bool IBrowserView.resetSelectionOnHide => true;

		bool IBrowserView.isRootView => true;

		List<Selectable> IBrowserView.onFocusPriority => onFocusPriority;

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
		public RequestPage<ModProfile> targetPage => transitionPage;

		[Obsolete("No longer necessary.")]
		[HideInInspector]
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
		public int itemsPerPage => pageTemplate.itemLimit;

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

		GameObject IBrowserView.gameObject => base.gameObject;

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
			AddTagToFilter(defaultTab);
			AddTagsToFilter(ModManager.GetTagsToDisableCrossPlatformMods());
		}

		protected virtual void Start()
		{
			refreshError = InternetConnectionWebRequestError;
			pageTemplate.gameObject.SetActive(value: false);
			GameObject gameObject = UnityEngine.Object.Instantiate(pageTemplate.gameObject, pageTemplate.transform.parent);
			gameObject.name = "Mod Page A";
			gameObject.SetActive(value: true);
			gameObject.transform.SetSiblingIndex(pageTemplate.transform.GetSiblingIndex() + 1);
			m_modPageContainer = gameObject.GetComponent<ModContainer>();
			m_modPageContainer.onItemLimitChanged += delegate
			{
				Refresh();
			};
			gameObject = UnityEngine.Object.Instantiate(pageTemplate.gameObject, pageTemplate.transform.parent);
			gameObject.name = "Mod Page B";
			gameObject.SetActive(value: false);
			gameObject.transform.SetSiblingIndex(pageTemplate.transform.GetSiblingIndex() + 2);
			m_transitionPageContainer = gameObject.GetComponent<ModContainer>();
			IExplorerViewElement[] componentsInChildren = base.gameObject.GetComponentsInChildren<IExplorerViewElement>(includeInactive: true);
			for (int num = 0; num < componentsInChildren.Length; num++)
			{
				componentsInChildren[num].SetExplorerView(this);
			}
			UpdateModPageDisplay();
			UpdatePageButtonInteractibility();
			Refresh();
		}

		private void InternetConnectionWebRequestError(WebRequestError error)
		{
			ShowNoConnectionDialog(active: true);
			Debug.LogWarning("[mod.io] Web Request Failed\n" + error.ToUnityDebugString());
		}

		private void ShowNoConnectionDialog(bool active)
		{
			if (noConnectionDisplay != null)
			{
				noConnectionDisplay.SetActive(active);
			}
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
			if (m_modPageContainer == null)
			{
				return;
			}
			displayPageIndex = 0;
			int num = m_modPageContainer.itemLimit;
			if (num < 0)
			{
				num = 100;
			}
			int num2 = 0 * num;
			bool wasDisplayUpdated = false;
			RequestPage<ModProfile> filteredPage = new RequestPage<ModProfile>
			{
				size = num,
				items = new ModProfile[num],
				resultOffset = num2,
				resultTotal = 0
			};
			m_modPage = filteredPage;
			Action<RequestPage<ModProfile>> onSuccessCallback = delegate(RequestPage<ModProfile> page)
			{
				if (this != null && m_modPage == filteredPage)
				{
					page.items = GetAcceptedProfiles(page.items);
					DisplayModPage(page);
					wasDisplayUpdated = true;
				}
			};
			ShowNoConnectionDialog(active: false);
			SearchMods(onSuccessCallback, refreshError, num2, num);
			if (!wasDisplayUpdated)
			{
				m_modPage = null;
				DisplayModPage(filteredPage);
			}
			UpdateFiltersTitle();
		}

		public void ClearCache()
		{
		}

		public void ClearCacheAndRefresh()
		{
			Refresh();
		}

		private bool ValidateTagFilter()
		{
			string[] tagFilter = GetTagFilter();
			if (tagFilter == null || tagFilter.Length < 1)
			{
				return false;
			}
			List<string> list = new List<string>(tagFilter);
			int num = 0;
			string[] array = tagFilter;
			foreach (string text in array)
			{
				if (Enum.TryParse<ContentType>(text, out var _))
				{
					num++;
					if (num > 1)
					{
						list.Remove(text);
					}
				}
			}
			if (num == 0)
			{
				return false;
			}
			tagMatchFieldFilter.filterValue = list.ToArray();
			return true;
		}

		private void SearchMods(Action<RequestPage<ModProfile>> onSuccessCallback, Action<WebRequestError> onErrorCallback, int pageOffset, int pageSize)
		{
			if (ValidateTagFilter() && canvasGroup.interactable)
			{
				ModProfileRequestManager.instance.FetchModProfilePage(m_requestFilter, pageOffset, pageSize, searchMethod, onSuccessCallback, onErrorCallback);
			}
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

		private ModProfile[] GetAcceptedProfiles(ModProfile[] profiles)
		{
			List<ModProfile> list = new List<ModProfile>();
			foreach (ModProfile modProfile in profiles)
			{
				if (modProfile.status == ModStatus.Accepted)
				{
					list.Add(modProfile);
				}
			}
			return list.ToArray();
		}

		private void UpdatePageCountText()
		{
			if (modPage != null)
			{
				modResultCountText.text = displayPageIndex + 1 + " / " + Mathf.Max(modPage.CalculatePageCount(), 1);
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
			displayPageIndex += pageDifferential;
			RequestPage<ModProfile> transitionPlaceholder = new RequestPage<ModProfile>
			{
				size = num,
				items = new ModProfile[num5],
				resultOffset = num3,
				resultTotal = m_modPage.resultTotal
			};
			m_transitionPage = transitionPlaceholder;
			UpdateTransitionPageDisplay();
			ModProfileRequestManager.instance.FetchModProfilePage(m_requestFilter, num3, num, searchMethod, delegate(RequestPage<ModProfile> page)
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
					equalToFilter2 = new EqualToFilter<string>
					{
						filterValue = nameFilter
					};
				}
				SetFieldFilters("_q", equalToFilter2);
			}
		}

		public string GetTitleFilter()
		{
			return nameFieldFilter?.filterValue;
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
				if (array2.Length != 0)
				{
					matchesArrayFilter2 = new MatchesArrayFilter<string>
					{
						filterArray = array2
					};
				}
				SetFieldFilters("tags", matchesArrayFilter2);
			}
		}

		public string[] GetTagFilter()
		{
			return tagMatchFieldFilter?.filterArray;
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

		private void AddTagsToFilter(string[] tagNames)
		{
			if (tagNames != null && tagNames.Length != 0)
			{
				int i = 0;
				for (int num = tagNames.Length; i < num; i++)
				{
					AddTagToFilter(tagNames[i]);
				}
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

		private void GenerateNewTextObject()
		{
			Component component = UnityEngine.Object.Instantiate(explorerFiltersTitleTemplate.displayComponent, explorerFiltersTitleTemplateContainer);
			component.gameObject.SetActive(value: true);
			explorerFiltersTitleCurrent.SetTextDisplayComponent(component);
		}

		public void UpdateFiltersTitle()
		{
			for (int i = 1; i < explorerFiltersTitleTemplateContainer.childCount; i++)
			{
				UnityEngine.Object.Destroy(explorerFiltersTitleTemplateContainer.GetChild(i).gameObject);
			}
			GenerateNewTextObject();
			switch (GetSortMethod().fieldName)
			{
			case "date_live":
				explorerFiltersTitleCurrent.text = "Newest";
				break;
			case "popular":
				explorerFiltersTitleCurrent.text = "Popularity";
				break;
			case "subscribers":
				explorerFiltersTitleCurrent.text = "Subscribers";
				break;
			case "rating":
				explorerFiltersTitleCurrent.text = "Rating";
				break;
			}
			GenerateNewTextObject();
			explorerFiltersTitleCurrent.text = " | ";
			GenerateNewTextObject();
			List<IRequestFieldFilter> value = null;
			if (m_requestFilter.fieldFilterMap.TryGetValue("date_live", out value) && value != null)
			{
				foreach (IRequestFieldFilter item in value)
				{
					DateTime dateTime = ServerTimeStamp.ToLocalDateTime((int)item.filterValue);
					int days = (ServerTimeStamp.ToLocalDateTime(ServerTimeStamp.Now) - dateTime).Days;
					if (days > 364)
					{
						explorerFiltersTitleCurrent.text = "All Time";
					}
					else if (days > 27)
					{
						explorerFiltersTitleCurrent.text = "This Month";
					}
					else if (days > 2)
					{
						explorerFiltersTitleCurrent.text = "This Week";
					}
					else
					{
						explorerFiltersTitleCurrent.text = "Today";
					}
				}
			}
			else
			{
				explorerFiltersTitleCurrent.text = "All Time";
			}
			GenerateNewTextObject();
			explorerFiltersTitleCurrent.text = " | ";
			string[] tagFilter = GetTagFilter();
			tagFilter = RemovePlatformProhibitedDisplayTags(tagFilter);
			if (tagFilter != null && tagFilter.Length > 1)
			{
				string text = null;
				for (int j = 0; j < tagFilter.Length; j++)
				{
					text = tagFilter[j];
					if (Enum.TryParse<ContentType>(text, out var _))
					{
						text = null;
						continue;
					}
					bool num = j >= 4;
					string text2 = (num ? "..." : text);
					GenerateNewTextObject();
					explorerFiltersTitleCurrent.text = text2;
					if (num)
					{
						UnityEngine.Object.Destroy(explorerFiltersTitleTemplateContainer.GetChild(explorerFiltersTitleTemplateContainer.childCount - 2).gameObject);
						continue;
					}
					GenerateNewTextObject();
					explorerFiltersTitleCurrent.text = ", ";
				}
				if (tagFilter.Length < 4)
				{
					string text3 = explorerFiltersTitleCurrent.text;
					explorerFiltersTitleCurrent.text = text3.Substring(0, text3.Length - 2);
					UnityEngine.Object.Destroy(explorerFiltersTitleTemplateContainer.GetChild(explorerFiltersTitleTemplateContainer.childCount - 1).gameObject);
				}
			}
			else
			{
				GenerateNewTextObject();
				explorerFiltersTitleCurrent.text = "All Tags";
			}
		}

		private string[] RemovePlatformProhibitedDisplayTags(string[] filters)
		{
			if (filters == null || filters.Length == 0)
			{
				return filters;
			}
			string[] prohibitedTags = GetPlatformProhibitedDisplayTags();
			if (prohibitedTags == null || prohibitedTags.Length == 0)
			{
				return filters;
			}
			tempProhibitedTags.Clear();
			int i = 0;
			for (int num = filters.Length; i < num; i++)
			{
				string text = filters[i];
				if (!IsTagProhibited(text))
				{
					tempProhibitedTags.Add(text);
				}
			}
			return tempProhibitedTags.ToArray();
			bool IsTagProhibited(string filterTag)
			{
				if (string.IsNullOrEmpty(filterTag))
				{
					return false;
				}
				int j = 0;
				for (int num2 = prohibitedTags.Length; j < num2; j++)
				{
					if (filterTag.ToUpper() == prohibitedTags[j])
					{
						return true;
					}
				}
				return false;
			}
		}

		private static string[] GetPlatformProhibitedDisplayTags()
		{
			return null;
		}

		public void SetSearchMethod(int searchMethod)
		{
			this.searchMethod = (SearchMethod)searchMethod;
			switch (this.searchMethod)
			{
			case SearchMethod.All:
				explorerTitleText.text = "Browse";
				break;
			case SearchMethod.Subscriptions:
				explorerTitleText.text = "Subscriptions";
				break;
			case SearchMethod.Creations:
				explorerTitleText.text = "My Creations";
				break;
			}
		}

		public void SetFieldFilters(string fieldName, params IRequestFieldFilter[] filters)
		{
			if (filters == null || filters.Length == 0 || (filters.Length == 1 && filters[0] == null))
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
			ModProfile[] displayProfiles = new ModProfile[count];
			ModStatistics[] displayStats = new ModStatistics[count];
			List<int> list = new List<int>(count);
			for (int i = 0; i < count; i++)
			{
				ModProfile modProfile = profileCollection[i];
				ModStatistics modStatistics = null;
				if (modProfile != null)
				{
					modStatistics = modProfile.statistics;
				}
				displayProfiles[i] = modProfile;
				displayStats[i] = modStatistics;
			}
			modContainer.DisplayMods(displayProfiles, displayStats);
			UpdatePageCountText();
			if (list.Count <= 0)
			{
				return;
			}
			ModStatisticsRequestManager.instance.RequestModStatistics(list, delegate(ModStatistics[] statsArray)
			{
				if (this != null && modContainer != null)
				{
					bool flag = displayProfiles.Length == modContainer.modProfiles.Length;
					int num = 0;
					while (flag && num < displayProfiles.Length)
					{
						ModProfile modProfile2 = displayProfiles[num];
						flag = modProfile2 == modContainer.modProfiles[num];
						if (flag && modProfile2 != null && displayStats[num] == null)
						{
							foreach (ModStatistics modStatistics2 in statsArray)
							{
								if (modStatistics2 != null && modStatistics2.modId == modProfile2.id)
								{
									displayStats[num] = modStatistics2;
									break;
								}
							}
						}
						num++;
					}
					if (flag)
					{
						modContainer.DisplayMods(displayProfiles, displayStats);
					}
				}
			}, WebRequestError.LogAsWarning);
		}

		public void InitiateTargetPageTransition(PageTransitionDirection direction, Action onTransitionCompleted)
		{
			if (!m_isTransitioning)
			{
				float num = ((RectTransform)m_modPageContainer.transform.parent).rect.width * ((direction == PageTransitionDirection.FromLeft) ? 1f : (-1f));
				float num2 = num * -1f;
				m_modPageContainer.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
				m_transitionPageContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(num2, 0f);
				StartCoroutine(TransitionPageCoroutine(num, num2, pageTransitionTimeSeconds, onTransitionCompleted));
			}
		}

		private IEnumerator TransitionPageCoroutine(float mainPaneTargetX, float transitionPaneStartX, float transitionLength, Action onTransitionCompleted)
		{
			m_isTransitioning = true;
			m_transitionPageContainer.gameObject.SetActive(value: true);
			float transitionTime = 0f;
			while (transitionTime < transitionLength)
			{
				float num = Mathf.Lerp(0f, mainPaneTargetX, transitionTime / transitionLength);
				m_modPageContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(num, 0f);
				m_transitionPageContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(num + transitionPaneStartX, 0f);
				transitionTime += Time.unscaledDeltaTime;
				yield return null;
			}
			ModContainer modPageContainer = m_modPageContainer;
			m_modPageContainer = m_transitionPageContainer;
			m_transitionPageContainer = modPageContainer;
			RequestPage<ModProfile> requestPage = modPage;
			m_modPage = m_transitionPage;
			m_transitionPage = requestPage;
			m_modPageContainer.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			m_transitionPageContainer.gameObject.SetActive(value: false);
			m_isTransitioning = false;
			UpdatePageButtonInteractibility();
			if (onModPageChanged != null)
			{
				onModPageChanged.Invoke(m_modPage);
			}
			onTransitionCompleted?.Invoke();
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
				AddTagToFilter(defaultTab);
				AddTagsToFilter(ModManager.GetTagsToDisableCrossPlatformMods());
				Refresh();
				if (onRequestFilterChanged != null)
				{
					onRequestFilterChanged.Invoke(m_requestFilter);
				}
			}
		}

		public void ClearAllFilters(string newDefaultTab)
		{
			defaultTab = newDefaultTab;
			ClearAllFilters();
		}

		protected void DisplayModPage(RequestPage<ModProfile> newModPage)
		{
			if (m_modPage != newModPage)
			{
				m_modPage = newModPage;
				UpdateModPageDisplay();
				UpdatePageButtonInteractibility();
				UpdatePageCountText();
				if (onModPageChanged != null)
				{
					onModPageChanged.Invoke(newModPage);
				}
			}
		}

		public void Select()
		{
			EventSystem.current.SetSelectedGameObject(defaultSelection);
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
			return (m_requestFilter.isSortAscending ? "" : "-") + m_requestFilter.sortFieldName;
		}

		[Obsolete]
		public RequestFilter GenerateRequestFilter()
		{
			return m_requestFilter;
		}

		public void OnModSubscriptionsUpdated(IList<int> addedSubscriptions, IList<int> removedSubscriptions)
		{
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BitCode.Networking;
using DM;
using GamepadUI.StateManager.Core;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using LevelCreator;
using ModIO;
using ModIO.UI;
using Sirenix.Utilities;
using TFBGames;
using UIStateManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DMWorkshopHandler : UIComponentMainMenu
{
	private enum ShowcaseMode
	{
		Ranked = 0,
		Grid = 1,
		Horizontal = 2
	}

	public const string NoErrorMessageExtension = "_NO_ERROR_MESSAGE";

	public const bool IncludeModIOErrorMessage = true;

	private const string LocalizedSubscribeFailedText = "POPUP_SUBSCRIBE_FAILED";

	private const string LocalizedUnSubscribeFailedText = "POPUP_UNSUBSCRIBE_FAILED";

	private const string LocalizedDownloadFailedText = "POPUP_DOWNLOAD_FAILED";

	private const string LocalizedLoadErrorText = "POPUP_LOADERROR";

	private const string RefreshModMessage = "SUBSCRIBED_MODS_POPUP";

	private const string FetchingContentText = "POPUP_FETCH_NEW_CONTENT";

	private const float WaitForUserAuthDuration = 0.5f;

	public static object uploadData;

	[Header("Main Buttons")]
	[SerializeField]
	private UISubMenu m_mainButtonsContainer;

	[SerializeField]
	private GameObject[] m_monthsTopCreationsAuthors;

	[SerializeField]
	private Image m_shaderSlicedBackground;

	[Header("Discover Buttons")]
	[SerializeField]
	private UISubMenu m_buttonsContainer;

	[Header("Showcase")]
	[SerializeField]
	private UISubMenu m_showcaseGroup;

	[SerializeField]
	private UISubMenu m_loadingScreen;

	[SerializeField]
	private ModView m_modProfileItem;

	[SerializeField]
	private Transform m_modShowcaseContainer;

	[SerializeField]
	private Transform m_rankedShowcaseContainer;

	[SerializeField]
	private LocalizeText m_showcaseTitle;

	[SerializeField]
	private GameObject m_showcaseRankingNumbers;

	[Header("Browser")]
	[SerializeField]
	private InspectorView m_inspectorView;

	[SerializeField]
	private GameObject m_configurePanel;

	[SerializeField]
	private GameObject m_deletePanel;

	[SerializeField]
	private UISubMenu m_loginPanel;

	[SerializeField]
	private LocalizeText m_noResultsHeader;

	[SerializeField]
	private LocalizeText m_noResultsDescription;

	[SerializeField]
	private Toggle[] m_searchMethodButtons;

	[Header("Publishing")]
	[SerializeField]
	private UISubMenu m_localContentBrowser;

	[SerializeField]
	private UISubMenu m_publishPanel;

	[SerializeField]
	private DMUploadPanel m_uploadPanel;

	[SerializeField]
	private InterfaceStateManager m_stateManager;

	[SerializeField]
	protected UIComponentMainMenu m_mainButtons;

	[SerializeField]
	private FadeLerper m_fade;

	[SerializeField]
	private Button m_uploadButton;

	[SerializeField]
	private Button m_updateButton;

	[Header("Custom map data")]
	[SerializeField]
	private MapAsset m_levelMapAsset;

	private ShowcaseMode showcaseMode;

	private int modCount;

	private PlayerActions m_playerActions;

	private InputService m_inputService;

	private ModalPanel m_modalPanel;

	private IPlayerPrefsPlatform m_PlayerPrefs;

	private EventSystem m_currentEventSystem;

	private InternetDisconnectPopupController m_internetDisconnectPopup;

	private CustomContentLoaderModIO m_customContentLoader;

	private DMLoginDialog m_dmLoginDialog;

	private int userId = -1;

	private bool isXboxUserAuthorized;

	private bool m_databaseDirty;

	private const string ALLOW_UGC_KEY = "ALLOW_UGC";

	private FileIOWrapper m_fileIO;

	private int currentSearchMethod;

	private Toggle currentSearchMethodButton => m_searchMethodButtons[currentSearchMethod];

	private bool shouldRefresh
	{
		get
		{
			if (!m_databaseDirty && LocalUser.QueuedSubscribes.Count <= 0)
			{
				return LocalUser.QueuedUnsubscribes.Count > 0;
			}
			return true;
		}
	}

	private bool fromMainMenu => true;

	protected override void Awake()
	{
		base.Awake();
		m_playerActions = PlayerActions.Instance;
		m_inputService = ServiceLocator.GetService<InputService>();
		m_modalPanel = ServiceLocator.GetService<ModalPanel>();
		m_customContentLoader = ServiceLocator.GetService<CustomContentLoaderModIO>();
		m_PlayerPrefs = ServiceLocator.GetService<IPlayerPrefsPlatform>();
		m_currentEventSystem = EventSystem.current;
		m_internetDisconnectPopup = base.gameObject.AddComponent<InternetDisconnectPopupController>();
		m_fileIO = ServiceLocator.GetService<FileIOWrapper>();
		m_loginPanel.gameObject.SetActive(value: false);
		m_dmLoginDialog = m_loginPanel.GetComponentInChildren<DMLoginDialog>(includeInactive: true);
		if (!HasValidToken())
		{
			m_customContentLoader.TryLogin();
		}
		StartCoroutine(TokenWait());
		if (uploadData != null)
		{
			m_uploadButton.onClick.AddListener(delegate
			{
				m_uploadPanel.OpenPanel(uploadData, isUpload: true);
			});
			m_updateButton.onClick.AddListener(delegate
			{
				m_uploadPanel.OpenToUpdateWithSelectedLocalContent(uploadData);
			});
			StartCoroutine(Delay());
		}
		else
		{
			m_uploadButton.onClick.AddListener(delegate
			{
				m_uploadPanel.OpenToUpload();
			});
			m_updateButton.onClick.AddListener(delegate
			{
				m_uploadPanel.OpenToUpdate();
			});
		}
		IEnumerator Delay()
		{
			yield return null;
			m_stateManager.OpenUIComponent(this);
		}
		IEnumerator TokenWait()
		{
			yield return new WaitUntil(() => HasValidToken() && LocalUser.EnabledModIds.Count > 0);
			yield return new WaitForSecondsRealtime(5f);
			DMNewContentManager.RefreshNewContentIDs();
		}
	}

	protected override void Update()
	{
		base.Update();
		if ((bool)m_playerActions.m_refresh)
		{
			Refresh();
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		SetInternetDisconnectPopupState(enableAndSubscribe: false);
	}

	public void UpdatePlatformTag()
	{
		ExplorerView explorerView = ViewManager.instance.explorerView;
		SettingsInstance.Platform currentPlatform = GlobalSettingsHandler.CurrentPlatform;
		if (!(explorerView == null))
		{
			switch (currentPlatform)
			{
			case SettingsInstance.Platform.Desktop:
				explorerView.platformTag = "PC";
				break;
			case SettingsInstance.Platform.Switch:
				explorerView.platformTag = "SWITCH";
				break;
			case SettingsInstance.Platform.XboxOne:
			case SettingsInstance.Platform.XboxOneX:
			case SettingsInstance.Platform.XboxLockhart:
			case SettingsInstance.Platform.XboxAnaconda:
				explorerView.platformTag = "XBOX";
				break;
			case SettingsInstance.Platform.PS4:
			case SettingsInstance.Platform.PS4Neo:
				explorerView.platformTag = "PLAYSTATION_4";
				break;
			case SettingsInstance.Platform.PS5:
				explorerView.platformTag = "PLAYSTATION_5";
				break;
			default:
				explorerView.platformTag = "";
				break;
			}
		}
	}

	public void RandomizeDiscoveryCategories()
	{
		List<int> list = new List<int>();
		int num = m_buttonsContainer.transform.childCount - 2;
		for (int i = 0; i < 100; i++)
		{
			if (list.Count == num)
			{
				break;
			}
			int item = UnityEngine.Random.Range(0, num);
			if (list.Contains(item))
			{
				i--;
			}
			else
			{
				list.Add(item);
			}
		}
		for (int j = 0; j < num; j++)
		{
			m_buttonsContainer.transform.GetChild(j).gameObject.SetActive(value: false);
			for (int k = 0; k < list.Count; k++)
			{
				if (list[k] == j)
				{
					m_buttonsContainer.transform.GetChild(j).gameObject.SetActive(value: true);
					break;
				}
			}
		}
	}

	public void ShowcasePopular()
	{
		RequestFilter filter = new RequestFilter
		{
			sortFieldName = "popular",
			isSortAscending = false
		};
		APIPaginationParameters pagination = new APIPaginationParameters
		{
			limit = 6,
			offset = 0
		};
		showcaseMode = ShowcaseMode.Grid;
		GetMods(filter, pagination, filterByStats: false, delegate(ModProfile[] profiles)
		{
			RebuildShowcase(profiles, "Something Popular");
		});
	}

	public void ShowcaseFiveGoodPicks()
	{
		RequestFilter filter = new RequestFilter
		{
			sortFieldName = "ratings_positive",
			isSortAscending = false
		};
		APIPaginationParameters pagination = new APIPaginationParameters
		{
			limit = 5,
			offset = 0
		};
		showcaseMode = ShowcaseMode.Grid;
		GetMods(filter, pagination, filterByStats: true, delegate(ModProfile[] profiles)
		{
			RebuildShowcase(profiles, "Your \"Five Good Picks\", sire!");
		});
	}

	public void ShowcaseRandomMods()
	{
		RequestFilter filter = new RequestFilter
		{
			sortFieldName = "popular",
			isSortAscending = false
		};
		APIPaginationParameters pagination = new APIPaginationParameters
		{
			limit = 6,
			offset = UnityEngine.Random.Range(0, modCount)
		};
		showcaseMode = ShowcaseMode.Grid;
		GetMods(filter, pagination, filterByStats: false, delegate(ModProfile[] profiles)
		{
			RebuildShowcase(profiles, "Random");
		});
	}

	public void ShowcasePopularPopCulture()
	{
		RequestFilter requestFilter = new RequestFilter
		{
			sortFieldName = "popular",
			isSortAscending = false
		};
		MatchesArrayFilter<string> matchesArrayFilter = new MatchesArrayFilter<string>();
		matchesArrayFilter.filterArray = new string[1] { "Pop culture" };
		MatchesArrayFilter<string> filter = matchesArrayFilter;
		requestFilter.AddFieldFilter("tags", filter);
		APIPaginationParameters pagination = new APIPaginationParameters
		{
			limit = 6,
			offset = 0
		};
		showcaseMode = ShowcaseMode.Grid;
		GetMods(requestFilter, pagination, filterByStats: false, delegate(ModProfile[] profiles)
		{
			RebuildShowcase(profiles, "Popular Pop culture");
		});
	}

	public void ShowcaseThisMonthsTopCreations(bool onlyGenerateThumbnail)
	{
		RequestFilter filter = new RequestFilter
		{
			sortFieldName = "popular",
			isSortAscending = false
		};
		APIPaginationParameters aPIPaginationParameters = new APIPaginationParameters
		{
			limit = 3,
			offset = 0
		};
		if (!onlyGenerateThumbnail)
		{
			showcaseMode = ShowcaseMode.Ranked;
			GetMods(filter, aPIPaginationParameters, filterByStats: false, delegate(ModProfile[] profiles)
			{
				RebuildShowcase(profiles, "LABEL_DISCOVER_TITLE_TOPCREATIONS");
			});
			return;
		}
		ModProfileRequestManager.instance.FetchModProfilePage(filter, aPIPaginationParameters.offset, 3, ExplorerView.SearchMethod.All, delegate(RequestPage<ModProfile> page)
		{
			for (int i = 0; i < Mathf.Min(page.items.Length, 3); i++)
			{
				int index = i;
				ModManager.GetModLogo(page.items[index], LogoSize.Original, delegate(Texture2D tex)
				{
					Sprite sprite = UIUtilities.CreateSpriteFromTexture(tex);
					string text = "_Tex" + (index + 1);
					if (m_shaderSlicedBackground != null)
					{
						m_shaderSlicedBackground.materialForRendering.SetTexture(text, sprite.texture);
					}
				}, WebRequestError.LogAsWarning);
			}
		}, WebRequestError.LogAsWarning);
	}

	public void ShowcaseHistoricalCampaigns()
	{
		RequestFilter requestFilter = new RequestFilter
		{
			sortFieldName = "downloads",
			isSortAscending = true
		};
		InArrayFilter<string> filter = new InArrayFilter<string>(new string[2] { "Historical", "Campaign" });
		requestFilter.AddFieldFilter("tags", filter);
		APIPaginationParameters pagination = new APIPaginationParameters
		{
			limit = 6,
			offset = 0
		};
		showcaseMode = ShowcaseMode.Grid;
		GetMods(requestFilter, pagination, filterByStats: false, delegate(ModProfile[] profiles)
		{
			RebuildShowcase(profiles, "Historical Campaigns");
		});
	}

	public void ShowcaseHighlyRated()
	{
		RequestFilter requestFilter = new RequestFilter
		{
			sortFieldName = "ratings_positive",
			isSortAscending = false
		};
		MinimumFilter<int> filter = new MinimumFilter<int>(50);
		requestFilter.AddFieldFilter("subscribers_total", filter);
		APIPaginationParameters pagination = new APIPaginationParameters
		{
			limit = 6,
			offset = 0
		};
		showcaseMode = ShowcaseMode.Grid;
		GetMods(requestFilter, pagination, filterByStats: true, delegate(ModProfile[] profiles)
		{
			RebuildShowcase(profiles, "Highly Rated");
		});
	}

	public void ShowcaseBlastFromThePast()
	{
		RequestFilter requestFilter = new RequestFilter
		{
			sortFieldName = "downloads",
			isSortAscending = true
		};
		int num = 14;
		int num2 = 6;
		int now = ServerTimeStamp.Now;
		int num3 = now - 2592000 * num;
		int num4 = num3 % (86400 * num);
		MinimumFilter<int> filter = new MinimumFilter<int>(num3 - num4);
		requestFilter.AddFieldFilter("date_live", filter);
		int num5 = now - 2592000 * num2;
		num4 = num5 % (86400 * num2);
		MaximumFilter<int> filter2 = new MaximumFilter<int>(num5 - num4);
		requestFilter.AddFieldFilter("date_live", filter2);
		APIPaginationParameters pagination = new APIPaginationParameters
		{
			limit = 6,
			offset = 0
		};
		showcaseMode = ShowcaseMode.Grid;
		GetMods(requestFilter, pagination, filterByStats: false, delegate(ModProfile[] profiles)
		{
			RebuildShowcase(profiles, "Popular content from the past");
		});
	}

	public void ShowcaseUsersFavorites()
	{
		RequestFilter requestFilter = new RequestFilter
		{
			sortFieldName = "subscribers",
			isSortAscending = true
		};
		InArrayFilter<string> filter = new InArrayFilter<string>(new string[1] { "Battle" });
		InArrayFilter<string> filter2 = new InArrayFilter<string>(new string[1] { "Campaign" });
		requestFilter.AddFieldFilter("tags", filter);
		requestFilter.AddFieldFilter("tags", filter2);
		RequestFilter secondFilter = new RequestFilter
		{
			sortFieldName = "subscribers",
			isSortAscending = true
		};
		NotInArrayFilter<string> filter3 = new NotInArrayFilter<string>(new string[2] { "Battle", "Campaign" });
		secondFilter.AddFieldFilter("tags", filter3);
		showcaseMode = ShowcaseMode.Grid;
		GetMods(requestFilter, new APIPaginationParameters
		{
			limit = 2
		}, filterByStats: false, delegate(ModProfile[] bcProfiles)
		{
			GetMods(secondFilter, new APIPaginationParameters
			{
				limit = 4
			}, filterByStats: false, delegate(ModProfile[] profiles)
			{
				RebuildShowcase(bcProfiles.Concat(profiles).ToArray(), "User's Favorites!");
			});
		});
	}

	public void ShowcaseBiggestBaddestBattles()
	{
		RequestFilter requestFilter = new RequestFilter
		{
			sortFieldName = "rating",
			isSortAscending = true
		};
		InArrayFilter<string> filter = new InArrayFilter<string>(new string[2] { "Battle", "High budget" });
		requestFilter.AddFieldFilter("tags", filter);
		showcaseMode = ShowcaseMode.Grid;
		GetMods(requestFilter, new APIPaginationParameters
		{
			limit = 6
		}, filterByStats: false, delegate(ModProfile[] profiles)
		{
			RebuildShowcase(profiles, "Biggest Baddest Battles");
		});
	}

	private void PrepareShowcase()
	{
		if (!m_loadingScreen.IsOpen)
		{
			CloseSubMenu(fromMainMenu ? m_mainButtonsContainer : m_buttonsContainer);
			if (showcaseMode == ShowcaseMode.Ranked)
			{
				m_showcaseRankingNumbers.gameObject.SetActive(value: true);
			}
			m_showcaseTitle.gameObject.SetActive(value: true);
			m_showcaseTitle.LocaleID = "";
			m_loadingScreen.gameObject.SetActive(value: true);
			m_loadingScreen.Open();
		}
	}

	private void ShowcaseFailed(WebRequestError e)
	{
		m_modalPanel.PopUp("POPUP_LOADERROR", delegate
		{
			m_loadingScreen.Close();
			OpenSubMenu(fromMainMenu ? m_mainButtonsContainer : m_buttonsContainer);
			Debug.LogError(e.errorMessage);
		}, new string[1] { e.displayMessage });
	}

	private void GetMods(RequestFilter filter, APIPaginationParameters pagination, bool filterByStats, Action<ModProfile[]> onSuccess)
	{
		PrepareShowcase();
		ModManager.AddTagsToDisableCrossPlatformMods(filter);
		Debug.Log("FILTER: " + filter.GenerateFilterString());
		if (filterByStats)
		{
			APIClient.GetAllModStats(filter, pagination, delegate(RequestPage<ModStatistics> statsPage)
			{
				modCount = statsPage.resultTotal;
				List<int> statIds = new List<int>();
				statsPage.items.ForEach(delegate(ModStatistics x)
				{
					statIds.Add(x.modId);
				});
				ModManager.GetModProfiles(statIds, delegate(ModProfile[] profiles)
				{
					if (profiles != null && profiles.Length != 0)
					{
						onSuccess(profiles);
					}
					else
					{
						ShowcaseFailed(new WebRequestError
						{
							displayMessage = "No content could be located"
						});
					}
				}, ShowcaseFailed);
			}, ShowcaseFailed);
			return;
		}
		APIClient.GetAllMods(filter, pagination, delegate(RequestPage<ModProfile> page)
		{
			if (page != null && page.items != null && page.items.Length != 0)
			{
				modCount = page.resultTotal;
				onSuccess(page.items);
			}
			else
			{
				ShowcaseFailed(new WebRequestError
				{
					displayMessage = "No content could be located"
				});
			}
		}, ShowcaseFailed);
	}

	private void RebuildShowcase(ModProfile[] profiles, string title)
	{
		PrepareShowcase();
		if (profiles != null && profiles.Length != 0)
		{
			DestroyShowcase();
			BuildShowcase(profiles);
			m_loadingScreen.Close();
			m_showcaseTitle.LocaleID = title;
			OpenSubMenu(m_showcaseGroup);
		}
		else
		{
			ShowcaseFailed(new WebRequestError
			{
				displayMessage = "No content could be located"
			});
		}
	}

	public void DestroyShowcase()
	{
		foreach (Transform item in m_modShowcaseContainer.transform)
		{
			UnityEngine.Object.Destroy(item.gameObject);
		}
		foreach (Transform item2 in m_rankedShowcaseContainer.transform)
		{
			UnityEngine.Object.Destroy(item2.gameObject);
		}
	}

	private void BuildShowcase(ModProfile[] profiles)
	{
		if (profiles == null || profiles.Length == 0)
		{
			return;
		}
		for (int i = 0; i < profiles.Length; i++)
		{
			ModProfile modProfile = profiles[i];
			if (modProfile == null)
			{
				continue;
			}
			Transform modShowcaseContainer = m_modShowcaseContainer;
			switch (showcaseMode)
			{
			case ShowcaseMode.Ranked:
				modShowcaseContainer = m_rankedShowcaseContainer;
				break;
			case ShowcaseMode.Grid:
				modShowcaseContainer = m_modShowcaseContainer;
				break;
			case ShowcaseMode.Horizontal:
				modShowcaseContainer = m_modShowcaseContainer;
				break;
			default:
				modShowcaseContainer = m_modShowcaseContainer;
				break;
			}
			ModView modView = UnityEngine.Object.Instantiate(m_modProfileItem, modShowcaseContainer);
			modView.profile = modProfile;
			Button componentInChildren = modView.GetComponentInChildren<Button>();
			if (componentInChildren != null)
			{
				componentInChildren.onClick.AddListener(delegate
				{
					modView.InspectMod();
				});
			}
			ModManager.GetModStatistics(modProfile.id, delegate(ModStatistics stats)
			{
				modView.statistics = stats;
			}, WebRequestError.LogAsWarning);
			if (i == 0)
			{
				DMNavigationGroup componentInParent = m_modShowcaseContainer.GetComponentInParent<DMNavigationGroup>();
				if (componentInParent != null)
				{
					componentInParent.m_defaultSelection = modView.gameObject;
					componentInParent.m_lastSelection = null;
				}
			}
		}
	}

	public override void OpenSubMenu(UISubMenu menu)
	{
		base.OpenSubMenu(menu);
	}

	protected override void OnOpen()
	{
		base.OnOpen();
		UpdatePlatformTag();
		ShowcaseThisMonthsTopCreations(onlyGenerateThumbnail: true);
		SetInternetDisconnectPopupState(enableAndSubscribe: true);
		if (!m_customContentLoader.DidGivePermissionToLoadMods)
		{
			m_databaseDirty = true;
		}
		StartCoroutine(WaitForToken(delegate
		{
			if (currentSubMenu == m_loginPanel)
			{
				while (!OnBackPressed())
				{
				}
				ClearSubmenuStack();
			}
			if (uploadData == null)
			{
				UpdateExplorerView();
				OpenSubMenu(m_mainButtonsContainer);
				m_mainButtonsContainer.GetComponent<CodeAnimation>().FinishAnimation();
				currentSearchMethodButton.onValueChanged.Invoke(arg0: true);
			}
			else
			{
				OpenSubMenu(m_publishPanel);
				m_fade.fadeValue = 1f;
			}
		}));
	}

	protected override void OnClose()
	{
		base.OnClose();
		SetInternetDisconnectPopupState(enableAndSubscribe: false);
	}

	private IEnumerator WaitForToken(System.Action onValidToken)
	{
		if (!HasValidToken())
		{
			m_customContentLoader.TryLogin();
		}
		while (m_customContentLoader.IsBusyAuthenticatingUser)
		{
			yield return new WaitForSecondsRealtime(0.5f);
		}
		if (!HasValidToken())
		{
			m_dmLoginDialog.blackBackground.SetActive(uploadData != null);
			OpenSubMenu(m_loginPanel);
		}
		yield return new WaitUntil(() => HasValidToken() || !base.gameObject.activeSelf || currentSubMenu != m_loginPanel);
		if (HasValidToken())
		{
			StartCoroutine(WaitForInvalidToken(delegate
			{
				CloseWorkshop();
			}));
			onValidToken?.Invoke();
		}
		else
		{
			CloseWorkshop();
		}
	}

	private IEnumerator WaitForInvalidToken(System.Action onInvalidToken)
	{
		yield return new WaitUntil(() => !HasValidToken());
		onInvalidToken?.Invoke();
	}

	private bool HasValidToken()
	{
		return LocalUser.AuthenticationState == AuthenticationState.ValidToken;
	}

	private void RefreshDatabase(System.Action refreshDone)
	{
		if (!shouldRefresh)
		{
			refreshDone?.Invoke();
		}
		else
		{
			StartCoroutine(Delay());
		}
		IEnumerator Delay()
		{
			bool isBusy = true;
			bool didGivePermission = false;
			m_customContentLoader.CheckPermissionToLoadMods(refresh: false, delegate(bool didGivePermissionToLoadMods)
			{
				isBusy = false;
				didGivePermission = didGivePermissionToLoadMods;
			});
			while (isBusy)
			{
				yield return null;
			}
			if (!didGivePermission)
			{
				refreshDone?.Invoke();
			}
			else
			{
				m_modalPanel.WaitPopUpWithFocus("POPUP_FETCH_NEW_CONTENT", -1f, null, null, true);
				yield return new WaitUntil(() => LocalUser.QueuedSubscribes.Count == 0 && LocalUser.QueuedUnsubscribes.Count == 0);
				m_modalPanel.CloseWaitPopup();
				m_modalPanel.WaitPopUpWithFocus("POPUP_REFRESHING_CONTENT", -1f, null, null, true);
				yield return new WaitForSecondsRealtime(0.5f);
				m_customContentLoader.Refresh(delegate
				{
					m_modalPanel.CloseWaitPopup();
					m_databaseDirty = false;
					refreshDone?.Invoke();
				});
			}
		}
	}

	public void CloseWorkshop()
	{
		SetInternetDisconnectPopupState(enableAndSubscribe: false);
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		RefreshDatabase(delegate
		{
			m_showcaseTitle.gameObject.SetActive(value: false);
			m_loadingScreen.gameObject.SetActive(value: false);
			m_showcaseGroup.gameObject.SetActive(value: false);
			m_showcaseRankingNumbers.SetActive(value: false);
			UnitButtonBase.onClickOverride = null;
			CustomContentFactionButton.onClickOverride = null;
			CustomContentBattleButton.onClickOverride = null;
			CustomContentCampaignButton.onClickOverride = null;
			CustomContentLevelButton.onClickOverride = null;
			uploadData = null;
			if (currentSubMenu != null)
			{
				currentSubMenu.SetFocus(focus: false);
			}
			while (!OnBackPressed())
			{
				if (currentSubMenu != null)
				{
					currentSubMenu.SetFocus(focus: false);
				}
			}
			ClearSubmenuStack();
			StopAllCoroutines();
			if (m_stateManager != null && m_mainButtons != null)
			{
				m_stateManager.OpenUIComponent(m_mainButtons);
			}
			else
			{
				Debug.LogError("Unable to close Workshop UI component, missing main buttons or ISM reference");
			}
		});
	}

	protected override void OnSubMenuPressedBackButton(UISubMenu menu)
	{
		if (!m_inputService.IsTextInputCurrentlySelected())
		{
			Back();
			return;
		}
		if (m_currentEventSystem == null)
		{
			m_currentEventSystem = EventSystem.current;
		}
		GameObject currentSelectedGameObject = m_currentEventSystem.currentSelectedGameObject;
		if (currentSelectedGameObject == null)
		{
			return;
		}
		NavigableTMPTextInput componentInParent = currentSelectedGameObject.GetComponentInParent<NavigableTMPTextInput>();
		if (componentInParent != null)
		{
			componentInParent.DisableTextInput();
			return;
		}
		NavigableUGUITextInput componentInParent2 = currentSelectedGameObject.GetComponentInParent<NavigableUGUITextInput>();
		if (componentInParent2 != null)
		{
			componentInParent2.DisableTextInput();
		}
	}

	public void Refresh()
	{
		m_modalPanel.Choice(string.Empty, "SUBSCRIBED_MODS_POPUP", delegate
		{
			m_modalPanel.WaitPopUpWithFocus("POPUP_FETCH_NEW_CONTENT", -1f, null, null, true);
			DataStorage.DeleteDirectory(DataStorage.INSTALLATION_DIRECTORY, null);
			StartCoroutine(ModManager.DownloadAndUpdateMods_Coroutine(LocalUser.SubscribedModIds, delegate
			{
				m_databaseDirty = true;
				m_customContentLoader.Refresh(delegate
				{
					m_modalPanel.CloseWaitPopup();
					m_databaseDirty = false;
				});
			}));
		}, null);
	}

	public void Back(bool forceBack = false)
	{
		if (!m_modalPanel.IsPopupOpen)
		{
			if (uploadData != null && currentSubMenu == m_publishPanel)
			{
				uploadData = null;
				TABSSceneManager.LoadCustomContentPage();
			}
			else if (OnBackPressed())
			{
				CloseWorkshop();
			}
		}
	}

	public void Back(int popCount)
	{
		for (int i = 0; i < popCount; i++)
		{
			Back();
		}
	}

	protected override void OnReceivedInvitation(IGameInvitation invite)
	{
		CloseWorkshop();
	}

	public void OpenLocalContentBrowser(bool enableTabs, WorkshopContentType tabOnEnable, Action<object> onCellClickOverride)
	{
		UnitButtonBase.onClickOverride = onCellClickOverride;
		CustomContentFactionButton.onClickOverride = onCellClickOverride;
		CustomContentBattleButton.onClickOverride = onCellClickOverride;
		CustomContentCampaignButton.onClickOverride = onCellClickOverride;
		CustomContentLevelButton.onClickOverride = onCellClickOverride;
		int num = -1;
		switch (tabOnEnable)
		{
		case WorkshopContentType.Battle:
			num = 0;
			break;
		case WorkshopContentType.Campaign:
			num = 1;
			break;
		case WorkshopContentType.Unit:
			num = 2;
			break;
		case WorkshopContentType.Faction:
			num = 3;
			break;
		case WorkshopContentType.Map:
			num = 4;
			break;
		default:
			num = -1;
			break;
		}
		OpenSubMenu(m_localContentBrowser);
		UnitCreatorFactionBrowser component = m_localContentBrowser.GetComponent<UnitCreatorFactionBrowser>();
		component.Init(enableBackButtons: false, enableCreateButtons: false, enableTabs, num);
		component.QuickRefresh();
		component.FocusSelection();
	}

	public void PlayModFromBrowserInspector()
	{
		PlayMod(GetComponentInChildren<InspectorView>().modView.profile);
	}

	public void PlayMod(ModProfile modProfile)
	{
		RefreshDatabase(delegate
		{
			ContentDatabase contentDatabase = ContentDatabase.Instance();
			if (modProfile.tagNames.Contains("Campaign"))
			{
				foreach (TABSCampaignAsset item in contentDatabase.GetUserCampaignsByOnEnabled(onlyEnabled: false))
				{
					if (item != null && item.ModProfile != null && modProfile != null && item.ModProfile.id == modProfile.id)
					{
						CampaignPlayerDataHolder.StartedPlayingNewCampaign(item, 0);
						TABSSceneManager.LoadCampaign();
						return;
					}
				}
				PlayFailed();
			}
			else if (modProfile.tagNames.Contains("Battle"))
			{
				foreach (TABSCampaignLevelAsset item2 in contentDatabase.GetUserCampaignLevelsByOnEnabled(onlyEnabled: false))
				{
					if (item2 != null && item2.ModProfile != null && modProfile != null && item2.ModProfile.id == modProfile.id)
					{
						SpawnLevel.SetCustomMapToLoad(contentDatabase.GetUserMap(item2.CustomMap));
						CampaignPlayerDataHolder.StartedPlayingBattle(item2);
						TABSSceneManager.LoadCampaign();
						return;
					}
				}
				PlayFailed();
			}
			else
			{
				if (modProfile.tagNames.Contains("Map"))
				{
					foreach (CustomMap item3 in contentDatabase.GetUserMapsByOnEnabled(onlyEnabled: false))
					{
						if (item3 != null && item3.ModProfile != null && modProfile != null && item3.ModProfile.id == modProfile.id)
						{
							SpawnLevel.SetCustomMapToLoad(item3);
							ServiceLocator.GetService<GameModeService>().SetGameMode<SandboxGameMode>();
							CampaignPlayerDataHolder.StartedPlayingSandbox();
							TABSSceneManager.LoadMap(m_levelMapAsset);
						}
					}
					return;
				}
				PlayFailed();
			}
		});
		void PlayFailed()
		{
			m_modalPanel.PopUp("POPUP_SUBSCRIBE_FAILED", "Please resubscribe");
		}
	}

	private void UpdateInspector()
	{
		m_inspectorView.GetComponentInChildren<ModSubscribedDisplay>().Refresh();
	}

	public void SubscribeToInspectedMod()
	{
		LocalUser.EnabledModIds.Add(ViewManager.instance.inspectorView.modId);
		UpdateInspector();
		m_inspectorView.UpdatePlayButton(assertDownloadedFiles: false);
		APIClient.SubscribeToMod(m_inspectorView.modId, delegate(ModProfile profile)
		{
			ModManager.DownloadAndUpdateMod(m_inspectorView.modId, delegate
			{
				m_inspectorView.UpdatePlayButton();
				ViewManager.instance.explorerView.ClearCache();
				UpdateNoResultText((ExplorerView.SearchMethod)currentSearchMethod);
				DMNewContentManager.AddNewContentID(profile, isSavedToLocal: false);
				m_databaseDirty = true;
			}, OnError);
		}, OnError);
		void OnError(WebRequestError e)
		{
			LocalUser.EnabledModIds.Remove(ViewManager.instance.inspectorView.modId);
			UpdateInspector();
			m_inspectorView.UpdatePlayButton(assertDownloadedFiles: false);
			m_modalPanel.PopUp("POPUP_SUBSCRIBE_FAILED", Localizer.GetSinglePhrase(e.displayMessage));
		}
	}

	public void UnsubscribeCheck()
	{
		m_modalPanel.Choice("POPUP_UNINSTALL_TTILE", "POPUP_UNINSTALL_TEXT", delegate
		{
			UnsubscribeFromInspectedMod();
		}, null, "BUTTON_UNSUBSCRIBE", "BUTTON_CANCEL", true, m_inspectorView.profile.name);
	}

	public void UnsubscribeFromInspectedMod()
	{
		m_modalPanel.WaitPopUpWithFocus("POPUP_UNSUBSCRIBING", -1f, null, null, true);
		APIClient.UnsubscribeFromMod(m_inspectorView.modId, delegate
		{
			ModManager.UninstallMod(m_inspectorView.modId, delegate(bool success)
			{
				if (!success)
				{
					OnError(new WebRequestError
					{
						displayMessage = "Uninstallation failed"
					});
				}
				else
				{
					m_modalPanel.CloseWaitPopup();
					LocalUser.EnabledModIds.Remove(m_inspectorView.modId);
					ViewManager.instance.explorerView.ClearCache();
					UpdateNoResultText((ExplorerView.SearchMethod)currentSearchMethod);
					m_databaseDirty = true;
					UpdateInspector();
					m_inspectorView.UpdatePlayButton();
					ModProfileRequestManager.instance.RequestModProfile(m_inspectorView.modId, delegate(ModProfile profile)
					{
						DMNewContentManager.RemoveNewContentID(profile, isSavedToLocal: false);
					}, OnError);
				}
			});
		}, OnError);
		void OnError(WebRequestError e)
		{
			m_modalPanel.CloseWaitPopup();
			m_modalPanel.PopUp("POPUP_UNSUBSCRIBE_FAILED", Localizer.GetSinglePhrase(e.displayMessage));
		}
	}

	public void ReportInspectedMod()
	{
		ViewManager.instance.reportDialog.SetModId(m_inspectorView.modId);
	}

	public void WaitForReportSubmit()
	{
		m_modalPanel.WaitPopUpWithFocus("POPUP_SUBMITTINGREPORT", -1f, null, null, true);
		ViewManager.instance.reportDialog.SubmitReport(delegate
		{
			if (ViewManager.instance.reportDialog.blockUser)
			{
				DMWorkshopUtility.AddBlockedUser(m_inspectorView.modView.profile.submittedBy.id.ToString(), delegate
				{
					UpdateExplorerView();
					Proceed();
				});
			}
			else
			{
				Proceed();
			}
		}, delegate(WebRequestError e)
		{
			m_modalPanel.CloseWaitPopup();
			if (string.IsNullOrEmpty(e.errorMessage))
			{
				m_modalPanel.PopUp(e.displayMessage);
			}
			else
			{
				m_modalPanel.PopUp(e.displayMessage, "\n", e.errorMessage);
			}
		});
		void Proceed()
		{
			m_modalPanel.CloseWaitPopup();
			m_modalPanel.PopUp("POPUP_REPORTED", delegate
			{
				Back(2);
			});
		}
	}

	private void UpdateExplorerView()
	{
		ExplorerView explorerView = ViewManager.instance.explorerView;
		StartCoroutine(WaitUntilBrowserIsOpen());
		IEnumerator WaitUntilBrowserIsOpen()
		{
			CanvasGroup explorerViewCanvasGroup = explorerView.GetComponentInChildren<CanvasGroup>();
			yield return new WaitUntil(() => explorerViewCanvasGroup.interactable);
			explorerView.Refresh();
		}
	}

	public void ClearBlockedUsers()
	{
		DMWorkshopUtility.ClearBlockedUsers(delegate
		{
			UpdateExplorerView();
		});
	}

	public void DownloadInspectedModToLocal()
	{
		m_modalPanel.WaitPopUpWithFocus("POPUP_DOWNLOADING", -1f, null, null, true);
		StartCoroutine(DownloadAndCopyCoroutine());
		IEnumerator DownloadAndCopyCoroutine()
		{
			bool fetchComplete = false;
			bool downloadComplete = false;
			bool abort = false;
			bool skipOverwriteCheck = false;
			bool copyFilesCompleted = false;
			int modId = ViewManager.instance.inspectorView.modId;
			ModProfile profile = null;
			ModProfileRequestManager.instance.RequestModProfile(modId, delegate(ModProfile fetchedProfile)
			{
				profile = fetchedProfile;
				fetchComplete = true;
			}, delegate(WebRequestError e)
			{
				abort = true;
				m_modalPanel.CloseWaitPopup();
				m_modalPanel.PopUp("POPUP_DOWNLOAD_FAILED", Localizer.GetSinglePhrase(e.displayMessage));
				WebRequestError.LogAsWarning(e);
			});
			ModManager.DownloadAndUpdateMod(modId, delegate
			{
				downloadComplete = true;
			}, delegate(WebRequestError e)
			{
				abort = true;
				m_modalPanel.CloseWaitPopup();
				m_modalPanel.PopUp("POPUP_DOWNLOAD_FAILED", Localizer.GetSinglePhrase(e.displayMessage));
				WebRequestError.LogAsWarning(e);
			});
			yield return new WaitUntil(() => (fetchComplete && downloadComplete) || abort);
			if (!abort)
			{
				m_modalPanel.CloseWaitPopup();
				m_modalPanel.WaitPopUpWithFocus("POPUP_COPYING", -1f, null, null, true);
				DMNewContentManager.AddNewContentID(profile, isSavedToLocal: true);
				m_fileIO = ServiceLocator.GetService<FileIOWrapper>();
				Modfile currentBuild = profile.currentBuild;
				string modPath = ModManager.GetModInstallDirectory(modId, currentBuild.id);
				for (int i = 0; i < Enum.GetValues(typeof(WorkshopContentType)).Length; i++)
				{
					WorkshopContentType contentType = (WorkshopContentType)i;
					string contentDirPath = Path.Combine(modPath, contentType.ToString());
					bool contentDirPathCompleted = false;
					bool contentDirPathExists = false;
					m_fileIO.DirectoryExists(contentDirPath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
					{
						contentDirPathExists = exists;
						contentDirPathCompleted = true;
					});
					yield return new WaitUntil(() => contentDirPathCompleted);
					if (contentDirPathExists)
					{
						string localContentDir = CustomContentFilePaths.GetCustomContentFilePath(contentType);
						if (!string.IsNullOrEmpty(localContentDir))
						{
							bool getSubFoldersCompleted = false;
							string[] contentTypeSubFolders = null;
							m_fileIO.GetDirectories(contentDirPath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(string[] subFolders, Exception contentTypeSubFoldersException)
							{
								contentTypeSubFolders = subFolders;
								getSubFoldersCompleted = true;
							});
							yield return new WaitUntil(() => getSubFoldersCompleted);
							if (contentTypeSubFolders != null)
							{
								string[] array = contentTypeSubFolders;
								foreach (string path in array)
								{
									bool getContentTypeFilesCompleted = false;
									string[] contentTypeFiles = null;
									m_fileIO.GetFiles(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(string[] files, Exception filesException)
									{
										contentTypeFiles = files;
										getContentTypeFilesCompleted = true;
									});
									yield return new WaitUntil(() => getContentTypeFilesCompleted);
									string dir;
									if (contentTypeFiles != null && contentTypeFiles.Length != 0)
									{
										string path2 = string.Empty;
										string[] array2 = contentTypeFiles;
										foreach (string path3 in array2)
										{
											string extension = Path.GetExtension(path3);
											string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path3);
											if (!(extension == ".png") && !(fileNameWithoutExtension.ToLower() == "icon"))
											{
												path2 = Path.GetFileNameWithoutExtension(path3);
												break;
											}
										}
										dir = Path.Combine(localContentDir, path2);
										bool contentDirExistCompleted = false;
										bool contentDirExist = false;
										m_fileIO.DirectoryExists(dir, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
										{
											contentDirExist = exists;
											contentDirExistCompleted = true;
										});
										yield return new WaitUntil(() => contentDirExistCompleted);
										if (!contentDirExist)
										{
											bool contentDirCreatedCompleted = false;
											m_fileIO.CreateDirectory(dir, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate
											{
												contentDirCreatedCompleted = true;
											});
											yield return new WaitUntil(() => contentDirCreatedCompleted);
											CopyContentFiles(contentTypeFiles);
										}
										else if (!skipOverwriteCheck)
										{
											m_modalPanel.CloseWaitPopup();
											bool hold = true;
											m_modalPanel.Choice("POPUP_DOWNLOADLOCAL_OVERWRITE_TITLE", "POPUP_DOWNLOADLOCAL_OVERWRITE_TEXT", delegate
											{
												skipOverwriteCheck = true;
												CopyContentFiles(contentTypeFiles);
												hold = false;
											}, delegate
											{
												DMNewContentManager.RemoveNewContentID(profile, isSavedToLocal: true);
												hold = false;
												abort = true;
											}, new string[1] { "\n" });
											yield return new WaitUntil(() => !hold);
											if (abort)
											{
												yield break;
											}
										}
										else
										{
											CopyContentFiles(contentTypeFiles);
										}
									}
									void CopyContentFiles(string[] downloadedFiles)
									{
										if (contentType == WorkshopContentType.Campaign)
										{
											string logoPath = Path.Combine(dir, "Picture.png");
											ImageRequestManager.instance.RequestModLogo(modId, profile.logoLocator, LogoSize.Original, delegate(Texture2D logo)
											{
												byte[] bytes = logo.EncodeToPNG();
												m_fileIO.WriteAllBytes(logoPath, bytes, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(Exception e)
												{
													if (e != null)
													{
														Debug.LogError(e.Message);
													}
												});
											}, delegate(Texture2D fallbackLogo)
											{
												byte[] bytes = fallbackLogo.EncodeToPNG();
												m_fileIO.WriteAllBytes(logoPath, bytes, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(Exception e)
												{
													if (e != null)
													{
														Debug.LogError(e.Message);
													}
												});
											}, WebRequestError.LogAsWarning);
										}
										int numberOfFilesToCopy = downloadedFiles.Length;
										int filesCopied = 0;
										foreach (string text in downloadedFiles)
										{
											string fileName = Path.GetFileName(text);
											string destinationPath = Path.Combine(dir, fileName);
											m_fileIO.CopyFile(text, destinationPath, overwrite: true, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(Exception e)
											{
												filesCopied++;
												if (filesCopied >= numberOfFilesToCopy)
												{
													copyFilesCompleted = true;
												}
												if (e != null)
												{
													Debug.LogError(e.Message);
												}
											});
										}
									}
								}
							}
						}
					}
				}
				yield return new WaitUntil(() => copyFilesCompleted);
				bool attemptToDeletedModCompleted = false;
				DeleteDownloadedMod(modPath, profile, delegate(Exception exception)
				{
					if (exception != null)
					{
						Debug.LogError($"Unable to delete downloaded mod from resources: {exception}");
					}
					attemptToDeletedModCompleted = true;
				});
				yield return new WaitUntil(() => attemptToDeletedModCompleted);
				m_modalPanel.CloseWaitPopup();
				m_modalPanel.PopUp("POPUP_DOWNLOADLOCAL_SUCCESS", delegate
				{
					m_databaseDirty = true;
				}, profile.name, "\n");
			}
		}
	}

	public void RefreshSearchMethod()
	{
		SetSearchMethod(currentSearchMethod);
	}

	public void SetSearchMethod(int searchMethod)
	{
		ModContainer.overrideItemAction = null;
		string[] tagFilter = ViewManager.instance.explorerView.GetTagFilter();
		string defaultTab = "Battle";
		string[] array = tagFilter;
		foreach (string text in array)
		{
			if (Enum.TryParse<WorkshopContentType>(text, out var result) && result != WorkshopContentType.Any)
			{
				defaultTab = text;
				break;
			}
		}
		UpdateSearchMethod(searchMethod, enableTabs: true, defaultTab, enableSearchMethodButtons: true, preserveSearchMethod: true);
	}

	public void SetSearchMethod(int searchMethod, Action<ModProfile> overrideItemAction, bool enableTabs, string defaultTab = "Battle")
	{
		ModContainer.overrideItemAction = overrideItemAction;
		UpdateSearchMethod(searchMethod, enableTabs, defaultTab, enableSearchMethodButtons: false, preserveSearchMethod: false);
	}

	private void DeleteDownloadedMod(string path, ModProfile modProfile, Action<Exception> callback)
	{
		if (LocalUser.EnabledModIds.Contains(modProfile.id))
		{
			callback?.Invoke(null);
			return;
		}
		m_fileIO.DeleteDirectory(path, recursive: true, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(Exception doneCallback)
		{
			callback?.Invoke(doneCallback);
		});
	}

	private void UpdateSearchMethod(int searchMethod, bool enableTabs, string defaultTab, bool enableSearchMethodButtons, bool preserveSearchMethod)
	{
		StartCoroutine(Delay());
		IEnumerator Delay()
		{
			yield return null;
			if (preserveSearchMethod)
			{
				currentSearchMethod = searchMethod;
			}
			ExplorerView explorerView = ViewManager.instance.explorerView;
			explorerView.tabs.SetActive(enableTabs);
			explorerView.updateTitle.SetActive(!enableTabs);
			explorerView.searchModeButtons.SetActive(enableSearchMethodButtons);
			explorerView.SetSearchMethod(searchMethod);
			UpdateNoResultText((ExplorerView.SearchMethod)searchMethod);
			MatchesArrayFilter<string> matchesArrayFilter = new MatchesArrayFilter<string>(explorerView.GetTagFilter());
			List<string> list = new List<string>(matchesArrayFilter.filterArray);
			list.Remove(explorerView.defaultTab);
			explorerView.defaultTab = defaultTab;
			list.Add(explorerView.defaultTab);
			matchesArrayFilter.filterArray = list.ToArray();
			explorerView.SetFieldFilters("tags", matchesArrayFilter);
		}
	}

	private void UpdateNoResultText(ExplorerView.SearchMethod searchMethod)
	{
		EnableNoResultTexts(enable: false);
		Action<WebRequestError> onError = delegate
		{
			m_noResultsHeader.LocaleID = "LABEL_NORESULT_BROWSE_HEADER";
			m_noResultsHeader.gameObject.SetActive(value: true);
		};
		ModProfileRequestManager.instance.FetchModProfilePage(new RequestFilter(), 0, 1, ExplorerView.SearchMethod.All, delegate(RequestPage<ModProfile> creations)
		{
			ModProfileRequestManager.instance.FetchModProfilePage(new RequestFilter(), 0, 1, ExplorerView.SearchMethod.All, delegate(RequestPage<ModProfile> subscriptions)
			{
				bool flag = subscriptions != null && subscriptions.items?.Length > 0;
				bool flag2 = false;
				ModProfile[] array = creations?.items;
				foreach (ModProfile modProfile in array)
				{
					if (modProfile != null && modProfile.status == ModStatus.Accepted)
					{
						flag2 = true;
						break;
					}
				}
				switch (searchMethod)
				{
				case ExplorerView.SearchMethod.All:
					m_noResultsHeader.LocaleID = "LABEL_NORESULT_BROWSE_HEADER";
					m_noResultsDescription.LocaleID = "LABEL_NORESULT_BROWSE_DESC";
					break;
				case ExplorerView.SearchMethod.Subscriptions:
					m_noResultsHeader.LocaleID = "LABEL_NORESULT_SUBS_HEADER";
					if (flag)
					{
						m_noResultsDescription.LocaleID = "LABEL_NORESULT_SUBS_FILTER";
					}
					else
					{
						m_noResultsDescription.LocaleID = "LABEL_NORESULT_SUBS_NOFILES";
					}
					break;
				case ExplorerView.SearchMethod.Creations:
					m_noResultsHeader.LocaleID = "LABEL_NORESULT_UPLOAD_HEADER";
					if (flag2)
					{
						m_noResultsDescription.LocaleID = "LABEL_NORESULT_UPLOAD_FILTER";
					}
					else
					{
						m_noResultsDescription.LocaleID = "LABEL_NORESULT_UPLOAD_NOFILES";
					}
					break;
				}
				EnableNoResultTexts(enable: true);
			}, onError);
		}, onError);
		void EnableNoResultTexts(bool enable)
		{
			m_noResultsHeader.gameObject.SetActive(enable);
			m_noResultsDescription.gameObject.SetActive(enable);
		}
	}

	private void SetInternetDisconnectPopupState(bool enableAndSubscribe)
	{
		if (!(m_internetDisconnectPopup == null))
		{
			m_internetDisconnectPopup.InternetDisconnectedPopupClosed -= OnInternetDisconnectedPopupClosed;
			if (enableAndSubscribe)
			{
				m_internetDisconnectPopup.InternetDisconnectedPopupClosed += OnInternetDisconnectedPopupClosed;
				m_internetDisconnectPopup.Enable();
			}
			else
			{
				m_internetDisconnectPopup.Disable();
			}
		}
	}

	private void OnInternetDisconnectedPopupClosed()
	{
		if (uploadData != null)
		{
			uploadData = null;
			TABSSceneManager.LoadCustomContentPage();
		}
		else
		{
			CloseWorkshop();
		}
	}
}

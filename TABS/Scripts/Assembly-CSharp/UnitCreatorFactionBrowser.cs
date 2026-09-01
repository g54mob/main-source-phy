using System.Linq;
using InControl;
using Landfall.TABS;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using LevelCreator;
using Sirenix.Utilities;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitCreatorFactionBrowser : MonoBehaviour
{
	[Header("Browser Properties")]
	public CustomContentSideBar customContentSideBar;

	public TMP_InputField searchBar;

	public CustomContentGridBrowser[] customContentBrowsers;

	private CustomContentGridBrowser currentBrowser;

	public static int selectedTab = -1;

	public static bool showDownloaded;

	public GameObject[] backButtons;

	public GameObject[] createButtons;

	public GameObject[] createGlyphs;

	public GameObject toggleLocalDownloadedGlyph;

	public GameObject tabs;

	public TMP_Text header;

	public GameObject[] newContentGraphics;

	public Toggle localToggle;

	public Toggle downloadedToggle;

	public CustomContetnManager contentManager;

	public DMCampaignCreator campaignCreator;

	public CustomContentPopup customPopup;

	public FactionCreatorManager factionCreator;

	[SerializeField]
	private CustomContentFactionBrowser factionBrowser;

	private PermissionsHelper permissionsHelper;

	private CanvasGroup canvasGroup;

	private ModalPanel modalPanel;

	private CustomContentLoaderModIO customContentLoaderModIO;

	private CodeAnimation factionCreatorCodeAnimation;

	private bool isMonitoringContentRefreshing;

	private bool browserInFocus
	{
		get
		{
			if (campaignCreator != null && !campaignCreator.isOpen && factionCreator != null && !factionCreator.isOpen && (canvasGroup.interactable || customContentSideBar.isOpen) && !modalPanel.IsPopupOpen)
			{
				return !customPopup.isOpen;
			}
			return false;
		}
	}

	private bool sideBarInFocus
	{
		get
		{
			if (customContentSideBar.isOpen)
			{
				return browserInFocus;
			}
			return false;
		}
	}

	private bool isSideBarLoadingIconVisible
	{
		get
		{
			if (sideBarInFocus)
			{
				return customContentSideBar.isLoadingIconVisible;
			}
			return false;
		}
	}

	public void Init(bool enableBackButtons = true, bool enableCreateButtons = true, bool enableTabs = true, int tabOnEnable = -1)
	{
		backButtons.ForEach(delegate(GameObject x)
		{
			x.SetActive(enableBackButtons);
		});
		createButtons.ForEach(delegate(GameObject x)
		{
			x.SetActive(enableCreateButtons);
		});
		createGlyphs.ForEach(delegate(GameObject x)
		{
			x.SetActive(enableCreateButtons);
		});
		toggleLocalDownloadedGlyph.SetActive(enableCreateButtons && permissionsHelper.CanViewDownloadTabs);
		SetDownloaded(enableCreateButtons);
		tabs.SetActive(enableTabs);
		header.gameObject.SetActive(!enableTabs);
		CustomContentGridBrowser[] array = customContentBrowsers;
		foreach (CustomContentGridBrowser customContentGridBrowser in array)
		{
			if (customContentGridBrowser.gameObject.activeSelf)
			{
				customContentGridBrowser.Deselect();
			}
		}
		if (tabOnEnable > -1)
		{
			selectedTab = -1;
			SelectTab(tabOnEnable);
		}
		else
		{
			SelectFirstTab();
		}
	}

	private void Start()
	{
		permissionsHelper = ServiceLocator.GetService<PermissionsHelper>();
		canvasGroup = GetComponent<CanvasGroup>();
		modalPanel = ServiceLocator.GetService<ModalPanel>();
		customContentLoaderModIO = ServiceLocator.GetService<CustomContentLoaderModIO>();
		if (factionCreator != null)
		{
			factionCreatorCodeAnimation = factionCreator.GetComponentInParent<CodeAnimation>();
		}
		PlayerActions.Instance.OnLastInputTypeChanged += OnInputChanged;
		QuickRefresh(WorkshopContentType.Unit);
		if (selectedTab == -1 || selectedTab == 0)
		{
			SelectFirstTab();
		}
		else
		{
			Transform child = tabs.transform.GetChild(1);
			child.GetChild(selectedTab * 2).GetComponent<Toggle>().OnPointerClick(new PointerEventData(EventSystem.current));
			child.GetComponent<DMInvokeCyclic>()?.SetIndex(selectedTab);
		}
		EnableLocalDownloadedTabs(showDownloaded && permissionsHelper.CanViewDownloadTabs);
		downloadedToggle.gameObject.SetActive(permissionsHelper.CanViewDownloadTabs);
		if (CustomContetnManager.returnToMapCreator)
		{
			tabs.SetActive(value: false);
		}
		TrySubscribeToFactionCreatorEvents(subscribe: true);
	}

	private void OnDestroy()
	{
		PlayerActions.Instance.OnLastInputTypeChanged -= OnInputChanged;
		TrySubscribeToFactionCreatorEvents(subscribe: false);
	}

	private void TrySubscribeToFactionCreatorEvents(bool subscribe)
	{
		if (!(factionCreator == null))
		{
			factionCreator.SavingFactionStarted -= OnSavingFactionStarted;
			factionCreator.SavingFactionCompleted -= OnSavingFactionCompleted;
			if (subscribe)
			{
				factionCreator.SavingFactionStarted += OnSavingFactionStarted;
				factionCreator.SavingFactionCompleted += OnSavingFactionCompleted;
			}
		}
	}

	private void OnInputChanged(BindingSourceType obj)
	{
		FocusSelection();
	}

	private void OnSavingFactionStarted()
	{
		isMonitoringContentRefreshing = true;
	}

	private void OnSavingFactionCompleted()
	{
		isMonitoringContentRefreshing = false;
	}

	private void SelectFirstTab()
	{
		SelectTab(0);
		selectedTab = -1;
	}

	public void OnFactionCreatorClosed()
	{
		if (contentManager != null && factionBrowser != null && factionBrowser.CustomFactionsCount <= 0 && customContentLoaderModIO != null && customContentLoaderModIO.IsRefreshingOrWaitingToRefresh())
		{
			contentManager.UpdateLoadingScreenState(CustomContentPageLoadingRefreshIcon.LoadingIconState.Loading);
		}
	}

	public void SelectTab(int tab)
	{
		selectedTab = tab;
		searchBar.text = string.Empty;
		if (currentBrowser == null)
		{
			currentBrowser = customContentBrowsers[tab];
		}
		if (currentBrowser != null)
		{
			currentBrowser.Deselect();
			CustomContentGridBrowser customContentGridBrowser = customContentBrowsers[selectedTab];
			currentBrowser = customContentGridBrowser;
			currentBrowser.Select();
		}
		RefreshCurrentBrowser();
	}

	public void RefreshCurrentBrowser()
	{
		if (currentBrowser != null)
		{
			currentBrowser.Refresh();
		}
	}

	public void ShowUnit(UnitBlueprint unit)
	{
		if (browserInFocus)
		{
			customContentSideBar.ShowUnit(unit);
		}
	}

	public void ShowFaction(Faction faction)
	{
		if (browserInFocus)
		{
			customContentSideBar.ShowFaction(faction);
		}
	}

	public void ShowBattle(TABSCampaignLevelAsset battle)
	{
		if (browserInFocus)
		{
			customContentSideBar.ShowBattle(battle);
		}
	}

	public void ShowCampaign(TABSCampaignAsset campaign)
	{
		if (browserInFocus)
		{
			customContentSideBar.ShowCampaign(campaign);
		}
	}

	public void ShowLevel(CustomMap customMap)
	{
		if (browserInFocus)
		{
			customContentSideBar.ShowLevel(customMap);
		}
	}

	public void QuickRefresh()
	{
		CustomContentGridBrowser[] array = customContentBrowsers;
		foreach (CustomContentGridBrowser obj in array)
		{
			obj.instantClear = true;
			obj.Populate();
		}
	}

	public void QuickRefresh(WorkshopContentType contentType)
	{
		CustomContentGridBrowser customContentGridBrowser = null;
		switch (contentType)
		{
		case WorkshopContentType.Unit:
			customContentGridBrowser = customContentBrowsers.OfType<CustomContentUnitBrowser>().ToList()[0];
			break;
		case WorkshopContentType.Layout:
		case WorkshopContentType.Battle:
			customContentGridBrowser = customContentBrowsers.OfType<CustomContentBattleBrowser>().ToList()[0];
			break;
		case WorkshopContentType.Campaign:
			customContentGridBrowser = customContentBrowsers.OfType<CustomContentCampaignBrowser>().ToList()[0];
			break;
		case WorkshopContentType.Faction:
			customContentGridBrowser = customContentBrowsers.OfType<CustomContentFactionBrowser>().ToList()[0];
			break;
		case WorkshopContentType.Map:
			customContentGridBrowser = customContentBrowsers.OfType<CustomContentLevelBrowser>().ToList()[0];
			break;
		default:
			QuickRefresh();
			break;
		}
		if (!(customContentGridBrowser == null))
		{
			customContentGridBrowser.instantClear = true;
			customContentGridBrowser.Populate(customContentGridBrowser.CurrentPage, customContentGridBrowser.CurrentPage % 2);
			customContentGridBrowser.UpdatePageButtons();
			customContentGridBrowser.UpdateChildAlignment();
		}
	}

	public void IncreasePageCurrentBrowser(int delta)
	{
		if (currentBrowser != null)
		{
			currentBrowser.IncreasePage(delta);
		}
	}

	public void FocusSelection()
	{
		if (currentBrowser != null)
		{
			currentBrowser.SelectCurrentLayoutFirstElement();
		}
	}

	public void SetDownloaded(bool enabled)
	{
		showDownloaded = enabled;
		if (currentBrowser != null)
		{
			currentBrowser.Refresh();
		}
	}

	private void EnableLocalDownloadedTabs(bool enabled)
	{
		if (enabled)
		{
			downloadedToggle.OnSubmit(null);
			localToggle.OnDeselect(null);
		}
		else
		{
			localToggle.OnSubmit(null);
			downloadedToggle.OnDeselect(null);
		}
		if (currentBrowser != null)
		{
			currentBrowser.SelectCurrentLayoutFirstElement();
		}
	}

	private void ToggleLocalDownloadedTabs()
	{
		EnableLocalDownloadedTabs(localToggle.isOn);
	}

	private void Update()
	{
		PlayerActions instance = PlayerActions.Instance;
		if (sideBarInFocus)
		{
			if (instance.m_editUnitName.WasPressed && !isSideBarLoadingIconVisible)
			{
				customContentSideBar.Rename();
			}
			else if (instance.m_upload.WasPressed && !isSideBarLoadingIconVisible)
			{
				customContentSideBar.Upload();
			}
			else if (instance.m_enterExitBattle.WasPressed && !isSideBarLoadingIconVisible)
			{
				customContentSideBar.Play();
			}
			else if (instance.m_accept.WasPressed && !isSideBarLoadingIconVisible)
			{
				customContentSideBar.Edit();
			}
			else if (instance.m_deleteContent.WasPressed && !isSideBarLoadingIconVisible)
			{
				customContentSideBar.DeleteUnit();
			}
			else if (instance.m_back.WasPressed)
			{
				customContentSideBar.CloseFactionPreview();
			}
		}
		else if (browserInFocus)
		{
			if (instance.m_newContent.WasPressed)
			{
				if (selectedTab < 0)
				{
					selectedTab = 0;
				}
				if (contentManager != null)
				{
					contentManager.UpdateLoadingScreenState(CustomContentPageLoadingRefreshIcon.LoadingIconState.HaveContent);
				}
				createButtons[selectedTab].GetComponent<Button>().onClick.Invoke();
			}
			else if (instance.m_back.WasPressed)
			{
				if (contentManager != null)
				{
					contentManager.GoToMainMenu();
				}
			}
			else if (instance.m_toggleDownloadTab.WasPressed)
			{
				ToggleLocalDownloadedTabs();
			}
		}
		else if (browserInFocus && !sideBarInFocus)
		{
			if (instance.m_back.WasPressed)
			{
				if (customContentSideBar.isOpen)
				{
					customContentSideBar.CloseFactionPreview();
				}
				else if (contentManager != null)
				{
					contentManager.GoToMainMenu();
				}
			}
			else if (instance.m_newContent.WasPressed)
			{
				if (selectedTab < 0)
				{
					selectedTab = 0;
				}
				if (contentManager != null)
				{
					contentManager.UpdateLoadingScreenState(CustomContentPageLoadingRefreshIcon.LoadingIconState.HaveContent);
				}
				createButtons[selectedTab].GetComponent<Button>().onClick.Invoke();
			}
		}
		UpdateMonitoringContentRefresh();
	}

	private void OnEnable()
	{
		DMNewContentManager.onIdAdded.AddListener(UpdateNewContentGraphic);
		DMNewContentManager.onIdRemoved.AddListener(UpdateNewContentGraphic);
		if (newContentGraphics == null || newContentGraphics.Length == 0)
		{
			return;
		}
		DMNewContentManager.HasNewContentOfType(WorkshopContentType.Battle, delegate(bool hasNewContent)
		{
			if (newContentGraphics != null && newContentGraphics[0] != null)
			{
				newContentGraphics[0].SetActive(hasNewContent);
			}
		});
		DMNewContentManager.HasNewContentOfType(WorkshopContentType.Campaign, delegate(bool hasNewContent)
		{
			if (newContentGraphics != null && newContentGraphics[1] != null)
			{
				newContentGraphics[1].SetActive(hasNewContent);
			}
		});
		DMNewContentManager.HasNewContentOfType(WorkshopContentType.Unit, delegate(bool hasNewContent)
		{
			if (newContentGraphics != null && newContentGraphics[2] != null)
			{
				newContentGraphics[2].SetActive(hasNewContent);
			}
		});
		DMNewContentManager.HasNewContentOfType(WorkshopContentType.Faction, delegate(bool hasNewContent)
		{
			if (newContentGraphics != null && newContentGraphics[3] != null)
			{
				newContentGraphics[3].SetActive(hasNewContent);
			}
		});
		DMNewContentManager.HasNewContentOfType(WorkshopContentType.Map, delegate(bool hasNewContent)
		{
			if (newContentGraphics != null && newContentGraphics[4] != null)
			{
				newContentGraphics[4].SetActive(hasNewContent);
			}
		});
	}

	private void OnDisable()
	{
		DMNewContentManager.onIdAdded.RemoveListener(UpdateNewContentGraphic);
		DMNewContentManager.onIdRemoved.RemoveListener(UpdateNewContentGraphic);
	}

	public void UpdateNewContentGraphic(DMNewContentManager.NewContentID newId, WorkshopContentType contentType)
	{
		int i = Mathf.Max(0, selectedTab);
		if (newContentGraphics != null && newContentGraphics[i] != null)
		{
			DMNewContentManager.HasNewContentOfType(contentType, delegate(bool hasNewContent)
			{
				newContentGraphics[i].SetActive(hasNewContent);
			});
		}
	}

	private void UpdateMonitoringContentRefresh()
	{
		bool flag = factionCreatorCodeAnimation != null && !factionCreatorCodeAnimation.isPlaying && factionCreatorCodeAnimation.currentState == CodeAnimationInstance.AnimationUse.Out;
		if (isMonitoringContentRefreshing && flag && !(contentManager == null) && !(customContentLoaderModIO == null) && !(factionBrowser == null) && customContentLoaderModIO.IsRefreshingOrWaitingToRefresh())
		{
			isMonitoringContentRefreshing = false;
			factionBrowser.Refresh();
		}
	}
}

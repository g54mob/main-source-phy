using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DM;
using Landfall.TABS;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using TFBGames;
using TMPro;
using UIStateManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class DMCampaignCreator : MonoBehaviour
{
	private const float InputFocusDelayTime = 0.3f;

	[SerializeField]
	private InterfaceStateManager interfaceManager;

	[SerializeField]
	private CustomContentCampaignBrowser campaignBrowser;

	[SerializeField]
	private GameObject m_CampaignScreenShotMakerObject;

	[SerializeField]
	private GameObject[] m_onScreenButtons;

	[SerializeField]
	private GameObject[] m_buttonPrompts;

	[Header("Design UI")]
	[SerializeField]
	private GameObject m_designUIRootObject;

	[SerializeField]
	private LocalizeText m_HeaderText;

	[SerializeField]
	private Transform m_LibraryGrid;

	[SerializeField]
	private GameObject m_ContentCell;

	[SerializeField]
	private GameObject m_BattleCell;

	[SerializeField]
	private GameObject m_reorderControllerPrompt;

	[SerializeField]
	private Transform m_ProgressionContent;

	[SerializeField]
	private GameObject m_ProgressionIndexTemplate;

	[SerializeField]
	private GameObject m_ProgressionWinTemplate;

	[Header("Save UI")]
	[SerializeField]
	private GameObject m_saveUIRootObject;

	[SerializeField]
	private Transform m_battleImageContainer;

	[SerializeField]
	private GameObject m_battleImageTemplate;

	[SerializeField]
	private TMP_Text m_battleImageCount;

	[SerializeField]
	private Button m_CampaignNameButton;

	[SerializeField]
	private TMP_Text m_CampaignNameText;

	[SerializeField]
	private LocalizeText m_CampaignNameTextLocalizer;

	[SerializeField]
	private Button m_CampaignDescriptionButton;

	[SerializeField]
	private TMP_Text m_CampaignDescriptionText;

	[SerializeField]
	private LocalizeText m_CampaignDescriptionTextLocalizer;

	[SerializeField]
	private Button m_CampaignThankYouTitleButton;

	[SerializeField]
	private TMP_Text m_CampaignThankYouTitleText;

	[SerializeField]
	private LocalizeText m_CampaignThankYouTitleTextLocalizer;

	[SerializeField]
	private Button m_CampaignThankYouDescButton;

	[SerializeField]
	private TMP_Text m_CampaignThankYouDescText;

	[SerializeField]
	private LocalizeText m_CampaignThankYouDescTextLocalizer;

	[SerializeField]
	private NavigableTMPTextInput m_searchField;

	public bool isOpen;

	private string m_campaignName;

	private string m_campaignDescription;

	private string m_campaignThankYouTitle;

	private string m_campaignThankYouDescription;

	private CampaignCreatorUIMode m_UIMode;

	private List<Selectable> m_libraryGridButtons;

	private List<Selectable> m_selectedMapsGridButtons;

	private InputService m_inputService;

	private FileIOWrapper m_FileIO;

	private BattleCreatorCampaignScreenshotMaker m_CampaignScreenshotMaker;

	private TABSCampaignAsset m_AlreadySelectedCampaign;

	private List<TABSCampaignLevelAsset> m_SelectedMapsForCampaign;

	private List<Sprite> m_SelectedMapsForCampaignSprites;

	private List<CampaignCreatorBattleCell> m_previousCampaigns = new List<CampaignCreatorBattleCell>();

	private List<CampaignCreatorBattleCell> m_campaignList = new List<CampaignCreatorBattleCell>();

	private GameObjectPool<CampaignCreatorBattleCell> m_battlePool;

	private List<CampaignCreatorBattleCell> currentBattleCells = new List<CampaignCreatorBattleCell>();

	private List<CampaignCreatorBattleCell> filteredItems = new List<CampaignCreatorBattleCell>();

	private Action m_OnSaveAction;

	private bool m_IsSaving;

	private ModalPanel m_ModalPanel;

	private int m_SavingModalPanelOpenId;

	private bool m_IsWaitingForSavingPopupToCloseInBack;

	private bool isWaitingToFocusInput;

	private float elapsedTimeWaitingForFocusInput;

	private PlayerActions m_playerActions;

	private ISystemKeyboard keyboard;

	private string filter = "";

	private static char[] split = new char[1] { ' ' };

	private string localisedCampaignNameText;

	private string localisedCampaignDescriptionText;

	private string localisedCampaignThankYouTitleText;

	private string localisedCampaignThankYouDescText;

	private bool saveActionPressed;

	private void Awake()
	{
		m_libraryGridButtons = new List<Selectable>();
		m_selectedMapsGridButtons = new List<Selectable>();
		m_playerActions = PlayerActions.Instance;
		Init();
	}

	private void OnEnable()
	{
		StoreLocalizedPlaceHolderText();
	}

	public void Init()
	{
		m_inputService = ServiceLocator.GetService<InputService>();
		m_ModalPanel = ServiceLocator.GetService<ModalPanel>();
		InitReferences();
		InitListeners();
		CreateExplicitNavigation();
	}

	private void StoreLocalizedPlaceHolderText()
	{
		localisedCampaignNameText = m_CampaignNameText.text;
		localisedCampaignDescriptionText = m_CampaignDescriptionText.text;
		localisedCampaignThankYouTitleText = m_CampaignThankYouTitleText.text;
		localisedCampaignThankYouDescText = m_CampaignThankYouDescText.text;
	}

	private void InitReferences()
	{
		m_SelectedMapsForCampaign = new List<TABSCampaignLevelAsset>();
		m_campaignList = new List<CampaignCreatorBattleCell>();
		m_battlePool = new GameObjectPool<CampaignCreatorBattleCell>(m_BattleCell, deactivateOnRelease: true, m_LibraryGrid);
		m_FileIO = ServiceLocator.GetService<FileIOWrapper>();
		keyboard = ServiceLocator.GetService<SystemKeyboardProvider>().Keyboard;
	}

	private void InitListeners()
	{
		ModalPanel modalPanel = ServiceLocator.GetService<ModalPanel>();
		m_CampaignNameButton.onClick.AddListener(delegate
		{
			modalPanel.Inputfield(new ModalPanel.InputFieldParameters
			{
				header = "POPUP_ENTERNAME_TITLE",
				onFinish = OnCampaignNameChanged,
				yesButton = "POPUP_ENTERNAME_YES",
				startInput = m_campaignName
			});
		});
		m_CampaignDescriptionButton.onClick.AddListener(delegate
		{
			modalPanel.Inputfield(new ModalPanel.InputFieldParameters
			{
				header = "POPUP_ENTERDESCRIPTION_TITLE",
				onFinish = OnCampaignDescriptionChanged,
				yesButton = "POPUP_ENTERDESCRIPTION_YES",
				startInput = m_campaignDescription,
				isMultiline = true
			});
		});
		m_CampaignThankYouTitleButton.onClick.AddListener(delegate
		{
			modalPanel.Inputfield(new ModalPanel.InputFieldParameters
			{
				header = "POPUP_ENTERTHANKYOU_TITLE",
				onFinish = OnThankyouTitleChanged,
				yesButton = "POPUP_ENTERTHANKYOU_YES",
				startInput = m_campaignThankYouTitle
			});
		});
		m_CampaignThankYouDescButton.onClick.AddListener(delegate
		{
			modalPanel.Inputfield(new ModalPanel.InputFieldParameters
			{
				header = "POPUP_ENTERTHANKYOUNOTE_TITLE",
				onFinish = OnThankyouDescriptionChanged,
				yesButton = "POPUP_ENTERTHANKYOUNOTE_YES",
				startInput = m_campaignThankYouDescription,
				isMultiline = true
			});
		});
	}

	private void OnCampaignNameChanged(string name)
	{
		m_campaignName = name;
		m_CampaignNameText.text = name;
		m_CampaignNameTextLocalizer.enabled = string.IsNullOrEmpty(name);
	}

	private void OnCampaignDescriptionChanged(string desc)
	{
		m_campaignDescription = desc;
		m_CampaignDescriptionText.text = desc;
		m_CampaignDescriptionTextLocalizer.enabled = string.IsNullOrEmpty(desc);
	}

	private void OnThankyouTitleChanged(string title)
	{
		m_campaignThankYouTitle = title;
		m_CampaignThankYouTitleText.text = title;
		m_CampaignThankYouTitleTextLocalizer.enabled = string.IsNullOrEmpty(title);
	}

	private void OnThankyouDescriptionChanged(string desc)
	{
		m_campaignThankYouDescription = desc;
		m_CampaignThankYouDescText.text = desc;
		m_CampaignThankYouDescTextLocalizer.enabled = string.IsNullOrEmpty(desc);
	}

	public void OnSave()
	{
		m_OnSaveAction?.Invoke();
	}

	private void SaveCampaign()
	{
		if (m_IsSaving)
		{
			return;
		}
		m_IsSaving = true;
		ShowSavingPopup();
		MakeCampaignSequence(default(DatabaseID), delegate(CampaignSequence sequence)
		{
			_ = CustomContentFilePaths.FilePathCampaign + sequence.CampaignName;
			TABSCampaignAsset existingAsset = null;
			ContentDatabase contentDatabase = ContentDatabase.Instance();
			UnityAction saveLocalAction = delegate
			{
				Debug.LogFormat("Save Local Action ID: {0}     alreadySelectedCampaign: {1}", sequence.ID, m_AlreadySelectedCampaign != null);
				if (m_AlreadySelectedCampaign != null)
				{
					contentDatabase.RemoveUserCampaign(m_AlreadySelectedCampaign.Entity.GUID);
					m_FileIO.DeleteDirectory(m_AlreadySelectedCampaign.FolderPath, recursive: true, FileHandlingFileType.CustomContentOrLocalStorageFile, null);
				}
				m_CampaignScreenShotMakerObject.SetActive(value: true);
				m_CampaignScreenshotMaker = m_CampaignScreenShotMakerObject.GetComponent<BattleCreatorCampaignScreenshotMaker>();
				m_CampaignScreenshotMaker.PopulateImages(TABSCampaignAsset.DeserializeCampaign(sequence, contentDatabase.GetCampaignLevel), delegate
				{
					HasPopulatedImages();
				});
			};
			TABSCampaignAsset existingCampaign = contentDatabase.GetUserLocalCampaignByExactName(m_campaignName);
			if (existingCampaign != null)
			{
				ServiceLocator.GetService<ModalPanel>().Choice("POPUP_SAVEANDREPLACE_TITLE", "POPUP_OVERWRITECAMPAIGN_TEXT", delegate
				{
					ShowSavingPopup();
					existingAsset = existingCampaign;
					sequence.ID = existingAsset.Entity.GUID;
					saveLocalAction();
				}, delegate
				{
					m_IsSaving = false;
					interfaceManager.OpenUIComponent(GetComponentInParent<UIComponentMainMenu>());
				}, new string[1] { sequence.CampaignName });
			}
			else
			{
				saveLocalAction();
			}
			void HasPopulatedImages()
			{
				CampaignHandler.SaveCampaign(sequence, existingAsset, delegate(bool success)
				{
					if (success)
					{
						if (existingAsset != null)
						{
							existingAsset.Entity.InvalidateSprite();
						}
						ServiceLocator.GetService<CustomContentLoaderModIO>().QuickRefresh(WorkshopContentType.Campaign, RefreshCampaignBrowser);
					}
					else
					{
						HandleSavingDone();
					}
				});
			}
		});
		void RefreshCampaignBrowser()
		{
			HandleSavingDone();
			UnitCreatorFactionBrowser componentInParent = campaignBrowser.GetComponentInParent<UnitCreatorFactionBrowser>();
			componentInParent.QuickRefresh(WorkshopContentType.Campaign);
			componentInParent.FocusSelection();
		}
	}

	private void ShowSavingPopup()
	{
		m_SavingModalPanelOpenId = m_ModalPanel.WaitPopUpWithFocus("POPUP_SAVING", -1f, null, null, true);
	}

	private async Task WaitForFrames(int frames)
	{
		for (int i = 0; i < frames; i++)
		{
			await Task.Yield();
		}
	}

	private async void HideSavingPopup()
	{
		await WaitForFrames(5);
		if (m_SavingModalPanelOpenId == m_ModalPanel.OpenId)
		{
			m_ModalPanel.CloseWaitPopup(restorePreviouslySelectedObject: false);
		}
	}

	private void HandleSavingDone()
	{
		m_IsSaving = false;
		HideSavingPopup();
		BackWithPopUpCheck(waitForSavingPopupToClose: true);
	}

	private bool ValidateCampaign()
	{
		MakeCampaignSequenceList();
		ModalPanel service = ServiceLocator.GetService<ModalPanel>();
		if (service != null)
		{
			if (m_SelectedMapsForCampaign.Count <= 0)
			{
				service.PopUp("POPUP_NOMAPS", SelectCampaignNameInput);
				return false;
			}
			if (!ValidateCampaignNameText())
			{
				service.PopUp("POPUP_NOCAMPAIGNNAME", SelectCampaignNameInput);
				return false;
			}
			if (!ValidateCampaignDescriptionText())
			{
				service.PopUp("POPUP_NOCAMPAIGNDESCRIPTION", SelectCampaignNameInput);
				return false;
			}
		}
		return true;
	}

	private void SelectCampaignNameInput()
	{
		if (m_CampaignNameButton != null)
		{
			m_CampaignNameButton.Select();
		}
	}

	private void MakeCampaignSequence(DatabaseID useThis = default(DatabaseID), Action<CampaignSequence> doneCallback = null)
	{
		CampaignSequence sequence = new CampaignSequence();
		sequence.CampaignName = m_campaignName;
		sequence.CampaignDescription = m_campaignDescription;
		sequence.CampaignThankYouTitle = m_campaignThankYouTitle;
		sequence.CampaignThankYouText = m_campaignThankYouDescription;
		sequence.ID = ((useThis == default(DatabaseID)) ? DatabaseID.NewID() : useThis);
		List<CampaignLevelReference> levels = new List<CampaignLevelReference>();
		int count = m_SelectedMapsForCampaign.Count;
		AsyncCounter asyncCounter = new AsyncCounter(count);
		for (int i = 0; i < count; i++)
		{
			AsyncCounter tempCounter = asyncCounter;
			int levelIndex = i;
			CampaignHandler.GetLoadedLayoutFromDisk(m_SelectedMapsForCampaign[levelIndex].FilePath, delegate(CampaignLevel lvl)
			{
				if (lvl == null)
				{
					if (tempCounter.OnAsyncDone())
					{
						sequence.Levels = levels.ToArray();
						doneCallback?.Invoke(sequence);
					}
				}
				else
				{
					CampaignLevelReference currLevel = new CampaignLevelReference(lvl, levelIndex);
					Debug.Log("Saving Campaign Sequence: Level: " + currLevel.LevelName + " Index: " + levelIndex);
					if (currLevel != null)
					{
						levels.Add(currLevel);
					}
					if (tempCounter.OnAsyncDone())
					{
						sequence.Levels = levels.ToArray();
						doneCallback?.Invoke(sequence);
					}
				}
			});
		}
	}

	private void MakeCampaignSequenceList()
	{
		m_SelectedMapsForCampaign = new List<TABSCampaignLevelAsset>();
		m_SelectedMapsForCampaignSprites = new List<Sprite>();
		for (int i = 0; i < m_campaignList.Count; i++)
		{
			CampaignCreatorBattleCell campaignCreatorBattleCell = m_campaignList[i];
			if (campaignCreatorBattleCell != null)
			{
				TABSCampaignLevelAsset levelAsset = campaignCreatorBattleCell.LevelAsset;
				if (levelAsset != null)
				{
					m_SelectedMapsForCampaign.Add(levelAsset);
					m_SelectedMapsForCampaignSprites.Add(campaignCreatorBattleCell.mapImage.sprite);
				}
			}
		}
	}

	private bool ValidateCampaignNameText()
	{
		return !string.IsNullOrWhiteSpace(m_campaignName);
	}

	private bool ValidateCampaignDescriptionText()
	{
		return !string.IsNullOrWhiteSpace(m_campaignDescription);
	}

	private void Populate()
	{
		m_battlePool.ReleaseAll();
		m_libraryGridButtons.Clear();
		PopulateBattles(delegate
		{
			if (m_AlreadySelectedCampaign != null)
			{
				for (int i = 0; i < m_AlreadySelectedCampaign.LevelsInCampaign.Length; i++)
				{
					SpawnLevelCellAlreadySelected(m_AlreadySelectedCampaign.LevelsInCampaign[i], i);
				}
			}
			SelectBattleInBattleChooser();
		});
	}

	private void PopulateBattles(Action doneCallback)
	{
		TABSCampaignLevelAsset[] array = ContentDatabase.Instance().GetUserCampaignLevels().ToArray();
		int num = array.Length;
		if (num <= 0)
		{
			doneCallback?.Invoke();
			return;
		}
		AsyncCounter asyncCounter = new AsyncCounter(num);
		for (int i = 0; i < num; i++)
		{
			AsyncCounter tempCounter = asyncCounter;
			TABSCampaignLevelAsset item = array[i];
			m_FileIO.FileExists(item.FilePath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
			{
				if (!exists)
				{
					if (tempCounter.OnAsyncDone())
					{
						doneCallback?.Invoke();
					}
				}
				else
				{
					if (new FileInfo(item.FilePath).Extension == CustomContentFilePaths.FileEndingBattle)
					{
						if (m_AlreadySelectedCampaign != null)
						{
							bool flag = false;
							TABSCampaignLevelAsset[] levelsInCampaign = m_AlreadySelectedCampaign.LevelsInCampaign;
							foreach (TABSCampaignLevelAsset tABSCampaignLevelAsset in levelsInCampaign)
							{
								if (tABSCampaignLevelAsset != null && item.Entity.GUID == tABSCampaignLevelAsset.Entity.GUID)
								{
									flag = true;
								}
							}
							if (!flag && !item.IsModIOLevel)
							{
								SpawnLevelCell(item);
							}
						}
						else if (!item.IsModIOLevel)
						{
							SpawnLevelCell(item);
						}
					}
					if (tempCounter.OnAsyncDone())
					{
						doneCallback?.Invoke();
					}
				}
			});
		}
	}

	private void SpawnLevelCellAlreadySelected(TABSCampaignLevelAsset level, int index)
	{
		if (!(level == null))
		{
			GameObject obj = UnityEngine.Object.Instantiate(m_ContentCell, m_LibraryGrid, worldPositionStays: false);
			obj.FetchComponent<BattleCreatorAssetUICell>();
			CampaignCreatorBattleCell component = obj.GetComponent<CampaignCreatorBattleCell>();
			component.Init(m_LibraryGrid, null, m_campaignList);
			component.Init(new BattleCreatorAssetUICellBase.CampaignLevelData(level.Entity.Name, level, null, null, null, null, null, ContentTypeFilter.Battles, BattleCreatorState.CampaignCreator));
			component.MovedToOtherList += UpdateBattleGridNavigation;
			component.EditedList += BattleCellEditedList;
			component.AddToList();
			obj.transform.localScale = Vector3.one;
			obj.SetActive(value: true);
		}
	}

	private void SpawnLevelCell(TABSCampaignLevelAsset level)
	{
		if (!(level == null))
		{
			CampaignCreatorBattleCell campaignCreatorBattleCell = m_battlePool.GetObject();
			if (campaignCreatorBattleCell != null)
			{
				campaignCreatorBattleCell.Init(m_LibraryGrid, null, m_campaignList);
				campaignCreatorBattleCell.Init(new BattleCreatorAssetUICellBase.CampaignLevelData(level.Entity.Name, level, null, null, null, null, null, ContentTypeFilter.Battles, BattleCreatorState.CampaignCreator));
				campaignCreatorBattleCell.MovedToOtherList += UpdateBattleGridNavigation;
				campaignCreatorBattleCell.EditedList += BattleCellEditedList;
				currentBattleCells.Add(campaignCreatorBattleCell);
				campaignCreatorBattleCell.gameObject.transform.localScale = Vector3.one;
				campaignCreatorBattleCell.gameObject.SetActive(value: true);
			}
		}
	}

	private void BattleCellEditedList()
	{
		BuildCampaignProgression();
	}

	private void BuildCampaignProgression()
	{
		foreach (Transform item in m_ProgressionContent)
		{
			UnityEngine.Object.Destroy(item.gameObject);
		}
		for (int i = 0; i < m_campaignList.Count; i++)
		{
			UnityEngine.Object.Instantiate(m_ProgressionIndexTemplate, m_ProgressionContent).GetComponentInChildren<TMP_Text>().text = (i + 1).ToString();
		}
		if (m_campaignList.Count > 0)
		{
			UnityEngine.Object.Instantiate(m_ProgressionWinTemplate, m_ProgressionContent);
		}
		GridLayoutGroup component = m_ProgressionContent.GetComponent<GridLayoutGroup>();
		Vector2 vector = new Vector2(75f, 75f);
		component.cellSize = vector / Mathf.Clamp((float)component.transform.childCount * 0.035f, 1f, 2f);
	}

	private void CheckBattleCreatorState()
	{
		m_CampaignNameButton.gameObject.SetActive(value: true);
		if (m_AlreadySelectedCampaign != null)
		{
			OnCampaignNameChanged(m_AlreadySelectedCampaign.Entity.Name);
			OnCampaignDescriptionChanged(m_AlreadySelectedCampaign.CampaignInfo.Description);
			OnThankyouTitleChanged(m_AlreadySelectedCampaign.CampaignInfo.ThankYouTitle);
			OnThankyouDescriptionChanged(m_AlreadySelectedCampaign.CampaignInfo.ThankYouText);
		}
		m_OnSaveAction = delegate
		{
			if (ValidateCampaign())
			{
				SaveCampaign();
			}
			else
			{
				interfaceManager.OpenUIComponent(GetComponentInParent<UIComponentMainMenu>());
			}
		};
	}

	private void GenerateBattlePreviewImages()
	{
		foreach (Transform item in m_battleImageContainer)
		{
			UnityEngine.Object.Destroy(item.gameObject);
		}
		MakeCampaignSequenceList();
		int num = 0;
		for (int i = 0; i < m_SelectedMapsForCampaignSprites.Count; i++)
		{
			if (i < 5)
			{
				UnityEngine.Object.Instantiate(m_battleImageTemplate, m_battleImageContainer).transform.GetChild(0).GetComponent<Image>().sprite = m_SelectedMapsForCampaignSprites[i];
			}
			else
			{
				num++;
			}
		}
		m_battleImageCount.text = ((num > 0) ? ("+" + num) : "");
	}

	public void OpenWithData(object data)
	{
		if (data != null)
		{
			m_AlreadySelectedCampaign = (TABSCampaignAsset)data;
		}
		Open();
		GetComponentInParent<UIComponentMainMenu>().Open();
	}

	public void OpenWithoutData()
	{
		m_AlreadySelectedCampaign = null;
		Open();
	}

	private void Open()
	{
		isOpen = true;
		m_inputService.OnUIOpen();
		base.gameObject.SetActive(value: true);
		ResetTexts();
		m_campaignList.Clear();
		ChangeUIControllerState(CampaignCreatorUIMode.Saving);
		BuildCampaignProgression();
		CheckBattleCreatorState();
		Populate();
		if (campaignBrowser != null && campaignBrowser.customContentManager != null)
		{
			campaignBrowser.customContentManager.UpdateLoadingScreenState(CustomContentPageLoadingRefreshIcon.LoadingIconState.HaveContent);
		}
		InputService service = ServiceLocator.GetService<InputService>();
		if (service != null)
		{
			service.InputChanged += OnInputSourceChanged;
		}
		if (keyboard != null)
		{
			keyboard.InputCompleted += OnKeyboardInputCompleted;
		}
		OnInputSourceChanged(PlayerActions.Instance.InputType);
		OpenSaveCampaign(revertCampaignSelection: false);
		StartDelayForInputFocus();
	}

	private void StartDelayForInputFocus()
	{
		if (!isWaitingToFocusInput)
		{
			isWaitingToFocusInput = true;
			elapsedTimeWaitingForFocusInput = 0f;
		}
	}

	private void StopInputFocusDelay()
	{
		isWaitingToFocusInput = false;
	}

	public void Back()
	{
		BackWithPopUpCheck();
	}

	private async void BackWithPopUpCheck(bool waitForSavingPopupToClose = false)
	{
		if (m_IsWaitingForSavingPopupToCloseInBack)
		{
			return;
		}
		if (waitForSavingPopupToClose && m_ModalPanel != null && m_ModalPanel.IsPopupOpen && m_ModalPanel.OpenId == m_SavingModalPanelOpenId)
		{
			m_IsWaitingForSavingPopupToCloseInBack = true;
			while (m_ModalPanel != null && m_ModalPanel.IsPopupOpen && m_ModalPanel.OpenId == m_SavingModalPanelOpenId)
			{
				await Task.Delay(200);
			}
			m_IsWaitingForSavingPopupToCloseInBack = false;
		}
		if (!(m_ModalPanel != null) || !m_ModalPanel.IsPopupOpen)
		{
			if (m_UIMode == CampaignCreatorUIMode.Designing)
			{
				OpenSaveCampaign(revertCampaignSelection: true);
			}
			else
			{
				Close();
			}
		}
	}

	public void Close()
	{
		interfaceManager.OpenUIComponent(campaignBrowser.GetComponentInParent<UIComponentMainMenu>());
		campaignBrowser.GetComponentInParent<UnitCreatorFactionBrowser>().customContentSideBar.CloseFactionPreview();
		InputService service = ServiceLocator.GetService<InputService>();
		if (service != null)
		{
			service.InputChanged -= OnInputSourceChanged;
			m_inputService.OnUIClose();
		}
		if (keyboard != null)
		{
			keyboard.InputCompleted -= OnKeyboardInputCompleted;
		}
		m_designUIRootObject.SetActive(value: false);
		m_saveUIRootObject.SetActive(value: false);
		campaignBrowser.Refresh();
		campaignBrowser.SelectCurrentLayoutFirstElement();
		isOpen = false;
		if (isWaitingToFocusInput)
		{
			StopInputFocusDelay();
		}
	}

	public void OpenDesignCampaign()
	{
		ChangeUIControllerState(CampaignCreatorUIMode.Designing);
		m_searchField.text = string.Empty;
		m_designUIRootObject.SetActive(value: true);
		m_saveUIRootObject.SetActive(value: false);
		m_HeaderText.LocaleID = string.Empty;
		SelectBattleInBattleChooser();
	}

	public void OpenSaveCampaign(bool revertCampaignSelection)
	{
		m_designUIRootObject.SetActive(value: false);
		m_saveUIRootObject.SetActive(value: true);
		SelectCampaignNameInput();
		ChangeUIControllerState(CampaignCreatorUIMode.Saving);
		if (revertCampaignSelection && m_libraryGridButtons != null)
		{
			m_campaignList = new List<CampaignCreatorBattleCell>(m_previousCampaigns);
			foreach (CampaignCreatorBattleCell usedObject in m_battlePool.UsedObjects)
			{
				usedObject.campaignList = m_campaignList;
				usedObject.UpdateIndex();
			}
			BuildCampaignProgression();
		}
		else
		{
			m_previousCampaigns = new List<CampaignCreatorBattleCell>(m_campaignList);
		}
		GenerateBattlePreviewImages();
		if (m_AlreadySelectedCampaign == null)
		{
			m_HeaderText.LocaleID = "LABEL_CAMPAIGNCREATOR_TITLE";
			return;
		}
		m_HeaderText.Args = new string[1] { m_AlreadySelectedCampaign.Entity.Name };
		m_HeaderText.LocaleID = "LABEL_EDIT_TITLE";
	}

	private void Update()
	{
		if (isWaitingToFocusInput)
		{
			if (elapsedTimeWaitingForFocusInput >= 0.3f)
			{
				SelectCampaignNameInput();
				StopInputFocusDelay();
			}
			elapsedTimeWaitingForFocusInput += Time.unscaledDeltaTime;
		}
		if (isOpen && m_UIMode == CampaignCreatorUIMode.Designing && m_searchField != null && m_playerActions.m_itemSelectSearch.WasPressed)
		{
			if (m_searchField.IsTextInputEnabled)
			{
				m_searchField.DisableTextInput();
				ApplyFilterOnSubmit();
			}
			else
			{
				m_searchField.EnableTextInput();
			}
		}
	}

	private void ApplyFilterOnSubmit()
	{
		ApplyFilter();
		SelectBattleInBattleChooser();
	}

	public void EnterFilter(string filter)
	{
		this.filter = filter;
		ApplyFilter();
	}

	private void OnKeyboardInputCompleted(string searchString)
	{
		if (m_UIMode == CampaignCreatorUIMode.Designing)
		{
			EnterFilter(searchString);
			ApplyFilterOnSubmit();
		}
	}

	public void ApplyFilter()
	{
		filteredItems.Clear();
		filteredItems.AddRange(m_battlePool.UsedObjects);
		filteredItems.ApplyFilter(filter);
		if (PlayerActions.Instance.InputType == InputType.Controller)
		{
			SelectBattleInBattleChooser();
		}
	}

	private void LateUpdate()
	{
		NavigateUIWithController(PlayerActions.Instance);
	}

	public void NavigateUIWithController(PlayerActions playerActions)
	{
		bool flag = m_ModalPanel != null && m_ModalPanel.IsPopupOpen;
		if (!isOpen || saveActionPressed || flag)
		{
			saveActionPressed = false;
			return;
		}
		switch (m_UIMode)
		{
		case CampaignCreatorUIMode.Designing:
		{
			if (playerActions.m_saveCustomContent.WasPressed)
			{
				saveActionPressed = true;
				OpenSaveCampaign(revertCampaignSelection: false);
			}
			if (playerActions.m_back.WasPressed)
			{
				OpenSaveCampaign(revertCampaignSelection: true);
			}
			if (!playerActions.m_accept.WasPressed)
			{
				break;
			}
			GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
			if (currentSelectedGameObject != null)
			{
				CampaignCreatorBattleCell component = currentSelectedGameObject.GetComponent<CampaignCreatorBattleCell>();
				if (component != null)
				{
					component.ToggleFromList();
				}
			}
			break;
		}
		case CampaignCreatorUIMode.Saving:
			if (playerActions.m_saveCustomContent.WasPressed)
			{
				saveActionPressed = true;
				OnSave();
			}
			if (playerActions.m_back.WasPressed && !m_inputService.IsTextInputCurrentlySelected())
			{
				Back();
			}
			break;
		default:
			throw new ArgumentOutOfRangeException();
		}
	}

	public void ChangeUIControllerState(CampaignCreatorUIMode mode)
	{
		m_UIMode = mode;
	}

	private void OnInputSourceChanged(InputType type)
	{
		if (type == InputType.Controller)
		{
			switch (m_UIMode)
			{
			case CampaignCreatorUIMode.Designing:
				SelectBattleInBattleChooser();
				break;
			case CampaignCreatorUIMode.Saving:
				SelectCampaignNameInput();
				break;
			default:
				throw new ArgumentOutOfRangeException("m_UIMode", m_UIMode, null);
			}
		}
		GameObject[] buttonPrompts = m_buttonPrompts;
		for (int i = 0; i < buttonPrompts.Length; i++)
		{
			buttonPrompts[i].SetActive(type == InputType.Controller);
		}
		buttonPrompts = m_onScreenButtons;
		for (int i = 0; i < buttonPrompts.Length; i++)
		{
			buttonPrompts[i].SetActive(type == InputType.Keyboard);
		}
	}

	private void SelectBattleInBattleChooser()
	{
		if (m_UIMode == CampaignCreatorUIMode.Designing)
		{
			UpdateBattleGridNavigation();
			if (m_libraryGridButtons.Count > 0 && m_libraryGridButtons[0] != null)
			{
				m_libraryGridButtons[0].Select();
			}
		}
	}

	private void CreateExplicitNavigation()
	{
		UIHelpers.CreateExplicitLinearNavigation(m_saveUIRootObject.GetComponentsInChildren<Selectable>(), horizontal: false);
	}

	public void UpdateBattleGridNavigation()
	{
		m_libraryGridButtons.Clear();
		m_selectedMapsGridButtons.Clear();
		GetComponentsInChildrenExclusively(m_LibraryGrid, ref m_libraryGridButtons);
		int max = m_libraryGridButtons.Count - 1;
		int num = 4;
		for (int i = 0; i < m_libraryGridButtons.Count; i++)
		{
			int value = i - num;
			int value2 = i + num;
			int value3 = (((i - 1) % num == num - 1) ? (i + num - 1) : (i - 1));
			int value4 = (((i + 1) % num == 0) ? (i - (num - 1)) : (i + 1));
			value = Mathf.Clamp(value, 0, max);
			value2 = Mathf.Clamp(value2, 0, max);
			value3 = Mathf.Clamp(value3, 0, max);
			value4 = Mathf.Clamp(value4, 0, max);
			Selectable selectable = m_libraryGridButtons[i];
			Selectable selectOnUp = m_libraryGridButtons[value];
			Selectable selectOnDown = m_libraryGridButtons[value2];
			Selectable selectOnLeft = m_libraryGridButtons[value3];
			Selectable selectOnRight = m_libraryGridButtons[value4];
			selectable.navigation = new Navigation
			{
				mode = Navigation.Mode.Explicit,
				selectOnUp = selectOnUp,
				selectOnDown = selectOnDown,
				selectOnLeft = selectOnLeft,
				selectOnRight = selectOnRight
			};
		}
	}

	private void GetComponentsInChildrenExclusively<T>(Transform parent, ref List<T> components)
	{
		for (int i = 0; i < parent.childCount; i++)
		{
			T component = parent.GetChild(i).GetComponent<T>();
			if (component != null)
			{
				components.Add(component);
			}
		}
	}

	private void ResetTexts()
	{
		if (!(m_AlreadySelectedCampaign != null))
		{
			OnCampaignNameChanged(localisedCampaignNameText);
			OnCampaignDescriptionChanged(localisedCampaignDescriptionText);
			OnThankyouTitleChanged(localisedCampaignThankYouTitleText);
			OnThankyouDescriptionChanged(localisedCampaignThankYouDescText);
		}
	}
}

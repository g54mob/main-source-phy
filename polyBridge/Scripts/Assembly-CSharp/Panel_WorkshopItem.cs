using System.Collections.Generic;
using System.IO;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_WorkshopItem : MonoBehaviour
{
	[Header("Header")]
	public Button m_CancelButton;

	public TextMeshProUGUI m_Title;

	public TextMeshProUGUI m_CreatedByName;

	[Header("Body")]
	public GameObject m_WaitingAnimation;

	public RawImage m_RawImage;

	[Header("Stars")]
	public Image[] m_Stars;

	[Header("Middle Icons Left")]
	public TextMeshProUGUI m_NumUpVotes;

	public TextMeshProUGUI m_NumDownVotes;

	[Header("Voting")]
	public Panel_WorkshopVoting m_Voting;

	[Header("Middle Icons Right")]
	public TextMeshProUGUI m_ProgressText;

	public Image m_CompletedIcon;

	public Image m_UnderBudgetIcon;

	public Image m_UnderBudgetUnbreakingIcon;

	public Image m_ActivatedIcon;

	public Image m_CheatIcon;

	public Image m_SuscribedIcon;

	public Image m_DateIcon;

	public ToolTipText m_DateIconToolTipText;

	[Header("Footer Right")]
	public Button m_SubscribeButton;

	public Button m_UnSubscribeButton;

	public Button m_ActivateButton;

	public Button m_DeActivateButton;

	public Button m_PlayButton;

	public Button m_EditButton;

	public GameObject m_SubscribeWaitAnimation;

	public GameObject m_UnSubscribeWaitAnimation;

	public GameObject m_SubscribeIconAndText;

	public GameObject m_UnSubscribeIconAndText;

	[Header("Footer Left")]
	public Button m_CopyToClipboardButton;

	public TextMeshProUGUI m_LevelIDText;

	public Button m_CreatedByButton;

	public Button m_TrashButton;

	public Button m_GalleryButton;

	[Header("Budget & Materials")]
	public GameObject m_BudgetAndMaterialsParent;

	public TextMeshProUGUI m_Budget;

	public GameObject[] m_MaterialIconObjects;

	[Header("Description")]
	public RectTransform m_DescriptionRectTransform;

	public TextMeshProUGUI m_Description;

	public ScrollRect m_DescriptionScrollRect;

	[Header("Cheats")]
	public GameObject m_UnlimitedBudgetLocked;

	public GameObject m_UnlimitedMaterialsLocked;

	public TwoStateButton m_UnlimitedBudgetButton;

	public TwoStateButton m_UnlimitedMaterialsButton;

	private WorkshopItemSlot m_Slot;

	private WorkshopItem m_Item;

	private bool m_Initialized;

	private RectTransform m_CreatedByButtonRectTransform;

	private readonly int DESCRIPTION_WIDTH_SMALL = 375;

	private readonly int DESCRIPTION_WIDTH_BIG = 580;

	private bool m_Subscribing;

	private bool m_UnSubscribing;

	private void Awake()
	{
		m_CreatedByButtonRectTransform = m_CreatedByButton.GetComponent<RectTransform>();
		m_UnlimitedBudgetButton.m_Button.onClick.AddListener(OnUnlimitedBudgetToggle);
		m_UnlimitedMaterialsButton.m_Button.onClick.AddListener(OnUnlimitedMaterialToggle);
	}

	private void OnEnable()
	{
		ActivePanels.Add(base.gameObject);
		BridgeCheat.m_ForceUnlimitedBudget = false;
		BridgeCheat.m_ForceUnlimitedMaterial = false;
		m_UnlimitedBudgetButton.TurnOn(on: false);
		m_UnlimitedMaterialsButton.TurnOn(on: false);
		InterfaceAudio.Play("ui_window_open");
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
	}

	private void Update()
	{
		if (base.gameObject.activeInHierarchy)
		{
			ProcessInput();
			UpdateHeader();
			UpdateFooterRight();
			UpdateMiddleIcons();
			UpdateCheatToggles();
		}
	}

	private void Init()
	{
		if (!m_Initialized)
		{
			m_CancelButton.onClick.AddListener(OnCancel);
			m_CopyToClipboardButton.onClick.AddListener(OnCopyToClipboard);
			m_CreatedByButton.onClick.AddListener(OnCreatedBy);
			m_TrashButton.onClick.AddListener(OnTrash);
			m_GalleryButton.onClick.AddListener(OnGallery);
			m_PlayButton.onClick.AddListener(OnPlay);
			m_EditButton.onClick.AddListener(OnEdit);
			m_SubscribeButton.onClick.AddListener(OnSubscribe);
			m_UnSubscribeButton.onClick.AddListener(OnUnSubscribe);
			m_ActivateButton.onClick.AddListener(OnActivate);
			m_DeActivateButton.onClick.AddListener(OnDeActivate);
			m_Initialized = true;
		}
	}

	public void Open(WorkshopItemSlot slot)
	{
		base.gameObject.SetActive(value: true);
		Init();
		m_Slot = slot;
		m_Item = slot.m_Item;
		m_WaitingAnimation.SetActive(value: false);
		m_DescriptionScrollRect.verticalNormalizedPosition = 1f;
		UpdateHeader();
		UpdateRawImage();
		UpdateMiddle();
		UpdateFooterLeft();
		UpdateFooterRight();
		UpdateBudgetMaterialsAndDescription();
		UpdateCheatToggles();
		m_TrashButton.gameObject.SetActive(m_Item.IsOwnedByMe());
		m_GalleryButton.gameObject.SetActive(m_Item.IsLevel());
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		if (m_PlayButton.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.NORTH, Localize.Get("UI_WORKSHOPITEM_PLAY"), GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
		}
		else
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
		}
	}

	public void Close()
	{
		m_Voting.MaybeWriteWorkshopItemVotes(m_Item);
		m_Voting.MaybeSyncVoteToSteam(m_Item);
		m_Voting.MaybeWriteWorkshopItemFavorite(m_Item);
		m_Voting.MaybeSyncFavoriteToSteam(m_Item);
		InterfaceAudio.Play("ui_window_close");
		base.gameObject.SetActive(value: false);
	}

	public bool IsPointerOverCreatedByButton()
	{
		return TMP_TextUtilities.IsIntersectingRectTransform(m_CreatedByButtonRectTransform, GameInput.GetMousePosition(), null);
	}

	public string GetCreatedByToolTipText()
	{
		return string.Format(Localize.Get("UI_WORKSHOPITEM_VIEW"), GameUI.GOLD_COLOR_HEX_TAG + m_Item.GetCreatorName());
	}

	public void UpdateHeader()
	{
		m_Title.text = m_Item.GetTitle();
		m_CreatedByName.text = Localize.Get("UI_WORKSHOP_BY", m_Slot.m_Item.GetCreatorName());
	}

	public void UpdateRawImage()
	{
		m_RawImage.color = Color.white;
		m_RawImage.texture = ((m_Item.m_PreviewTexture == null) ? GameUI.m_Instance.m_Workshop.m_DefaultSlotPreview : m_Item.m_PreviewTexture);
		if (m_RawImage.texture != null)
		{
			Utils.SizeRawImageToParent(m_RawImage);
		}
		m_RawImage.gameObject.SetActive(value: true);
	}

	public void UpdateMiddle()
	{
		m_Voting.UpdateVoteIcons(m_Item);
		m_Voting.UpdateFavoriteButtons(m_Item);
		UpdateMiddleIcons();
	}

	private void UpdateMiddleIcons()
	{
		UpdateNumUpVotes();
		m_CheatIcon.gameObject.SetActive(m_Slot.m_CheatIcon.gameObject.activeSelf);
		m_ActivatedIcon.gameObject.SetActive(m_Slot.m_ModActivatedIcon.gameObject.activeSelf);
		if (m_Item != null)
		{
			m_SuscribedIcon.gameObject.SetActive(m_Item.IsSubscribed());
			string text = GameUI.MarkupForGold(Localize.Get("UI_WORKSHOP_LAST_UPDATED") + ":") + "\n" + Utils.FormatShortDate(m_Item.GetLastUpdatedDate());
			m_DateIconToolTipText.m_Text = text;
			m_DateIcon.gameObject.SetActive(value: true);
		}
		else
		{
			m_SuscribedIcon.gameObject.SetActive(value: false);
			m_DateIcon.gameObject.SetActive(value: false);
		}
		m_CompletedIcon.gameObject.SetActive(m_Slot.m_CompletedIcon.gameObject.activeSelf);
		m_UnderBudgetIcon.gameObject.SetActive(m_Slot.m_UnderBudgetIcon.gameObject.activeSelf);
		m_UnderBudgetUnbreakingIcon.gameObject.SetActive(m_Slot.m_UnderBudgetUnbreakingIcon.gameObject.activeSelf);
		m_ProgressText.transform.gameObject.SetActive(m_Slot.m_ProgressText.transform.parent.gameObject.activeSelf);
		m_ProgressText.text = m_Slot.m_ProgressText.text;
	}

	public void UpdateFooterLeft()
	{
		m_LevelIDText.text = m_Item.GetId();
		m_CopyToClipboardButton.gameObject.SetActive(!Game.IsRunningOnSteamDeck() && GameInput.GetActiveGameDevice() == GameDevice.KeyboardAndMouse);
	}

	public void UpdateFooterRight()
	{
		bool num = m_SubscribeButton.gameObject.activeInHierarchy && m_SubscribeButton.GetComponent<HighlightOnHover>().IsHighlighted();
		bool num2 = m_UnSubscribeButton.gameObject.activeInHierarchy && m_UnSubscribeButton.GetComponent<HighlightOnHover>().IsHighlighted();
		m_SubscribeButton.gameObject.SetActive(!m_Item.IsSubscribed() || m_Subscribing);
		m_SubscribeIconAndText.SetActive(!m_Subscribing);
		m_SubscribeWaitAnimation.SetActive(m_Subscribing);
		if (num2 && m_SubscribeButton.gameObject.activeInHierarchy)
		{
			m_SubscribeButton.GetComponent<HighlightOnHover>().ForceHighlight();
		}
		m_UnSubscribeButton.gameObject.SetActive(!m_SubscribeButton.gameObject.activeSelf);
		m_UnSubscribeIconAndText.SetActive(!m_UnSubscribing);
		m_UnSubscribeWaitAnimation.SetActive(m_UnSubscribing);
		if (num && m_UnSubscribeButton.gameObject.activeInHierarchy)
		{
			m_UnSubscribeButton.GetComponent<HighlightOnHover>().ForceHighlight();
		}
		m_EditButton.gameObject.SetActive(m_Item.IsOwnedByMe() && m_Item.IsInstalled() && m_Item.IsSubscribed() && !m_Subscribing);
		if (m_Item.IsLevel() || m_Item.IsCampaign())
		{
			m_ActivateButton.gameObject.SetActive(value: false);
			m_DeActivateButton.gameObject.SetActive(value: false);
			m_PlayButton.gameObject.SetActive(m_Item.IsInstalled() && m_Item.IsSubscribed() && !m_Subscribing);
		}
		else
		{
			m_ActivateButton.gameObject.SetActive(!Mods.ModIsActive(m_Item.GetId()) && m_Item.IsInstalled() && m_Item.IsSubscribed() && !m_Subscribing);
			m_DeActivateButton.gameObject.SetActive(Mods.ModIsActive(m_Item.GetId()) && m_Item.IsSubscribed() && !m_Subscribing);
			m_PlayButton.gameObject.SetActive(value: false);
		}
		GameUI.SetAndEnableText(m_Description, (m_Item.GetDescription() != null) ? m_Item.GetDescription().Replace("\\n", "\n") : string.Empty);
	}

	private void UpdateBudgetMaterialsAndDescription()
	{
		if (m_Item.IsLevel())
		{
			m_BudgetAndMaterialsParent.SetActive(value: true);
			PopulateBudgetAndMaterials(m_Item.GetMetadata());
			m_DescriptionRectTransform.sizeDelta = new Vector2(DESCRIPTION_WIDTH_SMALL, m_DescriptionRectTransform.sizeDelta.y);
		}
		else
		{
			m_BudgetAndMaterialsParent.SetActive(value: false);
			m_DescriptionRectTransform.sizeDelta = new Vector2(DESCRIPTION_WIDTH_BIG, m_DescriptionRectTransform.sizeDelta.y);
		}
	}

	private void OnCancel()
	{
		InterfaceAudio.Play("ui_window_close");
		Close();
	}

	private void OnCreatedBy()
	{
		GameUI.m_Instance.m_Workshop.m_FilterBar.ShowAllItemsForCreator(m_Item);
		InterfaceAudio.Play("ui_window_close");
		Close();
	}

	private void OnTrash()
	{
		PopUpMessage.DisplayWarning(Localize.Get("POPUP_WORKSHOP_DELETE"), useYesNoLables: true, OnTrashConfirm);
	}

	private void OnGallery()
	{
		InterfaceAudio.Play("ui_window_close");
		Close();
		GameUI.m_Instance.m_Workshop.m_RootPanel.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_Workshop.m_Ducking.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_Gallery.CloseNoReturn();
		GameUI.m_Instance.m_Gallery.OpenWorkshopItem(m_Item.GetTitle(), m_Item.GetId());
		GameUI.m_Instance.m_Gallery.m_ReturnToWorkshop = true;
	}

	private void OnCopyToClipboard()
	{
		InterfaceAudio.Play("ui_menu_select");
		GameUI.CopyToClipboard(m_Item.GetId());
	}

	private void OnPlay()
	{
		m_Slot.OnPlay(m_UnlimitedBudgetButton.IsOn(), m_UnlimitedMaterialsButton.IsOn());
	}

	private void OnEdit()
	{
		if (m_Item.IsLevel())
		{
			string directory = m_Slot.m_Item.GetDirectory();
			if (string.IsNullOrEmpty(directory))
			{
				InterfaceAudio.PlayErrorBeep();
				return;
			}
			List<string> inactiveModsInLayout = Mods.GetInactiveModsInLayout(Path.Combine(directory, Workshop.LEVEL_LAYOUT_FILENAME));
			if (inactiveModsInLayout.Count > 0)
			{
				GameUI.m_Instance.m_ModsRequiredPopup.Open(inactiveModsInLayout, null, DoEditLevel);
				return;
			}
			Mods.DeactivateAutoLoadedMods();
			DoEditLevel(null);
		}
		else
		{
			string text = ModsSource.GetSourceFolder(ModsSource.GetLocalItemFromUpload(m_Slot.m_Item.GetId()));
			if (string.IsNullOrEmpty(text) || !Utils.DirectoryExists(text))
			{
				text = Mods.GetPathToMod(m_Slot.m_Item.GetId());
			}
			if (string.IsNullOrEmpty(text))
			{
				InterfaceAudio.PlayErrorBeep();
				return;
			}
			Close();
			GameUI.m_Instance.m_Workshop.m_SubmitModPanel.Open(m_Slot.m_Item.GetId(), m_Slot.m_Item.GetTitle(), text, null);
		}
	}

	private void DoEditLevel(FileSlot slot)
	{
		GameStatePreloadingAssets.PreloadLevel(Path.Combine(m_Slot.m_Item.GetDirectory(), Workshop.LEVEL_LAYOUT_FILENAME), slot, PreloadEditLevelCallback);
	}

	public void OpenLevel(WorkshopItemSlot slot)
	{
		GameStatePreloadingAssets.PreloadLevel(Path.Combine(slot.m_Item.GetDirectory(), Workshop.LEVEL_LAYOUT_FILENAME), null, PreloadEditLevelCallback);
	}

	private void PreloadEditLevelCallback(string layoutPath, FileSlot slot)
	{
		GameManager.SetGameMode(GameMode.SANDBOX, GameSubMode.NONE);
		GameStateManager.SwitchToStateImmediate(GameState.LOADING_LEVEL_IMMEDIATE);
		SandboxLayoutData sandboxLayoutData = SandboxLayout.Load(layoutPath);
		sandboxLayoutData.m_Workshop.m_Id = m_Item.GetId();
		Sandbox.Clear();
		Sandbox.Load(sandboxLayoutData.m_ThemeStubId, sandboxLayoutData, loadBridge: true);
		SandboxUndo.Clear();
		SandboxUndo.SnapShot();
		PointsOfView.OnLayoutLoaded(string.Empty);
		Sandbox.m_CurrentLayoutName = "";
		GameStateManager.SwitchToState(GameState.SANDBOX);
		SandboxSettings.m_Title = m_Item.GetTitle();
		SandboxSettings.m_Description = m_Item.GetDescription();
		GameUI.m_Instance.m_SandboxTitleAndDescription.SetTitle(SandboxSettings.m_Title);
		GameUI.m_Instance.m_SandboxTitleAndDescription.SetDescription(SandboxSettings.m_Description);
		GameUI.m_Instance.m_Workshop.m_WorkshopItemPanel.Close();
		GameUI.m_Instance.m_Workshop.Close();
	}

	private async void OnSubscribe()
	{
		if (m_Subscribing || m_Item.IsSubscribed())
		{
			return;
		}
		m_Subscribing = true;
		if (await m_Item.m_SteamItem.Subscribe())
		{
			m_Slot.m_SubscribedIcon.gameObject.SetActive(value: true);
			if (await m_Item.m_SteamItem.DownloadAsync())
			{
				Workshop.AddToSubscribedItems(new SteamItemInfo(m_Item.m_SteamItem));
				Workshop.SaveSubscribedItemsToDisk();
				GameAchievements.UnlockAchievement(GameAchievement.UI_ExtraFlavor);
				m_Slot.UpdateFields();
			}
		}
		else
		{
			Debug.LogWarning("Failed to Subscribe to " + m_Item.GetId());
		}
		m_Subscribing = false;
	}

	private async void OnUnSubscribe()
	{
		if (m_UnSubscribing || !m_Item.IsSubscribed())
		{
			return;
		}
		m_UnSubscribing = true;
		if (await m_Item.m_SteamItem.Unsubscribe())
		{
			m_Slot.m_SubscribedIcon.gameObject.SetActive(value: false);
			Workshop.RemoveFromSubscribedItems(m_Item.GetId());
			Workshop.SaveSubscribedItemsToDisk();
			if (m_Item.IsMod() || m_Item.IsCampaign())
			{
				Mods.DeactivateMod(m_Item.GetId());
			}
			m_Slot.UpdateFields();
		}
		else
		{
			Debug.LogWarning("Failed to Unsubscribe " + m_Item.GetId());
		}
		m_UnSubscribing = false;
	}

	private void OnActivate()
	{
		if (!Mods.ModIsActive(m_Item.GetId()))
		{
			Mods.ActivateMod(m_Item.GetId());
			UpdateFooterRight();
			UpdateMiddleIcons();
			m_ActivatedIcon.gameObject.SetActive(value: true);
			m_Slot.m_ModActivatedIcon.gameObject.SetActive(value: true);
		}
	}

	private void OnDeActivate()
	{
		string id = m_Item.GetId();
		if (Mods.ModIsActive(id))
		{
			Mods.DeactivateMod(id);
			UpdateFooterRight();
			UpdateMiddleIcons();
			m_ActivatedIcon.gameObject.SetActive(value: false);
			m_Slot.m_ModActivatedIcon.gameObject.SetActive(value: false);
		}
	}

	private async void OnTrashConfirm()
	{
		string itemId = m_Item.GetId();
		GameUI.m_Instance.m_Status.Open(Localize.Get("UI_STATUS_DELETING_WORKSHOP_ITEM"));
		if (!(await SteamUGC.DeleteFileAsync(m_Item.m_SteamItem.Id)))
		{
			GameUI.m_Instance.m_Status.Complete(Localize.Get("UI_STATUS_DELETE_WORKSHOP_ITEM_FAILED"));
			return;
		}
		if (m_Item.IsMod())
		{
			Mods.DeleteFromWorkshopPath(itemId);
			Mods.DeactivateMod(itemId);
		}
		Workshop.RemoveFromSubscribedItems(itemId);
		Workshop.SaveSubscribedItemsToDisk();
		Close();
		GameUI.m_Instance.m_Status.Complete(Localize.Get("UI_STATUS_DELETE_WORKSHOP_ITEM_SUCCESS"));
		GameUI.m_Instance.m_Workshop.ForceRefreshCurrentTab();
	}

	private void ProcessInput()
	{
		if (!GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			if (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
			{
				Close();
			}
			else if (m_PlayButton.gameObject.activeInHierarchy && GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH))
			{
				OnPlay();
			}
		}
	}

	private void PopulateBudgetAndMaterials(string metadata)
	{
		if (!string.IsNullOrEmpty(metadata))
		{
			int budget = WorkshopMetaData.GetBudget(metadata);
			m_Budget.text = Utils.FormatCash(budget);
			List<int> materialCounts = WorkshopMetaData.GetMaterialCounts(metadata);
			for (int i = 0; i < m_MaterialIconObjects.Length; i++)
			{
				m_MaterialIconObjects[i].SetActive(materialCounts != null && i < materialCounts.Count && materialCounts[i] > 0);
				WorkshopMetaData.SetMaterialCountForIcon(m_MaterialIconObjects[i], (!WorkshopMetaData.IsLegacy(metadata)) ? materialCounts[i] : 0);
			}
		}
	}

	private void UpdateNumUpVotes()
	{
		m_NumUpVotes.text = m_Item.m_SteamItem.VotesUp.ToString();
		m_NumDownVotes.text = m_Item.m_SteamItem.VotesDown.ToString();
	}

	private void UpdateCheatToggles()
	{
		int budget = WorkshopMetaData.GetBudget(m_Item.GetMetadata());
		bool flag = BridgeSaveSlots.HasCompletedLevelUnderBudgetNoBreaks(m_Item.GetId(), budget);
		bool flag2 = flag || BridgeSaveSlots.HasCompletedLevelUnderBudget(m_Item.GetId(), budget);
		m_UnlimitedBudgetLocked.SetActive(!flag2);
		m_UnlimitedMaterialsLocked.SetActive(!flag);
		m_UnlimitedBudgetButton.gameObject.SetActive(!m_UnlimitedBudgetLocked.activeSelf);
		m_UnlimitedMaterialsButton.gameObject.SetActive(!m_UnlimitedMaterialsLocked.activeSelf);
	}

	private void OnUnlimitedBudgetToggle()
	{
		InterfaceAudio.Play("ui_menu_select");
		m_UnlimitedBudgetButton.Toggle();
		m_UnlimitedBudgetButton.m_ToolTipText.m_RawLocalizationKey = (m_UnlimitedBudgetButton.IsOn() ? "UI_UNLIMITED_BUDGET_ON" : "UI_UNLIMITED_BUDGET_OFF");
	}

	private void OnUnlimitedMaterialToggle()
	{
		InterfaceAudio.Play("ui_menu_select");
		m_UnlimitedMaterialsButton.Toggle();
		m_UnlimitedMaterialsButton.m_ToolTipText.m_RawLocalizationKey = (m_UnlimitedMaterialsButton.IsOn() ? "UI_UNLIMITED_MATERIAL_ON" : "UI_UNLIMITED_MATERIAL_OFF");
	}
}

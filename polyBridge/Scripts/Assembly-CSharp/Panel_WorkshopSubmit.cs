using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_WorkshopSubmit : MonoBehaviour
{
	public RectTransform m_Root;

	public Panel_WorkshopConfirmBudget m_ConfirmBudgetPanel;

	[Header("Title")]
	public TMP_InputField m_TitleInputField;

	public Button m_TitleInputFieldGamepadButton;

	[Header("Description")]
	public TMP_InputField m_DescriptionInputField;

	public Scrollbar m_Scrollbar;

	public RectTransform m_ScrollbarRectTransform;

	public Button m_DescriptionInputFieldGamepadButton;

	[Header("Thumbnail")]
	public RawImage m_ThumbnailRawImage;

	[Header("Buttons")]
	public Button m_CancelButton;

	public Button m_SubmitButton;

	public Button m_EnterPhotoModeButton;

	[Header("Toggles")]
	public Toggle m_ShowPrebuildsToggle;

	public Toggle m_AutoPlayToggle;

	public Toggle m_AllowFeaturedToggle;

	private PointerEvents m_ShowPrebuildsTogglePointerEvents;

	private PointerEvents m_AutoPlayTogglePointerEvents;

	private PointerEvents m_AllowFeaturedTogglePointerEvents;

	private bool m_OpenedFromPhotoMode;

	private int m_FramesUntilAllowNextThumbnailCreate;

	private void Awake()
	{
		m_ShowPrebuildsTogglePointerEvents = m_ShowPrebuildsToggle.GetComponent<PointerEvents>();
		m_ShowPrebuildsTogglePointerEvents.RegisterOnClickedDelegate(OnShowPrebuildsToggle);
		m_AutoPlayTogglePointerEvents = m_AutoPlayToggle.GetComponent<PointerEvents>();
		m_AutoPlayTogglePointerEvents.RegisterOnClickedDelegate(OnAutoPlayToggle);
		m_AllowFeaturedTogglePointerEvents = m_AllowFeaturedToggle.GetComponent<PointerEvents>();
		m_AllowFeaturedTogglePointerEvents.RegisterOnClickedDelegate(OnAllowFeaturedToggle);
		m_TitleInputField.characterLimit = Workshop.TITLE_CHAR_LIMIT;
		m_DescriptionInputField.characterLimit = Workshop.DESCRIPTION_CHAR_LIMIT;
		TMP_InputField titleInputField = m_TitleInputField;
		titleInputField.onValidateInput = (TMP_InputField.OnValidateInput)Delegate.Combine(titleInputField.onValidateInput, new TMP_InputField.OnValidateInput(Utils.StripTab));
		TMP_InputField descriptionInputField = m_DescriptionInputField;
		descriptionInputField.onValidateInput = (TMP_InputField.OnValidateInput)Delegate.Combine(descriptionInputField.onValidateInput, new TMP_InputField.OnValidateInput(Utils.StripTab));
	}

	private void Start()
	{
		m_CancelButton.onClick.AddListener(OnCancel);
		m_SubmitButton.onClick.AddListener(OnTrySubmit);
		m_EnterPhotoModeButton.onClick.AddListener(OnEnterPhotoMode);
		m_TitleInputFieldGamepadButton.onClick.AddListener(OnTitleInputFieldGamepadButton);
		m_DescriptionInputFieldGamepadButton.onClick.AddListener(OnDescriptionInputFieldGamepadButton);
	}

	public void OpenFromPhotoMode(string itemID)
	{
		m_OpenedFromPhotoMode = true;
		Open(itemID);
	}

	public void Open(string itemID)
	{
		base.gameObject.SetActive(value: true);
		UpdateScrollbar();
		if (m_OpenedFromPhotoMode)
		{
			m_ThumbnailRawImage.texture = WorkshopPreview.m_PreviewTexture2D;
			m_ThumbnailRawImage.gameObject.SetActive(value: true);
			m_OpenedFromPhotoMode = false;
		}
		else
		{
			m_ThumbnailRawImage.texture = null;
			m_ThumbnailRawImage.gameObject.SetActive(value: false);
			MaybeUseExistingImage(itemID);
			if (m_ThumbnailRawImage.texture == null)
			{
				WorkshopPreview.Create(WorkshopSubmit.m_AutoPlay, WorkshopSubmit.m_ShowPrebuilds, PointOfViewType.SIM_LEFT, GameStateManager.GetState(), null, null, OnCreatePreviewComplete);
			}
			WorkshopSubmit.SetTitle(SandboxSettings.m_Title);
			WorkshopSubmit.SetDescription(SandboxSettings.m_Description);
			PopulateFromWorkshop();
			if (Sandbox.m_CurrentLayoutData != null)
			{
				m_TitleInputField.text = SandboxSettings.m_Title;
			}
			else
			{
				m_TitleInputField.text = string.Empty;
			}
		}
		UpdatePlaceholderText();
		m_TitleInputField.caretPosition = SandboxSettings.m_Title.Length;
		m_DescriptionInputField.caretPosition = SandboxSettings.m_Description.Length;
		m_FramesUntilAllowNextThumbnailCreate = 0;
		ActivePanels.Add(base.gameObject);
		ShowGamepadLegend();
	}

	public void UpdateForCurrentDevice()
	{
		m_TitleInputField.interactable = !GamepadVirtualKeyboard.IsSupported();
		m_DescriptionInputField.interactable = !GamepadVirtualKeyboard.IsSupported();
		m_TitleInputFieldGamepadButton.gameObject.SetActive(GamepadVirtualKeyboard.IsSupported());
		m_DescriptionInputFieldGamepadButton.gameObject.SetActive(GamepadVirtualKeyboard.IsSupported());
	}

	public void OnCancel()
	{
		InterfaceAudio.Play("ui_window_close");
		Close();
	}

	private void OnEnable()
	{
		m_Root.anchoredPosition = new Vector2(0f, Game.IsRunningOnSteamDeck() ? (-20) : (-70));
	}

	private void OnDisable()
	{
		PopulateToWorkshop();
		UpdateTitleAndDescriptionForLevel();
		m_TitleInputField.onSelect.RemoveAllListeners();
		m_DescriptionInputField.onSelect.RemoveAllListeners();
		ActivePanels.Remove(base.gameObject);
	}

	private void Update()
	{
		m_FramesUntilAllowNextThumbnailCreate--;
		if (m_FramesUntilAllowNextThumbnailCreate <= 0)
		{
			Game.m_TakingScreenshotForWorkshopSubmit = false;
			m_FramesUntilAllowNextThumbnailCreate = 0;
		}
		ProcessInput();
		UpdateScrollbar();
		UpdatePlaceholderText();
		if (ActivePanels.IsTopPanel(base.gameObject))
		{
			ShowGamepadLegend();
		}
	}

	public void SubmitAfterSimulation()
	{
		PopulateToWorkshop();
		UpdateTitleAndDescriptionForLevel();
		SandboxLayoutData layoutData = SandboxLayout.SerializeToProxies();
		List<string> automaticallyGeneratedTags = WorkshopSubmit.GetAutomaticallyGeneratedTags(layoutData);
		automaticallyGeneratedTags.Add(WorkshopTags.LEVEL_TAG);
		if (WorkshopSubmit.m_AutoPlay)
		{
			automaticallyGeneratedTags.Add(WorkshopTags.AUTOPLAY_TAG);
		}
		if (WorkshopSubmit.m_AllowFeatured)
		{
			automaticallyGeneratedTags.Add(WorkshopTags.ALLOWFEATURED_TAG);
		}
		if (!string.IsNullOrEmpty(Workshop.m_ForceWorkshopID))
		{
			DoSubmit(Workshop.m_ForceWorkshopID, layoutData, automaticallyGeneratedTags);
		}
		else
		{
			DoSubmit((Sandbox.m_CurrentLayoutData != null) ? Sandbox.m_CurrentLayoutData.m_Workshop.m_Id : string.Empty, layoutData, automaticallyGeneratedTags);
		}
	}

	private void DoSubmit(string workshopID, SandboxLayoutData layoutData, List<string> tags)
	{
		if (!string.IsNullOrEmpty(workshopID))
		{
			WorkshopSubmit.Overwrite(WorkshopPreview.m_PreviewBytes, layoutData, tags, workshopID);
		}
		else
		{
			WorkshopSubmit.Submit(WorkshopPreview.m_PreviewBytes, layoutData, tags);
		}
	}

	public void OnTrySubmit()
	{
		if (GameManager.IsSteamOffline())
		{
			PopUpMessage.DisplayErrorOkOnly(Localize.Get("UI_STEAM_OFFLINE"));
		}
		else if (!TitleIsValid())
		{
			PopUpMessage.DisplayWarningOkOnly(string.Format(Localize.Get("WARN_WORKSHOP_MIN_TITLE_LEN"), WorkshopSubmit.MIN_CHARS_IN_TITLE));
		}
		else if (!string.IsNullOrEmpty(Workshop.m_ForceWorkshopID))
		{
			PopUpMessage.DisplayWarning(Workshop.GetForceWorkshopIDWarningMessage(), useYesNoLables: true, ReallyOnTrySubmit);
		}
		else
		{
			ReallyOnTrySubmit();
		}
	}

	private void ReallyOnTrySubmit()
	{
		WorkshopSubmit.SetTitle(SandboxSettings.m_Title);
		WorkshopSubmit.SetDescription(SandboxSettings.m_Description);
		if (!string.IsNullOrEmpty(Workshop.m_ForceWorkshopID))
		{
			DoOverwrite();
			return;
		}
		string levelId = Game.GetLevelId();
		if (IsEditingExistingItem(levelId))
		{
			PopUpTwoChoices.Display(string.Format(Localize.Get("UI_MODS_OVERWRITE_CONFIRM")), Localize.Get("UI_SANDBOX_OVERWRITE_LEVEL"), Localize.Get("UI_SANDBOX_SUBMIT_NEW_LEVEL"), DoOverwrite, TrySubmitAsNew);
		}
		else
		{
			TrySubmitAsNew();
		}
	}

	private void TrySubmitAsNew()
	{
		Sandbox.m_CurrentLayoutData.m_Workshop.m_Id = string.Empty;
		if (Budget.m_CashBudget == Budget.UNLIMITED_CASH_BUDGET)
		{
			m_ConfirmBudgetPanel.Open();
		}
		else
		{
			SimulateAndSubmit();
		}
	}

	public void OnEnterPhotoMode()
	{
		GameStateManager.SwitchToState(GameState.PHOTO);
		GameStatePhoto.SetItemID(Game.GetLevelId());
		Close();
	}

	public void OnCreatePreviewComplete()
	{
		m_ThumbnailRawImage.texture = WorkshopPreview.m_PreviewTexture2D;
		m_ThumbnailRawImage.gameObject.SetActive(value: true);
	}

	private void DoOverwrite()
	{
		SimulateAndSubmit();
	}

	public void Submit()
	{
		PopulateToWorkshop();
		SimulateAndSubmit();
	}

	public void Close()
	{
		base.gameObject.SetActive(value: false);
	}

	private void PopulateToWorkshop()
	{
		WorkshopSubmit.m_Title = m_TitleInputField.text.Trim();
		WorkshopSubmit.m_Description = m_DescriptionInputField.text.Trim();
		WorkshopSubmit.m_AutoPlay = m_AutoPlayToggle.isOn;
		WorkshopSubmit.m_AllowFeatured = m_AllowFeaturedToggle.isOn;
		WorkshopSubmit.m_ShowPrebuilds = m_ShowPrebuildsToggle.isOn;
	}

	private void PopulateFromWorkshop()
	{
		m_TitleInputField.text = WorkshopSubmit.m_Title;
		m_DescriptionInputField.text = WorkshopSubmit.m_Description;
		m_AutoPlayToggle.isOn = WorkshopSubmit.m_AutoPlay;
		m_AllowFeaturedToggle.isOn = WorkshopSubmit.m_AllowFeatured;
		m_ShowPrebuildsToggle.isOn = WorkshopSubmit.m_ShowPrebuilds;
	}

	private void UpdateScrollbar()
	{
		if (Mathf.Approximately(m_Scrollbar.size, 1f))
		{
			m_ScrollbarRectTransform.anchoredPosition = new Vector2(5000f, m_ScrollbarRectTransform.anchoredPosition.y);
		}
		else
		{
			m_ScrollbarRectTransform.anchoredPosition = new Vector2(15f, m_ScrollbarRectTransform.anchoredPosition.y);
		}
	}

	private void SimulateAndSubmit()
	{
		GameStateManager.SwitchToState(GameState.SIM);
		InterfaceAudio.Play("ui_simulation_start");
		WorkshopSubmit.m_RunSimulationBeforeSubmit = true;
		WorkshopSubmit.m_SimulationPassed = false;
		WorkshopSubmit.m_RanSimulation = true;
		Close();
	}

	private void OnShowPrebuildsToggle()
	{
		if (m_FramesUntilAllowNextThumbnailCreate == 0)
		{
			InterfaceAudio.Play("ui_settings_toggle");
			Game.m_TakingScreenshotForWorkshopSubmit = true;
			WorkshopSubmit.m_AutoPlay = m_AutoPlayToggle.isOn;
			WorkshopSubmit.m_ShowPrebuilds = m_ShowPrebuildsToggle.isOn;
			WorkshopPreview.Create(m_AutoPlayToggle.isOn, m_ShowPrebuildsToggle.isOn, PointOfViewType.SIM_LEFT, GameStateManager.GetState(), null, null, OnCreatePreviewComplete);
			m_FramesUntilAllowNextThumbnailCreate = 4;
		}
		else
		{
			m_ShowPrebuildsToggle.isOn = !m_ShowPrebuildsToggle.isOn;
		}
	}

	private void OnAutoPlayToggle()
	{
		if (m_FramesUntilAllowNextThumbnailCreate == 0)
		{
			InterfaceAudio.Play("ui_settings_toggle");
			Game.m_TakingScreenshotForWorkshopSubmit = true;
			WorkshopSubmit.m_AutoPlay = m_AutoPlayToggle.isOn;
			WorkshopSubmit.m_ShowPrebuilds = m_ShowPrebuildsToggle.isOn;
			WorkshopPreview.Create(m_AutoPlayToggle.isOn, m_ShowPrebuildsToggle.isOn, PointOfViewType.SIM_LEFT, GameStateManager.GetState(), null, null, OnCreatePreviewComplete);
			m_FramesUntilAllowNextThumbnailCreate = 4;
		}
		else
		{
			m_AutoPlayToggle.isOn = !m_AutoPlayToggle.isOn;
		}
	}

	private void OnAllowFeaturedToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
	}

	private bool TitleIsValid()
	{
		return m_TitleInputField.text.Trim().Length >= WorkshopSubmit.MIN_CHARS_IN_TITLE;
	}

	private void ProcessInput()
	{
		if (GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
		{
			OnCancel();
		}
		else if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (m_TitleInputField.isFocused)
			{
				m_DescriptionInputField.ActivateInputField();
				m_DescriptionInputField.Select();
			}
			else if (m_DescriptionInputField.isFocused)
			{
				m_TitleInputField.ActivateInputField();
				m_TitleInputField.Select();
			}
		}
	}

	private void MaybeUseExistingImage(string itemID)
	{
		if (IsEditingExistingItem(itemID))
		{
			WorkshopItem item = WorkshopCaches.GetItem(WorkshopTab.LEVELS, itemID);
			if (item != null && !(item.m_PreviewTexture == null))
			{
				WorkshopPreview.m_PreviewBytes = item.m_PreviewTexture.EncodeToJPG();
				m_ThumbnailRawImage.texture = item.m_PreviewTexture;
				m_ThumbnailRawImage.gameObject.SetActive(value: true);
			}
		}
	}

	private bool IsEditingExistingItem(string itemID)
	{
		return !string.IsNullOrEmpty(itemID);
	}

	private void UpdatePlaceholderText()
	{
		m_TitleInputField.placeholder.GetComponent<TextMeshProUGUI>().text = Localize.Get("UI_SANDBOX_ENTER_TITLE");
		m_DescriptionInputField.placeholder.GetComponent<TextMeshProUGUI>().text = Localize.Get("UI_SANDBOX_ENTER_DESCRIPTION");
	}

	private void UpdateTitleAndDescriptionForLevel()
	{
		string text = m_TitleInputField.text.Trim();
		if (!string.IsNullOrEmpty(text))
		{
			SandboxSettings.m_Title = text;
		}
		string text2 = m_DescriptionInputField.text.Trim();
		if (!string.IsNullOrEmpty(text2))
		{
			SandboxSettings.m_Description = text2;
		}
	}

	private void OnTitleInputFieldGamepadButton()
	{
		GamepadVirtualKeyboard.MaybeOpenVirtualKeyboard(m_TitleInputField.text, m_TitleInputField.characterLimit, Localize.Get("UI_TITLE"), multiline: false, OnTitleEntered);
	}

	private void OnDescriptionInputFieldGamepadButton()
	{
		GamepadVirtualKeyboard.MaybeOpenVirtualKeyboard(m_DescriptionInputField.text, m_DescriptionInputField.characterLimit, Localize.Get("UI_DESCRIPTION"), multiline: true, OnDescriptionEntered);
	}

	private void OnTitleEntered(string title)
	{
		if (title != null)
		{
			m_TitleInputField.text = title;
		}
	}

	private void OnDescriptionEntered(string description)
	{
		if (description != null)
		{
			m_DescriptionInputField.text = description;
		}
	}

	private void ShowGamepadLegend()
	{
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
	}
}

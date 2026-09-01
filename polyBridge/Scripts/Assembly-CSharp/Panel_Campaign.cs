using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Campaign : MonoBehaviour
{
	public RectTransform m_Root;

	[Header("Header")]
	public Banner m_Banner;

	public TextMeshProUGUI m_ProgressText;

	public Button m_CancelButton;

	public TextMeshProUGUI m_WorldName;

	public TextMeshProUGUI m_WorldDifficulty;

	public TextMeshProUGUI m_WorldSubtitle;

	[Header("Level List")]
	public Panel_FileLoader m_FileLoader;

	[Header("Thumbnail")]
	public RawImage m_RawImage;

	public Button m_ThumbnailButton;

	[Header("Level Status")]
	public GameObject m_PassStatus;

	public GameObject m_UnderBudgetStatus;

	public GameObject m_UnbreakingStatus;

	[Header("Level Details")]
	public TextMeshProUGUI m_LevelName;

	public TextMeshProUGUI m_LevelDescription;

	public TextMeshProUGUI m_LevelBudget;

	public Image m_RoadIcon;

	public Image m_WoodIcon;

	public Image m_SteelIcon;

	public Image m_HydraulicIcon;

	public Image m_RopeIcon;

	public Image m_CableIcon;

	public Image m_SpringIcon;

	public Image m_PillarIcon;

	[Header("Footer")]
	public Button m_PlayButton;

	public GameObject m_PlayButtonIcon;

	[Header("Cheats")]
	public GameObject m_UnlimitedBudgetLocked;

	public GameObject m_UnlimitedMaterialsLocked;

	public TwoStateButton m_UnlimitedBudgetButton;

	public TwoStateButton m_UnlimitedMaterialsButton;

	private Dictionary<FileSlot, CampaignLevel> m_SlotLevels = new Dictionary<FileSlot, CampaignLevel>();

	private Dictionary<CampaignLevel, Texture2D> m_Thumbs = new Dictionary<CampaignLevel, Texture2D>();

	private float m_LastClickTime;

	private FileSlot m_SelectedSlot;

	private int m_SelectedSlotIndex;

	private int m_SelectedSlotSetOnFrameCount;

	private readonly int THUMB_WIDTH = 640;

	private readonly int THUMB_HEIGHT = 360;

	private readonly int ROOT_Y_STEAMDECK = 310;

	private readonly int ROOT_Y_DEFAULT = 310;

	private CampaignWorld m_CampaignWorld;

	private Panel_CampaignWorldSelection m_Panel_CampaignWorldSelection;

	private void Awake()
	{
		m_UnlimitedBudgetButton.m_Button.onClick.AddListener(OnUnlimitedBudgetToggle);
		m_UnlimitedMaterialsButton.m_Button.onClick.AddListener(OnUnlimitedMaterialToggle);
		m_Panel_CampaignWorldSelection = GetComponentInChildren<Panel_CampaignWorldSelection>();
	}

	private void OnEnable()
	{
		ActivePanels.Add(base.gameObject);
		m_Root.anchoredPosition = new Vector2(0f, Game.IsRunningOnSteamDeck() ? ROOT_Y_STEAMDECK : ROOT_Y_DEFAULT);
		UpdateHeader();
		ShowGamepadLegend();
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
		GameUI.m_Instance.m_GamepadLegend.HideButtons();
	}

	private void Start()
	{
		m_CancelButton.onClick.AddListener(OnCancel);
		m_PlayButton.onClick.AddListener(OnPlay);
		m_ThumbnailButton.onClick.AddListener(OnThumbnail);
	}

	public void Open(string levelID, string worldID)
	{
		if (string.IsNullOrEmpty(worldID))
		{
			m_CampaignWorld = CampaignWorlds.m_Instance.GetWorldWithLevelId(levelID);
		}
		else
		{
			m_CampaignWorld = CampaignWorlds.m_Instance.GetWorldById(worldID);
		}
		base.gameObject.SetActive(value: true);
		CampaignWorlds.m_Instance.ClearUnlimitedBudgetAndMaterialFlags();
		if (m_CampaignWorld == null)
		{
			m_CampaignWorld = CampaignWorlds.m_Instance.m_Worlds[0];
		}
		PopulateSlots(m_CampaignWorld);
		SelectLevel(levelID);
		UpdateHeader();
		GameAchievements.InvalidateSpeedRunnerTimer();
		m_LastClickTime = 0f;
	}

	public string GetSelectedLevelID()
	{
		if (m_SelectedSlot == null)
		{
			return string.Empty;
		}
		CampaignLevel levelForSlot = GetLevelForSlot(m_SelectedSlot);
		if (levelForSlot == null)
		{
			return string.Empty;
		}
		return levelForSlot.m_Id;
	}

	private void Update()
	{
		m_Panel_CampaignWorldSelection.UpdateManual();
		ProcessInput();
		RefreshPlayButton();
		RefreshCheatTooltips();
		if (ActivePanels.IsTopPanel(base.gameObject))
		{
			ShowGamepadLegend();
		}
	}

	public void Close()
	{
		base.gameObject.SetActive(value: false);
		if (GameStateManager.GetState() == GameState.MAIN_MENU)
		{
			GameUI.m_Instance.m_MainMenuNew.Open();
		}
	}

	public string GetSelectedWorldID()
	{
		if (!(m_CampaignWorld != null))
		{
			return string.Empty;
		}
		return m_CampaignWorld.m_Id;
	}

	private void RefreshPlayButton()
	{
		CampaignLevel levelForSlot = GetLevelForSlot(m_SelectedSlot);
		m_PlayButton.gameObject.SetActive(!IsLocked(levelForSlot));
	}

	private void RefreshCheatTooltips()
	{
		m_UnlimitedBudgetButton.m_ToolTipText.m_RawLocalizationKey = (m_UnlimitedBudgetButton.IsOn() ? "UI_UNLIMITED_BUDGET_ON" : "UI_UNLIMITED_BUDGET_OFF");
		m_UnlimitedMaterialsButton.m_ToolTipText.m_RawLocalizationKey = (m_UnlimitedMaterialsButton.IsOn() ? "UI_UNLIMITED_MATERIAL_ON" : "UI_UNLIMITED_MATERIAL_OFF");
	}

	private void SelectLevel(string levelId)
	{
		CampaignLevel levelFromId = CampaignWorlds.m_Instance.GetLevelFromId(levelId);
		if (!levelFromId)
		{
			return;
		}
		foreach (FileSlot slot in m_FileLoader.m_Slots)
		{
			if (slot.m_FileName == levelFromId.m_Filename)
			{
				SetSelectedSlot(slot);
				break;
			}
		}
	}

	private void UpdateHeader()
	{
		m_WorldName.text = m_CampaignWorld.GetDisplayName();
		m_WorldDifficulty.text = Campaign.FormatDifficultyLabel(m_CampaignWorld.m_NumStars);
		m_WorldSubtitle.text = m_CampaignWorld.GetDescription();
		m_WorldSubtitle.gameObject.SetActive(!Game.IsRunningOnSteamDeck());
		m_Banner.Refresh();
		UpdateProgress();
	}

	private void UpdateProgress()
	{
		int numCompletedLevels = Campaign.m_CampaignProgress.GetNumCompletedLevels();
		int numLevels = Campaign.GetNumLevels();
		if (numLevels > 0)
		{
			m_ProgressText.transform.parent.gameObject.SetActive(value: true);
			m_ProgressText.text = $"{numCompletedLevels}/{numLevels}";
		}
		else
		{
			m_ProgressText.text = string.Empty;
		}
	}

	private void PopulateSlots(CampaignWorld world)
	{
		m_FileLoader.DestroySlots();
		m_SlotLevels.Clear();
		AddCampaignLevels(world.m_Levels);
		SetSelectedSlot(m_FileLoader.GetFirstSlot());
	}

	private void AddCampaignLevels(CampaignLevel[] levels)
	{
		int num = 0;
		for (int i = 0; i < levels.Length; i++)
		{
			CampaignLevel campaignLevel = levels[i];
			string displayName = Localize.Get(campaignLevel.m_DisplayNameLocID);
			FileSlot fileSlot = m_FileLoader.AddSlot(campaignLevel.m_Filename, 0L, displayName, SlotClickedCallback, null);
			if (fileSlot != null)
			{
				if (campaignLevel.IsTutorial())
				{
					fileSlot.m_Prefix.text = "T";
					num = -1;
				}
				else
				{
					fileSlot.m_Prefix.text = $"{i + 1 + num}";
				}
				fileSlot.m_Prefix.color = GameUI.m_Instance.m_GoldColor;
				CampaignLevelStatus levelStatus = Campaign.m_CampaignProgress.GetLevelStatus(campaignLevel.m_Id);
				fileSlot.SetStatusIcon(levelStatus);
				m_SlotLevels.Add(fileSlot, campaignLevel);
			}
		}
	}

	private void SlotClickedCallback(FileSlot slot)
	{
		if (!slot)
		{
			return;
		}
		CampaignLevel campaignLevel = (m_SlotLevels.ContainsKey(slot) ? m_SlotLevels[slot] : null);
		if (!(campaignLevel == null))
		{
			float num = Time.realtimeSinceStartup - m_LastClickTime;
			m_LastClickTime = Time.realtimeSinceStartup;
			if (slot == m_SelectedSlot && num < GameUI.DOUBLE_CLICK_THRESHOLD_SECONDS && !IsLocked(campaignLevel) && GameInput.GetActiveGameDevice() == GameDevice.KeyboardAndMouse)
			{
				OnPlay();
			}
			if (slot != m_SelectedSlot)
			{
				SetSelectedSlot(slot);
				InterfaceAudio.Play("ui_menu_select");
			}
		}
	}

	private void SetSelectedSlot(FileSlot slot)
	{
		if (slot == null)
		{
			m_ThumbnailButton.gameObject.SetActive(value: false);
			return;
		}
		m_ThumbnailButton.gameObject.SetActive(value: true);
		m_SelectedSlot = slot;
		m_SelectedSlotIndex = m_FileLoader.GetSlotIndex(slot);
		m_SelectedSlotSetOnFrameCount = Time.frameCount;
		m_FileLoader.SelectSlot(slot);
		CampaignLevel campaignLevel = (m_SlotLevels.ContainsKey(slot) ? m_SlotLevels[slot] : null);
		if (campaignLevel != null)
		{
			UpdateLevelStatusPanel(campaignLevel);
			UpdateLevelInfoPanel(campaignLevel);
			UpdateLevelThumbnail(campaignLevel);
		}
	}

	private void UpdateLevelStatusPanel(CampaignLevel level)
	{
		CampaignLevelStatus levelStatus = Campaign.m_CampaignProgress.GetLevelStatus(level.m_Id);
		m_PassStatus.SetActive(levelStatus == CampaignLevelStatus.PASS || levelStatus == CampaignLevelStatus.UNDER_BUDGET || levelStatus == CampaignLevelStatus.UNDER_BUDGET_NO_BREAKS);
		m_UnderBudgetStatus.SetActive(levelStatus == CampaignLevelStatus.UNDER_BUDGET || levelStatus == CampaignLevelStatus.UNDER_BUDGET_NO_BREAKS);
		m_UnbreakingStatus.SetActive(levelStatus == CampaignLevelStatus.UNDER_BUDGET_NO_BREAKS);
	}

	private void UpdateLevelInfoPanel(CampaignLevel level)
	{
		m_LevelName.text = level.GetPrefix() + " <#FFFFFF>" + level.GetLocalizedDisplayNameWithoutPrefix();
		m_LevelDescription.text = level.GetLocalizedDescription();
		SandboxLayoutData sandboxLayoutData = SandboxLayout.Load(Campaign.GetLevelsPath(level.m_Id), level.m_Filename);
		if (sandboxLayoutData == null)
		{
			m_LevelBudget.text = string.Empty;
			DisableAllMaterialIcons();
		}
		else
		{
			m_LevelBudget.text = Utils.FormatCash(sandboxLayoutData.m_Budget.m_CashBudget);
			EnableMaterialIcons(sandboxLayoutData.m_Budget);
			SetMaterialLimits(sandboxLayoutData.m_Budget);
		}
		UpdateCheatToggles(level);
	}

	private void UpdateCheatToggles(CampaignLevel level)
	{
		m_UnlimitedBudgetLocked.SetActive(!Campaign.m_CampaignProgress.HasCompletedLevelUnderBudget(level.m_Id));
		m_UnlimitedMaterialsLocked.SetActive(!Campaign.m_CampaignProgress.HasCompletedLevelUnderBudgetNoBreaks(level.m_Id));
		m_UnlimitedBudgetButton.gameObject.SetActive(!m_UnlimitedBudgetLocked.activeSelf);
		m_UnlimitedMaterialsButton.gameObject.SetActive(!m_UnlimitedMaterialsLocked.activeSelf);
		m_UnlimitedBudgetButton.TurnOn(level.m_UnlimitedBudget);
		m_UnlimitedMaterialsButton.TurnOn(level.m_UnlimitedMaterial);
	}

	private void DisableAllMaterialIcons()
	{
		m_RoadIcon.gameObject.SetActive(value: false);
		m_WoodIcon.gameObject.SetActive(value: false);
		m_SteelIcon.gameObject.SetActive(value: false);
		m_HydraulicIcon.gameObject.SetActive(value: false);
		m_RopeIcon.gameObject.SetActive(value: false);
		m_CableIcon.gameObject.SetActive(value: false);
		m_SpringIcon.gameObject.SetActive(value: false);
	}

	private void EnableMaterialIcons(BudgetProxy budgetProxy)
	{
		m_RoadIcon.gameObject.SetActive(budgetProxy.m_RoadBudget > 0);
		m_WoodIcon.gameObject.SetActive(budgetProxy.m_AllowWood && budgetProxy.m_WoodBudget > 0);
		m_SteelIcon.gameObject.SetActive(budgetProxy.m_AllowSteel && budgetProxy.m_SteelBudget > 0);
		m_HydraulicIcon.gameObject.SetActive(budgetProxy.m_AllowHydraulic && budgetProxy.m_HydraulicBudget > 0);
		m_RopeIcon.gameObject.SetActive(budgetProxy.m_AllowRope && budgetProxy.m_RopeBudget > 0);
		m_CableIcon.gameObject.SetActive(budgetProxy.m_AllowCable && budgetProxy.m_CableBudget > 0);
		m_SpringIcon.gameObject.SetActive(budgetProxy.m_AllowSpring && budgetProxy.m_SpringBudget > 0);
		m_PillarIcon.gameObject.SetActive(budgetProxy.m_AllowPillar && budgetProxy.m_PillarBudget > 0);
	}

	private void SetMaterialLimits(BudgetProxy budgetProxy)
	{
		MaterialLimit componentInChildren = m_RoadIcon.GetComponentInChildren<MaterialLimit>(includeInactive: true);
		componentInChildren.Set(budgetProxy.m_RoadBudget);
		componentInChildren.gameObject.SetActive(budgetProxy.m_RoadBudget != Budget.UNLIMITED_MATERIAL_BUDGET && budgetProxy.m_RoadBudget != 0);
		MaterialLimit componentInChildren2 = m_WoodIcon.GetComponentInChildren<MaterialLimit>(includeInactive: true);
		componentInChildren2.Set(budgetProxy.m_WoodBudget);
		componentInChildren2.gameObject.SetActive(budgetProxy.m_WoodBudget != Budget.UNLIMITED_MATERIAL_BUDGET && budgetProxy.m_WoodBudget != 0);
		MaterialLimit componentInChildren3 = m_SteelIcon.GetComponentInChildren<MaterialLimit>(includeInactive: true);
		componentInChildren3.Set(budgetProxy.m_SteelBudget);
		componentInChildren3.gameObject.SetActive(budgetProxy.m_SteelBudget != Budget.UNLIMITED_MATERIAL_BUDGET && budgetProxy.m_SteelBudget != 0);
		MaterialLimit componentInChildren4 = m_HydraulicIcon.GetComponentInChildren<MaterialLimit>(includeInactive: true);
		componentInChildren4.Set(budgetProxy.m_HydraulicBudget);
		componentInChildren4.gameObject.SetActive(budgetProxy.m_HydraulicBudget != Budget.UNLIMITED_MATERIAL_BUDGET && budgetProxy.m_HydraulicBudget != 0);
		MaterialLimit componentInChildren5 = m_RopeIcon.GetComponentInChildren<MaterialLimit>(includeInactive: true);
		componentInChildren5.Set(budgetProxy.m_RoadBudget);
		componentInChildren5.gameObject.SetActive(budgetProxy.m_RopeBudget != Budget.UNLIMITED_MATERIAL_BUDGET && budgetProxy.m_RopeBudget != 0);
		MaterialLimit componentInChildren6 = m_CableIcon.GetComponentInChildren<MaterialLimit>(includeInactive: true);
		componentInChildren6.Set(budgetProxy.m_CableBudget);
		componentInChildren6.gameObject.SetActive(budgetProxy.m_CableBudget != Budget.UNLIMITED_MATERIAL_BUDGET && budgetProxy.m_CableBudget != 0);
		MaterialLimit componentInChildren7 = m_SpringIcon.GetComponentInChildren<MaterialLimit>(includeInactive: true);
		componentInChildren7.Set(budgetProxy.m_SpringBudget);
		componentInChildren7.gameObject.SetActive(budgetProxy.m_SpringBudget != Budget.UNLIMITED_MATERIAL_BUDGET && budgetProxy.m_SpringBudget != 0);
		MaterialLimit componentInChildren8 = m_PillarIcon.GetComponentInChildren<MaterialLimit>(includeInactive: true);
		componentInChildren8.Set(budgetProxy.m_PillarBudget);
		componentInChildren8.gameObject.SetActive(budgetProxy.m_PillarBudget != Budget.UNLIMITED_MATERIAL_BUDGET && budgetProxy.m_PillarBudget != 0);
	}

	private void UpdateLevelThumbnail(CampaignLevel level)
	{
		if (!TryLoadAutoSaveThumb(level))
		{
			TryLoadPreviewThumb(level);
		}
	}

	private bool TryLoadAutoSaveThumb(CampaignLevel level)
	{
		string text = Path.Combine(Path.Combine(BridgeSaveSlots.GetSavePath(Profiles.GetActiveProfileName()), Path.GetFileNameWithoutExtension(level.m_Filename)), $"{BridgeSaveSlots.AUTOSAVE_SLOT_NAME}.slot");
		if (File.Exists(text))
		{
			BridgeSaveSlotData bridgeSaveSlotData = BridgeSaveSlots.Load(text);
			if (bridgeSaveSlotData != null && bridgeSaveSlotData.m_Thumb != null)
			{
				LoadThumbBytesForLevel(level, bridgeSaveSlotData.m_Thumb);
				return true;
			}
		}
		return false;
	}

	private bool TryLoadPreviewThumb(CampaignLevel level)
	{
		string path = Path.Combine(Application.streamingAssetsPath, "LevelThumbnails");
		string path2 = Path.GetFileNameWithoutExtension(level.m_Filename) + ".png";
		string text = Path.Combine(path, path2);
		try
		{
			byte[] array = Utils.ReadAllBytes(text);
			if (array == null || array.Length == 0)
			{
				return false;
			}
			LoadThumbBytesForLevel(level, array);
			return true;
		}
		catch (Exception ex)
		{
			Debug.LogFormat("Failed to read thumbnail from '{0}' due to exception '{1}'", text, ex.Message);
		}
		return false;
	}

	private void LoadThumbBytesForLevel(CampaignLevel level, byte[] bytes)
	{
		Texture2D texture2D = null;
		if (m_Thumbs.ContainsKey(level))
		{
			texture2D = m_Thumbs[level];
		}
		else
		{
			texture2D = new Texture2D(THUMB_WIDTH, THUMB_HEIGHT, TextureFormat.RGB24, mipChain: true);
			m_Thumbs.Add(level, texture2D);
		}
		if (texture2D.LoadImage(bytes))
		{
			m_RawImage.texture = texture2D;
			m_RawImage.uvRect = new Rect(m_RawImage.uvRect.x, m_RawImage.uvRect.y, m_RawImage.uvRect.width, 0.999f);
		}
	}

	public void OnCancel()
	{
		InterfaceAudio.Play("ui_window_close");
		Close();
	}

	public void OnPlay()
	{
		if (m_SelectedSlot == null)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		CampaignLevel levelForSlot = GetLevelForSlot(m_SelectedSlot);
		if (levelForSlot == null)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		InterfaceAudio.Play("ui_menu_select");
		BridgeCheat.Clear();
		BridgeCheat.m_ForceUnlimitedBudget = levelForSlot.m_UnlimitedBudget;
		BridgeCheat.m_ForceUnlimitedMaterial = levelForSlot.m_UnlimitedMaterial;
		Campaign.m_LevelBeingPreloaded = levelForSlot;
		GameStatePreloadingAssets.PreloadLevel(levelForSlot.GetLayoutPath(), null, Campaign.DonePreloadFromMainMenu);
		GameUI.m_Instance.m_Campaign.Close();
	}

	private void CloseCampaignUI()
	{
		if (GameUI.m_Instance.m_Campaign.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_Campaign.Close();
		}
	}

	private void OnThumbnail()
	{
		float num = Time.realtimeSinceStartup - m_LastClickTime;
		m_LastClickTime = Time.realtimeSinceStartup;
		if (num < GameUI.DOUBLE_CLICK_THRESHOLD_SECONDS && !IsLocked(m_SelectedSlot) && GameInput.GetActiveGameDevice() == GameDevice.KeyboardAndMouse)
		{
			OnPlay();
		}
	}

	private bool IgnoreKeyboardInput()
	{
		if (uConsole.IsOn())
		{
			return true;
		}
		return false;
	}

	private void ProcessDpadUp()
	{
		ScrollUp();
		ForceGamepadCursorToSelecctedSlot();
		GameUI.m_NextAutoScrollTime = Time.unscaledTime + GameUI.AUTOSCROLL_START_DELAY;
	}

	private void ProcessDpadDown()
	{
		ScrollDown();
		ForceGamepadCursorToSelecctedSlot();
		GameUI.m_NextAutoScrollTime = Time.unscaledTime + GameUI.AUTOSCROLL_START_DELAY;
	}

	private void ForceGamepadCursorToSelecctedSlot()
	{
		if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad && m_SelectedSlotIndex != -1)
		{
			FileSlot fileSlot = m_FileLoader.FindSlotByIndex(m_SelectedSlotIndex);
			if (fileSlot != null && fileSlot.m_Prefix != null)
			{
				GameInput.SetVirtualMousePosition(fileSlot.m_Prefix.transform.position);
			}
		}
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
		if (GameInput.GetMouseButtonJustPressed(0) && !GameUI.PointerOver(typeof(Panel_Campaign)))
		{
			OnCancel();
		}
		if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && !IsLocked(m_SelectedSlot))
		{
			OnPlay();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH))
		{
			OnPlay();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.SOUTH) && m_SelectedSlot != null)
		{
			FileSlot fileSlotUnderPointer = GameUI.GetFileSlotUnderPointer();
			if (m_SelectedSlot == fileSlotUnderPointer && !IsLocked(m_SelectedSlot) && Time.frameCount > m_SelectedSlotSetOnFrameCount)
			{
				OnPlay();
			}
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
		{
			OnCancel();
		}
		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
		{
			ScrollUp();
			GameUI.m_NextAutoScrollTime = Time.unscaledTime + GameUI.AUTOSCROLL_START_DELAY;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
		{
			ScrollDown();
			GameUI.m_NextAutoScrollTime = Time.unscaledTime + GameUI.AUTOSCROLL_START_DELAY;
		}
		if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && Time.unscaledTime > GameUI.m_NextAutoScrollTime)
		{
			ScrollUp();
			GameUI.m_NextAutoScrollTime = Time.unscaledTime + GameUI.AUTOSCROLL_DELAY;
		}
		if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && Time.unscaledTime > GameUI.m_NextAutoScrollTime)
		{
			ScrollDown();
			GameUI.m_NextAutoScrollTime = Time.unscaledTime + GameUI.AUTOSCROLL_DELAY;
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_DOWN) || GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_DOWN))
		{
			ProcessDpadDown();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_UP) || GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_UP))
		{
			ProcessDpadUp();
		}
	}

	private void ScrollDown()
	{
		m_SelectedSlotIndex++;
		if (m_SelectedSlotIndex >= m_FileLoader.NumSlots())
		{
			m_SelectedSlotIndex = 0;
		}
		SetSelectedSlot(m_FileLoader.FindSlotByIndex(m_SelectedSlotIndex));
		InterfaceAudio.Play("ui_menu_select");
	}

	private void ScrollUp()
	{
		m_SelectedSlotIndex--;
		if (m_SelectedSlotIndex < 0)
		{
			m_SelectedSlotIndex = m_FileLoader.NumSlots() - 1;
		}
		SetSelectedSlot(m_FileLoader.FindSlotByIndex(m_SelectedSlotIndex));
		InterfaceAudio.Play("ui_menu_select");
	}

	private CampaignLevel GetLevelForSlot(FileSlot slot)
	{
		if (slot == null)
		{
			return null;
		}
		if (!m_SlotLevels.ContainsKey(slot))
		{
			return null;
		}
		return m_SlotLevels[slot];
	}

	private void OnUnlimitedBudgetToggle()
	{
		InterfaceAudio.Play("ui_menu_select");
		m_UnlimitedBudgetButton.Toggle();
		CampaignLevel levelForSlot = GetLevelForSlot(m_SelectedSlot);
		if (levelForSlot != null)
		{
			levelForSlot.m_UnlimitedBudget = m_UnlimitedBudgetButton.IsOn();
		}
	}

	private void OnUnlimitedMaterialToggle()
	{
		InterfaceAudio.Play("ui_menu_select");
		m_UnlimitedMaterialsButton.Toggle();
		CampaignLevel levelForSlot = GetLevelForSlot(m_SelectedSlot);
		if (levelForSlot != null)
		{
			levelForSlot.m_UnlimitedMaterial = m_UnlimitedMaterialsButton.IsOn();
		}
	}

	private bool IsLocked(string levelId)
	{
		CampaignWorld worldWithLevelId = CampaignWorlds.m_Instance.GetWorldWithLevelId(levelId);
		if (worldWithLevelId == null)
		{
			return true;
		}
		return worldWithLevelId.IsLocked();
	}

	private bool IsLocked(CampaignLevel level)
	{
		if (!(level != null))
		{
			return true;
		}
		return IsLocked(level.m_Id);
	}

	private bool IsLocked(FileSlot slot)
	{
		CampaignLevel levelForSlot = GetLevelForSlot(slot);
		if (!(levelForSlot != null))
		{
			return true;
		}
		return IsLocked(levelForSlot.m_Id);
	}

	private void ShowGamepadLegend()
	{
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.NORTH, Localize.Get("UI_PLAY_LEVEL"), GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
	}
}

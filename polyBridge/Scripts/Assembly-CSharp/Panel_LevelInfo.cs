using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel_LevelInfo : MonoBehaviour
{
	[Header("Panel")]
	public RectTransform m_Panel;

	public Panel_LevelInfoLite m_LevelInfoLite;

	[Header("Header")]
	public GameObject m_WorldBanner;

	public GameObject m_WorldBannerWorkshop;

	public PanelResizeHorizontal m_WorldBannerResizeHorizontal;

	public PanelResizeHorizontal m_WorldBannerWorkshopResizeHorizontal;

	public TextMeshProUGUI m_Title;

	public TextMeshProUGUI m_TitleWorkshop;

	public TextMeshProUGUI m_AuthorWorkshop;

	public Button m_Cancel;

	[Header("Footer")]
	public RectTransform m_BuildButtonRectTransform;

	public TextMeshProUGUI m_BuildButtonText;

	public TextMeshProUGUI m_BuildButtonText_OK;

	public RectTransform m_BuildButtonArrowRectTransform;

	public Button m_LevelID_Button;

	public TextMeshProUGUI m_LevelID;

	[Header("Budget")]
	public TextMeshProUGUI m_Budget;

	[Header("Description")]
	public TextMeshProUGUI m_Description;

	public RectTransform m_DescriptionPanel;

	[Header("Material Icons")]
	public Image m_RoadIcon;

	public Image m_WoodIcon;

	public Image m_SteelIcon;

	public Image m_HydraulicIcon;

	public Image m_RopeIcon;

	public Image m_CableIcon;

	public Image m_SpringIcon;

	public Image m_PillarIcon;

	private const int DEFAULT_DESCRIPTION_WIDTH = 250;

	private const int DEFAULT_PANEL_WIDTH = 520;

	private const int DEFAULT_PANEL_HEIGHT = 310;

	private const int MIN_BUILD_BUTTON_WIDTH_FOR_OK = 120;

	private void Start()
	{
		m_LevelID_Button.onClick.AddListener(OnLevelID);
		m_Cancel.onClick.AddListener(Close);
	}

	private void OnEnable()
	{
		ActivePanels.Add(base.gameObject);
		m_Panel.transform.localScale = (Game.IsRunningOnSteamDeck() ? new Vector3(1.1f, 1.1f, 1f) : Vector3.one);
		UpdateBuildButton();
		SetTitle();
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, (GameStateManager.GetState() == GameState.SANDBOX) ? Localize.Get("UI_OK") : Localize.Get("UI_BUILD"));
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
		GameUI.m_Instance.m_GamepadLegend.HideButtons();
	}

	private void Update()
	{
		ProcessInput();
		UpdatePanelDimensions();
		UpdateBuildButton();
	}

	public void Open()
	{
		OpenInternal(animateOpen: true);
		m_LevelInfoLite.UpdatePanelDimensions();
		UpdatePanelDimensions();
		UpdateLevelID();
	}

	public void OnLevelID()
	{
		Utils.OpenLocalPath(Path.Combine(BridgeSaveSlots.GetSavePath(Profiles.GetActiveProfileName()), Game.GetLevelId()));
	}

	public void Close()
	{
		if (base.gameObject.activeInHierarchy)
		{
			base.gameObject.SetActive(value: false);
			InterfaceAudio.Play("ui_window_close");
		}
	}

	public bool PointerIsOverPanel()
	{
		if (!EventSystem.current)
		{
			return false;
		}
		PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
		pointerEventData.position = GameInput.GetMousePosition();
		List<RaycastResult> list = new List<RaycastResult>();
		EventSystem.current.RaycastAll(pointerEventData, list);
		foreach (RaycastResult item in list)
		{
			if ((bool)item.gameObject.GetComponentInParent<Panel_LevelInfo>())
			{
				return true;
			}
		}
		return false;
	}

	private void SetTitle()
	{
		m_Title.text = Game.GetLevelTitleWithoutPrefix();
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN && Campaign.m_CurrentLevel != null)
		{
			m_WorldBanner.SetActive(value: true);
			m_WorldBannerWorkshop.SetActive(value: false);
			m_Title.text = Campaign.m_CurrentLevel.GetFullNameFormatted();
		}
		else if (GameManager.GetGameMode() == GameMode.WORKSHOP && Workshop.m_LastPlayedWorkshopItem != null)
		{
			m_WorldBanner.SetActive(value: false);
			m_WorldBannerWorkshop.SetActive(value: true);
			if (WorkshopCampaigns.Get(WorkshopCampaigns.m_ActiveWorkshopCampaignModId) != null)
			{
				m_TitleWorkshop.text = m_Title.text;
			}
			else
			{
				m_TitleWorkshop.text = GameUI.MarkupForGold(m_Title.text);
			}
			string id = Workshop.m_LastPlayedWorkshopItem.GetId();
			if (WeeklyChallenges.IsAWeeklyChallenge(id))
			{
				string originalCreatorDisplayName = WeeklyChallenges.GetOriginalCreatorDisplayName(id);
				m_AuthorWorkshop.text = (string.IsNullOrEmpty(originalCreatorDisplayName) ? string.Empty : Localize.Get("UI_WORKSHOP_BY", originalCreatorDisplayName));
			}
			else
			{
				m_AuthorWorkshop.text = Localize.Get("UI_WORKSHOP_BY", Workshop.m_LastPlayedWorkshopItem.GetCreatorName());
			}
		}
		else if (GameManager.GetGameMode() == GameMode.SANDBOX)
		{
			m_WorldBanner.SetActive(value: true);
			m_WorldBannerWorkshop.SetActive(value: false);
			m_Title.text = (string.IsNullOrEmpty(SandboxSettings.m_Title) ? Localize.Get("UI_UNTITLED_LAYOUT") : SandboxSettings.m_Title);
		}
		else
		{
			m_WorldBanner.SetActive(value: true);
			m_WorldBannerWorkshop.SetActive(value: false);
			m_Title.text = Localize.Get("UI_UNTITLED_LAYOUT");
		}
		if (m_WorldBannerResizeHorizontal.gameObject.activeInHierarchy)
		{
			m_Title.ForceMeshUpdate();
			m_WorldBannerResizeHorizontal.ForceUpdate();
		}
		if (m_WorldBannerWorkshopResizeHorizontal.gameObject.activeInHierarchy)
		{
			m_TitleWorkshop.ForceMeshUpdate();
			m_WorldBannerWorkshopResizeHorizontal.ForceUpdate();
		}
	}

	private void SetBudget()
	{
		m_Budget.text = Utils.FormatCash(Budget.m_CashBudget);
	}

	private void SetDescription()
	{
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN && Campaign.m_CurrentLevel != null && !string.IsNullOrEmpty(Campaign.m_CurrentLevel.GetLocalizedDescription()))
		{
			GameUI.SetAndEnableText(m_Description, Campaign.m_CurrentLevel.GetLocalizedDescription());
		}
		else if (GameManager.GetGameMode() == GameMode.WORKSHOP && Workshop.m_LastPlayedWorkshopItem != null && WeeklyChallenges.IsAWeeklyChallenge(Workshop.m_LastPlayedWorkshopItem.GetId()))
		{
			string descriptionFromEncodedDescription = WeeklyChallenges.GetDescriptionFromEncodedDescription(Workshop.m_LastPlayedWorkshopItem.GetDescription());
			GameUI.SetAndEnableText(m_Description, descriptionFromEncodedDescription);
		}
		else
		{
			GameUI.SetAndEnableText(m_Description, SandboxSettings.m_Description);
		}
	}

	private void SetMaterials()
	{
		EnableMaterialIcons();
		SetMaterialLimits(Sandbox.m_CurrentLayoutData.m_Budget);
	}

	private void EnableMaterialIcons()
	{
		m_RoadIcon.gameObject.SetActive(Budget.m_RoadBudget > 0);
		m_WoodIcon.gameObject.SetActive(Budget.m_AllowWood && Budget.m_WoodBudget > 0);
		m_SteelIcon.gameObject.SetActive(Budget.m_AllowSteel && Budget.m_SteelBudget > 0);
		m_HydraulicIcon.gameObject.SetActive(Budget.m_AllowHydraulic && Budget.m_HydraulicBudget > 0);
		m_RopeIcon.gameObject.SetActive(Budget.m_AllowRope && Budget.m_RopeBudget > 0);
		m_CableIcon.gameObject.SetActive(Budget.m_AllowCable && Budget.m_CableBudget > 0);
		m_SpringIcon.gameObject.SetActive(Budget.m_AllowSpring && Budget.m_SpringBudget > 0);
		m_PillarIcon.gameObject.SetActive(Budget.m_AllowPillar && Budget.m_PillarBudget > 0);
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
		componentInChildren5.Set(budgetProxy.m_RopeBudget);
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

	private void ProcessInput()
	{
		if (GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || GameInput.JustPressed(BindingType.START_SIM))
		{
			Close();
		}
		else if (GameInput.JustPressed(BindingType.LEVEL_INFO))
		{
			Close();
		}
		else if (GamepadManager.ButtonJustPressed(GamepadButtonType.SOUTH) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
		{
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
			{
				Game.ForceIgnoreNextSelection();
			}
			Close();
		}
	}

	private void UpdatePanelDimensions()
	{
		float num = Mathf.Abs(m_LevelInfoLite.m_Panel.sizeDelta.x - (float)m_LevelInfoLite.m_MinPanelWidth);
		float num2 = Mathf.Abs(m_LevelInfoLite.m_Panel.sizeDelta.y - (float)m_LevelInfoLite.m_MinPanelHeight);
		m_Panel.sizeDelta = new Vector2(520f + num, 310f + num2);
		m_DescriptionPanel.sizeDelta = new Vector2(250f + num, m_DescriptionPanel.sizeDelta.y);
	}

	private void UpdateBuildButton()
	{
		if (GameStateManager.GetState() == GameState.SANDBOX)
		{
			m_BuildButtonText_OK.gameObject.SetActive(value: true);
			m_BuildButtonText.gameObject.SetActive(value: false);
			m_BuildButtonArrowRectTransform.gameObject.SetActive(value: false);
			float x = Mathf.Max(120f, m_BuildButtonText_OK.preferredWidth);
			m_BuildButtonRectTransform.sizeDelta = new Vector2(x, m_BuildButtonRectTransform.sizeDelta.y);
		}
		else
		{
			m_BuildButtonText_OK.gameObject.SetActive(value: false);
			m_BuildButtonText.gameObject.SetActive(value: true);
			m_BuildButtonArrowRectTransform.gameObject.SetActive(value: true);
			float x2 = m_BuildButtonText.preferredWidth + m_BuildButtonArrowRectTransform.sizeDelta.x + 50f;
			m_BuildButtonRectTransform.sizeDelta = new Vector2(x2, m_BuildButtonRectTransform.sizeDelta.y);
		}
	}

	private void UpdateLevelID()
	{
		if (Game.m_AllowShowLevelID && GameManager.GameModeIsCampaignOrWorkshop())
		{
			m_LevelID_Button.gameObject.SetActive(value: true);
			m_LevelID.text = "ID: " + Game.GetLevelId();
		}
		else
		{
			m_LevelID_Button.gameObject.SetActive(value: false);
		}
	}

	private void OpenInternal(bool animateOpen)
	{
		if (!base.gameObject.activeInHierarchy)
		{
			base.gameObject.SetActive(value: true);
			SetTitle();
			SetBudget();
			SetMaterials();
			SetDescription();
			InterfaceAudio.Play("ui_window_open");
		}
	}
}

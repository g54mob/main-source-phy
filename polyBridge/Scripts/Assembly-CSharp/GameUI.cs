using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vectrosity;

public class GameUI : MonoBehaviour
{
	[Header("System")]
	public VirtualMouseUI m_VirtualMouseUI;

	public GraphicRaycaster m_Raycaster;

	public CanvasScaler m_CanvasScaler;

	public RectTransform m_RectTransform;

	public Panel_ModsScreenUI m_ModsScreenUI;

	[Header("Vectrosity")]
	public Material m_UnmaskedVectrosityMaterial;

	[Header("Gamepad")]
	public GamepadLegend m_GamepadLegend;

	public GamepadIconSets m_GamepadIconSets;

	public RectTransform m_GamepadSafeArea;

	[Header("ToolTips")]
	public ToolTip m_BuildToolTip;

	public ToolTip m_ToolTip;

	public ToolTip m_TraceLineToolTip;

	public BudgetToolTip m_BudgetToolTip;

	public VehicleToolTip m_VehicleToolTip;

	public CustomShapeToolTip m_CustomShapeToolTip;

	public PointerToolTip m_PointerToolTip;

	[Header("Main Menu")]
	public Panel_MainMenuNew m_MainMenuNew;

	public Panel_WeeklyChallenges m_WeeklyChallenges;

	public Panel_Workshop m_Workshop;

	public Panel_Gallery m_Gallery;

	public Panel_Campaign m_Campaign;

	public Panel_Credits m_Credits;

	[Header("Campaign Menu")]
	public Panel_Achievements m_Achievements;

	public Panel_Leaderboards m_LeaderboardsPanel;

	public Panel_MyRankings m_MyRankings;

	[Header("Sequencing Panels")]
	public Panel_PauseMenu m_PauseMenu;

	public Panel_LevelComplete m_LevelComplete;

	public Panel_LevelFailed m_LevelFailed;

	public Panel_ShareReplay m_ShareReplay;

	public Panel_ShareReplayStatus m_ShareReplayStatus;

	[Header("Tool Bars")]
	public Panel_BottomBar m_BottomBar;

	public Panel_TopBar m_TopBar;

	public Panel_BuildToolBar m_BuildToolBar;

	public Panel_SimToolBar m_SimToolBar;

	[Header("Common Panels")]
	public Panel_Help m_Help;

	public Panel_GamepadHelp m_GamepadHelp;

	public Panel_LevelInfo m_LevelInfo;

	public Panel_LevelInfoLite m_LevelInfoLite;

	public Panel_ProfileSelect m_ProfileSelect;

	public Panel_Settings m_Settings;

	public Panel_CampaignTutorial m_CampaignTutorial;

	public Panel_Recenter m_Recenter;

	public GameObject m_PreloadingObject;

	[Header("Build Panels")]
	public Panel_SelectionToolbar m_Selection;

	public Panel_ClipboardToolbar m_Clipboard;

	public Panel_TraceTool m_TraceTool;

	public Panel_LoadBridge m_LoadBridge;

	public Panel_SaveBridge m_SaveBridge;

	public Panel_HydraulicsController m_HydraulicsController;

	[Header("PolyTwitch Panels")]
	public Panel_PolyTwitchMain m_PolyTwitchMain;

	public Panel_PolyTwitchBridge m_PolyTwitchBridge;

	[Header("Sim Panels")]
	public Panel_LiveStress m_LiveStress;

	[Header("Sandbox Panels")]
	public Panel_SandboxMenu m_SandboxMenu;

	public Panel_SandboxResources m_SandboxResources;

	public Panel_SandboxTheme m_SandboxTheme;

	public Panel_SandboxModifiers m_SandboxModifiers;

	public Panel_SandboxTitleAndDescription m_SandboxTitleAndDescription;

	public Panel_SandboxCreateDecorObjects m_SandboxCreateDecorObjects;

	public Panel_SandboxCreateVehicles m_SandboxCreateVehicles;

	public Panel_SandboxCreateObjects m_SandboxCreateObjects;

	public Panel_SandboxMultiSelect m_SandboxMultiSelect;

	public Panel_SandboxEditAnchor m_SandboxEditAnchor;

	public Panel_SandboxEditHydraulicsPhase m_SandboxEditHydraulicsPhase;

	public Panel_SandboxEditCheckpoint m_SandboxEditCheckpoint;

	public Panel_SandboxEditBuildZone m_SandboxEditBuildZone;

	public Panel_SandboxEditCustomShape m_SandboxEditCustomShape;

	public Panel_SandboxEditCustomShapeTools m_SandboxEditCustomShapeTools;

	public Panel_SandboxEditFlyingObject m_SandboxEditFlyingObject;

	public Panel_SandboxEditPlatform m_SandboxEditPlatform;

	public Panel_SandboxEditRamp m_SandboxEditRamp;

	public Panel_SandboxEditRock m_SandboxEditRock;

	public Panel_SandboxEditPillar m_SandboxEditPillar;

	public Panel_SandboxEditDecor m_SandboxEditDecor;

	public Panel_SandboxEditTerrain m_SandboxEditTerrain;

	public Panel_SandboxEditVehicle m_SandboxEditVehicle;

	public Panel_SandboxEditVehicleRestartPhase m_SandboxEditVehicleRestartPhase;

	public Panel_SandboxEditVehicleStopTrigger m_SandboxEditVehicleStopTrigger;

	public Panel_SandboxEditWater m_SandboxEditWater;

	public Panel_WorkshopSubmit m_WorkshopSubmit;

	public Panel_SandboxEditZedAxisVehicle m_SandboxEditZedAxisVehicle;

	public Panel_LoadSandboxLayout m_LoadSandboxLayout;

	public Panel_SaveSandboxLayout m_SaveSandboxLayout;

	public Panel_CustomShapesLibrary m_CustomShapesLibrary;

	[Header("Event Editor")]
	public Panel_EventEditor m_EventEditor;

	[Header("Popups")]
	public Panel_CustomShapeReset m_CustomShapeReset;

	public Panel_Status m_Status;

	public Panel_PopUpBinding m_PopUpBinding;

	public Panel_PopUpBindingConflict m_PopUpBindingConflict;

	public Panel_PopUpMessage m_PopUpMessage;

	public Panel_PopUpInputField m_PopUpInputField;

	public Panel_PopUpTwoChoices m_PopUpTwoChoices;

	public Panel_PopUpVideoSettingsConfirm m_PopUpVideoSettingsConfirm;

	public Panel_AchievementPopup m_AchievementPopup;

	public Panel_ModsRequiredPopup m_ModsRequiredPopup;

	public GameObject m_ScreenDuck;

	[Header("Bridge Colors")]
	public Color m_JointColor;

	public Color m_JointGreyScaleColor;

	public Color m_JointOutlineColor;

	public Color m_StaticJointColor;

	public Color m_StaticJointOutlineColor;

	public Color m_JointHightlightColor;

	public Color m_PrebuiltJointHightlightColor;

	public Color m_StaticJointHightlightColor;

	public Color m_JointOutlineHightlightColor;

	public Color m_StaticJointOutlineHighlightColor;

	public Color m_SplitJointColor;

	public Color m_SplitJointHighlightColor;

	public Color m_EdgeSelectColor;

	public Color m_PillarSelectGoldColor;

	public Color m_EdgeHighlightColor;

	public Color m_GroupSelectionBoxColor;

	public Color m_GroupSelectionBoxOutlineColor;

	public Color m_InputFieldSelectColor;

	public Color m_CustomShapeDeleteColor;

	public Color m_CustomShapeAddColor;

	public Color m_CustomShapeDisabledColor;

	public Color m_DynamicAnchorColor;

	public Color m_PrebuiltColor;

	public Color m_NoBuildAnchorColor;

	public Color m_Split3_Color_A;

	public Color m_Split3_Color_B;

	public Color m_Split3_Color_C;

	[Header("UI Colors")]
	public Color m_TabDefaultColor;

	public Color m_TabHoverColor;

	public Color m_TabSelectedColor;

	public Color m_BlueprintBackgroundColor;

	public Color m_SimModeBackgroundColor;

	public Color m_BuildModeBackgroundColor;

	public Color m_SimModeWaterColor;

	public Color m_YellowTextColor;

	public Color m_GreenTextColor;

	public Color m_RedTextColor;

	public Color m_MenuSlotColor;

	public Color m_MenuSlotHoverColor;

	public Color m_StatusIconYellow;

	public Color m_StatusIconGreen;

	public Color m_StatusIconBlue;

	public Color m_LeaderboardDefaultHighlightColor;

	public Color m_LeaderboardSelfHighlightColor;

	[Header("ToolBar Colors")]
	public Color m_ToolBarBackgroundColor;

	public Color m_ToolBarForegroundColor;

	public Color m_ToolBarHighlightColor;

	[Header("New UI Colors")]
	public Color m_HighlightedIconColor;

	public Color m_DuckedIconColor;

	public Color m_TabActiveColor;

	public Color m_TabInActiveColor;

	public Color m_TabOutlineActiveColor;

	public Color m_TabOutlineInActiveColor;

	public Color m_RolloutBackgroundColor;

	public Color m_GoldColor;

	[Header("Event Stage Colors")]
	public Color m_EventStageBackgroundColor;

	public Color m_EventStageForegroundColor;

	public Color m_EventStageHeaderColor;

	public Sprite m_EventStageIconBackgroundSprite;

	[Header("Event Stage Blueprint Colors")]
	public Color m_EventStageBackgroundColor_Blueprint;

	public Color m_EventStageForegroundColor_Blueprint;

	public Color m_EventStageHeaderColor_Blueprint;

	public Sprite m_EventStageIconBackgroundSprite_Blueprint;

	[Header("Tooltip Colors")]
	public Color m_TooltipColor;

	public Color m_TooltipOutlineColor;

	public Color m_RulerTextColor;

	public Color m_RulerTextOutlineColor;

	[Header("Budget")]
	public Color m_BudgetTextGreen;

	public Color m_BudgetTextRed;

	public Color m_OverBudgetBackgroundColor;

	public Color m_OverBudgetForegroundColor;

	[Header("Placement")]
	public Color m_PlacementLineColor;

	public Color m_PlacementLineErrorColor;

	public Texture2D m_PlacementLineTexture;

	public float m_PlacementLineWidth;

	public float m_PlacementLineAnimSpeed;

	public GameObject m_PlacementDot;

	public float m_SelectionCircleDotsRotateDegreesPerSecond;

	[Header("Placement Crosshairs")]
	public Color m_PlacementCrosshairsColor;

	public float m_PlacementCrosshairsLineWidth;

	[Header("Pointers")]
	public Texture2D m_PointerMoveTexture;

	public Texture2D m_PointerNormalTexture;

	public Texture2D m_PointerSandboxSelectToggleTexture;

	public Texture2D m_PointerSelectTexture;

	public Texture2D m_PointerSelectToggleTexture;

	public Texture2D m_PointerEraseTexture;

	[Header("Foundations")]
	public Texture2D m_ChalkArrow2D;

	public Texture2D m_ChalkLine2D;

	[Header("Blueprint")]
	public GameObject m_RulerText;

	public Color m_WorldBoundsColor;

	public Color m_BuildZoneColor;

	public Color m_OutlineBuildZoneColor;

	public float m_OutlineTextureScale;

	[Header("Sandbox Outlines")]
	public Texture m_OutlineTextureSandbox;

	public Texture m_OutlineTextureDashedSandbox;

	public Color m_OutlineColorSandbox;

	public Color m_OutlineHoverColorSandbox;

	public Color m_OutlineSelectedColorSandbox;

	public float m_OutlineWidthSandbox;

	[Header("Build Mode Outlines")]
	public Texture m_OutlineTextureBuildMode;

	public Texture m_OutlineTextureDashedBuildMode;

	public Color m_OutlineColorBuildMode;

	public float m_OutlineWidthBuildMode;

	[Header("Object Labels")]
	public Color m_LabelTextColor;

	public Color m_LabelTextColorDucked;

	public Color m_LabelBackgroundColor;

	public Color m_LabelBackgroundColorDucked;

	public Color m_LabelOutlineColor;

	public Color m_LabelOutlineColorDucked;

	[Header("Replay")]
	public Texture2D m_Watermark;

	[Header("Avatar")]
	public Sprite m_DefaultAvatarSprite;

	[Header("Version")]
	public TextMeshProUGUI m_Version;

	public static Vehicle m_VehicleWithActiveTooltip;

	public static CustomShape m_CustomShapeWithActiveTooltip;

	public static GameUI m_Instance;

	public static string GOLD_COLOR_HEX_TAG = "<#FCAB0C>";

	public static string WHITE_COLOR_HEX_TAG = "<#FFFFFF>";

	public static readonly float DOUBLE_CLICK_THRESHOLD_SECONDS = 0.5f;

	public static readonly float AUTOSCROLL_START_DELAY = 0.4f;

	public static readonly float AUTOSCROLL_DELAY = 0.075f;

	public static readonly float KEY_REPEAT_START_DELAY_SECONDS = 0.4f;

	public static readonly float KEY_REPEAT_INTERVAL_SECONDS = 0.15f;

	private static readonly float MIN_SCALE_FACTOR_RELATIVE = 0.4f;

	private static readonly float MAX_SCALE_FACTOR_RELATIVE = 1.1f;

	public static TextEditor m_TextEditor = new TextEditor();

	public static bool m_DisableHud;

	public static float m_NextAutoScrollTime;

	private static PointerMode m_PointerMode;

	private static bool m_DisableTooltip;

	private static PointerEventData m_PointerEventData;

	private static List<RaycastResult> m_CachedRaycastResults = new List<RaycastResult>();

	private static bool m_RepopulateRaycastResults;

	private static int m_NumDucksActive;

	public static readonly float REFERENCE_RESOLUTION_Y_DEFAULT = 780f;

	public static readonly float REFERENCE_RESOLUTION_Y_STEAMDECK = 725f;

	public static void Init()
	{
		m_PointerEventData = new PointerEventData(EventSystem.current);
		m_Instance.StartManual();
		m_RepopulateRaycastResults = true;
	}

	private void Awake()
	{
		m_Instance = this;
	}

	private void StartManual()
	{
		SetPointerNormal();
		VectorLine.SetCamera3D(Cameras.MainCamera());
		m_WorkshopSubmit.gameObject.SetActive(value: false);
		m_WorkshopSubmit.m_ConfirmBudgetPanel.gameObject.SetActive(value: false);
		m_Help.gameObject.SetActive(value: false);
		m_GamepadHelp.gameObject.SetActive(value: false);
		m_LevelInfo.gameObject.SetActive(value: false);
		m_LevelInfoLite.gameObject.SetActive(value: false);
		m_CampaignTutorial.gameObject.SetActive(value: false);
		m_CustomShapesLibrary.gameObject.SetActive(value: false);
		m_TopBar.m_MessageTopLeft.gameObject.SetActive(value: false);
		m_TopBar.m_MessageTopCenter.gameObject.SetActive(value: false);
		m_TopBar.m_CostAndBudget.SetActive(value: true);
		m_BuildToolBar.gameObject.SetActive(value: false);
		m_SimToolBar.gameObject.SetActive(value: false);
		m_BuildToolBar.m_AutoDrawButton.transform.parent.gameObject.SetActive(value: false);
		m_HydraulicsController.gameObject.SetActive(value: false);
		m_ToolTip.gameObject.SetActive(value: false);
		m_BuildToolTip.gameObject.SetActive(value: false);
		m_PointerToolTip.gameObject.SetActive(value: false);
		m_BudgetToolTip.gameObject.SetActive(value: false);
		m_VehicleToolTip.gameObject.SetActive(value: false);
		m_CustomShapeToolTip.gameObject.SetActive(value: false);
		m_ScreenDuck.SetActive(value: false);
		m_PopUpBinding.gameObject.SetActive(value: false);
		m_PopUpBindingConflict.gameObject.SetActive(value: false);
		m_PopUpMessage.gameObject.SetActive(value: false);
		m_PopUpInputField.gameObject.SetActive(value: false);
		m_PopUpTwoChoices.gameObject.SetActive(value: false);
		m_PopUpVideoSettingsConfirm.gameObject.SetActive(value: false);
		m_AchievementPopup.gameObject.SetActive(value: false);
		m_ModsRequiredPopup.gameObject.SetActive(value: false);
		m_CustomShapeReset.gameObject.SetActive(value: false);
		m_Status.gameObject.SetActive(value: false);
		m_LevelComplete.gameObject.SetActive(value: false);
		m_LevelFailed.gameObject.SetActive(value: false);
		m_ShareReplay.gameObject.SetActive(value: false);
		m_MainMenuNew.gameObject.SetActive(value: false);
		m_Campaign.gameObject.SetActive(value: false);
		m_Credits.gameObject.SetActive(value: false);
		m_Achievements.gameObject.SetActive(value: false);
		m_LeaderboardsPanel.gameObject.SetActive(value: false);
		m_MyRankings.gameObject.SetActive(value: false);
		m_PauseMenu.gameObject.SetActive(value: false);
		m_ProfileSelect.gameObject.SetActive(value: false);
		m_Settings.gameObject.SetActive(value: false);
		m_WeeklyChallenges.gameObject.SetActive(value: false);
		m_Gallery.gameObject.SetActive(value: false);
		m_Gallery.m_GalleryVideo.gameObject.SetActive(value: false);
		m_Workshop.gameObject.SetActive(value: false);
		m_Workshop.m_WorkshopItemPanel.gameObject.SetActive(value: false);
		m_Workshop.m_WorkshopCampaignPanel.gameObject.SetActive(value: false);
		m_Settings.m_ControlsPanel.Init();
		m_Settings.m_TwitchPanel.Init();
		m_PolyTwitchMain.gameObject.SetActive(value: false);
		m_PolyTwitchBridge.gameObject.SetActive(value: false);
		m_Recenter.gameObject.SetActive(value: false);
		ToolTipDisable();
		CloseSaveLoadPanels();
		m_TraceTool.m_RolloutPanel.SetActive(value: false);
		m_TraceTool.m_FillPanel.SetActive(value: false);
		m_Version.text = Version.m_DisplayName;
		m_PreloadingObject.SetActive(value: false);
		Bindings.CheckForSpecialDuplicates();
		DisableSandboxEditPanels();
		UpdateReferenceResolution();
	}

	private void DisableSandboxEditPanels()
	{
		m_SandboxEditAnchor.gameObject.SetActive(value: false);
		m_SandboxEditHydraulicsPhase.gameObject.SetActive(value: false);
		m_SandboxEditCheckpoint.gameObject.SetActive(value: false);
		m_SandboxEditBuildZone.gameObject.SetActive(value: false);
		m_SandboxEditCustomShape.gameObject.SetActive(value: false);
		m_SandboxEditCustomShapeTools.gameObject.SetActive(value: false);
		m_SandboxEditFlyingObject.gameObject.SetActive(value: false);
		m_SandboxEditPlatform.gameObject.SetActive(value: false);
		m_SandboxEditRamp.gameObject.SetActive(value: false);
		m_SandboxEditRock.gameObject.SetActive(value: false);
		m_SandboxEditPillar.gameObject.SetActive(value: false);
		m_SandboxEditDecor.gameObject.SetActive(value: false);
		m_SandboxEditTerrain.gameObject.SetActive(value: false);
		m_SandboxEditVehicle.gameObject.SetActive(value: false);
		m_SandboxEditVehicleRestartPhase.gameObject.SetActive(value: false);
		m_SandboxEditVehicleStopTrigger.gameObject.SetActive(value: false);
		m_SandboxEditWater.gameObject.SetActive(value: false);
	}

	private void Update()
	{
		UpdateToolTip();
		if (Game.InDecorModeTopView())
		{
			GroupSelect.UpdateXZ(GameInput.GetMousePosition());
		}
		else
		{
			GroupSelect.UpdateXY(GameInput.GetMousePosition());
		}
		if (GameStateCommonInput.IgnoreKeyboardInput())
		{
			GroupSelect.Cancel();
		}
		if (m_Selection.gameObject.activeInHierarchy && BridgeSelectionSet.IsEmpty())
		{
			m_Selection.gameObject.SetActive(value: false);
			m_Selection.OnClose();
		}
		else if (!m_Selection.gameObject.activeInHierarchy && !BridgeSelectionSet.IsEmpty() && (GameInput.GetActiveGameDevice() == GameDevice.KeyboardAndMouse || SandboxSelectionSet.IsEmpty()) && GameToolMode.GetMode() != GameToolModeType.MOVE)
		{
			m_Selection.gameObject.SetActive(value: true);
		}
		bool flag = ClipboardManager.ReadyToPaste() && !CampaignTutorial.IsRunning();
		if (m_Clipboard.gameObject.activeInHierarchy && !flag)
		{
			m_Clipboard.gameObject.SetActive(value: false);
			m_Clipboard.OnClose();
		}
		else if (!m_Clipboard.gameObject.activeInHierarchy && flag)
		{
			m_Clipboard.gameObject.SetActive(value: true);
		}
		if (!WorkshopPreview.m_IsTakingScreenshot && m_TopBar.gameObject.activeInHierarchy)
		{
			m_TopBar.UpdateManual();
		}
		if (ActivePanels.m_Panels.Count > 0 && !m_Instance.m_SandboxEditCustomShapeTools.DeleteSubModeActive())
		{
			SetPointerMode(PointerMode.NORMAL);
		}
	}

	private void LateUpdate()
	{
		m_RepopulateRaycastResults = true;
	}

	private void UpdateToolTip()
	{
		if (m_DisableTooltip)
		{
			ToolTipDisable();
			m_DisableTooltip = false;
		}
		m_PointerToolTip.UpdateManual();
		bool num = MaybeShowToolTipText();
		m_VehicleWithActiveTooltip = MaybeShowVehicleToolTip();
		m_CustomShapeWithActiveTooltip = MaybeShowCustomShapeToolTip();
		bool flag = GameStateBuild.MaybeShowFirstBreakToolTip();
		flag |= MaybeShowGalleryCreatedByToolTip();
		flag |= MaybeShowGalleryAllLevelsToolTip();
		flag |= MaybeShowWorkShopNameToolTip();
		flag |= m_Instance.m_TopBar.MaybeShowBudgetToolTip();
		if (!num && !flag)
		{
			m_DisableTooltip = true;
		}
		if (!m_VehicleWithActiveTooltip)
		{
			m_VehicleToolTip.Disable();
		}
		if (!m_CustomShapeWithActiveTooltip)
		{
			m_CustomShapeToolTip.Disable();
		}
	}

	public static Color JointColor()
	{
		if (!m_Instance)
		{
			return Color.white;
		}
		return m_Instance.m_JointColor;
	}

	public static Color JointGreyScaleColor()
	{
		if (!m_Instance)
		{
			return Color.white;
		}
		return m_Instance.m_JointGreyScaleColor;
	}

	public static Color JointOutlineColor()
	{
		if (!m_Instance)
		{
			return Color.white;
		}
		return m_Instance.m_JointOutlineColor;
	}

	public static Color StaticJointColor()
	{
		if (!m_Instance)
		{
			return Color.white;
		}
		return m_Instance.m_StaticJointColor;
	}

	public static Color StaticJointOutlineColor()
	{
		if (!m_Instance)
		{
			return Color.white;
		}
		return m_Instance.m_StaticJointOutlineColor;
	}

	public static Color JointHightlightColor()
	{
		if (!m_Instance)
		{
			return Color.white;
		}
		return m_Instance.m_JointHightlightColor;
	}

	public static Color PrebuiltJointHightlightColor()
	{
		if (!m_Instance)
		{
			return Color.white;
		}
		return m_Instance.m_PrebuiltJointHightlightColor;
	}

	public static Color StaticJointHightlightColor()
	{
		if (!m_Instance)
		{
			return Color.white;
		}
		return m_Instance.m_StaticJointHightlightColor;
	}

	public static Color SplitJointHighlightColor()
	{
		if (!m_Instance)
		{
			return Color.white;
		}
		return m_Instance.m_SplitJointHighlightColor;
	}

	public static Color JointOutlineHightlightColor()
	{
		if (!m_Instance)
		{
			return Color.white;
		}
		return m_Instance.m_JointOutlineHightlightColor;
	}

	public static Color StaticJointOutlineHighlightColor()
	{
		if (!m_Instance)
		{
			return Color.white;
		}
		return m_Instance.m_StaticJointOutlineHighlightColor;
	}

	public static Color SplitJointColor()
	{
		if (!m_Instance)
		{
			return Color.white;
		}
		return m_Instance.m_SplitJointColor;
	}

	public static Color EdgeSelectColor()
	{
		if (!m_Instance)
		{
			return Color.white;
		}
		return m_Instance.m_EdgeSelectColor;
	}

	public static Color EdgeJointSelectorHoverColor()
	{
		if (!m_Instance)
		{
			return Color.white;
		}
		return m_Instance.m_EdgeHighlightColor;
	}

	public static Color PlacementLineColor()
	{
		if (!m_Instance)
		{
			return Color.white;
		}
		return m_Instance.m_PlacementLineColor;
	}

	public static Color PlacementLineErrorColor()
	{
		if (!m_Instance)
		{
			return Color.white;
		}
		return m_Instance.m_PlacementLineErrorColor;
	}

	public static Color GroupSelectionBoxColor()
	{
		if (!m_Instance)
		{
			return Color.white;
		}
		return m_Instance.m_GroupSelectionBoxColor;
	}

	public static Color GroupSelectionBoxOutlineColor()
	{
		if (!m_Instance)
		{
			return Color.white;
		}
		return m_Instance.m_GroupSelectionBoxOutlineColor;
	}

	public static Color BudgetTextGreen()
	{
		if (!m_Instance)
		{
			return Color.white;
		}
		return m_Instance.m_BudgetTextGreen;
	}

	public static Color BudgetTextRed()
	{
		if (!m_Instance)
		{
			return Color.white;
		}
		return m_Instance.m_BudgetTextRed;
	}

	public static Color OverBudgetBackgroundColor()
	{
		if (!m_Instance)
		{
			return Color.white;
		}
		return m_Instance.m_OverBudgetBackgroundColor;
	}

	public static Color OverBudgetForegroundColor()
	{
		if (!m_Instance)
		{
			return Color.white;
		}
		return m_Instance.m_OverBudgetForegroundColor;
	}

	public void CancelDropdownSelection(TMP_Dropdown dropdown)
	{
		if (!dropdown)
		{
			return;
		}
		Transform transform = dropdown.transform.Find("Dropdown List");
		if ((bool)transform)
		{
			UnityEngine.Object.Destroy(transform.gameObject);
			Transform transform2 = base.transform.Find("Blocker");
			if ((bool)transform2)
			{
				UnityEngine.Object.Destroy(transform2.gameObject);
			}
		}
	}

	public static bool IsPointerOverGameObject()
	{
		if (PointerOver(typeof(WaterRuler)))
		{
			return false;
		}
		if (BridgeTrace.m_ArcTracer.HandlesVisible() && BridgeTrace.m_ArcTracer.PointerOverArcHandle(GameInput.GetMousePosition()) != null)
		{
			return false;
		}
		if (PointerOver(typeof(ArcTracer)))
		{
			return false;
		}
		if ((bool)EventSystem.current)
		{
			return EventSystem.current.IsPointerOverGameObject();
		}
		return false;
	}

	public static PointerMode GetPointerMode()
	{
		return m_PointerMode;
	}

	public static void SetPointerMode(PointerMode pointerMode)
	{
		if (m_PointerMode != pointerMode)
		{
			switch (pointerMode)
			{
			case PointerMode.NORMAL:
				SetPointerNormal();
				break;
			case PointerMode.MOVE:
				SetPointerMove();
				break;
			case PointerMode.SELECT:
				SetPointerSelect();
				break;
			case PointerMode.SELECT_TOGGLE:
				SetPointerSelectToggle();
				break;
			case PointerMode.ERASE:
				SetPointerErase();
				break;
			default:
				Debug.LogErrorFormat("Unsupported pointer mode {0}", pointerMode.ToString());
				break;
			}
		}
	}

	public static void ShowMessage(ScreenMessageLocation location, string message, float durationSeconds)
	{
		if (!CampaignTutorial.IsRunning())
		{
			((location == ScreenMessageLocation.TOP_LEFT) ? m_Instance.m_TopBar.m_MessageTopLeft : m_Instance.m_TopBar.m_MessageTopCenter).ShowMessage(location, message, durationSeconds);
			if (location == ScreenMessageLocation.TOP_CENTER && GameStateManager.GetState() != GameState.SIM)
			{
				Budget.UpdateBridgeCost();
			}
		}
	}

	public static void OnLayoutLoaded()
	{
		ClearMessages();
		m_Instance.m_HydraulicsController.gameObject.SetActive(value: false);
		m_Instance.m_CampaignTutorial.gameObject.SetActive(value: false);
		m_Instance.m_Help.gameObject.SetActive(value: false);
		m_Instance.m_GamepadHelp.gameObject.SetActive(value: false);
	}

	public static void ClearMessages()
	{
		m_Instance.m_TopBar.m_MessageTopLeft.ClearMessage();
		m_Instance.m_TopBar.m_MessageTopCenter.ClearMessage();
	}

	public static void ToolTipEnable(string text, Sprite icon, float xOffset, float yOffset)
	{
		m_Instance.m_ToolTip.Enable();
		m_Instance.m_ToolTip.Set(text, icon);
		SetScreenPosClamped(m_Instance.m_ToolTip.gameObject, GameInput.GetMousePosition(), xOffset, yOffset);
	}

	public static void ToolTipEnable(string text, Sprite icon)
	{
		if (!string.IsNullOrEmpty(text))
		{
			m_Instance.m_ToolTip.Enable();
			m_Instance.m_ToolTip.Set(text, icon);
			SetScreenPosClamped(m_Instance.m_ToolTip.gameObject, GameInput.GetMousePosition(), 0f, 0f);
		}
	}

	public static void ToolTipForceEnable(string text)
	{
		m_Instance.m_ToolTip.ForceEnable();
		m_Instance.m_ToolTip.Set(text, null);
		SetScreenPosClamped(m_Instance.m_ToolTip.gameObject, GameInput.GetMousePosition(), 0f, 0f);
	}

	public static void ToolTipDisable()
	{
		m_Instance.m_ToolTip.Disable();
	}

	public static void ClosePanelsWhenSwitchingModes()
	{
		m_Instance.m_HydraulicsController.gameObject.SetActive(value: false);
		m_Instance.m_LevelInfo.Close();
		m_Instance.m_LevelInfoLite.gameObject.SetActive(value: false);
		m_Instance.m_Help.gameObject.SetActive(value: false);
		m_Instance.m_GamepadHelp.gameObject.SetActive(value: false);
		m_Instance.m_LevelComplete.Close();
		m_Instance.m_LevelFailed.Close();
		m_Instance.m_ShareReplay.gameObject.SetActive(value: false);
		m_Instance.m_CustomShapeReset.Close();
		m_Instance.m_PolyTwitchMain.gameObject.SetActive(value: false);
		m_Instance.m_PolyTwitchBridge.gameObject.SetActive(value: false);
		CloseSaveLoadPanels();
	}

	public static void CloseSaveLoadPanels()
	{
		m_Instance.m_LoadBridge.gameObject.SetActive(value: false);
		m_Instance.m_LoadSandboxLayout.gameObject.SetActive(value: false);
		m_Instance.m_SaveBridge.gameObject.SetActive(value: false);
		m_Instance.m_SaveSandboxLayout.gameObject.SetActive(value: false);
	}

	public static bool SaveLoadPanelIsActive()
	{
		if (!m_Instance.m_LoadBridge.gameObject.activeInHierarchy && !m_Instance.m_LoadSandboxLayout.gameObject.activeInHierarchy && !m_Instance.m_SaveBridge.gameObject.activeInHierarchy)
		{
			return m_Instance.m_SaveSandboxLayout.gameObject.activeInHierarchy;
		}
		return true;
	}

	public static bool LevelEndPanelIsActive()
	{
		if (!m_Instance.m_LevelComplete.gameObject.activeInHierarchy)
		{
			return m_Instance.m_LevelFailed.gameObject.activeInHierarchy;
		}
		return true;
	}

	public static bool PopupIsActive()
	{
		if (!m_Instance.m_PopUpMessage.gameObject.activeInHierarchy)
		{
			return m_Instance.m_PopUpTwoChoices.gameObject.activeInHierarchy;
		}
		return true;
	}

	public static void ToggleLevelInfoPanel()
	{
		if (GameStateManager.GetState() == GameState.BUILD || GameStateManager.GetState() == GameState.SANDBOX)
		{
			if (!m_Instance.m_LevelInfo.gameObject.activeInHierarchy)
			{
				m_Instance.m_LevelInfo.Open();
			}
			else
			{
				m_Instance.m_LevelInfo.Close();
			}
		}
		else if (GameStateManager.GetState() == GameState.SIM)
		{
			m_Instance.m_LevelInfoLite.gameObject.SetActive(!m_Instance.m_LevelInfoLite.gameObject.activeInHierarchy);
			InterfaceAudio.Play(m_Instance.m_LevelInfoLite.gameObject.activeInHierarchy ? "ui_window_open" : "ui_window_close");
		}
	}

	public static string MarkupForBlack(string text)
	{
		return MarkupForColor(text, Color.black);
	}

	public static string MarkupForGold(string text)
	{
		return MarkupForColor(text, m_Instance.m_GoldColor);
	}

	public static string MarkupForYellow(string text)
	{
		return MarkupForColor(text, m_Instance.m_YellowTextColor);
	}

	public static string MarkupForGreen(string text)
	{
		return MarkupForColor(text, m_Instance.m_GreenTextColor);
	}

	public static string MarkupForRed(string text)
	{
		return MarkupForColor(text, m_Instance.m_RedTextColor);
	}

	public static string MarkupForColor(string text, Color color)
	{
		return $"{Utils.ColorToHex(color)}{text}</color>";
	}

	public static string MarkupForBold(string text)
	{
		return "<b>" + text + "</b>";
	}

	public static bool IsScreenDucked()
	{
		return m_Instance.m_ScreenDuck.activeInHierarchy;
	}

	public static void DuckScreen()
	{
		ActivePanels.Add(m_Instance.m_ScreenDuck);
		m_Instance.m_ScreenDuck.SetActive(value: true);
	}

	public static void UnDuckScreen()
	{
		ActivePanels.Remove(m_Instance.m_ScreenDuck);
		m_Instance.m_ScreenDuck.SetActive(value: false);
	}

	public static bool PointerOver(GameObject go)
	{
		if (!EventSystem.current)
		{
			return false;
		}
		MaybeRepopulateCachedRaycastResults();
		foreach (RaycastResult cachedRaycastResult in m_CachedRaycastResults)
		{
			if (cachedRaycastResult.gameObject == go)
			{
				return true;
			}
		}
		return false;
	}

	public static bool PointerOver(Type type)
	{
		if (!EventSystem.current)
		{
			return false;
		}
		MaybeRepopulateCachedRaycastResults();
		foreach (RaycastResult cachedRaycastResult in m_CachedRaycastResults)
		{
			if ((bool)cachedRaycastResult.gameObject.GetComponentInParent(type) && cachedRaycastResult.gameObject.name != "Ducking")
			{
				return true;
			}
		}
		return false;
	}

	public static FileSlot GetFileSlotUnderPointer()
	{
		if (!EventSystem.current)
		{
			return null;
		}
		MaybeRepopulateCachedRaycastResults();
		foreach (RaycastResult cachedRaycastResult in m_CachedRaycastResults)
		{
			if ((bool)cachedRaycastResult.gameObject.GetComponent<FileSlot>() && cachedRaycastResult.gameObject.name != "Ducking")
			{
				return cachedRaycastResult.gameObject.GetComponent<FileSlot>();
			}
		}
		return null;
	}

	public static SettingsRow GetSettingsRowUnderPointer()
	{
		if (!EventSystem.current)
		{
			return null;
		}
		MaybeRepopulateCachedRaycastResults();
		foreach (RaycastResult cachedRaycastResult in m_CachedRaycastResults)
		{
			if ((bool)cachedRaycastResult.gameObject.GetComponentInParent<SettingsRow>() && cachedRaycastResult.gameObject.name != "Ducking")
			{
				return cachedRaycastResult.gameObject.GetComponentInParent<SettingsRow>();
			}
		}
		return null;
	}

	public static bool PointerOverStrict(Type type)
	{
		if (!EventSystem.current)
		{
			return false;
		}
		MaybeRepopulateCachedRaycastResults();
		foreach (RaycastResult cachedRaycastResult in m_CachedRaycastResults)
		{
			if ((bool)cachedRaycastResult.gameObject.GetComponent(type))
			{
				return true;
			}
		}
		return false;
	}

	public static void SetPointerNormal()
	{
		Cursor.SetCursor(m_Instance.m_PointerNormalTexture, new Vector2(2f, 2f), CursorMode.Auto);
		GamepadManager.m_VirtualMouseUI.SetCursorNormal();
		m_PointerMode = PointerMode.NORMAL;
	}

	public static void SetPointerMove()
	{
		Cursor.SetCursor(m_Instance.m_PointerMoveTexture, new Vector2(16f, 16f), CursorMode.Auto);
		GamepadManager.m_VirtualMouseUI.SetCursorMove();
		m_PointerMode = PointerMode.MOVE;
	}

	public static void SetPointerSelect()
	{
		Cursor.SetCursor((GameStateManager.GetState() == GameState.SANDBOX) ? m_Instance.m_PointerNormalTexture : m_Instance.m_PointerSelectTexture, new Vector2(2f, 2f), CursorMode.Auto);
		m_PointerMode = PointerMode.SELECT;
	}

	public static void SetPointerSelectToggle()
	{
		Cursor.SetCursor(m_Instance.m_PointerSandboxSelectToggleTexture, new Vector2(2f, 2f), CursorMode.Auto);
		m_PointerMode = PointerMode.SELECT_TOGGLE;
	}

	public static void SetPointerErase()
	{
		Cursor.SetCursor(m_Instance.m_PointerEraseTexture, new Vector2(2f, 2f), CursorMode.Auto);
		GamepadManager.m_VirtualMouseUI.SetCursorErase();
		m_PointerMode = PointerMode.ERASE;
	}

	public static void CopyToClipboard(string text)
	{
		m_TextEditor.text = text;
		m_TextEditor.SelectAll();
		m_TextEditor.Copy();
	}

	public static void ToggleHud()
	{
		EnableHud(!HudIsActive());
	}

	public static void EnableHud(bool on)
	{
		m_Instance.m_TopBar.gameObject.SetActive(on);
		m_Instance.m_BuildToolBar.gameObject.SetActive(on && GameStateManager.GetState() == GameState.BUILD);
		m_Instance.m_SimToolBar.gameObject.SetActive(on && GameStateManager.GetState() == GameState.SIM);
		m_Instance.m_BottomBar.gameObject.SetActive(on && GameStateManager.GetState() == GameState.BUILD);
		m_Instance.m_LiveStress.gameObject.SetActive(on && GameStateManager.GetState() == GameState.SIM);
		m_DisableHud = !on;
	}

	public static bool HudIsActive()
	{
		if (!m_Instance.m_TopBar.gameObject.activeInHierarchy)
		{
			return m_Instance.m_LiveStress.gameObject.activeInHierarchy;
		}
		return true;
	}

	public static void SetAndEnableText(TextMeshProUGUI gameFontText, string rawText)
	{
		gameFontText.text = rawText;
		gameFontText.gameObject.SetActive(value: true);
	}

	public Texture GetOutlineTexture(GameState state)
	{
		if (state != GameState.SANDBOX)
		{
			return m_OutlineTextureBuildMode;
		}
		return m_OutlineTextureSandbox;
	}

	public Texture GetOutlineDashedTexture(GameState state)
	{
		if (state != GameState.SANDBOX)
		{
			return m_OutlineTextureDashedBuildMode;
		}
		return m_OutlineTextureDashedSandbox;
	}

	public float GetOutlineWidth(GameState state)
	{
		if (state != GameState.SANDBOX)
		{
			return m_OutlineWidthBuildMode;
		}
		return m_OutlineWidthSandbox;
	}

	public Color GetOutlineColor(GameState state)
	{
		if (state != GameState.SANDBOX)
		{
			return m_OutlineColorBuildMode;
		}
		return m_OutlineColorSandbox;
	}

	public static bool ShouldCancelPan(Vector2 startPanScreenPos, float startPanTime)
	{
		Vector2 vector = Utils.V3toV2(GameInput.GetMousePosition()) - startPanScreenPos;
		float num = Mathf.Abs(vector.x) / (float)Screen.width;
		float num2 = Mathf.Abs(vector.y) / (float)Screen.height;
		float num3 = Time.realtimeSinceStartup - startPanTime;
		if (num < Mathf.Epsilon && num2 < Mathf.Epsilon)
		{
			return true;
		}
		if (num < GameSettings.PasteCancelThresholdX() && num2 < GameSettings.PasteCancelThresholdY() && num3 < GameSettings.PasteCancelThresholdSeconds())
		{
			return true;
		}
		return false;
	}

	public static string GetLocalizedPrevDirString()
	{
		return "[< " + Localize.Get("UI_PREVIOUS_DIRECTORY") + "]";
	}

	public static int GetScreenYFromAnchor(float anchorY)
	{
		return Mathf.RoundToInt(anchorY / m_Instance.m_CanvasScaler.referenceResolution.y * (float)Screen.height);
	}

	public static void SetScreenPosClamped(GameObject gameObject, Vector2 screenPos, float anchorOffsetX, float anchorOffsetY)
	{
		gameObject.transform.position = Utils.V2toV3(screenPos);
		RectTransform component = gameObject.GetComponent<RectTransform>();
		if (component == null)
		{
			Debug.LogWarning("Could not get RectTransform in SetScreenPosClamped for " + gameObject.name);
			return;
		}
		component.anchoredPosition = new Vector2(component.anchoredPosition.x + anchorOffsetX, component.anchoredPosition.y + anchorOffsetY);
		Vector3[] array = new Vector3[4];
		component.GetWorldCorners(array);
		Vector3[] array2 = new Vector3[4];
		m_Instance.m_RectTransform.GetWorldCorners(array2);
		Vector3 localScale = m_Instance.m_RectTransform.localScale;
		Vector2 vector = array[1] - array2[1];
		Vector2 vector2 = array[3] - array2[3];
		component.anchoredPosition += new Vector2(Mathf.Max(0f, (0f - vector.x) / localScale.x) + Mathf.Min(0f, (0f - vector2.x) / localScale.x), Mathf.Min(0f, (0f - vector.y) / localScale.y) + Mathf.Max(0f, (0f - vector2.y) / localScale.y));
	}

	public static void SetScreenPos(GameObject gameObject, Vector2 screenPos, float anchorOffsetX, float anchorOffsetY)
	{
		gameObject.transform.position = Utils.V2toV3(screenPos);
		RectTransform component = gameObject.GetComponent<RectTransform>();
		if (component == null)
		{
			Debug.LogWarning("Could not get RectTransform in SetScreenPosClamped for " + gameObject.name);
			return;
		}
		float x = component.anchoredPosition.x + anchorOffsetX;
		float y = component.anchoredPosition.y + anchorOffsetY;
		component.anchoredPosition = new Vector2(x, y);
	}

	public static bool IsEditingCustomShapeOrRamp()
	{
		if (m_Instance.m_SandboxEditCustomShapeTools.gameObject.activeInHierarchy)
		{
			return true;
		}
		if (m_Instance.m_SandboxEditRamp.IsEditingSplinePoints())
		{
			return true;
		}
		return false;
	}

	public static float GetBuildTooltipX()
	{
		return 30f;
	}

	public static float GetSecondaryBuildTooltipY()
	{
		return -50f;
	}

	public static void ApplyUIScaleMode(UIScaleMode scaleMode)
	{
		m_Instance.m_CanvasScaler.uiScaleMode = ((scaleMode == UIScaleMode.SCALE_WITH_SCREEN_SIZE) ? CanvasScaler.ScaleMode.ScaleWithScreenSize : CanvasScaler.ScaleMode.ConstantPixelSize);
	}

	public static void ApplyUIScaleFactor(float scaleFactor)
	{
		if (Profiles.m_ActiveProfile.m_UIScaleMode == UIScaleMode.SCALE_WITH_SCREEN_SIZE)
		{
			scaleFactor = Mathf.Clamp(scaleFactor, MIN_SCALE_FACTOR_RELATIVE, MAX_SCALE_FACTOR_RELATIVE);
		}
		else
		{
			float min = (float)Screen.height / m_Instance.m_CanvasScaler.referenceResolution.y * MIN_SCALE_FACTOR_RELATIVE;
			float max = 1.1f * ((float)Screen.height / m_Instance.m_CanvasScaler.referenceResolution.y);
			scaleFactor = Mathf.Clamp(scaleFactor, min, max);
		}
		float num = (Game.IsRunningOnSteamDeck() ? REFERENCE_RESOLUTION_Y_STEAMDECK : REFERENCE_RESOLUTION_Y_DEFAULT);
		if (Profiles.m_ActiveProfile.m_UIScaleMode == UIScaleMode.SCALE_WITH_SCREEN_SIZE)
		{
			m_Instance.m_CanvasScaler.referenceResolution = new Vector2(m_Instance.m_CanvasScaler.referenceResolution.x, num / scaleFactor);
			m_Instance.m_CanvasScaler.scaleFactor = 1f;
		}
		else
		{
			m_Instance.m_CanvasScaler.scaleFactor = scaleFactor;
			m_Instance.m_CanvasScaler.referenceResolution = new Vector2(m_Instance.m_CanvasScaler.referenceResolution.x, num);
		}
		if (!Mathf.Approximately(Profiles.m_ActiveProfile.m_UIScaleFactor, scaleFactor))
		{
			Profiles.m_ActiveProfile.m_UIScaleFactor = scaleFactor;
			Profiles.SaveActiveProfile();
		}
	}

	private static bool MaybeShowToolTipText()
	{
		if (!EventSystem.current)
		{
			return false;
		}
		MaybeRepopulateCachedRaycastResults();
		foreach (RaycastResult cachedRaycastResult in m_CachedRaycastResults)
		{
			ToolTipText component = cachedRaycastResult.gameObject.GetComponent<ToolTipText>();
			if ((bool)component)
			{
				Button component2 = component.GetComponent<Button>();
				if (!component2 || component2.interactable)
				{
					float yOffset = (TooTipInTopBar(component) ? (-50) : 0);
					ToolTipEnable(component.GetText(), (GameInput.GetActiveGameDevice() == GameDevice.Gamepad) ? m_Instance.m_GamepadLegend.GetIcon(component.m_GamepadButtonType) : null, 0f, yOffset);
					return true;
				}
			}
			else if (cachedRaycastResult.gameObject.activeInHierarchy)
			{
				break;
			}
		}
		return false;
	}

	private static bool TooTipInTopBar(ToolTipText tooltip)
	{
		if (tooltip.m_LocalizationKey != ToolTipLocalizationKey.TOOLTIP_START_SIM && tooltip.m_LocalizationKey != ToolTipLocalizationKey.TOOLTIP_STOP_SIM && tooltip.m_LocalizationKey != ToolTipLocalizationKey.TOOLTIP_PAUSE_SIM && tooltip.m_LocalizationKey != ToolTipLocalizationKey.TOOLTIP_RESUME_SIM && tooltip.m_LocalizationKey != ToolTipLocalizationKey.TOOLTIP_HELP && tooltip.m_LocalizationKey != ToolTipLocalizationKey.TOOLTIP_REPLAY && tooltip.m_LocalizationKey != ToolTipLocalizationKey.TOOLTIP_LEVEL_INFO && tooltip.m_LocalizationKey != ToolTipLocalizationKey.TOOLTIP_GOD_MODE && tooltip.m_LocalizationKey != ToolTipLocalizationKey.TOOLTIP_GOD_MODE_SELECTED && (tooltip.m_LocalizationKey != ToolTipLocalizationKey.TOOLTIP_UNDO || GameStateManager.GetState() != GameState.SANDBOX) && (tooltip.m_LocalizationKey != ToolTipLocalizationKey.TOOLTIP_REDO || GameStateManager.GetState() != GameState.SANDBOX) && (tooltip.m_LocalizationKey != ToolTipLocalizationKey.TOOLTIP_TRASH || GameStateManager.GetState() != GameState.SANDBOX) && tooltip.m_LocalizationKey != ToolTipLocalizationKey.TOOLTIP_PAUSE_MENU)
		{
			return tooltip.m_RawLocalizationKey == "UI_GAMEPAD_HELP";
		}
		return true;
	}

	private static Vehicle MaybeShowVehicleToolTip()
	{
		if (Game.IsCurrentLevelTutorial())
		{
			return null;
		}
		foreach (Vehicle vehicle in Vehicles.m_Vehicles)
		{
			if (GameStateBuild.VehicleHasHoverFocus(vehicle))
			{
				m_Instance.m_VehicleToolTip.Enable(vehicle);
				return vehicle;
			}
		}
		return null;
	}

	private static CustomShape MaybeShowCustomShapeToolTip()
	{
		foreach (CustomShape shape in CustomShapes.m_Shapes)
		{
			if (GameStateBuild.CustomShapeHasHoverFocus(shape))
			{
				m_Instance.m_CustomShapeToolTip.Enable(shape);
				return shape;
			}
		}
		return null;
	}

	public static void UpdateReferenceResolution()
	{
		ApplyUIScaleFactor(Profiles.m_ActiveProfile.m_UIScaleFactor);
	}

	private bool MaybeShowGalleryCreatedByToolTip()
	{
		if (!m_Instance.m_Gallery.m_GalleryVideo.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (m_Instance.m_Gallery.m_GalleryVideo.IsPointerOverCreatedByButton())
		{
			ToolTipEnable(m_Instance.m_Gallery.m_GalleryVideo.GetCreatedByToolTipText(), null);
			return true;
		}
		return false;
	}

	private bool MaybeShowGalleryAllLevelsToolTip()
	{
		if (!m_Instance.m_Gallery.m_GalleryVideo.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (m_Instance.m_Gallery.m_GalleryVideo.IsPointerOverAllLevelsButton())
		{
			ToolTipEnable(m_Instance.m_Gallery.m_GalleryVideo.GetAllLevelVideosToolTipText(), null);
			return true;
		}
		return false;
	}

	private bool MaybeShowWorkShopNameToolTip()
	{
		if (!m_Instance.m_Workshop.m_WorkshopItemPanel.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (m_Instance.m_Workshop.m_WorkshopItemPanel.IsPointerOverCreatedByButton())
		{
			ToolTipEnable(m_Instance.m_Workshop.m_WorkshopItemPanel.GetCreatedByToolTipText(), null);
			return true;
		}
		return false;
	}

	private static void MaybeRepopulateCachedRaycastResults()
	{
		if (m_RepopulateRaycastResults)
		{
			m_CachedRaycastResults.Clear();
			m_PointerEventData.position = GameInput.GetMousePosition();
			m_Instance.m_Raycaster.Raycast(m_PointerEventData, m_CachedRaycastResults);
			m_RepopulateRaycastResults = false;
		}
	}
}

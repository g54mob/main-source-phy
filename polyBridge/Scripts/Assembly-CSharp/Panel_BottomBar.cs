using UnityEngine;
using UnityEngine.UI;

public class Panel_BottomBar : MonoBehaviour
{
	[Header("Panel")]
	public RectTransform m_RootRectTransform;

	public PanelResizeHorizontal m_PanelResizeHorizontal;

	[Header("Material Buttons")]
	public BridgeMaterialButton m_RoadMaterialButton;

	public BridgeMaterialButton m_ReinforcedRoadMaterialButton;

	public BridgeMaterialButton m_WoodMaterialButton;

	public BridgeMaterialButton m_SteelMaterialButton;

	public BridgeMaterialButton m_HydraulicsMaterialButton;

	public BridgeMaterialButton m_RopeMaterialButton;

	public BridgeMaterialButton m_CableMaterialButton;

	public BridgeMaterialButton m_SpringMaterialButton;

	public BridgeMaterialButton m_PillarMaterialButton;

	[Header("Hydraulic Controller")]
	public Button m_HydraulicController;

	public Image m_HydraulicControllerIcon;

	[Header("HelpArrows")]
	public GameObject m_FoundationHelpArrow;

	public GameObject m_HydraulicControllerHelpArrow;

	private float DISABLED_ALPHA = 0.1f;

	private BridgeMaterialButton m_RoadButtonWithFocus;

	private void Start()
	{
		m_HydraulicController.onClick.AddListener(OnHydraulicsController);
		m_RoadMaterialButton.m_TwoStateButton.m_PointerDownEvent.AddListener(OnRoad);
		m_ReinforcedRoadMaterialButton.m_TwoStateButton.m_PointerDownEvent.AddListener(OnReinforcedRoad);
		m_WoodMaterialButton.m_TwoStateButton.m_PointerDownEvent.AddListener(OnWood);
		m_SteelMaterialButton.m_TwoStateButton.m_PointerDownEvent.AddListener(OnSteel);
		m_HydraulicsMaterialButton.m_TwoStateButton.m_PointerDownEvent.AddListener(OnHydraulics);
		m_RopeMaterialButton.m_TwoStateButton.m_PointerDownEvent.AddListener(OnRope);
		m_CableMaterialButton.m_TwoStateButton.m_PointerDownEvent.AddListener(OnCable);
		m_SpringMaterialButton.m_TwoStateButton.m_PointerDownEvent.AddListener(OnSpring);
		m_PillarMaterialButton.m_TwoStateButton.m_PointerDownEvent.AddListener(OnPillar);
		m_FoundationHelpArrow.gameObject.SetActive(value: false);
		m_HydraulicControllerHelpArrow.gameObject.SetActive(value: false);
	}

	private void OnEnable()
	{
		m_HydraulicControllerIcon.color = Color.white;
	}

	private void OnDisable()
	{
		m_FoundationHelpArrow.gameObject.SetActive(value: false);
		m_HydraulicControllerHelpArrow.gameObject.SetActive(value: false);
	}

	private void Update()
	{
		UpdateIconDucking();
		UpdateFoundationHelpArrow();
		RefreshLimits();
	}

	public void OnLayoutLoaded()
	{
		InitSlot(m_RoadMaterialButton, Budget.m_RoadBudget);
		InitSlot(m_ReinforcedRoadMaterialButton, Budget.m_RoadBudget);
		InitSlot(m_WoodMaterialButton, Budget.m_WoodBudget);
		InitSlot(m_SteelMaterialButton, Budget.m_SteelBudget);
		InitSlot(m_HydraulicsMaterialButton, Budget.m_HydraulicBudget);
		InitSlot(m_RopeMaterialButton, Budget.m_RopeBudget);
		InitSlot(m_CableMaterialButton, Budget.m_CableBudget);
		InitSlot(m_SpringMaterialButton, Budget.m_SpringBudget);
		InitSlot(m_PillarMaterialButton, Budget.m_PillarBudget);
		Bridge.m_BuildMaterialType = BridgeMaterialType.INVALID;
		Game.SelectFirstValidMaterial();
	}

	public void SetMaterialIconsAlpha()
	{
		m_RoadMaterialButton.m_TwoStateButton.SetAlpha((Budget.m_RoadBudget > 0) ? 1f : DISABLED_ALPHA);
		m_ReinforcedRoadMaterialButton.m_TwoStateButton.SetAlpha((Budget.m_RoadBudget > 0) ? 1f : DISABLED_ALPHA);
		m_WoodMaterialButton.m_TwoStateButton.SetAlpha((Budget.m_WoodBudget > 0) ? 1f : DISABLED_ALPHA);
		m_SteelMaterialButton.m_TwoStateButton.SetAlpha((Budget.m_SteelBudget > 0) ? 1f : DISABLED_ALPHA);
		m_HydraulicsMaterialButton.m_TwoStateButton.SetAlpha((Budget.m_HydraulicBudget > 0) ? 1f : DISABLED_ALPHA);
		m_RopeMaterialButton.m_TwoStateButton.SetAlpha((Budget.m_RopeBudget > 0) ? 1f : DISABLED_ALPHA);
		m_CableMaterialButton.m_TwoStateButton.SetAlpha((Budget.m_CableBudget > 0) ? 1f : DISABLED_ALPHA);
		m_SpringMaterialButton.m_TwoStateButton.SetAlpha((Budget.m_SpringBudget > 0) ? 1f : DISABLED_ALPHA);
		m_PillarMaterialButton.m_TwoStateButton.SetAlpha((Budget.m_PillarBudget > 0) ? 1f : DISABLED_ALPHA);
	}

	public void UpdateMaterialLimits()
	{
		m_RoadMaterialButton.m_MaterialLimit.Set(Budget.m_RoadLeft);
		m_ReinforcedRoadMaterialButton.m_MaterialLimit.Set(Budget.m_RoadLeft);
		m_WoodMaterialButton.m_MaterialLimit.Set(Budget.m_WoodLeft);
		m_SteelMaterialButton.m_MaterialLimit.Set(Budget.m_SteelLeft);
		m_HydraulicsMaterialButton.m_MaterialLimit.Set(Budget.m_HydraulicLeft);
		m_RopeMaterialButton.m_MaterialLimit.Set(Budget.m_RopeLeft);
		m_CableMaterialButton.m_MaterialLimit.Set(Budget.m_CableLeft);
		m_SpringMaterialButton.m_MaterialLimit.Set(Budget.m_SpringLeft);
		m_PillarMaterialButton.m_MaterialLimit.Set(Budget.m_PillarLeft);
	}

	public void RefreshLimits()
	{
		UpdateMaterialLimits();
		m_RoadMaterialButton.m_MaterialLimit.gameObject.SetActive(Budget.m_RoadBudget != Budget.UNLIMITED_MATERIAL_BUDGET && Budget.m_RoadBudget != 0 && m_RoadButtonWithFocus == m_RoadMaterialButton);
		m_ReinforcedRoadMaterialButton.m_MaterialLimit.gameObject.SetActive(Budget.m_RoadBudget != Budget.UNLIMITED_MATERIAL_BUDGET && Budget.m_RoadBudget != 0 && m_RoadButtonWithFocus == m_ReinforcedRoadMaterialButton);
		m_WoodMaterialButton.m_MaterialLimit.gameObject.SetActive(Budget.m_WoodBudget != Budget.UNLIMITED_MATERIAL_BUDGET && Budget.m_WoodBudget != 0);
		m_SteelMaterialButton.m_MaterialLimit.gameObject.SetActive(Budget.m_SteelBudget != Budget.UNLIMITED_MATERIAL_BUDGET && Budget.m_SteelBudget != 0);
		m_HydraulicsMaterialButton.m_MaterialLimit.gameObject.SetActive(Budget.m_HydraulicBudget != Budget.UNLIMITED_MATERIAL_BUDGET && Budget.m_HydraulicBudget != 0);
		m_RopeMaterialButton.m_MaterialLimit.gameObject.SetActive(Budget.m_RopeBudget != Budget.UNLIMITED_MATERIAL_BUDGET && Budget.m_RopeBudget != 0);
		m_CableMaterialButton.m_MaterialLimit.gameObject.SetActive(Budget.m_CableBudget != Budget.UNLIMITED_MATERIAL_BUDGET && Budget.m_CableBudget != 0);
		m_SpringMaterialButton.m_MaterialLimit.gameObject.SetActive(Budget.m_SpringBudget != Budget.UNLIMITED_MATERIAL_BUDGET && Budget.m_SpringBudget != 0);
		m_PillarMaterialButton.m_MaterialLimit.gameObject.SetActive(Budget.m_PillarBudget != Budget.UNLIMITED_MATERIAL_BUDGET && Budget.m_PillarBudget != 0);
		if (m_ReinforcedRoadMaterialButton.m_TwoStateButton.IsOn() && SandboxSettings.m_NoReinforcedRoad)
		{
			Bridge.m_BuildMaterialType = BridgeMaterialType.INVALID;
			Game.SelectFirstValidMaterial();
		}
		m_WoodMaterialButton.gameObject.SetActive(Budget.m_AllowWood);
		m_SteelMaterialButton.gameObject.SetActive(Budget.m_AllowSteel);
		m_HydraulicsMaterialButton.gameObject.SetActive(Budget.m_AllowHydraulic);
		m_RopeMaterialButton.gameObject.SetActive(Budget.m_AllowRope);
		m_CableMaterialButton.gameObject.SetActive(Budget.m_AllowCable);
		m_SpringMaterialButton.gameObject.SetActive(Budget.m_AllowSpring);
		m_PillarMaterialButton.gameObject.SetActive(Budget.m_AllowPillar);
		m_ReinforcedRoadMaterialButton.gameObject.SetActive(!SandboxSettings.m_NoReinforcedRoad);
	}

	public void OnRoad()
	{
		if (!GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			OnMaterial(BridgeMaterialType.ROAD);
			if (!Budget.HasZeroBudget(BridgeMaterialType.ROAD))
			{
				SetRoadButtonFocus(m_RoadMaterialButton);
			}
		}
	}

	public void OnReinforcedRoad()
	{
		if (!GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			OnMaterial(BridgeMaterialType.REINFORCED_ROAD);
			if (!Budget.HasZeroBudget(BridgeMaterialType.ROAD))
			{
				SetRoadButtonFocus(m_ReinforcedRoadMaterialButton);
			}
		}
	}

	public void OnWood()
	{
		if (!GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			OnMaterial(BridgeMaterialType.WOOD);
			BridgeJointPlacement.CancelSelection();
		}
	}

	public void OnSteel()
	{
		if (!GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			OnMaterial(BridgeMaterialType.STEEL);
			BridgeJointPlacement.CancelSelection();
		}
	}

	public void OnHydraulics()
	{
		if (!GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			OnMaterial(BridgeMaterialType.HYDRAULICS);
			BridgeJointPlacement.CancelSelection();
		}
	}

	public void OnRope()
	{
		if (!GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			OnMaterial(BridgeMaterialType.ROPE);
			BridgeJointPlacement.CancelSelection();
		}
	}

	public void OnCable()
	{
		if (!GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			OnMaterial(BridgeMaterialType.CABLE);
			BridgeJointPlacement.CancelSelection();
		}
	}

	public void OnSpring()
	{
		if (!GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			OnMaterial(BridgeMaterialType.SPRING);
			BridgeJointPlacement.CancelSelection();
		}
	}

	public void OnPillar()
	{
		if (!GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			if (Bridge.m_BuildMaterialType != BridgeMaterialType.PILLAR && Bridge.m_BuildMaterialType != BridgeMaterialType.INVALID)
			{
				BridgePillarPlacement.m_PreviousSelectedBridgeMaterialType = Bridge.m_BuildMaterialType;
			}
			OnMaterial(BridgeMaterialType.PILLAR);
			if (m_FoundationHelpArrow.activeInHierarchy)
			{
				m_FoundationHelpArrow.SetActive(value: false);
				Profiles.m_ActiveProfile.m_DismissedFoundationHelpArrow = true;
				Profiles.SaveActiveProfile();
			}
			BridgeJointPlacement.CancelSelection();
		}
	}

	public bool SelectMaterial(BridgeMaterialType materialType, bool animateTransition)
	{
		TurnOnSelectedMaterial(materialType, animateTransition);
		if (BridgeTrace.m_JustFilled)
		{
			BridgeTrace.TurnOffTracing();
			BridgeTrace.m_JustFilled = false;
		}
		if (materialType == BridgeMaterialType.PILLAR)
		{
			BridgeJointMovement.CancelSelection();
		}
		if (GameToolMode.GetMode() != GameToolModeType.BUILD && GameStateManager.GetState() == GameState.BUILD)
		{
			GameToolMode.SetMode(GameToolModeType.BUILD);
		}
		if (GetMaterialButton(materialType).IsDucked())
		{
			return true;
		}
		if (Bridge.m_BuildMaterialType != materialType)
		{
			TurnOffSelectedMaterial(Bridge.m_BuildMaterialType, animateTransition);
			Bridge.m_BuildMaterialType = materialType;
			return true;
		}
		return false;
	}

	public void ClearSelectedMaterial()
	{
		TurnOffSelectedMaterial(Bridge.m_BuildMaterialType, animateTransition: false);
	}

	public bool RoadHasFocus()
	{
		if (!RegularRoadHasFocus())
		{
			return ReinforcedRoadHasFocus();
		}
		return true;
	}

	public bool ReinforcedRoadHasFocus()
	{
		return m_RoadButtonWithFocus == m_ReinforcedRoadMaterialButton;
	}

	public bool RegularRoadHasFocus()
	{
		return m_RoadButtonWithFocus == m_RoadMaterialButton;
	}

	public void OnMaterial(BridgeMaterialType materialType)
	{
		TwoStateButton materialButton = GetMaterialButton(materialType);
		if (materialButton == null)
		{
			return;
		}
		if (CampaignTutorial.IsRunning() && ClipboardManager.ReadyToPaste())
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		ClipboardManager.ClearClipboard();
		if (materialButton.IsOff() || materialButton.IsDucked() || Budget.HasZeroBudget(materialType))
		{
			if (Budget.HasZeroBudget(materialType))
			{
				InterfaceAudio.PlayErrorBeep();
			}
			else if (SelectMaterial(materialType, animateTransition: true))
			{
				BridgeAudio.PlayMaterialSelect(materialType);
				GameToolMode.SetMode(GameToolModeType.BUILD);
			}
		}
	}

	public void SetMaterialIconsInteractive(bool interactable)
	{
		m_RoadMaterialButton.m_TwoStateButton.m_Button.interactable = interactable;
		m_ReinforcedRoadMaterialButton.m_TwoStateButton.m_Button.interactable = interactable;
		m_WoodMaterialButton.m_TwoStateButton.m_Button.interactable = interactable;
		m_SteelMaterialButton.m_TwoStateButton.m_Button.interactable = interactable;
		m_HydraulicsMaterialButton.m_TwoStateButton.m_Button.interactable = interactable;
		m_RopeMaterialButton.m_TwoStateButton.m_Button.interactable = interactable;
		m_CableMaterialButton.m_TwoStateButton.m_Button.interactable = interactable;
		m_SpringMaterialButton.m_TwoStateButton.m_Button.interactable = interactable;
		m_PillarMaterialButton.m_TwoStateButton.m_Button.interactable = interactable;
	}

	public void SetHydraulicsControllerIconColor(Color color)
	{
		m_HydraulicControllerIcon.color = color;
	}

	public void PulseHydraulicControllerIcon()
	{
		PulseIcons.Pulse(m_HydraulicControllerIcon, 0.8f, 1f, Color.white, GameUI.m_Instance.m_GoldColor);
	}

	public void CycleNext()
	{
		BridgeMaterialType buildMaterialType = Bridge.m_BuildMaterialType;
		BridgeMaterialType buildMaterialType2 = Bridge.m_BuildMaterialType;
		for (int i = (int)(buildMaterialType2 + 1); i <= 10; i++)
		{
			if (CanSelectMaterial((BridgeMaterialType)i))
			{
				OnMaterial((BridgeMaterialType)i);
				break;
			}
		}
		if (buildMaterialType2 == buildMaterialType)
		{
			InterfaceAudio.PlayErrorBeep();
		}
	}

	public void CyclePrev()
	{
		BridgeMaterialType buildMaterialType = Bridge.m_BuildMaterialType;
		BridgeMaterialType buildMaterialType2 = Bridge.m_BuildMaterialType;
		for (int num = (int)(buildMaterialType2 - 1); num >= 1; num--)
		{
			if (CanSelectMaterial((BridgeMaterialType)num))
			{
				OnMaterial((BridgeMaterialType)num);
				break;
			}
		}
		if (buildMaterialType2 == buildMaterialType)
		{
			InterfaceAudio.PlayErrorBeep();
		}
	}

	public void UpdateForCurrentDevice()
	{
		m_RootRectTransform.anchoredPosition = new Vector2(0f, (GameInput.GetActiveGameDevice() == GameDevice.Gamepad) ? GamepadLegend.HEIGHT : 0);
	}

	private bool CanSelectMaterial(BridgeMaterialType bridgeMaterialType)
	{
		if (bridgeMaterialType == BridgeMaterialType.REINFORCED_ROAD || bridgeMaterialType == BridgeMaterialType.BUNGINE_ROPE)
		{
			return false;
		}
		if (GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (CampaignTutorial.IsRunning() && ClipboardManager.ReadyToPaste())
		{
			return false;
		}
		TwoStateButton materialButton = GetMaterialButton(bridgeMaterialType);
		if (materialButton == null)
		{
			return false;
		}
		if (!materialButton.IsOff() && !materialButton.IsDucked())
		{
			return false;
		}
		if (Budget.HasZeroBudget(bridgeMaterialType))
		{
			return false;
		}
		return true;
	}

	private TwoStateButton GetMaterialButton(BridgeMaterialType materialType)
	{
		switch (materialType)
		{
		case BridgeMaterialType.ROAD:
			return m_RoadMaterialButton.m_TwoStateButton;
		case BridgeMaterialType.REINFORCED_ROAD:
			return m_ReinforcedRoadMaterialButton.m_TwoStateButton;
		case BridgeMaterialType.WOOD:
			return m_WoodMaterialButton.m_TwoStateButton;
		case BridgeMaterialType.STEEL:
			return m_SteelMaterialButton.m_TwoStateButton;
		case BridgeMaterialType.HYDRAULICS:
			return m_HydraulicsMaterialButton.m_TwoStateButton;
		case BridgeMaterialType.ROPE:
			return m_RopeMaterialButton.m_TwoStateButton;
		case BridgeMaterialType.CABLE:
			return m_CableMaterialButton.m_TwoStateButton;
		case BridgeMaterialType.SPRING:
			return m_SpringMaterialButton.m_TwoStateButton;
		case BridgeMaterialType.PILLAR:
			return m_PillarMaterialButton.m_TwoStateButton;
		default:
			Debug.LogWarningFormat("Unexpected materialType in PanelMaterials.GetMaterialButton: {0}", materialType.ToString());
			return null;
		}
	}

	private void InitSlot(BridgeMaterialButton bridgeMaterialButton, int limit)
	{
		bridgeMaterialButton.SelectNoAnimation(on: false);
		bridgeMaterialButton.m_MaterialLimit.gameObject.SetActive(limit < Budget.UNLIMITED_MATERIAL_BUDGET);
		bridgeMaterialButton.m_MaterialLimit.Set(limit);
	}

	private void TurnOnSelectedMaterial(BridgeMaterialType material, bool animateTransition)
	{
		switch (material)
		{
		case BridgeMaterialType.ROAD:
			if (animateTransition)
			{
				m_RoadMaterialButton.Select(on: true);
			}
			else
			{
				m_RoadMaterialButton.SelectNoAnimation(on: true);
			}
			SetRoadButtonFocus(m_RoadMaterialButton);
			break;
		case BridgeMaterialType.REINFORCED_ROAD:
			if (animateTransition)
			{
				m_ReinforcedRoadMaterialButton.Select(on: true);
			}
			else
			{
				m_ReinforcedRoadMaterialButton.SelectNoAnimation(on: true);
			}
			SetRoadButtonFocus(m_ReinforcedRoadMaterialButton);
			break;
		case BridgeMaterialType.WOOD:
			if (animateTransition)
			{
				m_WoodMaterialButton.Select(on: true);
			}
			else
			{
				m_WoodMaterialButton.SelectNoAnimation(on: true);
			}
			break;
		case BridgeMaterialType.STEEL:
			if (animateTransition)
			{
				m_SteelMaterialButton.Select(on: true);
			}
			else
			{
				m_SteelMaterialButton.SelectNoAnimation(on: true);
			}
			break;
		case BridgeMaterialType.HYDRAULICS:
			if (animateTransition)
			{
				m_HydraulicsMaterialButton.Select(on: true);
			}
			else
			{
				m_HydraulicsMaterialButton.SelectNoAnimation(on: true);
			}
			break;
		case BridgeMaterialType.ROPE:
			if (animateTransition)
			{
				m_RopeMaterialButton.Select(on: true);
			}
			else
			{
				m_RopeMaterialButton.SelectNoAnimation(on: true);
			}
			break;
		case BridgeMaterialType.CABLE:
			if (animateTransition)
			{
				m_CableMaterialButton.Select(on: true);
			}
			else
			{
				m_CableMaterialButton.SelectNoAnimation(on: true);
			}
			break;
		case BridgeMaterialType.SPRING:
			if (animateTransition)
			{
				m_SpringMaterialButton.Select(on: true);
			}
			else
			{
				m_SpringMaterialButton.SelectNoAnimation(on: true);
			}
			break;
		case BridgeMaterialType.PILLAR:
			if (animateTransition)
			{
				m_PillarMaterialButton.Select(on: true);
			}
			else
			{
				m_PillarMaterialButton.SelectNoAnimation(on: true);
			}
			break;
		case BridgeMaterialType.BUNGINE_ROPE:
			break;
		}
	}

	private void TurnOffSelectedMaterial(BridgeMaterialType material, bool animateTransition)
	{
		switch (material)
		{
		case BridgeMaterialType.ROAD:
			if (animateTransition)
			{
				m_RoadMaterialButton.Select(on: false);
			}
			else
			{
				m_RoadMaterialButton.SelectNoAnimation(on: false);
			}
			break;
		case BridgeMaterialType.REINFORCED_ROAD:
			if (animateTransition)
			{
				m_ReinforcedRoadMaterialButton.Select(on: false);
			}
			else
			{
				m_ReinforcedRoadMaterialButton.SelectNoAnimation(on: false);
			}
			break;
		case BridgeMaterialType.WOOD:
			if (animateTransition)
			{
				m_WoodMaterialButton.Select(on: false);
			}
			else
			{
				m_WoodMaterialButton.SelectNoAnimation(on: false);
			}
			break;
		case BridgeMaterialType.STEEL:
			if (animateTransition)
			{
				m_SteelMaterialButton.Select(on: false);
			}
			else
			{
				m_SteelMaterialButton.SelectNoAnimation(on: false);
			}
			break;
		case BridgeMaterialType.HYDRAULICS:
			if (animateTransition)
			{
				m_HydraulicsMaterialButton.Select(on: false);
			}
			else
			{
				m_HydraulicsMaterialButton.SelectNoAnimation(on: false);
			}
			break;
		case BridgeMaterialType.ROPE:
			if (animateTransition)
			{
				m_RopeMaterialButton.Select(on: false);
			}
			else
			{
				m_RopeMaterialButton.SelectNoAnimation(on: false);
			}
			break;
		case BridgeMaterialType.CABLE:
			if (animateTransition)
			{
				m_CableMaterialButton.Select(on: false);
			}
			else
			{
				m_CableMaterialButton.SelectNoAnimation(on: false);
			}
			break;
		case BridgeMaterialType.SPRING:
			if (animateTransition)
			{
				m_SpringMaterialButton.Select(on: false);
			}
			else
			{
				m_SpringMaterialButton.SelectNoAnimation(on: false);
			}
			break;
		case BridgeMaterialType.PILLAR:
			if (animateTransition)
			{
				m_PillarMaterialButton.Select(on: false);
			}
			else
			{
				m_PillarMaterialButton.SelectNoAnimation(on: false);
			}
			break;
		case BridgeMaterialType.BUNGINE_ROPE:
			break;
		}
	}

	public void UpdateIconDucking()
	{
		UpdateDuckingForButton(m_RoadMaterialButton.m_TwoStateButton, Budget.m_RoadBudget);
		UpdateDuckingForButton(m_WoodMaterialButton.m_TwoStateButton, Budget.m_WoodBudget);
		UpdateDuckingForButton(m_SteelMaterialButton.m_TwoStateButton, Budget.m_SteelBudget);
		UpdateDuckingForButton(m_HydraulicsMaterialButton.m_TwoStateButton, Budget.m_HydraulicBudget);
		UpdateDuckingForButton(m_RopeMaterialButton.m_TwoStateButton, Budget.m_RopeBudget);
		UpdateDuckingForButton(m_CableMaterialButton.m_TwoStateButton, Budget.m_CableBudget);
		UpdateDuckingForButton(m_SpringMaterialButton.m_TwoStateButton, Budget.m_SpringBudget);
		UpdateDuckingForButton(m_PillarMaterialButton.m_TwoStateButton, Budget.m_PillarBudget);
	}

	private void UpdateDuckingForButton(TwoStateButton button, int budget)
	{
		if (button.IsOn() && budget > 0 && (BridgeTrace.IsTracingActive() || GameToolMode.GetMode() != GameToolModeType.BUILD))
		{
			button.Duck();
		}
		else
		{
			button.UnDuck(budget);
		}
	}

	private void SetRoadButtonFocus(BridgeMaterialButton bridgeMaterialButton)
	{
		m_RoadButtonWithFocus = bridgeMaterialButton;
		bridgeMaterialButton.m_TwoStateButton.transform.localScale = new Vector3(1f, 1f, 1f);
		if (bridgeMaterialButton == m_RoadMaterialButton)
		{
			m_ReinforcedRoadMaterialButton.m_TwoStateButton.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
		}
		else
		{
			m_RoadMaterialButton.m_TwoStateButton.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
		}
	}

	private void OnHydraulicsController()
	{
		InterfaceAudio.Play("ui_build_hydraulicsController_select");
		GameToolMode.SetMode(GameToolModeType.BUILD);
		GameUI.m_Instance.m_HydraulicsController.gameObject.SetActive(!GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy);
	}

	private void UpdateFoundationHelpArrow()
	{
		bool active = GameManager.GetGameMode() == GameMode.CAMPAIGN && Campaign.m_CurrentLevel != null && Campaign.m_CurrentLevel.m_Id == "022" && !GameUI.m_Instance.m_LevelInfo.gameObject.activeInHierarchy && !Profiles.m_ActiveProfile.m_DismissedFoundationHelpArrow;
		m_FoundationHelpArrow.SetActive(active);
	}
}

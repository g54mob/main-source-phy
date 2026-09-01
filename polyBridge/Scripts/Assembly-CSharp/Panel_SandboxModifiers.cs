using UnityEngine;
using UnityEngine.UI;

public class Panel_SandboxModifiers : MonoBehaviour
{
	[Header("Rollouts")]
	public Rollout m_ModifiersRollout;

	[Header("Toggles")]
	public Toggle m_HydraulicControllerToggle;

	public Toggle m_UnbreakableToggle;

	public Toggle m_UnlimitedHeightFoundationsToggle;

	public Toggle m_NoWaterToggle;

	public Toggle m_NoReinforcedRoadToggle;

	public Toggle m_SpringAdjustmentsAllowedToggle;

	[Header("Fog")]
	public SandboxInputField m_InputFieldFogHeightStartMin;

	public SandboxInputField m_InputFieldFogHeightStartMax;

	public SandboxInputField m_InputFieldFogHeightEnd;

	[Header("Scrolling")]
	public ScrollRect m_ScrollRect;

	private PointerEvents m_HydraulicControllerTogglePointerEvents;

	private PointerEvents m_UnbreakableTogglePointerEvents;

	private PointerEvents m_UnlimitedHeightFoundationsTogglePointerEvents;

	private PointerEvents m_NoWaterTogglePointerEvents;

	private PointerEvents m_NoReinforcedRoadTogglePointerEvents;

	private PointerEvents m_SpringAdjustmentsAllowedTogglePointerEvents;

	private void Awake()
	{
		m_HydraulicControllerTogglePointerEvents = m_HydraulicControllerToggle.GetComponent<PointerEvents>();
		m_HydraulicControllerTogglePointerEvents.RegisterOnClickedDelegate(OnHydraulicControllerToggle);
		m_UnbreakableTogglePointerEvents = m_UnbreakableToggle.GetComponent<PointerEvents>();
		m_UnbreakableTogglePointerEvents.RegisterOnClickedDelegate(OnUnbreakableToggle);
		m_UnlimitedHeightFoundationsTogglePointerEvents = m_UnlimitedHeightFoundationsToggle.GetComponent<PointerEvents>();
		m_UnlimitedHeightFoundationsTogglePointerEvents.RegisterOnClickedDelegate(OnUnlimitedHeightFoundationsToggle);
		m_NoWaterTogglePointerEvents = m_NoWaterToggle.GetComponent<PointerEvents>();
		m_NoWaterTogglePointerEvents.RegisterOnClickedDelegate(OnNoWaterToggle);
		m_NoReinforcedRoadTogglePointerEvents = m_NoReinforcedRoadToggle.GetComponent<PointerEvents>();
		m_NoReinforcedRoadTogglePointerEvents.RegisterOnClickedDelegate(OnNoReinforcedRoadToggle);
		m_SpringAdjustmentsAllowedTogglePointerEvents = m_SpringAdjustmentsAllowedToggle.GetComponent<PointerEvents>();
		m_SpringAdjustmentsAllowedTogglePointerEvents.RegisterOnClickedDelegate(OnNoSpringAdjustmentsToggle);
	}

	private void OnEnable()
	{
		RefreshProperties();
	}

	private void Update()
	{
		m_ScrollRect.enabled = SandboxItems.m_NewUnPlacedItem == null;
		SetFogHeightVisibility();
	}

	public void RefreshProperties()
	{
		RefreshInputFields();
		RefreshToggles();
	}

	private void RefreshInputFields()
	{
		m_InputFieldFogHeightStartMin.m_InputField.text = Utils.FormatDistance(SandboxSettings.m_FogHeightMinWorldY);
		m_InputFieldFogHeightStartMax.m_InputField.text = Utils.FormatDistance(SandboxSettings.m_FogHeightMaxWorldY);
		m_InputFieldFogHeightEnd.m_InputField.text = Utils.FormatDistance(SandboxSettings.m_FogHeightEndRelativeY);
	}

	private void RefreshToggles()
	{
		m_HydraulicControllerToggle.isOn = SandboxSettings.m_HydraulicControllerEnabled;
		m_UnbreakableToggle.isOn = SandboxSettings.m_Unbreakable;
		m_UnlimitedHeightFoundationsToggle.isOn = SandboxSettings.m_UnlimitedHeightFoundations;
		m_NoWaterToggle.isOn = SandboxSettings.m_NoWater;
		m_NoReinforcedRoadToggle.isOn = SandboxSettings.m_NoReinforcedRoad;
		m_SpringAdjustmentsAllowedToggle.isOn = SandboxSettings.m_SpringAdjustmentsAllowed;
	}

	private void OnHydraulicControllerToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		SandboxSettings.m_HydraulicControllerEnabled = m_HydraulicControllerToggle.isOn;
		HydraulicsController.Reset();
		foreach (HydraulicsPhase phase in HydraulicsPhases.m_Phases)
		{
			HydraulicsController.AddAllPistonsToPhase(phase);
			HydraulicsController.AddAllSplitJointsToPhase(phase);
			HydraulicsController.EnableNewAdditionsFromPhase(phase);
		}
		SandboxUndo.SnapShot();
	}

	private void OnUnbreakableToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		SandboxSettings.m_Unbreakable = m_UnbreakableToggle.isOn;
		SandboxUndo.SnapShot();
	}

	private void OnUnlimitedHeightFoundationsToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		SandboxSettings.m_UnlimitedHeightFoundations = m_UnlimitedHeightFoundationsToggle.isOn;
		SandboxUndo.SnapShot();
	}

	private void OnNoWaterToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		SandboxSettings.m_NoWater = m_NoWaterToggle.isOn;
		SandboxUndo.SnapShot();
	}

	private void OnNoReinforcedRoadToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		SandboxSettings.m_NoReinforcedRoad = m_NoReinforcedRoadToggle.isOn;
		SandboxUndo.SnapShot();
	}

	private void OnNoSpringAdjustmentsToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		SandboxSettings.m_SpringAdjustmentsAllowed = m_SpringAdjustmentsAllowedToggle.isOn;
		if (!SandboxSettings.m_SpringAdjustmentsAllowed)
		{
			BridgeSprings.RemoveAllAdjustmentsForUnlocked();
		}
		SandboxUndo.SnapShot();
	}

	private void SetFogHeightVisibility()
	{
		m_InputFieldFogHeightStartMin.transform.parent.gameObject.SetActive(!m_NoWaterToggle.isOn);
		m_InputFieldFogHeightStartMax.transform.parent.gameObject.SetActive(!m_NoWaterToggle.isOn);
	}
}

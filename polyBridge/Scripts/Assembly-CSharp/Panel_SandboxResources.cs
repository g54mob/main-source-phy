using UnityEngine;
using UnityEngine.UI;

public class Panel_SandboxResources : MonoBehaviour
{
	[Header("Rollouts")]
	public Rollout m_MaterialTogglesRollout;

	[Header("Input Fields")]
	public SandboxInputField m_CashBudget;

	public SandboxInputField m_RoadBudget;

	public SandboxInputField m_WoodBudget;

	public SandboxInputField m_SteelBudget;

	public SandboxInputField m_HydraulicBudget;

	public SandboxInputField m_RopeBudget;

	public SandboxInputField m_CableBudget;

	public SandboxInputField m_SpringBudget;

	public SandboxInputField m_PillarBudget;

	[Header("Toggles")]
	public Toggle m_RoadToggle;

	public Toggle m_WoodToggle;

	public Toggle m_SteelToggle;

	public Toggle m_HydraulicToggle;

	public Toggle m_RopeToggle;

	public Toggle m_CableToggle;

	public Toggle m_SpringToggle;

	public Toggle m_PillarToggle;

	private PointerEvents m_WoodToggleEvents;

	private PointerEvents m_SteelToggleEvents;

	private PointerEvents m_HydraulicToggleEvents;

	private PointerEvents m_RopeToggleEvents;

	private PointerEvents m_CableToggleEvents;

	private PointerEvents m_SpringToggleEvents;

	private PointerEvents m_PillarToggleEvents;

	private void Awake()
	{
		m_WoodToggleEvents = m_WoodToggle.GetComponent<PointerEvents>();
		m_WoodToggleEvents.RegisterOnClickedDelegate(OnWoodToggle);
		m_SteelToggleEvents = m_SteelToggle.GetComponent<PointerEvents>();
		m_SteelToggleEvents.RegisterOnClickedDelegate(OnSteelToggle);
		m_HydraulicToggleEvents = m_HydraulicToggle.GetComponent<PointerEvents>();
		m_HydraulicToggleEvents.RegisterOnClickedDelegate(OnHydraulicToggle);
		m_RopeToggleEvents = m_RopeToggle.GetComponent<PointerEvents>();
		m_RopeToggleEvents.RegisterOnClickedDelegate(OnRopeToggle);
		m_CableToggleEvents = m_CableToggle.GetComponent<PointerEvents>();
		m_CableToggleEvents.RegisterOnClickedDelegate(OnCableToggle);
		m_SpringToggleEvents = m_SpringToggle.GetComponent<PointerEvents>();
		m_SpringToggleEvents.RegisterOnClickedDelegate(OnSpringToggle);
		m_PillarToggleEvents = m_PillarToggle.GetComponent<PointerEvents>();
		m_PillarToggleEvents.RegisterOnClickedDelegate(OnPillarToggle);
	}

	private void OnEnable()
	{
		RefreshProperties();
	}

	private void OnDisable()
	{
	}

	public void RefreshProperties()
	{
		RefreshInputFields();
		RefreshToggles();
	}

	public void ShowMaterialToggles()
	{
		EnableMaterialToggles(enable: true);
	}

	public void HideMaterialToggles()
	{
		EnableMaterialToggles(enable: false);
	}

	private void RefreshInputFields()
	{
		m_CashBudget.m_InputField.text = Utils.FormatCash(Budget.m_CashBudget);
		m_RoadBudget.m_InputField.text = Utils.FormatMaterialBudget(Budget.m_RoadBudget);
		m_WoodBudget.m_InputField.text = Utils.FormatMaterialBudget(Budget.m_WoodBudget);
		m_SteelBudget.m_InputField.text = Utils.FormatMaterialBudget(Budget.m_SteelBudget);
		m_HydraulicBudget.m_InputField.text = Utils.FormatMaterialBudget(Budget.m_HydraulicBudget);
		m_RopeBudget.m_InputField.text = Utils.FormatMaterialBudget(Budget.m_RopeBudget);
		m_CableBudget.m_InputField.text = Utils.FormatMaterialBudget(Budget.m_CableBudget);
		m_SpringBudget.m_InputField.text = Utils.FormatMaterialBudget(Budget.m_SpringBudget);
		m_PillarBudget.m_InputField.text = Utils.FormatMaterialBudget(Budget.m_PillarBudget);
	}

	private void RefreshToggles()
	{
		m_WoodToggle.isOn = Budget.m_AllowWood;
		m_SteelToggle.isOn = Budget.m_AllowSteel;
		m_HydraulicToggle.isOn = Budget.m_AllowHydraulic;
		m_RopeToggle.isOn = Budget.m_AllowRope;
		m_CableToggle.isOn = Budget.m_AllowCable;
		m_SpringToggle.isOn = Budget.m_AllowSpring;
		m_PillarToggle.isOn = Budget.m_AllowPillar;
	}

	private void OnWoodToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Budget.m_AllowWood = m_WoodToggle.isOn;
	}

	private void OnSteelToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Budget.m_AllowSteel = m_SteelToggle.isOn;
	}

	private void OnHydraulicToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Budget.m_AllowHydraulic = m_HydraulicToggle.isOn;
	}

	private void OnRopeToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Budget.m_AllowRope = m_RopeToggle.isOn;
	}

	private void OnCableToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Budget.m_AllowCable = m_CableToggle.isOn;
	}

	private void OnSpringToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Budget.m_AllowSpring = m_SpringToggle.isOn;
	}

	private void OnPillarToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Budget.m_AllowPillar = m_PillarToggle.isOn;
	}

	private void EnableMaterialToggles(bool enable)
	{
		m_MaterialTogglesRollout.gameObject.SetActive(enable);
	}
}

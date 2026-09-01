using UnityEngine;
using UnityEngine.UI;

public class Panel_SandboxEditVehicleStopTrigger : MonoBehaviour
{
	[Header("Nudge")]
	public Panel_SandboxNudge m_SandboxNudge;

	[Header("Input Fields")]
	public SandboxInputField m_InputFieldPosX;

	public SandboxInputField m_InputFieldPosY;

	[Header("Sliders")]
	public SandboxTapeSlider m_SliderRot;

	public SandboxTapeSlider m_SliderHeight;

	[Header("Toggles")]
	public Toggle m_FlipToggle;

	public Toggle m_InvisibleInSimToggle;

	private VehicleStopTrigger m_LastRefreshedTrigger;

	private PointerEvents m_FlipTogglePointerEvents;

	private PointerEvents m_InvisibleInSimPointerEvents;

	private bool m_SkipInputFieldUpdateFromSlider;

	private void Awake()
	{
		m_FlipTogglePointerEvents = m_FlipToggle.GetComponent<PointerEvents>();
		m_FlipTogglePointerEvents.RegisterOnClickedDelegate(OnFlipToggle);
		m_SliderRot.SetRange(-180f, 180f, 1f);
		m_SliderRot.SetCallback(RotSliderChanged);
		m_SliderHeight.SetRange(VehicleStopTriggers.MIN_HEIGHT_SLIDER, VehicleStopTriggers.MAX_HEIGHT_SLIDER, GameGrid.m_Spacing);
		m_SliderHeight.SetCallback(HeightSliderChanged);
		m_InvisibleInSimPointerEvents = m_InvisibleInSimToggle.GetComponent<PointerEvents>();
		m_InvisibleInSimPointerEvents.RegisterOnClickedDelegate(OnInvisibleInSimToggle);
	}

	private void Update()
	{
		VehicleStopTrigger selectedVehicleStopTrigger = SandboxSelectionSet.GetSelectedVehicleStopTrigger();
		if ((bool)selectedVehicleStopTrigger && selectedVehicleStopTrigger != m_LastRefreshedTrigger)
		{
			RefreshProperties(selectedVehicleStopTrigger);
		}
		ProcessInput(selectedVehicleStopTrigger);
	}

	private void OnEnable()
	{
		VehicleStopTrigger selectedVehicleStopTrigger = SandboxSelectionSet.GetSelectedVehicleStopTrigger();
		if ((bool)selectedVehicleStopTrigger)
		{
			RefreshProperties(selectedVehicleStopTrigger);
		}
	}

	private void OnDisable()
	{
		m_LastRefreshedTrigger = null;
		m_SliderRot.m_SandboxInputField.m_ExternalContinuousHoldActive = false;
		m_SliderHeight.m_SandboxInputField.m_ExternalContinuousHoldActive = false;
	}

	public void UpdateForCurrentDevice()
	{
		m_SandboxNudge.UpdateForCurrentDevice();
	}

	public void SkipInputFieldUpdateFromSlider()
	{
		m_SkipInputFieldUpdateFromSlider = true;
	}

	public void ForceRefresh()
	{
		m_LastRefreshedTrigger = null;
	}

	public void RefreshPosition(VehicleStopTrigger trigger)
	{
		m_InputFieldPosX.m_InputField.text = Utils.FormatThreeDecimalPlaces(trigger.transform.position.x);
		m_InputFieldPosY.m_InputField.text = Utils.FormatThreeDecimalPlaces(trigger.transform.position.y);
	}

	private void RefreshSliders(VehicleStopTrigger trigger)
	{
		m_SliderRot.SetValue(trigger.m_RotationDegrees);
		m_SliderRot.m_SandboxInputField.m_InputField.text = Utils.FormatAngle(trigger.m_RotationDegrees);
		m_SliderHeight.SetValue(trigger.m_Height);
		m_SliderHeight.m_SandboxInputField.m_InputField.text = Utils.FormatDistance(trigger.m_Height);
	}

	private void RefreshToggles(VehicleStopTrigger trigger)
	{
		m_FlipToggle.isOn = trigger.m_Flipped;
		m_InvisibleInSimToggle.isOn = trigger.m_InvisibleInSim;
	}

	public void RefreshProperties(VehicleStopTrigger trigger)
	{
		if ((bool)trigger)
		{
			RefreshPosition(trigger);
			RefreshToggles(trigger);
			RefreshSliders(trigger);
			m_LastRefreshedTrigger = trigger;
		}
	}

	public void OnStopVehicleChanged()
	{
		InterfaceAudio.Play("ui_menu_select");
		_ = (bool)SandboxSelectionSet.GetSelectedVehicleStopTrigger();
	}

	private void OnFlipToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		VehicleStopTrigger selectedVehicleStopTrigger = SandboxSelectionSet.GetSelectedVehicleStopTrigger();
		if ((bool)selectedVehicleStopTrigger)
		{
			selectedVehicleStopTrigger.m_Flipped = m_FlipToggle.isOn;
			selectedVehicleStopTrigger.m_SandboxItem.SetOutlineDirty(dirty: true);
			SetLocalScale(selectedVehicleStopTrigger, selectedVehicleStopTrigger.m_Flipped);
			SandboxUndo.SnapShot();
		}
	}

	private void OnInvisibleInSimToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		VehicleStopTrigger selectedVehicleStopTrigger = SandboxSelectionSet.GetSelectedVehicleStopTrigger();
		if ((bool)selectedVehicleStopTrigger)
		{
			selectedVehicleStopTrigger.m_InvisibleInSim = m_InvisibleInSimToggle.isOn;
			selectedVehicleStopTrigger.m_SandboxItem.m_Label.m_InvisibleIcon.gameObject.SetActive(m_InvisibleInSimToggle.isOn);
			SandboxUndo.SnapShot();
		}
	}

	private void SetLocalScale(VehicleStopTrigger trigger, bool flipped)
	{
		trigger.m_PoleAndFlag.transform.localScale = new Vector3(trigger.m_Flipped ? (-1f) : 1f, 1f, 1f);
	}

	private void ProcessInput(VehicleStopTrigger trigger)
	{
		if ((bool)trigger && !GameStateCommonInput.IgnoreKeyboardInput())
		{
			if (GameInput.JustPressed(BindingType.FLIP_HORIZONTAL))
			{
				InterfaceAudio.Play("ui_settings_toggle");
				trigger.m_Flipped = !trigger.m_Flipped;
				m_FlipToggle.isOn = trigger.m_Flipped;
				SetLocalScale(trigger, trigger.m_Flipped);
			}
			m_SliderRot.m_SandboxInputField.ProcessInputForRotation();
		}
	}

	private void RotSliderChanged(float angle)
	{
		VehicleStopTrigger selectedVehicleStopTrigger = SandboxSelectionSet.GetSelectedVehicleStopTrigger();
		if ((bool)selectedVehicleStopTrigger)
		{
			selectedVehicleStopTrigger.m_RotationDegrees = angle % 360f;
			selectedVehicleStopTrigger.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f - selectedVehicleStopTrigger.m_RotationDegrees));
			if (selectedVehicleStopTrigger.m_SandboxItem != null)
			{
				selectedVehicleStopTrigger.m_SandboxItem.SetFloatingTextToDefaultPosition();
				SandboxItems.ResolveOverlappingFloatingText();
			}
			selectedVehicleStopTrigger.m_SandboxItem.SetOutlineDirty(dirty: true);
			m_SliderRot.m_SandboxInputField.m_InputField.text = Utils.FormatAngle(selectedVehicleStopTrigger.m_RotationDegrees);
		}
	}

	private void HeightSliderChanged(float height)
	{
		if (m_SkipInputFieldUpdateFromSlider)
		{
			m_SkipInputFieldUpdateFromSlider = false;
			return;
		}
		VehicleStopTrigger selectedVehicleStopTrigger = SandboxSelectionSet.GetSelectedVehicleStopTrigger();
		if ((bool)selectedVehicleStopTrigger)
		{
			selectedVehicleStopTrigger.m_Height = Mathf.Clamp(height, VehicleStopTriggers.MIN_HEIGHT, VehicleStopTriggers.MAX_HEIGHT);
			selectedVehicleStopTrigger.SetPoleScaleForHeight(selectedVehicleStopTrigger.m_Height);
			selectedVehicleStopTrigger.m_SandboxItem.SetOutlineDirty(dirty: true);
			SandboxItems.ResolveOverlappingFloatingText();
			m_SliderHeight.m_SandboxInputField.m_InputField.text = Utils.FormatDistance(selectedVehicleStopTrigger.m_Height);
		}
	}
}

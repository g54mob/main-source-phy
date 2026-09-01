using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel_SandboxEditDecor : MonoBehaviour
{
	public Image m_Icon;

	public TextMeshProUGUI m_Title;

	[Header("Input Fields")]
	public SandboxInputField m_InputFieldPosX;

	public SandboxInputField m_InputFieldPosY;

	public SandboxInputField m_InputFieldPosZ;

	[Header("Nudge")]
	public Panel_SandboxNudge m_SandboxNudge;

	[Header("Toggles")]
	public Toggle m_UniformScaleToggle;

	public Toggle m_ShowInBuildModeToggle;

	[Header("Buttons")]
	public Button m_Duplicate;

	public Button m_Delete;

	[Header("Rot Sliders")]
	public SandboxTapeSlider m_SliderPitch;

	public SandboxTapeSlider m_SliderHeading;

	public SandboxTapeSlider m_SliderRoll;

	[Header("Scale Sliders")]
	public SandboxTapeSlider m_SliderScaleX;

	public SandboxTapeSlider m_SliderScaleY;

	public SandboxTapeSlider m_SliderScaleZ;

	private PointerEvents m_ShowInBuildModeTogglePointerEvents;

	private PointerEvents m_UniformScaleTogglePointerEvents;

	private Decor m_LastRefreshedSupportDecor;

	private bool m_SkipInputFieldUpdateFromSlider;

	private void Awake()
	{
		m_ShowInBuildModeTogglePointerEvents = m_ShowInBuildModeToggle.GetComponent<PointerEvents>();
		m_ShowInBuildModeTogglePointerEvents.RegisterOnClickedDelegate(OnShowInBuildModeToggle);
		m_UniformScaleTogglePointerEvents = m_UniformScaleToggle.GetComponent<PointerEvents>();
		m_UniformScaleTogglePointerEvents.RegisterOnClickedDelegate(OnUniformScaleToggle);
		m_SliderPitch.SetRange(-180f, 180f, 1f);
		m_SliderPitch.SetCallback(PitchSliderChanged);
		m_SliderHeading.SetRange(-180f, 180f, 1f);
		m_SliderHeading.SetCallback(HeadingSliderChanged);
		m_SliderRoll.SetRange(-180f, 180f, 1f);
		m_SliderRoll.SetCallback(RollSliderChanged);
		m_SliderScaleX.SetRange(Decors.MIN_NORMALIZED_SCALE_SLIDER_X * 100f, Decors.MAX_NORMALIZED_SCALE_SLIDER_X * 100f, 1f);
		m_SliderScaleX.SetCallback(ScaleXSliderChanged);
		m_SliderScaleY.SetRange(Decors.MIN_NORMALIZED_SCALE_SLIDER_Y * 100f, Decors.MAX_NORMALIZED_SCALE_SLIDER_Y * 100f, 1f);
		m_SliderScaleY.SetCallback(ScaleYSliderChanged);
		m_SliderScaleZ.SetRange(Decors.MIN_NORMALIZED_SCALE_SLIDER_Z * 100f, Decors.MAX_NORMALIZED_SCALE_SLIDER_Z * 100f, 1f);
		m_SliderScaleZ.SetCallback(ScaleZSliderChanged);
	}

	private void Update()
	{
		Decor selectedDecor = SandboxSelectionSet.GetSelectedDecor();
		if ((bool)selectedDecor && selectedDecor != m_LastRefreshedSupportDecor)
		{
			RefreshProperties(selectedDecor);
		}
		ProcessInput(selectedDecor);
	}

	public void UpdateForCurrentDevice()
	{
		m_SandboxNudge.UpdateForCurrentDevice();
	}

	private void OnEnable()
	{
		m_Duplicate.onClick.AddListener(OnDuplicate);
		m_Delete.onClick.AddListener(OnDelete);
		Decor selectedDecor = SandboxSelectionSet.GetSelectedDecor();
		if ((bool)selectedDecor)
		{
			RefreshProperties(selectedDecor);
		}
	}

	private void OnDisable()
	{
		m_LastRefreshedSupportDecor = null;
		m_Duplicate.onClick.RemoveAllListeners();
		m_Delete.onClick.RemoveAllListeners();
		m_SliderPitch.m_SandboxInputField.m_ExternalContinuousHoldActive = false;
		m_SliderHeading.m_SandboxInputField.m_ExternalContinuousHoldActive = false;
		m_SliderRoll.m_SandboxInputField.m_ExternalContinuousHoldActive = false;
	}

	public void SkipInputFieldUpdateFromSlider()
	{
		m_SkipInputFieldUpdateFromSlider = true;
	}

	public void ForceRefresh()
	{
		m_LastRefreshedSupportDecor = null;
	}

	public void RefreshProperties(Decor decor)
	{
		if ((bool)decor)
		{
			m_Icon.sprite = decor.GetStub().m_Sprite;
			m_Title.text = decor.GetLocalizedName();
			RefreshPosition(decor);
			RefreshToggles(decor);
			RefreshSliders(decor);
			m_LastRefreshedSupportDecor = decor;
		}
	}

	public void RefreshPosition(Decor decor)
	{
		m_InputFieldPosX.m_InputField.text = Utils.FormatThreeDecimalPlaces(decor.transform.position.x);
		m_InputFieldPosY.m_InputField.text = Utils.FormatThreeDecimalPlaces(decor.transform.position.y);
		m_InputFieldPosZ.m_InputField.text = Utils.FormatThreeDecimalPlaces(decor.transform.position.z);
	}

	private void RefreshToggles(Decor decor)
	{
		m_ShowInBuildModeToggle.isOn = decor.m_ShowInBuildMode;
		m_UniformScaleToggle.isOn = decor.m_UniformScale;
	}

	public void RefreshSliders(Decor decor)
	{
		m_SliderPitch.SetValue(decor.m_PitchRotationDegrees);
		m_SliderPitch.m_SandboxInputField.m_InputField.text = Utils.FormatAngle(decor.m_PitchRotationDegrees);
		m_SliderHeading.SetValue(decor.m_HeadingRotationDegrees);
		m_SliderHeading.m_SandboxInputField.m_InputField.text = Utils.FormatAngle(decor.m_HeadingRotationDegrees);
		m_SliderRoll.SetValue(decor.m_RollRotationDegrees);
		m_SliderRoll.m_SandboxInputField.m_InputField.text = Utils.FormatAngle(decor.m_RollRotationDegrees);
		m_SliderScaleX.SetValue(Mathf.Abs(decor.transform.localScale.x) * 100f);
		m_SliderScaleX.m_SandboxInputField.m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(Mathf.Abs(decor.transform.localScale.x));
		m_SliderScaleY.SetValue(Mathf.Abs(decor.transform.localScale.y) * 100f);
		m_SliderScaleY.m_SandboxInputField.m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(Mathf.Abs(decor.transform.localScale.y));
		m_SliderScaleZ.SetValue(Mathf.Abs(decor.transform.localScale.z) * 100f);
		m_SliderScaleZ.m_SandboxInputField.m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(Mathf.Abs(decor.transform.localScale.z));
	}

	private void OnDuplicate()
	{
		Decor selectedDecor = SandboxSelectionSet.GetSelectedDecor();
		if (!selectedDecor)
		{
			return;
		}
		DecorStub stub = selectedDecor.GetStub();
		if (stub == null)
		{
			return;
		}
		Vector3 offset = new Vector3(selectedDecor.GetDuplicateOffset(), 0f, 0f);
		GameObject asyncPrefab = Prefabs.GetAsyncPrefab(stub.m_PrefabAddress);
		if (asyncPrefab == null)
		{
			Debug.LogWarningFormat("Could not find preloaded decor prefab with address " + stub.m_PrefabAddress);
			return;
		}
		Decor decor = selectedDecor.Duplicate(asyncPrefab, stub.m_PrefabAddress, stub.m_ModId, offset);
		if ((bool)decor)
		{
			InterfaceAudio.Play("ui_build_generic_place");
			SandboxSelectionSet.ForceSelection(decor.m_SandboxItem);
			SandboxUndo.SnapShot();
		}
	}

	private void OnDelete()
	{
		if ((bool)SandboxSelectionSet.GetSelectedDecor())
		{
			InterfaceAudio.Play("ui_build_delete");
			SandboxSelectionSet.Delete();
		}
	}

	private void ProcessInput(Decor decor)
	{
		if ((bool)decor && !GameStateCommonInput.IgnoreKeyboardInput())
		{
			m_SliderPitch.m_SandboxInputField.ProcessInputForRotation();
			m_SliderHeading.m_SandboxInputField.ProcessInputForRotation();
			m_SliderRoll.m_SandboxInputField.ProcessInputForRotation();
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.WEST))
			{
				ExecuteEvents.Execute(m_Delete.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
			}
			else if (GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH))
			{
				ExecuteEvents.Execute(m_Duplicate.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
			}
		}
	}

	private void OnShowInBuildModeToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Decor selectedDecor = SandboxSelectionSet.GetSelectedDecor();
		if ((bool)selectedDecor)
		{
			selectedDecor.m_ShowInBuildMode = m_ShowInBuildModeToggle.isOn;
			SandboxUndo.SnapShot();
		}
	}

	private void OnUniformScaleToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Decor selectedDecor = SandboxSelectionSet.GetSelectedDecor();
		if ((bool)selectedDecor)
		{
			selectedDecor.m_UniformScale = m_UniformScaleToggle.isOn;
			SandboxUndo.SnapShot();
		}
	}

	private void PitchSliderChanged(float angle)
	{
		Decor selectedDecor = SandboxSelectionSet.GetSelectedDecor();
		if ((bool)selectedDecor)
		{
			selectedDecor.m_PitchRotationDegrees = angle % 360f;
			selectedDecor.transform.rotation = Quaternion.Euler(new Vector3(0f - selectedDecor.m_PitchRotationDegrees, 0f - selectedDecor.m_HeadingRotationDegrees, 0f - selectedDecor.m_RollRotationDegrees));
			m_SliderPitch.m_SandboxInputField.m_InputField.text = Utils.FormatAngle(selectedDecor.m_PitchRotationDegrees);
		}
	}

	private void HeadingSliderChanged(float angle)
	{
		Decor selectedDecor = SandboxSelectionSet.GetSelectedDecor();
		if ((bool)selectedDecor)
		{
			selectedDecor.m_HeadingRotationDegrees = angle % 360f;
			selectedDecor.transform.rotation = Quaternion.Euler(new Vector3(0f - selectedDecor.m_PitchRotationDegrees, 0f - selectedDecor.m_HeadingRotationDegrees, 0f - selectedDecor.m_RollRotationDegrees));
			m_SliderHeading.m_SandboxInputField.m_InputField.text = Utils.FormatAngle(selectedDecor.m_HeadingRotationDegrees);
		}
	}

	private void RollSliderChanged(float angle)
	{
		Decor selectedDecor = SandboxSelectionSet.GetSelectedDecor();
		if ((bool)selectedDecor)
		{
			selectedDecor.m_RollRotationDegrees = angle % 360f;
			selectedDecor.transform.rotation = Quaternion.Euler(new Vector3(0f - selectedDecor.m_PitchRotationDegrees, 0f - selectedDecor.m_HeadingRotationDegrees, 0f - selectedDecor.m_RollRotationDegrees));
			m_SliderRoll.m_SandboxInputField.m_InputField.text = Utils.FormatAngle(selectedDecor.m_RollRotationDegrees);
		}
	}

	private void ScaleXSliderChanged(float percentage)
	{
		if (m_SkipInputFieldUpdateFromSlider)
		{
			m_SkipInputFieldUpdateFromSlider = false;
			return;
		}
		Decor selectedDecor = SandboxSelectionSet.GetSelectedDecor();
		if ((bool)selectedDecor)
		{
			float num = Mathf.Clamp(percentage / 100f, Decors.MIN_NORMALIZED_SCALE, Decors.MAX_NORMALIZED_SCALE_X);
			if (!Mathf.Approximately(num, 0f))
			{
				Vector3 localScale = new Vector3(num, selectedDecor.transform.localScale.y, selectedDecor.transform.localScale.z);
				selectedDecor.transform.localScale = localScale;
				m_SliderScaleX.m_SandboxInputField.m_InputField.text = Utils.FormatPercentage(num);
			}
			if (selectedDecor.m_UniformScale)
			{
				selectedDecor.m_UniformScale = false;
				m_SliderScaleY.SetValue(percentage);
				m_SliderScaleZ.SetValue(percentage);
				ScaleYSliderChanged(percentage);
				ScaleZSliderChanged(percentage);
				selectedDecor.m_UniformScale = true;
			}
		}
	}

	private void ScaleYSliderChanged(float percentage)
	{
		if (m_SkipInputFieldUpdateFromSlider)
		{
			m_SkipInputFieldUpdateFromSlider = false;
			return;
		}
		Decor selectedDecor = SandboxSelectionSet.GetSelectedDecor();
		if ((bool)selectedDecor)
		{
			float num = Mathf.Clamp(percentage / 100f, Decors.MIN_NORMALIZED_SCALE, Decors.MAX_NORMALIZED_SCALE_Y);
			if (!Mathf.Approximately(num, 0f))
			{
				Vector3 localScale = new Vector3(selectedDecor.transform.localScale.x, num, selectedDecor.transform.localScale.z);
				selectedDecor.transform.localScale = localScale;
				m_SliderScaleY.m_SandboxInputField.m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(num);
			}
			if (selectedDecor.m_UniformScale)
			{
				selectedDecor.m_UniformScale = false;
				m_SliderScaleX.SetValue(percentage);
				m_SliderScaleZ.SetValue(percentage);
				ScaleXSliderChanged(percentage);
				ScaleZSliderChanged(percentage);
				selectedDecor.m_UniformScale = true;
			}
		}
	}

	private void ScaleZSliderChanged(float percentage)
	{
		if (m_SkipInputFieldUpdateFromSlider)
		{
			m_SkipInputFieldUpdateFromSlider = false;
			return;
		}
		Decor selectedDecor = SandboxSelectionSet.GetSelectedDecor();
		if ((bool)selectedDecor)
		{
			float num = Mathf.Clamp(percentage / 100f, Decors.MIN_NORMALIZED_SCALE, Decors.MAX_NORMALIZED_SCALE_Z);
			if (!Mathf.Approximately(num, 0f))
			{
				Vector3 localScale = new Vector3(selectedDecor.transform.localScale.x, selectedDecor.transform.localScale.y, num);
				selectedDecor.transform.localScale = localScale;
				m_SliderScaleZ.m_SandboxInputField.m_InputField.text = Utils.FormatPercentage(num);
			}
			if (selectedDecor.m_UniformScale)
			{
				selectedDecor.m_UniformScale = false;
				m_SliderScaleX.SetValue(percentage);
				m_SliderScaleY.SetValue(percentage);
				ScaleXSliderChanged(percentage);
				ScaleYSliderChanged(percentage);
				selectedDecor.m_UniformScale = true;
			}
		}
	}
}

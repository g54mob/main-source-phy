using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel_SandboxEditRock : MonoBehaviour
{
	[Header("Header")]
	public Image m_Icon;

	[Header("Nudge")]
	public Panel_SandboxNudge m_SandboxNudge;

	[Header("Input Fields")]
	public SandboxInputField m_InputFieldPosX;

	public SandboxInputField m_InputFieldPosY;

	[Header("Toggles")]
	public Toggle m_UniformScaleToggle;

	public Toggle m_FlipToggle;

	public Toggle m_LockToBottomToggle;

	[Header("Buttons")]
	public Button m_Duplicate;

	public Button m_Delete;

	[Header("Sliders")]
	public SandboxTapeSlider m_SliderScaleX;

	public SandboxTapeSlider m_SliderScaleY;

	public SandboxTapeSlider m_SliderScaleZ;

	private PointerEvents m_UniformScaleTogglePointerEvents;

	private PointerEvents m_FlipTogglePointerEvents;

	private PointerEvents m_LockToBottomTogglePointerEvents;

	private Rock m_LastRefreshedRock;

	private bool m_SkipInputFieldUpdateFromSlider;

	private void Awake()
	{
		m_UniformScaleTogglePointerEvents = m_UniformScaleToggle.GetComponent<PointerEvents>();
		m_UniformScaleTogglePointerEvents.RegisterOnClickedDelegate(OnUniformScaleToggle);
		m_FlipTogglePointerEvents = m_FlipToggle.GetComponent<PointerEvents>();
		m_FlipTogglePointerEvents.RegisterOnClickedDelegate(OnFlipToggle);
		m_LockToBottomTogglePointerEvents = m_LockToBottomToggle.GetComponent<PointerEvents>();
		m_LockToBottomTogglePointerEvents.RegisterOnClickedDelegate(OnLockToBottomToggle);
		m_SliderScaleX.SetRange(Rocks.MIN_NORMALIZED_SCALE_SLIDER_X * 100f, Rocks.MAX_NORMALIZED_SCALE_SLIDER_X * 100f, 1f);
		m_SliderScaleX.SetCallback(ScaleXSliderChanged);
		m_SliderScaleY.SetRange(Rocks.MIN_NORMALIZED_SCALE_SLIDER_Y * 100f, Rocks.MAX_NORMALIZED_SCALE_SLIDER_Y * 100f, 1f);
		m_SliderScaleY.SetCallback(ScaleYSliderChanged);
		m_SliderScaleZ.SetRange(Rocks.MIN_NORMALIZED_SCALE_SLIDER_Z * 100f, Rocks.MAX_NORMALIZED_SCALE_SLIDER_Z * 100f, 1f);
		m_SliderScaleZ.SetCallback(ScaleZSliderChanged);
	}

	private void Update()
	{
		Rock selectedRock = SandboxSelectionSet.GetSelectedRock();
		if ((bool)selectedRock && selectedRock != m_LastRefreshedRock)
		{
			RefreshProperties(selectedRock);
		}
		ProcessInput(selectedRock);
		if (selectedRock.m_LockToBottom && !Mathf.Approximately(selectedRock.transform.position.y, 0f))
		{
			selectedRock.transform.Translate(0f, 0f - selectedRock.transform.position.y, 0f);
			selectedRock.m_SandboxItem.SetOutlineDirty(dirty: true);
			selectedRock.UpdatePolygonShapes();
		}
	}

	private void OnEnable()
	{
		m_Duplicate.onClick.AddListener(OnDuplicate);
		m_Delete.onClick.AddListener(OnDelete);
		Rock selectedRock = SandboxSelectionSet.GetSelectedRock();
		if ((bool)selectedRock)
		{
			RefreshProperties(selectedRock);
		}
	}

	private void OnDisable()
	{
		m_LastRefreshedRock = null;
		m_Duplicate.onClick.RemoveAllListeners();
		m_Delete.onClick.RemoveAllListeners();
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
		m_LastRefreshedRock = null;
	}

	public void RefreshProperties(Rock rock)
	{
		if ((bool)rock)
		{
			RefreshPosition(rock);
			RefreshToggles(rock);
			RefreshSliders(rock);
			RefreshIcon(rock);
			m_LastRefreshedRock = rock;
		}
	}

	public void RefreshPosition(Rock rock)
	{
		m_InputFieldPosX.m_InputField.text = Utils.FormatThreeDecimalPlaces(rock.transform.position.x);
		m_InputFieldPosY.m_InputField.text = Utils.FormatThreeDecimalPlaces(rock.transform.position.y);
	}

	private void RefreshToggles(Rock rock)
	{
		m_UniformScaleToggle.isOn = rock.m_UniformScale;
		m_FlipToggle.isOn = rock.m_MeshRenderer.transform.localScale.x < 0f;
		m_LockToBottomToggle.isOn = rock.m_LockToBottom;
	}

	private void RefreshSliders(Rock rock)
	{
		m_SliderScaleX.SetValue(Mathf.Abs(rock.transform.localScale.x) * 100f);
		m_SliderScaleX.m_SandboxInputField.m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(Mathf.Abs(rock.transform.localScale.x));
		m_SliderScaleY.SetValue(Mathf.Abs(rock.transform.localScale.y) * 100f);
		m_SliderScaleY.m_SandboxInputField.m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(Mathf.Abs(rock.transform.localScale.y));
		m_SliderScaleZ.SetValue(Mathf.Abs(rock.transform.localScale.z) * 100f);
		m_SliderScaleZ.m_SandboxInputField.m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(Mathf.Abs(rock.transform.localScale.z));
	}

	private void RefreshIcon(Rock rock)
	{
		m_Icon.sprite = rock.m_Sprite;
	}

	private void OnDuplicate()
	{
		Rock selectedRock = SandboxSelectionSet.GetSelectedRock();
		if ((bool)selectedRock && Prefabs.m_PrefabsDict.ContainsKey(selectedRock.name))
		{
			Rock rock = selectedRock.Duplicate(Prefabs.m_PrefabsDict[selectedRock.name], new Vector3(selectedRock.m_MeshRenderer.bounds.size.x, 0f, 0f));
			if ((bool)rock)
			{
				InterfaceAudio.Play("ui_build_terrain_place");
				SandboxSelectionSet.ForceSelection(rock.m_SandboxItem);
				SandboxUndo.SnapShot();
			}
		}
	}

	private void OnUniformScaleToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Rock selectedRock = SandboxSelectionSet.GetSelectedRock();
		if ((bool)selectedRock)
		{
			selectedRock.m_UniformScale = m_UniformScaleToggle.isOn;
			SandboxUndo.SnapShot();
		}
	}

	private void OnFlipToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Rock selectedRock = SandboxSelectionSet.GetSelectedRock();
		if ((bool)selectedRock)
		{
			selectedRock.Flip(m_FlipToggle.isOn);
			selectedRock.UpdatePolygonShapes();
			SandboxUndo.SnapShot();
		}
	}

	private void OnLockToBottomToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Rock selectedRock = SandboxSelectionSet.GetSelectedRock();
		if ((bool)selectedRock)
		{
			selectedRock.m_LockToBottom = m_LockToBottomToggle.isOn;
			SandboxUndo.SnapShot();
		}
	}

	private void OnDelete()
	{
		if ((bool)SandboxSelectionSet.GetSelectedRock())
		{
			InterfaceAudio.Play("ui_build_delete");
			SandboxSelectionSet.Delete();
		}
	}

	private void ProcessInput(Rock rock)
	{
		if ((bool)rock && !GameStateCommonInput.IgnoreKeyboardInput())
		{
			if (GameInput.JustPressed(BindingType.FLIP_HORIZONTAL))
			{
				InterfaceAudio.Play("ui_settings_toggle");
				m_FlipToggle.isOn = !m_FlipToggle.isOn;
				rock.Flip(m_FlipToggle.isOn);
				rock.UpdatePolygonShapes();
			}
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

	private void ScaleXSliderChanged(float percentage)
	{
		if (m_SkipInputFieldUpdateFromSlider)
		{
			m_SkipInputFieldUpdateFromSlider = false;
			return;
		}
		Rock selectedRock = SandboxSelectionSet.GetSelectedRock();
		if ((bool)selectedRock)
		{
			float num = Mathf.Clamp(percentage / 100f, Rocks.MIN_NORMALIZED_SCALE, Rocks.MAX_NORMALIZED_SCALE_X);
			if (!Mathf.Approximately(num, 0f))
			{
				Vector3 localScale = new Vector3(num, selectedRock.transform.localScale.y, selectedRock.transform.localScale.z);
				selectedRock.transform.localScale = localScale;
				selectedRock.UpdatePolygonShapes();
				m_SliderScaleX.m_SandboxInputField.m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(num);
			}
			if (selectedRock.m_UniformScale)
			{
				selectedRock.m_UniformScale = false;
				m_SliderScaleY.SetValue(percentage);
				m_SliderScaleZ.SetValue(percentage);
				ScaleYSliderChanged(percentage);
				ScaleZSliderChanged(percentage);
				selectedRock.m_UniformScale = true;
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
		Rock selectedRock = SandboxSelectionSet.GetSelectedRock();
		if ((bool)selectedRock)
		{
			float num = Mathf.Clamp(percentage / 100f, Rocks.MIN_NORMALIZED_SCALE, Rocks.MAX_NORMALIZED_SCALE_Y);
			if (!Mathf.Approximately(num, 0f))
			{
				Vector3 localScale = new Vector3(selectedRock.transform.localScale.x, num, selectedRock.transform.localScale.z);
				selectedRock.transform.localScale = localScale;
				selectedRock.UpdatePolygonShapes();
				m_SliderScaleY.m_SandboxInputField.m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(num);
			}
			if (selectedRock.m_UniformScale)
			{
				selectedRock.m_UniformScale = false;
				m_SliderScaleX.SetValue(percentage);
				m_SliderScaleZ.SetValue(percentage);
				ScaleXSliderChanged(percentage);
				ScaleZSliderChanged(percentage);
				selectedRock.m_UniformScale = true;
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
		Rock selectedRock = SandboxSelectionSet.GetSelectedRock();
		if ((bool)selectedRock)
		{
			float num = Mathf.Clamp(percentage / 100f, Rocks.MIN_NORMALIZED_SCALE, Rocks.MAX_NORMALIZED_SCALE_Z);
			if (!Mathf.Approximately(num, 0f))
			{
				Vector3 localScale = new Vector3(selectedRock.transform.localScale.x, selectedRock.transform.localScale.y, num);
				selectedRock.transform.localScale = localScale;
				selectedRock.UpdatePolygonShapes();
				m_SliderScaleZ.m_SandboxInputField.m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(num);
			}
			if (selectedRock.m_UniformScale)
			{
				selectedRock.m_UniformScale = false;
				m_SliderScaleX.SetValue(percentage);
				m_SliderScaleY.SetValue(percentage);
				ScaleXSliderChanged(percentage);
				ScaleYSliderChanged(percentage);
				selectedRock.m_UniformScale = true;
			}
		}
	}
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel_SandboxEditPlatform : MonoBehaviour
{
	[Header("Nudge")]
	public Panel_SandboxNudge m_SandboxNudge;

	[Header("Input Fields")]
	public SandboxInputField m_InputFieldPosX;

	public SandboxInputField m_InputFieldPosY;

	public SandboxInputField m_InputFieldWidth;

	public SandboxInputField m_InputFieldHeight;

	[Header("Flip")]
	public Toggle m_FlipToggle;

	[Header("Buttons")]
	public Button m_DuplicateButton;

	public Button m_DeleteButton;

	[Header("Sliders")]
	public SandboxTapeSlider m_SliderWidth;

	public SandboxTapeSlider m_SliderHeight;

	private PointerEvents m_FlipTogglePointerEvents;

	private Platform m_LastRefreshedPlatform;

	private bool m_SkipInputFieldUpdateFromSlider;

	private void Awake()
	{
		m_DuplicateButton.onClick.AddListener(OnDuplicate);
		m_DeleteButton.onClick.AddListener(OnDelete);
		m_FlipTogglePointerEvents = m_FlipToggle.GetComponent<PointerEvents>();
		m_FlipTogglePointerEvents.RegisterOnClickedDelegate(OnFlipToggle);
		m_SliderWidth.SetRange(Platforms.MIN_WIDTH_SLIDER, Platforms.MAX_WIDTH_SLIDER, 0.5f);
		m_SliderWidth.SetCallback(WidthSliderChanged);
		m_SliderHeight.SetRange(Platforms.MIN_HEIGHT_SLIDER, Platforms.MAX_HEIGHT_SLIDER, 0.5f);
		m_SliderHeight.SetCallback(HeightSliderChanged);
	}

	private void Update()
	{
		Platform selectedPlatform = SandboxSelectionSet.GetSelectedPlatform();
		if ((bool)selectedPlatform && selectedPlatform != m_LastRefreshedPlatform)
		{
			RefreshProperties(selectedPlatform);
		}
		ProcessInput(selectedPlatform);
	}

	private void OnEnable()
	{
		Platform selectedPlatform = SandboxSelectionSet.GetSelectedPlatform();
		if ((bool)selectedPlatform)
		{
			RefreshProperties(selectedPlatform);
		}
	}

	private void OnDisable()
	{
		m_LastRefreshedPlatform = null;
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
		m_LastRefreshedPlatform = null;
	}

	public void OnDelete()
	{
		if ((bool)SandboxSelectionSet.GetSelectedPlatform())
		{
			InterfaceAudio.Play("ui_build_delete");
			SandboxSelectionSet.Delete();
		}
	}

	public void OnDuplicate()
	{
		Platform selectedPlatform = SandboxSelectionSet.GetSelectedPlatform();
		if (!selectedPlatform)
		{
			return;
		}
		Platform platform = selectedPlatform.Duplicate(new Vector3(1f, -1f, 0f));
		if ((bool)platform)
		{
			SandboxSelectionSet.CancelSelection();
			SandboxItem component = platform.GetComponent<SandboxItem>();
			if ((bool)component)
			{
				InterfaceAudio.Play("ui_build_terrain_place");
				SandboxSelectionSet.SelectItem(component);
				SandboxUndo.SnapShot();
			}
		}
	}

	public void RefreshPosition(Platform platform)
	{
		m_InputFieldPosX.m_InputField.text = Utils.FormatThreeDecimalPlaces(platform.transform.position.x);
		m_InputFieldPosY.m_InputField.text = Utils.FormatThreeDecimalPlaces(platform.transform.position.y);
	}

	public void RefreshWidth(Platform platform)
	{
		m_InputFieldWidth.m_InputField.text = Utils.FormatDistance(platform.m_Width);
	}

	public void RefreshHeight(Platform platform)
	{
		m_InputFieldHeight.m_InputField.text = Utils.FormatDistance(platform.m_Height);
	}

	public void RefreshProperties(Platform platform)
	{
		if ((bool)platform)
		{
			RefreshPosition(platform);
			RefreshToggles(platform);
			RefreshSliders(platform);
			RefreshWidth(platform);
			RefreshHeight(platform);
			m_LastRefreshedPlatform = platform;
		}
	}

	private void OnFlipToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Platform selectedPlatform = SandboxSelectionSet.GetSelectedPlatform();
		if ((bool)selectedPlatform)
		{
			selectedPlatform.m_Flipped = m_FlipToggle.isOn;
			selectedPlatform.RefreshMesh();
			SandboxUndo.SnapShot();
		}
	}

	private void RefreshToggles(Platform platform)
	{
		m_FlipToggle.isOn = platform.m_Flipped;
	}

	private void RefreshSliders(Platform platform)
	{
		m_SliderWidth.SetValue(platform.m_Width);
		m_SliderWidth.m_SandboxInputField.m_InputField.text = Utils.FormatDistance(platform.m_Width);
		m_SliderHeight.SetValue(platform.m_Height);
		m_SliderHeight.m_SandboxInputField.m_InputField.text = Utils.FormatDistance(platform.m_Height);
	}

	private void ProcessInput(Platform platform)
	{
		if ((bool)platform && !GameStateCommonInput.IgnoreKeyboardInput())
		{
			if (GameInput.JustPressed(BindingType.FLIP_HORIZONTAL))
			{
				InterfaceAudio.Play("ui_settings_toggle");
				platform.m_Flipped = !platform.m_Flipped;
				m_FlipToggle.isOn = platform.m_Flipped;
				platform.RefreshMesh();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.WEST))
			{
				ExecuteEvents.Execute(m_DeleteButton.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
			}
			else if (GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH))
			{
				ExecuteEvents.Execute(m_DuplicateButton.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
			}
		}
	}

	private void WidthSliderChanged(float width)
	{
		if (m_SkipInputFieldUpdateFromSlider)
		{
			m_SkipInputFieldUpdateFromSlider = false;
			return;
		}
		Platform selectedPlatform = SandboxSelectionSet.GetSelectedPlatform();
		if ((bool)selectedPlatform)
		{
			selectedPlatform.m_Width = Mathf.Clamp(width, Platforms.MIN_WIDTH, Platforms.MAX_WIDTH);
			selectedPlatform.RefreshMesh();
			m_SliderWidth.m_SandboxInputField.m_InputField.text = Utils.FormatDistance(selectedPlatform.m_Width);
		}
	}

	private void HeightSliderChanged(float height)
	{
		if (m_SkipInputFieldUpdateFromSlider)
		{
			m_SkipInputFieldUpdateFromSlider = false;
			return;
		}
		Platform selectedPlatform = SandboxSelectionSet.GetSelectedPlatform();
		if ((bool)selectedPlatform)
		{
			selectedPlatform.SetHeight(Mathf.Clamp(height, Platforms.MIN_HEIGHT, Platforms.MAX_HEIGHT));
			selectedPlatform.RefreshMesh();
			m_SliderHeight.m_SandboxInputField.m_InputField.text = Utils.FormatDistance(selectedPlatform.m_Height);
		}
	}
}

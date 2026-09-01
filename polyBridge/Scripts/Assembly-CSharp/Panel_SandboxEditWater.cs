using UnityEngine;
using UnityEngine.UI;

public class Panel_SandboxEditWater : MonoBehaviour
{
	[Header("Sliders")]
	public SandboxTapeSlider m_SliderHeight;

	[Header("Toggles")]
	public Toggle m_LockPositionToggle;

	private WaterBlock m_LastRefreshedWaterBlock;

	private PointerEvents m_LockPositionTogglePointerEvents;

	private bool m_SkipInputFieldUpdateFromSlider;

	private void Awake()
	{
		m_LockPositionTogglePointerEvents = m_LockPositionToggle.GetComponent<PointerEvents>();
		m_LockPositionTogglePointerEvents.RegisterOnClickedDelegate(OnLockPositionToggle);
		m_SliderHeight.SetCallback(HeightSliderChanged);
	}

	private void Update()
	{
		WaterBlock selectedWaterBlock = SandboxSelectionSet.GetSelectedWaterBlock();
		if ((bool)selectedWaterBlock)
		{
			m_SliderHeight.SetRange(WaterBlocks.MIN_HEIGHT_SLIDER, selectedWaterBlock.GetMaxHeight(), GameGrid.m_Spacing);
		}
		if ((bool)selectedWaterBlock && selectedWaterBlock != m_LastRefreshedWaterBlock)
		{
			RefreshProperties(selectedWaterBlock);
		}
	}

	private void OnEnable()
	{
		WaterBlock selectedWaterBlock = SandboxSelectionSet.GetSelectedWaterBlock();
		if ((bool)selectedWaterBlock)
		{
			RefreshProperties(selectedWaterBlock);
		}
	}

	private void OnDisable()
	{
		m_LastRefreshedWaterBlock = null;
	}

	public void SkipInputFieldUpdateFromSlider()
	{
		m_SkipInputFieldUpdateFromSlider = true;
	}

	public void ForceRefresh()
	{
		m_LastRefreshedWaterBlock = null;
	}

	public void RefreshProperties(WaterBlock waterBlock)
	{
		RefreshSliders(waterBlock);
		m_LockPositionToggle.isOn = waterBlock.m_LockPosition;
		m_LastRefreshedWaterBlock = waterBlock;
	}

	public void RefreshSliders(WaterBlock waterBlock)
	{
		m_SliderHeight.SetValue(waterBlock.m_Height);
		m_SliderHeight.m_SandboxInputField.m_InputField.text = Utils.FormatDistance(waterBlock.m_Height);
	}

	private void OnLockPositionToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		WaterBlock selectedWaterBlock = SandboxSelectionSet.GetSelectedWaterBlock();
		if ((bool)selectedWaterBlock)
		{
			selectedWaterBlock.m_LockPosition = m_LockPositionToggle.isOn;
			SandboxUndo.SnapShot();
		}
	}

	private void HeightSliderChanged(float height)
	{
		if (m_SkipInputFieldUpdateFromSlider)
		{
			m_SkipInputFieldUpdateFromSlider = false;
			return;
		}
		WaterBlock selectedWaterBlock = SandboxSelectionSet.GetSelectedWaterBlock();
		if ((bool)selectedWaterBlock)
		{
			selectedWaterBlock.m_Height = Mathf.Clamp(height, WaterBlocks.MIN_HEIGHT, selectedWaterBlock.GetMaxHeight());
			if ((bool)selectedWaterBlock.m_LeftTerrain)
			{
				selectedWaterBlock.m_LeftTerrain.m_RightEdgeWaterHeight = selectedWaterBlock.m_Height;
			}
			selectedWaterBlock.RefreshPosition();
			m_SliderHeight.m_SandboxInputField.m_InputField.text = Utils.FormatDistance(selectedWaterBlock.m_Height);
		}
	}
}

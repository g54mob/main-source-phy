using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel_SandboxEditPillar : MonoBehaviour
{
	[Header("Nudge")]
	public Panel_SandboxNudge m_SandboxNudge;

	[Header("Input Fields")]
	public SandboxInputField m_InputFieldPosX;

	public SandboxInputField m_InputFieldPosY;

	[Header("Buttons")]
	public Button m_Duplicate;

	public Button m_Delete;

	[Header("Sliders")]
	public SandboxTapeSlider m_SliderHeight;

	private Pillar m_LastRefreshedSupportPillar;

	private bool m_SkipInputFieldUpdateFromSlider;

	private void Awake()
	{
		m_SliderHeight.SetRange(Pillars.MIN_HEIGHT_SLIDER, Pillars.MAX_HEIGHT_SLIDER, GameGrid.m_Spacing);
		m_SliderHeight.SetCallback(HeightSliderChanged);
	}

	private void Update()
	{
		Pillar selectedPillar = SandboxSelectionSet.GetSelectedPillar();
		if ((bool)selectedPillar && selectedPillar != m_LastRefreshedSupportPillar)
		{
			RefreshProperties(selectedPillar);
		}
		ProcessInput(selectedPillar);
	}

	private void OnEnable()
	{
		m_Duplicate.onClick.AddListener(OnDuplicate);
		m_Delete.onClick.AddListener(OnDelete);
		Pillar selectedPillar = SandboxSelectionSet.GetSelectedPillar();
		if ((bool)selectedPillar)
		{
			RefreshProperties(selectedPillar);
		}
	}

	private void OnDisable()
	{
		m_LastRefreshedSupportPillar = null;
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
		m_LastRefreshedSupportPillar = null;
	}

	public void RefreshProperties(Pillar pillar)
	{
		if ((bool)pillar)
		{
			RefreshPosition(pillar);
			RefreshSliders(pillar);
			m_LastRefreshedSupportPillar = pillar;
		}
	}

	public void RefreshPosition(Pillar pillar)
	{
		m_InputFieldPosX.m_InputField.text = Utils.FormatThreeDecimalPlaces(pillar.transform.position.x);
		m_InputFieldPosY.m_InputField.text = Utils.FormatThreeDecimalPlaces(pillar.transform.position.y);
	}

	private void RefreshSliders(Pillar pillar)
	{
		m_SliderHeight.SetValue(pillar.m_Height);
		m_SliderHeight.m_SandboxInputField.m_InputField.text = Utils.FormatDistance(pillar.m_Height);
	}

	private void OnDuplicate()
	{
		Pillar selectedPillar = SandboxSelectionSet.GetSelectedPillar();
		if ((bool)selectedPillar && Prefabs.m_PrefabsDict.ContainsKey(selectedPillar.name))
		{
			Vector3 offset = new Vector3(selectedPillar.m_BoxCollider.bounds.size.x, 0f, 0f);
			Pillar pillar = selectedPillar.Duplicate(Prefabs.m_PrefabsDict[selectedPillar.name], offset);
			if ((bool)pillar)
			{
				InterfaceAudio.Play("ui_build_generic_place");
				SandboxSelectionSet.ForceSelection(pillar.m_SandboxItem);
				SandboxUndo.SnapShot();
			}
		}
	}

	private void OnDelete()
	{
		if ((bool)SandboxSelectionSet.GetSelectedPillar())
		{
			InterfaceAudio.Play("ui_build_delete");
			SandboxSelectionSet.Delete();
		}
	}

	private void HeightSliderChanged(float height)
	{
		if (m_SkipInputFieldUpdateFromSlider)
		{
			m_SkipInputFieldUpdateFromSlider = false;
			return;
		}
		Pillar selectedPillar = SandboxSelectionSet.GetSelectedPillar();
		if ((bool)selectedPillar)
		{
			selectedPillar.SetHeight(Mathf.Clamp(height, Pillars.MIN_HEIGHT, Pillars.MAX_HEIGHT));
			m_SliderHeight.m_SandboxInputField.m_InputField.text = Utils.FormatDistance(selectedPillar.m_Height);
		}
	}

	private void ProcessInput(Pillar pillar)
	{
		if ((bool)pillar && !GameStateCommonInput.IgnoreKeyboardInput())
		{
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
}

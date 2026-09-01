using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel_SandboxEditFlyingObject : MonoBehaviour
{
	[Header("Header")]
	public Image m_Icon;

	[Header("Nudge")]
	public Panel_SandboxNudge m_SandboxNudge;

	[Header("Input Fields")]
	public SandboxInputField m_InputFieldPosX;

	public SandboxInputField m_InputFieldPosY;

	[Header("Buttons")]
	public Button m_Duplicate;

	public Button m_Delete;

	[Header("Sliders")]
	public SandboxTapeSlider m_SliderScale;

	private FlyingObject m_LastRefreshedFlyingObject;

	private bool m_SkipInputFieldUpdateFromSlider;

	private void Awake()
	{
		m_SliderScale.SetRange(FlyingObjects.MIN_NORMALIZED_SCALE_SLIDER * 100f, FlyingObjects.MAX_NORMALIZED_SCALE_SLIDER * 100f, 1f);
		m_SliderScale.SetCallback(ScaleSliderChanged);
	}

	private void Update()
	{
		FlyingObject selectedFlyingObject = SandboxSelectionSet.GetSelectedFlyingObject();
		if ((bool)selectedFlyingObject && selectedFlyingObject != m_LastRefreshedFlyingObject)
		{
			RefreshProperties(selectedFlyingObject);
		}
		ProcessInput(selectedFlyingObject);
	}

	private void OnEnable()
	{
		m_Duplicate.onClick.AddListener(OnDuplicate);
		m_Delete.onClick.AddListener(OnDelete);
		FlyingObject selectedFlyingObject = SandboxSelectionSet.GetSelectedFlyingObject();
		if ((bool)selectedFlyingObject)
		{
			RefreshProperties(selectedFlyingObject);
		}
	}

	private void OnDisable()
	{
		m_LastRefreshedFlyingObject = null;
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
		m_LastRefreshedFlyingObject = null;
	}

	public void RefreshProperties(FlyingObject flyingObject)
	{
		if ((bool)flyingObject)
		{
			RefreshPosition(flyingObject);
			RefreshSliders(flyingObject);
			RefreshIcon(flyingObject);
			m_LastRefreshedFlyingObject = flyingObject;
		}
	}

	public void RefreshPosition(FlyingObject flyingObject)
	{
		m_InputFieldPosX.m_InputField.text = Utils.FormatThreeDecimalPlaces(flyingObject.transform.position.x);
		m_InputFieldPosY.m_InputField.text = Utils.FormatThreeDecimalPlaces(flyingObject.transform.position.y);
	}

	private void RefreshSliders(FlyingObject flyingObject)
	{
		m_SliderScale.SetValue(Mathf.Abs(flyingObject.transform.localScale.x) * 100f);
		m_SliderScale.m_SandboxInputField.m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(Mathf.Abs(flyingObject.transform.localScale.x));
	}

	private void RefreshIcon(FlyingObject flyingObject)
	{
		m_Icon.sprite = flyingObject.m_Sprite;
	}

	private void OnDuplicate()
	{
		FlyingObject selectedFlyingObject = SandboxSelectionSet.GetSelectedFlyingObject();
		if ((bool)selectedFlyingObject && Prefabs.m_PrefabsDict.ContainsKey(selectedFlyingObject.name))
		{
			FlyingObject flyingObject = selectedFlyingObject.Duplicate(Prefabs.m_PrefabsDict[selectedFlyingObject.name], new Vector3(selectedFlyingObject.m_MeshCollider.bounds.size.x, (0f - selectedFlyingObject.m_MeshCollider.bounds.size.y) / 2f, 0f));
			if ((bool)flyingObject)
			{
				InterfaceAudio.Play("ui_build_terrain_place");
				SandboxSelectionSet.ForceSelection(flyingObject.m_SandboxItem);
				SandboxUndo.SnapShot();
			}
		}
	}

	private void OnDelete()
	{
		if ((bool)SandboxSelectionSet.GetSelectedFlyingObject())
		{
			InterfaceAudio.Play("ui_build_delete");
			SandboxSelectionSet.Delete();
		}
	}

	private int GetVariantIndex(FlyingObject flyingObject)
	{
		for (int i = 0; i < Prefabs.m_Instance.m_FlyingObjects.Length; i++)
		{
			if (flyingObject.name == Prefabs.m_Instance.m_FlyingObjects[i].name)
			{
				return i;
			}
		}
		return -1;
	}

	private void ScaleSliderChanged(float percentage)
	{
		if (m_SkipInputFieldUpdateFromSlider)
		{
			m_SkipInputFieldUpdateFromSlider = false;
			return;
		}
		FlyingObject selectedFlyingObject = SandboxSelectionSet.GetSelectedFlyingObject();
		if ((bool)selectedFlyingObject)
		{
			float num = Mathf.Clamp(percentage / 100f, FlyingObjects.MIN_NORMALIZED_SCALE, FlyingObjects.MAX_NORMALIZED_SCALE);
			if (!Mathf.Approximately(num, 0f))
			{
				selectedFlyingObject.transform.localScale = new Vector3(num, num, num);
				selectedFlyingObject.UpdatePolygonShapes();
			}
			m_SliderScale.m_SandboxInputField.m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(num);
		}
	}

	private void ProcessInput(FlyingObject flyingObject)
	{
		if ((bool)flyingObject && !GameStateCommonInput.IgnoreKeyboardInput())
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

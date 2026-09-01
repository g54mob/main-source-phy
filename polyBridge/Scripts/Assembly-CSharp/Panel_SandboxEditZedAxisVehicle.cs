using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel_SandboxEditZedAxisVehicle : MonoBehaviour
{
	[Header("Header")]
	public Image m_Icon;

	public TMP_Dropdown m_VehicleTypeDropdown;

	[Header("Input Fields")]
	public SandboxInputField m_InputFieldPosX;

	public SandboxInputField m_InputFieldPosY;

	public SandboxInputField m_InputFieldSpeed;

	public SandboxInputField m_InputFieldTimeDelay;

	[Header("Sliders")]
	public SandboxTapeSlider m_SliderRot;

	public SandboxTapeSlider m_SliderScale;

	[Header("Toggles")]
	public Toggle m_ReverseToggle;

	public Toggle m_SnapToWaterLineToggle;

	[Header("Buttons")]
	public Button m_ButtonDuplicate;

	public Button m_ButtonDelete;

	[Header("Layout")]
	public RectTransform m_Content;

	private PointerEvents m_ReverseTogglePointerEvents;

	private PointerEvents m_SnapToWaterLineTogglePointerEvents;

	private ZedAxisVehicle m_LastRefreshedVehicle;

	private Dictionary<string, string> m_VehicleDropdownMap = new Dictionary<string, string>();

	private bool m_SkipInputFieldUpdateFromSlider;

	private void Awake()
	{
		m_VehicleTypeDropdown.onValueChanged.AddListener(delegate
		{
			OnVehicleTypeChanged();
		});
		m_ButtonDelete.onClick.AddListener(OnDelete);
		m_ButtonDuplicate.onClick.AddListener(OnDuplicate);
		m_ReverseTogglePointerEvents = m_ReverseToggle.GetComponent<PointerEvents>();
		m_ReverseTogglePointerEvents.RegisterOnClickedDelegate(OnReverseToggle);
		m_SnapToWaterLineTogglePointerEvents = m_SnapToWaterLineToggle.GetComponent<PointerEvents>();
		m_SnapToWaterLineTogglePointerEvents.RegisterOnClickedDelegate(OnSnapToWaterLineToggle);
		m_SliderRot.SetRange(-180f, 180f, 1f);
		m_SliderRot.SetCallback(RotSliderChanged);
		m_SliderScale.SetRange(ZedAxisVehicles.MIN_NORMALIZED_SCALE_SLIDER * 100f, ZedAxisVehicles.MAX_NORMALIZED_SCALE_SLIDER * 100f, 1f);
		m_SliderScale.SetCallback(ScaleSliderChanged);
		m_VehicleTypeDropdown.alphaFadeSpeed = 0f;
	}

	private void Update()
	{
		ZedAxisVehicle selectedZedAxisVehicle = SandboxSelectionSet.GetSelectedZedAxisVehicle();
		if ((bool)selectedZedAxisVehicle && selectedZedAxisVehicle != m_LastRefreshedVehicle)
		{
			RefreshProperties(selectedZedAxisVehicle);
		}
		ProcessInput(selectedZedAxisVehicle);
		LayoutRebuilder.ForceRebuildLayoutImmediate(m_Content);
	}

	private void OnEnable()
	{
		PopulateVehicleTypeDropdown();
		ZedAxisVehicle selectedZedAxisVehicle = SandboxSelectionSet.GetSelectedZedAxisVehicle();
		if ((bool)selectedZedAxisVehicle)
		{
			RefreshProperties(selectedZedAxisVehicle);
		}
	}

	private void OnDisable()
	{
		GameUI.m_Instance.CancelDropdownSelection(m_VehicleTypeDropdown);
		m_LastRefreshedVehicle = null;
	}

	public void SkipInputFieldUpdateFromSlider()
	{
		m_SkipInputFieldUpdateFromSlider = true;
	}

	public void ForceRefresh()
	{
		m_LastRefreshedVehicle = null;
	}

	private void RefreshSliders(ZedAxisVehicle vehicle)
	{
		m_SliderRot.SetValue(vehicle.m_RotationDegrees);
		m_SliderRot.m_SandboxInputField.m_InputField.text = Utils.FormatAngle(vehicle.m_RotationDegrees);
		if (vehicle.m_ScalingTransform != null)
		{
			float num = vehicle.m_ScalingTransform.localScale.x / vehicle.m_OriginalScale.x;
			m_SliderScale.SetValue(num * 100f);
			m_SliderScale.m_SandboxInputField.m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(num);
		}
		else
		{
			m_SliderScale.SetValue(100f);
			m_SliderScale.m_SandboxInputField.m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(1f);
		}
	}

	public void RefreshProperties(ZedAxisVehicle vehicle)
	{
		RefreshToggles(vehicle);
		RefreshDropdowns(vehicle);
		RefreshInputFields(vehicle);
		RefreshSliders(vehicle);
		m_LastRefreshedVehicle = vehicle;
		m_Icon.sprite = vehicle.m_Stub.m_Icon;
		LayoutRebuilder.ForceRebuildLayoutImmediate(m_Content);
	}

	public void RefreshPosition(ZedAxisVehicle vehicle)
	{
		m_InputFieldPosX.m_InputField.text = Utils.FormatThreeDecimalPlaces(vehicle.transform.position.x);
		m_InputFieldPosY.m_InputField.text = Utils.FormatThreeDecimalPlaces(vehicle.transform.position.y);
	}

	public bool VehicleTypeDropDownHasScrollFocus()
	{
		Scrollbar componentInChildren = m_VehicleTypeDropdown.GetComponentInChildren<Scrollbar>(includeInactive: false);
		if (!componentInChildren)
		{
			return false;
		}
		if (componentInChildren.gameObject.activeInHierarchy)
		{
			return GameUI.PointerOver(typeof(Panel_SandboxMenu));
		}
		return false;
	}

	public void RefreshInputFields(ZedAxisVehicle vehicle)
	{
		RefreshPosition(vehicle);
		m_InputFieldSpeed.m_InputField.text = Utils.FormatSpeed(vehicle.m_Speed);
		m_InputFieldTimeDelay.m_InputField.text = Utils.FormatSeconds(vehicle.m_TimeDelaySeconds);
	}

	public void ForceVehicleDropdownRefresh()
	{
		ZedAxisVehicle selectedZedAxisVehicle = SandboxSelectionSet.GetSelectedZedAxisVehicle();
		if ((bool)selectedZedAxisVehicle)
		{
			PopulateVehicleTypeDropdown();
			RefreshDropdowns(selectedZedAxisVehicle);
		}
	}

	private void RefreshToggles(ZedAxisVehicle vehicle)
	{
		m_ReverseToggle.isOn = vehicle.m_Reverse;
		m_SnapToWaterLineToggle.isOn = vehicle.m_SnapToWaterLine;
		m_SnapToWaterLineToggle.transform.parent.gameObject.SetActive(vehicle.GetVehicleType() == ZedAxisVehicleType.BOAT);
	}

	private void RefreshDropdowns(ZedAxisVehicle vehicle)
	{
		DropdownUtils.SelectItem(m_VehicleTypeDropdown, Localize.Get(vehicle.m_Stub.m_DisplayNameLocID));
	}

	public void PopulateVehicleTypeDropdown()
	{
		m_VehicleTypeDropdown.ClearOptions();
		m_VehicleDropdownMap.Clear();
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, ZedAxisVehicleStub> item in ZedAxisVehicleStubs.m_StubsDict)
		{
			string text = Localize.Get(item.Value.m_DisplayNameLocID);
			list.Add(text);
			m_VehicleDropdownMap.Add(text, item.Value.m_PrefabAddress);
		}
		list.Sort();
		m_VehicleTypeDropdown.AddOptions(list);
	}

	private void OnVehicleTypeChanged()
	{
		InterfaceAudio.Play("ui_menu_select");
		ZedAxisVehicle selectedZedAxisVehicle = SandboxSelectionSet.GetSelectedZedAxisVehicle();
		if (!selectedZedAxisVehicle)
		{
			return;
		}
		string text = m_VehicleTypeDropdown.captionText.text;
		if (Localize.Get(selectedZedAxisVehicle.m_Stub.m_DisplayNameLocID) == text || !m_VehicleDropdownMap.ContainsKey(text))
		{
			return;
		}
		string text2 = m_VehicleDropdownMap[text];
		GameObject gameObject = null;
		if (Prefabs.AsyncPrefabExists(text2))
		{
			gameObject = Prefabs.GetAsyncPrefab(text2);
			ZedAxisVehicle zedAxisVehicle = ZedAxisVehicles.CreateVehicle(gameObject, selectedZedAxisVehicle.m_ModId, selectedZedAxisVehicle.transform.position, selectedZedAxisVehicle.transform.rotation, selectedZedAxisVehicle.m_Guid);
			if ((bool)zedAxisVehicle)
			{
				zedAxisVehicle.m_RotationDegrees = selectedZedAxisVehicle.m_RotationDegrees;
				zedAxisVehicle.UpdatePolygonShapes();
				zedAxisVehicle.OnlyDrawOutline();
				EventTimelines.UpdateGameObjectReferences(selectedZedAxisVehicle.gameObject, zedAxisVehicle.gameObject);
				ZedAxisVehicles.Remove(selectedZedAxisVehicle);
				Object.Destroy(selectedZedAxisVehicle.gameObject);
				SandboxSelectionSet.CancelSelection();
				SandboxItem component = zedAxisVehicle.GetComponent<SandboxItem>();
				if ((bool)component)
				{
					SandboxSelectionSet.SelectItem(component);
					EventEditor.SelectIconMatchingGameObject(zedAxisVehicle.gameObject);
				}
				SandboxUndo.SnapShot();
			}
		}
		else
		{
			Prefabs.m_Instance.PreloadSingleAsset(text2, string.Empty, VehicleLoadedCallback);
		}
	}

	public void OnDelete()
	{
		if ((bool)SandboxSelectionSet.GetSelectedZedAxisVehicle())
		{
			InterfaceAudio.Play("ui_build_delete");
			SandboxSelectionSet.Delete();
		}
	}

	private void OnDuplicate()
	{
		ZedAxisVehicle selectedZedAxisVehicle = SandboxSelectionSet.GetSelectedZedAxisVehicle();
		if ((bool)selectedZedAxisVehicle)
		{
			ZedAxisVehicle zedAxisVehicle = selectedZedAxisVehicle.Duplicate(new Vector3(selectedZedAxisVehicle.m_MeshRenderer.bounds.size.x, 0f, 0f));
			if ((bool)zedAxisVehicle)
			{
				zedAxisVehicle.OnlyDrawOutline();
				InterfaceAudio.Play("ui_build_generic_place");
				SandboxSelectionSet.ForceSelection(zedAxisVehicle.m_SandboxItem);
				SandboxUndo.SnapShot();
			}
		}
	}

	private void ProcessInput(ZedAxisVehicle vehicle)
	{
		if ((bool)vehicle && !GameStateCommonInput.IgnoreKeyboardInput())
		{
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.WEST))
			{
				ExecuteEvents.Execute(m_ButtonDelete.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
			}
			else if (GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH))
			{
				ExecuteEvents.Execute(m_ButtonDuplicate.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
			}
			m_SliderRot.m_SandboxInputField.ProcessInputForRotation();
		}
	}

	private void VehicleLoadedCallback(string addressableName, string instanceID, bool success)
	{
		if (success)
		{
			OnVehicleTypeChanged();
		}
	}

	private void OnReverseToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		ZedAxisVehicle selectedZedAxisVehicle = SandboxSelectionSet.GetSelectedZedAxisVehicle();
		if ((bool)selectedZedAxisVehicle)
		{
			selectedZedAxisVehicle.m_Reverse = m_ReverseToggle.isOn;
			SandboxUndo.SnapShot();
		}
	}

	private void OnSnapToWaterLineToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		ZedAxisVehicle selectedZedAxisVehicle = SandboxSelectionSet.GetSelectedZedAxisVehicle();
		if ((bool)selectedZedAxisVehicle)
		{
			selectedZedAxisVehicle.m_SnapToWaterLine = m_SnapToWaterLineToggle.isOn;
			SandboxUndo.SnapShot();
		}
	}

	private void RotSliderChanged(float angle)
	{
		ZedAxisVehicle selectedZedAxisVehicle = SandboxSelectionSet.GetSelectedZedAxisVehicle();
		if ((bool)selectedZedAxisVehicle)
		{
			selectedZedAxisVehicle.m_RotationDegrees = angle % 360f;
			selectedZedAxisVehicle.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f - selectedZedAxisVehicle.m_RotationDegrees));
			if (selectedZedAxisVehicle.m_SandboxItem != null)
			{
				selectedZedAxisVehicle.m_SandboxItem.SetFloatingTextToDefaultPosition();
			}
			selectedZedAxisVehicle.UpdatePolygonShapes();
			m_SliderRot.m_SandboxInputField.m_InputField.text = Utils.FormatAngle(selectedZedAxisVehicle.m_RotationDegrees);
		}
	}

	private void ScaleSliderChanged(float percentage)
	{
		if (m_SkipInputFieldUpdateFromSlider)
		{
			m_SkipInputFieldUpdateFromSlider = false;
			return;
		}
		ZedAxisVehicle selectedZedAxisVehicle = SandboxSelectionSet.GetSelectedZedAxisVehicle();
		if ((bool)selectedZedAxisVehicle)
		{
			float num = Mathf.Clamp(percentage / 100f, ZedAxisVehicles.MIN_NORMALIZED_SCALE, ZedAxisVehicles.MAX_NORMALIZED_SCALE);
			if (!Mathf.Approximately(num, 0f))
			{
				selectedZedAxisVehicle.SetUniformScale(num);
				m_SliderScale.m_SandboxInputField.m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(num);
				SandboxItems.ResolveOverlappingFloatingText();
				selectedZedAxisVehicle.m_SandboxItem.SetOutlineDirty(dirty: true);
			}
		}
	}
}

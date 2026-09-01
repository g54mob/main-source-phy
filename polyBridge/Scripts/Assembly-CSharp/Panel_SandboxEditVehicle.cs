using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel_SandboxEditVehicle : MonoBehaviour
{
	public RectTransform m_VerticalLayoutRectTransform;

	public RectTransform m_Content;

	public ScrollRect m_ScrollRect;

	[Header("Nudge")]
	public Panel_SandboxNudge m_SandboxNudge;

	[Header("Header")]
	public Image m_Icon;

	public TMP_Dropdown m_VehicleTypeDropdown;

	public GameObject m_CheckpointsPanel;

	[Header("Flip")]
	public Button m_FlipButton;

	public Image m_FlipButtonImage;

	public Sprite m_ForwardDirectionSprite;

	public Sprite m_BackwardDirectionSprite;

	[Header("Input Fields")]
	public SandboxInputField m_InputFieldPosX;

	public SandboxInputField m_InputFieldPosY;

	public SandboxInputField m_InputFieldScale;

	public SandboxInputField m_InputFieldTimeDelay;

	[Header("Sliders")]
	public SandboxTapeSlider m_SliderRot;

	public SandboxTapeSlider m_SliderScale;

	[Header("Toggles")]
	public Toggle m_FlipToggle;

	[Header("Physics")]
	public SandboxInputField m_InputFieldSpeed;

	public SandboxInputField m_InputFieldWeight;

	public SandboxInputField m_InputFieldAcceleration;

	public SandboxInputField m_InputFieldBrakingForceMultiplier;

	public SandboxInputField m_InputFieldDesiredAcceleration;

	public SandboxInputField m_InputFieldShocksMultiplier;

	public Toggle m_IdleOnDownhillToggle;

	public Button m_ButtonDefaults;

	[Header("Checkpoints")]
	public Transform m_CheckpointsParent;

	public GameObject m_CheckpointTapePrefab;

	public Button m_ButtonAddCheckpoint;

	public Toggle m_OrderedCheckpointsToggle;

	public SandboxPanelResizer m_CheckpointsBackgroundPanelResizer;

	public SandboxPanelResizer m_CheckpointsPanelResizer;

	[Header("Buttons")]
	public Button m_ButtonDuplicate;

	public Button m_ButtonDelete;

	[Header("Swatches")]
	public SandboxSwatches m_SandboxSwatches;

	private PointerEvents m_FlipTogglePointerEvents;

	private PointerEvents m_IdleOnDownhillTogglePointerEvents;

	private PointerEvents m_OrderedCheckpointsTogglePointerEvents;

	private Vehicle m_LastRefreshedVehicle;

	private Dictionary<string, string> m_VehicleDropdownMap = new Dictionary<string, string>();

	private bool m_SkipInputFieldUpdateFromSlider;

	private bool m_ResolveOverlap;

	public List<SandboxTapeCheckpoint> m_SandboxTapeCheckpoints = new List<SandboxTapeCheckpoint>();

	private void Awake()
	{
		m_FlipTogglePointerEvents = m_FlipToggle.GetComponent<PointerEvents>();
		m_FlipTogglePointerEvents.RegisterOnClickedDelegate(OnFlipToggle);
		m_IdleOnDownhillTogglePointerEvents = m_IdleOnDownhillToggle.GetComponent<PointerEvents>();
		m_IdleOnDownhillTogglePointerEvents.RegisterOnClickedDelegate(OnIdleOnDownhillToggle);
		m_OrderedCheckpointsTogglePointerEvents = m_OrderedCheckpointsToggle.GetComponent<PointerEvents>();
		m_OrderedCheckpointsTogglePointerEvents.RegisterOnClickedDelegate(OnOrderedCheckpointsToggle);
		m_VehicleTypeDropdown.onValueChanged.AddListener(delegate
		{
			OnVehicleTypeChanged();
		});
		m_ButtonDelete.onClick.AddListener(OnDelete);
		m_ButtonDuplicate.onClick.AddListener(OnDuplicate);
		m_ButtonDefaults.onClick.AddListener(OnDefaults);
		m_FlipButton.onClick.AddListener(OnFlipButton);
		m_ButtonAddCheckpoint.onClick.AddListener(OnAddCheckpoint);
		m_SliderRot.SetRange(-180f, 180f, 1f);
		m_SliderRot.SetCallback(RotSliderChanged);
		m_SliderScale.SetRange(Vehicles.MIN_NORMALIZED_SCALE_SLIDER * 100f, Vehicles.MAX_NORMALIZED_SCALE_SLIDER * 100f, 1f);
		m_SliderScale.SetCallback(ScaleSliderChanged);
		m_VehicleTypeDropdown.alphaFadeSpeed = 0f;
	}

	private void Update()
	{
		Vehicle selectedVehicle = SandboxSelectionSet.GetSelectedVehicle();
		if (!(selectedVehicle == null))
		{
			if (selectedVehicle != m_LastRefreshedVehicle)
			{
				RefreshProperties(selectedVehicle);
				GameUI.m_Instance.m_SandboxEditVehicle.ForceUpdateLayout();
			}
			ProcessInput(selectedVehicle);
			RefreshCheckpointButtons();
			LayoutRebuilder.MarkLayoutForRebuild(m_VerticalLayoutRectTransform);
		}
	}

	private void LateUpdate()
	{
		if (!m_ResolveOverlap)
		{
			return;
		}
		foreach (SandboxItem item in SandboxItems.m_Items)
		{
			if ((bool)item.m_Label)
			{
				SandboxItems.ResolveOverlappingFloatingText();
			}
		}
		m_ResolveOverlap = false;
	}

	private void OnEnable()
	{
		PopulateVehicleTypeDropdown();
		Vehicle selectedVehicle = SandboxSelectionSet.GetSelectedVehicle();
		if ((bool)selectedVehicle)
		{
			RefreshProperties(selectedVehicle);
		}
		GameUI.m_Instance.m_SandboxEditVehicle.ForceUpdateLayout();
	}

	private void OnDisable()
	{
		GameUI.m_Instance.CancelDropdownSelection(m_VehicleTypeDropdown);
		m_LastRefreshedVehicle = null;
	}

	public void UpdateForCurrentDevice()
	{
		m_SandboxNudge.UpdateForCurrentDevice();
	}

	public void SkipInputFieldUpdateFromSlider()
	{
		m_SkipInputFieldUpdateFromSlider = true;
	}

	public void PopulateVehicleTypeDropdown()
	{
		m_VehicleTypeDropdown.ClearOptions();
		m_VehicleDropdownMap.Clear();
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, VehicleStub> item in VehicleStubs.m_StubsDict)
		{
			string text = Localize.Get(item.Value.m_DisplayNameLocID);
			list.Add(text);
			m_VehicleDropdownMap.Add(text, item.Value.m_PrefabAddress);
		}
		list.Sort();
		m_VehicleTypeDropdown.AddOptions(list);
	}

	public void RefreshProperties(Vehicle vehicle)
	{
		RefreshDropdowns(vehicle);
		RefreshInputFields(vehicle);
		RefreshSliders(vehicle);
		RefreshToggles(vehicle);
		RefreshPhysics(vehicle);
		RefreshCheckpoints(vehicle);
		m_SandboxSwatches.Refresh(vehicle);
		RefreshIcon(vehicle);
		UpdateIconsBasedOnFlip(vehicle);
		m_LastRefreshedVehicle = vehicle;
	}

	public void RefreshPosition(Vehicle vehicle)
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

	public void ForceRefresh()
	{
		m_LastRefreshedVehicle = null;
	}

	public void ForceVehicleDropdownRefresh()
	{
		Vehicle selectedVehicle = SandboxSelectionSet.GetSelectedVehicle();
		if ((bool)selectedVehicle)
		{
			PopulateVehicleTypeDropdown();
			RefreshDropdowns(selectedVehicle);
		}
	}

	public void RefreshIcon(Vehicle vehicle)
	{
		m_Icon.sprite = vehicle.GetIcon();
	}

	public void ForceUpdateLayout()
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(m_CheckpointsPanelResizer.GetComponent<RectTransform>());
		m_CheckpointsPanelResizer.ForceUpdate();
		LayoutRebuilder.ForceRebuildLayoutImmediate(m_CheckpointsBackgroundPanelResizer.GetComponent<RectTransform>());
		m_CheckpointsBackgroundPanelResizer.ForceUpdate();
		LayoutRebuilder.ForceRebuildLayoutImmediate(m_Content.transform.parent.GetComponent<RectTransform>());
		m_ScrollRect.Rebuild(CanvasUpdate.PostLayout);
	}

	private void OnDuplicate()
	{
		Vehicle selectedVehicle = SandboxSelectionSet.GetSelectedVehicle();
		if ((bool)selectedVehicle)
		{
			Vehicle vehicle = selectedVehicle.Duplicate(new Vector3(selectedVehicle.m_StaticBoundingBox.size.x, 0f, 0f));
			if ((bool)vehicle)
			{
				InterfaceAudio.Play("ui_build_generic_place");
				SandboxSelectionSet.ForceSelection(vehicle.m_SandboxItem);
				SandboxUndo.SnapShot();
			}
		}
	}

	private void RefreshInputFields(Vehicle vehicle)
	{
		RefreshPosition(vehicle);
		m_InputFieldTimeDelay.m_InputField.text = Utils.FormatSeconds(vehicle.m_TimeDelaySeconds);
	}

	private void RefreshSliders(Vehicle vehicle)
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

	private void RefreshDropdowns(Vehicle vehicle)
	{
		DropdownUtils.SelectItem(m_VehicleTypeDropdown, Localize.Get(vehicle.m_Stub.m_DisplayNameLocID));
	}

	private void RefreshToggles(Vehicle vehicle)
	{
		m_FlipToggle.isOn = vehicle.m_Flipped;
	}

	private void RefreshPhysics(Vehicle vehicle)
	{
		m_InputFieldSpeed.m_InputField.text = Utils.FormatSpeed(vehicle.m_TargetSpeed);
		m_InputFieldWeight.m_InputField.text = Utils.FormatWeight(vehicle.m_Mass * BridgePhysics.KgToPg);
		m_InputFieldAcceleration.m_InputField.text = Utils.FormatAcceleration(vehicle.m_Acceleration);
		m_InputFieldBrakingForceMultiplier.m_InputField.text = Utils.FormatOneDecimalPlace(vehicle.m_BrakingForceMultiplier);
		m_InputFieldDesiredAcceleration.m_InputField.text = Utils.FormatAcceleration(vehicle.m_DesiredAcceleration);
		m_InputFieldShocksMultiplier.m_InputField.text = Utils.FormatOneDecimalPlace(vehicle.m_ShocksMultiplier);
		m_IdleOnDownhillToggle.isOn = vehicle.m_IdleOnDownhill;
	}

	public void RefreshCheckpoints(Vehicle vehicle)
	{
		m_OrderedCheckpointsToggle.isOn = vehicle.m_OrderedCheckpoints;
		RegenerateTapeCheckpoints(vehicle);
	}

	private void OnFlipButton()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Vehicle selectedVehicle = SandboxSelectionSet.GetSelectedVehicle();
		if ((bool)selectedVehicle)
		{
			selectedVehicle.m_Flipped = !selectedVehicle.m_Flipped;
			selectedVehicle.SetLocalScale(selectedVehicle.m_Flipped);
			selectedVehicle.UpdatePolygonShapes();
			UpdateIconsBasedOnFlip(selectedVehicle);
			SandboxUndo.SnapShot();
		}
	}

	private void UpdateIconsBasedOnFlip(Vehicle vehicle)
	{
		m_FlipButtonImage.sprite = (vehicle.m_Flipped ? m_BackwardDirectionSprite : m_ForwardDirectionSprite);
		m_Icon.transform.localScale = new Vector3(vehicle.m_Flipped ? (-1f) : 1f, 1f, 1f);
	}

	private void OnFlipToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Vehicle selectedVehicle = SandboxSelectionSet.GetSelectedVehicle();
		if ((bool)selectedVehicle)
		{
			selectedVehicle.m_Flipped = m_FlipToggle.isOn;
			selectedVehicle.SetLocalScale(selectedVehicle.m_Flipped);
			selectedVehicle.UpdatePolygonShapes();
			SandboxUndo.SnapShot();
		}
	}

	private void OnIdleOnDownhillToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Vehicle selectedVehicle = SandboxSelectionSet.GetSelectedVehicle();
		if ((bool)selectedVehicle)
		{
			selectedVehicle.m_IdleOnDownhill = m_IdleOnDownhillToggle.isOn;
			SandboxUndo.SnapShot();
		}
	}

	private void OnOrderedCheckpointsToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Vehicle selectedVehicle = SandboxSelectionSet.GetSelectedVehicle();
		if ((bool)selectedVehicle)
		{
			selectedVehicle.m_OrderedCheckpoints = m_OrderedCheckpointsToggle.isOn;
			SandboxUndo.SnapShot();
			m_ResolveOverlap = true;
		}
	}

	private void OnVehicleTypeChanged()
	{
		Vehicle selectedVehicle = SandboxSelectionSet.GetSelectedVehicle();
		if (!selectedVehicle)
		{
			return;
		}
		string text = m_VehicleTypeDropdown.captionText.text;
		if (Localize.Get(selectedVehicle.m_Stub.m_DisplayNameLocID) == text || !m_VehicleDropdownMap.ContainsKey(text))
		{
			return;
		}
		string text2 = m_VehicleDropdownMap[text];
		GameObject gameObject = null;
		if (Prefabs.AsyncPrefabExists(text2))
		{
			gameObject = Prefabs.GetAsyncPrefab(text2);
			VehicleStub stubByAddressable = VehicleStubs.GetStubByAddressable(gameObject.name);
			Vehicle vehicle = Vehicles.CreateVehicle(gameObject, (stubByAddressable != null) ? stubByAddressable.m_ModId : string.Empty, selectedVehicle.transform.position, selectedVehicle.transform.rotation, Utils.GenerateUniqueId());
			if (!vehicle)
			{
				return;
			}
			InterfaceAudio.Play("ui_menu_select");
			vehicle.m_RotationDegrees = selectedVehicle.m_RotationDegrees;
			vehicle.m_TimeDelaySeconds = selectedVehicle.m_TimeDelaySeconds;
			vehicle.m_Flipped = selectedVehicle.m_Flipped;
			vehicle.SetLocalScale(vehicle.m_Flipped);
			vehicle.m_OrderedCheckpoints = selectedVehicle.m_OrderedCheckpoints;
			vehicle.m_SpawnPos = selectedVehicle.transform.position;
			vehicle.m_SpawnRot = selectedVehicle.transform.rotation;
			vehicle.ApplyRandomSkin();
			vehicle.UpdatePolygonShapes();
			if (selectedVehicle.HasModifiedPhysicsProperties())
			{
				vehicle.CopyPhysicsPropertiesFrom(selectedVehicle);
			}
			EventTimelines.UpdateGameObjectReferences(selectedVehicle.gameObject, vehicle.gameObject);
			EventTimelines.UpdateForVehicleSkinChange(vehicle);
			vehicle.m_Checkpoints.AddRange(selectedVehicle.m_Checkpoints);
			selectedVehicle.m_Checkpoints.Clear();
			VehicleStopTrigger vehicleStopTrigger = VehicleStopTriggers.FindTriggerThatStopsVehicle(selectedVehicle.m_Guid);
			if ((bool)vehicleStopTrigger)
			{
				vehicleStopTrigger.m_VehicleGuid = vehicle.m_Guid;
				vehicleStopTrigger.m_SandboxItem.m_UndoGuid = Utils.GenerateUniqueId();
			}
			for (int num = vehicle.m_Checkpoints.Count - 1; num >= 0; num--)
			{
				vehicle.m_Checkpoints[num].m_VehicleGuid = vehicle.m_Guid;
				vehicle.m_Checkpoints[num].m_SandboxItem.m_UndoGuid = Utils.GenerateUniqueId();
			}
			vehicle.SetFlagAndCheckpointColor();
			Vehicles.DestroyVehicle(selectedVehicle);
			SandboxSelectionSet.CancelSelection();
			SandboxItem component = vehicle.GetComponent<SandboxItem>();
			if ((bool)component)
			{
				SandboxSelectionSet.SelectItem(component);
				EventEditor.SelectIconMatchingGameObject(vehicle.gameObject);
			}
			for (int i = 0; i < vehicle.m_Checkpoints.Count; i++)
			{
				if ((bool)vehicle.m_Checkpoints[i].m_Timeline)
				{
					vehicle.m_Checkpoints[i].m_Timeline.SetCheckpointSprite();
				}
			}
			SandboxUndo.SnapShot();
		}
		else
		{
			Prefabs.m_Instance.PreloadSingleAsset(text2, string.Empty, VehicleLoadedCallback);
		}
	}

	private void OnDelete()
	{
		if ((bool)SandboxSelectionSet.GetSelectedVehicle())
		{
			InterfaceAudio.Play("ui_build_delete");
			SandboxSelectionSet.Delete();
		}
	}

	private void ProcessInput(Vehicle vehicle)
	{
		if ((bool)vehicle && !GameStateCommonInput.IgnoreKeyboardInput())
		{
			if (GameInput.JustPressed(BindingType.FLIP_HORIZONTAL))
			{
				vehicle.m_Flipped = !vehicle.m_Flipped;
				m_FlipToggle.isOn = vehicle.m_Flipped;
				vehicle.SetLocalScale(vehicle.m_Flipped);
				vehicle.UpdatePolygonShapes();
				SandboxUndo.SnapShot();
			}
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

	private void OnDefaults()
	{
		InterfaceAudio.Play("ui_menu_select");
		PopUpMessage.DisplayWarning(Localize.Get("POPUP_RESET_PHYSICS"), useYesNoLables: true, ConfirmOnDefaults);
	}

	public void OnAddCheckpoint()
	{
		InterfaceAudio.Play("ui_menu_select");
		Checkpoint checkpoint = Sandbox.CreateCheckpointForVehicle(SandboxSelectionSet.GetSelectedVehicle());
		if ((bool)checkpoint)
		{
			SandboxTapeCheckpoint sandboxTapeCheckpoint = CreateCheckpointPanel(checkpoint, m_CheckpointsParent);
			if (sandboxTapeCheckpoint != null)
			{
				m_SandboxTapeCheckpoints.Add(sandboxTapeCheckpoint);
			}
		}
	}

	private SandboxTapeCheckpoint CreateCheckpointPanel(Checkpoint checkpoint, Transform parent)
	{
		GameObject gameObject = Object.Instantiate(m_CheckpointTapePrefab);
		if (gameObject == null)
		{
			return null;
		}
		Vector3 localScale = gameObject.transform.localScale;
		gameObject.transform.SetParent(parent);
		gameObject.transform.localScale = localScale;
		SandboxTapeCheckpoint component = gameObject.GetComponent<SandboxTapeCheckpoint>();
		component.m_Checkpoint = checkpoint;
		component.m_Text.text = checkpoint.GetTextMeshString();
		return component;
	}

	private void ConfirmOnDefaults()
	{
		Vehicle selectedVehicle = SandboxSelectionSet.GetSelectedVehicle();
		if ((bool)selectedVehicle)
		{
			selectedVehicle.SetDefaultPhysicsProperties();
			SandboxUndo.SnapShot();
			RefreshProperties(selectedVehicle);
		}
	}

	private void RotSliderChanged(float angle)
	{
		Vehicle selectedVehicle = SandboxSelectionSet.GetSelectedVehicle();
		if ((bool)selectedVehicle)
		{
			selectedVehicle.m_RotationDegrees = angle % 360f;
			selectedVehicle.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f - selectedVehicle.m_RotationDegrees));
			if (selectedVehicle.m_SandboxItem != null)
			{
				selectedVehicle.m_SandboxItem.SetFloatingTextToDefaultPosition();
			}
			selectedVehicle.UpdatePolygonShapes();
			m_SliderRot.m_SandboxInputField.m_InputField.text = Utils.FormatAngle(selectedVehicle.m_RotationDegrees);
		}
	}

	private void ScaleSliderChanged(float percentage)
	{
		if (m_SkipInputFieldUpdateFromSlider)
		{
			m_SkipInputFieldUpdateFromSlider = false;
			return;
		}
		Vehicle selectedVehicle = SandboxSelectionSet.GetSelectedVehicle();
		if ((bool)selectedVehicle)
		{
			float num = Mathf.Clamp(percentage / 100f, Vehicles.MIN_NORMALIZED_SCALE, Vehicles.MAX_NORMALIZED_SCALE);
			if (!Mathf.Approximately(num, 0f))
			{
				selectedVehicle.SetUniformScale(num);
				m_SliderScale.m_SandboxInputField.m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(num);
				SandboxItems.ResolveOverlappingFloatingText();
				selectedVehicle.m_SandboxItem.SetOutlineDirty(dirty: true);
			}
		}
	}

	public void RegenerateTapeCheckpoints(Vehicle vehicle)
	{
		for (int i = 0; i < m_SandboxTapeCheckpoints.Count; i++)
		{
			m_SandboxTapeCheckpoints[i].gameObject.SetActive(value: false);
			Object.Destroy(m_SandboxTapeCheckpoints[i].gameObject);
		}
		m_SandboxTapeCheckpoints.Clear();
		foreach (Checkpoint checkpoint in vehicle.m_Checkpoints)
		{
			SandboxTapeCheckpoint sandboxTapeCheckpoint = CreateCheckpointPanel(checkpoint, m_CheckpointsParent);
			if (sandboxTapeCheckpoint != null)
			{
				m_SandboxTapeCheckpoints.Add(sandboxTapeCheckpoint);
			}
		}
	}

	private void RefreshCheckpointButtons()
	{
		if (m_SandboxTapeCheckpoints.Count == 1)
		{
			m_SandboxTapeCheckpoints[0].EnableMoveUpButton(enable: false);
			m_SandboxTapeCheckpoints[0].EnableMoveDownButton(enable: false);
			return;
		}
		for (int i = 0; i < m_SandboxTapeCheckpoints.Count; i++)
		{
			if (i == 0)
			{
				m_SandboxTapeCheckpoints[i].EnableMoveUpButton(enable: false);
				m_SandboxTapeCheckpoints[i].EnableMoveDownButton(enable: true);
			}
			else if (i == m_SandboxTapeCheckpoints.Count - 1)
			{
				m_SandboxTapeCheckpoints[i].EnableMoveUpButton(enable: true);
				m_SandboxTapeCheckpoints[i].EnableMoveDownButton(enable: false);
			}
			else
			{
				m_SandboxTapeCheckpoints[i].EnableMoveUpButton(enable: true);
				m_SandboxTapeCheckpoints[i].EnableMoveDownButton(enable: true);
			}
		}
	}
}

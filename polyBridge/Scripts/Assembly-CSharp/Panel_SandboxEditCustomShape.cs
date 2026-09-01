using System.Collections.Generic;
using Assets.SimpleColorPicker.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel_SandboxEditCustomShape : MonoBehaviour
{
	public RectTransform m_VerticalLayoutRectTransform;

	[Header("Panels")]
	public GameObject m_MeshPanel;

	public GameObject m_TexturePanel;

	public GameObject m_ColorPanel;

	[Header("Nudge")]
	public Panel_SandboxNudge m_SandboxNudge;

	[Header("Dropdowns")]
	public TMP_Dropdown m_MeshDropdown;

	public TMP_Dropdown m_TextureDropdown;

	public TMP_Dropdown m_BehaviorDropdown;

	[Header("Scrolling")]
	public RectTransform m_ContentRectTransform;

	public Scrollbar m_Scrollbar;

	[Header("Buttons")]
	public Button m_EditShape;

	public Button m_Duplicate;

	public Button m_Delete;

	public Button m_ExportCustomShape;

	[Header("Input Fields")]
	public SandboxInputField m_InputFieldPosX;

	public SandboxInputField m_InputFieldPosY;

	public SandboxInputField m_InputFieldPosZ;

	public SandboxInputField m_InputFieldThickness;

	public SandboxInputField m_InputFieldMass;

	public SandboxInputField m_InputFieldBounciness;

	public SandboxInputField m_InputFieldPinMotorStrength;

	public SandboxInputField m_InputFieldPinTargetVelocity;

	public SandboxInputField m_InputFieldPinTargetAcceleration;

	public SandboxInputField m_InputFieldTiling;

	[Header("Icons")]
	public Image m_MotorStrengthIcon;

	public Image m_MotorStrengthRedIcon;

	[Header("Sliders")]
	public SandboxTapeSlider m_SliderRot;

	public SandboxTapeSlider m_SliderScale;

	public SandboxTapeSlider m_SliderMeshScale;

	[Header("Containers")]
	public GameObject m_PhyscisContainer;

	public GameObject m_VisualsContainer;

	[Header("Toggles")]
	public Toggle m_FlipToggle;

	public Toggle m_LowFrictionToggle;

	public Toggle m_CollideWithVehiclesToggle;

	public Toggle m_CollideWithRoadToggle;

	public Toggle m_CollideWithNodesToggle;

	public Toggle m_CollideWithRampsToggle;

	public Toggle m_CollideWithSplitNodesToggle;

	[Header("Colors")]
	public ColorPicker m_ColorPicker;

	private PointerEvents m_FlipTogglePointerEvents;

	private PointerEvents m_LowFrictionTogglePointerEvents;

	private PointerEvents m_CollideWithVehiclesTogglePointerEvents;

	private PointerEvents m_CollideWithRoadTogglePointerEvents;

	private PointerEvents m_CollideWithNodesTogglePointerEvents;

	private PointerEvents m_CollideWithRampsTogglePointerEvents;

	private PointerEvents m_CollideWithSplitNodesTogglePointerEvents;

	private CustomShape m_LastRefreshedShape;

	private bool m_IsDraggingScrollbar;

	private float m_ContentLastY;

	private PointerEvents m_ContentPointerEvents;

	private Dictionary<int, string> m_TextureDropdownMap = new Dictionary<int, string>();

	private Dictionary<string, string> m_CustomShapeMeshes = new Dictionary<string, string>();

	private Dictionary<int, string> m_MeshDropdownMap = new Dictionary<int, string>();

	private const float TIME_SHOW_CUSTOM_MESH_SECONDS = 2f;

	private bool m_DynamicPropSetupEnabled;

	private bool m_SkipInputFieldUpdateFromSlider;

	private void Awake()
	{
		m_FlipTogglePointerEvents = m_FlipToggle.GetComponent<PointerEvents>();
		m_FlipTogglePointerEvents.RegisterOnClickedDelegate(OnFlipToggle);
		m_LowFrictionTogglePointerEvents = m_LowFrictionToggle.GetComponent<PointerEvents>();
		m_LowFrictionTogglePointerEvents.RegisterOnClickedDelegate(OnLowFrictionToggle);
		m_CollideWithVehiclesTogglePointerEvents = m_CollideWithVehiclesToggle.GetComponent<PointerEvents>();
		m_CollideWithVehiclesTogglePointerEvents.RegisterOnClickedDelegate(OnCollideWithVehiclesToggle);
		m_CollideWithRoadTogglePointerEvents = m_CollideWithRoadToggle.GetComponent<PointerEvents>();
		m_CollideWithRoadTogglePointerEvents.RegisterOnClickedDelegate(OnCollideWithRoadToggle);
		m_CollideWithNodesTogglePointerEvents = m_CollideWithNodesToggle.GetComponent<PointerEvents>();
		m_CollideWithNodesTogglePointerEvents.RegisterOnClickedDelegate(OnCollideWithNodesToggle);
		m_CollideWithRampsTogglePointerEvents = m_CollideWithRampsToggle.GetComponent<PointerEvents>();
		m_CollideWithRampsTogglePointerEvents.RegisterOnClickedDelegate(OnCollideWithRampsToggle);
		m_CollideWithSplitNodesTogglePointerEvents = m_CollideWithSplitNodesToggle.GetComponent<PointerEvents>();
		m_CollideWithSplitNodesTogglePointerEvents.RegisterOnClickedDelegate(OnCollideWithSplitNodesToggle);
		m_MeshDropdown.onValueChanged.AddListener(delegate
		{
			OnMeshChanged();
		});
		m_MeshDropdown.alphaFadeSpeed = 0f;
		m_TextureDropdown.onValueChanged.AddListener(delegate
		{
			OnTextureChanged();
		});
		m_TextureDropdown.alphaFadeSpeed = 0f;
		m_BehaviorDropdown.onValueChanged.AddListener(delegate
		{
			OnBehaviorChanged();
		});
		m_BehaviorDropdown.alphaFadeSpeed = 0f;
		m_SliderRot.SetRange(-180f, 180f, 1f);
		m_SliderRot.SetCallback(RotSliderChanged);
		m_SliderScale.SetRange(CustomShapes.MIN_NORMALIZED_SCALE_SLIDER * 100f, CustomShapes.MAX_NORMALIZED_SCALE_SLIDER * 100f, 1f);
		m_SliderScale.SetCallback(ScaleSliderChanged);
		m_SliderMeshScale.SetRange(CustomShapes.MIN_NORMALIZED_SCALE_SLIDER * 100f, CustomShapes.MAX_NORMALIZED_SCALE_SLIDER * 100f, 1f);
		m_SliderMeshScale.SetCallback(ScaleMeshSliderChanged);
		m_EditShape.onClick.AddListener(OnEditShape);
		m_Duplicate.onClick.AddListener(OnDuplicate);
		m_Delete.onClick.AddListener(OnDelete);
		m_ExportCustomShape.onClick.AddListener(OnExportCustomShape);
		m_ContentLastY = m_ContentRectTransform.anchoredPosition.y;
		m_ContentPointerEvents = m_ContentRectTransform.GetComponent<PointerEvents>();
		InitCustomShapeMeshesDict();
	}

	private void Update()
	{
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if (!(selectedCustomShape == null))
		{
			if (selectedCustomShape != m_LastRefreshedShape)
			{
				RefreshProperties(selectedCustomShape);
			}
			if (Mathf.Abs(m_ContentRectTransform.anchoredPosition.y - m_ContentLastY) > 0.001f)
			{
				m_IsDraggingScrollbar = true;
			}
			m_ContentLastY = m_ContentRectTransform.anchoredPosition.y;
			if (m_IsDraggingScrollbar && GameInput.GetMouseButtonJustReleased(0))
			{
				m_IsDraggingScrollbar = false;
			}
			MaybeUpdateColor();
			SetPosZVisibility();
			SetPanelVisibility();
			SetTilingVisibility();
			SetFieldVisibilityBasedOnBehavior(selectedCustomShape.m_Behavior);
			SetMotorStrengthIcon(selectedCustomShape);
			ProcessInput(selectedCustomShape);
			LayoutRebuilder.MarkLayoutForRebuild(m_VerticalLayoutRectTransform);
		}
	}

	public void UpdateForCurrentDevice()
	{
		m_SandboxNudge.UpdateForCurrentDevice();
	}

	public void ForceRefresh()
	{
		m_LastRefreshedShape = null;
	}

	public void DynamicPropSetupEnable(bool enable)
	{
		m_DynamicPropSetupEnabled = enable;
	}

	private void SetPosZVisibility()
	{
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if ((bool)selectedCustomShape)
		{
			m_InputFieldPosZ.gameObject.SetActive(selectedCustomShape.m_Anchors.Count == 0);
		}
	}

	private void SetPanelVisibility()
	{
		m_MeshPanel.SetActive(m_DynamicPropSetupEnabled);
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if ((bool)selectedCustomShape)
		{
			bool active = m_DynamicPropSetupEnabled || !selectedCustomShape.IsDynamicProp();
			m_TexturePanel.gameObject.SetActive(active);
			m_ColorPanel.gameObject.SetActive(active);
			m_ExportCustomShape.gameObject.SetActive(active);
			m_EditShape.gameObject.SetActive(active);
			m_InputFieldThickness.gameObject.SetActive(!selectedCustomShape.IsDynamicProp());
		}
	}

	private void SetTilingVisibility()
	{
		m_InputFieldTiling.gameObject.SetActive(m_TextureDropdown.value > 1 && m_MeshDropdown.value == 0);
		m_VisualsContainer.SetActive(m_TextureDropdown.value > 1 && m_MeshDropdown.value == 0);
	}

	private void SetFieldVisibilityBasedOnBehavior(CustomShapeBehavior behavior)
	{
		m_InputFieldMass.gameObject.SetActive(behavior != CustomShapeBehavior.STATIC);
		m_InputFieldPinMotorStrength.gameObject.SetActive(behavior == CustomShapeBehavior.MOTORIZED);
		m_InputFieldPinTargetVelocity.gameObject.SetActive(behavior == CustomShapeBehavior.MOTORIZED);
		m_InputFieldPinTargetAcceleration.gameObject.SetActive(behavior == CustomShapeBehavior.MOTORIZED);
	}

	private void SetMotorStrengthIcon(CustomShape shape)
	{
		m_MotorStrengthIcon.gameObject.SetActive(shape.m_PinMotorStrength >= shape.m_CollisionInfo.minStrengthForDesiredAcceleration);
		m_MotorStrengthRedIcon.gameObject.SetActive(!m_MotorStrengthIcon.gameObject.activeSelf);
	}

	private void OnEnable()
	{
		PopulateMeshDropdown();
		PopulateTextureDropdown();
		PopulateBehaviorDropdown();
		m_IsDraggingScrollbar = false;
		Update();
	}

	private void OnDisable()
	{
		if ((bool)m_LastRefreshedShape)
		{
			m_LastRefreshedShape.EnableMeshRendering(on: false);
		}
		if ((bool)m_LastRefreshedShape && m_LastRefreshedShape.m_Dirty)
		{
			m_LastRefreshedShape.RebuildMesh();
			m_LastRefreshedShape.m_Dirty = false;
		}
		GameUI.m_Instance.CancelDropdownSelection(m_TextureDropdown);
		GameUI.m_Instance.CancelDropdownSelection(m_BehaviorDropdown);
		m_LastRefreshedShape = null;
		m_SliderRot.m_SandboxInputField.m_ExternalContinuousHoldActive = false;
	}

	public void SkipInputFieldUpdateFromSlider()
	{
		m_SkipInputFieldUpdateFromSlider = true;
	}

	public bool CustomShapeTextureDropDownHasScrollFocus()
	{
		Scrollbar componentInChildren = m_TextureDropdown.GetComponentInChildren<Scrollbar>(includeInactive: false);
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

	public void RefreshProperties(CustomShape shape)
	{
		if ((bool)shape)
		{
			RefreshPosition(shape);
			RefreshDropdowns(shape);
			RefreshInputFields(shape);
			RefreshSliders(shape);
			RefreshToggles(shape);
			RefreshColorPicker(shape.m_Color);
			m_LastRefreshedShape = shape;
		}
	}

	public void RefreshPosition(CustomShape shape)
	{
		m_InputFieldPosX.m_InputField.text = Utils.FormatThreeDecimalPlaces(shape.transform.position.x);
		m_InputFieldPosY.m_InputField.text = Utils.FormatThreeDecimalPlaces(shape.transform.position.y);
		m_InputFieldPosZ.m_InputField.text = Utils.FormatThreeDecimalPlaces(shape.transform.position.z);
	}

	public void SetShapeColor(Color color)
	{
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if ((bool)selectedCustomShape && !selectedCustomShape.m_Color.Equals(color))
		{
			selectedCustomShape.UpdateShaderProperties(color, buildMode: false);
			RefreshColorPicker(color);
		}
	}

	public bool IsDraggingScrollbar()
	{
		return m_IsDraggingScrollbar;
	}

	public bool HasScrollFocus()
	{
		if (m_Scrollbar.gameObject.activeInHierarchy)
		{
			return m_ContentPointerEvents.m_IsHovering;
		}
		return false;
	}

	public bool ColorPickerHasInputFocus()
	{
		return m_ColorPicker.InputFieldHasFocus();
	}

	private void RefreshInputFields(CustomShape shape)
	{
		m_InputFieldThickness.m_InputField.text = Utils.FormatDistanceOneDecimalPlace(shape.m_Thickness);
		m_InputFieldMass.m_InputField.text = Utils.FormatWeight(shape.m_Mass * BridgePhysics.KgToPg);
		m_InputFieldBounciness.m_InputField.text = Utils.FormatTwoDecimalPlaces(shape.m_Bounciness);
		m_InputFieldPinMotorStrength.m_InputField.text = Utils.FormatOneDecimalPlace(shape.m_PinMotorStrength);
		m_InputFieldPinTargetVelocity.m_InputField.text = Utils.FormatOneDecimalPlace(shape.m_PinTargetVelocity);
		m_InputFieldPinTargetAcceleration.m_InputField.text = Utils.FormatSeconds(shape.m_PinTargetAccelerationSeconds);
		m_InputFieldTiling.m_InputField.text = Utils.FormatTwoDecimalPlaces(shape.m_TextureTiling);
	}

	private void RefreshSliders(CustomShape shape)
	{
		m_SliderRot.SetValue(shape.m_RotationDegrees);
		m_SliderRot.m_SandboxInputField.m_InputField.text = Utils.FormatAngle(shape.m_RotationDegrees);
		float num = Mathf.Abs(shape.transform.localScale.x);
		m_SliderScale.SetValue(num * 100f);
		m_SliderScale.m_SandboxInputField.m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(num);
		if ((bool)shape.m_CustomMesh)
		{
			float num2 = Mathf.Abs(shape.m_CustomMesh.transform.localScale.x);
			m_SliderMeshScale.SetValue(num2 * 100f);
			m_SliderMeshScale.m_SandboxInputField.m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(num2);
		}
	}

	private void RefreshToggles(CustomShape shape)
	{
		m_FlipToggle.isOn = shape.transform.localScale.x < 0f;
		m_LowFrictionToggle.isOn = shape.m_LowFriction;
		m_CollideWithVehiclesToggle.isOn = shape.m_CollidesWithVehicles;
		m_CollideWithRoadToggle.isOn = shape.m_CollidesWithRoad;
		m_CollideWithNodesToggle.isOn = shape.m_CollidesWithNodes;
		m_CollideWithRampsToggle.isOn = shape.m_CollidesWithRamps;
		m_CollideWithSplitNodesToggle.isOn = shape.m_CollidesWithSplitNodes;
	}

	private void RefreshColorPicker(Color color)
	{
		if (m_ColorPicker.Texture != null)
		{
			m_ColorPicker.SetColor(color);
		}
	}

	private void OnEditShape()
	{
		InterfaceAudio.Play("ui_menu_select");
		if ((bool)SandboxSelectionSet.GetSelectedCustomShape())
		{
			GameUI.m_Instance.m_Help.gameObject.SetActive(value: false);
			GameUI.m_Instance.m_SandboxEditCustomShape.gameObject.SetActive(value: false);
			GameUI.m_Instance.m_SandboxEditCustomShapeTools.gameObject.SetActive(value: true);
		}
	}

	private void OnDuplicate()
	{
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if ((bool)selectedCustomShape)
		{
			Vector3 offset = new Vector3(GameGrid.RoundToNearestGridSquare(selectedCustomShape.m_PolygonCollider2D.bounds.size.x + GameGrid.m_Spacing), 0f, 0f);
			CustomShape customShape = selectedCustomShape.Duplicate(Prefabs.m_Instance.m_CustomShape, offset);
			if ((bool)customShape)
			{
				SandboxUndo.SnapShot();
				SandboxSelectionSet.ForceSelection(customShape.m_SandboxItem);
				InterfaceAudio.Play("ui_build_generic_place");
			}
		}
	}

	private void OnFlipToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if ((bool)selectedCustomShape)
		{
			selectedCustomShape.Flip(m_FlipToggle.isOn);
			SandboxUndo.SnapShot();
		}
	}

	private void OnLowFrictionToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if ((bool)selectedCustomShape)
		{
			selectedCustomShape.m_LowFriction = m_LowFrictionToggle.isOn;
			SandboxUndo.SnapShot();
		}
	}

	private void OnCollideWithRoadToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if ((bool)selectedCustomShape)
		{
			selectedCustomShape.m_CollidesWithRoad = m_CollideWithRoadToggle.isOn;
			SandboxUndo.SnapShot();
		}
	}

	private void OnCollideWithVehiclesToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if ((bool)selectedCustomShape)
		{
			selectedCustomShape.m_CollidesWithVehicles = m_CollideWithVehiclesToggle.isOn;
			SandboxUndo.SnapShot();
		}
	}

	private void OnCollideWithNodesToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if ((bool)selectedCustomShape)
		{
			selectedCustomShape.m_CollidesWithNodes = m_CollideWithNodesToggle.isOn;
			SandboxUndo.SnapShot();
		}
	}

	private void OnCollideWithRampsToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if ((bool)selectedCustomShape)
		{
			selectedCustomShape.m_CollidesWithRamps = m_CollideWithRampsToggle.isOn;
			SandboxUndo.SnapShot();
		}
	}

	private void OnCollideWithSplitNodesToggle()
	{
		if (m_CollideWithSplitNodesToggle.interactable)
		{
			InterfaceAudio.Play("ui_settings_toggle");
			CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
			if ((bool)selectedCustomShape)
			{
				selectedCustomShape.m_CollidesWithSplitNodes = m_CollideWithSplitNodesToggle.isOn;
				SandboxUndo.SnapShot();
			}
		}
		else
		{
			InterfaceAudio.PlayErrorBeep();
		}
	}

	private void OnDelete()
	{
		if ((bool)SandboxSelectionSet.GetSelectedCustomShape())
		{
			InterfaceAudio.Play("ui_build_delete");
			SandboxSelectionSet.Delete();
		}
	}

	private void ProcessInput(CustomShape shape)
	{
		if ((bool)shape && !GameStateCommonInput.IgnoreKeyboardInput())
		{
			if (GameInput.JustPressed(BindingType.FLIP_HORIZONTAL))
			{
				m_FlipToggle.isOn = !m_FlipToggle.isOn;
				shape.Flip(m_FlipToggle.isOn);
				shape.UpdatePolygonShapes();
				SandboxUndo.SnapShot();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.WEST))
			{
				ExecuteEvents.Execute(m_Delete.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
			}
			else if (GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH))
			{
				ExecuteEvents.Execute(m_Duplicate.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
			}
			m_SliderRot.m_SandboxInputField.ProcessInputForRotation();
		}
	}

	private void OnBehaviorChanged()
	{
		InterfaceAudio.Play("ui_menu_select");
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if (!selectedCustomShape)
		{
			return;
		}
		selectedCustomShape.m_Behavior = (CustomShapeBehavior)m_BehaviorDropdown.value;
		if (selectedCustomShape.m_Behavior == CustomShapeBehavior.MOTORIZED && selectedCustomShape.m_Pins.Count == 0)
		{
			Vector3 worldPos = new Vector3(selectedCustomShape.transform.position.x, selectedCustomShape.transform.position.y, (0f - selectedCustomShape.m_Thickness) / 2f);
			CustomShapePin customShapePin = selectedCustomShape.AddPin(worldPos);
			if (customShapePin != null)
			{
				customShapePin.ShowMesh(show: false);
			}
		}
		CustomShapes.UpdateCustomShapeMinimumStrengthHint(selectedCustomShape);
		SandboxUndo.SnapShot();
	}

	private void OnMeshChanged()
	{
		InterfaceAudio.Play("ui_menu_select");
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if (!selectedCustomShape)
		{
			return;
		}
		if (!m_MeshDropdownMap.ContainsKey(m_MeshDropdown.value))
		{
			Debug.LogWarning($"Could not find {m_MeshDropdown.value} in m_TextureDropdownMap");
			return;
		}
		string text = m_MeshDropdownMap[m_MeshDropdown.value];
		if (!(selectedCustomShape.m_MeshId != text))
		{
			return;
		}
		if (text == CustomShapes.AUTO_GENERATED_MESH_ID)
		{
			selectedCustomShape.RebuildMesh();
			selectedCustomShape.m_AutoGeneratedMesh.SetActive(value: true);
			selectedCustomShape.m_MeshRenderer = selectedCustomShape.m_AutoGeneratedMesh.GetComponent<MeshRenderer>();
			selectedCustomShape.m_DisableMeshRenderingTime = Time.realtimeSinceStartup + 2f;
			if (selectedCustomShape.m_CustomMesh != null)
			{
				selectedCustomShape.m_CustomMesh.SetActive(value: false);
			}
		}
		else
		{
			selectedCustomShape.UseCustomMesh(text, Vector3.zero, 2f);
			selectedCustomShape.m_AutoGeneratedMesh.SetActive(value: false);
		}
		selectedCustomShape.m_MeshId = text;
		SandboxUndo.SnapShot();
	}

	private void OnTextureChanged()
	{
		InterfaceAudio.Play("ui_menu_select");
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if (!selectedCustomShape)
		{
			return;
		}
		if (!m_TextureDropdownMap.ContainsKey(m_TextureDropdown.value))
		{
			Debug.LogWarning($"Could not find {m_TextureDropdown.value} in m_TextureDropdownMap");
			return;
		}
		string text = m_TextureDropdownMap[m_TextureDropdown.value];
		if (!(selectedCustomShape.m_Texture != null) || !(selectedCustomShape.m_Texture.m_ID == text))
		{
			Debug.Log("Change texture to " + text);
			selectedCustomShape.m_Texture = CustomShapeTextures.m_Instance.GetTextureFromId(text);
			selectedCustomShape.UpdateShaderProperties(selectedCustomShape.m_Color, buildMode: false);
			SandboxUndo.SnapShot();
		}
	}

	private void RefreshDropdowns(CustomShape shape)
	{
		RefreshMeshDropdown(shape);
		RefreshTextureDropdown(shape);
		DropdownUtils.SelectItem(m_BehaviorDropdown, (int)shape.m_Behavior);
	}

	private void RefreshMeshDropdown(CustomShape shape)
	{
		if (shape.m_MeshId == CustomShapes.AUTO_GENERATED_MESH_ID)
		{
			DropdownUtils.SelectItem(m_MeshDropdown, 0);
			return;
		}
		foreach (KeyValuePair<int, string> item in m_MeshDropdownMap)
		{
			if (item.Value == shape.m_MeshId)
			{
				DropdownUtils.SelectItem(m_MeshDropdown, item.Key);
			}
		}
	}

	private void RefreshTextureDropdown(CustomShape shape)
	{
		if (shape.m_Texture == null)
		{
			DropdownUtils.SelectItem(m_TextureDropdown, 0);
			return;
		}
		string text = Localize.Get(shape.m_Texture.m_DisplayNameLocID);
		if (string.IsNullOrEmpty(text))
		{
			text = shape.m_Texture.m_DisplayNameLocID;
		}
		DropdownUtils.SelectItem(m_TextureDropdown, text);
	}

	private void RotSliderChanged(float angle)
	{
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if ((bool)selectedCustomShape)
		{
			selectedCustomShape.m_RotationDegrees = angle % 360f;
			selectedCustomShape.UpdateAfterRotation();
			selectedCustomShape.UpdatePolygonShapes();
			selectedCustomShape.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f - selectedCustomShape.m_RotationDegrees));
			if (selectedCustomShape.m_SandboxItem != null)
			{
				selectedCustomShape.m_SandboxItem.SetFloatingTextToDefaultPosition();
			}
			m_SliderRot.m_SandboxInputField.m_InputField.text = Utils.FormatAngle(selectedCustomShape.m_RotationDegrees);
		}
	}

	private void ScaleSliderChanged(float percentage)
	{
		if (m_SkipInputFieldUpdateFromSlider)
		{
			m_SkipInputFieldUpdateFromSlider = false;
			return;
		}
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if ((bool)selectedCustomShape)
		{
			float num = Mathf.Clamp(percentage / 100f, CustomShapes.MIN_NORMALIZED_SCALE, CustomShapes.MAX_NORMALIZED_SCALE);
			if (!Mathf.Approximately(num, 0f))
			{
				selectedCustomShape.transform.localScale = new Vector3((selectedCustomShape.transform.localScale.x < 0f) ? (0f - num) : num, num, selectedCustomShape.IsDynamicProp() ? num : 1f);
				selectedCustomShape.UpdateVisualScale();
				selectedCustomShape.UpdatePolygonShapes();
				m_SliderScale.m_SandboxInputField.m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(num);
				selectedCustomShape.m_SandboxItem.SetOutlineDirty(dirty: true);
			}
		}
	}

	private void ScaleMeshSliderChanged(float percentage)
	{
		if (m_SkipInputFieldUpdateFromSlider)
		{
			m_SkipInputFieldUpdateFromSlider = false;
			return;
		}
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if ((bool)selectedCustomShape && (bool)selectedCustomShape.m_CustomMesh)
		{
			float num = Mathf.Clamp(percentage / 100f, CustomShapes.MIN_NORMALIZED_SCALE, CustomShapes.MAX_NORMALIZED_SCALE);
			if (!Mathf.Approximately(num, 0f))
			{
				selectedCustomShape.m_CustomMesh.transform.localScale = new Vector3((selectedCustomShape.m_CustomMesh.transform.localScale.x < 0f) ? (0f - num) : num, num, 1f);
				m_SliderMeshScale.m_SandboxInputField.m_InputField.text = Utils.FormatPercentage(num);
			}
		}
	}

	private void OnExportCustomShape()
	{
		PopupInputField.Display(Localize.Get("UI_CUSTOM_SHAPE_EXPORT_NAME"), string.Empty, isFilename: false, isDirectory: false, SandboxSelectionSet.ExportSelectedCustomShapes);
	}

	private void PopulateBehaviorDropdown()
	{
		m_BehaviorDropdown.ClearOptions();
		List<string> list = new List<string>();
		list.Add(Localize.Get("UI_CUSTOM_SHAPE_DYNAMIC"));
		list.Add(Localize.Get("UI_CUSTOM_SHAPE_STATIC"));
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if ((bool)selectedCustomShape && !selectedCustomShape.IsDynamicProp())
		{
			list.Add(Localize.Get("UI_CUSTOM_SHAPE_MOTORIZED"));
		}
		m_BehaviorDropdown.AddOptions(list);
	}

	private void PopulateMeshDropdown()
	{
		m_MeshDropdown.ClearOptions();
		m_MeshDropdownMap.Clear();
		List<string> list = new List<string>();
		list.Add("Auto Generated");
		m_MeshDropdownMap.Add(0, CustomShapes.AUTO_GENERATED_MESH_ID);
		int num = 1;
		foreach (KeyValuePair<string, string> customShapeMesh in m_CustomShapeMeshes)
		{
			list.Add(Localize.Get(customShapeMesh.Key));
			m_MeshDropdownMap.Add(num++, customShapeMesh.Value);
		}
		m_MeshDropdown.AddOptions(list);
	}

	private void PopulateTextureDropdown()
	{
		m_TextureDropdown.ClearOptions();
		m_TextureDropdownMap.Clear();
		if (CustomShapeTextures.m_Instance == null)
		{
			return;
		}
		List<string> list = new List<string>();
		int num = 0;
		foreach (CustomShapeTexture allTexture in CustomShapeTextures.m_Instance.GetAllTextures())
		{
			string text = Localize.Get(allTexture.m_DisplayNameLocID);
			if (string.IsNullOrEmpty(text))
			{
				text = allTexture.m_DisplayNameLocID;
			}
			list.Add(text);
			m_TextureDropdownMap.Add(num++, allTexture.m_ID);
		}
		m_TextureDropdown.AddOptions(list);
	}

	private void InitCustomShapeMeshesDict()
	{
		m_CustomShapeMeshes.Add("CUSTOM_SHAPE_BARRIER", "CustomShapeBarrier");
		m_CustomShapeMeshes.Add("DECOR_BARREL", "CustomShapeBarrel");
		m_CustomShapeMeshes.Add("DECOR_TOXICGASTANK", "CustomShapeToxicGasTank");
		m_CustomShapeMeshes.Add("UI_PROP_ANVIL", "CustomShapeAnvil");
		m_CustomShapeMeshes.Add("UI_PROP_BOWLINGBALL", "CustomShapeBowlingBall");
		m_CustomShapeMeshes.Add("UI_PROP_BOWLINGPIN", "CustomShapeBowlingPin");
		m_CustomShapeMeshes.Add("UI_PROP_CARDBOARDBOX1", "CustomShapeCardboardBox1");
		m_CustomShapeMeshes.Add("UI_PROP_CARDBOARDBOX2", "CustomShapeCardboardBox2");
		m_CustomShapeMeshes.Add("UI_PROP_CARDBOARDBOX3", "CustomShapeCardboardBox3");
		m_CustomShapeMeshes.Add("UI_PROP_CONE", "CustomShapeCone");
		m_CustomShapeMeshes.Add("UI_PROP_CRATE", "CustomShapeCrate");
		m_CustomShapeMeshes.Add("UI_PROP_LARGECRATE", "CustomShapeLargeCrate");
		m_CustomShapeMeshes.Add("UI_PROP_FRIDGE", "CustomShapeFridge");
		m_CustomShapeMeshes.Add("UI_PROP_WHEELS", "CustomShapeWheels");
	}

	private void MaybeUpdateColor()
	{
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if ((bool)selectedCustomShape && GameInput.GetMouseButtonJustReleased(0) && m_ColorPicker.Color != selectedCustomShape.m_Color)
		{
			SetShapeColor(m_ColorPicker.Color);
			SandboxUndo.SnapShot();
		}
	}
}

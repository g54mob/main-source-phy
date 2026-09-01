using System;
using Poly.Geometry;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_SandboxEditCustomShapeTools : MonoBehaviour
{
	[NonSerialized]
	public CustomShapeAnchor m_GhostAnchor;

	private CustomShapeAnchor m_HoverAnchor;

	private CustomShapeAnchor m_MovingAnchor;

	private bool m_CreatedAnchorBeforeMoving;

	[NonSerialized]
	public CustomShapePin m_GhostPin;

	private CustomShapePin m_HoverPin;

	private CustomShapePin m_MovingPin;

	private bool m_CreatedPinBeforeMoving;

	[Header("Buttons")]
	public Button m_ButtonBack;

	public Button m_ResetShape;

	[Header("Buttons")]
	public SandboxRadioButton m_EditShape;

	public SandboxRadioButton m_EditStaticPins;

	public SandboxRadioButton m_EditDynamicAnchors;

	[Header("Toggles")]
	public Toggle m_SnapToGridToggle;

	public ModeToggle m_EditOrDeleteToggle;

	[Header("Help")]
	public GameObject m_HelpPanel;

	public TextMeshProUGUI m_HelpText;

	private CustomShape m_LastRefreshedShape;

	private Vector2 m_OffsetFromPointer;

	private Vector2 m_StartDragMouseScreenPos;

	private CustomShapeEditMode m_EditMode;

	private CustomShapeEditSubMode m_EditSubMode;

	private bool m_ReturnToAddMoveSubmode;

	private PointerEvents m_SnapToGridTogglePointerEvents;

	[NonSerialized]
	public CustomShapeVert m_GhostVert;

	private CustomShapeVert m_HoverVert;

	private CustomShapeVert m_MovingVert;

	private Vector3 m_LastValidPositionOfMovingVert;

	private readonly float VERT_MIN_DISTANCE = 0.01001f;

	private void StartAnchors()
	{
		CreateGhostAnchor();
	}

	private void UpdateAnchors()
	{
		UpdateDefaultColorForAllAnchors();
		UpdateHoverAnchor();
		UpdateGhostAnchor(GameInput.GetMousePosition());
		UpdateMovingAnchor(GameInput.GetMousePosition());
	}

	private void OnEnableAnchors()
	{
	}

	private void OnDisableAnchors()
	{
		UpdateDefaultColorForAllAnchors();
		m_CreatedAnchorBeforeMoving = false;
		if ((bool)m_GhostAnchor)
		{
			m_GhostAnchor.gameObject.SetActive(value: false);
		}
		m_MovingAnchor = null;
	}

	public bool IsMovingAnchor()
	{
		if (!m_MovingAnchor)
		{
			return false;
		}
		return true;
	}

	private void EnterEditModeAnchors()
	{
		ShowAnchors();
		ShowPinsDisabled();
		HideVerts();
	}

	private void ProcessInputAnchors()
	{
		if (GameInput.GetMouseButtonIsDown(0) && DeleteSubModeActive() && (bool)m_HoverAnchor)
		{
			DeleteAnchor(m_HoverAnchor);
		}
		if (GameInput.GetMouseButtonJustPressed(0) && !DeleteSubModeActive())
		{
			if (m_GhostAnchor.gameObject.activeInHierarchy)
			{
				if (!BridgeJoints.AnchorOverlapsBounds(m_GhostAnchor.m_BoxCollider.bounds))
				{
					AddAnchor(m_GhostAnchor.transform.position);
					m_CreatedAnchorBeforeMoving = true;
					UpdateHoverAnchor();
					StartMovingAnchor(m_HoverAnchor, GameInput.GetMousePosition());
				}
			}
			else
			{
				StartMovingAnchor(m_HoverAnchor, GameInput.GetMousePosition());
			}
		}
		if (GameInput.JustReleased(BindingType.DRAW_BUILD) && ((bool)m_MovingAnchor || m_CreatedAnchorBeforeMoving))
		{
			if (MouseHasMovedSinceStartDrag(GameInput.GetMousePosition()) || m_CreatedAnchorBeforeMoving)
			{
				SandboxUndo.SnapShot();
			}
			m_CreatedAnchorBeforeMoving = false;
			m_MovingAnchor = null;
			m_OffsetFromPointer = Vector2.zero;
		}
	}

	private void UpdateDefaultColorForAllAnchors()
	{
		if (!m_LastRefreshedShape)
		{
			return;
		}
		foreach (CustomShapeAnchor anchor in m_LastRefreshedShape.m_Anchors)
		{
			anchor.m_SpriteRenderer.color = (m_LastRefreshedShape.OverlapsPoint(anchor.transform.position) ? Color.white : GameUI.m_Instance.m_RedTextColor);
		}
	}

	private void UpdateHoverAnchor()
	{
		if (!m_MovingAnchor)
		{
			if ((bool)m_HoverAnchor)
			{
				m_HoverAnchor.m_SpriteRenderer.color = Color.white;
			}
			m_HoverAnchor = GetAnchorUnderMouse(GameInput.GetMousePosition());
		}
	}

	private void UpdateGhostAnchor(Vector2 mouseScreenPos)
	{
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if (!selectedCustomShape || DeleteSubModeActive() || GameInput.GetMouseButtonIsDown(0) || (bool)m_HoverAnchor)
		{
			m_GhostAnchor.gameObject.SetActive(value: false);
		}
		else if (IsMouseOverShape(selectedCustomShape, GameInput.GetMousePosition()))
		{
			m_GhostAnchor.gameObject.SetActive(value: true);
			m_GhostAnchor.transform.position = Utils.GetWorldPointFromScreenPos(m_OffsetFromPointer + mouseScreenPos);
			if (!MoveOffGrid())
			{
				m_GhostAnchor.transform.position = GameGrid.SnapPosToGrid(m_GhostAnchor.transform.position);
			}
		}
		else
		{
			m_GhostAnchor.gameObject.SetActive(value: false);
		}
	}

	private void UpdateMovingAnchor(Vector2 mouseScreenPos)
	{
		if (!m_MovingAnchor)
		{
			return;
		}
		CustomShape componentInParent = m_MovingAnchor.GetComponentInParent<CustomShape>();
		if ((bool)componentInParent && MouseHasMovedSinceStartDrag(mouseScreenPos))
		{
			BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(m_MovingAnchor.m_BridgeJointGuid);
			Vector3 position = m_MovingAnchor.transform.position;
			bridgeJoint.m_SandboxItem.SetOutlineDirty(dirty: true);
			Vector3 vector = Utils.GetWorldPointFromScreenPos(m_OffsetFromPointer + mouseScreenPos);
			if (!MoveOffGrid())
			{
				vector = GameGrid.SnapPosToGrid(vector);
			}
			vector = Utils.V3toV2(vector - componentInParent.transform.position) + Utils.V3toV2(componentInParent.transform.position);
			m_MovingAnchor.transform.position = Utils.V2toV3(vector);
			if (BridgeJoints.AnchorOverlapsAnchor(bridgeJoint))
			{
				m_MovingAnchor.transform.position = position;
			}
		}
	}

	private CustomShapeAnchor GetAnchorUnderMouse(Vector2 screenPos)
	{
		Vector3 worldPointFromScreenPos = Utils.GetWorldPointFromScreenPos(screenPos);
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if (selectedCustomShape != null)
		{
			foreach (CustomShapeAnchor anchor in selectedCustomShape.m_Anchors)
			{
				BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(anchor.m_BridgeJointGuid);
				worldPointFromScreenPos.z = bridgeJoint.m_SandboxItem.m_Colliders[0].bounds.center.z;
				if (bridgeJoint.m_SandboxItem.m_Colliders[0].bounds.Contains(worldPointFromScreenPos))
				{
					return anchor;
				}
			}
		}
		return null;
	}

	public void AddAnchor(Vector3 worldPos)
	{
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if ((bool)selectedCustomShape && selectedCustomShape.AddAnchor(m_GhostAnchor.transform.position))
		{
			InterfaceAudio.Play("ui_menu_hover");
		}
	}

	private void DeleteAnchor(CustomShapeAnchor anchor)
	{
		if ((bool)anchor)
		{
			CustomShape componentInParent = anchor.GetComponentInParent<CustomShape>();
			if (componentInParent != null)
			{
				componentInParent.DestroyAnchor(anchor);
				SandboxUndo.SnapShot();
			}
		}
	}

	private void StartMovingAnchor(CustomShapeAnchor anchor, Vector2 mouseScreenPos)
	{
		m_MovingAnchor = anchor;
		if (anchor != null)
		{
			StartDrag(anchor.transform.position, mouseScreenPos);
		}
	}

	private void ShowAnchors()
	{
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if ((bool)selectedCustomShape)
		{
			selectedCustomShape.ShowAnchors();
		}
	}

	private void ShowAnchorsDisabled()
	{
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if (!selectedCustomShape)
		{
			return;
		}
		ShowAnchors();
		foreach (CustomShapeAnchor anchor in selectedCustomShape.m_Anchors)
		{
			anchor.m_SpriteRenderer.color = GameUI.m_Instance.m_CustomShapeDisabledColor;
		}
	}

	private void HideAnchors()
	{
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if ((bool)selectedCustomShape)
		{
			selectedCustomShape.HideAnchors();
		}
	}

	private void CreateGhostAnchor()
	{
		m_GhostAnchor = CustomShapes.CreateAnchor(Vector3.zero, null, Vector3.one);
		if ((bool)m_GhostAnchor)
		{
			UnityEngine.Object.DontDestroyOnLoad(m_GhostAnchor);
			Utils.SetLayerRecursively(m_GhostAnchor.gameObject, Utils.DEFAULT_LAYER);
			m_GhostAnchor.m_SpriteRenderer.color = GameUI.m_Instance.m_CustomShapeAddColor;
			m_GhostAnchor.gameObject.SetActive(value: false);
		}
	}

	private void StartPins()
	{
		CreateGhostPin();
	}

	private void UpdatePins()
	{
		UpdateDefaultColorForAllPins();
		UpdateHoverPin();
		UpdateGhostPin(GameInput.GetMousePosition());
		UpdateMovingPin(GameInput.GetMousePosition());
	}

	private void OnEnablePins()
	{
	}

	private void OnDisablePins()
	{
		if ((bool)m_LastRefreshedShape)
		{
			UpdateDefaultColorForAllPins();
			m_LastRefreshedShape.MovePinsToFrontOfMesh();
		}
		if ((bool)m_GhostPin)
		{
			m_GhostPin.gameObject.SetActive(value: false);
		}
		m_CreatedPinBeforeMoving = false;
		m_MovingPin = null;
	}

	public bool IsMovingPin()
	{
		if (!m_MovingPin)
		{
			return false;
		}
		return true;
	}

	private void EnterEditModePins()
	{
		ShowPins();
		ShowAnchorsDisabled();
		HideVerts();
	}

	private void ProcessInputPins()
	{
		if (GameInput.GetMouseButtonIsDown(0) && DeleteSubModeActive() && (bool)m_HoverPin)
		{
			DeletePin(m_HoverPin);
		}
		if (GameInput.GetMouseButtonJustPressed(0) && !DeleteSubModeActive())
		{
			if (m_GhostPin.gameObject.activeInHierarchy)
			{
				if (TryAddPin(m_GhostPin.transform.position))
				{
					m_CreatedPinBeforeMoving = true;
					UpdateHoverPin();
					StartMovingPin(m_HoverPin, GameInput.GetMousePosition());
				}
			}
			else
			{
				StartMovingPin(m_HoverPin, GameInput.GetMousePosition());
			}
		}
		if (GameInput.JustReleased(BindingType.DRAW_BUILD) && ((bool)m_MovingPin || m_CreatedPinBeforeMoving))
		{
			if (MouseHasMovedSinceStartDrag(GameInput.GetMousePosition()) || m_CreatedPinBeforeMoving)
			{
				SandboxUndo.SnapShot();
			}
			m_CreatedPinBeforeMoving = false;
			m_MovingPin = null;
			m_OffsetFromPointer = Vector2.zero;
		}
	}

	private void UpdateDefaultColorForAllPins()
	{
		if (m_LastRefreshedShape == null)
		{
			return;
		}
		foreach (CustomShapePin pin in m_LastRefreshedShape.m_Pins)
		{
			pin.SetColor(Color.white);
		}
	}

	private void UpdateHoverPin()
	{
		if (!m_MovingPin)
		{
			if ((bool)m_HoverPin)
			{
				m_HoverPin.SetColor(Color.white);
			}
			m_HoverPin = GetPinUnderMouse(GameInput.GetMousePosition());
		}
	}

	private void UpdateGhostPin(Vector2 mouseScreenPos)
	{
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if (!selectedCustomShape || DeleteSubModeActive() || GameInput.GetMouseButtonIsDown(0) || (bool)m_HoverPin)
		{
			m_GhostPin.gameObject.SetActive(value: false);
		}
		else if (IsMouseOverShape(selectedCustomShape, GameInput.GetMousePosition()))
		{
			m_GhostPin.gameObject.SetActive(value: true);
			m_GhostPin.transform.position = Utils.GetWorldPointFromScreenPos(m_OffsetFromPointer + mouseScreenPos);
			if (!MoveOffGrid())
			{
				m_GhostPin.transform.position = GameGrid.SnapPosToGrid(m_GhostPin.transform.position);
			}
		}
		else
		{
			m_GhostPin.gameObject.SetActive(value: false);
		}
	}

	private void UpdateMovingPin(Vector2 mouseScreenPos)
	{
		if (!m_MovingPin)
		{
			return;
		}
		CustomShape componentInParent = m_MovingPin.GetComponentInParent<CustomShape>();
		if ((bool)componentInParent && MouseHasMovedSinceStartDrag(mouseScreenPos))
		{
			Vector3 vector = Utils.GetWorldPointFromScreenPos(m_OffsetFromPointer + mouseScreenPos);
			if (!MoveOffGrid())
			{
				vector = GameGrid.SnapPosToGrid(vector);
			}
			vector = Utils.V3toV2(vector - componentInParent.transform.position) + Utils.V3toV2(componentInParent.transform.position);
			m_MovingPin.transform.position = Utils.V2toV3(vector);
			CustomShapes.UpdateCustomShapeMinimumStrengthHint(componentInParent);
		}
	}

	private CustomShapePin GetPinUnderMouse(Vector2 screenPos)
	{
		Collider closestRaycastHit = Utils.GetClosestRaycastHit(screenPos, Utils.CUSTOM_SHAPE_LAYER_MASK);
		if (closestRaycastHit == null)
		{
			return null;
		}
		if (closestRaycastHit.GetComponentInParent<CustomShape>() != SandboxSelectionSet.GetSelectedCustomShape())
		{
			return null;
		}
		return closestRaycastHit.transform.parent.GetComponent<CustomShapePin>();
	}

	private bool IsMouseOverShape(CustomShape shape, Vector2 screenPos)
	{
		if (shape == null)
		{
			return false;
		}
		Vector3 worldPointFromScreenPos = Utils.GetWorldPointFromScreenPos(screenPos);
		return shape.OverlapsPoint(worldPointFromScreenPos);
	}

	private bool TryAddPin(Vector3 worldPos)
	{
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if (!selectedCustomShape)
		{
			return false;
		}
		if (selectedCustomShape.m_Pins.Count == 1)
		{
			if (!GameUI.m_Instance.m_PopUpMessage.gameObject.activeInHierarchy)
			{
				PopUpMessage.DisplayWarningOkOnly(Localize.Get("WARN_STATIC_PINS_MAX"));
			}
			return false;
		}
		CustomShapePin customShapePin = selectedCustomShape.AddPin(worldPos);
		if (customShapePin != null)
		{
			customShapePin.ShowMesh(show: false);
			InterfaceAudio.Play("ui_menu_hover");
		}
		return customShapePin != null;
	}

	private void DeletePin(CustomShapePin pin)
	{
		if (!pin)
		{
			return;
		}
		CustomShape componentInParent = pin.GetComponentInParent<CustomShape>();
		if ((bool)componentInParent)
		{
			componentInParent.DestroyPin(pin);
			if (componentInParent.m_Pins.Count == 0 && componentInParent.m_Behavior == CustomShapeBehavior.MOTORIZED)
			{
				componentInParent.m_Behavior = CustomShapeBehavior.STATIC;
			}
			SandboxUndo.SnapShot();
		}
	}

	private void StartMovingPin(CustomShapePin pin, Vector2 mouseScreenPos)
	{
		m_MovingPin = pin;
		if (pin != null)
		{
			StartDrag(pin.transform.position, mouseScreenPos);
		}
	}

	private void ShowPins()
	{
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if ((bool)selectedCustomShape)
		{
			selectedCustomShape.ShowPins();
		}
	}

	private void ShowPinsDisabled()
	{
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if (!selectedCustomShape)
		{
			return;
		}
		ShowPins();
		foreach (CustomShapePin pin in selectedCustomShape.m_Pins)
		{
			pin.SetColor(GameUI.m_Instance.m_CustomShapeDisabledColor);
		}
	}

	private void HidePins()
	{
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if ((bool)selectedCustomShape)
		{
			selectedCustomShape.HidePins();
		}
	}

	private void CreateGhostPin()
	{
		m_GhostPin = CustomShapes.CreatePin(Vector3.zero, null, Vector3.one);
		if ((bool)m_GhostPin)
		{
			UnityEngine.Object.DontDestroyOnLoad(m_GhostPin);
			Utils.SetLayerRecursively(m_GhostPin.gameObject, Utils.DEFAULT_LAYER);
			m_GhostPin.SetColor(GameUI.m_Instance.m_CustomShapeAddColor);
			m_GhostPin.gameObject.SetActive(value: false);
			m_GhostPin.m_MeshRenderer.gameObject.SetActive(value: false);
		}
	}

	private void Start()
	{
		m_ResetShape.onClick.AddListener(OnResetShape);
		m_ButtonBack.onClick.AddListener(OnBack);
		m_EditShape.SetCallback(OnRadioButtonClicked, 0);
		m_EditStaticPins.SetCallback(OnRadioButtonClicked, 1);
		m_EditDynamicAnchors.SetCallback(OnRadioButtonClicked, 2);
		m_EditShape.LinkButton(m_EditStaticPins);
		m_EditShape.LinkButton(m_EditDynamicAnchors);
		m_EditStaticPins.LinkButton(m_EditShape);
		m_EditStaticPins.LinkButton(m_EditDynamicAnchors);
		m_EditDynamicAnchors.LinkButton(m_EditShape);
		m_EditDynamicAnchors.LinkButton(m_EditStaticPins);
		m_EditSubMode = CustomShapeEditSubMode.ADD_OR_MOVE;
		m_EditOrDeleteToggle.SetCallback(ModeRefresh);
		m_SnapToGridTogglePointerEvents = m_SnapToGridToggle.GetComponent<PointerEvents>();
		m_SnapToGridTogglePointerEvents.RegisterOnClickedDelegate(OnSnapToGridToggle);
		StartVerts();
		StartPins();
		StartAnchors();
	}

	private void Update()
	{
		ProcessInput();
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if (selectedCustomShape == null)
		{
			ExitCustomShapeEditToolsMode();
			return;
		}
		if ((bool)selectedCustomShape && selectedCustomShape != m_LastRefreshedShape)
		{
			OnBack();
			return;
		}
		switch (m_EditMode)
		{
		case CustomShapeEditMode.VERTS:
			ShowVerts();
			ProcessInputVerts();
			UpdateVerts();
			break;
		case CustomShapeEditMode.PINS:
			ShowPins();
			ProcessInputPins();
			UpdatePins();
			break;
		case CustomShapeEditMode.ANCHORS:
			ShowAnchors();
			ProcessInputAnchors();
			UpdateAnchors();
			break;
		default:
			UpdateDefaultColorForAllAnchors();
			UpdateDefaultColorForAllPins();
			break;
		}
		UpdateHelpText();
		m_EditOrDeleteToggle.UpdateManual();
	}

	private void OnEnable()
	{
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if ((bool)selectedCustomShape)
		{
			RefreshProperties(selectedCustomShape);
		}
		OnEnableVerts();
		OnEnablePins();
		OnEnableAnchors();
		m_HelpPanel.gameObject.SetActive(value: false);
		m_EditStaticPins.TurnOff();
		m_EditDynamicAnchors.TurnOff();
		m_EditShape.TurnOn();
		SetEditMode(CustomShapeEditMode.VERTS);
		m_EditSubMode = CustomShapeEditSubMode.ADD_OR_MOVE;
		m_EditOrDeleteToggle.SetStateImmediate(ToggleSliderState.OFF);
		UpdateHelpText();
		ActivePanels.Add(base.gameObject);
	}

	private void OnDisable()
	{
		if (!Main.m_ShuttingDown)
		{
			if ((bool)m_LastRefreshedShape)
			{
				m_LastRefreshedShape.RemoveAnchorsOutsideShape();
			}
			OnDisableVerts();
			OnDisablePins();
			OnDisableAnchors();
			m_EditSubMode = CustomShapeEditSubMode.ADD_OR_MOVE;
			ActivePanels.Remove(base.gameObject);
		}
	}

	public bool DeleteSubModeActive()
	{
		if (base.gameObject.activeInHierarchy)
		{
			return m_EditSubMode == CustomShapeEditSubMode.DELETE;
		}
		return false;
	}

	public void RefreshProperties(CustomShape shape)
	{
		if ((bool)shape)
		{
			m_LastRefreshedShape = shape;
		}
	}

	public void ExitCustomShapeEditToolsMode()
	{
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if ((bool)selectedCustomShape)
		{
			selectedCustomShape.EnableMeshRendering(on: false);
		}
		SetEditMode(CustomShapeEditMode.NONE);
		GameToolMode.SetMode(GameToolModeType.BUILD);
		GameUI.m_Instance.m_SandboxEditCustomShape.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_SandboxEditCustomShapeTools.gameObject.SetActive(value: false);
	}

	private void SetEditMode(CustomShapeEditMode mode)
	{
		switch (mode)
		{
		case CustomShapeEditMode.NONE:
			m_EditMode = CustomShapeEditMode.NONE;
			m_HelpPanel.SetActive(value: false);
			HideVerts();
			break;
		case CustomShapeEditMode.VERTS:
			EnterEditModeVerts();
			m_EditMode = CustomShapeEditMode.VERTS;
			break;
		case CustomShapeEditMode.PINS:
			EnterEditModePins();
			m_EditMode = CustomShapeEditMode.PINS;
			break;
		case CustomShapeEditMode.ANCHORS:
			EnterEditModeAnchors();
			m_EditMode = CustomShapeEditMode.ANCHORS;
			break;
		default:
			Debug.LogWarningFormat("Unrecognized custom shape edit mode {0}", mode.ToString());
			break;
		}
	}

	private void OnRadioButtonClicked(int index)
	{
		switch (index)
		{
		case 0:
			SetEditMode(CustomShapeEditMode.VERTS);
			break;
		case 1:
			SetEditMode(CustomShapeEditMode.PINS);
			break;
		case 2:
			SetEditMode(CustomShapeEditMode.ANCHORS);
			break;
		default:
			Debug.LogWarning($"Unexpected radio button index '{index}' in OnRadioButtonClicked");
			break;
		}
		InterfaceAudio.Play("ui_menu_select");
	}

	private void OnResetShape()
	{
		InterfaceAudio.Play("ui_settings_reset");
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if ((bool)selectedCustomShape)
		{
			SetEditMode(CustomShapeEditMode.NONE);
			GameUI.m_Instance.m_CustomShapeReset.gameObject.SetActive(value: true);
			GameUI.m_Instance.m_CustomShapeReset.m_CustomShape = selectedCustomShape;
		}
	}

	private void OnBack()
	{
		ExitCustomShapeEditToolsMode();
	}

	private void StartDrag(Vector3 worldPos, Vector2 mouseScreenPos)
	{
		Vector2 vector = (Vector2)Cameras.MainCamera().WorldToScreenPoint(worldPos) - mouseScreenPos;
		m_OffsetFromPointer = new Vector2(vector.x, vector.y);
		m_StartDragMouseScreenPos = mouseScreenPos;
	}

	private bool MouseHasMovedSinceStartDrag(Vector2 mouseScreenPos)
	{
		if (Mathf.RoundToInt(mouseScreenPos.x) != Mathf.RoundToInt(m_StartDragMouseScreenPos.x))
		{
			return true;
		}
		if (Mathf.RoundToInt(mouseScreenPos.y) != Mathf.RoundToInt(m_StartDragMouseScreenPos.y))
		{
			return true;
		}
		return false;
	}

	private void ProcessInput()
	{
		if (!GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			if (GameInput.JustPressed(BindingType.DELETE_SELECTION))
			{
				m_EditOrDeleteToggle.Toggle();
				InterfaceAudio.Play("ui_menu_select");
			}
			if (GameInput.IsDown(BindingType.ERASE) && m_EditOrDeleteToggle.GetState() == ToggleSliderState.OFF)
			{
				m_EditOrDeleteToggle.SetStateAnimated(ToggleSliderState.ON);
				InterfaceAudio.Play("ui_menu_select");
				m_ReturnToAddMoveSubmode = true;
			}
			if (GameInput.JustReleased(BindingType.ERASE) && m_ReturnToAddMoveSubmode && m_EditOrDeleteToggle.GetState() == ToggleSliderState.ON)
			{
				m_EditOrDeleteToggle.SetStateAnimated(ToggleSliderState.OFF);
				m_ReturnToAddMoveSubmode = false;
			}
			if (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
			{
				OnBack();
			}
		}
	}

	private void ModeRefresh()
	{
		if (m_EditOrDeleteToggle.GetState() == ToggleSliderState.ON)
		{
			m_EditSubMode = CustomShapeEditSubMode.DELETE;
			GameToolMode.SetMode(GameToolModeType.ERASE);
		}
		if (m_EditOrDeleteToggle.GetState() == ToggleSliderState.OFF)
		{
			m_EditSubMode = CustomShapeEditSubMode.ADD_OR_MOVE;
			GameToolMode.SetMode(GameToolModeType.BUILD);
		}
	}

	private void UpdateHelpText()
	{
		string locIDForHelpText = GetLocIDForHelpText(m_EditMode);
		if (string.IsNullOrEmpty(locIDForHelpText) || GameInput.GetActiveGameDevice() == GameDevice.Gamepad)
		{
			m_HelpPanel.gameObject.SetActive(value: false);
			return;
		}
		m_HelpPanel.gameObject.SetActive(value: true);
		string format = Localize.Get(locIDForHelpText);
		string arg = GameUI.MarkupForGold(Bindings.m_Bindings[BindingType.MOVE_OFF_GRID].GetTooltipBindingString());
		m_HelpText.text = string.Format(format, arg);
	}

	private string GetLocIDForHelpText(CustomShapeEditMode editMode)
	{
		return editMode switch
		{
			CustomShapeEditMode.VERTS => "UI_SANDBOX_CUSTOM_SHAPE_HELP_VERTS", 
			CustomShapeEditMode.PINS => "UI_SANDBOX_CUSTOM_SHAPE_HELP_PINS", 
			CustomShapeEditMode.ANCHORS => "UI_SANDBOX_CUSTOM_SHAPE_HELP_ANCHORS", 
			_ => string.Empty, 
		};
	}

	private bool MoveOffGrid()
	{
		return GameInput.IsDown(BindingType.MOVE_OFF_GRID);
	}

	private void OnSnapToGridToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
	}

	private void StartVerts()
	{
		CreateGhostVert();
	}

	private void UpdateVerts()
	{
		UpdateHoverVert();
		UpdateGhostVert(GameInput.GetMousePosition());
		UpdateMovingVert(GameInput.GetMousePosition());
	}

	private void OnEnableVerts()
	{
		GameToolMode.SetMode(GameToolModeType.BUILD);
	}

	private void OnDisableVerts()
	{
		if ((bool)m_LastRefreshedShape)
		{
			m_LastRefreshedShape.RecalculatePivot();
			m_LastRefreshedShape.RebuildCollider();
			m_LastRefreshedShape.HideVerts();
		}
		if ((bool)m_LastRefreshedShape && m_LastRefreshedShape.m_Dirty)
		{
			if (m_LastRefreshedShape.m_MeshId == CustomShapes.AUTO_GENERATED_MESH_ID)
			{
				m_LastRefreshedShape.RebuildMesh();
			}
			m_LastRefreshedShape.m_Dirty = false;
		}
		if ((bool)m_GhostVert)
		{
			m_GhostVert.gameObject.SetActive(value: false);
		}
		m_MovingVert = null;
	}

	public bool IsMovingVert()
	{
		if (!m_MovingVert)
		{
			return false;
		}
		return true;
	}

	private void EnterEditModeVerts()
	{
		m_HelpPanel.SetActive(value: true);
		ShowVerts();
		ShowPinsDisabled();
		ShowAnchorsDisabled();
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if (selectedCustomShape != null && selectedCustomShape.m_MeshId != CustomShapes.AUTO_GENERATED_MESH_ID)
		{
			selectedCustomShape.EnableMeshRendering(on: true);
		}
	}

	private void ProcessInputVerts()
	{
		if (GameInput.GetMouseButtonIsDown(0) && DeleteSubModeActive() && (bool)m_HoverVert)
		{
			DeleteVert(m_HoverVert);
		}
		if (GameInput.GetMouseButtonJustPressed(0) && !DeleteSubModeActive())
		{
			if (m_GhostVert.gameObject.activeInHierarchy)
			{
				TryAddVert(m_GhostVert.transform.position);
			}
			else
			{
				StartMovingVert(m_HoverVert, GameInput.GetMousePosition());
			}
		}
		if (!GameInput.JustReleased(BindingType.DRAW_BUILD))
		{
			return;
		}
		if ((bool)m_MovingVert)
		{
			if (MouseHasMovedSinceStartDrag(GameInput.GetMousePosition()))
			{
				SandboxUndo.SnapShot();
			}
			m_MovingVert = null;
			m_OffsetFromPointer = Vector2.zero;
		}
		m_LastValidPositionOfMovingVert = Vector3.zero;
	}

	private void TryAddVert(Vector3 pos)
	{
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if ((bool)selectedCustomShape)
		{
			if (selectedCustomShape.m_Verts.Count >= CustomShape.MAX_VERTS)
			{
				PopUpMessage.DisplayWarningOkOnly(string.Format(Localize.Get("WARN_SHAPE_MAX_VERTS"), CustomShape.MAX_VERTS));
				return;
			}
			AddVert(selectedCustomShape, new Vector3(pos.x, pos.y, selectedCustomShape.transform.position.z));
			InterfaceAudio.Play("ui_menu_hover");
			UpdateHoverVert();
			StartMovingVert(m_HoverVert, GameInput.GetMousePosition());
		}
	}

	private void UpdateHoverVert()
	{
		if (!m_MovingVert)
		{
			if ((bool)m_HoverVert)
			{
				m_HoverVert.m_SpriteRenderer.color = Color.white;
			}
			if (!GameUI.PopupIsActive())
			{
				m_HoverVert = GetVertUnderMouse(GameInput.GetMousePosition());
			}
		}
	}

	private void UpdateGhostVert(Vector2 mouseScreenPos)
	{
		if (GameUI.PopupIsActive())
		{
			m_GhostVert.gameObject.SetActive(value: false);
			return;
		}
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if (!selectedCustomShape || DeleteSubModeActive() || GameInput.GetMouseButtonIsDown(0) || (bool)m_HoverVert)
		{
			m_GhostVert.gameObject.SetActive(value: false);
			return;
		}
		Vector3 worldPointFromScreenPos = Utils.GetWorldPointFromScreenPos(mouseScreenPos);
		Vector3 nearestPosOnOutline = selectedCustomShape.GetNearestPosOnOutline(worldPointFromScreenPos);
		if (Vector2.Distance(worldPointFromScreenPos, nearestPosOnOutline) < 0.1f && !PositionOverlapsOtherVerts(selectedCustomShape, m_GhostVert, nearestPosOnOutline))
		{
			m_GhostVert.gameObject.SetActive(value: true);
			m_GhostVert.transform.position = nearestPosOnOutline;
		}
		else
		{
			m_GhostVert.gameObject.SetActive(value: false);
		}
	}

	private void UpdateMovingVert(Vector2 mouseScreenPos)
	{
		if (!m_MovingVert)
		{
			return;
		}
		CustomShape componentInParent = m_MovingVert.GetComponentInParent<CustomShape>();
		if (!componentInParent || !MouseHasMovedSinceStartDrag(mouseScreenPos))
		{
			return;
		}
		Vector3 worldPointFromScreenPos = Utils.GetWorldPointFromScreenPos(m_OffsetFromPointer + mouseScreenPos);
		Vector3 vector = new Vector3(worldPointFromScreenPos.x, worldPointFromScreenPos.y, componentInParent.transform.position.z);
		if (!MoveOffGrid())
		{
			vector = GameGrid.SnapPosToGrid(vector);
		}
		vector = (Vector3)Utils.V3toV2(vector - componentInParent.transform.position) + componentInParent.transform.position;
		if (!PositionOverlapsOtherVerts(componentInParent, m_MovingVert, vector))
		{
			m_MovingVert.transform.position = vector;
			if (PolygonUtil.AreVertsFormingAValidPolygon(componentInParent.GetVerticesInWorldSpace()))
			{
				m_LastValidPositionOfMovingVert = m_MovingVert.transform.position;
			}
			else
			{
				m_MovingVert.transform.position = m_LastValidPositionOfMovingVert;
			}
			componentInParent.RebuildCollider();
		}
	}

	private CustomShapeVert GetVertUnderMouse(Vector2 screenPos)
	{
		Collider closestRaycastHit = Utils.GetClosestRaycastHit(screenPos, Utils.CUSTOM_SHAPE_LAYER_MASK);
		if (!closestRaycastHit)
		{
			return null;
		}
		return closestRaycastHit.transform.parent.GetComponent<CustomShapeVert>();
	}

	private void AddVert(CustomShape shape, Vector3 worldPos)
	{
		CustomShapeEdge nearestEdge = shape.GetNearestEdge(worldPos);
		shape.InsertVertOnEdge(nearestEdge, worldPos);
		shape.RebuildCollider();
		shape.m_Dirty = true;
		SandboxUndo.SnapShot();
	}

	private void DeleteVert(CustomShapeVert vert)
	{
		if ((bool)vert)
		{
			CustomShape componentInParent = vert.GetComponentInParent<CustomShape>();
			if ((bool)componentInParent && componentInParent.m_Verts.Count > 3)
			{
				componentInParent.DestroyVert(vert);
				componentInParent.RebuildCollider();
				componentInParent.m_Dirty = true;
				InterfaceAudio.Play("ui_pop");
				SandboxUndo.SnapShot();
			}
		}
	}

	private void StartMovingVert(CustomShapeVert vert, Vector2 mouseScreenPos)
	{
		if (!vert)
		{
			return;
		}
		m_MovingVert = vert;
		m_LastValidPositionOfMovingVert = vert.transform.position;
		if (vert != null)
		{
			StartDrag(vert.transform.position, mouseScreenPos);
			CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
			if ((bool)selectedCustomShape)
			{
				selectedCustomShape.m_Dirty = true;
			}
		}
	}

	private void ShowVerts()
	{
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if ((bool)selectedCustomShape)
		{
			selectedCustomShape.ShowVerts();
		}
	}

	private void HideVerts()
	{
		CustomShape selectedCustomShape = SandboxSelectionSet.GetSelectedCustomShape();
		if ((bool)selectedCustomShape)
		{
			selectedCustomShape.HideVerts();
		}
	}

	private void CreateGhostVert()
	{
		m_GhostVert = CreateVert();
		if ((bool)m_GhostVert)
		{
			UnityEngine.Object.DontDestroyOnLoad(m_GhostVert);
			Utils.SetLayerRecursively(m_GhostVert.gameObject, Utils.DEFAULT_LAYER);
			m_GhostVert.m_SpriteRenderer.color = GameUI.m_Instance.m_CustomShapeAddColor;
			m_GhostVert.gameObject.SetActive(value: false);
		}
	}

	private CustomShapeVert CreateVert()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Prefabs.m_Instance.m_CustomShapeVert);
		if (!gameObject)
		{
			return null;
		}
		return gameObject.GetComponent<CustomShapeVert>();
	}

	private bool PositionOverlapsOtherVerts(CustomShape shape, CustomShapeVert movingVert, Vector2 targetPos)
	{
		foreach (CustomShapeVert vert in shape.m_Verts)
		{
			if (vert != movingVert && Vector2.Distance(targetPos, vert.transform.position) < VERT_MIN_DISTANCE)
			{
				return true;
			}
		}
		return false;
	}
}

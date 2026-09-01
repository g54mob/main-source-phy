using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel_SandboxEditBuildZone : MonoBehaviour
{
	[Header("Nudge")]
	public Panel_SandboxNudge m_SandboxNudge;

	[Header("Input Fields")]
	public SandboxInputField m_InputFieldPosX;

	public SandboxInputField m_InputFieldPosY;

	public SandboxInputField m_InputFieldWidth;

	public SandboxInputField m_InputFieldHeight;

	[Header("Toggles")]
	public Toggle m_LockPositionToggle;

	[Header("Buttons")]
	public Button m_Duplicate;

	public Button m_Delete;

	[Header("Sliders")]
	public SandboxTapeSlider m_SliderRot;

	public SandboxTapeSlider m_SliderWidth;

	public SandboxTapeSlider m_SliderHeight;

	[Header("Edit Mode")]
	private BuildZone m_LastRefreshedBuildZone;

	private PointerEvents m_LockPositionTogglePointerEvents;

	private bool m_SkipInputFieldUpdateFromSlider;

	private BuildZoneControlPoint m_HoverControlPoint;

	private BuildZoneControlPoint m_MovingControlPoint;

	private Vector2 m_OffsetFromPointer;

	private Vector2 m_StartDragMouseScreenPos;

	private readonly float VERT_MIN_DISTANCE = 0.01001f;

	private void Awake()
	{
		m_LockPositionTogglePointerEvents = m_LockPositionToggle.GetComponent<PointerEvents>();
		m_LockPositionTogglePointerEvents.RegisterOnClickedDelegate(OnLockPositionToggle);
		m_SliderRot.SetRange(-180f, 180f, 1f);
		m_SliderRot.SetCallback(RotSliderChanged);
		m_SliderWidth.SetRange(BuildZones.MIN_WIDTH_SLIDER, BuildZones.MAX_WIDTH_SLIDER, 0.5f);
		m_SliderWidth.SetCallback(WidthSliderChanged);
		m_SliderHeight.SetRange(BuildZones.MIN_HEIGHT_SLIDER, BuildZones.MAX_HEIGHT_SLIDER, 0.5f);
		m_SliderHeight.SetCallback(HeightSliderChanged);
	}

	private void Update()
	{
		BuildZone selectedBuildZone = SandboxSelectionSet.GetSelectedBuildZone();
		if ((bool)selectedBuildZone && selectedBuildZone != m_LastRefreshedBuildZone)
		{
			ExitEditMode(m_LastRefreshedBuildZone);
			RefreshProperties(selectedBuildZone);
			EnterEditMode(selectedBuildZone);
		}
		ProcessInput(selectedBuildZone);
		UpdateHoverVert();
		UpdateMovingControlPoint(GameInput.GetMousePosition());
		UpdateControlPointColors(selectedBuildZone);
	}

	private void OnEnable()
	{
		m_Duplicate.onClick.AddListener(OnDuplicate);
		m_Delete.onClick.AddListener(OnDelete);
		BuildZone selectedBuildZone = SandboxSelectionSet.GetSelectedBuildZone();
		if ((bool)selectedBuildZone)
		{
			RefreshProperties(selectedBuildZone);
			EnterEditMode(selectedBuildZone);
		}
	}

	private void OnDisable()
	{
		if ((bool)m_LastRefreshedBuildZone)
		{
			ExitEditMode(m_LastRefreshedBuildZone);
		}
		m_LastRefreshedBuildZone = null;
		m_SliderRot.m_SandboxInputField.m_ExternalContinuousHoldActive = false;
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
		m_LastRefreshedBuildZone = null;
	}

	public void RefreshProperties(BuildZone buildZone)
	{
		if ((bool)buildZone)
		{
			RefreshPosition(buildZone);
			RefreshWidthAndHeight(buildZone);
			RefreshSliders(buildZone);
			m_LockPositionToggle.isOn = buildZone.m_LockPosition;
			m_LastRefreshedBuildZone = buildZone;
		}
	}

	public void RefreshPosition(BuildZone buildZone)
	{
		m_InputFieldPosX.m_InputField.text = Utils.FormatThreeDecimalPlaces(buildZone.GetPosition().x);
		m_InputFieldPosY.m_InputField.text = Utils.FormatThreeDecimalPlaces(buildZone.GetPosition().y);
	}

	public void RefreshWidthAndHeight(BuildZone buildZone)
	{
		m_InputFieldWidth.gameObject.SetActive(buildZone.m_Type == BuildZoneType.RECTANGLE);
		m_InputFieldHeight.gameObject.SetActive(buildZone.m_Type == BuildZoneType.RECTANGLE);
		m_InputFieldWidth.m_InputField.text = Utils.FormatThreeDecimalPlaces(buildZone.GetSize().x);
		m_InputFieldHeight.m_InputField.text = Utils.FormatThreeDecimalPlaces(buildZone.GetSize().y);
	}

	public void RefreshSliders(BuildZone buildZone)
	{
		m_SliderRot.SetValue(buildZone.m_RotationDegrees);
		m_SliderRot.m_SandboxInputField.m_InputField.text = Utils.FormatAngle(buildZone.m_RotationDegrees);
		m_SliderWidth.SetValue(buildZone.GetSize().x);
		m_SliderWidth.m_SandboxInputField.m_InputField.text = Utils.FormatThreeDecimalPlaces(buildZone.GetSize().x);
		m_SliderHeight.SetValue(buildZone.GetSize().y);
		m_SliderHeight.m_SandboxInputField.m_InputField.text = Utils.FormatThreeDecimalPlaces(buildZone.GetSize().y);
	}

	public bool IsEditing()
	{
		if (!m_HoverControlPoint)
		{
			return m_MovingControlPoint;
		}
		return true;
	}

	private void OnDuplicate()
	{
		BuildZone selectedBuildZone = SandboxSelectionSet.GetSelectedBuildZone();
		if ((bool)selectedBuildZone)
		{
			BuildZone buildZone = selectedBuildZone.Duplicate(BuildZones.GetPrefabForType(selectedBuildZone.m_Type), new Vector3(selectedBuildZone.GetSize().x / 2f, (0f - selectedBuildZone.GetSize().y) / 2f, 0f));
			if ((bool)buildZone)
			{
				InterfaceAudio.Play("ui_build_generic_place");
				SandboxSelectionSet.ForceSelection(buildZone.m_SandboxItem);
				SandboxUndo.SnapShot();
			}
		}
	}

	private void OnDelete()
	{
		if ((bool)SandboxSelectionSet.GetSelectedBuildZone())
		{
			InterfaceAudio.Play("ui_build_delete");
			SandboxSelectionSet.Delete();
		}
	}

	private void OnLockPositionToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		BuildZone selectedBuildZone = SandboxSelectionSet.GetSelectedBuildZone();
		if ((bool)selectedBuildZone)
		{
			selectedBuildZone.m_LockPosition = m_LockPositionToggle.isOn;
			SandboxUndo.SnapShot();
		}
	}

	private void ProcessInput(BuildZone buildZone)
	{
		if (!buildZone || GameStateCommonInput.IgnoreKeyboardInput())
		{
			return;
		}
		m_SliderRot.m_SandboxInputField.ProcessInputForRotation();
		if (GameInput.GetMouseButtonJustPressed(0))
		{
			StartMovingControlPoint(m_HoverControlPoint, GameInput.GetMousePosition());
		}
		if (GameInput.JustReleased(BindingType.DRAW_BUILD) && (bool)m_MovingControlPoint)
		{
			if (MouseHasMovedSinceStartDrag(GameInput.GetMousePosition()))
			{
				buildZone.RecalculatePivot();
				buildZone.RecalculateGridOffset();
				buildZone.UpdateBuildZoneFromControlPoints();
				buildZone.m_SandboxItem.SetOutlineDirty(dirty: true);
				RefreshPosition(buildZone);
				SandboxUndo.SnapShot();
			}
			m_MovingControlPoint = null;
			m_OffsetFromPointer = Vector2.zero;
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

	private void RotSliderChanged(float angle)
	{
		BuildZone selectedBuildZone = SandboxSelectionSet.GetSelectedBuildZone();
		if ((bool)selectedBuildZone)
		{
			selectedBuildZone.m_RotationDegrees = angle % 360f;
			selectedBuildZone.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f - selectedBuildZone.m_RotationDegrees));
			if (selectedBuildZone.m_SandboxItem != null)
			{
				selectedBuildZone.m_SandboxItem.SetFloatingTextToDefaultPosition();
			}
			selectedBuildZone.m_SandboxItem.SetOutlineDirty(dirty: true);
			m_SliderRot.m_SandboxInputField.m_InputField.text = Utils.FormatAngle(selectedBuildZone.m_RotationDegrees);
		}
	}

	private void WidthSliderChanged(float width)
	{
		if (m_SkipInputFieldUpdateFromSlider)
		{
			m_SkipInputFieldUpdateFromSlider = false;
			return;
		}
		BuildZone selectedBuildZone = SandboxSelectionSet.GetSelectedBuildZone();
		if ((bool)selectedBuildZone)
		{
			width = Mathf.Clamp(width, BuildZones.MIN_WIDTH, BuildZones.MAX_WIDTH);
			selectedBuildZone.SetBounds(selectedBuildZone.GetPosition(), new Vector2(width, selectedBuildZone.GetSize().y));
			m_SliderWidth.m_SandboxInputField.m_InputField.text = Utils.FormatThreeDecimalPlaces(selectedBuildZone.GetSize().x);
		}
	}

	private void HeightSliderChanged(float height)
	{
		if (m_SkipInputFieldUpdateFromSlider)
		{
			m_SkipInputFieldUpdateFromSlider = false;
			return;
		}
		BuildZone selectedBuildZone = SandboxSelectionSet.GetSelectedBuildZone();
		if ((bool)selectedBuildZone)
		{
			height = Mathf.Clamp(height, BuildZones.MIN_HEIGHT, float.MaxValue);
			selectedBuildZone.SetBounds(selectedBuildZone.GetPosition(), new Vector2(selectedBuildZone.GetSize().x, height));
			m_SliderHeight.m_SandboxInputField.m_InputField.text = Utils.FormatThreeDecimalPlaces(selectedBuildZone.GetSize().y);
		}
	}

	private void EnterEditMode(BuildZone buildZone)
	{
		if (!(buildZone == null))
		{
			if (buildZone.m_ControlPoints.Count == 0)
			{
				buildZone.CreateControlPoints();
				buildZone.PositionControlPoints();
			}
			buildZone.EnterEditMode();
		}
	}

	private void ExitEditMode(BuildZone buildZone)
	{
		if (buildZone != null)
		{
			buildZone.ExitEditMode();
		}
	}

	private void UpdateHoverVert()
	{
		if (!m_MovingControlPoint && !GameUI.PopupIsActive())
		{
			m_HoverControlPoint = GetControlPointUnderMouse(GameInput.GetMousePosition());
		}
	}

	private void ComputeRectMovementLine(BuildZone buildZone, BuildZoneControlPoint controlPoint, ref Vector3 start, ref Vector3 end)
	{
		if (controlPoint.m_RectHandleType == BuildZoneRectHandleType.TOP)
		{
			Vector3 up = buildZone.transform.up;
			start = buildZone.GetControlPoint(BuildZoneRectHandleType.BOTTOM).transform.position + up * GameGrid.m_Spacing;
			end = buildZone.GetControlPoint(BuildZoneRectHandleType.BOTTOM).transform.position + up * BuildZones.MAX_HEIGHT;
		}
		else if (controlPoint.m_RectHandleType == BuildZoneRectHandleType.BOTTOM)
		{
			Vector3 vector = -buildZone.transform.up;
			start = buildZone.GetControlPoint(BuildZoneRectHandleType.TOP).transform.position + vector * GameGrid.m_Spacing;
			end = buildZone.GetControlPoint(BuildZoneRectHandleType.TOP).transform.position + vector * BuildZones.MAX_HEIGHT;
		}
		else if (controlPoint.m_RectHandleType == BuildZoneRectHandleType.LEFT)
		{
			Vector3 vector2 = -buildZone.transform.right;
			start = buildZone.GetControlPoint(BuildZoneRectHandleType.RIGHT).transform.position + vector2 * GameGrid.m_Spacing;
			end = buildZone.GetControlPoint(BuildZoneRectHandleType.RIGHT).transform.position + vector2 * BuildZones.MAX_WIDTH;
		}
		else if (controlPoint.m_RectHandleType == BuildZoneRectHandleType.RIGHT)
		{
			Vector3 right = buildZone.transform.right;
			start = buildZone.GetControlPoint(BuildZoneRectHandleType.LEFT).transform.position + right * GameGrid.m_Spacing;
			end = buildZone.GetControlPoint(BuildZoneRectHandleType.LEFT).transform.position + right * BuildZones.MAX_WIDTH;
		}
		else
		{
			Debug.LogError($"Unexpepcted Rect Handle Type: '{controlPoint.m_RectHandleType}'");
		}
	}

	private void UpdateMovingControlPoint(Vector2 mouseScreenPos)
	{
		if (!m_MovingControlPoint)
		{
			return;
		}
		BuildZone componentInParent = m_MovingControlPoint.GetComponentInParent<BuildZone>();
		if (!componentInParent || !MouseHasMovedSinceStartDrag(mouseScreenPos))
		{
			return;
		}
		Vector3 vector = Utils.GetWorldPointFromScreenPos(m_OffsetFromPointer + mouseScreenPos);
		if (m_MovingControlPoint.m_Restriction == BuildZoneControlPointRestriction.LOCAL_XAXIS || m_MovingControlPoint.m_Restriction == BuildZoneControlPointRestriction.LOCAL_YAXIS)
		{
			Vector3 start = Vector3.zero;
			Vector3 end = Vector3.zero;
			ComputeRectMovementLine(componentInParent, m_MovingControlPoint, ref start, ref end);
			vector = Utils.NearestPointOnLineSegment(start, end, vector);
		}
		if (!GameInput.IsDown(BindingType.MOVE_OFF_GRID))
		{
			vector = GameGrid.SnapPosToGrid(vector);
			vector = MaybeSnapToNearbyVert(componentInParent, vector);
		}
		vector = Utils.V3toV2(vector - componentInParent.transform.position) + Utils.V3toV2(componentInParent.transform.position);
		if (!PositionOverlapsOtherControlPoints(componentInParent, m_MovingControlPoint, vector))
		{
			m_MovingControlPoint.transform.position = Utils.V2toV3(vector);
			if (componentInParent.m_Type == BuildZoneType.TRIANGLE)
			{
				componentInParent.PutControlPointsInClockwiseOrder();
			}
			componentInParent.UpdateBuildZoneFromControlPoints();
			componentInParent.m_SandboxItem.SetOutlineDirty(dirty: true);
		}
	}

	private Vector3 MaybeSnapToNearbyVert(BuildZone buildZone, Vector3 targetPos)
	{
		foreach (BuildZone buildZone2 in BuildZones.m_BuildZones)
		{
			if (buildZone2 == buildZone)
			{
				continue;
			}
			Vector3[] vertsLocalSpace = buildZone2.m_VertsLocalSpace;
			foreach (Vector3 position in vertsLocalSpace)
			{
				Vector3 vector = buildZone2.transform.TransformPoint(position);
				if (Vector2.Distance(vector, targetPos) < GameGrid.m_Spacing)
				{
					return vector;
				}
			}
		}
		return targetPos;
	}

	private void StartMovingControlPoint(BuildZoneControlPoint vert, Vector2 mouseScreenPos)
	{
		if ((bool)vert)
		{
			m_MovingControlPoint = vert;
			if (vert != null)
			{
				StartDragControlPoint(vert.transform.position, mouseScreenPos);
				Game.ForceIgnoreNextSelection();
			}
		}
	}

	private BuildZoneControlPoint GetControlPointUnderMouse(Vector2 screenPos)
	{
		Collider closestRaycastHit = Utils.GetClosestRaycastHit(screenPos, Utils.JOINT_SELECTOR_LAYER_MASK);
		if (!closestRaycastHit)
		{
			return null;
		}
		return closestRaycastHit.transform.parent.GetComponent<BuildZoneControlPoint>();
	}

	private void StartDragControlPoint(Vector3 worldPos, Vector2 mouseScreenPos)
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

	private bool PositionOverlapsOtherControlPoints(BuildZone buildZone, BuildZoneControlPoint movingVert, Vector2 targetPos)
	{
		foreach (BuildZoneControlPoint controlPoint in buildZone.m_ControlPoints)
		{
			if (controlPoint != movingVert && Vector2.Distance(targetPos, controlPoint.transform.position) < VERT_MIN_DISTANCE)
			{
				return true;
			}
		}
		return false;
	}

	private void UpdateControlPointColors(BuildZone buildZone)
	{
		foreach (BuildZoneControlPoint controlPoint in buildZone.m_ControlPoints)
		{
			if (controlPoint == m_MovingControlPoint || controlPoint == m_HoverControlPoint)
			{
				controlPoint.m_SpriteRenderer.color = Color.green;
			}
			else
			{
				controlPoint.m_SpriteRenderer.color = Color.white;
			}
		}
	}
}

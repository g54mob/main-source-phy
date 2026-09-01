using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel_SandboxEditRamp : MonoBehaviour
{
	public RectTransform m_VerticalLayoutRectTransform;

	[Header("Nudge")]
	public Panel_SandboxNudge m_SandboxNudge;

	[Header("Input Fields")]
	public SandboxInputField m_InputFieldPosX;

	public SandboxInputField m_InputFieldPosY;

	[Header("Buttons")]
	public Button m_ButtonDuplicate;

	public Button m_ButtonDelete;

	[Header("Sliders")]
	public SandboxTapeSlider m_SliderHeight;

	[Header("Flip")]
	public Toggle m_FlipVerticalToggle;

	public Toggle m_FlipHorizontalToggle;

	public Toggle m_FlipLegsToggle;

	public Toggle m_HideLegsToggle;

	[Header("Edit Spline")]
	public SandboxToggleSlider m_ToggleSlider;

	public Button m_ButtonAddPoint;

	public Button m_ButtonDeletePoint;

	private PointerEvents m_FlipVerticalTogglePointerEvents;

	private PointerEvents m_FlipHorizontalTogglePointerEvents;

	private PointerEvents m_FlipLegsTogglePointerEvents;

	private PointerEvents m_HideLegsTogglePointerEvents;

	private Ramp m_LastRefreshedRamp;

	private bool m_RefreshEditModeLayout;

	private SplineControlPoint m_SelectedSplineControlPoint;

	private bool m_SelectedSplineControlPointFollowsMouse;

	private Vector2 m_OffsetFromPointer;

	private const float MIN_NODE_SEPARATION = 0.01f;

	private bool m_SkipInputFieldUpdateFromSlider;

	private void Awake()
	{
		m_FlipVerticalTogglePointerEvents = m_FlipVerticalToggle.GetComponent<PointerEvents>();
		m_FlipVerticalTogglePointerEvents.RegisterOnClickedDelegate(OnFlipVerticalToggle);
		m_FlipHorizontalTogglePointerEvents = m_FlipHorizontalToggle.GetComponent<PointerEvents>();
		m_FlipHorizontalTogglePointerEvents.RegisterOnClickedDelegate(OnFlipHorizontalToggle);
		m_FlipLegsTogglePointerEvents = m_FlipLegsToggle.GetComponent<PointerEvents>();
		m_FlipLegsTogglePointerEvents.RegisterOnClickedDelegate(OnFlipLegsToggle);
		m_HideLegsTogglePointerEvents = m_HideLegsToggle.GetComponent<PointerEvents>();
		m_HideLegsTogglePointerEvents.RegisterOnClickedDelegate(OnHideLegsToggle);
		m_ButtonDuplicate.onClick.AddListener(OnDuplicate);
		m_ButtonDelete.onClick.AddListener(OnDelete);
		m_ButtonAddPoint.onClick.AddListener(OnAddPoint);
		m_ButtonDeletePoint.onClick.AddListener(OnDeletePoint);
		m_SliderHeight.SetRange(Ramps.MIN_HEIGHT_SLIDER, Ramps.MAX_HEIGHT_SLIDER, 0.5f);
		m_SliderHeight.SetCallback(HeightSliderChanged);
		m_ToggleSlider.SetCallback(EditModeRefresh);
	}

	private void Update()
	{
		Ramp selectedRamp = SandboxSelectionSet.GetSelectedRamp();
		if ((bool)selectedRamp && selectedRamp != m_LastRefreshedRamp)
		{
			RefreshProperties(selectedRamp);
		}
		ProcessInput(selectedRamp);
		MoveControlPointWithMouse(GameInput.GetMousePosition());
		UpdateEditMode();
		if (m_RefreshEditModeLayout)
		{
			m_VerticalLayoutRectTransform.gameObject.SetActive(value: false);
			m_VerticalLayoutRectTransform.gameObject.SetActive(value: true);
			m_RefreshEditModeLayout = false;
		}
	}

	public void UpdateForCurrentDevice()
	{
		m_SandboxNudge.UpdateForCurrentDevice();
	}

	public void ForceRefresh()
	{
		m_LastRefreshedRamp = null;
	}

	public void EditModeRefresh()
	{
		m_RefreshEditModeLayout = true;
		if (m_ToggleSlider.GetState() == ToggleSliderState.ON)
		{
			EnterSplineEditMode();
		}
		if (m_ToggleSlider.GetState() == ToggleSliderState.OFF)
		{
			ExitSplineEditMode();
		}
	}

	private void UpdateEditMode()
	{
		bool active = m_ToggleSlider.GetState() == ToggleSliderState.ON || m_ToggleSlider.GetState() == ToggleSliderState.TRANSITION_OFF;
		m_ButtonAddPoint.gameObject.SetActive(active);
		m_ButtonDeletePoint.gameObject.SetActive(active);
	}

	private void OnEnable()
	{
		Ramp selectedRamp = SandboxSelectionSet.GetSelectedRamp();
		if ((bool)selectedRamp)
		{
			RefreshProperties(selectedRamp);
			m_ToggleSlider.SetStateImmediate(ToggleSliderState.OFF);
		}
	}

	private void OnDisable()
	{
		if (EditToggleIsOn())
		{
			ExitSplineEditMode();
		}
		m_LastRefreshedRamp = null;
	}

	public void SkipInputFieldUpdateFromSlider()
	{
		m_SkipInputFieldUpdateFromSlider = true;
	}

	public bool IsEditingSplinePoints()
	{
		if (base.gameObject.activeInHierarchy)
		{
			return EditToggleIsOn();
		}
		return false;
	}

	public void RefreshPosition(Ramp ramp)
	{
		m_InputFieldPosX.m_InputField.text = Utils.FormatThreeDecimalPlaces(ramp.transform.position.x);
		m_InputFieldPosY.m_InputField.text = Utils.FormatThreeDecimalPlaces(ramp.transform.position.y);
	}

	public void RefreshProperties(Ramp ramp)
	{
		if ((bool)ramp)
		{
			RefreshPosition(ramp);
			RefreshToggles(ramp);
			RefreshSliders(ramp);
			m_LastRefreshedRamp = ramp;
		}
	}

	public void OnDelete()
	{
		if ((bool)SandboxSelectionSet.GetSelectedRamp())
		{
			InterfaceAudio.Play("ui_build_delete");
			SandboxSelectionSet.Delete();
		}
	}

	public void OnDuplicate()
	{
		Ramp selectedRamp = SandboxSelectionSet.GetSelectedRamp();
		if ((bool)selectedRamp)
		{
			if (EditToggleIsOn())
			{
				ExitSplineEditMode();
			}
			Ramp ramp = selectedRamp.Duplicate(new Vector3(1f, -1f, 0f));
			if ((bool)ramp)
			{
				InterfaceAudio.Play("ui_build_terrain_place");
				SandboxSelectionSet.ForceSelection(ramp.m_SandboxItem);
				SandboxUndo.SnapShot();
			}
		}
	}

	private void OnFlipVerticalToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Ramp selectedRamp = SandboxSelectionSet.GetSelectedRamp();
		if ((bool)selectedRamp)
		{
			selectedRamp.m_FlippedVertical = m_FlipVerticalToggle.isOn;
			selectedRamp.FlipVertical();
			SandboxUndo.SnapShot();
		}
	}

	private void OnFlipHorizontalToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Ramp selectedRamp = SandboxSelectionSet.GetSelectedRamp();
		if ((bool)selectedRamp)
		{
			selectedRamp.m_FlippedHorizontal = m_FlipHorizontalToggle.isOn;
			selectedRamp.FlipHorizontal();
			SandboxUndo.SnapShot();
		}
	}

	private void OnFlipLegsToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Ramp selectedRamp = SandboxSelectionSet.GetSelectedRamp();
		if ((bool)selectedRamp)
		{
			selectedRamp.m_FlippedLegs = m_FlipLegsToggle.isOn;
			selectedRamp.RefreshLegs();
			SandboxUndo.SnapShot();
		}
	}

	private void OnHideLegsToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Ramp selectedRamp = SandboxSelectionSet.GetSelectedRamp();
		if ((bool)selectedRamp)
		{
			selectedRamp.m_HideLegs = m_HideLegsToggle.isOn;
			selectedRamp.RefreshMesh();
			SandboxUndo.SnapShot();
		}
	}

	private void RefreshToggles(Ramp ramp)
	{
		m_FlipVerticalToggle.isOn = ramp.m_FlippedVertical;
		m_FlipHorizontalToggle.isOn = ramp.m_FlippedHorizontal;
		m_FlipLegsToggle.isOn = ramp.m_FlippedLegs;
		m_HideLegsToggle.isOn = ramp.m_HideLegs;
	}

	private void RefreshSliders(Ramp ramp)
	{
		m_SliderHeight.SetValue(ramp.m_Height);
		m_SliderHeight.m_SandboxInputField.m_InputField.text = Utils.FormatDistance(ramp.m_Height);
	}

	private void ProcessInput(Ramp ramp)
	{
		if (!ramp || GameStateCommonInput.IgnoreKeyboardInput())
		{
			return;
		}
		if (GameInput.JustPressed(BindingType.FLIP_HORIZONTAL))
		{
			InterfaceAudio.Play("ui_settings_toggle");
			ramp.m_FlippedHorizontal = !ramp.m_FlippedHorizontal;
			m_FlipHorizontalToggle.isOn = ramp.m_FlippedHorizontal;
			ramp.FlipHorizontal();
		}
		if (GameInput.JustPressed(BindingType.FLIP_VERTICAL))
		{
			InterfaceAudio.Play("ui_settings_toggle");
			ramp.m_FlippedVertical = !ramp.m_FlippedVertical;
			m_FlipVerticalToggle.isOn = ramp.m_FlippedVertical;
			ramp.FlipVertical();
		}
		if (GameInput.GetMouseButtonJustPressed(0) && !GameUI.IsPointerOverGameObject())
		{
			SplineControlPoint controlPointUnderPos = GetControlPointUnderPos(GameInput.GetMousePosition());
			if ((bool)m_SelectedSplineControlPoint)
			{
				m_SelectedSplineControlPoint.DeSelect();
			}
			m_SelectedSplineControlPoint = controlPointUnderPos;
			if ((bool)m_SelectedSplineControlPoint)
			{
				InterfaceAudio.Play("ui_build_select");
				m_SelectedSplineControlPoint.Select();
				StartMovingSelection(m_SelectedSplineControlPoint, GameInput.GetMousePosition());
			}
		}
		if (GameInput.GetMouseButtonJustReleased(0) && m_SelectedSplineControlPointFollowsMouse)
		{
			m_SelectedSplineControlPointFollowsMouse = false;
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
	}

	public bool IsMovingControlPoint()
	{
		return m_SelectedSplineControlPointFollowsMouse;
	}

	public void ProcessDelete()
	{
		OnDeletePoint();
	}

	public void EnterSplineEditMode()
	{
		Ramp selectedRamp = SandboxSelectionSet.GetSelectedRamp();
		if ((bool)selectedRamp)
		{
			selectedRamp.ActivateControlPoints();
		}
		GameToolMode.SetMode(GameToolModeType.BUILD);
		m_SelectedSplineControlPointFollowsMouse = false;
	}

	public void ExitSplineEditMode()
	{
		if ((bool)m_LastRefreshedRamp)
		{
			m_LastRefreshedRamp.RefreshMesh();
			m_LastRefreshedRamp.DeActivateControlPoints();
			m_LastRefreshedRamp.m_SplineComputer.gameObject.layer = Utils.SCENEGEO_LAYER;
		}
		if (Cameras.MainCamera() != null)
		{
			Game.SetCameraCullingMasks(GameState.SANDBOX);
		}
	}

	private void OnAddPoint()
	{
		InterfaceAudio.Play("ui_build_select");
		Ramp selectedRamp = SandboxSelectionSet.GetSelectedRamp();
		if (!selectedRamp || !m_SelectedSplineControlPoint)
		{
			return;
		}
		int num = selectedRamp.m_ControlPoints.IndexOf(m_SelectedSplineControlPoint);
		if (num == -1)
		{
			return;
		}
		float num2 = ((num == selectedRamp.m_ControlPoints.Count - 1) ? float.MaxValue : (selectedRamp.m_ControlPoints[num + 1].transform.position.x - 0.01f - 0.01f));
		Vector3 position = m_SelectedSplineControlPoint.transform.position + new Vector3(1f, 0f, 0f);
		if (position.x > num2)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		GameObject gameObject = Object.Instantiate(Prefabs.m_Instance.m_SplineControlPoint, position, Quaternion.identity);
		if (!gameObject)
		{
			return;
		}
		SplineControlPoint component = gameObject.GetComponent<SplineControlPoint>();
		gameObject.name = Prefabs.m_Instance.m_SplineControlPoint.name;
		gameObject.transform.SetParent(selectedRamp.transform);
		int num3 = selectedRamp.m_ControlPoints.IndexOf(m_SelectedSplineControlPoint);
		if (num3 == -1)
		{
			return;
		}
		selectedRamp.m_ControlPoints.Insert(num3 + 1, component);
		List<Vector2> list = new List<Vector2>();
		foreach (SplineControlPoint controlPoint in selectedRamp.m_ControlPoints)
		{
			list.Add(Utils.V3toV2(controlPoint.transform.position - selectedRamp.transform.position));
		}
		selectedRamp.SetSplineComputerControlPoints(list);
		selectedRamp.RecalulateNumSegments();
		selectedRamp.RefreshLegs();
		m_SelectedSplineControlPoint.DeSelect();
		m_SelectedSplineControlPoint = component;
		m_SelectedSplineControlPoint.Select();
		SandboxUndo.SnapShot();
	}

	private void OnDeletePoint()
	{
		InterfaceAudio.Play("ui_build_delete");
		Ramp selectedRamp = SandboxSelectionSet.GetSelectedRamp();
		if (!selectedRamp || !m_SelectedSplineControlPoint || selectedRamp.m_ControlPoints.Count < 3 || selectedRamp.m_ControlPoints.IndexOf(m_SelectedSplineControlPoint) == -1)
		{
			return;
		}
		selectedRamp.m_ControlPoints.Remove(m_SelectedSplineControlPoint);
		Object.Destroy(m_SelectedSplineControlPoint.gameObject);
		m_SelectedSplineControlPoint = null;
		List<Vector2> list = new List<Vector2>();
		foreach (SplineControlPoint controlPoint in selectedRamp.m_ControlPoints)
		{
			list.Add(Utils.V3toV2(controlPoint.transform.position - selectedRamp.transform.position));
		}
		selectedRamp.SetSplineComputerControlPoints(list);
		selectedRamp.RecalulateNumSegments();
		selectedRamp.RefreshMesh();
		SandboxUndo.SnapShot();
	}

	private void StartMovingSelection(SplineControlPoint point, Vector2 mouseScreenPos)
	{
		m_SelectedSplineControlPointFollowsMouse = true;
		Vector2 vector = (Vector2)Cameras.MainCamera().WorldToScreenPoint(point.transform.position) - mouseScreenPos;
		m_OffsetFromPointer = new Vector2(vector.x, vector.y);
	}

	private void MoveControlPointWithMouse(Vector2 mouseScreenPos)
	{
		if (!m_SelectedSplineControlPointFollowsMouse || !m_SelectedSplineControlPoint)
		{
			return;
		}
		Ramp component = m_SelectedSplineControlPoint.transform.parent.gameObject.GetComponent<Ramp>();
		if (!component)
		{
			return;
		}
		int num = component.m_ControlPoints.IndexOf(m_SelectedSplineControlPoint);
		if (num != -1)
		{
			Vector3 worldPos = Cameras.MainCamera().ScreenToWorldPoint(m_OffsetFromPointer + mouseScreenPos);
			if (!GameInput.IsDown(BindingType.MOVE_OFF_GRID))
			{
				worldPos = GameGrid.SnapPosToGrid(worldPos);
			}
			if (SplineControlPointIsEndpoint(component, m_SelectedSplineControlPoint))
			{
				worldPos = GameGrid.SnapPosToGrid(worldPos);
			}
			float min = ((num == 0) ? float.MinValue : (component.m_ControlPoints[num - 1].transform.position.x + 0.01f));
			float max = ((num == component.m_ControlPoints.Count - 1) ? float.MaxValue : (component.m_ControlPoints[num + 1].transform.position.x - 0.01f));
			worldPos = new Vector3(Mathf.Clamp(worldPos.x, min, max), worldPos.y, worldPos.z);
			worldPos = Utils.V3toV2(worldPos - component.transform.position) + Utils.V3toV2(component.transform.position);
			m_SelectedSplineControlPoint.transform.position = Utils.V2toV3(worldPos);
			component.m_SplineComputer.SetPointPosition(component.m_ControlPoints.IndexOf(m_SelectedSplineControlPoint), m_SelectedSplineControlPoint.transform.position);
			component.RecalulateNumSegments();
			component.RefreshMesh();
		}
	}

	private bool SplineControlPointIsEndpoint(Ramp ramp, SplineControlPoint controlPoint)
	{
		if (ramp.m_ControlPoints.IndexOf(controlPoint) != 0)
		{
			return ramp.m_ControlPoints.IndexOf(controlPoint) == ramp.m_ControlPoints.Count - 1;
		}
		return true;
	}

	private SplineControlPoint GetControlPointUnderPos(Vector3 screenPos)
	{
		if (Physics.Raycast(Cameras.MainCamera().ScreenPointToRay(screenPos), out var hitInfo, float.MaxValue, Utils.SPLINE_CONTROL_POINT_MASK))
		{
			return hitInfo.transform.GetComponent<SplineControlPoint>();
		}
		return null;
	}

	private bool EditToggleIsOn()
	{
		if (m_ToggleSlider.GetState() != ToggleSliderState.ON)
		{
			return m_ToggleSlider.GetState() == ToggleSliderState.TRANSITION_OFF;
		}
		return true;
	}

	private void HeightSliderChanged(float height)
	{
		if (m_SkipInputFieldUpdateFromSlider)
		{
			m_SkipInputFieldUpdateFromSlider = false;
			return;
		}
		Ramp selectedRamp = SandboxSelectionSet.GetSelectedRamp();
		if ((bool)selectedRamp)
		{
			selectedRamp.m_Height = Mathf.Clamp(height, Ramps.MIN_HEIGHT, Ramps.MAX_HEIGHT);
			selectedRamp.RefreshMesh();
			m_SliderHeight.m_SandboxInputField.m_InputField.text = Utils.FormatDistance(selectedRamp.m_Height);
		}
	}
}

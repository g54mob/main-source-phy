using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventEditor
{
	public static EventStage m_PendingStage;

	public static float DEFAULT_ANCHOR_Y = 110f;

	public static float MAX_ANCHOR_Y = 600f;

	public static float MIN_ANCHOR_Y = 0f;

	private static EventUnit m_HoverIcon;

	private static EventUnit m_SelectedUnit;

	private static PointerEventData m_PointerEventData;

	private static List<RaycastResult> m_RaycastResults = new List<RaycastResult>();

	private static EventUnit m_MovingIcon;

	private static EventStage m_HoverStage;

	private static EventStage m_MovingStage;

	private static Vector2 m_PanOffset;

	private static bool m_AllowedToPan;

	private static Panel_EventEditor m_Panel;

	public static void Init()
	{
		m_PointerEventData = new PointerEventData(EventSystem.current);
		m_Panel = GameUI.m_Instance.m_EventEditor;
		EventTimelines.CreateTimeline();
	}

	public static void Clear()
	{
		m_Panel.m_CollapsePanel.Collapse();
		EventTimelines.Clear();
	}

	public static void ExitSandbox()
	{
		m_AllowedToPan = false;
	}

	public static void UpdateManual()
	{
		if (GameUI.m_Instance.m_LevelInfoLite.gameObject.activeInHierarchy)
		{
			if ((bool)m_MovingIcon)
			{
				ForceClearMovingIcon();
			}
			return;
		}
		ProcessInput();
		m_Panel.UpdateManual();
		UpdateHover();
		UpdateMovingIcon(m_MovingIcon);
		UpdateMovingStage(m_MovingStage);
		m_Panel.m_InsertLine.UpdateManual();
		UpdatePendingStage();
		MaybeHighlightStage();
		MaybeCancelSelection();
	}

	public static void CancelSelection()
	{
		SelectIcon(null);
	}

	public static bool IsIconMoving()
	{
		return m_MovingIcon != null;
	}

	public static EventUnit GetMovingIcon()
	{
		return m_MovingIcon;
	}

	public static bool IsStageMoving()
	{
		return m_MovingStage != null;
	}

	public static EventStage GetMovingStage()
	{
		return m_MovingStage;
	}

	public static bool IsPanning()
	{
		return m_AllowedToPan;
	}

	public static bool IsMovingCollapseBar()
	{
		return m_Panel.m_CollapseBar.IsMoving();
	}

	public static bool PointerOverEventStages()
	{
		return GameUI.PointerOver(typeof(Panel_EventStages));
	}

	public static bool PointerOverEventObjects()
	{
		return GameUI.PointerOver(typeof(Panel_EventObjects));
	}

	public static bool PointerOverEventEditor()
	{
		return GameUI.PointerOver(typeof(Panel_EventEditor));
	}

	public static bool PointerOverEventCanvas()
	{
		if (!PointerOverEventEditor())
		{
			return false;
		}
		m_PointerEventData.position = GameInput.GetMousePosition();
		m_RaycastResults.Clear();
		GameUI.m_Instance.m_Raycaster.Raycast(m_PointerEventData, m_RaycastResults);
		foreach (RaycastResult raycastResult in m_RaycastResults)
		{
			if ((bool)raycastResult.gameObject.GetComponent<RectMask2D>())
			{
				return true;
			}
		}
		return false;
	}

	public static EventStage GetStageUnderMouse(Vector2 pixelOffset)
	{
		m_PointerEventData.position = Utils.V3toV2(GameInput.GetMousePosition()) + pixelOffset;
		m_RaycastResults.Clear();
		GameUI.m_Instance.m_Raycaster.Raycast(m_PointerEventData, m_RaycastResults);
		foreach (RaycastResult raycastResult in m_RaycastResults)
		{
			EventStage componentInParent = raycastResult.gameObject.GetComponentInParent<EventStage>();
			if ((bool)componentInParent)
			{
				return componentInParent;
			}
		}
		return null;
	}

	public static void SelectIconMatchingGameObject(GameObject gameObject)
	{
		if (!m_SelectedUnit || !(m_SelectedUnit.m_SourceObject == gameObject))
		{
			if ((bool)m_SelectedUnit)
			{
				m_SelectedUnit.DeSelect();
			}
			EventUnit unitMatchingGameObject = EventTimelines.GetUnitMatchingGameObject(gameObject);
			if ((bool)unitMatchingGameObject)
			{
				SelectIcon(unitMatchingGameObject);
			}
		}
	}

	public static void SelectIcon(EventUnit unit)
	{
		if (m_SelectedUnit == unit)
		{
			if ((bool)m_SelectedUnit)
			{
				m_SelectedUnit.Select();
			}
			return;
		}
		if ((bool)m_SelectedUnit)
		{
			m_SelectedUnit.DeSelect();
			if ((bool)SandboxSelectionSet.GetSelectedVehicle() && SandboxSelectionSet.GetSelectedVehicle() == m_SelectedUnit.GetVehicle())
			{
				SandboxSelectionSet.CancelSelection();
			}
			if ((bool)SandboxSelectionSet.GetSelectedZedAxisVehicle() && SandboxSelectionSet.GetSelectedZedAxisVehicle() == m_SelectedUnit.GetZedAxisVehicle())
			{
				SandboxSelectionSet.CancelSelection();
			}
		}
		m_SelectedUnit = unit;
		if ((bool)m_SelectedUnit)
		{
			m_SelectedUnit.Select();
		}
	}

	public static void StartMovingIcon(EventUnit unit, Vector3 mousePosition)
	{
		if (!m_MovingIcon && (bool)unit)
		{
			m_MovingIcon = unit;
			Vector3 vector = unit.m_Icon.transform.position - mousePosition;
			unit.m_OffsetFromPointer = new Vector3(vector.x, vector.y, 0f);
			unit.m_StartMovementPos = Utils.V3toV2(unit.m_Icon.transform.position);
			unit.m_Icon.raycastTarget = false;
			unit.m_Icon.maskable = false;
			unit.m_Icon.transform.SetParent(GameUI.m_Instance.m_EventEditor.m_StagesRectTransform);
		}
	}

	public static void StartMovingStage(EventStage stage, Vector3 mousePosition)
	{
		if (!m_MovingStage && (bool)stage)
		{
			m_MovingStage = stage;
			Vector3 vector = stage.transform.position - mousePosition;
			stage.m_OffsetFromPointer = new Vector3(vector.x, vector.y, 0f);
			stage.m_StartMovementPos = Utils.V3toV2(stage.transform.position);
			stage.MakeMaskable(maskable: false);
			stage.MakeRaycastTarget(raycastTarget: false);
			stage.transform.SetParent(GameUI.m_Instance.m_EventEditor.m_StagesRectTransform);
			GameUI.m_Instance.m_EventEditor.ToggleViewportRectMask();
		}
	}

	public static void CreatePendingStage(GameObject gameObject, EventUnitType eventUnitType)
	{
		if (EventTimelines.m_Timelines.Count > 0)
		{
			m_PendingStage = EventTimelines.m_Timelines[0].AddStage();
			m_PendingStage.AddUnit(gameObject, eventUnitType);
			m_PendingStage.UpdateManual();
		}
	}

	public static void RemoveUnit(GameObject gameObject)
	{
		foreach (EventTimeline timeline in EventTimelines.m_Timelines)
		{
			foreach (EventStage stage in timeline.m_Stages)
			{
				foreach (EventUnit unit in stage.m_Units)
				{
					if (unit.m_SourceObject == gameObject)
					{
						if (m_MovingIcon == unit)
						{
							ForceClearMovingIcon();
						}
						stage.DestroyUnit(unit);
						break;
					}
				}
			}
		}
	}

	public static bool CanPlaceUnitOnStage(EventUnit unit, EventStage stage)
	{
		if (!unit || !stage)
		{
			return false;
		}
		if ((bool)unit.GetHydraulicsPhase() && stage.ContainsHydraulicsPhase() && unit.m_ParentStage != stage)
		{
			return false;
		}
		return true;
	}

	public static bool CanPlaceUnitOnTimeline(EventUnit unit, EventTimeline timeline)
	{
		if (!unit || !timeline)
		{
			return false;
		}
		if (unit.m_Type == EventUnitType.NONE)
		{
			return false;
		}
		Vehicle vehicle = unit.GetVehicle();
		if ((bool)vehicle)
		{
			foreach (Checkpoint checkpoint in vehicle.m_Checkpoints)
			{
				if ((bool)checkpoint && checkpoint.m_TriggerTimeline && (bool)checkpoint.m_Timeline && (checkpoint.m_Timeline == timeline || checkpoint.m_Timeline.HasChild(timeline)))
				{
					return false;
				}
			}
		}
		return true;
	}

	public static bool CanPlaceStageOnTimeline(EventStage stage, EventTimeline timeline)
	{
		if (!stage || !timeline)
		{
			return false;
		}
		foreach (EventUnit unit in stage.m_Units)
		{
			if (!CanPlaceUnitOnTimeline(unit, timeline))
			{
				return false;
			}
		}
		return true;
	}

	public static bool SelectedUnitIsVehicleWithCheckpoint(Checkpoint checkpoint)
	{
		if (!m_SelectedUnit)
		{
			return false;
		}
		Vehicle vehicle = m_SelectedUnit.GetVehicle();
		if ((bool)vehicle)
		{
			return vehicle.m_Checkpoints.Contains(checkpoint);
		}
		return false;
	}

	public static void DestroyPendingStage()
	{
		ClearMovingIcon();
		if (m_PendingStage != null)
		{
			m_PendingStage.ClearAndDestroySourceObjects();
			Object.Destroy(m_PendingStage.gameObject);
			m_PendingStage = null;
			m_MovingIcon = null;
		}
	}

	private static void ProcessInput()
	{
		if (ActivePanels.m_Panels.Count > 0)
		{
			return;
		}
		if (GameInput.GetMouseButtonJustPressed(0))
		{
			if (PointerOverEventCanvas())
			{
				m_PanOffset = GameInput.GetMousePosition() - m_Panel.m_RootCanvas.transform.position;
				m_AllowedToPan = true;
			}
			if ((bool)m_HoverIcon)
			{
				StartMovingIcon(m_HoverIcon, GameInput.GetMousePosition());
			}
			else if ((bool)m_HoverStage)
			{
				StartMovingStage(m_HoverStage, GameInput.GetMousePosition());
			}
		}
		if (GameInput.GetMouseButtonJustReleased(0))
		{
			if (!m_HoverIcon && !m_MovingIcon && !m_HoverStage && PointerOverEventEditor())
			{
				CancelSelection();
			}
			if ((bool)m_HoverIcon && !IconMovedFromStartPos(m_HoverIcon))
			{
				SelectIcon(m_HoverIcon);
				SelectIconSandboxItem(m_HoverIcon);
			}
			else if ((bool)m_MovingStage && !StageMovedFromStartPos(m_MovingStage) && m_MovingStage.m_Units.Count == 1)
			{
				SelectIcon(m_MovingStage.m_Units[0]);
				SelectIconSandboxItem(m_MovingStage.m_Units[0]);
				ClearMovingStage();
			}
			if ((bool)m_MovingIcon)
			{
				DropIcon();
			}
			if ((bool)m_MovingStage)
			{
				DropStage();
			}
			m_AllowedToPan = false;
		}
	}

	private static void SelectIconSandboxItem(EventUnit unit)
	{
		SandboxItem sandboxItemFromSelectedIcon = GetSandboxItemFromSelectedIcon(unit);
		if ((bool)sandboxItemFromSelectedIcon)
		{
			SandboxSelectionSet.CancelSelection();
			SandboxSelectionSet.SelectItem(sandboxItemFromSelectedIcon);
			GameUI.m_Instance.m_SandboxMenu.MaybeActivateEditSubmenu();
		}
	}

	public static void DropStage()
	{
		if (m_MovingStage == null)
		{
			return;
		}
		if (GameUI.m_Instance.m_EventEditor.m_InsertLine.IsActive())
		{
			InsertStageBetweenStages(m_MovingStage);
		}
		else
		{
			EventStage stageUnderMouse = GetStageUnderMouse(Vector2.zero);
			if ((bool)stageUnderMouse && CanPlaceStageOnTimeline(m_MovingStage, stageUnderMouse.m_ParentTimeline))
			{
				CombineStages(m_MovingStage, stageUnderMouse);
			}
		}
		ClearMovingStage();
		m_PendingStage = null;
	}

	public static void DropIcon()
	{
		if (m_MovingIcon == null)
		{
			return;
		}
		if (IconMovedFromStartPos(m_MovingIcon))
		{
			EventStage stageUnderMouse = GetStageUnderMouse(Vector2.zero);
			if ((bool)stageUnderMouse)
			{
				TryDropIconOnStage(m_MovingIcon, stageUnderMouse);
			}
			else
			{
				DropIconOnEmptySpace(m_MovingIcon);
			}
		}
		ClearMovingIcon();
		m_PendingStage = null;
	}

	public static void ForceClearMovingIcon()
	{
		DropIconOnEmptySpace(m_MovingIcon);
		ClearMovingIcon();
		m_PendingStage = null;
	}

	private static void ClearMovingIcon()
	{
		if (m_MovingIcon != null)
		{
			m_MovingIcon.m_Icon.transform.SetParent(m_MovingIcon.transform);
			m_MovingIcon.m_Icon.transform.localPosition = Vector3.zero;
			m_MovingIcon.transform.localScale = Vector3.one;
			m_MovingIcon.transform.localPosition = Vector3.zero;
			m_MovingIcon.m_Icon.raycastTarget = true;
			m_MovingIcon.m_Icon.maskable = true;
			m_MovingIcon.m_OffsetFromPointer = Vector2.zero;
			if (m_MovingIcon.m_Type == EventUnitType.VEHICLE_RESTART_PHASE && (bool)m_MovingIcon.m_SourceObject)
			{
				m_MovingIcon.AdjustIconForVehicleRestart();
			}
			m_MovingIcon.m_Icon.gameObject.SetActive(value: false);
			m_MovingIcon.m_Icon.gameObject.SetActive(value: true);
			LayoutRebuilder.MarkLayoutForRebuild(m_MovingIcon.m_ParentStage.m_GridLayoutGroup.GetComponent<RectTransform>());
			m_MovingIcon = null;
		}
	}

	private static void ClearMovingStage()
	{
		if (m_MovingStage != null)
		{
			m_MovingStage.transform.SetParent(m_MovingStage.m_ParentTimeline.m_HorizontalLayoutGroup.transform);
			m_MovingStage.transform.localPosition = Vector3.zero;
			m_MovingStage.MakeMaskable(maskable: true);
			m_MovingStage.MakeRaycastTarget(raycastTarget: true);
			m_MovingStage.m_OffsetFromPointer = Vector2.zero;
			m_MovingStage.m_ParentTimeline.SyncHierarchy();
			LayoutRebuilder.MarkLayoutForRebuild(m_MovingStage.m_ParentTimeline.m_HorizontalLayoutGroup.GetComponent<RectTransform>());
			m_MovingStage = null;
			GameUI.m_Instance.m_EventEditor.ToggleViewportRectMask();
		}
	}

	private static void TryDropIconOnStage(EventUnit unit, EventStage stage)
	{
		if ((bool)unit)
		{
			if (EventTimelines.CalculateNumStages() > EventTimelines.MAX_STAGES && stage == m_PendingStage)
			{
				DestroyPendingStage();
			}
			else if (CanPlaceUnitOnTimeline(unit, stage.m_ParentTimeline) && CanPlaceUnitOnStage(unit, stage))
			{
				DropIconOnStage(unit, stage);
			}
		}
	}

	private static void DropIconOnStage(EventUnit unit, EventStage stage)
	{
		if (unit.m_ParentStage != stage)
		{
			TransferIcon(unit, stage);
			m_PendingStage = null;
			SandboxUndo.SnapShot();
		}
		unit.m_Icon.transform.localPosition = Vector3.zero;
		LayoutRebuilder.MarkLayoutForRebuild(stage.m_GridLayoutGroup.GetComponent<RectTransform>());
	}

	private static void TransferIcon(EventUnit unit, EventStage targetStage)
	{
		unit.m_ParentStage.m_Units.Remove(unit);
		targetStage.m_Units.Add(unit);
		unit.m_ParentStage = targetStage;
		unit.transform.SetParent(targetStage.m_IconsParent.transform);
	}

	private static void DropIconOnEmptySpace(EventUnit unit)
	{
		if (EventTimelines.CalculateNumStages() > EventTimelines.MAX_STAGES)
		{
			DestroyPendingStage();
		}
		else if (GameUI.m_Instance.m_EventEditor.m_InsertLine.IsActive())
		{
			InsertIconBetweenStages(unit);
			m_PendingStage = null;
			SandboxUndo.SnapShot();
		}
		else
		{
			ClearMovingIcon();
			m_PendingStage = null;
		}
	}

	private static void InsertIconBetweenStages(EventUnit unit)
	{
		EventStage insertLeftStage = GameUI.m_Instance.m_EventEditor.m_InsertLine.GetInsertLeftStage();
		EventStage insertRightStage = GameUI.m_Instance.m_EventEditor.m_InsertLine.GetInsertRightStage();
		if ((bool)insertLeftStage && (bool)insertRightStage)
		{
			EventTimeline parentTimeline = insertLeftStage.m_ParentTimeline;
			if (CanPlaceUnitOnTimeline(unit, parentTimeline))
			{
				int stageIndex = Mathf.Max(parentTimeline.m_Stages.IndexOf(insertLeftStage), parentTimeline.m_Stages.IndexOf(insertRightStage));
				InsertIconToNewStage(unit, parentTimeline, stageIndex);
			}
		}
		else if ((bool)insertRightStage)
		{
			EventTimeline parentTimeline2 = insertRightStage.m_ParentTimeline;
			if (CanPlaceUnitOnTimeline(unit, parentTimeline2))
			{
				InsertIconToNewStage(unit, parentTimeline2, 0);
			}
		}
	}

	private static void InsertStageBetweenStages(EventStage stage)
	{
		EventStage insertLeftStage = GameUI.m_Instance.m_EventEditor.m_InsertLine.GetInsertLeftStage();
		EventStage insertRightStage = GameUI.m_Instance.m_EventEditor.m_InsertLine.GetInsertRightStage();
		if (!insertLeftStage && !insertRightStage)
		{
			return;
		}
		EventTimeline eventTimeline = (insertLeftStage ? insertLeftStage.m_ParentTimeline : insertRightStage.m_ParentTimeline);
		if (CanPlaceStageOnTimeline(stage, eventTimeline))
		{
			stage.m_ParentTimeline.m_Stages.Remove(stage);
			if (insertLeftStage == null)
			{
				eventTimeline.m_Stages.Insert(0, stage);
			}
			else
			{
				eventTimeline.m_Stages.Insert(eventTimeline.m_Stages.IndexOf(insertLeftStage) + 1, stage);
			}
			stage.m_ParentTimeline = eventTimeline;
			stage.transform.SetParent(eventTimeline.m_HorizontalLayoutGroup.transform);
			eventTimeline.SyncHierarchy();
		}
	}

	private static void CombineStages(EventStage droppedStage, EventStage baseStage)
	{
		List<EventUnit> list = new List<EventUnit>();
		List<EventUnit> list2 = new List<EventUnit>();
		bool flag = baseStage.ContainsHydraulicsPhase();
		foreach (EventUnit unit in droppedStage.m_Units)
		{
			if (unit.m_Type == EventUnitType.HYDRAULICS_PHASE && flag)
			{
				list2.Add(unit);
			}
			else
			{
				list.Add(unit);
			}
		}
		foreach (EventUnit item in list)
		{
			TransferIcon(item, baseStage);
		}
		foreach (EventUnit item2 in list2)
		{
			droppedStage.DestroyUnit(item2);
		}
		baseStage.MakeMaskable(maskable: true);
		baseStage.MakeRaycastTarget(raycastTarget: true);
		droppedStage.m_ParentTimeline.CullEmptyStages(null);
		GameUI.m_Instance.m_EventEditor.ToggleViewportRectMask();
	}

	private static void InsertIconToNewStage(EventUnit unit, EventTimeline timeline, int stageIndex)
	{
		if (EventTimelines.m_Timelines.Count > 0)
		{
			EventStage destStage = timeline.InsertStage(stageIndex);
			timeline.SyncHierarchy();
			unit.m_ParentStage.MoveUnit(unit, destStage);
		}
	}

	private static SandboxItem GetSandboxItemFromSelectedIcon(EventUnit unit)
	{
		if (unit == null || unit.m_SourceObject == null || unit.m_Type == EventUnitType.NONE)
		{
			return null;
		}
		return unit.m_SourceObject.GetComponent<SandboxItem>();
	}

	private static void UpdateHover()
	{
		UpdateHoverIcon();
		UpdateHoverStage();
	}

	private static void UpdateHoverIcon()
	{
		EventUnit eventUnit = GetIconUnderPointer();
		if (ActivePanels.m_Panels.Count > 0)
		{
			eventUnit = null;
		}
		if (eventUnit != m_HoverIcon)
		{
			if ((bool)m_HoverIcon)
			{
				m_HoverIcon.UnHover();
			}
			m_HoverIcon = eventUnit;
			if ((bool)m_HoverIcon && !m_MovingIcon && !m_MovingStage)
			{
				m_HoverIcon.Hover();
			}
		}
	}

	private static void UpdateHoverStage()
	{
		EventStage eventStage = GetStageHeaderUnderPointer();
		if (ActivePanels.m_Panels.Count > 0)
		{
			eventStage = null;
		}
		if (eventStage != m_HoverStage)
		{
			if ((bool)m_HoverStage)
			{
				m_HoverStage.HightlightOff();
			}
			m_HoverStage = eventStage;
		}
		if ((bool)m_HoverStage && !m_MovingIcon && !m_MovingStage)
		{
			m_HoverStage.HightlightOn(GameUI.m_Instance.m_GoldColor);
		}
	}

	private static EventUnit GetIconUnderPointer()
	{
		m_PointerEventData.position = GameInput.GetMousePosition();
		m_RaycastResults.Clear();
		GameUI.m_Instance.m_Raycaster.Raycast(m_PointerEventData, m_RaycastResults);
		foreach (RaycastResult raycastResult in m_RaycastResults)
		{
			if ((bool)raycastResult.gameObject.transform.parent && (bool)raycastResult.gameObject.transform.parent.GetComponent<EventUnit>())
			{
				return raycastResult.gameObject.transform.parent.GetComponent<EventUnit>();
			}
		}
		return null;
	}

	private static EventStage GetStageHeaderUnderPointer()
	{
		EventStage stageUnderMouse = GetStageUnderMouse(Vector2.zero);
		if (stageUnderMouse != null && GameUI.PointerOver(typeof(EventStageHeader)))
		{
			return stageUnderMouse;
		}
		return null;
	}

	private static void UpdateMovingIcon(EventUnit unit)
	{
		if (!unit)
		{
			return;
		}
		unit.m_Icon.transform.position = unit.m_OffsetFromPointer + GameInput.GetMousePosition();
		if (IconMovedFromStartPos(unit))
		{
			if (m_SelectedUnit == unit)
			{
				m_SelectedUnit.DeSelect();
				m_SelectedUnit = null;
			}
			unit.UnHover();
			EventTimeline closestTimelineToPointer = m_Panel.GetClosestTimelineToPointer(GameInput.GetMousePosition());
			if (m_PendingStage == null)
			{
				m_PendingStage = closestTimelineToPointer.AddStage();
				m_PendingStage.UpdateManual();
			}
			else if (m_PendingStage.m_ParentTimeline != closestTimelineToPointer)
			{
				m_PendingStage.m_ParentTimeline.m_Stages.Remove(m_PendingStage);
				closestTimelineToPointer.m_Stages.Add(m_PendingStage);
				m_PendingStage.AssignParentTimeline(closestTimelineToPointer);
			}
		}
	}

	private static void UpdateMovingStage(EventStage stage)
	{
		if ((bool)stage)
		{
			stage.transform.position = stage.m_OffsetFromPointer + GameInput.GetMousePosition();
		}
	}

	public static void UpdatePendingStage()
	{
		if ((bool)m_PendingStage)
		{
			SetPendingStageColor();
		}
	}

	private static void SetPendingStageColor()
	{
		if ((bool)m_PendingStage)
		{
			if ((bool)m_MovingIcon && !CanPlaceUnitOnTimeline(m_MovingIcon, m_PendingStage.m_ParentTimeline))
			{
				m_PendingStage.m_Outline.color = m_PendingStage.m_ParentTimeline.m_OutlineErrorColor;
			}
			else
			{
				m_PendingStage.m_Outline.color = m_PendingStage.m_ParentTimeline.m_OutlineDisabledColor;
			}
			if (m_PendingStage.m_Outline.color != m_PendingStage.m_ParentTimeline.m_OutlineDisabledColor)
			{
				m_PendingStage.m_ParentTimeline.m_Outline.color = m_PendingStage.m_Outline.color;
			}
		}
	}

	private static void MaybeHighlightStage()
	{
		if ((bool)m_MovingIcon || (bool)SandboxItems.m_NewUnPlacedItem)
		{
			EventStage stageUnderMouse = GetStageUnderMouse(Vector2.zero);
			if (!stageUnderMouse)
			{
				return;
			}
			if ((bool)m_MovingIcon && (!CanPlaceUnitOnTimeline(m_MovingIcon, stageUnderMouse.m_ParentTimeline) || !CanPlaceUnitOnStage(m_MovingIcon, stageUnderMouse)))
			{
				stageUnderMouse.m_Outline.color = stageUnderMouse.GetDefaultOutlineColor();
			}
			else if ((bool)m_MovingIcon && IconMovedFromStartPos(m_MovingIcon))
			{
				stageUnderMouse.HightlightOn(stageUnderMouse.m_ParentTimeline.m_OutlineHightlightColor);
			}
		}
		if (!m_MovingStage)
		{
			return;
		}
		EventStage stageUnderMouse2 = GetStageUnderMouse(Vector2.zero);
		if ((bool)stageUnderMouse2)
		{
			if (!CanPlaceStageOnTimeline(m_MovingStage, stageUnderMouse2.m_ParentTimeline))
			{
				stageUnderMouse2.m_Outline.color = stageUnderMouse2.GetDefaultOutlineColor();
			}
			else
			{
				stageUnderMouse2.HightlightOn(stageUnderMouse2.m_ParentTimeline.m_OutlineHightlightColor);
			}
		}
	}

	private static void MaybeCancelSelection()
	{
		if ((bool)m_SelectedUnit && m_SelectedUnit.m_SourceObject != null && !SandboxSelectionSet.IsSelected(m_SelectedUnit.m_SourceObject.GetComponent<SandboxItem>()))
		{
			CancelSelection();
		}
	}

	private static bool IconMovedFromStartPos(EventUnit unit)
	{
		if (unit == null)
		{
			return false;
		}
		int value = Mathf.FloorToInt(unit.m_Icon.transform.position.x - unit.m_StartMovementPos.x);
		int value2 = Mathf.FloorToInt(unit.m_Icon.transform.position.y - unit.m_StartMovementPos.y);
		if (Mathf.Abs(value) <= 1)
		{
			return Mathf.Abs(value2) > 1;
		}
		return true;
	}

	private static bool StageMovedFromStartPos(EventStage stage)
	{
		int value = Mathf.FloorToInt(stage.transform.position.x - stage.m_StartMovementPos.x);
		int value2 = Mathf.FloorToInt(stage.transform.position.y - stage.m_StartMovementPos.y);
		if (Mathf.Abs(value) <= 1)
		{
			return Mathf.Abs(value2) > 1;
		}
		return true;
	}
}
